using System;
using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory {

	/// <summary>
	/// Represents a property model for a dictionary entry.
	/// </summary>
	/// <typeparam name="TKey">The key type.</typeparam>
	/// <typeparam name="TValue">The value type.</typeparam>
	public class DictionaryEntryPropertyModel<TKey, TValue> : CachedPropertyModelBase {

		private Dictionary<TKey, TValue>	dictionary;
		private TKey						key;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <see cref="DictionaryEntryPropertyModel"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="key">The key.</param>
		public DictionaryEntryPropertyModel(Dictionary<TKey, TValue> dictionary, TKey key) {
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");
			if (key == null)
				throw new ArgumentNullException("key");

			this.dictionary = dictionary;
			this.key = key;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether the property can be removed from an associated collection/parent.
		/// </summary>
		/// <value>
		/// <c>true</c> if the property can be removed from an associated collection/parent; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool CanRemoveCore {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets whether the property can be reset to its default value.
		/// </summary>
		/// <value>
		/// <c>true</c> if the property can be reset to its default value; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool CanResetValueCore {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// Gets whether the property can be merged when presenting multiple objects.
		/// </summary>
		/// <value>
		/// <c>true</c> if the property can be merged when presenting multiple objects; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool IsMergeableCore {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// Gets whether the data model has been modified.
		/// </summary>
		/// <value>
		/// <c>true</c> if the data model has been modified; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool IsModifiedCore {
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Gets whether the <see cref="Value"/> property is read-only.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Value"/> property is read-only; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool IsValueReadOnlyCore {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// Gets the name of the data model.
		/// </summary>
		/// <value>The name of the data model.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override string NameCore {
			get {
				return key.ToString();
			}
		}
		
		/// <summary>
		/// Removes the property from an associated collection/parent.
		/// </summary>
		public override void Remove() {
			var rootModel = this.Parent as IRootModel;
			if (rootModel != null) {
				var e = new PropertyModelChildChangeEventArgs(rootModel, this);
				this.RaiseChildPropertyRemovingEvent(e);
				if (!e.Cancel) {
					if (!dictionary.Remove(key))
						return;

					rootModel.Refresh(PropertyRefreshReason.CollectionItemRemoved);

					this.RaiseChildPropertyRemovedEvent(new PropertyModelChildChangeEventArgs(rootModel, this));

					// Focus the first entry
					var propGrid = rootModel.Source as PropertyGrid;
					if (propGrid != null) {
						var propertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault();
						if (propertyModel != null)
							propGrid.FocusItem(propertyModel);
					}
				}
			}
		}

		/// <summary>
		/// Gets the target object that owns the property.
		/// </summary>
		/// <value>The target object that owns the property.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override object TargetCore {
			get {
				return dictionary;
			}
		}
		
		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		/// <value>The property value.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override object ValueCore {
			get {
				return dictionary[key];
			}
			set {
				dictionary[key] = (TValue)value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="Type"/> of the <see cref="Value"/> property.
		/// </summary>
		/// <value>The <see cref="Type"/> of the <see cref="Value"/> property.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override Type ValueTypeCore {
			get {
				return typeof(TValue);
			}
		}
		
	}

}
