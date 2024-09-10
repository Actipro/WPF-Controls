using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a tab row toolbar control within a ribbon.
	/// </summary>
	public class RibbonTabRowToolBarViewModel : ObservableObjectBase {

		private bool isVisible = true;

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
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.Items.Count} items]";
		}

	}

}
