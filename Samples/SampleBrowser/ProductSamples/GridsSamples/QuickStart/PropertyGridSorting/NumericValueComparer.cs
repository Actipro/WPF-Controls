using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting;

/// <summary>
/// Provides a sort comparer implementation that sorts display name numeric values.
/// </summary>
public class NumericValueComparer : DataModelSortComparer {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override int Compare(IDataModel? x, IDataModel? y) {
		_ = int.TryParse(x?.DisplayName, out var xValue);
		_ = int.TryParse(y?.DisplayName, out var yValue);

		return xValue.CompareTo(yValue) * (SortDescending ? -1 : 1);
	}

	/// <summary>
	/// Indicates whether to sort in descending order.
	/// </summary>
	public bool SortDescending { get; set; }

}
