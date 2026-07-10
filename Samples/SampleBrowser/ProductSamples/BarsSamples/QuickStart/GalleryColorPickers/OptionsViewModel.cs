namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers;

/// <summary>
/// Defines configurable options for this sample.
/// </summary>
public class OptionsViewModel : ObservableObjectBase {

	private bool _areSurroundingSeparatorsAllowed = true;
	private Color _fontColor = Colors.Red;
	private bool _fontColorCanCategorize = true;
	private bool _fontColorCanFilter = false;
	private int _itemSpacing = 4;
	private Color _livePreviewColor = Colors.Transparent;
	private string? _selectedFontColorCategory;
	private Color _textHighlightColor = Colors.Yellow;
	private int _textHighlightColCount = 5;
	private bool _useAccentedItemBorder = true;
	private bool _useCustomColors = false;
	private bool _useMenuItemIndent = false;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates if separators are displayed immediately before and after a gallery.
	/// </summary>
	[DisplayName("Use surrounding separators")]
	public bool AreSurroundingSeparatorsAllowed {
		get => _areSurroundingSeparatorsAllowed;
		set => SetProperty(ref _areSurroundingSeparatorsAllowed, value);
	}

	/// <summary>
	/// The current font color.
	/// </summary>
	public Color FontColor {
		get => _fontColor;
		set => SetProperty(ref _fontColor, value);
	}

	/// <summary>
	/// Indicates if font color controls can be categorized;
	/// </summary>
	public bool FontColorCanCategorize {
		get => _fontColorCanCategorize;
		set => SetProperty(ref _fontColorCanCategorize, value);
	}

	/// <summary>
	/// Indicates if font color controls can be filtered;
	/// </summary>
	/// <remarks>This property will typically only be set to <c>true</c> when categorization is also enabled.</remarks>
	public bool FontColorCanFilter {
		get => _fontColorCanFilter;
		set => SetProperty(ref _fontColorCanFilter, value);
	}

	/// <summary>
	/// The amount of spacing between non-grouped gallery items.
	/// </summary>
	public int ItemSpacing {
		get => _itemSpacing;
		set => SetProperty(ref _itemSpacing, value);
	}

	/// <summary>
	/// The current live preview color as a result of moving the mouse over gallery items.
	/// </summary>
	/// <value>
	/// A <see cref="Color"/> value; or <see cref="Colors.Transparent"/> if there is not a preview color.
	/// </value>
	public Color LivePreviewColor {
		get => _livePreviewColor;
		set => SetProperty(ref _livePreviewColor, value);
	}

	/// <summary>
	/// The selected font color category.
	/// </summary>
	public string? SelectedFontColorCategory {
		get => _selectedFontColorCategory;
		set => SetProperty(ref _selectedFontColorCategory, value);
	}

	/// <summary>
	/// The current text highlight color.
	/// </summary>
	public Color TextHighlightColor {
		get => _textHighlightColor;
		set => SetProperty(ref _textHighlightColor, value);
	}

	/// <summary>
	/// The number of columns to be displayed for a text highlight color picker.
	/// </summary>
	[DisplayName("Highlight color col count")]
	public int TextHighlightColCount {
		get => _textHighlightColCount;
		set => SetProperty(ref _textHighlightColCount, value);
	}

	/// <summary>
	/// Indicates if an accented border is displayed around gallery items.
	/// </summary>
	public bool UseAccentedItemBorder {
		get => _useAccentedItemBorder;
		set => SetProperty(ref _useAccentedItemBorder, value);
	}

	/// <summary>
	/// Indicates if a collection of custom colors should be used instead of the default colors.
	/// </summary>
	public bool UseCustomColors {
		get => _useCustomColors;
		set => SetProperty(ref _useCustomColors, value);
	}

	/// <summary>
	/// Indicates if gallery items are indented on the left and right like other menu items.
	/// </summary>
	/// <value>
	/// <c>true</c> use indenting consistent with menu items; otherwise <c>false</c> to use all available horizontal space for the gallery.
	/// </value>
	public bool UseMenuItemIndent {
		get => _useMenuItemIndent;
		set => SetProperty(ref _useMenuItemIndent, value);
	}

}
