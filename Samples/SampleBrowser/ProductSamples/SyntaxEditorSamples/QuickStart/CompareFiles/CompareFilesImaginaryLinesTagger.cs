#if DEBUG
//#define DEBUG_TAGS
#if DEBUG_TAGS
using System.Diagnostics;
#endif
#endif

using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Provides <see cref="IIntraLineSpacerTag"/> objects over text ranges.
	/// </summary>
	public class CompareFilesImaginaryLinesTagger : TaggerBase<IIntraLineSpacerTag> {

		/// <summary>
		/// The special line index used to refer to the tag that appears above the first line since line index 0 is used for the tag below the first line.
		/// </summary>
		private const int FirstLineTopTagIndex = -2;

		private readonly	ConcurrentDictionary<int, ImaginaryLineDifferenceTag>	imaginaryLineAnchors = new ConcurrentDictionary<int, ImaginaryLineDifferenceTag>();
		private readonly	IEditorView												view;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CompareFilesImaginaryLinesTagger"/> class.
		/// </summary>
		/// <param name="view">The view to which this tagger is attached.</param>
		public CompareFilesImaginaryLinesTagger(IEditorView view) : base("CompareFilesImaginaryLines", null, view.SyntaxEditor.Document, true) {
			this.view = view;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Invalidates all previously created tags.
		/// </summary>
		/// <param name="updateDefaultLineHeight">When <c>true</c>, the <see cref="ImaginaryLineDifferenceTag.DefaultLineHeight"/> of each tag will be updated to match the current setting for the view.</param>
		private void InvalidateAllTags(bool updateDefaultLineHeight) {
			#if DEBUG_TAGS
			Debug.WriteLine($"{this.GetType().Name}.{nameof(InvalidateAllTags)}");
			#endif
			
			// Update the default line height of each tag
			if (updateDefaultLineHeight) {
				var keys = imaginaryLineAnchors.Keys.ToArray();
				foreach (var key in keys) {
					if (imaginaryLineAnchors.TryGetValue(key, out var tag)) {
						// Quit if the DefaultLineHeight does not need to change
						if (tag.DefaultLineHeight == view.DefaultLineHeight)
							break;
						tag.DefaultLineHeight = view.DefaultLineHeight;
					}
				}
			}

			// NOTE: You generally want to minimize the range passed to TagsChanged events, but in this case we don't know beforehand where differences appear throughout the document
			this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(view.SyntaxEditor.Document.CurrentSnapshot, view.SyntaxEditor.Document.CurrentSnapshot.TextRange)));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates and adds new <see cref="IIntraLineSpacerTag"/> instances for the given differences.
		/// </summary>
		/// <param name="differences">The collection of differences to apply.</param>
		public void ApplyDifferences(IDifferenceCollection differences) {
			// Clear any existing differences
			imaginaryLineAnchors.Clear();

			// Find any imaginary lines and create spacers for them
			if (differences != null) {
				var lastRealDiffIndex = -1;
				for (int i = 0; i < differences.Count; i++) {
					if (differences[i].Kind == DifferenceKind.Imaginary) {

						var startIndex = i;
						int lineCount = 1;

						// Advance through adjacent imaginary lines and create a single spacer for all of them
						while ((i < (differences.Count - 1)) && (differences[i + 1].Kind == DifferenceKind.Imaginary)) {
							lineCount++;
							i++;
						}

						// Create a tag
						var tag = new ImaginaryLineDifferenceTag() {
							DefaultLineHeight = view.DefaultLineHeight,
							LineCount = lineCount,
						};

						// Imaginary spacers appear below the previous real line except for imaginary
						// lines at the top of the document that must appear before the first line.
						if (lastRealDiffIndex >= 0) {
							// Configure the tag below the real line
							tag.IsBottomPosition = true;

							// Get the document line index of the last real line
							var lastRealLineIndex = differences[lastRealDiffIndex].Position.Value;

							// Associate the tag with the real line
							imaginaryLineAnchors[lastRealLineIndex] = tag;

							#if DEBUG_TAGS
							Debug.WriteLine($"{this.GetType().Name}.{nameof(ApplyDifferences)} :: {lineCount} imaginary line(s) anchored below real document line index {lastRealLineIndex} (diff index {lastRealDiffIndex}).");
							#endif
						}
						else {
							// Configure the tag above the real line
							tag.IsBottomPosition = false;

							// Associate the tag with the first real line
							imaginaryLineAnchors[FirstLineTopTagIndex] = tag;

							#if DEBUG_TAGS
							Debug.WriteLine($"{this.GetType().Name}.{nameof(ApplyDifferences)} :: {lineCount} imaginary line(s) anchored above first real line.");
							#endif
						}
					}
					else {
						// Track the last real line since the imaginary lines must be anchored to a real line
						lastRealDiffIndex = i;
					}
				}
			}

			// Notify that tags changed
			InvalidateAllTags(updateDefaultLineHeight: false);
		}

		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<IIntraLineSpacerTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			#if DEBUG_TAGS
			bool showDebug = true;
			Debug.WriteLineIf(showDebug, $"\r\n\r\n{this.GetType().Name}.{nameof(GetTags)} :: (ENTER)");
			#endif

			var linesProcessed = new HashSet<int>();

			if ((snapshotRanges != null) && (imaginaryLineAnchors != null)) {
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

					// Check any special top tags on the first line
					if (lineIndex == 0) {
						#if DEBUG_TAGS
						Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: Testing first line as imaginary anchor for top tags (total anchor lines = {imaginaryLineAnchors.Count})");
						#endif
						if (imaginaryLineAnchors.TryGetValue(FirstLineTopTagIndex, out var topSpacerTag)) {
							#if DEBUG_TAGS
							Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: First line tagged as anchor for top {topSpacerTag.LineCount} imaginary lines.");
							#endif

							// Yield the tag
							yield return new TagSnapshotRange<IIntraLineSpacerTag>(
								TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartLine.StartOffset, snapshotRange.StartLine.Length), topSpacerTag);
						}
					}

					// Find any spacers anchored below this line
					#if DEBUG_TAGS
					Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: Testing line index {lineIndex} as imaginary line anchor (total anchor lines = {imaginaryLineAnchors.Count})");
					#endif
					if (imaginaryLineAnchors.TryGetValue(lineIndex, out var spacerTag)) {
						#if DEBUG_TAGS
						Debug.WriteLineIf(showDebug, $"{this.GetType().Name}.{nameof(GetTags)} :: Line index {lineIndex} tagged as anchor for {spacerTag.LineCount} imaginary lines.");
						#endif

						// Yield the tag
						yield return new TagSnapshotRange<IIntraLineSpacerTag>(
							TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartLine.StartOffset, snapshotRange.StartLine.Length), spacerTag);
					}

				}
			}
		}

		/// <summary>
		/// Invalidates all previously created tags and ensures the <see cref="ImaginaryLineDifferenceTag.DefaultLineHeight"/> matches the current setting for the view.
		/// </summary>
		public void InvalidateAllTags() {
			InvalidateAllTags(updateDefaultLineHeight: true);
		}

	}

}
