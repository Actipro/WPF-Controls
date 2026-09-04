namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for image and text content within a ribbon footer.
/// </summary>
public class RibbonFooterInfoBarContentViewModel : ObservableObjectBase, IHasTag {

	private object? _action;
	private DataTemplate? _actionTemplate;
	private DataTemplateSelector? _actionTemplateSelector;
	private bool _canClose = true;
	private object? _content;
	private DataTemplate? _contentTemplate;
	private DataTemplateSelector? _contentTemplateSelector;
	private ImageSource? _iconSource;
	private bool _isIconVisible = true;
	private string? _message;
	private Thickness _padding = new(10, 5, 10, 5);
	private InfoBarSeverity _severity = InfoBarSeverity.Information;
	private object? _tag;
	private string? _title;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The optional action to be displayed in the info bar.
	/// </summary>
	public object? Action {
		get => _action;
		set => SetProperty(ref _action, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> used to display the <see cref="Action"/>.
	/// </summary>
	public DataTemplate? ActionTemplate {
		get => _actionTemplate;
		set => SetProperty(ref _actionTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the <see cref="Action"/>.
	/// </summary>
	public DataTemplateSelector? ActionTemplateSelector {
		get => _actionTemplateSelector;
		set => SetProperty(ref _actionTemplateSelector, value);
	}

	/// <summary>
	/// Indicates whether the info bar can be closed by the user.
	/// </summary>
	/// <value>
	/// <c>true</c> if the info bar can be closed; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool CanClose {
		get => _canClose;
		set => SetProperty(ref _canClose, value);
	}

	/// <summary>
	/// The <see cref="ImageSource"/> that defines the icon.
	/// </summary>
	public ImageSource? IconSource {
		get => _iconSource;
		set => SetProperty(ref _iconSource, value);
	}

	/// <summary>
	/// The optional content to be displayed in the info bar.
	/// </summary>
	public object? Content {
		get => _content;
		set => SetProperty(ref _content, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> used to display the <see cref="Content"/>.
	/// </summary>
	public DataTemplate? ContentTemplate {
		get => _contentTemplate;
		set => SetProperty(ref _contentTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the <see cref="Content"/>.
	/// </summary>
	public DataTemplateSelector? ContentTemplateSelector {
		get => _contentTemplateSelector;
		set => SetProperty(ref _contentTemplateSelector, value);
	}

	/// <summary>
	/// Indicates whether the icon is visible.
	/// </summary>
	/// <value>
	/// <c>true</c> if the icon should be visible; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool IsIconVisible {
		get => _isIconVisible;
		set => SetProperty(ref _isIconVisible, value);
	}

	/// <summary>
	/// The message content.
	/// </summary>
	public string? Message {
		get => _message;
		set => SetProperty(ref _message, value);
	}

	/// <summary>
	/// The padding inside the control.
	/// </summary>
	/// <value>
	/// The default value is <c>10,5</c>.
	/// </value>
	public Thickness Padding {
		get => _padding;
		set => SetProperty(ref _padding, value);
	}

	/// <summary>
	/// The severity of the info bar.
	/// </summary>
	/// <value>
	/// The default value is <see cref="InfoBarSeverity.Information"/>.
	/// </value>
	public InfoBarSeverity Severity {
		get => _severity;
		set => SetProperty(ref _severity, value);
	}

	/// <inheritdoc cref="IHasTag.Tag"/>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <summary>
	/// The title content.
	/// </summary>
	public string? Title {
		get => _title;
		set => SetProperty(ref _title, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[Title='{Title}']";

}
