using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.FooterInfoBar {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private bool								canClose		= true;
		private bool								isIconVisible	= true;
		private Thickness							padding			= new Thickness(10, 5, 10, 5);
		private RibbonQuickAccessToolBarLocation	qatLocation		= RibbonQuickAccessToolBarLocation.Below;
		private InfoBarSeverity						severity		= InfoBarSeverity.Success;
		private ICommand							showFooterMvvmCommand;
		private ICommand							showFooterXamlCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if the info bar can be closed.
		/// </summary>
		/// <value><c>true</c> if the info bar can be closed; otherwise <c>false</c>.</value>
		public bool CanClose {
			get => canClose;
			set => SetProperty(ref canClose, value);
		}

		/// <summary>
		/// Gets or sets if the info bar icon is visible.
		/// </summary>
		/// <value><c>true</c> if the info bar icon is visible; otherwise <c>false</c>.</value>
		public bool IsIconVisible {
			get => isIconVisible;
			set => SetProperty(ref isIconVisible, value);
		}

		/// <summary>
		/// Gets or sets the padding for the info bar.
		/// </summary>
		/// <value>A <see cref="Thickness"/> value.</value>
		public Thickness Padding {
			get => padding;
			set => SetProperty(ref padding, value);
		}

		/// <summary>
		/// Gets or sets the location of the Quick Access Toolbar.
		/// </summary>
		/// <value>One of the <see cref="RibbonQuickAccessToolBarLocation"/> values.</value>
		[DisplayName("QAT location")]
		public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
			get => qatLocation;
			set => SetProperty(ref qatLocation, value);
		}

		/// <summary>
		/// Gets of sets the severity of the info bar.
		/// </summary>
		/// <value>One of the <see cref="InfoBarSeverity"/> values.</value>
		public InfoBarSeverity Severity {
			get => severity;
			set => SetProperty(ref severity, value);
		}

		/// <summary>
		/// Gets or sets the command that will be executed to show the MVVM-based footer.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ShowFooterMvvmCommand {
			get => showFooterMvvmCommand;
			set => SetProperty(ref showFooterMvvmCommand, value);
		}

		/// <summary>
		/// Gets or sets the command that will be executed to show the XAML-based footer.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ShowFooterXamlCommand {
			get => showFooterXamlCommand;
			set => SetProperty(ref showFooterXamlCommand, value);
		}

	}

}
