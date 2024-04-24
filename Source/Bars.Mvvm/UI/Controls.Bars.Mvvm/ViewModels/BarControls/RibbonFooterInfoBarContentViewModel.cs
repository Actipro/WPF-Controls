using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for image and text content within a ribbon footer.
	/// </summary>
	public class RibbonFooterInfoBarContentViewModel : ObservableObjectBase {

		private object action;
		private DataTemplate actionTemplate;
		private DataTemplateSelector actionTemplateSelector;
		private bool canClose = true;
		private object content;
		private DataTemplate contentTemplate;
		private DataTemplateSelector contentTemplateSelector;
		private ImageSource iconSource;
		private bool isIconVisible = true;
		private string message;
		private Thickness padding = new Thickness(10, 5, 10, 5);
		private InfoBarSeverity severity = InfoBarSeverity.Information;
		private string title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the optional action to be displayed in the info bar.
		/// </summary>
		/// <value>The action.</value>
		public object Action {
			get => action;
			set => SetProperty(ref action, value);
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> used to display the <see cref="Action"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> used to display the <see cref="Action"/>.</value>
		public DataTemplate ActionTemplate {
			get => actionTemplate;
			set => SetProperty(ref actionTemplate, value);
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the <see cref="Action"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the <see cref="Action"/>.</value>
		public DataTemplateSelector ActionTemplateSelector {
			get => actionTemplateSelector;
			set => SetProperty(ref actionTemplateSelector, value);
		}

		/// <summary>
		/// Gets or sets whether the info bar can be closed by the user.
		/// </summary>
		/// <value>
		/// <c>true</c> if the info bar can be closed; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanClose {
			get => canClose;
			set => SetProperty(ref canClose, value);
		}

		/// <summary>
		/// Gets or sets the <see cref="ImageSource"/> that defines the icon.
		/// </summary>
		/// <value>The <see cref="ImageSource"/> that defines the icon.</value>
		public ImageSource IconSource {
			get => iconSource;
			set => SetProperty(ref iconSource, value);
		}

		/// <summary>
		/// Gets or sets the optional content to be displayed in the info bar.
		/// </summary>
		/// <value>The content.</value>
		public object Content {
			get => content;
			set => SetProperty(ref content, value);
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> used to display the <see cref="Content"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> used to display the <see cref="Content"/>.</value>
		public DataTemplate ContentTemplate {
			get => contentTemplate;
			set => SetProperty(ref contentTemplate, value);
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the <see cref="Content"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="DataTemplate"/> used to display the <see cref="Content"/>.</value>
		public DataTemplateSelector ContentTemplateSelector {
			get => contentTemplateSelector;
			set => SetProperty(ref contentTemplateSelector, value);
		}

		/// <summary>
		/// Gets or sets whether the icon is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the icon should be visible; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsIconVisible {
			get => isIconVisible;
			set => SetProperty(ref isIconVisible, value);
		}

		/// <summary>
		/// Gets or sets the message content.
		/// </summary>
		/// <value>The message content.</value>
		public string Message {
			get => message;
			set => SetProperty(ref message, value);
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
			set => SetProperty(ref padding, value);
		}

		/// <summary>
		/// Gets of sets the severity of the info bar.
		/// </summary>
		/// <value>
		/// One of the <see cref="InfoBarSeverity"/> values.
		/// The default value is <see cref="InfoBarSeverity.Information"/>.
		/// </value>
		public InfoBarSeverity Severity {
			get => severity;
			set => SetProperty(ref severity, value);
		}

		/// <summary>
		/// Gets or sets the title content.
		/// </summary>
		/// <value>The title content.</value>
		public string Title {
			get => title;
			set => SetProperty(ref title, value);
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Title='{this.Title}']";
		}
		
	}

}
