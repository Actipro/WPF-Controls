using System.Windows.Threading;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlAdditionalItems;

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
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnButtonClick(object sender, RoutedEventArgs e)
		=> MessageBox.Show("You clicked a button", "ZoomContentControl", MessageBoxButton.OK, MessageBoxImage.Information);

	private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
		Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () => {
			zoomContentControl.BeginUpdate();
			try {
				zoomContentControl.CenterView();
			}
			finally {
				zoomContentControl.EndUpdate(animate: false);
			}
		});
	}

}
