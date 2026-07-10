namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a dockable toolbar control.
/// </summary>
public class DockableToolBarViewModel : BarKeyedObjectViewModelBase {

	private bool? _hasGripper;
	private bool? _hasOptionsButton;
	private bool _isVisible = true;
	private int _lineIndex;
	private double _offset;
	private Dock _placement = Dock.Top;
	private int _sortOrder;
	private string? _title;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DockableToolBarViewModel()  // Parameterless constructor required for XAML support
		: this(key: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key.  The title is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	public DockableToolBarViewModel(string? key)
		: this(key, title: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified key and title.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="title">The toolbar's title.</param>
	public DockableToolBarViewModel(string? key, string? title)
		: base(key) {

		_title = title ?? BarControlService.LabelGenerator.FromKey(key);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the toolbar has a gripper.
	/// </summary>
	/// <value>
	/// <c>true</c> if the toolbar has a gripper; otherwise, <c>false</c>.
	/// The default value is <c>null</c>, meaning inherit the value from the <see cref="DockableToolBarHostViewModel.ToolBarsHaveGrippers"/> property.
	/// </value>
	public bool? HasGripper {
		get => _hasGripper;
		set => SetProperty(ref _hasGripper, value);
	}

	/// <summary>
	/// Indicates whether the toolbar has an options button.
	/// </summary>
	/// <value>
	/// <c>true</c> if the toolbar has an options button; otherwise, <c>false</c>.
	/// The default value is <c>null</c>, meaning inherit the value from the <see cref="DockableToolBarHostViewModel.ToolBarsHaveOptionsButtons"/> property.
	/// </value>
	public bool? HasOptionsButton {
		get => _hasOptionsButton;
		set => SetProperty(ref _hasOptionsButton, value);
	}

	/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
	public bool IsVisible {
		get => _isVisible;
		set => SetProperty(ref _isVisible, value);
	}

	/// <summary>
	/// The collection of items in the control.
	/// </summary>
	public ObservableCollection<object> Items { get; } = [];

	/// <summary>
	/// The index of the <see cref="Placement"/> line the toolbar is within.
	/// </summary>
	public int LineIndex {
		get => _lineIndex;
		set => SetProperty(ref _lineIndex, value);
	}

	/// <summary>
	/// The anchor offset of the toolbar within its line.
	/// </summary>
	public double Offset {
		get => _offset;
		set => SetProperty(ref _offset, value);
	}

	/// <summary>
	/// A <see cref="Dock"/> specifying the toolbar placement.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Dock.Top"/>.
	/// </value>
	public Dock Placement {
		get => _placement;
		set => SetProperty(ref _placement, value);
	}

	/// <summary>
	/// The toolbar's sort order within its <see cref="Placement"/> line.
	/// </summary>
	public int SortOrder {
		get => _sortOrder;
		set => SetProperty(ref _sortOrder, value);
	}

	/// <summary>
	/// The toolbar's title.
	/// </summary>
	public string? Title {
		get => _title;
		set => SetProperty(ref _title, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[{Items.Count} items]";

}
