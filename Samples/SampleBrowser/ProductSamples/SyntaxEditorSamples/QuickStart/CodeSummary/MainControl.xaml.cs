using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Controls.Docking;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeSummary {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		private StatData			currentData;
		private BackgroundWorker	processingBackgroundWorker;

		private class StatData {
			public int CSharpFileCount;
			public string FolderPath;
			public int NonWhitespaceLineCount;
			public int VBFileCount;
			public int WhitespaceLineCount;
			public int XamlFileCount;
		}

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
		/// Adds statistics for the specified file.
		/// </summary>
		/// <param name="data">The data object to update.</param>
		/// <param name="path">The path to the file.</param>
		private void AddStatsForFile(StatData data, string path) {
			if (!File.Exists(path))
				return;

			// Get the file text
			try {
				string text = File.ReadAllText(path);

				// Calculate stats
				ActiproSoftware.Text.Implementation.TextStatistics statistics = new ActiproSoftware.Text.Implementation.TextStatistics(text);

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
			catch {}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCalculateStatisticsButtonClick(object sender, RoutedEventArgs e) {
			// Validate path
			string folderPath = folderTextBox.Text.Trim();
			if ((string.IsNullOrEmpty(folderPath)) || (!Directory.Exists(folderPath))) {
				MessageBox.Show("Please enter a valid folder path.", "Invalid Folder", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			// Initialize the background worker
			processingBackgroundWorker = new BackgroundWorker();
			processingBackgroundWorker.WorkerReportsProgress = true;
			processingBackgroundWorker.DoWork += delegate(object sndr, DoWorkEventArgs eventArgs) {
				StatData data = (StatData)eventArgs.Argument;

				// Recurse and queue files
				List<string> queuedFiles = new List<string>();
				this.QueueFolder(queuedFiles, data.FolderPath);
				
				// Add stats for files
				for (int index = 0; index < queuedFiles.Count; index++) {
					processingBackgroundWorker.ReportProgress(
						(int)((index + 1) * 100.0 / queuedFiles.Count), 
						String.Format("Examining {0}...", queuedFiles[index])
						);
					this.AddStatsForFile(data, queuedFiles[index]);
				}

			};
			processingBackgroundWorker.ProgressChanged += delegate(object sndr, ProgressChangedEventArgs eventArgs) {
				messageTextBox.Text = eventArgs.UserState.ToString();
				progressBar.Value = eventArgs.ProgressPercentage;
			};
			processingBackgroundWorker.RunWorkerCompleted += delegate(object sndr, RunWorkerCompletedEventArgs eventArgs) {
				// Show results
				List<ActiproSoftware.Text.ITextStatistic> statistics = new List<ActiproSoftware.Text.ITextStatistic>();
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("RootPath", "Root Path", currentData.FolderPath));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("TotalFiles", "Total Files", currentData.CSharpFileCount + currentData.VBFileCount + currentData.XamlFileCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("C#Files", "C# Files", currentData.CSharpFileCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("VBFiles", "VB Files", currentData.VBFileCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("XAMLFiles", "XAML Files", currentData.XamlFileCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("TotalLines", "Total Lines", currentData.NonWhitespaceLineCount + currentData.WhitespaceLineCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("NonWhitespaceLines", "Non-Whitespace Lines", currentData.NonWhitespaceLineCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("WhitespaceLines", "Whitespace Lines", currentData.WhitespaceLineCount));
				statistics.Add(ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic("WhitespaceLinePercent", "Whitespace Line %", currentData.WhitespaceLineCount * 100.0 / Math.Max(1, currentData.NonWhitespaceLineCount + currentData.WhitespaceLineCount)));
				resultsListView.ItemsSource = statistics;

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
			currentData = new StatData();
			currentData.FolderPath = folderPath;
			processingBackgroundWorker.RunWorkerAsync(currentData);
		}
		
		/// <summary>
		/// Queues the files in a folder.
		/// </summary>
		/// <param name="queuedFiles">The list of queued files.</param>
		/// <param name="path">The path to the folder.</param>
		/// <param name="searchPattern">The search pattern.</param>
		private void QueueFiles(List<string> queuedFiles, string path, string searchPattern) {
			string[] files = Directory.GetFiles(path, searchPattern);
			if (files != null) {
				foreach (string file in files)
					queuedFiles.Add(file);
			}
		}

		/// <summary>
		/// Queues up files in the specified folder.
		/// </summary>
		/// <param name="queuedFiles">The list of queued files.</param>
		/// <param name="path">The path to the folder.</param>
		private void QueueFolder(List<string> queuedFiles, string path) {
			this.QueueFiles(queuedFiles, path, "*.cs");
			this.QueueFiles(queuedFiles, path, "*.vb");
			this.QueueFiles(queuedFiles, path, "*.xaml");

			string[] childFolders = Directory.GetDirectories(path);
			if (childFolders != null) {
				foreach (string childFolder in childFolders)
					this.QueueFolder(queuedFiles, childFolder);
			}
		}

    }

}