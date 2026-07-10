using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ReadOnlyRegions;

/// <summary>
/// Provides a custom implementation of a tagger that can mark text ranges as read-only within a text buffer.
/// </summary>
/// <param name="document">The document to which this tagger is attached.</param>
public class CustomReadOnlyRegionTagger(ICodeDocument document)
	: CollectionTagger<IReadOnlyRegionTag>("Custom", [new Ordering(TaggerKeys.Token, OrderPlacement.Before)], document, isForLanguage: true), ITagger<IClassificationTag> {

	private bool _highlightReadOnlyRegions = true;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static CustomReadOnlyRegionTagger() {
		// Access the ReadOnlyRegion through BuiltInClassificationTypeProvider and it will automatically
		//   register a default IHighlightingStyle to be used with ClassificationTypes.ReadOnlyRegion (which
		//   is the default IClassificationType for ActiproSoftware.Text.Tagging.Implementation.ReadOnlyRegionTag).
		_ = new BuiltInClassificationTypeProvider().ReadOnlyRegion;
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	IEnumerable<TagSnapshotRange<IClassificationTag>> ITagger<IClassificationTag>.GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		// We implement ITagger<IClassificationTag> explicitly so that the core CollectionTagger can
		//   return tags of type IReadOnlyRegionTag.  This method can return IClassificationTag tags so that the core
		//   SyntaxEditor rendering procedures can update syntax highlighting over the marked ranges
		if (!_highlightReadOnlyRegions)
			yield break;

		foreach (var tagRange in GetTags(snapshotRanges, parameter))
			yield return new TagSnapshotRange<IClassificationTag>(tagRange.SnapshotRange, (IClassificationTag)tagRange.Tag);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether to highlight read-only regions.
	/// </summary>
	public bool HighlightReadOnlyRegions {
		get => _highlightReadOnlyRegions;
		set {
			if (_highlightReadOnlyRegions == value)
				return;

			_highlightReadOnlyRegions = value;

			// Raise an event so that the entire document is reclassified
			if (Document?.CurrentSnapshot is { } snapshot)
				OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, snapshot.TextRange)));
		}
	}

}
