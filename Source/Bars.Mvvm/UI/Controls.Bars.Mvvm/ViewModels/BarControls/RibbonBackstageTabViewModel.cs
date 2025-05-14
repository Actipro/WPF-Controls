using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a tab control within a ribbon backstage.
	/// </summary>
	public class RibbonBackstageTabViewModel : BarKeyedObjectViewModelBase {

		private object content;
		private DataTemplate contentTemplate;
		private DataTemplateSelector contentTemplateSelector;
		private string description;
		private RibbonBackstageHeaderAlignment headerAlignment = RibbonBackstageHeaderAlignment.Top;
		private bool isVisible = true;
		private string keyTipText;
		private string label;
		private ImageSource largeImageSource;
		private ImageSource smallImageSource;
		private string title;
		private VariantSize variantSize = VariantSize.Medium;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public RibbonBackstageTabViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public RibbonBackstageTabViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public RibbonBackstageTabViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public RibbonBackstageTabViewModel(string key, string label, string keyTipText)
			: base(key) {

			this.label = label ?? BarControlService.LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromLabel(this.label);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The content for the tab's content area, which can be a UI control, a data object, or even this view model instance.
		/// </summary>
		public object Content {
			get => content;
			set {
				if (content != value) {
					content = value;
					this.NotifyPropertyChanged(nameof(Content));
				}
			}
		}

		/// <summary>
		/// The <see cref="DataTemplate"/> for the tab's <see cref="Content"/>.
		/// </summary>
		public DataTemplate ContentTemplate {
			get => contentTemplate;
			set {
				if (contentTemplate != value) {
					contentTemplate = value;
					this.NotifyPropertyChanged(nameof(ContentTemplate));
				}
			} 
		}

		/// <summary>
		/// The <see cref="DataTemplateSelector"/> for the tab's <see cref="Content"/>.
		/// </summary>
		public DataTemplateSelector ContentTemplateSelector {
			get => contentTemplateSelector;
			set {
				if (contentTemplateSelector != value) {
					contentTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(ContentTemplateSelector));
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
		/// Gets or sets a <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// The default value is <see cref="RibbonBackstageHeaderAlignment.Top"/>.
		/// </value>
		public RibbonBackstageHeaderAlignment HeaderAlignment {
			get => headerAlignment;
			set {
				if (headerAlignment != value) {
					headerAlignment = value;
					this.NotifyPropertyChanged(nameof(HeaderAlignment));
				}
			}
		}

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
		public string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.LargeImageSource"/>
		public ImageSource LargeImageSource {
			get => largeImageSource;
			set {
				if (largeImageSource != value) {
					largeImageSource = value;
					this.NotifyPropertyChanged(nameof(LargeImageSource));
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

		/// <summary>
		/// Gets or sets the variant size of the tab.
		/// </summary>
		/// <value>
		/// One of the <see cref="Bars.VariantSize"/> value.
		/// The default value is <see cref="VariantSize.Medium"/>.
		/// </value>
		public VariantSize VariantSize {
			get => variantSize;
			set {
				if (variantSize != value) {
					variantSize = value;
					this.NotifyPropertyChanged(nameof(VariantSize));
				}
			}
		}

	}

}
