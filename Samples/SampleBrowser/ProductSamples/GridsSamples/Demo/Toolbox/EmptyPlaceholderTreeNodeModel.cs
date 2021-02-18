namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox {

	/// <summary>
	/// Provides a tree node model implementation of a placeholder for an empty toolbox category.
	/// </summary>
	public class EmptyPlaceholderTreeNodeModel : ToolboxTreeNodeModel {

		private const string DefaultPlaceholderText = "There are no controls in this group.";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyPlaceholderTreeNodeModel"/> class.
		/// </summary>
		public EmptyPlaceholderTreeNodeModel() {
			this.Name = DefaultPlaceholderText;
		}		
		
	}

}
