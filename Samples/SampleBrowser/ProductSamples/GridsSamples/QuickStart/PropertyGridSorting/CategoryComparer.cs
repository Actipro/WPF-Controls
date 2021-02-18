using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting {
	
	/// <summary>
	/// Provides a sort comparer implementation that sorts category "Z" before anything else.
	/// </summary>
	public class CategoryComparer : DataModelSortComparer {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than, equal to or greater than the other.
		/// </summary>
		/// <param name="x">First object to compare.</param>
		/// <param name="y">Second object to compare.</param>
		/// <returns>A value indicating whether one is less than, equal to or greater than the other.</returns>
		public override int Compare(IDataModel x, IDataModel y) {
			// Sort the "Z" category before anything else
			var xCategoryModel = x as ICategoryModel;
			var yCategoryModel = y as ICategoryModel;
			if ((xCategoryModel != null) && (xCategoryModel.Name == "Z")) {
				if ((yCategoryModel != null) && (yCategoryModel.Name == "Z"))
					return 0;
				else
					return -1;
			}
			else if ((yCategoryModel != null) && (yCategoryModel.Name == "Z"))
				return 1;

			// Call the base method
			return base.Compare(x, y);
		}

	}

}
