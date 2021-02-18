using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomContextContent {

	/// <summary>
	/// Represents the context view-model.
	/// </summary>
	public class ContextViewModel : ObservableObjectBase {

		private bool isApproved;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the data is approved.
		/// </summary>
		/// <value>
		/// <c>true</c> if the data is approved; otherwise, <c>false</c>.
		/// </value>
		public bool IsApproved {
			get {
				return isApproved;
			}
			set {
				if (isApproved != value) {
					isApproved = value;
					this.NotifyPropertyChanged(nameof(IsApproved));
				}
			}
		}

	}

}
