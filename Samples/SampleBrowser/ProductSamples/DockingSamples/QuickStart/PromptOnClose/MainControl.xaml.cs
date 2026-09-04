using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.Windows.Controls.Docking;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.PromptOnClose;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _documentIndex = 3;

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
	/// Occurs before one or more docking windows are closed, allowing for cancellation of the close.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsClosing(object sender, DockingWindowsEventArgs e) {
		var documents = e.Windows.OfType<DocumentWindow>().ToArray();
		if (documents.Length > 0) {
			var message = new StringBuilder("Are you sure you want to close:");
			foreach (var document in documents)
				message.Append("\r\n* " + document.FileName);

			if (MessageBox.Show(message.ToString(), "Confirm Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No) {
				e.Cancel = true;
				e.Handled = true;
			}
		}
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnNewDocumentMenuItemClick(object sender, RoutedEventArgs e)
		=> DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnOpenDocumentMenuItemClick(object sender, RoutedEventArgs e)
		=> DocumentHelper.OpenTextDocumentWindow(dockSite);

}
