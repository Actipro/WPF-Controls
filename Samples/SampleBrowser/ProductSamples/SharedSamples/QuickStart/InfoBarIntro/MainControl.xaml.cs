using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Input;
using System;
using System.Windows;
using System.Windows.Input;


namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.InfoBarIntro {

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

		private void OnInfoBarClosing(object sender, InfoBarClosingEventArgs e) {
			if (cancelCloseCheckBox.IsChecked == true) {
				ThemedMessageBox.Show($"Closing of the info bar for reason '{e.Reason}' canceled within the 'Closing' event.", "InfoBar Close Canceled", MessageBoxButton.OK, MessageBoxImage.Warning);

				// Prevent the info bar from closing
				e.Cancel = true;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ICommand ActionCommand { get; } = new DelegateCommand<object>(
			_ => {
				ThemedMessageBox.Show("Processing of a custom action was requested by an info bar.", "InfoBar Action Requested", MessageBoxButton.OK, MessageBoxImage.Information);
			});

		public ICommand CloseButtonCommand { get; } = new DelegateCommand<object>(
			(param) => {
				if (param is InfoBar infoBar) {
					var result = ThemedMessageBox.Show($"Are you sure you want to close the '{infoBar.Title}' info bar?",
						"Close Info Bar?",
						button: MessageBoxButton.YesNo,
						icon: MessageBoxImage.Question);

					if (result == MessageBoxResult.Yes) {
						// Close the info bar
						infoBar.IsOpen = false;
					}
				}
			});

		public ICommand NoCloseCommand { get; } = new DelegateCommand<object>(
			_ => {
				// The info bar will stay open unless the InfoBar.IsOpen property is set to false
				ThemedMessageBox.Show("This sample shows the InfoBar close button for illustration purposes only and will keep the info bar open.", "InfoBar Demo", MessageBoxButton.OK, MessageBoxImage.Information);
			});

		public ICommand ShowInfoBarCommand { get; } = new DelegateCommand<object>(
			(param) => {
				if (param is InfoBar infoBar) {
					infoBar.IsOpen = true;
				}
			});

	}

}