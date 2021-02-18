using System;
using System.Collections;
using System.ComponentModel;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems {
	
	/// <summary>
	/// Represents a type converter for lists that makes the items read-only.
	/// </summary>
	public class ReadOnlyItemsCollectionConverter : ExpandableCollectionConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates an instance of <see cref="PropertyDescriptor"/> for the dictionary item.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="attributes">An array of type <see cref="Attribute"/> that will be used as a filter.</param>
		/// <param name="dictionary">The dictionary that contains the item.</param>
		/// <param name="key">The key of the item in the dictionary.</param>
		/// <param name="itemType">Type of the item.</param>
		/// <returns>
		/// An instance of <see cref="PropertyDescriptor"/> for the dictionary item.
		/// </returns>
		protected override PropertyDescriptor CreateDictionaryItemPropertyDescriptor(ITypeDescriptorContext context, Attribute[] attributes, IDictionary dictionary, object key, Type itemType) {
			return new DictionaryItemPropertyDescriptor(dictionary, key, itemType, null, false, true);
		}

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
			return new ListItemPropertyDescriptor(list, index, itemType, null, false, true);
		}

	}

}
