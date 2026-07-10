namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides a tree node model implementation of a special "Favorites" toolbox category.
/// </summary>
public class FavoritesCategoryTreeNodeModel : CategoryTreeNodeModel {

	private const string DefaultName = "Favorites";
	private const string DefaultEmptyPlaceholderText = "Drag and drop controls here to add them to favorites.";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public FavoritesCategoryTreeNodeModel() {
		Name = DefaultName;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override EmptyPlaceholderTreeNodeModel CreateEmptyPlaceholderTreeNodeModel() {
		// Provide a custom placeholder message for the favorites category
		var model = base.CreateEmptyPlaceholderTreeNodeModel();
		model.Name = DefaultEmptyPlaceholderText;
		return model;
	}

}
