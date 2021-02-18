using System.Collections.Generic;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting {
	
	/// <summary>
	/// Provides a sort comparer implementation that sorts display name numeric values.
	/// </summary>
	public class NumericValueComparer : DataModelSortComparer {

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
			var xValue = 0;
			var yValue = 1;

			int.TryParse(x.DisplayName, out xValue);
			int.TryParse(y.DisplayName, out yValue);

			return xValue.CompareTo(yValue) * (this.SortDescending ? -1 : 1);
		}

		/// <summary>
		/// Gets or sets whether to sort in descending order.
		/// </summary>
		/// <value>
		/// <c>true</c> if items should be sorted in descending order; otherwise, <c>false</c>.
		/// </value>
		public bool SortDescending { get; set; }

	}

}
