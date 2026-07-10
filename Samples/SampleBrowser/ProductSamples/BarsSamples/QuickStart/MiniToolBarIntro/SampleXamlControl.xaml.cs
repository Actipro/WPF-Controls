using ActiproSoftware.ProductSamples.BarsSamples.Common;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.MiniToolBarIntro;

/// <summary>
/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
/// </summary>
public partial class SampleXamlControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleXamlControl() {
		InitializeComponent();

		// Bind the view to itself since we do not define an explicit view model
		DataContext = this;

		// Define the command associated with bar controls that are used only for demonstration purposes
		NotImplementedCommand = new BarManager().NotImplementedCommand;

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
	/// </summary>
	public ICommand NotImplementedCommand { get; }

}
