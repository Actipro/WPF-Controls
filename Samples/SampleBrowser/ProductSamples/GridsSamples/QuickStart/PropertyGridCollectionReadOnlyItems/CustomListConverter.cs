using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems;

/// <summary>
/// Represents a type converter for lists that makes the first three items read-only.
/// </summary>
public class CustomListConverter : ExpandableCollectionConverter {

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Represents a <see cref="PropertyDescriptor"/> for an entry in a list.
	/// </summary>
	protected class CustomListPropertyDescriptor : ListItemPropertyDescriptor {

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="list">The list associated with the item.</param>
		/// <param name="index">The index of the item in the list.</param>
		/// <param name="itemType">Type of the item.</param>
		/// <param name="attributes">An <c>Attribute[]</c> with attributes to associated with the property.</param>
		/// <param name="isCollectionReadOnly">Whether the list is forced to be read-only, and this item cannot be removed from it.</param>
		/// <param name="isReadOnly">if set to <c>true</c> then this property will be read-only.</param>
		public CustomListPropertyDescriptor(IList list, int index, Type itemType, Attribute[]? attributes, bool isCollectionReadOnly, bool isReadOnly)
			: base(list, index, itemType, attributes, isCollectionReadOnly, isReadOnly) {
			// No-op
		}

		/// <inheritdoc/>
		public override bool CanRemove
			=> (Index >= 2) && base.CanRemove;

		/// <inheritdoc/>
		public override bool IsReadOnly
			=> (Index < 2) || base.IsReadOnly;

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override PropertyDescriptor CreateListItemPropertyDescriptor(ITypeDescriptorContext? context, Attribute[]? attributes, IList list, int index, Type itemType)
		=> new CustomListPropertyDescriptor(list, index, itemType, attributes: null, isCollectionReadOnly: false, isReadOnly: false);

}
