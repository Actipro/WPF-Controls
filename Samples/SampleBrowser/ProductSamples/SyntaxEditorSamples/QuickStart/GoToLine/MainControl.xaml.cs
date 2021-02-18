using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLine {

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
		}
			
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGoToLineButtonClick(object sender, RoutedEventArgs e) {
			// Validate
			int lineIndex;
			if ((!int.TryParse(lineNumberTextBox.Text, out lineIndex)) || (lineIndex < 1) || (lineIndex > editor.ActiveView.CurrentSnapshot.Lines.Count)) {
				MessageBox.Show(String.Format("Please enter a valid line number (1-{0}).", editor.ActiveView.CurrentSnapshot.Lines.Count));
				return;
			}

			// Set caret position (make zero-based)
			editor.ActiveView.Selection.CaretPosition = new TextPosition(lineIndex - 1, 0);
			editor.ActiveView.Scroller.ScrollLineToVisibleMiddle();

			// Focus the editor
			editor.Focus();
		}
		
	}

}