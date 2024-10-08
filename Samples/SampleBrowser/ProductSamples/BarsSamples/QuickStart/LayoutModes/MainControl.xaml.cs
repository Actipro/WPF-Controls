using ActiproSoftware.ProductSamples.BarsSamples.Common;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.LayoutModes {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Don't use a tab row toolbar in this sample
			var viewModel = SampleViewModelFactory.CreateDefaultRichTextEditorRibbonWindowViewModel();
			viewModel.Ribbon.TabRowToolBar = null;
			this.DataContext = viewModel;
		}

	}
}