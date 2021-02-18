using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects {
	
	/// <summary>
	/// Represents a base object.
	/// </summary>
	public abstract class BaseObject : ObservableObjectBase {

		private string derivedOnly;
		private string name;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[DefaultValue("")]
		[Description("A property that is defined on BaseObject and will appear on derived objects. Changes in one entry will be reflected all duplicate entries.")]
		public string DerivedOnly {
			get {
				return this.derivedOnly;
			}
			set {
				if (this.derivedOnly != value) {
					this.derivedOnly = value;
					this.NotifyPropertyChanged("DerivedOnly");
				}
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[DefaultValue("")]
		[Description("The name of the object, which can appear more than once in this sample. Changes in one entry will be reflected all duplicate entries.")]
		public string Name {
			get {
				return this.name;
			}
			set {
				if (this.name != value) {
					this.name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}

	}

}
