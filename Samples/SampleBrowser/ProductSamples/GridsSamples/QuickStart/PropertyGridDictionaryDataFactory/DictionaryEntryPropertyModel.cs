using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory;

/// <summary>
/// Represents a property model for a dictionary entry.
/// </summary>
/// <typeparam name="TKey">The key type.</typeparam>
/// <typeparam name="TValue">The value type.</typeparam>
public class DictionaryEntryPropertyModel<TKey, TValue> : CachedPropertyModelBase where TKey: notnull {

	private readonly Dictionary<TKey, TValue> _dictionary;
	private readonly TKey _key;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="dictionary">The dictionary.</param>
	/// <param name="key">The key.</param>
	public DictionaryEntryPropertyModel(Dictionary<TKey, TValue> dictionary, TKey key) {
		if (dictionary is null)
			throw new ArgumentNullException(nameof(dictionary));
		if (key is null)
			throw new ArgumentNullException(nameof(key));

		_dictionary = dictionary;
		_key = key;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override bool CanRemoveCore
		=> true;

	/// <inheritdoc/>
	protected override bool CanResetValueCore
		=> false;

	/// <inheritdoc/>
	protected override bool IsMergeableCore
		=> false;

	/// <inheritdoc/>
	protected override bool IsModifiedCore
		=> true;

	/// <inheritdoc/>
	protected override bool IsValueReadOnlyCore
		=> false;

	/// <inheritdoc/>
	protected override string? NameCore
		=> _key.ToString();

	/// <inheritdoc/>
	public override void Remove() {
		if (Parent is IRootModel rootModel) {
			var e = new PropertyModelChildChangeEventArgs(rootModel, this);
			RaiseChildPropertyRemovingEvent(e);
			if (!e.Cancel) {
				if (!_dictionary.Remove(_key))
					return;

				rootModel.Refresh(PropertyRefreshReason.CollectionItemRemoved);

				RaiseChildPropertyRemovedEvent(new PropertyModelChildChangeEventArgs(rootModel, this));

				// Focus the first entry
				if (
					rootModel.Source is PropertyGrid propGrid
					&& propGrid.Items.OfType<IPropertyModel>().FirstOrDefault() is { } propertyModel
				) {
					propGrid.FocusItem(propertyModel);
				}
			}
		}
	}

	/// <inheritdoc/>
	protected override object TargetCore
		=> _dictionary;

	/// <inheritdoc/>
	protected override object? ValueCore {
		get => _dictionary[_key];
		set => _dictionary[_key] = (TValue)value!;
	}

	/// <inheritdoc/>
	protected override Type ValueTypeCore
		=> typeof(TValue);

}
