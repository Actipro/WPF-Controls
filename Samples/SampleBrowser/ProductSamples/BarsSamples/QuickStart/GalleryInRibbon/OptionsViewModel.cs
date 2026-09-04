using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryInRibbon;

/// <summary>
/// Defines configurable options for this sample.
/// </summary>
public class OptionsViewModel : ObservableObjectBase {

	private bool _canCategorizeOnMenu = false;
	private bool _canFilterOnMenu = false;
	private int _itemSpacing = 4;
	private DataTemplate? _itemTemplate;
	private bool _isSetColorCommandEnabled = true;
	private int _minLargeRibbonColumnCount = 6;
	private int _maxMenuColumnCount = int.MaxValue;
	private int _maxRibbonColumnCount = int.MaxValue;
	private int _minMediumRibbonColumnCount = 3;
	private ControlResizeMode _menuResizeMode = ControlResizeMode.Both;
	private int _minMenuColumnCount = 1;
	private string? _selectedColorCategory;
	private bool _useAccentedItemBorder = true;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates if the gallery is categorized when displayed as a menu.
	/// </summary>
	public bool CanCategorizeOnMenu {
		get => _canCategorizeOnMenu;
		set {
			if (SetProperty(ref _canCategorizeOnMenu, value)) {
				if (!_canCategorizeOnMenu) {
					// Disable filtering if categories are not active
					CanFilterOnMenu = false;
				}
			}
		}
	}

	/// <summary>
	/// Indicates if the gallery can be filtered when displayed as a menu.
	/// </summary>
	public bool CanFilterOnMenu {
		get => _canFilterOnMenu;
		set {
			if (SetProperty(ref _canFilterOnMenu, value)) {
				if (_canFilterOnMenu) {
					// Ensure categorization is enabled or filtering has no effect
					CanCategorizeOnMenu = true;
				}
			}
		}
	}

	/// <summary>
	/// Indicates if the <see cref="SetColorCommand"/> can be executed.
	/// </summary>
	[DisplayName("Is gallery command enabled")]
	public bool IsSetColorCommandEnabled {
		get => _isSetColorCommandEnabled;
		set => SetProperty(ref _isSetColorCommandEnabled, value);
	}

	/// <summary>
	/// The amount of spacing between gallery items.
	/// </summary>
	public int ItemSpacing {
		get => _itemSpacing;
		set => SetProperty(ref _itemSpacing, value);
	}

	/// <summary>
	/// The template used to display items in the gallery.
	/// </summary>
	public DataTemplate? ItemTemplate {
		get => _itemTemplate;
		set => SetProperty(ref _itemTemplate, value);
	}

	/// <summary>
	/// The maximum number of columns used for gallery items when displayed in a menu.
	/// </summary>
	[DisplayName("Max col count (menu)")]
	public int MaxMenuColumnCount {
		get => _maxMenuColumnCount;
		set => SetProperty(ref _maxMenuColumnCount, value);
	}

	/// <summary>
	/// The maximum number of columns used for gallery items when displayed in the ribbon.
	/// </summary>
	[DisplayName("Max col count (ribbon)")]
	public int MaxRibbonColumnCount {
		get => _maxRibbonColumnCount;
		set => SetProperty(ref _maxRibbonColumnCount, value);
	}

	/// <summary>
	/// Indicates if a menu can be resized.
	/// </summary>
	public ControlResizeMode MenuResizeMode {
		get => _menuResizeMode;
		set => SetProperty(ref _menuResizeMode, value);
	}

	/// <summary>
	/// The minimum number of columns used for gallery items when displayed in the ribbon with a large variant size.
	/// </summary>
	[DisplayName("Min large col count (ribbon)")]
	public int MinLargeRibbonColumnCount {
		get => _minLargeRibbonColumnCount;
		set => SetProperty(ref _minLargeRibbonColumnCount, value);
	}

	/// <summary>
	/// The minimum number of columns used for gallery items when displayed in the ribbon with a medium variant size.
	/// </summary>
	[DisplayName("Min med col count (ribbon)")]
	public int MinMediumRibbonColumnCount {
		get => _minMediumRibbonColumnCount;
		set => SetProperty(ref _minMediumRibbonColumnCount, value);
	}

	/// <summary>
	/// The minimum number of columns used for gallery items when displayed in a menu.
	/// </summary>
	[DisplayName("Min col count (menu)")]
	public int MinMenuColumnCount {
		get => _minMenuColumnCount;
		set => SetProperty(ref _minMenuColumnCount, value);
	}

	/// <summary>
	/// The selected color category.
	/// </summary>
	public string? SelectedColorCategory {
		get => _selectedColorCategory;
		set => SetProperty(ref _selectedColorCategory, value);
	}

	/// <summary>
	/// Indicates if an accented border is displayed around gallery items.
	/// </summary>
	public bool UseAccentedItemBorder {
		get => _useAccentedItemBorder;
		set => SetProperty(ref _useAccentedItemBorder, value);
	}

}
