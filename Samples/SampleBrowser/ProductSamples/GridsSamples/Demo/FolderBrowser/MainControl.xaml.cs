using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;
using System.Threading;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.FolderBrowser;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly Random _random = new();
	private readonly FolderTreeNodeModel _thisPCModel;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		_thisPCModel = new FolderTreeNodeModel { Name = "This PC" };
		treeListBox.RootItem = _thisPCModel;

		// Expand the root node after adding it to the tree so the "expanding" event is raised/handled
		_thisPCModel.IsExpanded = true;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs before an item is expanded.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTreeListBoxItemExpanding(object sender, TreeListBoxItemExpansionEventArgs e) {
		// Quit if children collection is not empty (i.e., children have already been loaded)
		if (e.Item is not FolderTreeNodeModel { Children.Count: 0 } model)
			return;

		var delay = (int)(MaxDelay * _random.NextDouble());

		model.IsLoading = true;

		Task? task = null;
		if (model == _thisPCModel) {

			//
			// Load the logical drives from this PC
			//

			task = new Task(() => {
				string[]? logicalDrives = null;
				try {
					logicalDrives = Environment.GetLogicalDrives();
				}
				catch (IOException) { }

				Dispatcher.BeginInvoke(DispatcherPriority.Background, () => {
					model.Children.Clear();
					if (logicalDrives is not null) {
						foreach (var logicalDrive in logicalDrives) {
							var driveNode = new FolderTreeNodeModel {
								Name = logicalDrive,
								Path = logicalDrive
							};
							model.Children.Add(driveNode);
						}
					}
					model.IsLoading = false;
				});
			});
		}
		else {

			//
			// Load child folders to a logical drive or folder
			//

			task = new Task(() => {
				// Introduce a faux delay to demonstrate how the async loading works
				if (delay > 0)
					Thread.Sleep(delay);

				string[]? childFolders = null;
				if (model.Path is not null) {
					try {
						childFolders = Directory.GetDirectories(model.Path);
					}
					catch (IOException) { } // Ignore
				}

				Dispatcher.BeginInvoke(DispatcherPriority.Background, () => {
					model.Children.Clear();
					if (childFolders is not null) {
						foreach (var childFolder in childFolders) {
							var folderInfo = new DirectoryInfo(childFolder);
							if ((folderInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
								var childFolderModel = new FolderTreeNodeModel {
									Name = Path.GetFileName(childFolder),
									Path = childFolder
								};
								model.Children.Add(childFolderModel);
							}
						}
					}
					model.IsLoading = false;
				});
			});
		}

		task.Start();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The maximum delay.
	/// </summary>
	public int MaxDelay { get; set; } = 2000;

}
