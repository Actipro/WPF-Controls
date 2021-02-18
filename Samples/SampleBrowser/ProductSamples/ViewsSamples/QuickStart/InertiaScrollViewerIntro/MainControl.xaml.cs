using System.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.InertiaScrollViewerIntro {

	/// <summary>
	/// The QuickStart for <see cref="InertiaScrollViewer"/>.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainControl"/> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Called when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnButtonClicked(object sender, RoutedEventArgs e) {
			MessageBox.Show("The button has been clicked.", "Button Clicked", MessageBoxButton.OK);
		}

		#endregion NON-PUBLIC PROCEDURES
	}
}
