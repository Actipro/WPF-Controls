using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlOverlays {

	/// <summary>
	/// Interaction logic for PushPin.xaml
	/// </summary>
	public partial class PushPin : Control {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="PushPin"/> class.
		/// </summary>
		public PushPin() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>Click</c> event of the close button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnCloseButtonClick(object sender, RoutedEventArgs e) {
			ZoomContentControl zoomContentControl = VisualTreeHelperExtended.GetAncestor(this, typeof(ZoomContentControl)) as ZoomContentControl;
			if (null != zoomContentControl)
				zoomContentControl.Overlays.Remove(this);
		}
	}
}
