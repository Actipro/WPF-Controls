using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbAutoMinimize;

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
	/// Handles the <see cref="Breadcrumb.ConvertItem"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbConvertItem(object? sender, BreadcrumbConvertItemEventArgs e)
		=> ConvertItemHelper.HandleConvertItem(sender, e);

	private void OnDisplayStyleListSelectionChanged(object sender, SelectionChangedEventArgs e) {
		var comboBox = (ComboBox)sender;
		var style = comboBox.SelectedIndex switch {
			1 => (Style)FindResource("BreadcrumbItemStyleEllipsis"),
			2 => (Style)FindResource("BreadcrumbItemStyleImageOnly"),
			3 => (Style)FindResource("BreadcrumbItemStyleNormal"),
			_ => (Style)FindResource("BreadcrumbItemStylePopupOnly")
		};

		if (style is not null) {
			// In order to dynamically switch the style of items already created, we need to recreate them. To do this we will
			//   state the current state of the Breadcrumb, clear it's selected and root items, then restore the state. Keep
			//   in mind that this process is only needed because we are trying to demonstrate different styles that can be
			//   applied during runtime. Typically, a style would be applied up front and not changed during runtime.
			//

			// Save the current state
			var maxTailItemCount = breadcrumb.MaxTailItemCount;
			var rootItem = breadcrumb.RootItem;
			var selectedItem = breadcrumb.SelectedItem;
			object? tailItem = null;
			if (breadcrumb.SelectedContainer is not null) {
				var container = breadcrumb.SelectedContainer.ExpandedContainer;
				while (container is not null) {
					tailItem = container.DataContext;
					container = container.ExpandedContainer;
				}
			}

			// Clear out the Breadcrumb
			breadcrumb.MaxTailItemCount = 0;
			breadcrumb.RootItem = null;
			breadcrumb.UpdateLayout();

			// Set the new style
			breadcrumb.ItemContainerStyle = style;

			// Restore the state
			breadcrumb.RootItem = rootItem;
			breadcrumb.MaxTailItemCount = maxTailItemCount;
			breadcrumb.UpdateLayout();
			if (tailItem is not null) {
				breadcrumb.SelectedItem = tailItem;
				breadcrumb.UpdateLayout();
			}
			if (selectedItem is not null) {
				breadcrumb.SelectedItem = selectedItem;
				breadcrumb.UpdateLayout();
			}
		}
	}

	private void OnSelectLeafItemClick(object? sender, RoutedEventArgs e)
		=> breadcrumb.SelectedPath = @"Desktop\Computer\Local Disk (C:)\Program Files\Actipro Software\WPF Controls";

}
