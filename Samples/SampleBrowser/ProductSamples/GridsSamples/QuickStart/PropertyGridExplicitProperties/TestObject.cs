using System.ComponentModel.DataAnnotations;
using System.Windows;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridExplicitProperties {
	
	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject : ObservableObjectBase {

		private Size recommendedSize;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		[Display(GroupName = "Defined on TestObject", ShortName = "Recommended size", Description = "This property is defined on the TestObject class instance bound to PropertyGrid.DataObject.")]
		public Size RecommendedSize {
			get {
				return recommendedSize;
			}
			set {
				if (recommendedSize != value) {
					recommendedSize = value;
					this.NotifyPropertyChanged("RecommendedSize");
				}
			}
		}

	}

}
