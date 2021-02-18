using System.Windows.Documents;
using System.Windows.Markup;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.Demo.Printing {

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

			// Create a FixedDocument containing bar codes defined in a FixedPage XAML resource of this sample
			FixedPage fixedPage = this.FindResource("SampleFixedPage") as FixedPage;
			if (fixedPage != null) {
				FixedDocument document = new FixedDocument();
				PageContent pageContent = new PageContent();
				((IAddChild)pageContent).AddChild(fixedPage);
				document.Pages.Add(pageContent);
				docViewer.Document = document;
			}
		}
		
	}

}