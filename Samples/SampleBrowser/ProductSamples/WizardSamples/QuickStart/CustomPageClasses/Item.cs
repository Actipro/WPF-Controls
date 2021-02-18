using System;
using System.ComponentModel;
using System.Windows;

namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses {

	/// <summary>
	/// Represents an item for this sample.
	/// </summary>
	public class Item {

		private string description;
		private int index;
		private string name;

		/// <summary>
		/// Gets or sets the description of the item.
		/// </summary>
		/// <value>The description of the item.</value>
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}

		/// <summary>
		/// Gets or sets the index of the item.
		/// </summary>
		/// <value>The index of the item.</value>
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the item.
		/// </summary>
		/// <value>The name of the item.</value>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

	}

}
