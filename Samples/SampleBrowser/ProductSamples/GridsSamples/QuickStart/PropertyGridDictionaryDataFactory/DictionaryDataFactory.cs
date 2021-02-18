using System.Collections.Generic;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory {

	/// <summary>
	/// Represents a custom data factory for editing a <see cref="Dictionary{TKey, TValue}"/>.
	/// </summary>
	/// <typeparam name="TKey">The key type.</typeparam>
	/// <typeparam name="TValue">The value type.</typeparam>
	public class DictionaryDataFactory<TKey, TValue> : DataFactoryBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the <see cref="IPropertyModel"/> collection for the specified data object.
		/// </summary>
		/// <param name="dataObject">The single object to examine.</param>
		/// <param name="request">An <see cref="IDataFactoryRequest"/> that contains data about the request.</param>
		/// <returns>A collection of <see cref="IPropertyModel"/> objects, if the specified data object has any properties.</returns>
		protected override IList<IPropertyModel> GetPropertyModels(object dataObject, IDataFactoryRequest request) {
			var dictionary = dataObject as Dictionary<TKey, TValue>;
			if (dictionary != null) {
				var propertyModels = new List<IPropertyModel>(dictionary.Count);
				foreach (var entry in dictionary)
					propertyModels.Add(new DictionaryEntryPropertyModel<TKey, TValue>(dictionary, entry.Key));

				return propertyModels;
			}

			return null;
		}

	}

}
