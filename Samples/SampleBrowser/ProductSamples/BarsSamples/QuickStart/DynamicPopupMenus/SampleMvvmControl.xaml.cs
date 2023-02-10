using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DynamicPopupMenus {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : SampleControlBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleMvvmControl"/> class.
		/// </summary>
		public SampleMvvmControl()
		{
			InitializeComponent();

			// Bind the view to itself since we do not define an explicit view model
			this.DataContext = this;

			// Initialize the Ribbon and StandaloneToolBar view models
			InitializeRibbonViewModels();
			InitializeStandaloneToolBarViewModels();

			// Initialize the TextBox ContextMenu
			textBox.ContextMenu = CreateTextBoxContextMenu();

			// Ensure the textbox is focused when this sample is loaded
			this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (Action)(() => {
				textBox.Focus();
			}));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a view model that is used as a placeholder child item.
		/// </summary>
		/// <returns>A new <see cref="BarButtonViewModel"/>.</returns>
		private static BarButtonViewModel CreateChildPlaceholder() {
			// A BarPopupButtonViewModel or BarSplitButtonViewModel will always raise the MenuOpening
			// event even if they do not have children, but when displayed on a menu as a
			// BarMenuItem (which derives from MenuItem) it must define at least one child to be recognized
			// as a MenuItem with a sub-menu. Since BarPopupButtonViewModel and BarSplitButtonViewModel
			// can be cloned to an overflow menu where they are displayed as BarMenuItem, it is still
			// important to add a placeholder child item.
			return new BarButtonViewModel(BarControlKeys.PlaceholderChild);
		}

		/// <summary>
		/// Creates the context menu that will be assigned to the <see cref="TextBoxControl"/> used
		/// by this sample.
		/// </summary>
		/// <returns>A <see cref="ContextMenu"/> object.</returns>
		private ContextMenu CreateTextBoxContextMenu() {
			// Configure the context menu
			var contextMenu = new BarContextMenu() {

				// Make sure the ContextMenu has the same ItemContainerTemplateSelector as the Ribbon so
				// view models are properly templated
				ItemContainerTemplateSelector = Ribbon.ItemContainerTemplateSelector,

				Items = {
					new BarSplitButtonViewModel(BarControlKeys.OpenDocument) {
						Command = ApplicationCommands.Open,
						Description = "The recent documents in this popup menu are dynamically generated.",
						Label = "Open",
						LargeImageSource = ImageLoader.GetIcon("Open32.png"),
						SmallImageSource = ImageLoader.GetIcon("Open16.png"),
						MenuItems = { CreateChildPlaceholder() }
					},
					new BarPopupButtonViewModel(BarControlKeys.DynamicPopupButton) {
						Description = "The menu of this popup button is dynamically generated when the popup is opened.",
						Label = "Popup Button with Dynamic Menu",
						LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
						SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
						MenuItems = { CreateChildPlaceholder() }
					},
				}
			};

			// The context menu must have a RootBarControl set to either a Ribbon or StandaloneToolBar so
			// the MenuOpening event can be raised that allows for customizing the popup menu
			BarControlService.SetRootBarControl(contextMenu, ribbon);

			return contextMenu;
		}

		/// <summary>
		/// Initializes the view models for the ribbon.
		/// </summary>
		private void InitializeRibbonViewModels() {
			Ribbon = new RibbonViewModel(RibbonLayoutMode.Simplified) {
				IsApplicationButtonVisible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				Tabs = {
					new RibbonTabViewModel("MvvmSamples", "MVVM Samples") {
						Groups = {

							new RibbonGroupViewModel("Sample") {
								CanAutoCollapse = false,
								Items = {
									new BarSplitButtonViewModel(BarControlKeys.OpenDocument) {
										Command = ApplicationCommands.Open,
										Description = "The recent documents in this popup menu are dynamically generated.",
										Label = "Open",
										LargeImageSource = ImageLoader.GetIcon("Open32.png"),
										MaxSimplifiedVariantSize = VariantSize.Medium,
										SmallImageSource = ImageLoader.GetIcon("Open16.png"),
										MenuItems = { CreateChildPlaceholder() }
									},
									new BarPopupButtonViewModel(BarControlKeys.DynamicPopupButton) {
										Description = "The menu of this popup button is dynamically generated when the popup is opened.",
										Label = "Popup Button with Dynamic Menu",
										LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
										MaxSimplifiedVariantSize = VariantSize.Medium,
										SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
										MenuItems = { CreateChildPlaceholder() }
									},
								},
							},

							new RibbonGroupViewModel("SimplifiedLayoutOverflow") {
								CanAutoCollapse = false,
								Items = {
									new BarPopupButtonViewModel(BarControlKeys.DynamicPopupOverflowButton) {
										Label = "Popup Button Overflowed in Simplified Layout",
										LargeImageSource = ImageLoader.GetIcon("QuickStartGreen32.png"),
										MaxSimplifiedVariantSize = VariantSize.Collapsed,
										SmallImageSource = ImageLoader.GetIcon("QuickStartGreen16.png"),
										MenuItems = { CreateChildPlaceholder() }
									},
								}
							}

						}
					},

				}
			};
		}

		/// <summary>
		/// Initializes the view models for the stand-alone toolbar.
		/// </summary>
		private void InitializeStandaloneToolBarViewModels() {
			StandaloneToolBar = new StandaloneToolBarViewModel() {
				Items = {
					new BarPopupButtonViewModel(BarControlKeys.DynamicStandaloneToolBarButton) {
						Description = "The menu of this popup button is dynamically generated when the popup is opened.",
						Label = "Popup Button with Dynamic Menu",
						LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
						SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
					}
				}
			};
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
				targetList.Add(new BarButtonViewModel() {
					CanCloneToRibbonQuickAccessToolBar = false,
					Command = ApplicationCommands.Open,
					CommandParameter = docReference,
					IsInputGestureTextVisible = false,
					Label = docReference.Name,
					LargeImageSource = docReference.ImageSourceLarge,
					SmallImageSource = docReference.ImageSourceSmall,
					Title = docReference.Location?.LocalPath,
				});
			}
			bool includeHeaders = (pinnedMenuItems.Any() && unpinnedMenuItems.Any());
			if (pinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarHeadingViewModel("Pinned Documents"));
				foreach (var menuItem in pinnedMenuItems)
					menuItems.Add(menuItem);
			}
			if (unpinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarHeadingViewModel("Unpinned Documents"));
				foreach (var menuItem in unpinnedMenuItems)
					menuItems.Add(menuItem);
			}

			if (menuItems.Count == 0)
				menuItems.Add(new BarMenuItem("No recent documents") { IsEnabled = false });

			menuItems.Add(new BarSeparatorViewModel());
			menuItems.Add(new BarButtonViewModel(ApplicationCommands.Open) {
				CanCloneToRibbonQuickAccessToolBar = false,
				Label = "Browse...",
				LargeImageSource = ImageLoader.GetIcon("Open32.png"),
				SmallImageSource = ImageLoader.GetIcon("Open16.png"),
			});
		}

		/// <inheritdoc/>
		protected override void PopulateSampleDynamicMenuItems(string key, IList menuItems) {
			// Dynamically generate new menu items
			menuItems.Add(new BarHeadingViewModel(key));
			menuItems.Add(new BarSeparatorViewModel());
			var itemCount = random.Next(2, 9); // Randomize the number of menu items
			for (int index = 0; index < itemCount; index++) {
				menuItems.Add(new BarButtonViewModel(
					key: $"DynamicItem{(index + 1)}",
					label: $"Dynamic Item #{(index + 1)}") {
						CanCloneToRibbonQuickAccessToolBar = false
					});
			}
			if (key != BarControlKeys.DynamicMenuItem) {
				// Add a menu item whose sub-menu is dynamically generated
				menuItems.Add(new BarSeparatorViewModel());
				menuItems.Add(new BarPopupButtonViewModel(BarControlKeys.DynamicMenuItem, "Dynamic Sub-Menu") {
					CanCloneToRibbonQuickAccessToolBar = false,
					MenuItems = { CreateChildPlaceholder() }
				});
			}
			menuItems.Add(new BarSeparatorViewModel());
			menuItems.Add(new BarHeadingViewModel($"Menu Created @ {DateTime.Now.ToLongTimeString()}"));
		}

		/// <summary>
		/// Gets or sets the view model for the Ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; private set; }

		/// <summary>
		/// Gets or sets the view model for the StandaloneToolBar control.
		/// </summary>
		/// <value>A <see cref="StandaloneToolBarViewModel"/>.</value>
		public StandaloneToolBarViewModel StandaloneToolBar { get; private set; }

	}

}
