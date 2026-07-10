namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a footer within a ribbon.
/// </summary>
public class RibbonFooterViewModel : ObservableObjectBase, IHasTag {

	private object? _content;
	private DataTemplate? _contentTemplate;
	private DataTemplateSelector? _contentTemplateSelector = new RibbonFooterContentTemplateSelector();
	private RibbonFooterKind _kind;
	private Thickness _padding = new(10, 5, 10, 5);
	private object? _tag;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The content to display within the ribbon footer.
	/// </summary>
	public object? Content {
		get => _content;
		set => SetProperty(ref _content, value);
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> used to display the content.
	/// </summary>
	public DataTemplate? ContentTemplate {
		get => _contentTemplate;
		set => SetProperty(ref _contentTemplate, value);
	}

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the content.
	/// </summary>
	public DataTemplateSelector? ContentTemplateSelector {
		get => _contentTemplateSelector;
		set => SetProperty(ref _contentTemplateSelector, value);
	}

	/// <summary>
	/// A <see cref="RibbonFooterKind"/> indicating the kind of footer, which determines its appearance.
	/// </summary>
	/// <value>
	/// The default value is <see cref="RibbonFooterKind.Default"/>.
	/// </value>
	public RibbonFooterKind Kind {
		get => _kind;
		set => SetProperty(ref _kind, value);
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

	/// <inheritdoc cref="IHasTag.Tag"/>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[Content='{Content}']";

}
