namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridExplicitProperties;

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

	private void OnUpdateValueButtonClick(object sender, RoutedEventArgs e)
		=> unboundProperty.Value = "Set at " + DateTime.Now.ToLongTimeString();

}
