using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlViewControlPaneCustom {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Setup default InputBindings
			this.zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartPanDrag,
				new MouseGesture(MouseAction.LeftClick)));

			double factor = Mouse.MouseWheelDeltaForOneLine / SystemParameters.WheelScrollLines;
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ScrollBar.LineUpCommand,
				new MouseWheelGesture(MouseWheelAction.PositiveDelta)) { CommandParameter = factor });
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ScrollBar.LineDownCommand,
				new MouseWheelGesture(MouseWheelAction.NegativeDelta)) { CommandParameter = factor });
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ScrollBar.LineLeftCommand,
				new MouseWheelGesture(MouseWheelAction.PositiveDelta, ModifierKeys.Shift)) { CommandParameter = factor });
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ScrollBar.LineRightCommand,
				new MouseWheelGesture(MouseWheelAction.NegativeDelta, ModifierKeys.Shift)) { CommandParameter = factor });

			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ZoomContentControlCommands.ZoomInToPoint,
				new MouseWheelGesture(MouseWheelAction.PositiveDelta, ModifierKeys.Control)));
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ZoomContentControlCommands.ZoomInToPoint,
				new MouseWheelGesture(MouseWheelAction.PositiveDelta, ModifierKeys.Control | ModifierKeys.Shift)));
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ZoomContentControlCommands.ZoomOutFromPoint,
				new MouseWheelGesture(MouseWheelAction.NegativeDelta, ModifierKeys.Control)));
			this.zoomContentControl.InputBindings.Add(new MouseWheelBinding(ZoomContentControlCommands.ZoomOutFromPoint,
				new MouseWheelGesture(MouseWheelAction.NegativeDelta, ModifierKeys.Control | ModifierKeys.Shift)));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>Checked</c> event of the <see cref="logoRadioButton"/> or <see cref="buttonRadioButton"/> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
			if (null == this.zoomContentControl)
				return;

			// Remove the MouseBinding that is bound to the LeftClick action
			for (int i = 0; i < this.zoomContentControl.InputBindings.Count; i++) {
				MouseBinding binding = this.zoomContentControl.InputBindings[i] as MouseBinding;
				if (null != binding) {
					MouseGesture gesture = binding.Gesture as MouseGesture;
					if (null != gesture && MouseAction.LeftClick == gesture.MouseAction && ModifierKeys.None == gesture.Modifiers) {
						this.zoomContentControl.InputBindings.RemoveAt(i);
						break;
					}
				}
			}

			// Add in a new MouseBinding for the LeftClick action
			this.zoomContentControl.InputBindings.Clear();
			if (true == this.panDragRadioButton.IsChecked)
				this.zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartPanDrag,
					new MouseGesture(MouseAction.LeftClick)));
			else if (true == this.zoomInRadioButton.IsChecked)
				this.zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartZoomIn,
					new MouseGesture(MouseAction.LeftClick)));
			else if (true == this.zoomOutRadioButton.IsChecked)
				this.zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartZoomOut,
					new MouseGesture(MouseAction.LeftClick)));
			else if (true == this.zoomDragRadioButton.IsChecked)
				this.zoomContentControl.InputBindings.Add(new MouseBinding(ZoomContentControlCommands.StartZoomDrag,
					new MouseGesture(MouseAction.LeftClick)));

			this.zoomContentControl.UpdateCursor();
		}
	}
}