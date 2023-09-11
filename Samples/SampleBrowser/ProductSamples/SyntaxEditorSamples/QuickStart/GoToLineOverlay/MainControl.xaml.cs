using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLineOverlay {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Add a command binding to action
			var goToLineAction = new GoToLineAction();
			var commandBinding = goToLineAction.CreateCommandBinding();
			editor.CommandBindings.Insert(0, commandBinding);

			// Bind to Ctrl+G
			var inputBinding = new KeyBinding(goToLineAction, Key.G, ModifierKeys.Control);
			editor.InputBindings.Add(inputBinding);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnShowOverlayButtonClick(object sender, RoutedEventArgs e) {
			// Show the overlay for the active editor view
			GoToLineOverlayPane.Show(editor.ActiveView);
		}
	}

}