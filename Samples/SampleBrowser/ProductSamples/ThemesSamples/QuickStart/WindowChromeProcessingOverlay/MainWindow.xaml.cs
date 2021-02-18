using ActiproSoftware.Windows.Themes;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeProcessingOverlay {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		private BackgroundWorker backgroundWorker;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
			backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the background worker needs to complete work.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DoWorkEventArgs"/> that contains the event data.</param>
		private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e) {
			// This example just delays several seconds instead of doing real work
			Thread.Sleep(TimeSpan.FromSeconds(3));
		}

		/// <summary>
		/// Occurs when the background worker has completed work.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DoWorkEventArgs"/> that contains the event data.</param>
		private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			// Hide the overlay
			WindowChrome.SetIsOverlayVisible(this, false);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnStartProcessingButtonClick(object sender, RoutedEventArgs e) {
			if (!backgroundWorker.IsBusy) {
				// Show the overlay
				WindowChrome.SetIsOverlayVisible(this, true);

				// Start the background worker
				backgroundWorker.RunWorkerAsync();
			}
		}

	}

}