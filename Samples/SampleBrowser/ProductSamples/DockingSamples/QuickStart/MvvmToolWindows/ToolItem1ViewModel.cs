using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows;

/// <summary>
/// Represents the tool view-model.
/// </summary>
public class ToolItem1ViewModel : ToolItemViewModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ToolItem1ViewModel() {
		// NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
		SerializationId = "Tool1";
		Title = "Tool 1";
	}

}
