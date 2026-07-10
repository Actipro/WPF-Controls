using ActiproSoftware.Windows.Controls.Editors;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CurrentLineHighlighting;

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
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Css.langdef");

		// Register the default display item classification types on the ambient registry
		var provider = new BuiltInClassificationTypeProvider();
		provider.RegisterAll();

		// Get the style for the current line and bind it to the edit box
		var style = AmbientHighlightingStyleRegistry.Instance[provider.CurrentLine];
		var valueBinding = new Binding {
			Source = style,
			Path = new PropertyPath("Background"),
			UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
		};
		colorEditbox.SetBinding(ColorEditBox.ValueProperty, valueBinding);
	}

}
