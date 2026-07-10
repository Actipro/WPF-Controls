namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Specifies a tool item view's default location.
/// </summary>
public class ToolItemDefaultLocation : ObservableObjectBase {

	private ToolItemDockSide? _dockSide;
	private string? _targetSerializationId;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The optional <see cref="ToolItemDockSide"/> to dock against the target control.
	/// </summary>
	public ToolItemDockSide? DockSide {
		get => _dockSide;
		set => SetProperty(ref _dockSide, value);
	}

	/// <summary>
	/// The serialization ID of the target view-model.
	/// </summary>
	public string? TargetSerializationId {
		get => _targetSerializationId;
		set => SetProperty(ref _targetSerializationId, value);
	}

}
