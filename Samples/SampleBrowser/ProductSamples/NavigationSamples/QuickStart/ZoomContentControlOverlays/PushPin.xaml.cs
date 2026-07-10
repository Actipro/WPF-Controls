using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlOverlays;

/// <summary>
/// Interaction logic for PushPin.xaml
/// </summary>
public partial class PushPin : Control {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public PushPin() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnCloseButtonClick(object sender, RoutedEventArgs e) {
		var zoomContentControl = this.FindAncestorOfType<ZoomContentControl>();
		zoomContentControl?.Overlays.Remove(this);
	}

}
