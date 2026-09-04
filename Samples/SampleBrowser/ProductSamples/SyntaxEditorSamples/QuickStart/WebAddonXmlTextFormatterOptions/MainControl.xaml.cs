using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.Xml;
using ActiproSoftware.Text.Languages.Xml.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.WebAddonXmlTextFormatterOptions;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

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

		editor.Document.Language = new XmlSyntaxLanguage();
		if (Formatter is { } formatter) {
			formatter.AttributeSpacingMode = XmlAttributeSpacingMode.NormalizeWhitespace;
			formatter.ElementSpacingMode = XmlElementSpacingMode.NormalizeEmptyLines;
			formatter.TagWrapLength = 120;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The formatter.
	/// </summary>
	public XmlTextFormatter? Formatter
		=> editor.Document.Language.GetTextFormatter() as XmlTextFormatter;

}
