using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Windows.Controls.Navigation;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlCustomInputBindings {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private static RoutedCommand[] commands = new RoutedCommand[] {
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
		};

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>SelectionChanged</c> event of the <c>ComboBox</c> controls.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			this.zoomContentControl.UpdateCursor();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the commands supported by <see cref="ZoomContentControl"/>.
		/// </summary>
		/// <value>The commands supported by <see cref="ZoomContentControl"/>.</value>
		public static RoutedCommand[] Commands {
			get { return commands; }
		}
	}
}