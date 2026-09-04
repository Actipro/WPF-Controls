using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04a;

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

		// Load the EBNF language
		ebnfEditor.Document.Language = SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Ebnf.langdef");

		// Show the EBNF
		var parser = editor.Document.Language.GetParser() as ILLParser;
		if (parser is not null)
			ebnfEditor.Document.SetText(parser.Grammar.ToEbnfString());
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IParseError error }) {
			if (error.PositionRange.HasValue)
				editor.ActiveView.Selection.StartPosition = error.PositionRange.Value.StartPosition;

			editor.Focus();
		}
	}

	private void OnEditorDocumentParseDataChanged(object sender, EventArgs e) {
		//
		// NOTE: The parse data here is generated in a worker thread... this event handler is called
		//   back in the UI thread though so any processing done below could slow down UI if
		//   the processing is lengthy
		//

		var parseData = editor.Document.ParseData as ILLParseData;
		if (parseData is not null) {
			// Show the AST
			astOutputEditor.Document.SetText(parseData.Ast?.ToTreeString(0).Replace("\t", "  "));

			// Output errors
			errorListView.ItemsSource = parseData.Errors;
		}
	}

}
