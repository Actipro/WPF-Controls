using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.TabbedMdiOnly;

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
