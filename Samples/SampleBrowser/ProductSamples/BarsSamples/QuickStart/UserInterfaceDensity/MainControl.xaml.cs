using ActiproSoftware.ProductSamples.BarsSamples.Common;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.UserInterfaceDensity;

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

		DataContext = SampleViewModelFactory.CreateDefaultRichTextEditorRibbonWindowViewModel();
	}

}
