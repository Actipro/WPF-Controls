using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.InertiaScrollViewerIntro;

/// <summary>
/// The QuickStart for <see cref="InertiaScrollViewer"/>.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnButtonClicked(object sender, RoutedEventArgs e)
		=> MessageBox.Show("The button has been clicked.", "Button Clicked", MessageBoxButton.OK);

}
