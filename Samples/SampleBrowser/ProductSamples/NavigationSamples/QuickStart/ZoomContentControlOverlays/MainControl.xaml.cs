using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlOverlays {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private static RoutedCommand addPushPin;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.CommandBindings.Add(new CommandBinding(MainControl.AddPushPin, OnAddPushPinExecute));

			this.zoomContentControl.ZoomToFit();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnAddPushPinExecute(object sender, ExecutedRoutedEventArgs e) {
			// Create a push pin and set the canvas location, anchoring the bottom-left point
			PushPin pushPin = new PushPin();
			Point point = Mouse.GetPosition(this.map);
			Canvas.SetLeft(pushPin, point.X);
			Canvas.SetBottom(pushPin, this.map.ActualHeight - point.Y);

			this.zoomContentControl.Overlays.Add(pushPin);
			e.Handled = true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="RoutedCommand"/> that is used to add a push pin.
		/// </summary>
		/// <value>The <see cref="RoutedCommand"/> that is used to add a push pin.</value>
		public static RoutedCommand AddPushPin {
			get {
				if (null == addPushPin)
					addPushPin = new RoutedCommand("AddPushPin", typeof(MainControl));
				return addPushPin;
			}
		}
	}
}