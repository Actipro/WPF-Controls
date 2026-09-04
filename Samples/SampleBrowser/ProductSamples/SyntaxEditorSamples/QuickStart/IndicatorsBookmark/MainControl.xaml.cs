using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsBookmark;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("JavaScript.langdef");

		// Add some indicators
		ToggleIndicator(editor.ActiveView.CurrentSnapshot.Lines[15]);
		editor.Document.IndicatorManager.Bookmarks.ToggleEnabledState(ToggleIndicator(editor.ActiveView.CurrentSnapshot.Lines[17])!.Tag);
		ToggleIndicator(editor.ActiveView.CurrentSnapshot.Lines[24]);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnClearIndicatorsButtonClick(object sender, RoutedEventArgs e) {
		// Clear the tags
		editor.Document.IndicatorManager.Bookmarks.Clear();

		// Focus the editor
		editor.Focus();
	}

	private void OnGoToNextIndicatorButtonClick(object sender, RoutedEventArgs e) {
		// Create search options (only find enabled bookmarks)
		var options = new TagSearchOptions<BookmarkIndicatorTag> {
			CanWrap = true,
			SearchUp = false,
			Filter = (tr => tr.Tag.IsEnabled)
		};

		// Find the next indicator
		var tagRange = editor.Document.IndicatorManager.Bookmarks.FindNext(editor.ActiveView.Selection.EndSnapshotOffset.Line, options);
		if (tagRange is not null) {
			// Move the caret
			var translatedSnapshotRange = tagRange.VersionRange.Translate(editor.ActiveView.CurrentSnapshot);
			if (translatedSnapshotRange.HasValue)
				editor.ActiveView.Selection.CaretOffset = translatedSnapshotRange.Value.StartOffset;
		}

		// Focus the editor
		editor.Focus();
	}

	private void OnGoToPreviousIndicatorButtonClick(object sender, RoutedEventArgs e) {
		// Create search options (only find enabled bookmarks)
		var options = new TagSearchOptions<BookmarkIndicatorTag> {
			CanWrap = true,
			SearchUp = true,
			Filter = (tr => tr.Tag.IsEnabled)
		};

		// Find the previous indicator
		var tagRange = editor.Document.IndicatorManager.Bookmarks.FindNext(editor.ActiveView.Selection.EndSnapshotOffset.Line, options);
		if (tagRange is not null) {
			// Move the caret
			var translatedSnapshotRange = tagRange.VersionRange.Translate(editor.ActiveView.CurrentSnapshot);
			if (translatedSnapshotRange.HasValue)
				editor.ActiveView.Selection.CaretOffset = translatedSnapshotRange.Value.StartOffset;
		}

		// Focus the editor
		editor.Focus();
	}

	private void OnToggleBookmarkEnabledButtonClick(object sender, RoutedEventArgs e) {
		// Get the bookmarks at the caret and toggle their enabled states
		var tagRanges = editor.Document.IndicatorManager.Bookmarks.GetInstances(editor.ActiveView.Selection.EndSnapshotOffset.Line);
		var count = 0;
		foreach (var tagRange in tagRanges) {
			if (editor.Document.IndicatorManager.Bookmarks.ToggleEnabledState(tagRange.Tag))
				count++;
		}

		if (count == 0)
			MessageBox.Show("No bookmarks were found at the caret.", "Toggle Bookmark Enabled State", MessageBoxButton.OK, MessageBoxImage.Exclamation);

		// Focus the editor
		editor.Focus();
	}

	private void OnToggleIndicatorButtonClick(object sender, RoutedEventArgs e) {
		// Toggle an indicator
		ToggleIndicator(editor.ActiveView.Selection.EndSnapshotOffset.Line);

		// Focus the editor
		editor.Focus();
	}

	/// <summary>
	/// Toggles an indicator.
	/// </summary>
	/// <param name="snapshotLine">The <see cref="ITextSnapshotLine"/> of the indicator.</param>
	/// <returns>The tagged range that was created, if any.</returns>
	private TagVersionRange<BookmarkIndicatorTag>? ToggleIndicator(ITextSnapshotLine snapshotLine)
		=> editor.Document.IndicatorManager.Bookmarks.Toggle(snapshotLine);

}
