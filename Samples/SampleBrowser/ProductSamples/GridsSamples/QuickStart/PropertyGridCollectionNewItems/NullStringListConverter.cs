using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionNewItems;

/// <summary>
/// Represents a type converter for a string list that adds null values.
/// </summary>
public class NullStringListConverter : ExpandableCollectionConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? CreateItem(IPropertyModel propertyModel)
		=> null;

}
