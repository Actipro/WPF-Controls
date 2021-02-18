using System.Linq;
using System.Windows;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemToListButtonClick(object sender, RoutedEventArgs e) {
			var parentObj = (ParentObject)propGrid.DataObject;
			parentObj.ChildrenList.Add(new ChildObject());

			// Since a regular List<T> doesn't implement INotifyCollectionChanged, the property model must be notified manually...
			//   This isn't necessary if an observable collection is used
			var propertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault(m => m.Name == "ChildrenList");
			if (propertyModel != null)
				propertyModel.Refresh(PropertyRefreshReason.CollectionItemAdded);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemToObservableCollectionButtonClick(object sender, RoutedEventArgs e) {
			var parentObj = (ParentObject)propGrid.DataObject;
			parentObj.ChildrenObservableCollection.Add(new ChildObject());
		}

	}

}
