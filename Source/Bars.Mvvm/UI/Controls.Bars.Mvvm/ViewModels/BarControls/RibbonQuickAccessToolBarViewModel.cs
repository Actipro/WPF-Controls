using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a quick-access toolbar control within a ribbon.
	/// </summary>
	public class RibbonQuickAccessToolBarViewModel : ObservableObjectBase {

		private bool isCustomizeButtonVisible = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the collection of common items that should appear in the QAT's Customize menu, from which they can be toggled into and out of <see cref="Items"/>.
		/// </summary>
		/// <value>The collection of common items that should appear in the QAT's Customize menu.</value>
		public ObservableCollection<object> CommonItems { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// Gets or sets whether the customize button is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the customize button is visible; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsCustomizeButtonVisible {
			get {
				return isCustomizeButtonVisible;
			}
			set {
				if (isCustomizeButtonVisible != value) {
					isCustomizeButtonVisible = value;
					this.NotifyPropertyChanged(nameof(IsCustomizeButtonVisible));
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
