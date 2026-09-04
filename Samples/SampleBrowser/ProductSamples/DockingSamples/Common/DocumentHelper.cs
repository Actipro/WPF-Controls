using ActiproSoftware.Windows.Controls.Docking;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

/// <summary>
/// Provides common code for working with documents in the various samples.
/// </summary>
public static class DocumentHelper {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new text <see cref="DocumentWindow"/>.
	/// </summary>
	/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
	/// <param name="filename">The filename to open; <c>null</c> to create a new document.</param>
	/// <param name="documentIndex">The document index, if a new document is being created.</param>
	private static DocumentWindow CreateTextDocumentWindow(DockSite dockSite, string? filename, int documentIndex) {
		if (dockSite is null)
			throw new ArgumentNullException(nameof(dockSite));

		// Create a TextBox
		var textBox = new TextBox {
			BorderThickness = new Thickness(0),
			TextWrapping = TextWrapping.Wrap,
			VerticalScrollBarVisibility = ScrollBarVisibility.Auto
		};

		string name;
		string title;
		if (filename is not null) {
			// Open an existing document
			textBox.Text = File.ReadAllText(filename);
			name = Path.GetFileNameWithoutExtension(filename);
			title = Path.GetFileName(filename);
		}
		else {
			// Create a new document
			textBox.Text = string.Format("Document {0} created at {1}.", documentIndex, DateTime.Now);
			name = string.Format("Document{0}", documentIndex);
			title = string.Format("Document{0}.txt", documentIndex);
			filename = title;
		}

		// Create the document
		var documentWindow = new DocumentWindow(dockSite, name, title,
			new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)), textBox) {
			Description = "Text document",
			FileName = filename
		};

		// Activate the document
		documentWindow.Activate();

		return documentWindow;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new text <see cref="DocumentWindow"/>.
	/// </summary>
	/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
	/// <param name="documentIndex">The document index.</param>
	public static DocumentWindow CreateTextDocumentWindow(DockSite dockSite, int documentIndex)
		=> CreateTextDocumentWindow(dockSite, filename: null, documentIndex);

	/// <summary>
	/// Shows an open file dialog and creates a <see cref="DocumentWindow"/> when a file is picked.
	/// </summary>
	/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
	/// <returns>The <see cref="DocumentWindow"/> that was created, if any.</returns>
	public static DocumentWindow? OpenTextDocumentWindow(DockSite dockSite) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Create a document window
			return CreateTextDocumentWindow(dockSite, dialog.FileName, documentIndex: 0);
		}

		return null;
	}

}
