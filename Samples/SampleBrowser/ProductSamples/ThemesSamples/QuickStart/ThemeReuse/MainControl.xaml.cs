namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.ThemeReuse;

/// <summary>
/// Provides the main user control for this sample.
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

	private void OnCloseButtonClick(object sender, RoutedEventArgs e)
		=> dialog.Visibility = Visibility.Collapsed;

	private void OnOpenButtonClick(object sender, RoutedEventArgs e)
		=> dialog.Visibility = Visibility.Visible;

}
