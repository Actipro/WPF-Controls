using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.MultiColumnPanelIntro;

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
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The configurable panel.
	/// </summary>
	public Panel? ConfigurablePanel
		=> peopleItemsControl.FindDescendantOfType<MultiColumnPanel>();

}
