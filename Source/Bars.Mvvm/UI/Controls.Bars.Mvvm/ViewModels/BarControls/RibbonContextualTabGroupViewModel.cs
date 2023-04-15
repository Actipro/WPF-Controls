namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a contextual tab group control within a ribbon.
	/// </summary>
	public class RibbonContextualTabGroupViewModel : BarKeyedObjectViewModelBase {

		private bool isActive;
		private string label;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonContextualTabGroupViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public RibbonContextualTabGroupViewModel(string key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public RibbonContextualTabGroupViewModel(string key, string label)
			: base(key) {

			this.label = label ?? LabelGenerator.FromKey(key);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether the contextual tab group is active and showing its tabs.
		/// </summary>
		/// <value>
		/// <c>true</c> if the contextual tab group is active and showing its tabs; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsActive {
			get {
				return isActive;
			}
			set {
				if (isActive != value) {
					isActive = value;
					this.NotifyPropertyChanged(nameof(IsActive));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string Label {
			get {
				return label;
			}
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}

	}

}
