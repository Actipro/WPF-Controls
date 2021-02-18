using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.MacroRecording {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.Loaded += new RoutedEventHandler(OnLoaded);
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate(object arg) {
				this.SetStatusMessage(null);

				editor.Focus();
				return null;
			}, null);
		}
		
		/// <summary>
		/// Occurs when the <c>SyntaxEditor.MacroRecordingStateChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorMacroRecordingStateChanged(object sender, RoutedEventArgs e) {
			string statusMessage;
			switch (editor.MacroRecording.State) {
				case MacroRecordingState.Recording:
					statusMessage = "Macro recording is active";
					recordMacroButtonImage.Source = new BitmapImage(new Uri("/Images/Icons/MacroRecordingStop16.png", UriKind.Relative));
					recordMacroButtonTextBlock.Text = "Stop Recording";
					pauseRecordingButton.IsChecked = false;
					pauseRecordingButtonTextBlock.Text = "Pause Recording";
					break;
				case MacroRecordingState.Paused:
					statusMessage = "Macro recording is paused";
					pauseRecordingButton.IsChecked = true;
					pauseRecordingButtonTextBlock.Text = "Resume Recording";
					break;
				default:
					statusMessage = null;
					recordMacroButtonImage.Source = new BitmapImage(new Uri("/Images/Icons/MacroRecordingRecord16.png", UriKind.Relative));
					recordMacroButtonTextBlock.Text = "Record Macro";
					pauseRecordingButton.IsChecked = false;
					pauseRecordingButtonTextBlock.Text = "Pause Recording";
					break;
			}

			this.SetStatusMessage(statusMessage);
		}

		/// <summary>
		/// Sets the status message.
		/// </summary>
		/// <param name="statusMessage">The status message.</param>
		private void SetStatusMessage(string statusMessage) {
			var viewModel = this.DataContext as ApplicationViewModel;
			if (viewModel != null)
				viewModel.StatusMessage = statusMessage;
		}
		
    }

}