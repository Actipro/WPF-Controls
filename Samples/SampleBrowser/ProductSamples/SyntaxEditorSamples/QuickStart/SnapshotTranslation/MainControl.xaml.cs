using ActiproSoftware.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SnapshotTranslation;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly ITextSnapshot _originalSnapshot;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		topEditor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");
		bottomEditor.Document.Language = topEditor.Document.Language;

		// Store the original snapshot of the bottom document
		_originalSnapshot = bottomEditor.Document.CurrentSnapshot;

		// Update the top document with the same content as the bottom
		topEditor.Document.SetText(_originalSnapshot.Text);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnUpdateSelectionButtonClick(object sender, RoutedEventArgs e) {
		var currentSnapshot = bottomEditor.ActiveView.CurrentSnapshot;
		var textRange = topEditor.ActiveView.Selection.TextRange.Translate(_originalSnapshot, currentSnapshot, TextRangeTrackingModes.Default);
		if (textRange.HasValue)
			bottomEditor.ActiveView.Selection.TextRange = textRange.Value;
		bottomEditor.Focus();
	}

}
