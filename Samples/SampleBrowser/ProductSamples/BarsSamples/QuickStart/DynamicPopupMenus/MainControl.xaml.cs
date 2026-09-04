using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.DocumentManagement;
using Microsoft.Win32;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DynamicPopupMenus;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Configure command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecute));

		// Bind RecentDocumentManager to pre-populated sample data for the recent document list
		//   so we have something to show
		DocumentReferenceGenerator.BindRecentDocumentManager(RecentDocumentManager);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the <see cref="ApplicationCommands.Open"/> RoutedCommand is executed.
	/// </summary>
	private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e) {
		e.Handled = true;

		if (e.Parameter is IDocumentReference docReference) {
			// Process recent document clicks
			MessageBox.Show($"Open document '{docReference.Name}' here.", "Open Recent Document", MessageBoxButton.OK, MessageBoxImage.Information);
			if (docReference.Location is not null) {
				// Notify the recent document manager that an existing document is being opened and
				//   it will update the IDocumentReference.LastOpenedDateTime property
				RecentDocumentManager.NotifyDocumentOpened(docReference.Location);
			}
			return;
		}

		// Show the open file dialog
		var dialog = new OpenFileDialog() {
			CheckFileExists = true,
			Filter = "All Files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Add a new document reference to the recent document manager by calling the helper notify method...
			//   Alternatively you could create a DocumentReference and add it to RecentDocumentManager.Documents manually
			//   but the benefit of this helper is that it checks for an existing Uri match so that you don't add duplicates
			var fileInfo = new FileInfo(dialog.FileName);
			var newDocReference = RecentDocumentManager.NotifyDocumentOpened(new Uri(fileInfo.FullName));
			MessageBox.Show($"The document '{newDocReference.Name}' has been added/updated in the recent documents list.", "Open Document", MessageBoxButton.OK, MessageBoxImage.Information);

			// Optionally define additional details about the new document (if not already defined)
			if (newDocReference.ImageSourceSmall is null)
				newDocReference.ImageSourceSmall = ImageLoader.GetIcon("BlankPage16.png");
			if (newDocReference.ImageSourceLarge is null)
				newDocReference.ImageSourceLarge = ImageLoader.GetIcon("BlankPage32.png");
		}

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The manager class for maintaining a list of recent document references.
	/// </summary>
	public RecentDocumentManager RecentDocumentManager { get; } = new RecentDocumentManager();
}
