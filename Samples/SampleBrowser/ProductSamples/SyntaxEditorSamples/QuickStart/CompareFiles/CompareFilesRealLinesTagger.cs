#if DEBUG
//#define DEBUG_TAGS
#endif

using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles;

/// <summary>
/// Provides <see cref="RealDifferenceTag"/> objects over text ranges.
/// </summary>
/// <param name="view">The view to which this tagger is attached.</param>
public class CompareFilesRealLinesTagger(IEditorView view) : TaggerBase<RealDifferenceTag>("CompareFilesRealLines", orderings: null, view.SyntaxEditor.Document, isForLanguage: true) {

	private IDifferenceCollection? _differences;
	private bool _isLatest;
	private readonly IEditorView _view = view ?? throw new ArgumentNullException(nameof(view));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates and adds new <see cref="RealDifferenceTag"/> instances for the given model.
	/// </summary>
	public void ApplyDifferences(IDifferenceCollection? differences, bool isLatest) {
		_differences = differences;
		_isLatest = isLatest;

		// Notify that tags changed
		// NOTE: You generally want to minimize the range passed to TagsChanged events, but in this case we don't know beforehand where differences appear throughout the document
		OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(_view.SyntaxEditor.Document.CurrentSnapshot, _view.SyntaxEditor.Document.CurrentSnapshot.TextRange)));
	}


	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<RealDifferenceTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		#if DEBUG_TAGS
		bool showDebug = true;
		Debug.WriteLineIf(showDebug, $"{Environment.NewLine}{Environment.NewLine}{GetType().Name}.{nameof(GetTags)} :: (ENTER)");
		#endif

		var linesProcessed = new HashSet<int>();

		if ((snapshotRanges is not null) && (_differences is not null)) {
			// Loop through the snapshot ranges
			foreach (var snapshotRange in snapshotRanges) {
				// Get the index of the line for this snapshot range
				var lineIndex = snapshotRange.StartLine.Index;
				#if DEBUG_TAGS
				Debug.WriteLineIf(showDebug, $"{GetType().Name}.{nameof(GetTags)} :: Checking line index {lineIndex}.");
				#endif

				// Ignore if the line has already been processed
				if (linesProcessed.Contains(lineIndex)) {
					#if DEBUG_TAGS
					Debug.WriteLineIf(showDebug, $"{GetType().Name}.{nameof(GetTags)} :: Skipping line index {lineIndex} which has already been processed.");
					#endif
					continue;
				}
				linesProcessed.Add(lineIndex);

				// Find any diff associated with this line
				var lineDiff = _differences.FirstOrDefault(x => x.Position == lineIndex);
				if (lineDiff is { Kind: not DifferenceKind.None }) {
					#if DEBUG_TAGS
					Debug.WriteLineIf(showDebug, $"{GetType().Name}.{nameof(GetTags)} :: Line index {lineIndex} tagged for type {lineDiff.Kind}.");
					#endif

					// Create a tag
					var lineTag = new RealDifferenceTag() {
						Kind = lineDiff.Kind,
						IsForLine = true,
						IsLatest = _isLatest,
					};

					// The tag length cannot be zero unless it appears at the end of the document. Otherwise, an adornment will not be added for the tag.
					var lineTagLength = snapshotRange.StartLine.Length;
					if ((lineTagLength == 0) && !(snapshotRange.StartLine.IsLastLine))
						lineTagLength = 1;

					// Yield the line tag
					yield return new TagSnapshotRange<RealDifferenceTag>(
						TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartLine.StartOffset, lineTagLength),
						lineTag
					);

					// Yield the child differences as character tags
					var nextCharOffset = 0;
					foreach (var charDiff in lineDiff.Children) {
						if (charDiff.Position.HasValue && charDiff.Length > 0) {
							if (charDiff.Kind is not (DifferenceKind.Imaginary or DifferenceKind.None)) {
								// Create a tag
								var charTag = new RealDifferenceTag() {
									Kind = charDiff.Kind,
									IsForLine = false,
									IsLatest = _isLatest,
								};

								yield return new TagSnapshotRange<RealDifferenceTag>(
									TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartLine.StartOffset + nextCharOffset, charDiff.Length),
									charTag
								);
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
