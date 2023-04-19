using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a tab control within a ribbon.
	/// </summary>
	public class RibbonTabViewModel : BarKeyedObjectViewModelBase {

		private string contextualTabGroupKey;
		private VariantCollection controlVariants;
		private string description;
		private VariantCollection groupVariants;
		private string keyTipText;
		private string label;
		private string title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public RibbonTabViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public RibbonTabViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public RibbonTabViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public RibbonTabViewModel(string key, string label, string keyTipText)
			: base(key) {
			this.label = label ?? LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? KeyTipTextGenerator.FromLabel(this.label);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the string key of the related contextual tab group, if this should be a contextual tab.
		/// </summary>
		/// <value>The string key of the related contextual tab group, if this should be a contextual tab.</value>
		public string ContextualTabGroupKey {
			get {
				return contextualTabGroupKey;
			}
			set {
				if (contextualTabGroupKey != value) {
					contextualTabGroupKey = value;
					this.NotifyPropertyChanged(nameof(ContextualTabGroupKey));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the collection of variant size transitions to apply to all controls within the tab when the ribbon is in Simplified layout mode.
		/// </summary>
		/// <value>The collection of variant size transitions to apply to all controls within the tab when the ribbon is in Simplified layout mode.</value>
		public VariantCollection ControlVariants {
			get {
				return controlVariants;
			}
			set {
				if (controlVariants != value) {
					controlVariants = value;
					this.NotifyPropertyChanged(nameof(ControlVariants));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string Description {
			get {
				return description;
			}
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged(nameof(Description));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of group view models within the tab.
		/// </summary>
		/// <value>The collection of group view models within the tab.</value>
		public ObservableCollection<RibbonGroupViewModel> Groups { get; } = new ObservableCollection<RibbonGroupViewModel>();

		/// <summary>
		/// Gets or sets the collection of variant size transitions to apply to all groups within the tab when the ribbon is in Classic layout mode.
		/// </summary>
		/// <value>The collection of variant size transitions to apply to all groups within the tab when the ribbon is in Classic layout mode.</value>
		public VariantCollection GroupVariants {
			get {
				return groupVariants;
			}
			set {
				if (groupVariants != value) {
					groupVariants = value;
					this.NotifyPropertyChanged(nameof(GroupVariants));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string KeyTipText {
			get {
				return keyTipText;
			}
			set {
				if (keyTipText != value) {
					keyTipText = value;
					this.NotifyPropertyChanged(nameof(KeyTipText));
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
		
		/// <inheritdoc cref="BarButtonViewModel.Title"/>
		public string Title {
			get => title;
			set {
				if (title != value) {
					title = value;
					this.NotifyPropertyChanged(nameof(Title));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Label='{this.Label}']";
		}

	}

}
