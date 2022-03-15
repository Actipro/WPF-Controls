#if DEBUG
//#define DEBUG_TAGS
#if DEBUG_TAGS
using System.Diagnostics;
#endif
#endif

using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Provides <see cref="RealDifferenceTag"/> objects over text ranges.
	/// </summary>
	public class CompareFilesRealLinesTagger : TaggerBase<RealDifferenceTag> {

		private IDifferenceCollection differences;
		private bool isLatest;
		private readonly IEditorView view;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CompareFilesRealLinesTagger"/> class.
		/// </summary>
		/// <param name="view">The view to which this tagger is attached.</param>
		public CompareFilesRealLinesTagger(IEditorView view) : base("CompareFilesRealLines", null, view.SyntaxEditor.Document, true) {
			this.view = view;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates and adds new <see cref="RealDifferenceTag"/> instances for the given model.
		/// </summary>
		public void ApplyDifferences(IDifferenceCollection differences, bool isLatest) {
			this.differences = differences;
			this.isLatest = isLatest;

			// Notify that tags changed
			// NOTE: You generally want to minimize the range passed to TagsChanged events, but in this case we don't know beforehand where differences appear throughout the document
			this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(view.SyntaxEditor.Document.CurrentSnapshot, view.SyntaxEditor.Document.CurrentSnapshot.TextRange)));
		}


		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<RealDifferenceTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			#if DEBUG_TAGS
			bool showDebug = true;
			Debug.WriteLineIf(showDebug, $"\r\n\r\n{this.GetType().Name}.{nameof(GetTags)} :: (ENTER)");
			#endif

			var linesProcessed = new HashSet<int>();

			if (snapshotRanges != null && differences != null) {
				// Loop through the snapshot ranges
				foreach (TextSnapshotRange snapshotRange in snapshotRanges) {
					// Get the index of the line for this snapshot range
					var lineIndex = snapshotRange.StartLine.Index;
					#if DEBUG_TAGS
					Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: Checking line index {lineIndex}.");
					#endif

					// Ignore if the line has already been processed
					if (linesProcessed.Contains(lineIndex)) {
						#if DEBUG_TAGS
						Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: Skipping line index {lineIndex} which has already been processed.");
						#endif
						continue;
					}
					linesProcessed.Add(lineIndex);

					// Find any diff associated with this line
					var lineDiff = differences.FirstOrDefault(x => x.Position == lineIndex);
					if ((lineDiff != null) && (lineDiff.Kind != DifferenceKind.None)) {
						#if DEBUG_TAGS
						Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: Line index {lineIndex} tagged for type {lineDiff.Kind}.");
						#endif

						// Create a tag
						var lineTag = new RealDifferenceTag() {
							Kind = lineDiff.Kind,
							IsForLine = true,
							IsLatest = isLatest,
						};

						// The tag length cannot be zero unless it appears at the end of the document. Otherwise an adornment will not be added for the tag.
						int lineTagLength = snapshotRange.StartLine.Length;
						if ((lineTagLength == 0) && !(snapshotRange.StartLine.IsLastLine))
							lineTagLength = 1;

						// Yield the line tag
						yield return new TagSnapshotRange<RealDifferenceTag>(
							TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartLine.StartOffset, lineTagLength), lineTag);

						// Yield the child differences as character tags
						int nextCharOffset = 0;
						foreach (var charDiff in lineDiff.Children) {
							if (charDiff.Position.HasValue && charDiff.Length > 0) {
								if (!((charDiff.Kind == DifferenceKind.Imaginary) || (charDiff.Kind == DifferenceKind.None))) {
									// Create a tag
									var charTag = new RealDifferenceTag() {
										Kind = charDiff.Kind,
										IsForLine = false,
										IsLatest = isLatest,
									};

									yield return new TagSnapshotRange<RealDifferenceTag>(
										TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartLine.StartOffset + nextCharOffset, charDiff.Length), charTag);
								}

								// Advance the start of the next character difference based on the length of this difference
								nextCharOffset += charDiff.Length;
							}
						}
					}

				}
			}
		}

	}

}
