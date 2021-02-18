using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Text.Exporters;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.Exporting {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Select the first exporter
			exporterComboBox.SelectedIndex = 0;

			// Export
			this.Export();
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Updates the preview of exported text.
		/// </summary>
		private void Export() {
			TextExporterFactory exporterFactory = new TextExporterFactory(editor.HighlightingStyleRegistry);
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
				this.LoadRtf(exportEditor.Document.CurrentSnapshot.Text);
			}
			else {
				exportEditor.Document.Language = SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Html.langdef");
				this.LoadRtf("No RTF preview available since this is an HTML export.");
			}
		}

		/// <summary>
		/// Loads RTF in the previewer.
		/// </summary>
		/// <param name="rtf">The RTF to load.</param>
		private void LoadRtf(string rtf) {
			if (string.IsNullOrEmpty(rtf))
				return;

			TextRange textRange = new TextRange(previewRichTextBox.Document.ContentStart, previewRichTextBox.Document.ContentEnd);

			using (MemoryStream stream = new MemoryStream()) {
				using (StreamWriter writer = new StreamWriter(stream)) {
					writer.Write(rtf);
					writer.Flush();
					stream.Seek(0, SeekOrigin.Begin);

					textRange.Load(stream, DataFormats.Rtf);
				}
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnExportButtonClick(object sender, RoutedEventArgs e) {
			this.Export();
		}

    }

}