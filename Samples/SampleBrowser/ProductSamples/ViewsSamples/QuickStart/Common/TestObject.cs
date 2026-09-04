namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;

/// <summary>
/// Represents a test object for various Views samples.
/// </summary>
public class TestObject {

	private static readonly Random _random = new(Environment.TickCount);
	private static int _counter = 0;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TestObject() {
		Id = ++_counter;

		var r = (byte)(_random.NextDouble() * 255);
		var g = (byte)(_random.NextDouble() * 255);
		var b = (byte)(_random.NextDouble() * 255);
		Brush = new SolidColorBrush(Color.FromArgb(0x88, r, g, b));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The associated brush.
	/// </summary>
	public Brush Brush { get; }

	/// <summary>
	/// The identifier.
	/// </summary>
	public int Id { get; }

	/// <summary>
	/// Resets the counter.
	/// </summary>
	public static void ResetCounter()
		=> _counter = 0;

}
