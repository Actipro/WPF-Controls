using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrintingToXps {

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
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSaveToFileButtonClick(object sender, RoutedEventArgs e) {
			// Show a save dialog
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.CheckPathExists = true;
			dialog.Title = "Save XPS Document";
			dialog.FileName = "SyntaxEditorDocument.xps";
			dialog.Filter = "XPS files (*.xps)|*.xps";
			dialog.OverwritePrompt = true;
			if (dialog.ShowDialog() == true) {
				// Write the document to an XPS file
				FixedDocument document = editor.PrintSettings.CreateFixedDocument(editor);
				XpsDocument xpsd = new XpsDocument(dialog.FileName, FileAccess.Write);
				XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
				xw.Write(document);
				xpsd.Close();
			}
		}

    }

}