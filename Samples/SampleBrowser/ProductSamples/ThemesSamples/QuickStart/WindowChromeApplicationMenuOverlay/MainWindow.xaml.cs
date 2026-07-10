using ActiproSoftware.Windows.Themes;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeApplicationMenuOverlay;

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
		// Force an Office Colorful theme for this sample
		if (ThemeManager.CurrentTheme?.StartsWith("OfficeColorful") != true) {
			ThemeManager.UnregisterAutomaticThemes();
			ThemeManager.CurrentTheme = ThemeNames.OfficeColorfulIndigo;
		}

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnHelpCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Show documentation here.", "Documentation", MessageBoxButton.OK, MessageBoxImage.Information);

	private void OnIsOverlayVisibleChanged(object sender, RoutedEventArgs e) {
		// NOTE: This event handler is a good place to programmatically adjust the UI when the overlay state changes, if necessary
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnKeyDown(KeyEventArgs e) {
		base.OnKeyDown(e);

		if (!e.Handled) {
			switch (e.Key) {
				case Key.Escape:
					// Ensure the overlay is closed when Esc is pressed
					if (WindowChrome.GetIsOverlayVisible(window))
						WindowChrome.SetIsOverlayVisible(window, false);
					break;
			}
		}
	}

}
