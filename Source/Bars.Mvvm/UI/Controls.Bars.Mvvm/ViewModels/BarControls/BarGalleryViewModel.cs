namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a standard gallery control within a bar control.
/// </summary>
[ContentProperty(nameof(Items))]
public class BarGalleryViewModel : BarGalleryViewModelBase, IHasVariantImages {

	private bool _canCategorize = true;
	private bool _canFilter;
	private bool _canRibbonAutoScrollToSelectedItem;
	private DataTemplate? _categoryHeaderTemplate;
	private string? _collapsedButtonDescription;
	private bool _hasCategoryHeaders = true;
	private InvocationFocusBehavior _invocationFocusBehavior = InvocationFocusBehavior.Default;
	private bool _isSelectionSupported = true;
	private bool? _isSynchronizedWithCurrentItem = null;
	private IEnumerable? _items;
	private string? _keyTipText;
	private ImageSource? _largeImageSource;
	private int _maxMenuColumnCount = int.MaxValue;
	private int _maxRibbonColumnCount = 15;
	private ImageSource? _mediumImageSource;
	private double _maxMenuGalleryHeight = 1000.0;
	private ControlResizeMode _menuResizeMode = ControlResizeMode.None;
	private int _minLargeRibbonColumnCount = 5;
	private int _minMediumRibbonColumnCount = 3;
	private int _minMenuColumnCount = 1;
	private ICommand? _popupOpeningCommand;
	private IBarGalleryItemViewModel? _selectedItem;
	private string? _selectedFilterCategory;
	private ItemCollapseBehavior _toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
	private ItemVariantBehavior _toolBarItemVariantBehavior = ItemVariantBehavior.AlwaysSmall;
	private bool _useAccentedItemBorder;
	private bool _useMenuItemAppearance;
	private bool _useMenuItemIndent;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarButtonViewModel()"/>
	public BarGalleryViewModel()  // Parameterless constructor required for XAML support
		: this(key: null) { }

	/// <inheritdoc cref="BarButtonViewModel(string)"/>
	public BarGalleryViewModel(string? key)
		: this(key, label: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key and items.  The label and key tip text are auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(string? key, IEnumerable? items)
		: this(key, label: null, items) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
	public BarGalleryViewModel(string? key, string? label)
		: this(key, label, keyTipText: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key, label, and items.  The key tip text is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(string? key, string? label, IEnumerable? items)
		: this(key, label, keyTipText: null, items) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
	public BarGalleryViewModel(string? key, string? label, string? keyTipText)
		: this(key, label, keyTipText, items: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key, label, key tip text, and items.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
	/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(string? key, string? label, string? keyTipText, IEnumerable? items)
		: this(key, label, keyTipText, command: null, items) { }

	/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
	public BarGalleryViewModel(RoutedCommand? routedCommand)
		: this(routedCommand?.Name, routedCommand) { }

	/// <summary>
	/// Initializes an instance of the class with the specified items and <see cref="RoutedCommand"/>, also used to auto-generate a key, label, and key tip text.
	/// </summary>
	/// <param name="routedCommand">The command to attach to the control.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(RoutedCommand? routedCommand, IEnumerable? items)
		: this(routedCommand?.Name, routedCommand, items) { }

	/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
	public BarGalleryViewModel(string? key, ICommand? command)
		: this(key, label: null, command) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
	public BarGalleryViewModel(string? key, string? label, ICommand? command)
		: this(key, label, keyTipText: null, command) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
	public BarGalleryViewModel(string? key, string? label, string? keyTipText, ICommand? command)
		: this(key, label, keyTipText, command, items: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key, command, and items.  The label and key tip text are auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="command">The command to attach to the control.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(string? key, ICommand? command, IEnumerable? items)
		: this(key, label: null, keyTipText: null, command, items) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key, label, command, and items.  The key tip text is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
	/// <param name="command">The command to attach to the control.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(string? key, string? label, ICommand? command, IEnumerable? items)
		: this(key, label, keyTipText: null, command, items) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key, label, key tip text, command, and items.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
	/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="command"/> or <paramref name="label"/> if <c>null</c>.</param>
	/// <param name="command">The command to attach to the control.</param>
	/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
	public BarGalleryViewModel(string? key, string? label, string? keyTipText, ICommand? command, IEnumerable? items)
		: base(key, label, command) {

		_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(Label);
		_items = items;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of menu items that appear above the menu gallery in a popup.
	/// </summary>
	public ObservableCollection<object> AboveMenuItems { get; } = [];

	/// <summary>
	/// The collection of menu items that below above the menu gallery in a popup.
	/// </summary>
	public ObservableCollection<object> BelowMenuItems { get; } = [];

	/// <summary>
	/// Indicates whether the gallery sorts and displays items by category when an <see cref="ICollectionView"/> is set to the <c>ItemsSource</c>.
	/// </summary>
	/// <value>
	/// <c>true</c> if the gallery sorts and displays items by category when an <see cref="ICollectionView"/> is set to the <c>ItemsSource</c>; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool CanCategorize {
		get => _canCategorize;
		set => SetProperty(ref _canCategorize, value);
	}

	/// <summary>
	/// Indicates whether the gallery supports filtering of items by category.
	/// </summary>
	/// <value>
	/// <c>true</c> if the gallery supports filtering of items by category; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool CanFilter {
		get => _canFilter;
		set => SetProperty(ref _canFilter, value);
	}

	/// <summary>
	/// Indicates whether to automatically scroll a ribbon gallery to the selected item on selection changes.
	/// </summary>
	/// <value>
	/// <c>true</c> if the ribbon gallery selected item should automatically be scrolled into view on selection changes; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool CanRibbonAutoScrollToSelectedItem {
		get => _canRibbonAutoScrollToSelectedItem;
		set => SetProperty(ref _canRibbonAutoScrollToSelectedItem, value);
	}

	/// <summary>
	/// A <see cref="DataTemplate"/> for category headers when categorization is enabled.
	/// </summary>
	public DataTemplate? CategoryHeaderTemplate {
		get => _categoryHeaderTemplate;
		set => SetProperty(ref _categoryHeaderTemplate, value);
	}

	/// <summary>
	/// Creates a <see cref="CollectionViewSource"/> for the specified collection of gallery item view models, allowing for possible categorization and filtering.
	/// </summary>
	/// <param name="items">The collection of gallery item view models to include in the collection view.</param>
	/// <param name="categorize">Whether the collection view source should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
	/// <returns>The <see cref="CollectionViewSource"/> of gallery item view models that was created.</returns>
	public static CollectionViewSource CreateCollectionViewSource(IEnumerable<IBarGalleryItemViewModel> items, bool categorize) {
		var viewSource = new CollectionViewSource() {
			Source = items
		};

		if (categorize)
			viewSource.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IBarGalleryItemViewModel.Category)));

		return viewSource;
	}

	/// <summary>
	/// The text description to display in screen tips for the gallery when it is rendered as a collapsed button.
	/// </summary>
	public string? CollapsedButtonDescription {
		get => _collapsedButtonDescription;
		set => SetProperty(ref _collapsedButtonDescription, value);
	}

	/// <summary>
	/// Indicates whether the gallery has category headers when categorizing.
	/// </summary>
	/// <value>
	/// <c>true</c> if the gallery has category headers when categorizing; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool HasCategoryHeaders {
		get => _hasCategoryHeaders;
		set => SetProperty(ref _hasCategoryHeaders, value);
	}

	/// <summary>
	/// Indicates how the control processes focus when a commit occurs.
	/// </summary>
	/// <value>
	/// The default value is <see cref="InvocationFocusBehavior.Default"/>.
	/// </value>
	public InvocationFocusBehavior InvocationFocusBehavior {
		get => _invocationFocusBehavior;
		set => SetProperty(ref _invocationFocusBehavior, value);
	}

	/// <summary>
	/// Indicates whether selection is supported by the gallery.
	/// </summary>
	/// <value>
	/// <c>true</c> if selection is supported by the gallery; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool IsSelectionSupported {
		get => _isSelectionSupported;
		set => SetProperty(ref _isSelectionSupported, value);
	}

	/// <summary>
	/// Indicates whether the gallery control should keep its <see cref="SelectedItem"/> property synchronized with the current item in its <see cref="Items"/> property.
	/// </summary>
	/// <value>
	/// <c>true</c> if the <see cref="SelectedItem"/> is always synchronized with the current item in the <see cref="Items"/>;
	/// <c>false</c> if the <see cref="SelectedItem"/> is never synchronized with the current item;
	/// <c>null</c> if the <see cref="SelectedItem"/> is synchronized with the current item only if the gallery uses a <c>CollectionView</c>.
	/// The default value is <c>null</c>.
	/// </value>
	public bool? IsSynchronizedWithCurrentItem {
		get => _isSynchronizedWithCurrentItem;
		set => SetProperty(ref _isSynchronizedWithCurrentItem, value);
	}

	/// <summary>
	/// The collection of gallery items.
	/// </summary>
	public IEnumerable? Items {
		get => _items;
		set => SetProperty(ref _items, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
	public string? KeyTipText {
		get => _keyTipText;
		set => SetProperty(ref _keyTipText, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.LargeImageSource"/>
	public ImageSource? LargeImageSource {
		get => _largeImageSource;
		set => SetProperty(ref _largeImageSource, value);
	}

	/// <summary>
	/// The maximum number of columns when in a menu.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Int32.MaxValue"/>.
	/// </value>
	public int MaxMenuColumnCount {
		get => _maxMenuColumnCount;
		set => SetProperty(ref _maxMenuColumnCount, value);
	}

	/// <summary>
	/// The maximum height of the menu gallery in the popup.
	/// </summary>
	/// <value>
	/// The default value is <c>1000.0</c>.
	/// </value>
	public double MaxMenuGalleryHeight {
		get => _maxMenuGalleryHeight;
		set => SetProperty(ref _maxMenuGalleryHeight, value);
	}

	/// <summary>
	/// The maximum number of columns when in-ribbon.
	/// </summary>
	/// <value>
	/// The default value is <c>15</c>.
	/// </value>
	public int MaxRibbonColumnCount {
		get => _maxRibbonColumnCount;
		set => SetProperty(ref _maxRibbonColumnCount, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.MediumImageSource"/>
	public ImageSource? MediumImageSource {
		get => _mediumImageSource;
		set => SetProperty(ref _mediumImageSource, value);
	}

	/// <summary>
	/// A <see cref="ControlResizeMode"/> that indicates how the popup menu can resize.
	/// </summary>
	/// <value>
	/// The default value is <see cref="ControlResizeMode.None"/>.
	/// </value>
	public ControlResizeMode MenuResizeMode {
		get => _menuResizeMode;
		set => SetProperty(ref _menuResizeMode, value);
	}

	/// <summary>
	/// The minimum number of columns to use when in-ribbon with a <see cref="VariantSize.Large"/> variant size.
	/// </summary>
	/// <value>
	/// The default value is <c>5</c>.
	/// </value>
	public int MinLargeRibbonColumnCount {
		get => _minLargeRibbonColumnCount;
		set => SetProperty(ref _minLargeRibbonColumnCount, value);
	}

	/// <summary>
	/// The minimum number of columns to use when in-ribbon with a <see cref="VariantSize.Medium"/> variant size.
	/// </summary>
	/// <value>
	/// The default value is <c>3</c>.
	/// </value>
	public int MinMediumRibbonColumnCount {
		get => _minMediumRibbonColumnCount;
		set => SetProperty(ref _minMediumRibbonColumnCount, value);
	}

	/// <summary>
	/// The minimum number of columns when in a menu.
	/// </summary>
	/// <value>
	/// The default value is <c>1</c>.
	/// </value>
	public int MinMenuColumnCount {
		get => _minMenuColumnCount;
		set => SetProperty(ref _minMenuColumnCount, value);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that executes before the gallery's popup is opened, allowing its items to be customized in MVVM scenarios.
	/// </summary>
	public ICommand? PopupOpeningCommand {
		get => _popupOpeningCommand;
		set => SetProperty(ref _popupOpeningCommand, value);
	}

	/// <summary>
	/// The selected filter category.
	/// </summary>
	public string? SelectedFilterCategory {
		get => _selectedFilterCategory;
		set => SetProperty(ref _selectedFilterCategory, value);
	}

	/// <summary>
	/// The selected item.
	/// </summary>
	public IBarGalleryItemViewModel? SelectedItem {
		get => _selectedItem;
		set => SetProperty(ref _selectedItem, value);
	}

	/// <summary>
	/// Selects an item in the gallery that matches the predicate.
	/// </summary>
	/// <typeparam name="T">The type of <see cref="IBarGalleryItemViewModel"/> to examine.</typeparam>
	/// <param name="predicate">A predicate that determines when an item matches criteria.</param>
	/// <returns>
	/// The <see cref="BarGalleryViewModelBase"/> that was selected.
	/// </returns>
	public virtual T? SelectItemByValueMatch<T>(Func<T, bool> predicate) where T : IBarGalleryItemViewModel {
		if (predicate is null)
			throw new ArgumentNullException(nameof(predicate));

		var newSelectedItem = Items is not null
			? Items.OfType<T>().FirstOrDefault(predicate)
			: default;

		SelectedItem = newSelectedItem;

		return newSelectedItem;
	}

	/// <inheritdoc cref="BarButtonViewModel.ToolBarItemCollapseBehavior"/>
	public ItemCollapseBehavior ToolBarItemCollapseBehavior {
		get => _toolBarItemCollapseBehavior;
		set => SetProperty(ref _toolBarItemCollapseBehavior, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.ToolBarItemVariantBehavior"/>
	public ItemVariantBehavior ToolBarItemVariantBehavior {
		get => _toolBarItemVariantBehavior;
		set => SetProperty(ref _toolBarItemVariantBehavior, value);
	}

	/// <summary>
	/// Indicates whether to use an accented item border for gallery items, common when they have vibrant content such as color swatches.
	/// </summary>
	/// <value>
	/// <c>true</c> if an accented item border should be used for gallery items; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool UseAccentedItemBorder {
		get => _useAccentedItemBorder;
		set => SetProperty(ref _useAccentedItemBorder, value);
	}

	/// <summary>
	/// Indicates whether to use a menu item appearance for gallery items, common for single-column menu galleries.
	/// </summary>
	/// <value>
	/// <c>true</c> if gallery items should use a menu item appearance; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool UseMenuItemAppearance {
		get => _useMenuItemAppearance;
		set => SetProperty(ref _useMenuItemAppearance, value);
	}

	/// <summary>
	/// Indicates whether to align gallery items in a menu so that they indent past the menu's icon column.
	/// </summary>
	/// <value>
	/// <c>true</c> if gallery items should be indented past the menu's icon column; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool UseMenuItemIndent {
		get => _useMenuItemIndent;
		set => SetProperty(ref _useMenuItemIndent, value);
	}

}
