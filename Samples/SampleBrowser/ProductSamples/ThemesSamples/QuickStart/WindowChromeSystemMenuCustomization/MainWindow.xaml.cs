using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Windows.Controls;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeSystemMenuCustomization {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnHelpCommandExecuted(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Open the documentation here.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Occurs when the window's system menu is opening.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="ContextMenuOpeningEventArgs"/> that contains the event data.</param>
		private void OnWindowSystemMenuOpening(object sender, ContextMenuOpeningEventArgs e) {
			// If not allowing a custom system menu, clear e.Menu and quit
			if (!true.Equals(useCustomSystemMenuCheckBox.IsChecked)) {
				e.Menu = null;
				return;
			}

			var separator = e.Menu.Items.OfType<Separator>().LastOrDefault();
			var index = (separator != null ? e.Menu.Items.IndexOf(separator) : e.Menu.Items.Count);

			// Inject a Help menu item
			e.Menu.Items.Insert(index++, new Separator());
			e.Menu.Items.Insert(index++, new MenuItem() { 
				Header = "Help", 
				Command = ApplicationCommands.Help,
				CommandTarget = this,
				InputGestureText = "F1"
			});
		}

	}

}