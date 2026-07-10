using ActiproSoftware.Windows.Themes;
using System.Threading;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeProcessingOverlay;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow {

	private readonly BackgroundWorker _backgroundWorker;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();

		_backgroundWorker = new BackgroundWorker();
		_backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
		_backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnBackgroundWorkerDoWork(object? sender, DoWorkEventArgs e) {
		// This example just delays several seconds instead of doing real work
		Thread.Sleep(TimeSpan.FromSeconds(3));
	}

	private void OnBackgroundWorkerRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) {
		// Hide the overlay
		WindowChrome.SetIsOverlayVisible(this, false);
	}

	private void OnStartProcessingButtonClick(object? sender, RoutedEventArgs e) {
		if (!_backgroundWorker.IsBusy) {
			// Show the overlay
			WindowChrome.SetIsOverlayVisible(this, true);

			// Start the background worker
			_backgroundWorker.RunWorkerAsync();
		}
	}

}
