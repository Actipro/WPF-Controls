using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting;

/// <summary>
/// Represents a custom data factory.
/// </summary>
public class CustomDataFactory : TypeDescriptorFactory {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override ICategoryModel CreateCategoryModel(string name, IDataFactoryRequest request) {
		var categoryModel = base.CreateCategoryModel(name, request);

		if (name == "B")
			categoryModel.SortComparer = new NumericValueComparer();

		return categoryModel;
	}

}
