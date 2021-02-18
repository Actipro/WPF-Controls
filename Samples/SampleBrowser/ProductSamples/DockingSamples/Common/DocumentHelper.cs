using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Docking;
using Microsoft.Win32;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	/// <summary>
	/// Provides common code for working with documents in the various samples.
	/// </summary>
	public static class DocumentHelper {
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a new text <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
		/// <param name="filename">The filename to open; <c>null</c> to create a new document.</param>
		/// <param name="documentIndex">The document index, if a new document is being created.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		private static DocumentWindow CreateTextDocumentWindow(DockSite dockSite, string filename, int documentIndex) {
			if (dockSite == null)
				throw new ArgumentNullException("dockSite");

			// Create a TextBox
			var textBox = new TextBox();
			textBox.BorderThickness = new Thickness(0);
			textBox.TextWrapping = TextWrapping.Wrap;
			textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

			string name, title;
			if (filename != null) {
				// Open an existing document
				textBox.Text = File.ReadAllText(filename);
				name = System.IO.Path.GetFileNameWithoutExtension(filename);
				title = System.IO.Path.GetFileName(filename);
			}
			else {
				// Create a new document
				textBox.Text = String.Format("Document {0} created at {1}.", documentIndex, DateTime.Now);
				name = String.Format("Document{0}", documentIndex);
				title = String.Format("Document{0}.txt", documentIndex);
				filename = title;
			}

			// Create the document
			var documentWindow = new DocumentWindow(dockSite, name, title, 
				new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)), textBox);
			documentWindow.Description = "Text document";
			documentWindow.FileName = filename;

			// Activate the document
			documentWindow.Activate();

			return documentWindow;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a new text <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
		/// <param name="documentIndex">The document index.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		public static DocumentWindow CreateTextDocumentWindow(DockSite dockSite, int documentIndex) {
			return CreateTextDocumentWindow(dockSite, null, documentIndex);
		}

		/// <summary>
		/// Shows an open file dialog and creates a <see cref="DocumentWindow"/> when a file is picked.
		/// </summary>
		/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created, if any.</returns>
		public static DocumentWindow OpenTextDocumentWindow(DockSite dockSite) {
			// Show a file open dialog
			var dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Create a document window
				return DocumentHelper.CreateTextDocumentWindow(dockSite, dialog.FileName, 0);
			}

			return null;
		}
		
	}

}
