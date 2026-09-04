namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball;

/// <summary>
/// A baseball team.
/// </summary>
public class Team : ObservableObjectBase {

	private Color _color;
	private string? _name;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The color.
	/// </summary>
	public Color Color {
		get => _color;
		set => SetProperty(ref _color, value);
	}

	/// <summary>
	/// The name.
	/// </summary>
	public string? Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

}
