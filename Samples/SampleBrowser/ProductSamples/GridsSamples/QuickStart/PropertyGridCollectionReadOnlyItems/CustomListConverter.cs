using System;
using System.Collections;
using System.ComponentModel;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems {
	
	/// <summary>
	/// Represents a type converter for lists that makes the first three items read-only.
	/// </summary>
	public class CustomListConverter : ExpandableCollectionConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Represents a <see cref="PropertyDescriptor"/> for an entry in a list.
		/// </summary>
		protected class CustomListPropertyDescriptor : ListItemPropertyDescriptor {

			/// <summary>
			/// Initializes a new instance of the <see cref="ListItemPropertyDescriptor"/> class.
			/// </summary>
			/// <param name="list">The list associated with the item.</param>
			/// <param name="index">The index of the item in the list.</param>
			/// <param name="itemType">Type of the item.</param>
			/// <param name="attributes">An <c>Attribute[]</c> with attributes to associated with the property.</param>
			/// <param name="isCollectionReadOnly">Whether the list is forced to be read-only, and this item cannot be removed from it.</param>
			/// <param name="isReadOnly">if set to <c>true</c> then this property will be read-only.</param>
			public CustomListPropertyDescriptor(IList list, int index, Type itemType, Attribute[] attributes, bool isCollectionReadOnly, bool isReadOnly)
				: base(list, index, itemType, attributes, isCollectionReadOnly, isReadOnly) {
				// No-op
			}

			/// <summary>
			/// Gets a value indicating whether the collection entry can be removed.
			/// </summary>
			/// <value>
			/// <c>true</c> if the collection entry can be removed; otherwise, <c>false</c>.
			/// </value>
			public override bool CanRemove {
				get {
					return (this.Index >= 2 && base.CanRemove);
				}
			}

			/// <summary>
			/// Gets a value indicating whether this property is read-only.
			/// </summary>
			/// <value></value>
			/// <returns><c>true</c> if the property is read-only; <c>false</c> if the property is read/write.</returns>
			public override bool IsReadOnly {
				get {
					return (this.Index < 2 || base.IsReadOnly);
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates an instance of <see cref="PropertyDescriptor"/> for the list item.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="attributes">An array of type <see cref="Attribute"/> that will be used as a filter.</param>
		/// <param name="list">The list that contains the item.</param>
		/// <param name="index">The index of the item in the list.</param>
		/// <param name="itemType">Type of the item.</param>
		/// <returns>
		/// An instance of <see cref="PropertyDescriptor"/> for the list item.
		/// </returns>
		protected override PropertyDescriptor CreateListItemPropertyDescriptor(ITypeDescriptorContext context, Attribute[] attributes, IList list, int index, Type itemType) {
			return new CustomListPropertyDescriptor(list, index, itemType, null, false, false);
		}

	}

}
