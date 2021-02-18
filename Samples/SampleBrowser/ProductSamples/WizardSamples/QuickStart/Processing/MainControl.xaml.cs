using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Wizard;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.Processing {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {
		
		private BackgroundWorker simpleProcessingBackgroundWorker;

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
		/// Occurs when the control is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void startProcessingButton_Click(object sender, RoutedEventArgs e) {
			// Disable the buttons while processing occurs
			startProcessingButton.IsEnabled = false;
			processingPage.CancelButtonEnabled = false;
			processingPage.BackButtonEnabled = false;
			processingPage.NextButtonEnabled = false;

			// Initialize the background worker
			if (simpleProcessingBackgroundWorker == null) {
				simpleProcessingBackgroundWorker = new BackgroundWorker();
				simpleProcessingBackgroundWorker.WorkerReportsProgress = true;
				simpleProcessingBackgroundWorker.DoWork += delegate(object sndr, DoWorkEventArgs eventArgs) {
					// Simply sleep for 100ms to simulate processing
					for (int index = 0; index <= 10; index++) {
						Thread.Sleep(100);
						simpleProcessingBackgroundWorker.ReportProgress(index * 10);
					}
				};
				simpleProcessingBackgroundWorker.ProgressChanged += delegate(object sndr, ProgressChangedEventArgs eventArgs) {
					progressTextBlock.Text = (eventArgs.ProgressPercentage < 100 ? eventArgs.ProgressPercentage + "% complete" : "Processing completed");
					progressBar.Value = eventArgs.ProgressPercentage; 
				};
				simpleProcessingBackgroundWorker.RunWorkerCompleted += delegate(object sndr, RunWorkerCompletedEventArgs eventArgs) {
					// Re-enable the buttons now that the processing is complete
					startProcessingButton.IsEnabled = true;
					processingPage.CancelButtonEnabled = null;
					processingPage.BackButtonEnabled = null;
					processingPage.NextButtonEnabled = null;
				};
			}

			// Start the background work
			simpleProcessingBackgroundWorker.RunWorkerAsync();
		}
		
		/// <summary>
		/// Occurs after the wizard's selected page has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void wizard_SelectedPageChanged(object sender, WizardSelectedPageChangeEventArgs e) {
			if (e.NewSelectedPage == processingPage) {
				// Clear the processing amount
				progressBar.Value = 0;
			}
		}

	}
}