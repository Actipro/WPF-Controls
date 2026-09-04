namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.BackgroundPicker;

/// <summary>
/// Stores brush data.
/// </summary>
public class BrushData : ObservableObjectBase {

	private Brush? _brush;
	private string? _description;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Brush"/>.
	/// </summary>
	public Brush? Brush {
		get => _brush;
		set => SetProperty(ref _brush, value);
	}

	/// <summary>
	/// The description of the brush.
	/// </summary>
	public string? Description {
		get => _description;
		set => SetProperty(ref _description, value);
	}

}
