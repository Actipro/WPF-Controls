using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory {

	/// <summary>
	/// Represents a referral source view model object.
	/// </summary>
	public class ReferralSourceViewModel : ObservableObjectBase {

		private int id;
		private string name;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
				this.NotifyPropertyChanged("Id");
			}
		}
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				this.NotifyPropertyChanged("Name");
			}
		}

	}

}
