using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Use the document text and language in both editors
		readOnlyEditor.Document.SetText(editor.Document.CurrentSnapshot.Text);
		readOnlyEditor.Document.Language = editor.Document.Language;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnCollapseButtonClick(object sender, RoutedEventArgs e) {
		if (editor.ActiveView.Selection.IsZeroLength) {
			MessageBox.Show("Please select at least one character to collapse.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}

		// Get the tagger that was created by the language and has been persisted in the document's properties
		//   while the language is active on the document
		if (editor.Document.Properties.TryGetValue<CollapsedRegionTagger>(out var tagger)) {
			// Create a version range
			var versionRange = editor.ActiveView.Selection.SnapshotRange.ToVersionRange(TextRangeTrackingModes.DeleteWhenZeroLength);

			// Add the tag to the tagger
			tagger!.Add(new TagVersionRange<ICollapsedRegionTag>(versionRange, new CollapsedRegionTag()));

			// Collapse the selection
			editor.ActiveView.Selection.Collapse();
		}

		// Focus the editor
		editor.Focus();
	}

}
