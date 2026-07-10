using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeTitleBarBackButton;

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

		frame.Navigate(new Page1());
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnBrowseBackCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Browse backward a page here.", "Back Executed", MessageBoxButton.OK, MessageBoxImage.Information);

}
