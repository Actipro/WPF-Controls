namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeTitleBarBackButton;

/// <summary>
/// Represents a page.
/// </summary>
public partial class Page1 : Page {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public Page1() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnNavigateToPage2ButtonClick(object sender, RoutedEventArgs e)
		=> NavigationService.Navigate(new Page2());

}
