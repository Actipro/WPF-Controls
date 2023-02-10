using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Footer {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : SampleControlBase {

		private RibbonFooterViewModel footerViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleMvvmControl"/> class.
		/// </summary>
		public SampleMvvmControl() {
			InitializeComponent();

			// Configure this code-behind to be the view model for this sample
			this.DataContext = this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the view model for the footer.
		/// </summary>
		/// <value>A <see cref="RibbonFooterViewModel"/> value.</value>
		private RibbonFooterViewModel FooterViewModel {
			get {
				if (footerViewModel is null) {
					footerViewModel = new RibbonFooterViewModel() {

						// The RibbonFooterSimpleContentViewModel can be used to define a common footer
						// with an image to the left of a text message
						Content = new RibbonFooterSimpleContentViewModel() {
							Text = "The footer can be set to any content and is great for tips or notifications.",
							ImageSource = ImageLoader.GetIcon("InformationClear16.png"),
						},

						Kind = Options?.FooterKind ?? RibbonFooterKind.Default,
						Padding = Options?.Padding ?? new Thickness(),
					};
				}
				return footerViewModel;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void UpdateFooter() {
			if (Options == null)
				return;

			// Update properties to match the sample options
			FooterViewModel.Padding = Options.Padding;
			FooterViewModel.Kind = Options.FooterKind;

			// A footer is only visible with the RibbonViewModel.Footer property is populated
			Ribbon.Footer = Options.IsFooterVisible
				? FooterViewModel
				: null;
		}

	}

}
