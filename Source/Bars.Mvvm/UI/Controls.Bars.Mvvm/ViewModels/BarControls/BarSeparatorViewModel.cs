namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a separator control within a bar control.
	/// </summary>
	public class BarSeparatorViewModel : ObservableObjectBase, IHasTag {

		private bool isVisible = true;
		private object tag;

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

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object Tag {
			get => tag;
			set {
				if (tag != value) {
					tag = value;
					this.NotifyPropertyChanged(nameof(Tag));
				}
			}
		}

	}

}
