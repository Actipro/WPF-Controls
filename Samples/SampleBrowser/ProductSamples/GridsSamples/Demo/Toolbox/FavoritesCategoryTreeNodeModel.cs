namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox {

	/// <summary>
	/// Provides a tree node model implementation of a special "Favorites" toolbox category.
	/// </summary>
	public class FavoritesCategoryTreeNodeModel : CategoryTreeNodeModel {

		private const string DefaultName = "Favorites";
		private const string DefaultEmptyPlaceholderText = "Drag and drop controls here to add them to favorites.";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>FavoritesCategoryTreeNodeModel</c> class.
		/// </summary>
		public FavoritesCategoryTreeNodeModel() {
			this.Name = DefaultName;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new instance of the model used for the placeholder of a category when it has no controls as children.
		/// </summary>
		/// <returns>A new instance of <see cref="EmptyPlaceholderTreeNodeModel"/>.</returns>
		protected override EmptyPlaceholderTreeNodeModel CreateEmptyPlaceholderTreeNodeModel() {
			// Provide a custom placeholder message for the favorites category
			var model = base.CreateEmptyPlaceholderTreeNodeModel();
			model.Name = DefaultEmptyPlaceholderText;
			return model;
		}

	}

}
