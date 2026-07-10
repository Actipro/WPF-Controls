using ActiproSoftware.Windows.Controls.Wizard;
using System.Threading;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.Processing;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private BackgroundWorker? _simpleProcessingBackgroundWorker;

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

	private void OnStartProcessingButtonClick(object sender, RoutedEventArgs e) {
		// Disable the buttons while processing occurs
		startProcessingButton.IsEnabled = false;
		processingPage.CancelButtonEnabled = false;
		processingPage.BackButtonEnabled = false;
		processingPage.NextButtonEnabled = false;

		// Initialize the background worker
		if (_simpleProcessingBackgroundWorker is null) {
			_simpleProcessingBackgroundWorker = new BackgroundWorker {
				WorkerReportsProgress = true
			};
			_simpleProcessingBackgroundWorker.DoWork += (_, _) => {
				// Simply sleep for 100ms to simulate processing
				for (var index = 0; index <= 10; index++) {
					Thread.Sleep(100);
					_simpleProcessingBackgroundWorker.ReportProgress(index * 10);
				}
			};
			_simpleProcessingBackgroundWorker.ProgressChanged += (_, e) => {
				progressTextBlock.Text = (e.ProgressPercentage < 100 ? e.ProgressPercentage + "% complete" : "Processing completed");
				progressBar.Value = e.ProgressPercentage;
			};
			_simpleProcessingBackgroundWorker.RunWorkerCompleted += (_, _) => {
				// Re-enable the buttons now that the processing is complete
				startProcessingButton.IsEnabled = true;
				processingPage.CancelButtonEnabled = null;
				processingPage.BackButtonEnabled = null;
				processingPage.NextButtonEnabled = null;
			};
		}

		// Start the background work
		_simpleProcessingBackgroundWorker.RunWorkerAsync();
	}

	private void OnWizardSelectedPageChanged(object sender, WizardSelectedPageChangeEventArgs e) {
		if (e.NewSelectedPage == processingPage) {
			// Clear the processing amount
			progressBar.Value = 0;
		}
	}

}
