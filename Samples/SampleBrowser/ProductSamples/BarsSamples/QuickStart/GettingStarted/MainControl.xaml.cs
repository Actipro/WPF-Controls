using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted;

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

		// Define the steps in the series
		itemsControl.ItemsSource = new List<GettingStartedItemInfo>() {
			new(stepNumber: 1, "Step01/MainWindow", "Create a RibbonWindow configured with an empty Ribbon."),
			new(stepNumber: 2, "Step02/MainWindow", "Create SampleApplicationViewModel and RibbonViewModel that will be bound to the sample."),
			new(stepNumber: 3, "Step03/MainWindow", "Create SampleBarManager to manage working with view models for controls within the Ribbon."),
			new(stepNumber: 4, "Step04/MainWindow", "Add the first Tab to the Ribbon."),
			new(stepNumber: 5, "Step05/MainWindow", "Expand the current sample to include a RichTextBox with a more diverse set of commands in the Ribbon."),
			new(stepNumber: 6, "Step06/MainWindow", "Replace a default ContextMenu with one based on Bars controls."),
			new(stepNumber: 7, "Step07/MainWindow", "Add the Quick Access Toolbar."),
			new(stepNumber: 8, "Step08/MainWindow", "Add the Backstage with buttons."),
			new(stepNumber: 9, "Step09/MainWindow", "Expand the Backstage to include Tabs."),
		};

	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLaunchButtonClick(object sender, RoutedEventArgs e) {
		if (
			DataContext is ApplicationViewModel viewModel
			&& sender is Button { DataContext: GettingStartedItemInfo itemInfo }
		) {
			viewModel.OpenExternalSample(itemInfo.Path);
		}
	}

}
