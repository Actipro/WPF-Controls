namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for a mini-toolbar control.
/// </summary>
public class MiniToolBarViewModel : ObservableObjectBase, IHasTag {

	private bool _canUseMultiRowLayout;
	private UserInterfaceDensity _userInterfaceDensity = UserInterfaceDensity.Compact;
	private object? _tag;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the items can be arranged in a multi-row layout.
	/// </summary>
	/// <value>
	/// <c>true</c> if the items can be arranged in a multi-row layout; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool CanUseMultiRowLayout {
		get => _canUseMultiRowLayout;
		set => SetProperty(ref _canUseMultiRowLayout, value);
	}

	/// <summary>
	/// The collection of items in the control.
	/// </summary>
	public ObservableCollection<object> Items { get; } = [];

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
