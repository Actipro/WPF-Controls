using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Languages.Xml;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using System.Reflection;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.WebAddonXmlGettingStarted;

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

		//
		// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
		//   since it tells you how to set up an ambient parse request dispatcher within your
		//   application OnStartup code, and add related cleanup in your application OnExit code.
		//   These steps are essential to having the add-on perform well.
		//

		// Register the schema resolver service with the XML language (needed to support IntelliPrompt)
		var schemaResolver = new XmlSchemaResolver();
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Mammals.xsd")!) {
			schemaResolver.LoadSchemaFromStream(stream);
		}
		xmlEditor.Document.Language.RegisterXmlSchemaResolver(schemaResolver);
	}

}
