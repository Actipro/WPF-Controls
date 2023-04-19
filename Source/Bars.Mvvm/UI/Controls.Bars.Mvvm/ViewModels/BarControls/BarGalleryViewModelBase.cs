using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents an abstract view model base for a gallery control within a bar control.
	/// </summary>
	public abstract class BarGalleryViewModelBase : BarKeyedObjectViewModelBase {

		private bool areSurroundingSeparatorsAllowed = true;
		private bool canCloneToRibbonQuickAccessToolBar = true;
		private ICommand command;
		private Style itemContainerStyle;
		private StyleSelector itemContainerStyleSelector;
		private double itemSpacing;
		private DataTemplate itemTemplate;
		private DataTemplateSelector itemTemplateSelector;
		private string label;
		private double minItemHeight = 16.0;
		private double minItemWidth = 16.0;
		private ImageSource smallImageSource;
		private string title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		protected BarGalleryViewModelBase()  // Parameterless constructor required for XAML support
			: this(key: null) { }
		
		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		protected BarGalleryViewModelBase(string key)
			: base(key) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		protected BarGalleryViewModelBase(string key, string label, ICommand command)
			: base(key) {

			this.label = label ?? LabelGenerator.FromCommand(command) ?? LabelGenerator.FromKey(key);
			this.command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether the menu gallery can render surrounding separators.
		/// </summary>
		/// <value>
		/// <c>true</c> if the menu gallery can render surrounding separators; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool AreSurroundingSeparatorsAllowed {
			get => areSurroundingSeparatorsAllowed;
			set {
				if (areSurroundingSeparatorsAllowed != value) {
					areSurroundingSeparatorsAllowed = value;
					this.NotifyPropertyChanged(nameof(AreSurroundingSeparatorsAllowed));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.CanCloneToRibbonQuickAccessToolBar"/>
		public bool CanCloneToRibbonQuickAccessToolBar {
			get => canCloneToRibbonQuickAccessToolBar;
			set {
				if (canCloneToRibbonQuickAccessToolBar != value) {
					canCloneToRibbonQuickAccessToolBar = value;
					this.NotifyPropertyChanged(nameof(CanCloneToRibbonQuickAccessToolBar));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.Command"/>
		public ICommand Command {
			get => command;
			set {
				if (command != value) {
					command = value;
					this.NotifyPropertyChanged(nameof(Command));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Style"/> to apply to gallery item container elements.
		/// </summary>
		/// <value>The <see cref="Style"/> to apply to gallery item container elements.</value>
		public Style ItemContainerStyle {
			get => itemContainerStyle;
			set {
				if (itemContainerStyle != value) {
					itemContainerStyle = value;
					this.NotifyPropertyChanged(nameof(ItemContainerStyle));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="StyleSelector"/> that picks a <see cref="Style"/> to apply to gallery item container elements.
		/// </summary>
		/// <value>The <see cref="StyleSelector"/> that picks a <see cref="Style"/> to apply to gallery item container elements.</value>
		public StyleSelector ItemContainerStyleSelector {
			get => itemContainerStyleSelector;
			set {
				if (itemContainerStyleSelector != value) {
					itemContainerStyleSelector = value;
					this.NotifyPropertyChanged(nameof(ItemContainerStyleSelector));
				}
			}
		}

		/// <summary>
		/// Gets or sets the amount of spacing between gallery items.
		/// </summary>
		/// <value>
		/// The amount of spacing between gallery items.
		/// The default value is <c>0.0</c>.
		/// </value>
		public double ItemSpacing {
			get => itemSpacing;
			set {
				if (itemSpacing != value) {
					itemSpacing = value;
					this.NotifyPropertyChanged(nameof(ItemSpacing));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> used to display the content for each gallery item.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> used to display the content for each gallery item.</value>
		public DataTemplate ItemTemplate {
			get => itemTemplate;
			set {
				if (itemTemplate != value) {
					itemTemplate = value;
					this.NotifyPropertyChanged(nameof(ItemTemplate));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the content for each gallery item.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplateSelector"/> used to display the content for each gallery item.</value>
		public DataTemplateSelector ItemTemplateSelector {
			get => itemTemplateSelector;
			set {
				if (itemTemplateSelector != value) {
					itemTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(ItemTemplateSelector));
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
		
		/// <summary>
		/// Gets the minimum item height.
		/// </summary>
		/// <value>
		/// The minimum item height.
		/// The default value is <c>16.0</c>.
		/// </value>
		public double MinItemHeight {
			get => minItemHeight;
			set {
				if (minItemHeight != value) {
					minItemHeight = value;
					this.NotifyPropertyChanged(nameof(MinItemHeight));
				}
			}
		}
		
		/// <summary>
		/// Gets the minimum item width.
		/// </summary>
		/// <value>
		/// The minimum item width.
		/// The default value is <c>16.0</c>.
		/// </value>
		public double MinItemWidth {
			get => minItemWidth;
			set {
				if (minItemWidth != value) {
					minItemWidth = value;
					this.NotifyPropertyChanged(nameof(MinItemWidth));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.SmallImageSource"/>
		public ImageSource SmallImageSource {
			get => smallImageSource;
			set {
				if (smallImageSource != value) {
					smallImageSource = value;
					this.NotifyPropertyChanged(nameof(SmallImageSource));
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

	}

}
