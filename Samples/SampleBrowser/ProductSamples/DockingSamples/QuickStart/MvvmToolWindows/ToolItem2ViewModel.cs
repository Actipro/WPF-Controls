using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows {

	/// <summary>
	/// Represents the tool view-model.
	/// </summary>
	public class ToolItem2ViewModel : ToolItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolItem2ViewModel"/> class.
		/// </summary>
		public ToolItem2ViewModel() {
			this.SerializationId = "Tool2";  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			this.Title = "Tool 2";
		}

	}

}
