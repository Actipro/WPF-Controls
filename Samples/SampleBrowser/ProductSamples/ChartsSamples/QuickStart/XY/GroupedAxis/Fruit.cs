namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.GroupedAxis;

/// <summary>
/// Represents a fruit, used for sample data.
/// </summary>
/// <param name="name">The name.</param>
/// <param name="color">The color.</param>
/// <param name="calories">The calories.</param>
public class Fruit(string name, string color, double calories) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	public string Name { get; } = name;

	/// <summary>
	/// The color.
	/// </summary>
	public string Color { get; } = color;

	/// <summary>
	/// The calories.
	/// </summary>
	public double Calories { get; } = calories;

}
