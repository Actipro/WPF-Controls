using System;
using System.Collections.Generic;
using System.Windows;

namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses {

	/// <summary>
	/// Represents a storage object for items.
	/// </summary>
	public class ItemStore {
		
		private int			currentIndex;
		private List<Item>	items			= new List<Item>();

		/// <summary>
		/// Gets or sets the index of the current item.
		/// </summary>
		/// <value>The index of the current item.</value>
		public int CurrentIndex {
			get {
				return currentIndex;
			}
			set {
				currentIndex = value;
			}
		}

		/// <summary>
		/// Gets the collection of items.
		/// </summary>
		/// <value>The collection of items.</value>
		public List<Item> Items {
			get {
				return items;
			}
		}

	}

}
