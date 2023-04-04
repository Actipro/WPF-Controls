using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using System.ComponentModel;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryInRibbon {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private bool				canCategorizeOnMenu			= false;
		private bool				canFilterOnMenu				= false;
		private int					itemSpacing					= 4;
		private DataTemplate		itemTemplate;
		private bool				isSetColorCommandEnabled	= true;
		private int					minLargeRibbonColumnCount	= 6;
		private int					maxMenuColumnCount			= int.MaxValue;
		private int					maxRibbonColumnCount		= int.MaxValue;
		private int					minMediumRibbonColumnCount	= 3;
		private ControlResizeMode	menuResizeMode				= ControlResizeMode.Both;
		private int					minMenuColumnCount			= 1;
		private bool				useAccentedItemBorder		= true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if the gallery is categorized when displayed as a menu.
		/// </summary>
		/// <value><c>true</c> to categorize; otherwise <c>false</c>.</value>
		public bool CanCategorizeOnMenu {
			get => canCategorizeOnMenu;
			set {
				if (canCategorizeOnMenu != value) {
					canCategorizeOnMenu = value;
					NotifyPropertyChanged(nameof(CanCategorizeOnMenu));

					if (!canCategorizeOnMenu) {
						// Disable filtering if categories are not active
						CanFilterOnMenu = false;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets if the gallery can be filtered when displayed as a menu.
		/// </summary>
		/// <value><c>true</c> to allow filtering; otherwise <c>false</c>.</value>
		public bool CanFilterOnMenu {
			get => canFilterOnMenu;
			set {
				if (canFilterOnMenu != value) {
					canFilterOnMenu = value;
					NotifyPropertyChanged(nameof(CanFilterOnMenu));

					if (canFilterOnMenu) {
						// Ensure categorization is enabled or filtering has no effect
						CanCategorizeOnMenu = true;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets if the <see cref="SetColorCommand"/> can be executed.
		/// </summary>
		/// <value><c>true</c> if the command can execute; otherwise <c>false</c>.</value>
		[DisplayName("Is gallery command enabled")]
		public bool IsSetColorCommandEnabled {
			get => isSetColorCommandEnabled;
			set {
				if (isSetColorCommandEnabled != value) {
					isSetColorCommandEnabled = value;
					NotifyPropertyChanged(nameof(IsSetColorCommandEnabled));
				}
			}
		}

		/// <summary>
		/// Gets or sets the amount of spacing between gallery items.
		/// </summary>
		/// <value>An integer value.</value>
		public int ItemSpacing {
			get {
				return itemSpacing;
			}
			set {
				if (itemSpacing != value) {
					itemSpacing = value;
					NotifyPropertyChanged(nameof(ItemSpacing));
				}
			}
		}

		/// <summary>
		/// Gets or sets the template used to display items in the gallery.
		/// </summary>
		/// <value>A <see cref="DataTemplate"/>.</value>
		public DataTemplate ItemTemplate {
			get => itemTemplate;
			set {
				if (itemTemplate != value) {
					itemTemplate = value;
					NotifyPropertyChanged(nameof(ItemTemplate));
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum number of columns used for gallery items when displayed in a menu.
		/// </summary>
		/// <value>An integer value.</value>
		[DisplayName("Max col count (menu)")]
		public int MaxMenuColumnCount {
			get => maxMenuColumnCount;
			set {
				if (maxMenuColumnCount != value) {
					maxMenuColumnCount = value;
					NotifyPropertyChanged(nameof(MaxMenuColumnCount));
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum number of columns used for gallery items when displayed in the ribbon.
		/// </summary>
		/// <value>An integer value.</value>
		[DisplayName("Max col count (ribbon)")]
		public int MaxRibbonColumnCount {
			get => maxRibbonColumnCount;
			set {
				if (maxRibbonColumnCount != value) {
					maxRibbonColumnCount = value;
					NotifyPropertyChanged(nameof(MaxRibbonColumnCount));
				}
			}
		}

		/// <summary>
		/// Gets or sets if a menu can be resized.
		/// </summary>
		/// <value>One of the <see cref="ControlResizeMode"/> values.</value>
		public ControlResizeMode MenuResizeMode {
			get => menuResizeMode;
			set {
				if (menuResizeMode != value) {
					menuResizeMode = value;
					NotifyPropertyChanged(nameof(MenuResizeMode));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the minimum number of columns used for gallery items when displayed in the ribbon with a large variant size.
		/// </summary>
		/// <value>An integer value.</value>
		[DisplayName("Min large col count (ribbon)")]
		public int MinLargeRibbonColumnCount {
			get => minLargeRibbonColumnCount;
			set {
				if (minLargeRibbonColumnCount != value) {
					minLargeRibbonColumnCount = value;
					NotifyPropertyChanged(nameof(MinLargeRibbonColumnCount));
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum number of columns used for gallery items when displayed in the ribbon with a medium variant size.
		/// </summary>
		/// <value>An integer value.</value>
		[DisplayName("Min med col count (ribbon)")]
		public int MinMediumRibbonColumnCount {
			get => minMediumRibbonColumnCount;
			set {
				if (minMediumRibbonColumnCount != value) {
					minMediumRibbonColumnCount = value;
					NotifyPropertyChanged(nameof(MinMediumRibbonColumnCount));
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum number of columns used for gallery items when displayed in a menu.
		/// </summary>
		/// <value>An integer value.</value>
		[DisplayName("Min col count (menu)")]
		public int MinMenuColumnCount {
			get => minMenuColumnCount;
			set {
				if (minMenuColumnCount != value) {
					minMenuColumnCount = value;
					NotifyPropertyChanged(nameof(MinMenuColumnCount));
				}
			}
		}

		/// <summary>
		/// Gets or sets if an accented border is displayed around gallery items.
		/// </summary>
		/// <value><c>true</c> to use an accented border; otherwise <c>false</c>.</value>
		public bool UseAccentedItemBorder {
			get {
				return useAccentedItemBorder;
			}
			set {
				if (useAccentedItemBorder != value) {
					useAccentedItemBorder = value;
					NotifyPropertyChanged(nameof(UseAccentedItemBorder));
				}
			}
		}

	}

}
