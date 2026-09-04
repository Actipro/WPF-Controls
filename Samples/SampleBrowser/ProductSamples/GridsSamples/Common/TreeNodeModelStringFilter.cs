using ActiproSoftware.Windows.Data.Filtering;

namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// Provides a common implementation of string-based filter for tree node model.
/// </summary>
public class TreeNodeModelStringFilter : StringFilterBase {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override DataFilterResult Filter(object item, object? context) {
		var model = item as TreeNodeModel;
		var source = model?.Name;

		return FilterByString(source, Value)
			? IncludedFilterResult
			: DataFilterResult.IncludedByDescendants;
	}

}
