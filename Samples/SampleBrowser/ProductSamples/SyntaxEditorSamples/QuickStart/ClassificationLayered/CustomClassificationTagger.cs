using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ClassificationLayered;

/// <summary>
/// Provides a custom implementation of a tagger that can classify ranges of text within a text buffer.
/// </summary>
public class CustomClassificationTagger : TaggerBase<IClassificationTag> {

	private bool _highlightDocumentationComments = true;
	private bool _highlightIdentifiers;
	private readonly IClassificationType _syntaxErrorClassificationType;

	private readonly ITagAggregator<ITokenTag> _tokenTagAggregator;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="document">The document to which this manager is attached.</param>
	public CustomClassificationTagger(ICodeDocument document) : base("Custom", [new Ordering(TaggerKeys.Token, OrderPlacement.Before)], document) {
		// Get the syntax error classification type, which also registers its default style
		_syntaxErrorClassificationType = new BuiltInClassificationTypeProvider().SyntaxError;

		// Get a token tag aggregator
		_tokenTagAggregator = document.CreateTagAggregator<ITokenTag>();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<IClassificationTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		// Loop through the requested snapshot ranges...
		foreach (var snapshotRange in snapshotRanges) {
			// Ignore zero-length snapshot range
			if (snapshotRange.IsZeroLength)
				continue;

			// Get the tag ranges that insert with the snapshot range
			var tokenTagRanges = _tokenTagAggregator.GetTags(snapshotRange);
			if (tokenTagRanges is null)
				continue;

			foreach (var tokenTagRange in tokenTagRanges) {
				switch (tokenTagRange.Tag.Token?.Key) {
					case "XmlCommentText":
						if (HighlightDocumentationComments) {
							// Get the text of the token
							var text = tokenTagRange.SnapshotRange.Text;

							// Look for the text "Actipro"
							var index = text.IndexOf("Actipro");
							while (index != -1) {
								// Add a highlighted range
								yield return new TagSnapshotRange<IClassificationTag>(
									new TextSnapshotRange(snapshotRange.Snapshot, TextRange.FromSpan(tokenTagRange.SnapshotRange.StartOffset + index, length: 7)),
									new ClassificationTag(_syntaxErrorClassificationType)
								);

								// Look for another match
								index = text.IndexOf("Actipro", index + 7);
							}
						}
						break;
					case "Identifier":
						if (HighlightIdentifiers) {
							// Get the text of the token
							var text = tokenTagRange.SnapshotRange.Text;

							// If the text is "Actipro"...
							if (text == "Actipro") {
								// Add a highlighted range
								yield return new TagSnapshotRange<IClassificationTag>(
									new TextSnapshotRange(snapshotRange.Snapshot, tokenTagRange.SnapshotRange.TextRange),
									new ClassificationTag(_syntaxErrorClassificationType)
								);
							}
						}
						break;
				}
			}
		}
	}

	/// <summary>
	/// Indicates whether to highlight 'Actipro' in documentation comments.
	/// </summary>
	public bool HighlightDocumentationComments {
		get => _highlightDocumentationComments;
		set {
			if (_highlightDocumentationComments == value)
				return;

			_highlightDocumentationComments = value;

			// Raise an event so that the entire document is reclassified
			if (Document?.CurrentSnapshot is { } snapshot)
				OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, snapshot.TextRange)));
		}
	}

	/// <summary>
	/// Indicates whether to highlight 'Actipro' in identifiers.
	/// </summary>
	public bool HighlightIdentifiers {
		get => _highlightIdentifiers;
		set {
			if (_highlightIdentifiers == value)
				return;

			_highlightIdentifiers = value;

			// Raise an event so that the entire document is reclassified
			if (Document?.CurrentSnapshot is { } snapshot)
				OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, snapshot.TextRange)));
		}
	}

}
