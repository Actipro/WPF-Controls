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
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.WebAddonXmlEditor {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		private int					documentNumber;
		private bool				hasPendingParseData;
		private XmlSchemaResolver	schemaResolver = new XmlSchemaResolver();

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
			xmlEditor.Document.Language.RegisterXmlSchemaResolver(schemaResolver);

			// Initialize
			this.NewFile();
			this.OpenMammalsSchema();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a new XML file.
		/// </summary>
		private void NewFile() {
			this.OpenFile(String.Format("Document{0}.xml", ++documentNumber), null);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCloseSchemaButtonClick(object sender, RoutedEventArgs e) {
			// Clear the schema
			schemaResolver.SchemaSet = null;

			// Set the title
			schemaDocumentWindow.Title = "NoSchema.xsd";

			// Clear the text
			schemaEditor.Document.SetText(null);

			// Queue a new parse since the schema data changed
			xmlEditor.Document.QueueParseRequest();
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
				xmlEditor.ActiveView.Selection.StartPosition = error.PositionRange.StartPosition;
				xmlDocumentWindow.Activate();
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnNewFileButtonClick(object sender, RoutedEventArgs e) {
			this.NewFile();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenFileButtonClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Open a document (use dialog to help open the file because of security restrictions in XBAP/Silverlight)
				using (Stream stream = dialog.OpenFile()) {
					// Read the file
					this.OpenFile(Path.GetFileName(dialog.FileName), stream);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenSchemaButtonClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "XSD files (*.xsd)|*.xsd|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Open a document (use dialog to help open the file because of security restrictions in XBAP/Silverlight)
				using (Stream stream = dialog.OpenFile()) {
					// Read the file
					this.OpenSchema(Path.GetFileName(dialog.FileName), null, stream);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenXsdSchemaButtonClick(object sender, RoutedEventArgs e) {
			this.OpenXsdSchema();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenXsltSchemaButtonClick(object sender, RoutedEventArgs e) {
			this.OpenXsltSchema();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenXhtmlSchemaButtonClick(object sender, RoutedEventArgs e) {
			this.OpenXhtmlSchema();
		}

		/// <summary>
		/// Occurs when the document's parse data has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to this event.</param>
		private void OnXmlEditorDocumentParseDataChanged(object sender, EventArgs e) {
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
		private void OnXmlEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
			// If there is a pending parse data change...
			if (hasPendingParseData) {
				// Clear flag
				hasPendingParseData = false;

				XmlParseData parseData = xmlEditor.Document.ParseData as XmlParseData;
				if (parseData != null) {
					if (xmlEditor.Document.CurrentSnapshot.Length < 10000) {
						// Show the AST
						if (parseData.Ast != null)
							astOutputEditor.Document.SetText(parseData.Ast.ToTreeString(0));
						else
							astOutputEditor.Document.SetText(null);
					}
					else
						astOutputEditor.Document.SetText("(Not displaying large AST for performance reasons)");

					// Output errors
					errorListView.ItemsSource = parseData.Errors;

					// Show well-formed state
					messagePanel.Content = String.Format("Well-formed: {0}", parseData.IsWellFormed ? "Yes" : "No");
				}
				else {
					// Clear UI
					astOutputEditor.Document.SetText(null);
					errorListView.ItemsSource = null;
					messagePanel.Content = "Ready";
				}
			}
		}

		/// <summary>
		/// Occurs when the document's view selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EditorViewSelectionEventArgs"/> that contains data related to this event.</param>
		private void OnXmlEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			// Update line, col, and character display
			linePanel.Text = String.Format("Ln {0}", e.CaretPosition.DisplayLine);
			columnPanel.Text = String.Format("Col {0}", e.CaretDisplayCharacterColumn);
			characterPanel.Text = String.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
		}

		/// <summary>
		/// Opens a file.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="stream">The <see cref="Stream"/> to load.</param>
		private void OpenFile(string filename, Stream stream) {
			// Load the file
			if (stream != null)
				xmlEditor.Document.LoadFile(stream, Encoding.UTF8);
			else
				xmlEditor.Document.SetText(null);

			// Set the title
			xmlDocumentWindow.Title = filename;
		}
		
		/// <summary>
		/// Opens the mammals schema.
		/// </summary>
		private void OpenMammalsSchema() {
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Mammals.xsd")) {
				this.OpenSchema("Mammals.xsd", "http://ActiproSoftware/Mammals", stream);
			}

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Mammals-Dog.xml")) {
				this.OpenFile("Mammals-Dog.xml", stream);
			}
		}
		
		/// <summary>
		/// Opens a schema.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="defaultNamespace">The optional default namespace.</param>
		/// <param name="stream">The <see cref="Stream"/> to load.</param>
		/// <param name="additionalStreams">The additional streams to load.</param>
		private void OpenSchema(string filename, string defaultNamespace, Stream stream, params Stream[] additionalStreams) {
			// Load the schema
			schemaEditor.Document.LoadFile(stream, Encoding.UTF8);

			// This allows the rich editing functionality to continue working, even when there is no xmlns in the root element
			schemaResolver.DefaultNamespace = defaultNamespace;

			// Load the schema
			schemaResolver.LoadSchemaFromString(schemaEditor.Document.CurrentSnapshot.Text);

			// Load any additional streams that are required
			if (additionalStreams != null) {
				foreach (Stream additionalStream in additionalStreams)
					schemaResolver.AddSchemaFromStream(additionalStream);
			}
			
			// Set the title
			schemaDocumentWindow.Title = filename;

			// Queue a new parse since the schema data changed
			xmlEditor.Document.QueueParseRequest();
		}
		
		/// <summary>
		/// Opens the XHTML schema.
		/// </summary>
		private void OpenXhtmlSchema() {
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.xsd")) {
				// Xml.xsd is also required for Xhtml.xsd
				using (Stream stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
					this.OpenSchema("Xhtml.xsd", null, stream, stream2);
				}
			}

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.html")) {
				this.OpenFile("Xhtml.html", stream);
			}
		}
		
		/// <summary>
		/// Opens the XSD schema.
		/// </summary>
		private void OpenXsdSchema() {
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "XmlSchema.xsd")) {
				this.OpenSchema("XmlSchema.xsd", null, stream);
			}

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "XmlSchema.xsd")) {
				this.OpenFile("XmlSchema.xsd", stream);
			}
		}
		
		/// <summary>
		/// Opens the XSLT schema.
		/// </summary>
		private void OpenXsltSchema() {
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xslt.xsd")) {
				// XmlSchema.xsd is required for Xslt.xsd
				using (Stream stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "XmlSchema.xsd")) {
					// Xml.xsd is also required for Xslt.xsd
					using (Stream stream3 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
						this.OpenSchema("Xslt.xsd", null, stream, stream2, stream3);
					}
				}
			}

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xslt.xslt")) {
				this.OpenFile("Xslt.xslt", stream);
			}
		}
		
	}

}