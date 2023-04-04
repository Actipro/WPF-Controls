using System.Diagnostics;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
	/// </summary>
	public partial class SampleXamlControl : SampleControlBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleXamlControl"/> class.
		/// </summary>
		public SampleXamlControl() {
			InitializeComponent();

			// Configure this code-behind to be the view model for this sample
			this.DataContext = this;
			
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnDialogCancelButtonClick(object sender, System.Windows.RoutedEventArgs e) {
			Debug.WriteLine("Cancel");

			// TODO: How to close the popup from the cancel button?
		}
	}
}
