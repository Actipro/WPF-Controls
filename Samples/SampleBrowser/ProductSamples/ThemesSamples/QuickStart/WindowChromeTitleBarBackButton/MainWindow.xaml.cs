using System;
using System.Windows;
using System.Windows.Input;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeTitleBarBackButton {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			frame.Navigate(new Page1());
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnBrowseBackCommandExecuted(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Browse backward a page here.", "Back Executed", MessageBoxButton.OK, MessageBoxImage.Information);
		}

	}

}