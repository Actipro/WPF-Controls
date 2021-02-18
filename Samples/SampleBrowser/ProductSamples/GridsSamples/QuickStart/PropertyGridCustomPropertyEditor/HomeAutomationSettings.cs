using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomPropertyEditor {
	
	/// <summary>
	/// Stores home automation settings for demonstration purposes.
	/// </summary>
	public class HomeAutomationSettings : ObservableObjectBase {

		private OnOffAuto alarm;
		private OnOffAuto familyRoomLights;
		private OnOffAuto foyerLights;
		private OnOffAuto kitchenLights;
		private string profileName;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets a setting.
		/// </summary>
		/// <value>A setting.</value>
		[Category("Security")]
		[Description("The security alarm.")]
		public OnOffAuto Alarm {
			get {
				return this.alarm;
			}
			set {
				if (this.alarm != value) {
					this.alarm = value;
					this.NotifyPropertyChanged("Alarm");
				}
			}
		}

		/// <summary>
		/// Gets or sets a setting.
		/// </summary>
		/// <value>A setting.</value>
		[Category("Lighting")]
		[Description("The family room lights.")]
		public OnOffAuto FamilyRoomLights {
			get {
				return this.familyRoomLights;
			}
			set {
				if (this.familyRoomLights != value) {
					this.familyRoomLights = value;
					this.NotifyPropertyChanged("FamilyRoomLights");
				}
			}
		}

		/// <summary>
		/// Gets or sets a setting.
		/// </summary>
		/// <value>A setting.</value>
		[Category("Lighting")]
		[Description("The foyer lights.")]
		public OnOffAuto FoyerLights {
			get {
				return this.foyerLights;
			}
			set {
				if (this.foyerLights != value) {
					this.foyerLights = value;
					this.NotifyPropertyChanged("FoyerLights");
				}
			}
		}

		/// <summary>
		/// Gets or sets a setting.
		/// </summary>
		/// <value>A setting.</value>
		[Category("Lighting")]
		[Description("The kitchen lights.")]
		public OnOffAuto KitchenLights {
			get {
				return this.kitchenLights;
			}
			set {
				if (this.kitchenLights != value) {
					this.kitchenLights = value;
					this.NotifyPropertyChanged("KitchenLights");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a setting.
		/// </summary>
		/// <value>A setting.</value>
		[Category("General")]
		[Description("The profile name.")]
		public string ProfileName {
			get {
				return this.profileName;
			}
			set {
				if (this.profileName != value) {
					this.profileName = value;
					this.NotifyPropertyChanged("ProfileName");
				}
			}
		}

	}

}
