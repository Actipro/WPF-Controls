using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems;

/// <summary>
/// Represents a type converter for lists that makes the items read-only.
/// </summary>
public class ReadOnlyItemsCollectionConverter : ExpandableCollectionConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override PropertyDescriptor CreateDictionaryItemPropertyDescriptor(ITypeDescriptorContext? context, Attribute[]? attributes, IDictionary dictionary, object key, Type itemType)
		=> new DictionaryItemPropertyDescriptor(dictionary, key, itemType, attributes: null, isCollectionReadOnly: false, isReadOnly: true);

	/// <inheritdoc/>
	protected override PropertyDescriptor CreateListItemPropertyDescriptor(ITypeDescriptorContext? context, Attribute[]? attributes, IList list, int index, Type itemType)
		=> new ListItemPropertyDescriptor(list, index, itemType, attributes: null, isCollectionReadOnly: false, isReadOnly: true);

}
