using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Win32;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningCollapsedText {

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
		private void OnOpenButtonClick(object sender, RoutedEventArgs e) {
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Code files (*.js)|*.js|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Open a document 
				if (BrowserInteropHelper.IsBrowserHosted) {
					// Use dialog to help open the file because of security restrictions
					using (Stream stream = dialog.OpenFile()) {
						// Read the file
						editor.Document.LoadFile(stream, Encoding.UTF8);
					}
				}
				else {
					// Security is not an issue in a Windows app so use simple method
					editor.Document.LoadFile(dialog.FileName);
				}
			}

			// Focus the editor
			editor.Focus();
		}

	}

}