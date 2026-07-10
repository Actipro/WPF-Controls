namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a tab control within a ribbon backstage.
/// </summary>
public class RibbonBackstageTabViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

	private object? _content;
	private DataTemplate? _contentTemplate;
	private DataTemplateSelector? _contentTemplateSelector;
	private string? _description;
	private RibbonBackstageHeaderAlignment _headerAlignment = RibbonBackstageHeaderAlignment.Top;
	private bool _isEnabled = true;
	private bool _isVisible = true;
	private string? _keyTipText;
	private string? _label;
	private ImageSource? _largeImageSource;
	private ImageSource? _smallImageSource;
	private string? _title;
	private VariantSize _variantSize = VariantSize.Medium;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarButtonViewModel()"/>
	public RibbonBackstageTabViewModel()  // Parameterless constructor required for XAML support
		: this(key: null) { }

	/// <inheritdoc cref="BarButtonViewModel(string)"/>
	public RibbonBackstageTabViewModel(string? key)
		: this(key, label: null) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
	public RibbonBackstageTabViewModel(string? key, string? label)
		: this(key, label, keyTipText: null) { }

	/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
	public RibbonBackstageTabViewModel(string? key, string? label, string? keyTipText)
		: base(key) {

		// It is critical that the content of this view model is initialized to itself so
		//   that the DataTemplateSelector assigned to RibbonBackstage.ContentTemplateSelector
		//   can use the view model instance to select the appropriate template. If a
		//   DataTemplate is not defined, the view model's ToString() output will be displayed
		//   as an indicator that a template is needed.
		Content = this;

		_label = label ?? BarControlService.LabelGenerator.FromKey(key);
		_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromLabel(_label);
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	ImageSource? IHasVariantImages.MediumImageSource {
		get => null;
		set { /* No-op since a medium image is not supported by the control */ }
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The content for the tab's content area, which can be a UI control, a data object, or even this view model instance.
	/// </summary>
	public object? Content {
		get => _content;
		set => SetProperty(ref _content, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> for the tab's <see cref="Content"/>.
	/// </summary>
	public DataTemplate? ContentTemplate {
		get => _contentTemplate;
		set => SetProperty(ref _contentTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> for the tab's <see cref="Content"/>.
	/// </summary>
	public DataTemplateSelector? ContentTemplateSelector {
		get => _contentTemplateSelector;
		set => SetProperty(ref _contentTemplateSelector, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.Description"/>
	public string? Description {
		get => _description;
		set => SetProperty(ref _description, value);
	}

	/// <summary>
	/// A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
	/// </summary>
	/// <value>
	/// The default value is <see cref="RibbonBackstageHeaderAlignment.Top"/>.
	/// </value>
	public RibbonBackstageHeaderAlignment HeaderAlignment {
		get => _headerAlignment;
		set => SetProperty(ref _headerAlignment, value);
	}

	/// <summary>
	/// Indicates whether the control is currently enabled.
	/// </summary>
	/// <value>
	/// The default value is <c>true</c>.
	/// </value>
	public bool IsEnabled {
		get => _isEnabled;
		set => SetProperty(ref _isEnabled, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
	public bool IsVisible {
		get => _isVisible;
		set => SetProperty(ref _isVisible, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
	public string? KeyTipText {
		get => _keyTipText;
		set => SetProperty(ref _keyTipText, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.Label"/>
	public string? Label {
		get => _label;
		set => SetProperty(ref _label, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.LargeImageSource"/>
	public ImageSource? LargeImageSource {
		get => _largeImageSource;
		set => SetProperty(ref _largeImageSource, value);
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

	/// <summary>
	/// The variant size of the tab.
	/// </summary>
	/// <value>
	/// The default value is <see cref="VariantSize.Medium"/>.
	/// </value>
	public VariantSize VariantSize {
		get => _variantSize;
		set => SetProperty(ref _variantSize, value);
	}

}
