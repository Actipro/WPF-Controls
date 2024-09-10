using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a multi-row control group control within a ribbon group.
	/// </summary>
	public class RibbonMultiRowControlGroupViewModel : ObservableObjectBase {

		private bool isVisible = true;
		private Int32Collection threeRowItemSortOrder;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => isVisible;
			set {
				if (isVisible != value) {
					isVisible = value;
					this.NotifyPropertyChanged(nameof(IsVisible));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// Gets or sets a collection of integers that indicates the indices of how items should be sorted when in a three-row layout.
		/// </summary>
		/// <value>A collection of integers that indicates the indices of how items should be sorted when in a three-row layout.</value>
		public Int32Collection ThreeRowItemSortOrder {
			get {
				return threeRowItemSortOrder;
			}
			set {
				if (threeRowItemSortOrder != value) {
					threeRowItemSortOrder = value;
					this.NotifyPropertyChanged(nameof(ThreeRowItemSortOrder));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.Items.Count} item(s)']";
		}

	}

}
