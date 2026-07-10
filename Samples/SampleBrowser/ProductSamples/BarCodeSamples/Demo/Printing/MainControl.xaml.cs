using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.Demo.Printing;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Create a FixedDocument containing bar codes defined in a FixedPage XAML resource of this sample
		if (FindResource("SampleFixedPage") is FixedPage fixedPage) {
			var pageContent = new PageContent();
			((IAddChild)pageContent).AddChild(fixedPage);

			var document = new FixedDocument();
			document.Pages.Add(pageContent);

			docViewer.Document = document;
		}
	}

}
