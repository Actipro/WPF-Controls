namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers {

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

	}
}
