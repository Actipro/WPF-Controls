using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.DocumentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DynamicPopupMenus {

	/// <summary>
	/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
	/// </summary>
	public partial class SampleXamlControl : SampleControlBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleXamlControl"/> class.
		/// </summary>
		public SampleXamlControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a view model that is used as a placeholder child item.
		/// </summary>
		/// <returns>A new <see cref="BarButtonViewModel"/>.</returns>
		private BarMenuItem CreateChildPlaceholder() {
			// A BarPopupButton or BarSplitButton will always raise the MenuOpening
			// event even if they do not have children, but a BarMenuItem (which derives from MenuItem)
			// must define at least one child to be recognized as a MenuItem with a sub-menu.
			// Since BarPopupButton and BarSplitButton can be cloned to an overflow menu where they are
			// displayed as BarMenuItem, it is still important to add a placeholder child item.
			return new BarMenuItem() { Key = BarControlKeys.PlaceholderChild };
		}

		/// <summary>
		/// Processes the <see cref="ActiproSoftware.Windows.Controls.Bars.Ribbon.MenuOpening"/> event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void OnRibbonMenuOpening(object sender, BarMenuEventArgs e) {
			NotifyMenuOpening(sender, e);
		}

		/// <summary>
		/// Processes the <see cref="ActiproSoftware.Windows.Controls.Bars.Ribbon.MenuOpening"/> event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void OnStandaloneToolBarMenuOpening(object sender, BarMenuEventArgs e) {
			NotifyMenuOpening(sender, e);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void PopulateOpenDocumentMenuItems(IList menuItems) {
			// Dynamically add a menu item for each recent document
			var pinnedMenuItems = new List<object>();
			var unpinnedMenuItems = new List<object>();
			foreach (IDocumentReference docReference in RecentDocumentManager.FilteredDocuments) {
				var targetList = docReference.IsPinnedRecentDocument ? pinnedMenuItems : unpinnedMenuItems;
				targetList.Add(new BarMenuItem(docReference.Name) {
					CanCloneToRibbonQuickAccessToolBar = false,
					Command = ApplicationCommands.Open,
					CommandParameter = docReference,
					IsInputGestureTextVisible = false,
					LargeImageSource = docReference.ImageSourceLarge,
					SmallImageSource = docReference.ImageSourceSmall,
					ScreenTipHeader = docReference.Location?.LocalPath,
				});
			}
			bool includeHeaders = (pinnedMenuItems.Any() && unpinnedMenuItems.Any());
			if (pinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarMenuHeading("Pinned Documents"));
				foreach (var menuItem in pinnedMenuItems)
					menuItems.Add(menuItem);
			}
			if (unpinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarMenuHeading("Unpinned Documents"));
				foreach (var menuItem in unpinnedMenuItems)
					menuItems.Add(menuItem);
			}

			if (menuItems.Count == 0)
				menuItems.Add(new BarMenuItem("No recent documents") { CanCloneToRibbonQuickAccessToolBar = false, IsEnabled = false });

			menuItems.Add(new BarMenuSeparator());
			menuItems.Add(new BarMenuItem("Browse...") {
				CanCloneToRibbonQuickAccessToolBar = false,
				Command = ApplicationCommands.Open,
				LargeImageSource = ImageLoader.GetIcon("Open32.png"),
				SmallImageSource = ImageLoader.GetIcon("Open16.png"),
			});
		}

		/// <inheritdoc/>
		protected override void PopulateSampleDynamicMenuItems(string key, IList menuItems) {
			// Dynamically generate new menu items
			menuItems.Add(new BarMenuHeading(key));
			menuItems.Add(new BarMenuSeparator());
			var itemCount = random.Next(2, 9); // Randomize the number of menu items
			for (int index = 0; index < itemCount; index++) {
				var menuItem = new BarMenuItem(label: $"Dynamic Item #{(index + 1)}") {
					CanCloneToRibbonQuickAccessToolBar = false,
					Key = $"DynamicItem{(index + 1)}"
				};
				menuItems.Add(menuItem);
			}
			if (key != BarControlKeys.DynamicMenuItem) {
				// Add a menu item whose sub-menu is dynamically generated
				menuItems.Add(new BarMenuSeparator());
				menuItems.Add(new BarMenuItem() {
					CanCloneToRibbonQuickAccessToolBar = false,
					Key = BarControlKeys.DynamicMenuItem,
					Label = "Dynamic Sub-Menu",
					Items = { CreateChildPlaceholder() }
				});
			}
			menuItems.Add(new BarMenuSeparator());
			menuItems.Add(new BarMenuHeading($"Menu Created @ {DateTime.Now.ToLongTimeString()}"));
		}

	}

}
