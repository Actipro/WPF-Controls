using ActiproSoftware.SampleBrowser;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.GettingStarted {

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
		/// Occurs when button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLaunchButtonClick(object sender, RoutedEventArgs e) {
			var viewModel = this.DataContext as ApplicationViewModel;
			if (viewModel != null)
				viewModel.OpenExternalSample(((ListBoxItem)quickStartListBox.SelectedItem).Tag as string);
		}

	}

}
