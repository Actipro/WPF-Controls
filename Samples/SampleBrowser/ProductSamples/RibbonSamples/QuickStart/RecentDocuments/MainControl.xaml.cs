using ActiproSoftware.ProductSamples.RibbonSamples.Common;
using ActiproSoftware.Windows.DocumentManagement;
using Microsoft.Win32;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.RecentDocuments;

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
		// Register UI providers before doing anything else, including InitializeComponent
		Demo.DocumentEditor.ApplicationCommands.RegisterUIProvidersForNonRibbonCommands();

		InitializeComponent();

		// Populate some sample recent documents
		DocumentReferenceGenerator.BindRecentDocumentManager(recentDocManager);

		// Add command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e) {
		if (e.Parameter is IDocumentReference documentReference) {
			// Process recent document clicks
			MessageBox.Show($"Open document '{documentReference.Name}' here.", "Open Recent Document", MessageBoxButton.OK, MessageBoxImage.Information);
			return;
		}

		// Show the open file dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Filter = "All Files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Add a new document reference to the recent document manager by calling the helper notify method...
			//   Alternatively you could create a DocumentReference and add it to recentDocManager.Documents manually
			//   but the benefit of this helper is that it checks for an existing Uri match so that you don't add duplicates
			recentDocManager.NotifyDocumentOpened(new Uri(dialog.FileName));
		}

	}

}
