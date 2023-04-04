using ActiproSoftware.Windows.Themes;
using System.Collections.ObjectModel;
using System.Windows;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a ribbon control.
	/// </summary>
	public class RibbonViewModel : ObservableObjectBase {

		private RibbonApplicationButtonViewModel applicationButton;
		private RibbonBackstageViewModel backstage;
		private bool canChangeLayoutMode = true;
		private Size collapseThresholdSize = new Size(270, 170);
		private RibbonFooterViewModel footer;
		private bool isApplicationButtonVisible = true;
		private bool isCollapsible = true;
		private bool isMinimizable = true;
		private BarControlTemplateSelector itemContainerTemplateSelector = new BarControlTemplateSelector();
		private RibbonLayoutMode layoutMode = RibbonLayoutMode.Classic;
		private RibbonQuickAccessToolBarViewModel quickAccessToolBar;
		private RibbonQuickAccessToolBarLocation quickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Above;
		private RibbonQuickAccessToolBarMode quickAccessToolBarMode = RibbonQuickAccessToolBarMode.Visible;
		private RibbonTabRowToolBarViewModel tabRowToolBar;
		private UserInterfaceDensity userInterfaceDensity = UserInterfaceDensity.Compact;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class in Classic layout mode.
		/// </summary>
		public RibbonViewModel()
			: this(RibbonLayoutMode.Classic) { }

		/// <summary>
		/// Initializes a new instance of the class in the specified layout mode.
		/// </summary>
		public RibbonViewModel(RibbonLayoutMode layoutMode) {
			// Initialize the layout mode
			this.layoutMode = layoutMode;

			// Initialize other properties with defaults based on the given layout mode
			this.userInterfaceDensity = (layoutMode == RibbonLayoutMode.Classic)
				? UserInterfaceDensity.Compact
				: UserInterfaceDensity.Spacious;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a <see cref="RibbonApplicationButtonViewModel"/> for the application button.
		/// </summary>
		/// <value>A <see cref="RibbonApplicationButtonViewModel"/> for the application button.</value>
		public RibbonApplicationButtonViewModel ApplicationButton {
			get => applicationButton;
			set {
				if (applicationButton != value) {
					applicationButton = value;
					this.NotifyPropertyChanged(nameof(ApplicationButton));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonApplicationButtonViewModel"/> for the optional Backstage.
		/// </summary>
		/// <value>A <see cref="RibbonApplicationButtonViewModel"/> for the optional Backstage.</value>
		public RibbonBackstageViewModel Backstage {
			get => backstage;
			set {
				if (backstage != value) {
					backstage = value;
					this.NotifyPropertyChanged(nameof(Backstage));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the end user can change the layout mode.
		/// </summary>
		/// <value>
		/// <c>true</c> if the end user can change the layout mode; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanChangeLayoutMode {
			get => canChangeLayoutMode;
			set {
				if (canChangeLayoutMode != value) {
					canChangeLayoutMode = value;
					this.NotifyPropertyChanged(nameof(CanChangeLayoutMode));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the threshold <see cref="Size"/> that triggers a ribbon collapse if the ribbon is sized smaller than the threshold.
		/// </summary>
		/// <value>
		/// The threshold <see cref="Size"/> that triggers a ribbon collapse if the ribbon is sized smaller than the threshold.
		/// The default value is <c>270, 170</c>.
		/// </value>
		public Size CollapseThresholdSize {
			get => collapseThresholdSize;
			set {
				if (collapseThresholdSize != value) {
					collapseThresholdSize = value;
					this.NotifyPropertyChanged(nameof(CollapseThresholdSize));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of optional contextual tab groups within the ribbon.
		/// </summary>
		/// <value>The collection of optional contextual tab groups within the ribbon.</value>
		public ObservableCollection<RibbonContextualTabGroupViewModel> ContextualTabGroups { get; } = new ObservableCollection<RibbonContextualTabGroupViewModel>();
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonFooterViewModel"/> for the optional footer.
		/// </summary>
		/// <value>A <see cref="RibbonFooterViewModel"/> for the optional footer.</value>
		public RibbonFooterViewModel Footer {
			get => footer;
			set {
				if (footer != value) {
					footer = value;
					this.NotifyPropertyChanged(nameof(Footer));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the application button is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the application button is visible; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsApplicationButtonVisible  {
			get => isApplicationButtonVisible;
			set {
				if (isApplicationButtonVisible != value) {
					isApplicationButtonVisible = value;
					this.NotifyPropertyChanged(nameof(IsApplicationButtonVisible ));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the ribbon collapses when it becomes smaller than a minimum threshold width/height 
		/// as specified by the <see cref="CollapseThresholdSize"/> property.
		/// </summary>
		/// <value>
		/// <c>true</c> if the ribbon auto-collapsed when it becomes smaller than the threshold; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsCollapsible {
			get => isCollapsible;
			set {
				if (isCollapsible != value) {
					isCollapsible = value;
					this.NotifyPropertyChanged(nameof(IsCollapsible ));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the ribbon is minimizable.
		/// </summary>
		/// <value>
		/// <c>true</c> if the ribbon is minimizable; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsMinimizable {
			get => isMinimizable;
			set {
				if (isMinimizable != value) {
					isMinimizable = value;
					this.NotifyPropertyChanged(nameof(IsMinimizable));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
		/// </summary>
		/// <value>The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.</value>
		public BarControlTemplateSelector ItemContainerTemplateSelector {
			get => itemContainerTemplateSelector;
			set {
				if (itemContainerTemplateSelector != value) {
					itemContainerTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(ItemContainerTemplateSelector));
				}
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="RibbonLayoutMode"/> that indicates the current layout mode (Classic vs. Simplified).
		/// </summary>
		/// <value>
		/// A <see cref="RibbonLayoutMode"/> that indicates the current layout mode.
		/// The default value is <see cref="RibbonLayoutMode.Classic"/>.
		/// </value>
		public RibbonLayoutMode LayoutMode {
			get => layoutMode;
			set {
				if (layoutMode != value) {
					layoutMode = value;
					this.NotifyPropertyChanged(nameof(LayoutMode));
				}
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="RibbonQuickAccessToolBarViewModel"/> for the quick-access toolbar.
		/// </summary>
		/// <value>A <see cref="RibbonQuickAccessToolBarViewModel"/> for the quick-access toolbar.</value>
		public RibbonQuickAccessToolBarViewModel QuickAccessToolBar {
			get => quickAccessToolBar;
			set {
				if (quickAccessToolBar != value) {
					quickAccessToolBar = value;
					this.NotifyPropertyChanged(nameof(QuickAccessToolBar));
				}
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="RibbonQuickAccessToolBarLocation"/> that indicates the current location of the quick-access toolbar.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonQuickAccessToolBarLocation"/> that indicates the current location of the quick-access toolbar.
		/// The default value is <see cref="RibbonQuickAccessToolBarLocation.Above"/>.
		/// </value>
		public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
			get => quickAccessToolBarLocation;
			set {
				if (quickAccessToolBarLocation != value) {
					quickAccessToolBarLocation = value;
					this.NotifyPropertyChanged(nameof(QuickAccessToolBarLocation));
				}
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="RibbonQuickAccessToolBarMode"/> that indicates the current display mode for the quick-access toolbar.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonQuickAccessToolBarMode"/> that indicates the current display mode for the quick-access toolbar.
		/// The default value is <see cref="RibbonQuickAccessToolBarMode.Visible"/>.
		/// </value>
		public RibbonQuickAccessToolBarMode QuickAccessToolBarMode {
			get => quickAccessToolBarMode;
			set {
				if (quickAccessToolBarMode != value) {
					quickAccessToolBarMode = value;
					this.NotifyPropertyChanged(nameof(QuickAccessToolBarMode));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonTabRowToolBarViewModel"/> for the tab row toolbar that optionally appears to the right of the tabs.
		/// </summary>
		/// <value>A <see cref="RibbonTabRowToolBarViewModel"/> for the tab row toolbar that optionally appears to the right of the tabs.</value>
		public RibbonTabRowToolBarViewModel TabRowToolBar {
			get => tabRowToolBar;
			set {
				if (tabRowToolBar != value) {
					tabRowToolBar = value;
					this.NotifyPropertyChanged(nameof(TabRowToolBar));
				}
			}
		}

		/// <summary>
		/// Gets the collection of tabs within the ribbon.
		/// </summary>
		/// <value>The collection of tabs within the ribbon.</value>
		public ObservableCollection<RibbonTabViewModel> Tabs { get; } = new ObservableCollection<RibbonTabViewModel>();

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.Tabs.Count} tabs]";
		}

		/// <summary>
		/// Gets or sets a <see cref="Themes.UserInterfaceDensity"/> that indicates how compact or spacious the UI should appear.
		/// </summary>
		/// <value>
		/// A <see cref="Themes.UserInterfaceDensity"/> that indicates how compact or spacious the UI should appear.
		/// The default value is <see cref="UserInterfaceDensity.Compact"/>.
		/// </value>
		public UserInterfaceDensity UserInterfaceDensity {
			get => userInterfaceDensity;
			set {
				if (userInterfaceDensity != value) {
					userInterfaceDensity = value;
					this.NotifyPropertyChanged(nameof(UserInterfaceDensity));
				}
			}
		}

	}

}
