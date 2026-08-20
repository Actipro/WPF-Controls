using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory;

/// <summary>
/// Represents a property model that can be used to add entries.
/// </summary>
public class AddEntryPropertyModel : PropertyModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AddEntryPropertyModel() {
		SortImportance = DataModelSortImportance.AfterProperty;
		ValueType = typeof(string);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void AddChild() {
		if (
			CanAddChild
			&& DisplayName is { } displayName
			&& ValueAsString is { } valueAsString
		) {
			var rootModel = ((IDataModel)this).Parent as IRootModel;
			if (rootModel is { Value: Dictionary<string, string> dictionary }) {
				var entry = new KeyValuePair<string, string>(displayName, valueAsString);

				var e = new PropertyModelChildChangeEventArgs(rootModel, entry);
				RaiseChildPropertyAddingEvent(e);
				if (!e.Cancel) {
					dictionary[entry.Key] = entry.Value;

					rootModel.Refresh(PropertyRefreshReason.CollectionItemAdded);

					RaiseChildPropertyAddedEvent(new PropertyModelChildChangeEventArgs(rootModel, entry));

					DisplayName = null;
					ValueAsString = null;

					// Focus the entry that was created
					if (rootModel.Source is PropertyGrid propGrid) {
						var propertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault(m => m.Name == entry.Key);
						if (propertyModel is not null)
							propGrid.FocusItem(propertyModel);
					}
				}
			}
		}
	}

	/// <inheritdoc/>
	public override bool CanAddChild
		=> this is {DisplayName: not null, ValueAsString: not null };

	/// <inheritdoc/>
	protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(DisplayName):
			case nameof(Value):
				// When the Name or ValueAsString properties are edited, notify that the CanAddChild property might have changed
				OnPropertyChanged(nameof(CanAddChild));
				break;
		}

		base.OnPropertyChanged(e);
	}

}
