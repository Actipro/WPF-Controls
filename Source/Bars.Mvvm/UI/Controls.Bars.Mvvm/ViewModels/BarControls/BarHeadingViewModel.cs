namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a heading control within a bar control.
	/// </summary>
	public class BarHeadingViewModel : ObservableObjectBase {

		private string label;

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

	}

}
