using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.PrimaryDocumentTracking;

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

		Loaded += (sender, e) => {
			// Activate the first tool window
			if (dockSite.ToolWindows.Count > 0)
				dockSite.ToolWindows[0].Activate();
		};
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the primary document is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSitePrimaryDocumentChanged(object sender, DockingWindowEventArgs e)
		=> UpdateStatusBar();

	/// <summary>
	/// Occurs when a docking window is activated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowActivated(object sender, DockingWindowEventArgs e)
		=> UpdateStatusBar();

	/// <summary>
	/// Occurs when a docking window is deactivated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowDeactivated(object sender, DockingWindowEventArgs e)
		=> UpdateStatusBar();

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

	/// <summary>
	/// Updates the status bar.
	/// </summary>
	private void UpdateStatusBar() {
		primaryDocumentTextBlock.Text = (dockSite.PrimaryDocument is not null ? dockSite.PrimaryDocument.Title : "(none)");
		activeWindowTextBlock.Text = (dockSite.ActiveWindow is not null ? dockSite.ActiveWindow.Title : "(none)");
	}

}
