using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ReadOnlyRegions;

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

		// Load a language from a language definition
		var language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

		// Attach a custom read-only region tagger to the language (use a singleton key so it can be retrieved later)
		language.RegisterService(new CodeDocumentTaggerProvider<CustomReadOnlyRegionTagger>(typeof(CustomReadOnlyRegionTagger)));

		// Assign the language to the document
		editor.Document.Language = language;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnHighlightRegionsCheckBoxCheckedChanged(object sender, RoutedEventArgs e) {
		if (editor?.Document.Properties.TryGetValue<CustomReadOnlyRegionTagger>(out var tagger) == true)
			tagger!.HighlightReadOnlyRegions = (highlightRegionsCheckBox.IsChecked == true);
	}

	private void OnMakeSelectionReadOnlyButtonClick(object sender, RoutedEventArgs e) {
		if (editor?.Document.Properties.TryGetValue<CustomReadOnlyRegionTagger>(out var tagger) == true) {
			tagger!.Clear();
			if (editor.ActiveView.Selection.Length > 0)
				tagger!.Add(editor.ActiveView.Selection.SnapshotRange, new ReadOnlyRegionTag());
		}
	}

}
