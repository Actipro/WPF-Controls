using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows {

	/// <summary>
	/// Represents the tool view-model.
	/// </summary>
	public class ToolItem1ViewModel : ToolItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolItem1ViewModel"/> class.
		/// </summary>
		public ToolItem1ViewModel() {
			this.SerializationId = "Tool1";  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			this.Title = "Tool 1";
		}

	}

}
