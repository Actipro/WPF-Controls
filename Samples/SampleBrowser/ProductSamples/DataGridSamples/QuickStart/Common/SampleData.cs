namespace ActiproSoftware.ProductSamples.DataGridSamples.Common;

/// <summary>
/// Represents a simple set of data for demonstration purposes.
/// </summary>
public class SampleData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A boolean value.
	/// </summary>
	public bool Boolean { get; set; }

	/// <summary>
	/// A date/time value.
	/// </summary>
	public DateTime DateTime { get; set; }

	/// <summary>
	/// An integer value.
	/// </summary>
	public int Integer { get; set; }

	/// <summary>
	/// A long string value.
	/// </summary>
	public string? LongString { get; set; }

	/// <summary>
	/// A string value.
	/// </summary>
	public string? String { get; set; }

	/// <summary>
	/// The fixed static values.
	/// </summary>
	public static List<SampleData> Values {
		get {
			var now = DateTime.Now;

			var values = new List<SampleData>();
			for (var x = 1; x <= 10; x++) {
				var data = new SampleData {
					Boolean = (0 == (x % 2)),
					DateTime = new DateTime(now.Year, now.Month, x),
					Integer = x,
					String = string.Format("String {0}", x),
					LongString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In dui nunc, feugiat a vestibulum eget, interdum quis nunc. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed non turpis erat, eu placerat lorem. Cras quis enim eget eros malesuada sagittis nec vel diam. Integer scelerisque fringilla sapien ac condimentum. Integer consequat libero sed tortor venenatis dapibus. Duis ultricies molestie ligula, quis tristique justo egestas sit amet. Duis fringilla velit a sem rhoncus aliquam."
				};
				values.Add(data);
			}
			return values;
		}
	}

}
