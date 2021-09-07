using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {
    
	/// <summary>
	/// Provides <see cref="CodeLensTag"/> objects over text ranges.
	/// </summary>
	public class CodeLensTagger : TaggerBase<IIntraLineSpacerTag> {

		private readonly List<CodeLensDeclaration> cachedDeclarations = new List<CodeLensDeclaration>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CodeLensTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this tagger is attached.</param>
		public CodeLensTagger(ICodeDocument document) : base("CodeLensTagger", null, document, true) {
			// Initialize declarations and tags from the current document
			CacheDeclarationsAndInvalidateTags();

			// Watch for parse data changes
			document.ParseDataChanged += this.OnDocumentParseDataChanged;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Performs a binary search for the declaration that starts at or after the specified snapshot offset.
		/// </summary>
		/// <param name="snapshotRange">The target snapshot range.</param>
		/// <returns>
		/// The index of the specified value in the specified array, if value is found. 
		/// If value is not found and value is less than one or more elements in array, 
		/// a negative number which is the bitwise complement of the index of the first element that is larger than value. 
		/// If value is not found and value is greater than any of the elements in array, 
		/// a negative number which is the bitwise complement of (the index of the last element plus 1). 
		/// </returns>
		private int BinarySearchDeclarations(TextSnapshotRange snapshotRange) {	
			int lowerIndex = 0;
			int upperIndex = cachedDeclarations.Count - 1;
			int index;

			while (lowerIndex <= upperIndex) {
				index = (lowerIndex + upperIndex) / 2;

				var translatedSnapshotRange = cachedDeclarations[index].VersionRange.Translate(snapshotRange.Snapshot);
				if (translatedSnapshotRange.StartOffset == snapshotRange.StartOffset)
					return index;
				else if (translatedSnapshotRange.StartOffset > snapshotRange.StartOffset)
					upperIndex = index - 1;
				else 
					lowerIndex = index + 1;
			}

			if ((upperIndex >= 0)) {
				var translatedSnapshotRange = cachedDeclarations[upperIndex].VersionRange.Translate(snapshotRange.Snapshot);
				if (translatedSnapshotRange.StartOffset > snapshotRange.StartOffset)
					return ~upperIndex;
				else
					return ~(upperIndex + 1);
			}
			else
				return -1;
		}
		
		/// <summary>
		/// Caches parsed declarations and invalidates tags.
		/// </summary>
		private void CacheDeclarationsAndInvalidateTags() {
			if (this.Document == null)
				return;

			var snapshot = this.Document.CurrentSnapshot;

			var parseData = this.Document.ParseData as CodeLensParseData;
			if (parseData != null) {
				int? invalidStartOffset = null;
				int? invalidEndOffset = null;

				var cacheIndex = 0;
				foreach (var declaration in parseData.Declarations) {
					var declarationStartOffset = declaration.VersionRange.Translate(snapshot).StartOffset;

					// Remove old cached declarations that no longer apply and are before the current declaration
					var currentCachedDeclarationMatchesOffset = false;
					while (cacheIndex < cachedDeclarations.Count) {
						var cachedDeclarationStartOffset = cachedDeclarations[cacheIndex].VersionRange.Translate(snapshot).StartOffset;

						if (cachedDeclarationStartOffset < declarationStartOffset) {
							cachedDeclarations.RemoveAt(cacheIndex);

							if (!invalidStartOffset.HasValue)
								invalidStartOffset = cachedDeclarationStartOffset;
							invalidEndOffset = cachedDeclarationStartOffset;
						}
						else {
							currentCachedDeclarationMatchesOffset = (cachedDeclarationStartOffset == declarationStartOffset);
							break;
						}
					}

					// If the current cached declaration matches the current declaration's offset...
					if (currentCachedDeclarationMatchesOffset) {
						// If there is a key match...
						if (declaration.Key == cachedDeclarations[cacheIndex].Key) {
							// Keep using the same declaration as before
							cacheIndex++;
							continue;
						}
						else {
							// Since the key has changed, remove the old cached declaration
							cachedDeclarations.RemoveAt(cacheIndex);
						}
					}

					// Add a new declaration
					cachedDeclarations.Insert(cacheIndex++, declaration);

					if (!invalidStartOffset.HasValue)
						invalidStartOffset = declarationStartOffset;
					invalidEndOffset = declarationStartOffset;
				}

				// Remove remaining old cached declarations
				while (cacheIndex < cachedDeclarations.Count) {
					var cachedDeclarationStartOffset = cachedDeclarations[cacheIndex].VersionRange.Translate(snapshot).StartOffset;
					cachedDeclarations.RemoveAt(cacheIndex);

					if (!invalidStartOffset.HasValue)
						invalidStartOffset = cachedDeclarationStartOffset;
					invalidEndOffset = cachedDeclarationStartOffset;
				}

				// Invalidate any affected range
				if (invalidStartOffset.HasValue)
					this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, invalidStartOffset.Value, invalidEndOffset.Value)));
			}
			else if (cachedDeclarations.Count > 0) {
				var snapshotRange = new TextSnapshotRange(snapshot, cachedDeclarations[0].VersionRange.Translate(snapshot).StartOffset, 
					cachedDeclarations[cachedDeclarations.Count - 1].VersionRange.Translate(snapshot).StartOffset);

				cachedDeclarations.Clear();

				this.OnTagsChanged(new TagsChangedEventArgs(snapshotRange));
			}
		}

		/// <summary>
		/// Occurs when the document parse data is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <c>ParseDataPropertyChangedEventArgs</c> that contains the event data.</param>
		private void OnDocumentParseDataChanged(object sender, ParseDataPropertyChangedEventArgs e) {
			// Refresh declarations and tags after parse
			CacheDeclarationsAndInvalidateTags();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<IIntraLineSpacerTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			if (snapshotRanges != null) {
				foreach (var snapshotRange in snapshotRanges) {
					var index = this.BinarySearchDeclarations(snapshotRange);
					if (index < 0)
						index = ~index;

					while (index < cachedDeclarations.Count) {
						var declaration = cachedDeclarations[index++];

						var startOffset = declaration.VersionRange.Translate(snapshotRange.Snapshot).StartOffset;
						if (snapshotRange.Contains(startOffset)) {
							yield return new TagSnapshotRange<IIntraLineSpacerTag>(new TextSnapshotRange(snapshotRange.Snapshot, startOffset),
								new CodeLensTag() {
									Declaration = declaration,
									TopMargin = 12
								});
						}
					}
				}
			}
		}
	
		/// <summary>
		/// Occurs when the tagger is closed.
		/// </summary>
		protected override void OnClosed() {
			// Call the base method
			base.OnClosed();
			
			// Stop watching for parse data changes
			if (this.Document != null)
				this.Document.ParseDataChanged -= this.OnDocumentParseDataChanged;
		}
		
	}

}
