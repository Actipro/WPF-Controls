using System;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionNewItems {
	
	/// <summary>
	/// Represents a type converter for a string list that adds custom strings.
	/// </summary>
	public class CustomStringListConverter : ExpandableCollectionConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Creates a new item that can be added into to the collection.
		/// </summary>
		/// <param name="propertyModel">The <see cref="IPropertyModel"/> associated with the collection.</param>
		/// <returns>The item that was created.</returns>
		public override object CreateItem(IPropertyModel propertyModel) {
			return string.Format("Added at {0:T}", DateTime.Now);
		}

	}

}
