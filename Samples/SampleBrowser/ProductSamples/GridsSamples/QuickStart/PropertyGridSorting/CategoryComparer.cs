using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting;

/// <summary>
/// Provides a sort comparer implementation that sorts category "Z" before anything else.
/// </summary>
public class CategoryComparer : DataModelSortComparer {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override int Compare(IDataModel? x, IDataModel? y) {
		// Sort the "Z" category before anything else
		var xCategoryModel = x as ICategoryModel;
		var yCategoryModel = y as ICategoryModel;

		if (xCategoryModel?.Name == "Z")
			return (yCategoryModel?.Name == "Z") ? 0 : -1;
		else if (yCategoryModel?.Name == "Z")
			return 1;
		else
			return base.Compare(x, y);
	}

}
