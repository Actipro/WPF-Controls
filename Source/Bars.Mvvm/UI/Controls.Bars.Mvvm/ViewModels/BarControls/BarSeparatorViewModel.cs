namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a separator control within a bar control.
	/// </summary>
	public class BarSeparatorViewModel : ObservableObjectBase {

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
	}

}
