using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows {

	/// <summary>
	/// Represents the tool view-model.
	/// </summary>
	public class ToolItem3ViewModel : ToolItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolItem3ViewModel"/> class.
		/// </summary>
		public ToolItem3ViewModel() {
			this.SerializationId = "Tool3";  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			this.Title = "Tool 3";
		}

	}

}
