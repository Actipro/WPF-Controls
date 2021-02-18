using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchResultHighlighting {

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

			// Load a language from a language definition
			editor.Document.Language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("JavaScript.langdef");

			// Ensure all classification types and related styles have been registered
			//   since classification types are used for the highlight display
			new DisplayItemClassificationTypeProvider().RegisterAll();

			// Refresh highlights
			this.RefreshHighlights();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the control receives focus.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="TextChangedEventArgs"/> that contains the event data.</param>
		private void OnFindWhatTextBoxGotFocus(object sender, RoutedEventArgs e) {
			this.RefreshHighlights();
		}

		/// <summary>
		/// Occurs when the text is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="TextChangedEventArgs"/> that contains the event data.</param>
		private void OnFindWhatTextBoxTextChanged(object sender, TextChangedEventArgs e) {
			this.RefreshHighlights();
		}

		/// <summary>
		/// Refreshes the highlights.
		/// </summary>
		private void RefreshHighlights() {
			if (editor == null)
				return;

			EditorSearchOptions options = new EditorSearchOptions();
			options.FindText = findWhatTextBox.Text;
			editor.ActiveView.HighlightedResultSearchOptions = options;
		}
		
	}

}