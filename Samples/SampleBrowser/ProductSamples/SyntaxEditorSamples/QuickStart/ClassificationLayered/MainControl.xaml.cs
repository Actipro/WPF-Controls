using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ClassificationLayered;

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

		// Attach a custom classification tagger to the language (use a singleton key so it can be retrieved later)
		language.RegisterService(new CodeDocumentTaggerProvider<CustomClassificationTagger>(typeof(CustomClassificationTagger)));

		// Assign the language to the document
		editor.Document.Language = language;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnCommentsCheckBoxCheckedChanged(object sender, RoutedEventArgs e) {
		if (editor?.Document.Properties.TryGetValue<CustomClassificationTagger>(out var tagger) == true)
			tagger!.HighlightDocumentationComments = (commentsCheckBox.IsChecked == true);
	}

	private void OnIdentifierCheckBoxCheckedChanged(object sender, RoutedEventArgs e) {
		if (editor?.Document.Properties.TryGetValue<CustomClassificationTagger>(out var tagger) == true)
			tagger!.HighlightIdentifiers = (identifierCheckBox.IsChecked == true);
	}

}
