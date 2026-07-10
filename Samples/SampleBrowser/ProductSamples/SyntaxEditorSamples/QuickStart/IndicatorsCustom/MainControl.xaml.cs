using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom;

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

		// Register an indicator quick info provider
		editor.Document.Language.RegisterService(new IndicatorQuickInfoProvider());

		// Add some indicators
		AddIndicatorForWordAtOffset(editor.ActiveView.CurrentSnapshot.Lines[15].StartOffset + 10);
		AddIndicatorForWordAtOffset(editor.ActiveView.CurrentSnapshot.Lines[22].StartOffset + 10);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds an indicator.
	/// </summary>
	/// <param name="snapshotRange">The <see cref="TextSnapshotRange"/> of the indicator.</param>
	private void AddIndicator(TextSnapshotRange snapshotRange) {
		// Create an indicator tag
		var tag = new CustomIndicatorTag {
			ContentProvider = new PlainTextContentProvider("Custom indicator created at " + DateTime.Now.ToLongTimeString())
		};

		// Add the indicator tag (use a generic method provided on the indicator manager for custom indicators)
		editor.Document.IndicatorManager.Add<CustomIndicatorTagger, CustomIndicatorTag>(snapshotRange, tag);
	}

	/// <summary>
	/// Adds an indicator for the word at the specified offset.
	/// </summary>
	/// <param name="startOffset">The offset to examine.</param>
	private void AddIndicatorForWordAtOffset(int startOffset) {
		var reader = editor.ActiveView.CurrentSnapshot.GetReader(startOffset);

		if (!reader.IsAtTokenStart) {
			reader.GoToCurrentWordStart();
			startOffset = reader.Offset;
		}

		reader.GoToCurrentWordEnd();
		var endOffset = reader.Offset;

		AddIndicator(new TextSnapshotRange(reader.Snapshot, startOffset, endOffset));
	}

	private void OnAddIndicatorButtonClick(object sender, RoutedEventArgs e) {
		// Validate
		if (editor.ActiveView.Selection.IsZeroLength) {
			MessageBox.Show("Please make a selection of at least one character.", "Add Indicator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}

		// Add an indicator
		AddIndicator(editor.ActiveView.Selection.SnapshotRange);

		// Focus the editor
		editor.Focus();
	}

	private void OnClearIndicatorsButtonClick(object sender, RoutedEventArgs e) {
		// Clear the tags (use a generic method provided on the indicator manager for custom indicators)
		editor.Document.IndicatorManager.Clear<CustomIndicatorTagger, CustomIndicatorTag>();

		// Focus the editor
		editor.Focus();
	}

	private void OnGoToNextIndicatorButtonClick(object sender, RoutedEventArgs e) {
		// Create search options
		var options = new TagSearchOptions<CustomIndicatorTag> {
			CanWrap = true,
			SearchUp = false
		};

		// Find the next indicator (use a generic method provided on the indicator manager for custom indicators)
		var tagRange = editor.Document.IndicatorManager.FindNext<CustomIndicatorTagger, CustomIndicatorTag>(editor.ActiveView.Selection.EndSnapshotOffset, options);
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
		// Create search options
		var options = new TagSearchOptions<CustomIndicatorTag> {
			CanWrap = true,
			SearchUp = true
		};

		// Find the previous indicator (use a generic method provided on the indicator manager for custom indicators)
		var tagRange = editor.Document.IndicatorManager.FindNext<CustomIndicatorTagger, CustomIndicatorTag>(editor.ActiveView.Selection.EndSnapshotOffset, options);
		if (tagRange is not null) {
			// Move the caret
			var translatedSnapshotRange = tagRange.VersionRange.Translate(editor.ActiveView.CurrentSnapshot);
			if (translatedSnapshotRange.HasValue)
				editor.ActiveView.Selection.CaretOffset = translatedSnapshotRange.Value.StartOffset;
		}

		// Focus the editor
		editor.Focus();
	}

}
