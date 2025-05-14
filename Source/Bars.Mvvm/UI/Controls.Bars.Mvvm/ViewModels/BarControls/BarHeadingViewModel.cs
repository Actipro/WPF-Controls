namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a heading control within a bar control.
	/// </summary>
	public class BarHeadingViewModel : ObservableObjectBase, IHasTag {

		private bool isVisible = true;
		private string label;
		private object tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public BarHeadingViewModel()  // Parameterless constructor required for XAML support
			: this(label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified label.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		public BarHeadingViewModel(string label) {
			this.label = label;
		}

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

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
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
