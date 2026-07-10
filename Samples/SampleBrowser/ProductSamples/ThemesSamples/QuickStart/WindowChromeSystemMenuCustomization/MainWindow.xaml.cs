using ActiproSoftware.Windows.Controls;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeSystemMenuCustomization;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnHelpCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Open the documentation here.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);

	private void OnWindowSystemMenuOpening(object sender, ContextMenuOpeningEventArgs e) {
		// If not allowing a custom system menu, clear e.Menu and quit
		if (useCustomSystemMenuCheckBox.IsChecked != true) {
			e.Menu = null;
			return;
		}

		if (e.Menu is { } menu) {
			var separator = menu.Items.OfType<Separator>().LastOrDefault();
			var index = (separator is not null ? menu.Items.IndexOf(separator) : menu.Items.Count);

			// Inject a Help menu item
			menu.Items.Insert(index++, new Separator());
			menu.Items.Insert(index++, new MenuItem() {
				Header = "Help",
				Command = ApplicationCommands.Help,
				CommandTarget = this,
				InputGestureText = "F1"
			});
		}
	}

}
