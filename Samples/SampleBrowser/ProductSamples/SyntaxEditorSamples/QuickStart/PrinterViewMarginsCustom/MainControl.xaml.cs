using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrinterViewMarginsCustom;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLoaded(object sender, RoutedEventArgs e) {
		Dispatcher.BeginInvoke(DispatcherPriority.Send, () => {
			RefreshPrintPreview();
		});
	}

	private void OnRefreshPrintPreviewButtonClick(object sender, RoutedEventArgs e)
		=> RefreshPrintPreview();

	private void OnShowPrintPreviewDialogButtonClick(object sender, RoutedEventArgs e)
		=> editor.ShowPrintPreviewDialog();

	/// <summary>
	/// Refreshes the print preview.
	/// </summary>
	private void RefreshPrintPreview()
		=> documentViewer.Document = editor.PrintSettings?.CreateFixedDocument(editor);


}
