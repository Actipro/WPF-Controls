namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents an abstract view model base for a gallery control within a bar control.
/// </summary>
public abstract class BarGalleryViewModelBase : BarKeyedObjectViewModelBase {

	private bool _areSurroundingSeparatorsAllowed = true;
	private bool _canCloneToRibbonQuickAccessToolBar = true;
	private ICommand? _command;
	private bool _isVisible = true;
	private Style? _itemContainerStyle;
	private StyleSelector? _itemContainerStyleSelector;
	private double _itemSpacing;
	private DataTemplate? _itemTemplate;
	private DataTemplateSelector? _itemTemplateSelector;
	private string? _label;
	private double _minItemHeight = 16.0;
	private double _minItemWidth = 16.0;
	private ImageSource? _smallImageSource;
	private string? _title;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarButtonViewModel()"/>
	protected BarGalleryViewModelBase()  // Parameterless constructor required for XAML support
		: this(key: null) { }

	/// <inheritdoc cref="BarButtonViewModel(string)"/>
	protected BarGalleryViewModelBase(string? key)
		: base(key) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
	protected BarGalleryViewModelBase(string? key, string? label, ICommand? command)
		: base(key) {

		_label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
		_command = command;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the menu gallery can render surrounding separators.
	/// </summary>
	/// <value>
	/// <c>true</c> if the menu gallery can render surrounding separators; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool AreSurroundingSeparatorsAllowed {
		get => _areSurroundingSeparatorsAllowed;
		set => SetProperty(ref _areSurroundingSeparatorsAllowed, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.CanCloneToRibbonQuickAccessToolBar"/>
	public bool CanCloneToRibbonQuickAccessToolBar {
		get => _canCloneToRibbonQuickAccessToolBar;
		set => SetProperty(ref _canCloneToRibbonQuickAccessToolBar, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.Command"/>
	public ICommand? Command {
		get => _command;
		set => SetProperty(ref _command, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
	public bool IsVisible {
		get => _isVisible;
		set => SetProperty(ref _isVisible, value);
	}

	/// <summary>
	/// The <see cref="Style"/> to apply to gallery item container elements.
	/// </summary>
	public Style? ItemContainerStyle {
		get => _itemContainerStyle;
		set => SetProperty(ref _itemContainerStyle, value);
	}

	/// <summary>
	/// The <see cref="StyleSelector"/> that picks a <see cref="Style"/> to apply to gallery item container elements.
	/// </summary>
	public StyleSelector? ItemContainerStyleSelector {
		get => _itemContainerStyleSelector;
		set => SetProperty(ref _itemContainerStyleSelector, value);
	}

	/// <summary>
	/// The amount of spacing between gallery items.
	/// </summary>
	/// <value>
	/// The default value is <c>0.0</c>.
	/// </value>
	public double ItemSpacing {
		get => _itemSpacing;
		set => SetProperty(ref _itemSpacing, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> used to display the content for each gallery item.
	/// </summary>
	public DataTemplate? ItemTemplate {
		get => _itemTemplate;
		set => SetProperty(ref _itemTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the content for each gallery item.
	/// </summary>
	public DataTemplateSelector? ItemTemplateSelector {
		get => _itemTemplateSelector;
		set => SetProperty(ref _itemTemplateSelector, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.Label"/>
	public string? Label {
		get => _label;
		set => SetProperty(ref _label, value);
	}

	/// <summary>
	/// The minimum item height.
	/// </summary>
	/// <value>
	/// The default value is <c>16.0</c>.
	/// </value>
	public double MinItemHeight {
		get => _minItemHeight;
		set => SetProperty(ref _minItemHeight, value);
	}

	/// <summary>
	/// The minimum item width.
	/// </summary>
	/// <value>
	/// The default value is <c>16.0</c>.
	/// </value>
	public double MinItemWidth {
		get => _minItemWidth;
		set => SetProperty(ref _minItemWidth, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.SmallImageSource"/>
	public ImageSource? SmallImageSource {
		get => _smallImageSource;
		set => SetProperty(ref _smallImageSource, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.Title"/>
	public string? Title {
		get => _title;
		set => SetProperty(ref _title, value);
	}

}
