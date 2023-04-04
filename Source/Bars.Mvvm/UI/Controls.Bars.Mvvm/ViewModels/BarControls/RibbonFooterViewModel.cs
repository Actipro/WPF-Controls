using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a footer within a ribbon.
	/// </summary>
	public class RibbonFooterViewModel : ObservableObjectBase {

		private object content;
		private DataTemplate contentTemplate;
		private DataTemplateSelector contentTemplateSelector = new RibbonFooterContentTemplateSelector();
		private RibbonFooterKind kind;
		private Thickness padding = new Thickness(10, 5, 10, 5);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the content to display within the ribbon footer.
		/// </summary>
		/// <value>The content to display within the ribbon footer.</value>
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
		/// Gets or sets the <see cref="DataTemplate"/> used to display the content.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> used to display the content.</value>
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
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the content.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplateSelector"/> used to display the content.</value>
		public DataTemplateSelector ContentTemplateSelector {
			get => contentTemplateSelector;
			set {
				if (contentTemplateSelector != value) {
					contentTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(ContentTemplateSelector));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonFooterKind"/> indicating the kind of footer, which determines its appearance.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonFooterKind"/> indicating the kind of footer, which determines its appearance.
		/// The default value is <see cref="RibbonFooterKind.Default"/>.
		/// </value>
		public RibbonFooterKind Kind {
			get => kind;
			set {
				if (kind != value) {
					kind = value;
					this.NotifyPropertyChanged(nameof(Kind));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the padding inside the control.
		/// </summary>
		/// <value>
		/// The padding inside the control.
		/// The default value is <c>10,5</c>.
		/// </value>
		public Thickness Padding {
			get => padding;
			set {
				if (padding != value) {
					padding = value;
					this.NotifyPropertyChanged(nameof(Padding));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Content='{this.Content}']";
		}
		
	}

}
