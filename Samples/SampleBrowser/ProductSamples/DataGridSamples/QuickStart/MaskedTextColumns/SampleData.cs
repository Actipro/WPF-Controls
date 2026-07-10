namespace ActiproSoftware.ProductSamples.DataGridSamples.QuickStart.MaskedTextColumns;

/// <summary>
/// Represents a simple set of data for demonstration purposes.
/// </summary>
public class SampleData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// An identifier.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// A social security number.
	/// </summary>
	public string? SocialSecurity { get; set; }

	/// <summary>
	/// A phone number.
	/// </summary>
	public string? Phone { get; set; }

	/// <summary>
	/// The fixed static values.
	/// </summary>
	public static List<SampleData> Values {
		get => [
			new() { Id = 1, SocialSecurity = "123-45-6789", Phone = "1-703-555-1212" },
			new() { Id = 2, SocialSecurity = "234-56-7890", Phone = "(571) 555-1212" },
			new() { Id = 3, SocialSecurity = "345-67-8901", Phone = "212-555-1212" },
			new() { Id = 4, SocialSecurity = "456-78-9012", Phone = "555-1212" },
			new() { Id = 5, SocialSecurity = "567-89-0123", Phone = "(202) 555-1212" },
		];
	}

}
