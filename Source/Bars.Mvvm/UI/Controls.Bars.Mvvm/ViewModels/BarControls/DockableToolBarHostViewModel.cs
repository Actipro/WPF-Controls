namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a dockable toolbar host control.
/// </summary>
public class DockableToolBarHostViewModel : ObservableObjectBase, IHasTag {

	private bool _canToolBarsFloat = true;
	private VariantCollection? _controlVariants;
	private double _lineSpacing = 1.0;
	private double _toolBarItemSpacing = 1.0;
	private double _toolBarSpacing = 1.0;
	private BarControlTemplateSelector _itemContainerTemplateSelector = new();
	private object? _tag;
	private bool _toolBarsHaveGrippers = true;
	private bool _toolBarsHaveOptionsButtons = true;
	private UserInterfaceDensity _userInterfaceDensity = UserInterfaceDensity.Compact;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether toolbars can float.
	/// </summary>
	/// <value>
	/// <c>true</c> if toolbars can float; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool CanToolBarsFloat {
		get => _canToolBarsFloat;
		set => SetProperty(ref _canToolBarsFloat, value);
	}

	/// <summary>
	/// The collection of variant size transitions to apply to all controls within the toolbars.
	/// </summary>
	public VariantCollection? ControlVariants {
		get => _controlVariants;
		set => SetProperty(ref _controlVariants, value);
	}

	/// <summary>
	/// The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
	/// </summary>
	public BarControlTemplateSelector ItemContainerTemplateSelector {
		get => _itemContainerTemplateSelector;
		set => SetProperty(ref _itemContainerTemplateSelector, value);
	}

	/// <summary>
	/// The spacing between lines.
	/// </summary>
	/// <value>
	/// The default value is <c>1.0</c>.
	/// </value>
	public double LineSpacing {
		get => _lineSpacing;
		set => SetProperty(ref _lineSpacing, value);
	}

	/// <inheritdoc cref="IHasTag.Tag"/>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <summary>
	/// The amount of spacing between toolbar items.
	/// </summary>
	/// <value>
	/// The default value is <c>1.0</c>.
	/// </value>
	public double ToolBarItemSpacing {
		get => _toolBarItemSpacing;
		set => SetProperty(ref _toolBarItemSpacing, value);
	}

	/// <summary>
	/// The collection of dockable toolbars managed by the host.
	/// </summary>
	public ObservableCollection<DockableToolBarViewModel> ToolBars { get; } = [];

	/// <summary>
	/// The spacing between toolbars on the same line.
	/// </summary>
	/// <value>
	/// The default value is <c>1.0</c>.
	/// </value>
	public double ToolBarSpacing {
		get => _toolBarSpacing;
		set => SetProperty(ref _toolBarSpacing, value);
	}

	/// <summary>
	/// The default setting for whether toolbars have grippers.
	/// </summary>
	/// <value>
	/// <c>true</c> if toolbars have grippers by default; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool ToolBarsHaveGrippers {
		get => _toolBarsHaveGrippers;
		set => SetProperty(ref _toolBarsHaveGrippers, value);
	}

	/// <summary>
	/// The default setting for whether toolbars have options buttons.
	/// </summary>
	/// <value>
	/// <c>true</c> if toolbars have options buttons by default; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool ToolBarsHaveOptionsButtons {
		get => _toolBarsHaveOptionsButtons;
		set => SetProperty(ref _toolBarsHaveOptionsButtons, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[{ToolBars.Count} toolbars]";

	/// <summary>
	/// A <see cref="Themes.UserInterfaceDensity"/> that indicates how compact or spacious the UI should appear.
	/// </summary>
	/// <value>
	/// The default value is <see cref="UserInterfaceDensity.Compact"/>.
	/// </value>
	public UserInterfaceDensity UserInterfaceDensity {
		get => _userInterfaceDensity;
		set => SetProperty(ref _userInterfaceDensity, value);
	}

}
