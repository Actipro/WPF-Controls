using System.Windows.Documents;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Implements a simple <see cref="FlowDocument"/> with basic styling.
/// </summary>
public class SimpleFlowDocument : FlowDocument {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleFlowDocument() {
		DefaultStyleKey = typeof(SimpleFlowDocument);
	}

}
