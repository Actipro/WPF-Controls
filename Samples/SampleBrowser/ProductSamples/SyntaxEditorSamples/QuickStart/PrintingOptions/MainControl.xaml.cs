using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrintingOptions;

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

		editor.ColumnGuides!.Add(80);

		Loaded += new RoutedEventHandler(OnLoaded);
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
	private void RefreshPrintPreview() {
		if (editor.PrintSettings is { } printSettings) {
			printSettings.DocumentTitle = documentTitleTextBox.Text;
			printSettings.IsDocumentTitleMarginVisible = (isDocumentTitleMarginVisibleCheckBox.IsChecked == true);
			printSettings.IsLineNumberMarginVisible = (isLineNumberMarginVisibleCheckBox.IsChecked == true);
			printSettings.IsPageNumberMarginVisible = (isPageNumberMarginVisibleCheckBox.IsChecked == true);
			printSettings.IsSyntaxHighlightingEnabled = (isSyntaxHighlightingEnabledCheckBox.IsChecked == true);
			printSettings.IsWordWrapGlyphMarginVisible = (isWordWrapGlyphMarginVisibleCheckBox.IsChecked == true);
			printSettings.IsWhitespaceVisible = (isWhitespaceVisibleCheckBox.IsChecked == true);
			printSettings.AreCollapsedOutliningNodesAllowed = (areCollapsedOutliningNodesAllowedCheckBox.IsChecked == true);
			printSettings.AreColumnGuidesVisible = (areColumnGuidesVisibleCheckBox.IsChecked == true);
			printSettings.AreIndentationGuidesVisible = (areIndentationGuidesVisibleCheckBox.IsChecked == true);
			printSettings.AreSquiggleLinesVisible = (areSquiggleLinesVisibleCheckBox.IsChecked == true);

			documentViewer.Document = printSettings.CreateFixedDocument(editor);
		}
	}

}
