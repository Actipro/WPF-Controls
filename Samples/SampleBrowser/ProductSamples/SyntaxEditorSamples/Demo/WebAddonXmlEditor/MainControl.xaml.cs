using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Languages.Xml;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using Microsoft.Win32;
using System.Reflection;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.WebAddonXmlEditor;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private int _documentNumber;
	private bool _hasPendingParseData;
	private readonly XmlSchemaResolver _schemaResolver = new();

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
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
		xmlEditor.Document.Language.RegisterXmlSchemaResolver(_schemaResolver);

		// Initialize
		NewFile();
		OpenMammalsSchema();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new file.
	/// </summary>
	private void NewFile()
		=> OpenFile(string.Format("Document{0}.xml", ++_documentNumber));

	private void OnCloseSchemaButtonClick(object sender, RoutedEventArgs e) {
		// Clear the schema
		_schemaResolver.SchemaSet = null;

		// Set the title
		schemaDocumentWindow.Title = "NoSchema.xsd";

		// Clear the text
		schemaEditor.Document.SetText(null);

		// Queue a new parse since the schema data changed
		xmlEditor.Document.QueueParseRequest();
	}

	private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IParseError error }) {
			if (error.PositionRange.HasValue)
				xmlEditor.ActiveView.Selection.StartPosition = error.PositionRange.Value.StartPosition;

			xmlDocumentWindow.Activate();
		}
	}

	private void OnNewFileButtonClick(object sender, RoutedEventArgs e)
		=> NewFile();

	private void OnOpenFileButtonClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Open a document (use dialog to help open the file because of possible security restrictions)
			using (var stream = dialog.OpenFile()) {
				// Read the file
				OpenFile(Path.GetFileName(dialog.FileName), stream);
			}
		}
	}

	private void OnOpenSchemaButtonClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "XSD files (*.xsd)|*.xsd|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Open a document (use dialog to help open the file because of possible security restrictions)
			using (var stream = dialog.OpenFile()) {
				// Read the file
				OpenSchema(Path.GetFileName(dialog.FileName), defaultNamespace: null, stream);
			}
		}
	}

	private void OnOpenXsdSchemaButtonClick(object sender, RoutedEventArgs e)
		=> OpenXsdSchema();

	private void OnOpenXsltSchemaButtonClick(object sender, RoutedEventArgs e)
		=> OpenXsltSchema();

	private void OnOpenXhtmlSchemaButtonClick(object sender, RoutedEventArgs e)
		=> OpenXhtmlSchema();

	private void OnXmlEditorDocumentParseDataChanged(object sender, EventArgs e) {
		//
		// NOTE: The parse data here is generated in a worker thread... this event handler is called
		//   back in the UI thread immediately when the worker thread completes... it is best
		//   practice to delay UI updates until the end user stops typing... we will flag that
		//   there is a pending parse data change, which will be handled in the
		//   UserInterfaceUpdate event
		//

		_hasPendingParseData = true;
	}

	/// <summary>
	/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
	/// </summary>
	private void OnXmlEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
		// If there is a pending parse data change...
		if (_hasPendingParseData) {
			// Clear flag
			_hasPendingParseData = false;

			var parseData = xmlEditor.Document.ParseData as XmlParseData;
			if (parseData is not null) {
				if (xmlEditor.Document.CurrentSnapshot.Length < 10000) {
					// Show the AST
					astOutputEditor.Document.SetText(parseData.Ast?.ToTreeString(0));
				}
				else
					astOutputEditor.Document.SetText("(Not displaying large AST for performance reasons)");

				// Output errors
				errorListView.ItemsSource = parseData.Errors;

				// Show well-formed state
				messagePanel.Content = string.Format("Well-formed: {0}", parseData.IsWellFormed ? "Yes" : "No");
			}
			else {
				// Clear UI
				astOutputEditor.Document.SetText(null);
				errorListView.ItemsSource = null;
				messagePanel.Content = "Ready";
			}
		}
	}

	private void OnXmlEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		// Update line, col, and character display
		linePanel.Text = string.Format("Ln {0}", e.CaretPosition.DisplayLine);
		columnPanel.Text = string.Format("Col {0}", e.CaretDisplayCharacterColumn);
		characterPanel.Text = string.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
	}

	/// <summary>
	/// Opens a file.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	/// <param name="stream">The optional <see cref="Stream"/> to load.</param>
	private void OpenFile(string fileName, Stream? stream = null) {
		// Load the file
		if (stream is not null)
			xmlEditor.Document.LoadFile(stream, Encoding.UTF8);
		else
			xmlEditor.Document.SetText(null);

		// Set the title
		xmlDocumentWindow.Title = fileName;
	}

	/// <summary>
	/// Opens the mammals schema.
	/// </summary>
	private void OpenMammalsSchema() {
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Mammals.xsd")) {
			OpenSchema("Mammals.xsd", "http://ActiproSoftware/Mammals", stream);
		}

		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Mammals-Dog.xml")) {
			OpenFile("Mammals-Dog.xml", stream);
		}
	}

	/// <summary>
	/// Opens a schema.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	/// <param name="defaultNamespace">The optional default namespace.</param>
	/// <param name="stream">The <see cref="Stream"/> to load.</param>
	/// <param name="additionalStreams">The additional streams to load.</param>
	private void OpenSchema(string fileName, string? defaultNamespace, Stream? stream, params Stream?[]? additionalStreams) {
		if (stream is null)
			return;

		// Load the schema
		schemaEditor.Document.LoadFile(stream, Encoding.UTF8);

		// This allows the rich editing functionality to continue working, even when there is no xmlns in the root element
		_schemaResolver.DefaultNamespace = defaultNamespace;

		// Load the schema
		_schemaResolver.LoadSchemaFromString(schemaEditor.Document.CurrentSnapshot.Text);

		// Load any additional streams that are required
		if (additionalStreams is not null) {
			foreach (var additionalStream in additionalStreams) {
				if (additionalStream is not null)
					_schemaResolver.AddSchemaFromStream(additionalStream);
			}
		}

		// Set the title
		schemaDocumentWindow.Title = fileName;

		// Queue a new parse since the schema data changed
		xmlEditor.Document.QueueParseRequest();
	}

	/// <summary>
	/// Opens the XHTML schema.
	/// </summary>
	private void OpenXhtmlSchema() {
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.xsd")) {
			// Xml.xsd is also required for Xhtml.xsd
			using (var stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
				OpenSchema("Xhtml.xsd", defaultNamespace: null, stream, stream2);
			}
		}

		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.html")) {
			OpenFile("Xhtml.html", stream);
		}
	}

	/// <summary>
	/// Opens the XSD schema.
	/// </summary>
	private void OpenXsdSchema() {
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "XmlSchema.xsd")) {
			OpenSchema("XmlSchema.xsd", defaultNamespace: null, stream);
		}

		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "XmlSchema.xsd")) {
			OpenFile("XmlSchema.xsd", stream);
		}
	}

	/// <summary>
	/// Opens the XSLT schema.
	/// </summary>
	private void OpenXsltSchema() {
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xslt.xsd")) {
			// XmlSchema.xsd is required for Xslt.xsd
			using (var stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "XmlSchema.xsd")) {
				// Xml.xsd is also required for Xslt.xsd
				using (var stream3 = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
					OpenSchema("Xslt.xsd", null, stream, stream2, stream3);
				}
			}
		}

		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xslt.xslt")) {
			OpenFile("Xslt.xslt", stream);
		}
	}

}
