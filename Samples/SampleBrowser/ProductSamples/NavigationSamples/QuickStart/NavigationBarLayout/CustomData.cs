using System.Xml.Serialization;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarLayout;

/// <summary>
/// Custom data that can be serialized to a layout.
/// </summary>
public class CustomData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The string value in this custom data.
	/// </summary>
	[XmlAttribute()]
	public string? StringValue { get; set; }

}

