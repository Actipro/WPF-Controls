using ActiproSoftware.Windows.Controls.Navigation;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlCustomInputBindings;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static MainControl() {
		Commands = [
			ZoomContentControlCommands.CenterAndZoomInToPoint,
			ZoomContentControlCommands.CenterAndZoomOutFromPoint,
			ZoomContentControlCommands.CenterToPoint,
			ZoomContentControlCommands.LineDown,
			ZoomContentControlCommands.LineLeft,
			ZoomContentControlCommands.LineRight,
			ZoomContentControlCommands.LineUp,
			ZoomContentControlCommands.PageDown,
			ZoomContentControlCommands.PageLeft,
			ZoomContentControlCommands.PageRight,
			ZoomContentControlCommands.PageUp,
			ZoomContentControlCommands.ResetView,
			ZoomContentControlCommands.StartPanDrag,
			ZoomContentControlCommands.StartZoomDrag,
			ZoomContentControlCommands.StartZoomIn,
			ZoomContentControlCommands.StartZoomOut,
			ZoomContentControlCommands.StartZoomToRegion,
			ZoomContentControlCommands.ZoomIn,
			ZoomContentControlCommands.ZoomInToPoint,
			ZoomContentControlCommands.ZoomOut,
			ZoomContentControlCommands.ZoomOutFromPoint,
			ZoomContentControlCommands.ZoomToFit
		];
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		=> zoomContentControl.UpdateCursor();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The commands supported by <see cref="ZoomContentControl"/>.
	/// </summary>
	public static RoutedCommand[] Commands { get; }

}
