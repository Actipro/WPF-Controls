using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeSummary;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private StatData? _currentData;
	private BackgroundWorker? _processingBackgroundWorker;

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	private class StatData(string folderPath) {
		public int CSharpFileCount;
		public string FolderPath { get; } = folderPath;
		public int NonWhitespaceLineCount;
		public int VBFileCount;
		public int WhitespaceLineCount;
		public int XamlFileCount;
	}

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

	/// <summary>
	/// Adds statistics for the specified file.
	/// </summary>
	/// <param name="data">The data object to update.</param>
	/// <param name="path">The path to the file.</param>
	private static void AddStatsForFile(StatData data, string path) {
		if (!File.Exists(path))
			return;

		// Get the file text
		try {
			var text = File.ReadAllText(path);

			// Calculate stats
			var statistics = new Text.Implementation.TextStatistics(text);

			// Append stats
			switch (Path.GetExtension(path).ToLower()) {
				case ".cs":
					data.CSharpFileCount++;
					break;
				case ".vb":
					data.VBFileCount++;
					break;
				case ".xaml":
					data.XamlFileCount++;
					break;
			}
			data.NonWhitespaceLineCount += statistics.NonWhitespaceLines;
			data.WhitespaceLineCount += statistics.WhitespaceLines;
		}
		catch { } // Ignore
	}

	private void OnCalculateStatisticsButtonClick(object sender, RoutedEventArgs e) {
		// Validate path
		var folderPath = folderTextBox.Text.Trim();
		if ((string.IsNullOrEmpty(folderPath)) || (!Directory.Exists(folderPath))) {
			MessageBox.Show("Please enter a valid folder path.", "Invalid Folder", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}

		// Initialize the background worker
		_processingBackgroundWorker = new BackgroundWorker {
			WorkerReportsProgress = true
		};
		_processingBackgroundWorker.DoWork += (_, e) =>  {
			if (e.Argument is not StatData data)
				return;

			// Recurse and queue files
			var queuedFiles = new List<string>();
			QueueFolder(queuedFiles, data.FolderPath);

			// Add stats for files
			for (var index = 0; index < queuedFiles.Count; index++) {
				_processingBackgroundWorker.ReportProgress(
					(int)((index + 1) * 100.0 / queuedFiles.Count),
					string.Format("Examining {0}...", queuedFiles[index])
				);
				AddStatsForFile(data, queuedFiles[index]);
			}

		};
		_processingBackgroundWorker.ProgressChanged += (_, e) => {
			messageTextBox.Text = e.UserState?.ToString();
			progressBar.Value = e.ProgressPercentage;
		};
		_processingBackgroundWorker.RunWorkerCompleted += (_, _) => {
			if (_currentData is not null) {
				// Show results
				List<Text.ITextStatistic> statistics = [
					Text.Implementation.TextStatistics.CreateStatistic("RootPath", "Root Path", _currentData.FolderPath),
					Text.Implementation.TextStatistics.CreateStatistic("TotalFiles", "Total Files", _currentData.CSharpFileCount + _currentData.VBFileCount + _currentData.XamlFileCount),
					Text.Implementation.TextStatistics.CreateStatistic("C#Files", "C# Files", _currentData.CSharpFileCount),
					Text.Implementation.TextStatistics.CreateStatistic("VBFiles", "VB Files", _currentData.VBFileCount),
					Text.Implementation.TextStatistics.CreateStatistic("XAMLFiles", "XAML Files", _currentData.XamlFileCount),
					Text.Implementation.TextStatistics.CreateStatistic("TotalLines", "Total Lines", _currentData.NonWhitespaceLineCount + _currentData.WhitespaceLineCount),
					Text.Implementation.TextStatistics.CreateStatistic("NonWhitespaceLines", "Non-Whitespace Lines", _currentData.NonWhitespaceLineCount),
					Text.Implementation.TextStatistics.CreateStatistic("WhitespaceLines", "Whitespace Lines", _currentData.WhitespaceLineCount),
					Text.Implementation.TextStatistics.CreateStatistic("WhitespaceLinePercent", "Whitespace Line %", _currentData.WhitespaceLineCount * 100.0 / Math.Max(1, _currentData.NonWhitespaceLineCount + _currentData.WhitespaceLineCount)),
				];
				resultsListView.ItemsSource = statistics;
			}

			// Processing is complete
			messageTextBox.Text = "Ready";
			progressBar.Value = 0;
			calculateStatisticsButton.IsEnabled = true;
		};

		// Initialize UI
		calculateStatisticsButton.IsEnabled = false;
		messageTextBox.Text = "Discovering files...";
		progressBar.Value = 0;

		// Start the background work
		_currentData = new StatData(folderPath);
		_processingBackgroundWorker.RunWorkerAsync(_currentData);
	}

	/// <summary>
	/// Queues the files in a folder.
	/// </summary>
	/// <param name="queuedFiles">The list of queued files.</param>
	/// <param name="path">The path to the folder.</param>
	/// <param name="searchPattern">The search pattern.</param>
	private static void QueueFiles(List<string> queuedFiles, string path, string searchPattern) {
		var files = Directory.GetFiles(path, searchPattern);
		if (files is not null) {
			foreach (var file in files)
				queuedFiles.Add(file);
		}
	}

	/// <summary>
	/// Queues up files in the specified folder.
	/// </summary>
	/// <param name="queuedFiles">The list of queued files.</param>
	/// <param name="path">The path to the folder.</param>
	private static void QueueFolder(List<string> queuedFiles, string path) {
		QueueFiles(queuedFiles, path, "*.cs");
		QueueFiles(queuedFiles, path, "*.vb");
		QueueFiles(queuedFiles, path, "*.xaml");

		var childFolders = Directory.GetDirectories(path);
		if (childFolders is not null) {
			foreach (var childFolder in childFolders)
				QueueFolder(queuedFiles, childFolder);
		}
	}

}
