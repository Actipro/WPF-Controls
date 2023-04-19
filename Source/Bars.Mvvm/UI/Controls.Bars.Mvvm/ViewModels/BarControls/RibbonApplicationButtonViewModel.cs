using ActiproSoftware.Products.Bars.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for an application button control within a ribbon.
	/// </summary>
	[ContentProperty(nameof(MenuItems))]
	public class RibbonApplicationButtonViewModel : ObservableObjectBase {

		private string keyTipText;
		private string label;
		private object menuAdditionalContent;
		private DataTemplate menuAdditionalContentTemplate;
		private DataTemplateSelector menuAdditionalContentTemplateSelector;
		private object menuFooter;
		private DataTemplate menuFooterTemplate;
		private DataTemplateSelector menuFooterTemplateSelector;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonApplicationButtonViewModel()  // Parameterless constructor required for XAML support
			: this(label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified label.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		public RibbonApplicationButtonViewModel(string label)
			: this(label, keyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified label and key tip text.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		public RibbonApplicationButtonViewModel(string label, string keyTipText) {
			this.label = label ?? SR.GetString(SRName.UIApplicationButtonText);
			this.keyTipText = keyTipText ?? KeyTipTextGenerator.FromLabel(this.label);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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
		
		/// <summary>
		/// Gets the additional content that optionally appears on the right side of the menu.
		/// </summary>
		/// <value>The additional content that optionally appears on the right side of the menu.</value>
		public object MenuAdditionalContent {
			get => menuAdditionalContent;
			set {
				if (menuAdditionalContent != value) {
					menuAdditionalContent = value;
					this.NotifyPropertyChanged(nameof(MenuAdditionalContent));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> used to display the menu additional content.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> used to display the menu additional content.</value>
		public DataTemplate MenuAdditionalContentTemplate {
			get => menuAdditionalContentTemplate;
			set {
				if (menuAdditionalContentTemplate != value) {
					menuAdditionalContentTemplate = value;
					this.NotifyPropertyChanged(nameof(MenuAdditionalContentTemplate));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the menu additional content.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplateSelector"/> used to display the menu additional content.</value>
		public DataTemplateSelector MenuAdditionalContentTemplateSelector {
			get => menuAdditionalContentTemplateSelector;
			set {
				if (menuAdditionalContentTemplateSelector != value) {
					menuAdditionalContentTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(MenuAdditionalContentTemplateSelector));
				}
			}
		}
		
		/// <summary>
		/// Gets the footer content that optionally appears at the bottom of the menu.
		/// </summary>
		/// <value>The footer content that optionally appears at the bottom of the menu.</value>
		public object MenuFooter {
			get => menuFooter;
			set {
				if (menuFooter != value) {
					menuFooter = value;
					this.NotifyPropertyChanged(nameof(MenuFooter));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> used to display the menu footer content.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> used to display the menu footer content.</value>
		public DataTemplate MenuFooterTemplate {
			get => menuFooterTemplate;
			set {
				if (menuFooterTemplate != value) {
					menuFooterTemplate = value;
					this.NotifyPropertyChanged(nameof(MenuFooterTemplate));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the menu footer content.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplateSelector"/> used to display the menu footer content.</value>
		public DataTemplateSelector MenuFooterTemplateSelector {
			get => menuFooterTemplateSelector;
			set {
				if (menuFooterTemplateSelector != value) {
					menuFooterTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(MenuFooterTemplateSelector));
				}
			}
		}
				
		/// <summary>
		/// Gets the collection of items that appear within the menu.
		/// </summary>
		/// <value>The collection of items that appear within the menu.</value>
		public ObservableCollection<object> MenuItems { get; } = new ObservableCollection<object>();

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Label='{this.Label}']";
		}

	}

}
