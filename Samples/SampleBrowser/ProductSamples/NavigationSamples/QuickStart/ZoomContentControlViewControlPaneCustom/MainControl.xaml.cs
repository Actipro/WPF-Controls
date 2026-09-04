using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlViewControlPaneCustom;

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

		// Setup default InputBindings
		zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartPanDrag, new MouseGesture(MouseAction.LeftClick)));

		var factor = Mouse.MouseWheelDeltaForOneLine / SystemParameters.WheelScrollLines;
		zoomContentControl.InputBindings.AddRange(new MouseWheelBinding[] {
			new(ScrollBar.LineUpCommand, new(MouseWheelAction.PositiveDelta)) { CommandParameter = factor },
			new(ScrollBar.LineDownCommand, new(MouseWheelAction.NegativeDelta)) { CommandParameter = factor },
			new(ScrollBar.LineLeftCommand, new(MouseWheelAction.PositiveDelta, ModifierKeys.Shift)) { CommandParameter = factor },
			new(ScrollBar.LineRightCommand, new(MouseWheelAction.NegativeDelta, ModifierKeys.Shift)) { CommandParameter = factor },

			new(ZoomContentControlCommands.ZoomInToPoint, new(MouseWheelAction.PositiveDelta, ModifierKeys.Control)),
			new(ZoomContentControlCommands.ZoomInToPoint, new(MouseWheelAction.PositiveDelta, ModifierKeys.Control | ModifierKeys.Shift)),
			new(ZoomContentControlCommands.ZoomOutFromPoint, new(MouseWheelAction.NegativeDelta, ModifierKeys.Control)),
			new(ZoomContentControlCommands.ZoomOutFromPoint, new(MouseWheelAction.NegativeDelta, ModifierKeys.Control | ModifierKeys.Shift)),
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
		if (zoomContentControl is null)
			return;

		// Remove the MouseBinding that is bound to the LeftClick action
		for (var i = 0; i < zoomContentControl.InputBindings.Count; i++) {
			var binding = zoomContentControl.InputBindings[i] as MouseBinding;
			if (binding?.Gesture is MouseGesture gesture) {
				if ((MouseAction.LeftClick == gesture.MouseAction) && (ModifierKeys.None == gesture.Modifiers)) {
					zoomContentControl.InputBindings.RemoveAt(i);
					break;
				}
			}
		}

		// Add in a new MouseBinding for the LeftClick action
		zoomContentControl.InputBindings.Clear();
		if (panDragRadioButton.IsChecked == true)
			zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartPanDrag, new MouseGesture(MouseAction.LeftClick)));
		else if (zoomInRadioButton.IsChecked == true)
			zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartZoomIn, new MouseGesture(MouseAction.LeftClick)));
		else if (zoomOutRadioButton.IsChecked == true)
			zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartZoomOut, new MouseGesture(MouseAction.LeftClick)));
		else if (zoomDragRadioButton.IsChecked == true)
			zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartZoomDrag, new MouseGesture(MouseAction.LeftClick)));

		zoomContentControl.UpdateCursor();
	}

}
