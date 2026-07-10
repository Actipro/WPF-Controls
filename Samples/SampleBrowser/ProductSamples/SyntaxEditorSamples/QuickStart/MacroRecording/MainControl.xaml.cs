using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.MacroRecording;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLoaded(object sender, RoutedEventArgs e) {
		Dispatcher.BeginInvoke(DispatcherPriority.Send, () => {
			SetStatusMessage(null);
			editor.Focus();
		});
	}

	private void OnSyntaxEditorMacroRecordingStateChanged(object sender, RoutedEventArgs e) {
		string? statusMessage;
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

		SetStatusMessage(statusMessage);
	}

	/// <summary>
	/// Sets the status message.
	/// </summary>
	/// <param name="statusMessage">The status message.</param>
	private void SetStatusMessage(string? statusMessage) {
		if (DataContext is ApplicationViewModel viewModel)
			viewModel.StatusMessage = statusMessage;
	}

}
