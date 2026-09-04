using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Exporters;
using ActiproSoftware.Text.Implementation;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.Exporting;

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

		// Select the first exporter
		exporterComboBox.SelectedIndex = 0;

		// Export
		Export();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Updates the preview of exported text.
	/// </summary>
	private void Export() {
		var exporterFactory = new TextExporterFactory(editor.HighlightingStyleRegistry);
		switch (exporterComboBox.SelectedIndex) {
			case 0:
				exportEditor.Document.SetText(editor.Document.CurrentSnapshot.Export(exporterFactory.CreateRtf()));
				break;
			case 1:
				exportEditor.Document.SetText(editor.Document.CurrentSnapshot.Export(exporterFactory.CreateHtmlClassBased()));
				break;
			case 2:
				exportEditor.Document.SetText(editor.Document.CurrentSnapshot.Export(exporterFactory.CreateHtmlInline()));
				break;
			case 3:
				exportEditor.Document.SetText(editor.Document.CurrentSnapshot.Export(exporterFactory.CreateHtmlInlineFragment()));
				break;
		}

		if (exporterComboBox.SelectedIndex == 0) {
			exportEditor.Document.Language = SyntaxLanguage.PlainText;
			LoadRtf(exportEditor.Document.CurrentSnapshot.Text);
		}
		else {
			exportEditor.Document.Language = SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Html.langdef");
			LoadRtf("No RTF preview available since this is an HTML export.");
		}
	}

	/// <summary>
	/// Loads RTF in the previewer.
	/// </summary>
	/// <param name="rtf">The RTF to load.</param>
	private void LoadRtf(string rtf) {
		if (string.IsNullOrEmpty(rtf))
			return;

		var textRange = new TextRange(previewRichTextBox.Document.ContentStart, previewRichTextBox.Document.ContentEnd);

		using (var stream = new MemoryStream()) {
			using (var writer = new StreamWriter(stream)) {
				writer.Write(rtf);
				writer.Flush();
				stream.Seek(0, SeekOrigin.Begin);

				textRange.Load(stream, DataFormats.Rtf);
			}
		}
	}

	private void OnExportButtonClick(object sender, RoutedEventArgs e)
		=> Export();

}
