using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.DocumentManagement;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DynamicPopupMenus {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public abstract class SampleControlBase : UserControl {

		protected readonly Random random = new Random();

		#region Dependency Properties

		public static readonly DependencyProperty RecentDocumentManagerProperty = DependencyProperty.Register(nameof(RecentDocumentManager), typeof(RecentDocumentManager), typeof(SampleControlBase));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the items for a dynamically generated menu.
		/// </summary>
		/// <param name="menu">The menu whose items are to be generated.</param>
		private void InitializeDynamicMenuItems(ItemsControl menu) {
			// Use the menu's key to determine which items will be added
			var key = BarControlService.GetKey(menu);
			switch (key) {
				case BarControlKeys.DynamicMenuItem:
				case BarControlKeys.DynamicPopupButton:
				case BarControlKeys.DynamicPopupOverflowButton:
				case BarControlKeys.DynamicStandaloneToolBarButton:
					// Populate with sample menu items that clearly show they are dynamically generated
					var dynamicMenuItems = ResolveMenuItemsList(menu);
					if (dynamicMenuItems != null) {
						dynamicMenuItems.Clear();
						PopulateSampleDynamicMenuItems(key, dynamicMenuItems);
					}
					break;

				case BarControlKeys.OpenDocument:
					// Populate recently opened documents for an Open command
					var openDocumentItems = ResolveMenuItemsList(menu);
					if (openDocumentItems != null) {
						openDocumentItems.Clear();
						PopulateOpenDocumentMenuItems(openDocumentItems);
					}
					break;
			}
		}

		/// <summary>
		/// Resolves the list of menu items associated with a menu.
		/// </summary>
		/// <param name="menu">The menu to examine.</param>
		/// <returns>An <see cref="IList"/> for the menu items associated with a menu.</returns>
		private static IList ResolveMenuItemsList(ItemsControl menu) {
			// Always work with an ItemsSource when available; otherwise, fall back to the Items collection
			return (menu?.ItemsSource as IList) ?? menu?.Items;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Processes the <see cref="ActiproSoftware.Windows.Controls.Bars.Ribbon.MenuOpening"/> event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		protected void NotifyMenuOpening(object sender, BarMenuEventArgs e) {
			// This demo focuses only on command popup menus
			if (e.Kind == BarMenuKind.PopupButtonMenu
				|| e.Kind == BarMenuKind.MenuItemSubmenu) {

				InitializeDynamicMenuItems(e.Menu);
			}
		}

		/// <summary>
		/// Populates the list of menu items for the open document popup menu.
		/// </summary>
		/// <param name="menuItems">The list of menu items to populate.</param>
		protected abstract void PopulateOpenDocumentMenuItems(IList menuItems);

		/// <summary>
		/// Populates a list of dynamically generated menu items for the generic sample controls.
		/// </summary>
		/// <param name="key">The key of the control whose menu is being generated.</param>
		/// <param name="menuItems">The list of menu items to populate.</param>
		protected abstract void PopulateSampleDynamicMenuItems(string key, IList menuItems);

		/// <summary>
		/// Gets or sets the recent document manager associated with this control.
		/// </summary>
		public RecentDocumentManager RecentDocumentManager {
			get => (RecentDocumentManager)GetValue(RecentDocumentManagerProperty);
			set => SetValue(RecentDocumentManagerProperty, value);
		}

	}

}
