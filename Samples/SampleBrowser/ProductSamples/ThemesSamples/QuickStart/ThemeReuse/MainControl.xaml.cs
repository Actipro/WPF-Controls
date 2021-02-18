using System.Windows;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.ThemeReuse {

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
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCloseButtonClick(object sender, RoutedEventArgs e) {
			dialog.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenButtonClick(object sender, RoutedEventArgs e) {
			dialog.Visibility = Visibility.Visible;
		}

	}
}