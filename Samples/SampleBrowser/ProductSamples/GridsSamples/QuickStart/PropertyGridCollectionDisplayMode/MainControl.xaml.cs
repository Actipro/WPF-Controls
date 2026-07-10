using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnAddItemToListButtonClick(object sender, RoutedEventArgs e) {
		if (propGrid.DataObject is ParentObject parentObj) {
			parentObj.ChildrenList.Add(new ChildObject());

			// Since a regular List<T> doesn't implement INotifyCollectionChanged, the property model must be notified manually...
			//   This isn't necessary if an observable collection is used
			var propertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault(m => m.Name == "ChildrenList");
			propertyModel?.Refresh(PropertyRefreshReason.CollectionItemAdded);
		}
	}

	private void OnAddItemToObservableCollectionButtonClick(object sender, RoutedEventArgs e) {
		if (propGrid.DataObject is ParentObject parentObj)
			parentObj.ChildrenObservableCollection.Add(new ChildObject());
	}

}
