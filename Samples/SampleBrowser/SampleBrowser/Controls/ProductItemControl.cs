using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Renders the user interface of a <see cref="ProductItemInfo"/>.
/// </summary>
[ContentProperty(nameof(Child))]
public class ProductItemControl : Control {

	private DelegateCommand<object>? _toggleIsSideBarVisibleCommand;

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Child"/> property.
	/// </summary>
	public static readonly DependencyProperty ChildProperty
		= DependencyProperty.Register(nameof(Child), typeof(UIElement), typeof(ProductItemControl), new FrameworkPropertyMetadata(defaultValue: null, OnChildPropertyValueChanged));

	/// <summary>
	/// Defines the <see cref="IsSideBarVisible"/> property.
	/// </summary>
	public static readonly DependencyProperty IsSideBarVisibleProperty
		= DependencyProperty.Register(nameof(IsSideBarVisible), typeof(bool), typeof(ProductItemControl), new FrameworkPropertyMetadata(defaultValue: true));

	/// <summary>
	/// Defines the <see cref="SideBarContent"/> property.
	/// </summary>
	public static readonly DependencyProperty SideBarContentProperty
		= DependencyProperty.Register(nameof(SideBarContent), typeof(UIElement), typeof(ProductItemControl), new FrameworkPropertyMetadata(defaultValue: null));

	/// <summary>
	/// Defines the <see cref="SideBarWidth"/> property.
	/// </summary>
	public static readonly DependencyProperty SideBarWidthProperty
		= DependencyProperty.Register(nameof(SideBarWidth), typeof(double), typeof(ProductItemControl), new FrameworkPropertyMetadata(defaultValue: 400.0));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ProductItemControl() {
		DefaultStyleKey = typeof(ProductItemControl);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnChildPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
		var control = (ProductItemControl)sender;

		if (e.OldValue is UIElement oldChild)
			control.RemoveLogicalChild(oldChild);
		if (e.NewValue is UIElement newChild)
			control.AddLogicalChild(newChild);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The child element.
	/// </summary>
	public UIElement? Child {
		get => (UIElement)GetValue(ChildProperty);
		set => SetValue(ChildProperty, value);
	}

	/// <summary>
	/// Indicates whether the sidebar is visible.
	/// </summary>
	public bool IsSideBarVisible {
		get => (bool)GetValue(IsSideBarVisibleProperty);
		set => SetValue(IsSideBarVisibleProperty, value);
	}

	/// <inheritdoc/>
	protected override IEnumerator LogicalChildren {
		get {
			if (Child is { } child)
				yield return child;
		}
	}

	/// <summary>
	/// Notifies the UI that it has been unloaded.
	/// </summary>
	public virtual void NotifyUnloaded() { }

	/// <summary>
	/// The sidebar content element.
	/// </summary>
	public UIElement? SideBarContent {
		get => (UIElement)GetValue(SideBarContentProperty);
		set => SetValue(SideBarContentProperty, value);
	}

	/// <summary>
	/// The sidebar width.
	/// </summary>
	public double SideBarWidth {
		get => (double)GetValue(SideBarWidthProperty);
		set => SetValue(SideBarWidthProperty, value);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that toggles whether the sidebar is open.
	/// </summary>
	public ICommand ToggleIsSideBarVisibleCommand {
		get => _toggleIsSideBarVisibleCommand ??= new DelegateCommand<object>(_ => {
			IsSideBarVisible = !IsSideBarVisible;
		});
	}

}
