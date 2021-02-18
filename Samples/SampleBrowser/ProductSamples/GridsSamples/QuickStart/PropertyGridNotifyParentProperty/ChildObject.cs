using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNotifyParentProperty {

	/// <summary>
	/// Represents an expandable class with two properties.
	/// </summary>
	[TypeConverter(typeof(ChildObjectConverter))]
	public class ChildObject {

		private string willNotify = "Actipro";
		private string willNotNotify = "Software";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a property that will notify it's parent property of changes.
		/// </summary>
		/// <value>A string value.</value>
		[DefaultValue("Actipro")]
		[Description("Changes to this property will be automatically reflected in the parent property.")]
		[NotifyParentProperty(true)]
		public string WillNotify {
			get {
				return this.willNotify;
			}
			set {
				this.willNotify = value;
			}
		}

		/// <summary>
		/// Gets or sets a property that will not notify it's parent property of changes.
		/// </summary>
		/// <value>A string value.</value>
		[DefaultValue("Software")]
		[Description("Changes to this property will *not* be automatically reflected in the parent property.")]
		public string WillNotNotify {
			get {
				return this.willNotNotify;
			}
			set {
				this.willNotNotify = value;
			}
		}

	}

}
