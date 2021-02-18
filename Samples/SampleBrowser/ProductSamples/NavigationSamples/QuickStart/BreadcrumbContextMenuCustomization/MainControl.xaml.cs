using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbContextMenuCustomization {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the Click event of the menuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnMenuItemClick(object sender, RoutedEventArgs e) {
			this.breadcrumb.SelectedPath = "Desktop\\Recycle Bin";
		}

		/// <summary>
		/// Handles the ConvertItem event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ActiproSoftware.Windows.Controls.Navigation.BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			ConvertItemHelper.HandleConvertItem(sender, e);
		}

		/// <summary>
		/// Occurs when the BreadcrumbItem's navigate button is clicked, allowing you to change the <c>ContextMenu</c> that is
		/// displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="ItemRoutedEventArgs{ContextMenu}"/> that contains the event data.</param>
		private void OnBreadcrumbItemNavigateButtonClick(object sender, ContextMenuItemRoutedEventArgs e) {
			// Add a custom menu item to the end of the context menu that will be displayed
			e.Item.Items.Add(new Separator());
			MenuItem menuItem = new MenuItem();
			menuItem.Header = "Jump to Recycle Bin (Custom)";
			menuItem.Click += new RoutedEventHandler(OnMenuItemClick);
			e.Item.Items.Add(menuItem);
		}

		/// <summary>
		/// Occurs when the Breadcrumb's overflow button is clicked, allowing you to change the <c>ContextMenu</c> that is
		/// displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="ItemRoutedEventArgs{ContextMenu}"/> that contains the event data.</param>
		private void OnBreadcrumbOverflowButtonClick(object sender, ContextMenuItemRoutedEventArgs e) {
			// Add a custom menu item to the end of the context menu that will be displayed
			e.Item.Items.Add(new Separator());
			MenuItem menuItem = new MenuItem();
			menuItem.Header = "Jump to Recycle Bin (Custom)";
			menuItem.Click += new RoutedEventHandler(OnMenuItemClick);
			e.Item.Items.Add(menuItem);
		}
	}
}