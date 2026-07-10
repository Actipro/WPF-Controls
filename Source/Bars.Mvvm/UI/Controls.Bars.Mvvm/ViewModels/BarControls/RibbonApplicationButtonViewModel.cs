namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for an application button control within a ribbon.
/// </summary>
[ContentProperty(nameof(MenuItems))]
public class RibbonApplicationButtonViewModel : ObservableObjectBase, IHasTag {

	private string? _keyTipText;
	private string? _label;
	private object? _menuAdditionalContent;
	private DataTemplate? _menuAdditionalContentTemplate;
	private DataTemplateSelector? _menuAdditionalContentTemplateSelector;
	private object? _menuFooter;
	private DataTemplate? _menuFooterTemplate;
	private DataTemplateSelector? _menuFooterTemplateSelector;
	private object? _tag;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RibbonApplicationButtonViewModel()  // Parameterless constructor required for XAML support
		: this(label: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified label.
	/// </summary>
	/// <param name="label">The text label to display.</param>
	public RibbonApplicationButtonViewModel(string? label)
		: this(label, keyTipText: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified label and key tip text.
	/// </summary>
	/// <param name="label">The text label to display.</param>
	/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
	public RibbonApplicationButtonViewModel(string? label, string? keyTipText) {
		_label = label ?? SR.GetString(SRName.UIApplicationButtonText);
		_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromLabel(_label);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

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

	/// <summary>
	/// The additional content that optionally appears on the right side of the menu.
	/// </summary>
	public object? MenuAdditionalContent {
		get => _menuAdditionalContent;
		set => SetProperty(ref _menuAdditionalContent, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> used to display the menu additional content.
	/// </summary>
	public DataTemplate? MenuAdditionalContentTemplate {
		get => _menuAdditionalContentTemplate;
		set => SetProperty(ref _menuAdditionalContentTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the menu additional content.
	/// </summary>
	public DataTemplateSelector? MenuAdditionalContentTemplateSelector {
		get => _menuAdditionalContentTemplateSelector;
		set => SetProperty(ref _menuAdditionalContentTemplateSelector, value);
	}

	/// <summary>
	/// The footer content that optionally appears at the bottom of the menu.
	/// </summary>
	public object? MenuFooter {
		get => _menuFooter;
		set => SetProperty(ref _menuFooter, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> used to display the menu footer content.
	/// </summary>
	public DataTemplate? MenuFooterTemplate {
		get => _menuFooterTemplate;
		set => SetProperty(ref _menuFooterTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the menu footer content.
	/// </summary>
	public DataTemplateSelector? MenuFooterTemplateSelector {
		get => _menuFooterTemplateSelector;
		set => SetProperty(ref _menuFooterTemplateSelector, value);
	}

	/// <inheritdoc cref="IHasTag.Tag"/>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <summary>
	/// The collection of items that appear within the menu.
	/// </summary>
	public ObservableCollection<object> MenuItems { get; } = [];

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[Label='{Label}']";

}
