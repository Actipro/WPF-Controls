using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;
using System.IO;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.FolderBrowser {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Random rnd = new Random();
		private FolderTreeNodeModel thisPCModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			thisPCModel = new FolderTreeNodeModel();
			thisPCModel.Name = "This PC";
			treeListBox.RootItem = thisPCModel;

			thisPCModel.IsExpanded = true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs before an item is expanded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>TreeListBoxItemExpansionEventArgs</c> that contains data related to this event.</param>
		private void OnTreeListBoxItemExpanding(object sender, TreeListBoxItemExpansionEventArgs e) {
			var model = e.Item as FolderTreeNodeModel;

			// Quit if some items are already loaded
			if (model.Children.Count > 0)
				return;

			var delay = (int)(this.MaxDelay * rnd.NextDouble());

			model.IsLoading = true;

			Task task = null;
			if (model == thisPCModel) {
				task = new Task(() => {
					string[] logicalDrives = null;
					try {
						logicalDrives = Environment.GetLogicalDrives();
					}
					catch (IOException) { }

					this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => {
						model.Children.Clear();
						if (logicalDrives != null) {
							foreach (var logicalDrive in logicalDrives) {
								var driveNode = new FolderTreeNodeModel();
								driveNode.Name = logicalDrive;
								driveNode.Path = logicalDrive;
								model.Children.Add(driveNode);
							}
						}
						model.IsLoading = false;
					}));
				});
			}
			else {
				task = new Task(() => {
					// Introduce a faux delay to demonstrate how the async loading works
					if (delay > 0)
						Thread.Sleep(delay);

					string[] childFolders = null;
					try {
						childFolders = Directory.GetDirectories(model.Path);
					}
					catch (IOException) {}

					this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => {
						model.Children.Clear();
						if (childFolders != null) {
							foreach (var childFolder in childFolders) {
								var folderInfo = new DirectoryInfo(childFolder);
								if ((folderInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
									var childFolderModel = new FolderTreeNodeModel();
									childFolderModel.Name = Path.GetFileName(childFolder);
									childFolderModel.Path = childFolder;
									model.Children.Add(childFolderModel);
								}
							}
						}
						model.IsLoading = false;
					}));
				});
			}

			task.Start();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the maximum delay.
		/// </summary>
		/// <value>The maximum delay.</value>
		public int MaxDelay { get; set; } = 2000;

	}

}
