namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a standalone toolbar control.
/// </summary>
public class StandaloneToolBarViewModel : ObservableObjectBase, IHasTag {

	private bool _isVisible = true;
	private BarControlTemplateSelector _itemContainerTemplateSelector = new();
	private object? _tag;
	private UserInterfaceDensity _userInterfaceDensity;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

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
	/// The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
	/// </summary>
	public BarControlTemplateSelector ItemContainerTemplateSelector {
		get => _itemContainerTemplateSelector;
		set => SetProperty(ref _itemContainerTemplateSelector, value);
	}

	/// <inheritdoc cref="IHasTag.Tag"/>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[{Items.Count} items]";

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
