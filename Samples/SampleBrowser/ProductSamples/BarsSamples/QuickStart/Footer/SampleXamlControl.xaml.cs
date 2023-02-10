namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Footer {

	/// <summary>
	/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
	/// </summary>
	public partial class SampleXamlControl : SampleControlBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleXamlControl"/> class.
		/// </summary>
		public SampleXamlControl() {
			InitializeComponent();

			// Configure this code-behind to be the view model for this sample
			this.DataContext = this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void UpdateFooter() {
			if (Options is null)
				return;

			// A footer is only visible with the Ribbon.FooterContent property is populated
			// NOTE: The 'footer' element is defined in XAML
			ribbon.FooterContent = Options.IsFooterVisible
				? footer
				: null;
		}

	}

}
