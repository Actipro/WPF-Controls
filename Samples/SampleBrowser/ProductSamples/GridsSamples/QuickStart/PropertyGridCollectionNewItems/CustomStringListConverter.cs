using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionNewItems;

/// <summary>
/// Represents a type converter for a string list that adds custom strings.
/// </summary>
public class CustomStringListConverter : ExpandableCollectionConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object CreateItem(IPropertyModel propertyModel)
		=> string.Format("Added at {0:T}", DateTime.Now);

}
