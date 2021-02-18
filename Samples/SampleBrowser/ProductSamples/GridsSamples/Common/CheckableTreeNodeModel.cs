namespace ActiproSoftware.ProductSamples.GridsSamples.Common {
	
	/// <summary>
	/// Provides a common implementation of a tree node model that supports checking.
	/// </summary>
	public class CheckableTreeNodeModel : TreeNodeModel {
		
		private bool isCheckable;
		private bool? isChecked = false;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the node is checkable.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is checkable; otherwise, <c>false</c>.
		/// </value>
		public bool IsCheckable {
			get {
				return isCheckable;
			}
			set {
				if (isCheckable == value)
					return;

				isCheckable = value;
				this.NotifyPropertyChanged("IsCheckable");
			}
		}

		/// <summary>
		/// Gets or sets whether the node is checked.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is checked; otherwise, <c>false</c>.
		/// </value>
		public bool? IsChecked {
			get {
				return isChecked;
			}
			set {
				if (isChecked == value)
					return;

				isChecked = value;
				this.NotifyPropertyChanged("IsChecked");
			}
		}
		
	}

}
