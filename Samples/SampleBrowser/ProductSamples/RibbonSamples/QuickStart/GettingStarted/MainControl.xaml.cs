using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.GettingStarted;

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

	private void OnLaunchButtonClick(object sender, RoutedEventArgs e) {
		var viewModel = DataContext as ApplicationViewModel;
		viewModel?.OpenExternalSample(((ListBoxItem)quickStartListBox.SelectedItem).Tag as string);
	}

}
