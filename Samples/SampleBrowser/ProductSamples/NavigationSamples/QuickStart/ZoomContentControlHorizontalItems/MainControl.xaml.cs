using System.Windows;
using System.Windows.Threading;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlAdditionalItems {

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
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the Click event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnButtonClick(object sender, RoutedEventArgs e) {
			MessageBox.Show("You clicked a button", "ZoomContentControl", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Handles the <c>Checked</c> event of the <see cref="logoRadioButton"/> or <see cref="buttonRadioButton"/> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
			this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)delegate(object arg) {
				this.zoomContentControl.BeginUpdate();
				try {
					this.zoomContentControl.CenterView();
				}
				finally {
					this.zoomContentControl.EndUpdate(false);
				}
				return null;
			}, null);
		}
	}
}