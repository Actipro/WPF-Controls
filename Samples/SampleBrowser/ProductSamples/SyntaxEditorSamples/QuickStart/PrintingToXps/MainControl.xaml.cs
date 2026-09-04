using Microsoft.Win32;
using System.Windows.Xps.Packaging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrintingToXps;

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
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnSaveToFileButtonClick(object sender, RoutedEventArgs e) {
		// Show a save dialog
		var dialog = new SaveFileDialog {
			CheckPathExists = true,
			Title = "Save XPS Document",
			FileName = "SyntaxEditorDocument.xps",
			Filter = "XPS files (*.xps)|*.xps",
			OverwritePrompt = true
		};
		if (dialog.ShowDialog() == true) {
			// Write the document to an XPS file
			var document = editor.PrintSettings?.CreateFixedDocument(editor);
			var xpsd = new XpsDocument(dialog.FileName, FileAccess.Write);
			var xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
			xw.Write(document);
			xpsd.Close();
		}
	}

}
