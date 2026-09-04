namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

//
// NOTE: The DefaultDockSide and State properties in this VM class return ToolItemDockSide and ToolItemState enums,
//   which allows for an abstraction layer between them and the Side/DockingWindowState enum values they represent.
//   This is useful in scenarios where you don't wish to have your models directly reference types in the Docking/MDI assembly.
//   If that is not a factor, there is nothing wrong with changing the properties to directly return the two Actipro types instead
//

/// <summary>
/// Represents a tool item view-model.
/// </summary>
public class ToolItemViewModel : DockingItemViewModelBase {

	private ToolItemDockSide _defaultDockSide = ToolItemDockSide.Right;
	private ToolItemState _state = ToolItemState.Docked;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The default side that the tool window will dock towards when no prior location is known.
	/// </summary>
	public ToolItemDockSide DefaultDockSide {
		get => _defaultDockSide;
		set => SetProperty(ref _defaultDockSide, value);
	}

	/// <inheritdoc/>
	public override bool IsTool
		=> true;

	/// <summary>
	/// The current state of the view.
	/// </summary>
	public ToolItemState State {
		get => _state;
		set => SetProperty(ref _state, value);
	}

}
