namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item within a bar gallery control.
	/// </summary>
	public abstract class BarGalleryItemViewModelBase : ObservableObjectBase {
		
		private string category;
		private string description;
		private string keyTipText;
		private string label;
		private BarGalleryItemLayoutBehavior layoutBehavior = BarGalleryItemLayoutBehavior.Default;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with the specified category.
		/// </summary>
		/// <param name="category">The item's category.</param>
		protected BarGalleryItemViewModelBase(string category) {
			this.category = category;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the item's category.
		/// </summary>
		/// <value>The item's category.</value>
		public string Category {
			get => category;
			set {
				if (category != value) {
					category = value;
					this.NotifyPropertyChanged(nameof(Category));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string Description {
			get => description;
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged(nameof(Description));
				}
			}
		}

		/// <summary>
		/// Gets whether the <see cref="Label"/> is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Label"/> is visible; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsLabelVisible
			=> layoutBehavior == BarGalleryItemLayoutBehavior.MenuItem;
		
		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string KeyTipText {
			get => keyTipText;
			set {
				if (keyTipText != value) {
					keyTipText = value;
					this.NotifyPropertyChanged(nameof(KeyTipText));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public virtual string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="BarGalleryItemLayoutBehavior"/> indicating how the gallery item should be visually displayed.
		/// </summary>
		/// <value>
		/// A <see cref="BarGalleryItemLayoutBehavior"/> indicating how the gallery item should be visually displayed.
		/// The default value is <see cref="BarGalleryItemLayoutBehavior.Default"/>.
		/// </value>
		public BarGalleryItemLayoutBehavior LayoutBehavior {
			get => layoutBehavior;
			set {
				if (layoutBehavior != value) {
					layoutBehavior = value;
					this.NotifyPropertyChanged(nameof(LayoutBehavior));
					this.NotifyPropertyChanged(nameof(IsLabelVisible));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Label='{this.Label}']";
		}

	}

}
