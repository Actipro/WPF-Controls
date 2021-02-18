using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting {

	/// <summary>
	/// Represents a custom data factory.
	/// </summary>
	public class CustomDataFactory : TypeDescriptorFactory {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates an <see cref="ICategoryModel"/> for the specified category name.
		/// </summary>
		/// <param name="name">The category name.</param>
		/// <param name="request">An <see cref="IDataFactoryRequest"/> that contains data about the request.</param>
		/// <returns>The <see cref="ICategoryModel"/> that was created.</returns>
		protected override ICategoryModel CreateCategoryModel(string name, IDataFactoryRequest request) {
			var categoryModel = base.CreateCategoryModel(name, request);

			if (name == "B")
				categoryModel.SortComparer = new NumericValueComparer();

			return categoryModel;
		}

	}

}
