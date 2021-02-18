using System;
using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04c {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			
			// Load the EBNF language
			ebnfEditor.Document.Language = SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Ebnf.langdef");
			
			// Show the EBNF
			ILLParser parser = editor.Document.Language.GetParser() as ILLParser;
			if (parser != null)
				ebnfEditor.Document.SetText(parser.Grammar.ToEbnfString());
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
			ListBox listBox = (ListBox)sender;
			IParseError error = listBox.SelectedItem as IParseError;
			if (error != null) {
				editor.ActiveView.Selection.StartPosition = error.PositionRange.StartPosition;
				editor.Focus();
			}
		}
		
		/// <summary>
		/// Occurs when the document's parse data has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to this event.</param>
		private void OnEditorDocumentParseDataChanged(object sender, EventArgs e) {

			//
			// NOTE: The parse data here is generated in a worker thread... this event handler is called 
			//         back in the UI thread though so any processing done below could slow down UI if 
			//         the processing is lengthy
			//

			ILLParseData parseData = editor.Document.ParseData as ILLParseData;
			if (parseData != null) {
				// Show the AST
				if (parseData.Ast != null)
					astOutputEditor.Document.SetText(parseData.Ast.ToTreeString(0).Replace("\t", "  "));
				else
					astOutputEditor.Document.SetText(null);

				// Output errors
				errorListView.ItemsSource = parseData.Errors;
			}
		}

	}
}