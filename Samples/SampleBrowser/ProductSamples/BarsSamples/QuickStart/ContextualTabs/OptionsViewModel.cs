using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ContextualTabs {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private bool isPictureToolsContextualTabGroupVisible	= false;
		private bool isTableToolsContextualTabGroupVisible		= true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if the Picture Tools contextual tab group is visible.
		/// </summary>
		/// <value><c>true</c> if the tab group is visible; otherwise <c>false</c>.</value>
		public bool IsPictureToolsContextualTabGroupVisible {
			get => isPictureToolsContextualTabGroupVisible;
			set {
				if (isPictureToolsContextualTabGroupVisible != value) {
					isPictureToolsContextualTabGroupVisible = value;
					NotifyPropertyChanged(nameof(IsPictureToolsContextualTabGroupVisible));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the Table Tools contextual tab group is visible.
		/// </summary>
		/// <value><c>true</c> if the tab group is visible; otherwise <c>false</c>.</value>
		public bool IsTableToolsContextualTabGroupVisible {
			get => isTableToolsContextualTabGroupVisible;
			set {
				if (isTableToolsContextualTabGroupVisible != value) {
					isTableToolsContextualTabGroupVisible = value;
					NotifyPropertyChanged(nameof(IsTableToolsContextualTabGroupVisible));
				}
			}
		}

	}

}
