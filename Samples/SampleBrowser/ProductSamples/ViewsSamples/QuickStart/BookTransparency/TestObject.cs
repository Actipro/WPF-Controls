namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookTransparency;

/// <summary>
/// Represents a test object for this sample.
/// </summary>
public class TestObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TestObject() {
		Header = " ";
		Footer = " ";
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The footer.
	/// </summary>
	public string Footer { get; set; }

	/// <summary>
	/// The header.
	/// </summary>
	public string Header { get; set; }

	/// <summary>
	/// The content.
	/// </summary>
	public ImageSource? ImageSource { get; set; }

}
