using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.Win32;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Languages.Xml;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.WebAddonHtmlEditor {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		private bool hasPendingParseData;
		private ISearchResultSet lastResultSet;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			
			//
			// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
			//   since it tells you how to set up an ambient parse request dispatcher within your 
			//   application OnStartup code, and add related cleanup in your application OnExit code.  
			//   These steps are essential to having the add-on perform well.
			//
			
			// Register the schema resolver service with the XML language (needed to support IntelliPrompt)
			XmlSchemaResolver resolver = new XmlSchemaResolver();
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.xsd")) {
				resolver.AddSchemaFromStream(stream);
			}

			// Xml.xsd is also required for Xhtml.xsd
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
				resolver.AddSchemaFromStream(stream);
			}

			syntaxEditor.Document.Language.RegisterXmlSchemaResolver(resolver);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a search operation occurs in a view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorViewSearchEventArgs"/> that contains the event data.</param>
		private void OnEditorViewSearch(object sender, EditorViewSearchEventArgs e) {
			this.UpdateResults(e.ResultSet);
		}

		/// <summary>
		/// Occurs when a mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
			ListBox listBox = (ListBox)sender;
			IParseError error = listBox.SelectedItem as IParseError;
			if (error != null) {
				syntaxEditor.ActiveView.Selection.StartPosition = error.PositionRange.StartPosition;
				syntaxEditor.Focus();
			}
		}
		
		/// <summary>
		/// Occurs when the mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnFindResultsTextBoxDoubleClick(object sender, MouseButtonEventArgs e) {
			// Quit if there is not result set stored yet
			if (lastResultSet == null)
				return;

			int charIndex = findResultsTextBox.GetCharacterIndexFromPoint(e.GetPosition(findResultsTextBox), true);
			int lineIndex = findResultsTextBox.GetLineIndexFromCharacterIndex(charIndex);

			int resultIndex = lineIndex - 1;  // Account for first line in results displaying search info
			if ((resultIndex >= 0) && (resultIndex < lastResultSet.Results.Count)) {
				// A valid result was clicked
				ISearchResult result = lastResultSet.Results[resultIndex];
				if (result.ReplaceSnapshotRange.IsDeleted) {
					// Find result
					syntaxEditor.ActiveView.Selection.SelectRange(result.FindSnapshotRange.TranslateTo(syntaxEditor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default).TextRange);
				}
				else {
					// Replace result
					syntaxEditor.ActiveView.Selection.SelectRange(result.ReplaceSnapshotRange.TranslateTo(syntaxEditor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default).TextRange);
				}

				// Focus the editor
				syntaxEditor.Focus();
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnNewButtonClick(object sender, RoutedEventArgs e) {
			syntaxEditor.Document.SetText(null);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenButtonClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "XHTML files (*.html;*.xhtml)|*.html;*.xhtml|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Open a document 
				if (BrowserInteropHelper.IsBrowserHosted) {
					// Use dialog to help open the file because of security restrictions
					using (Stream stream = dialog.OpenFile()) {
						// Read the file
						syntaxEditor.Document.LoadFile(stream, Encoding.UTF8);
					}
				}
				else {
					// Security is not an issue in a Windows app so use simple method
					syntaxEditor.Document.LoadFile(dialog.FileName);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the document's parse data has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to this event.</param>
		private void OnSyntaxEditorDocumentParseDataChanged(object sender, EventArgs e) {
			//
			// NOTE: The parse data here is generated in a worker thread... this event handler is called 
			//         back in the UI thread immediately when the worker thread completes... it is best
			//         practice to delay UI updates until the end user stops typing... we will flag that
			//         there is a pending parse data change, which will be handled in the 
			//         UserInterfaceUpdate event
			//

			hasPendingParseData = true;
		}
		
		/// <summary>
		/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnSyntaxEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
			// If there is a pending parse data change...
			if (hasPendingParseData) {
				// Clear flag
				hasPendingParseData = false;

				XmlParseData parseData = syntaxEditor.Document.ParseData as XmlParseData;
				if (parseData != null) {
					// Output errors
					errorListView.ItemsSource = parseData.Errors;

					// Show well-formed state
					messagePanel.Content = String.Format("Well-formed: {0}", parseData.IsWellFormed ? "Yes" : "No");
				}
				else {
					// Clear UI
					errorListView.ItemsSource = null;
					messagePanel.Content = "Ready";
				}
			}
		}

		/// <summary>
		/// Occurs when the document's view selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> that contains data related to this event.</param>
		private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			// Update line, col, and character display
			linePanel.Text = String.Format("Ln {0}", e.CaretPosition.DisplayLine);
			columnPanel.Text = String.Format("Col {0}", e.CaretDisplayCharacterColumn);
			characterPanel.Text = String.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
		}
		
		/// <summary>
		/// Updates the results.
		/// </summary>
		/// <param name="resultSet">The <see cref="ISearchResultSet"/> containing results.</param>
		private void UpdateResults(ISearchResultSet resultSet) {
			// Show the results
			findResultsToolWindow.Title = String.Format("Find Results - {0} match{1}", resultSet.Results.Count, (resultSet.Results.Count == 1 ? String.Empty : "es"));
			findResultsTextBox.Text = resultSet.ToString();

			switch (resultSet.OperationType) {
				case SearchOperationType.FindAll:
				case SearchOperationType.ReplaceAll:
					// Activate the find results tool window
					findResultsToolWindow.Activate(false);
					break;
			}

			// Save the result set
			lastResultSet = resultSet;
		}

	}

}