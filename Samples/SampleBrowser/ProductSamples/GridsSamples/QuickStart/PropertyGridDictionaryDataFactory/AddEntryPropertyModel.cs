using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory {

	/// <summary>
	/// Represents a property model that can be used to add entries.
	/// </summary>
	public class AddEntryPropertyModel : PropertyModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <see cref="AddEntryPropertyModel"/> class.
		/// </summary>
		public AddEntryPropertyModel() {
			this.SortImportance = DataModelSortImportance.AfterProperty;
			this.ValueType = typeof(string);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds a new child into to an associated collection/parent.
		/// </summary>
		public override void AddChild() {
			if (this.CanAddChild) {
				var rootModel = ((IDataModel)this).Parent as IRootModel;
				if (rootModel != null) {
					var dictionary = rootModel.Value as Dictionary<string, string>;
					if (dictionary != null) {
						var entry = new KeyValuePair<string, string>(this.DisplayName, this.ValueAsString);

						var e = new PropertyModelChildChangeEventArgs(rootModel, entry);
						this.RaiseChildPropertyAddingEvent(e);
						if (!e.Cancel) {
							dictionary[entry.Key] = entry.Value;

							rootModel.Refresh(PropertyRefreshReason.CollectionItemAdded);

							this.RaiseChildPropertyAddedEvent(new PropertyModelChildChangeEventArgs(rootModel, entry));

							this.DisplayName = null;
							this.ValueAsString = null;

							// Focus the entry that was created
							var propGrid = rootModel.Source as PropertyGrid;
							if (propGrid != null) {
								var propertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault(m => m.Name == entry.Key);
								if (propertyModel != null)
									propGrid.FocusItem(propertyModel);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets whether a new child can be added to an associated collection/parent.
		/// </summary>
		/// <value>
		/// <c>true</c> if a new child can be added to an associated collection/parent; otherwise, <c>false</c>.
		/// </value>
		public override bool CanAddChild {
			get {
				return (!string.IsNullOrEmpty(this.DisplayName)) && (!string.IsNullOrEmpty(this.ValueAsString));
			}
		}
		
		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
		protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
			if (e != null) {
				switch (e.PropertyName) {
					case "DisplayName":
					case "Value":
						// When the Name or ValueAsString properties are edited, notify that the CanAddChild property might have changed
						this.NotifyPropertyChanged("CanAddChild");
						break;
				}
			}

			// Call the base method
			base.OnPropertyChanged(e);
		}
		
	}

}
