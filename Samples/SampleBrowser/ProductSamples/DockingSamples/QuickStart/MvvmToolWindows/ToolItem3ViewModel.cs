using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows;

/// <summary>
/// Represents the tool view-model.
/// </summary>
public class ToolItem3ViewModel : ToolItemViewModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ToolItem3ViewModel() {
		// NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
		SerializationId = "Tool3";
		Title = "Tool 3";
	}

}
