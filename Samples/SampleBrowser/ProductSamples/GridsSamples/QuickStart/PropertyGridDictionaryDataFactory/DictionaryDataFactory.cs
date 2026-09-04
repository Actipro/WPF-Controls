using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory;

/// <summary>
/// Represents a custom data factory for editing a <see cref="Dictionary{TKey, TValue}"/>.
/// </summary>
/// <typeparam name="TKey">The key type.</typeparam>
/// <typeparam name="TValue">The value type.</typeparam>
public class DictionaryDataFactory<TKey, TValue> : DataFactoryBase where TKey: notnull {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IList<IPropertyModel>? GetPropertyModels(object dataObject, IDataFactoryRequest request) {
		if (dataObject is Dictionary<TKey, TValue> dictionary) {
			var propertyModels = new List<IPropertyModel>(dictionary.Count);
			foreach (var entry in dictionary)
				propertyModels.Add(new DictionaryEntryPropertyModel<TKey, TValue>(dictionary, entry.Key));

			return propertyModels;
		}

		return null;
	}

}
