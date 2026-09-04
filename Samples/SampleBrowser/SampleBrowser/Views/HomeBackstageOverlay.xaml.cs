using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the user control for a Backstage overlay.
/// </summary>
public partial class HomeBackstageOverlay {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public HomeBackstageOverlay() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override InertiaScrollViewer? ScrollViewer
		=> scrollViewer;

}
