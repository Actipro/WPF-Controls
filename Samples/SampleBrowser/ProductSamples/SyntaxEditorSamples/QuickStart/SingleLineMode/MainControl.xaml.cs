using System;
using System.IO;
using System.Reflection;
using ActiproSoftware.Text.Languages.Xml;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SingleLineMode {

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

			// Load a language from a language definition
			cSharpEditor.Document.Language = SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

			// For the HTML text box that uses the Web Languages Add-on, the following code is needed:

			//
			// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
			//   since it tells you how to set up an ambient parse request dispatcher within your 
			//   application OnStartup code, and add related cleanup in your application OnExit code.  
			//   These steps are essential to having the add-on perform well.
			//
			
			// Register the schema resolver service with the XML language (needed to support IntelliPrompt for HTML)
			XmlSchemaResolver resolver = new XmlSchemaResolver();
			resolver.DefaultNamespace = "http://www.w3.org/1999/xhtml";
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.xsd")) {
				resolver.AddSchemaFromStream(stream);
			}
			// Xml.xsd is also required for Xhtml.xsd
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
				resolver.AddSchemaFromStream(stream);
			}
			xmlEditor.Document.Language.RegisterXmlSchemaResolver(resolver);
		}
		
	}

}