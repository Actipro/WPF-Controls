using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarContextMenuCustomization;

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
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the navigation bar's customize button is clicked, allowing you to change the <c>ContextMenu</c> that is displayed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnNavigationBarCustomizeButtonClick(object sender, ContextMenuItemRoutedEventArgs e) {
		// Add a custom menu item to the end of the context menu that will be displayed
		e.Item.Items.Add(new Separator());

		var menuItem = new MenuItem {
			Header = "Custom menu item"
		};
		e.Item.Items.Add(menuItem);
	}

}
