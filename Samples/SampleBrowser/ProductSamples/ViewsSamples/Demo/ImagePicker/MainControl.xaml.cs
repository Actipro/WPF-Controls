using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.ImagePicker;

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

		var data = FindResource("ProductData") as ProductData;
		if (data is not null) {
			// Add product logos as sample images
			productNameListBox.ItemsSource = data.ProductFamilies;
			productLogoListBox.ItemsSource = productNameListBox.ItemsSource;
		}
	}

}
