using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.Rendering;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;
using System.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightWord;

/// <summary>
/// Provides a custom implementation of a view-based classification tagger that tags the word that that view's caret is in.
/// </summary>
public class WordHighlightTagger : TaggerBase<IClassificationTag> {

	private string _currentWord = string.Empty;
	private IEditorView? _view;

	private static readonly Regex _wordCheck = new(@"[A-Za-z_]\w*", RegexOptions.Compiled);
	private static readonly ClassificationType _wordHighlightClassificationType = new("WordHighlight", "Word Highlight");

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static WordHighlightTagger() {
		// This sample assumes the editor will use the AmbientHighlightingStyleRegistry
		var registry = AmbientHighlightingStyleRegistry.Instance;

		// Configure light/dark color palettes with default colors
		var key = _wordHighlightClassificationType.Key;
		registry.LightColorPalette?.SetBackground(key, UIColor.FromWebColor("#40c0c0c0"));
		registry.LightColorPalette?.SetBorder(key, UIColor.FromWebColor("#c0c0c0"));
		registry.DarkColorPalette?.SetBackground(key, UIColor.FromWebColor("#40717171"));
		registry.DarkColorPalette?.SetBorder(key, UIColor.FromWebColor("#717171"));

		// Define a style with a border
		var style = new HighlightingStyle() {
			BorderCornerKind = HighlightingStyleBorderCornerKind.Rounded,
			BorderKind = LineKind.Solid,
			IsBorderEditable = true,
			IsForegroundEditable = false,
		};

		// Associate the style with the classification type
		//   and the current color palette color will be automatically applied
		registry.Register(_wordHighlightClassificationType, style);
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public WordHighlightTagger(IEditorView view) : base("Custom", [new Ordering(TaggerKeys.Token, OrderPlacement.Before)], view.SyntaxEditor.Document) {

		// Initialize
		_view = view;
		_view.SelectionChanged += OnViewSelectionChanged;

		// Update current word
		UpdateCurrentWord();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnViewSelectionChanged(object? sender, EditorViewSelectionEventArgs e) {
		if (_view is null)
			return;

		// Update the current word
		UpdateCurrentWord();
	}

	/// <summary>
	/// Updates the current word.
	/// </summary>
	private void UpdateCurrentWord() {
		if (_view?.Selection is null)
			return;

		// Save the old current word
		var oldCurrentWord = _currentWord;

		// Get the current word and ensure it has only letter or number characters
		_currentWord = (_view.Selection.Length == 0)
			? _view.GetCurrentWordText().Trim()
			: _view.SelectedText;
		var match = _wordCheck.Match(_currentWord);
		if ((match is null) || (match.Index != 0) || (match.Length != _currentWord.Length))
			_currentWord = string.Empty;

		// If the current word changed...
		if (oldCurrentWord != _currentWord) {
			// Notify that tags changed
			// NOTE: You generally want to minimize the range passed to TagsChanged events, but in this case we don't know beforehand where word matches are made throughout the document
			OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(_view.SyntaxEditor.Document.CurrentSnapshot, _view.SyntaxEditor.Document.CurrentSnapshot.TextRange)));
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<IClassificationTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		if (string.IsNullOrEmpty(_currentWord))
			yield break;

		// Get a regex of the current word
		var search = new Regex(string.Format(@"\b{0}\b", _currentWord), RegexOptions.Singleline);

		// Loop through the requested snapshot ranges...
		foreach (var snapshotRange in snapshotRanges) {
			// If the snapshot range is not zero-length...
			if (!snapshotRange.IsZeroLength) {
				// Look for current word matches
				foreach (Match match in search.Matches(snapshotRange.Text)) {
					// Add a highlighted range
					yield return new TagSnapshotRange<IClassificationTag>(
						new TextSnapshotRange(snapshotRange.Snapshot, TextRange.FromSpan(snapshotRange.StartOffset + match.Index, match.Length)),
						new ClassificationTag(_wordHighlightClassificationType)
					);
				}
			}
		}
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Detach from the view
		if (_view is not null) {
			_view.SelectionChanged -= OnViewSelectionChanged;
			_view = null;
		}

		base.OnClosed();
	}

}
