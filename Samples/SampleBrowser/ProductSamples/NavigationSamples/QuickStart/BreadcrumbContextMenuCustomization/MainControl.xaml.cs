using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;
using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbContextMenuCustomization;

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

	private void OnMenuItemClick(object? sender, RoutedEventArgs e)
		=> breadcrumb.SelectedPath = @"Desktop\Recycle Bin";

	/// <summary>
	/// Handles the <see cref="Breadcrumb.ConvertItem"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbConvertItem(object? sender, BreadcrumbConvertItemEventArgs e)
		=> ConvertItemHelper.HandleConvertItem(sender, e);

	/// <summary>
	/// Occurs when the BreadcrumbItem's navigate button is clicked, allowing you to change the <c>ContextMenu</c> that is displayed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbItemNavigateButtonClick(object? sender, ContextMenuItemRoutedEventArgs e) {
		// Add a custom menu item to the end of the context menu that will be displayed
		e.Item.Items.Add(new Separator());

		var menuItem = new MenuItem {
			Header = "Jump to Recycle Bin (Custom)"
		};
		menuItem.Click += OnMenuItemClick;
		e.Item.Items.Add(menuItem);
	}

	/// <summary>
	/// Occurs when the Breadcrumb's overflow button is clicked, allowing you to change the <c>ContextMenu</c> that is
	/// displayed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbOverflowButtonClick(object? sender, ContextMenuItemRoutedEventArgs e) {
		// Add a custom menu item to the end of the context menu that will be displayed
		e.Item.Items.Add(new Separator());

		var menuItem = new MenuItem {
			Header = "Jump to Recycle Bin (Custom)"
		};
		menuItem.Click += OnMenuItemClick;
		e.Item.Items.Add(menuItem);
	}

}
