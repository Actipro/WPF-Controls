using System;
using System.Windows.Controls;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Text.Languages.Xml;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.WebAddonXmlTextFormatterOptions {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

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
			
			editor.Document.Language = new XmlSyntaxLanguage();
			XmlTextFormatter formatter = editor.Document.Language.GetTextFormatter() as XmlTextFormatter;
			if (formatter != null) {
				formatter.AttributeSpacingMode = XmlAttributeSpacingMode.NormalizeWhitespace;
				formatter.ElementSpacingMode = XmlElementSpacingMode.NormalizeEmptyLines;
				formatter.TagWrapLength = 120;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the formatter.
		/// </summary>
		/// <value>The formatter.</value>
		public XmlTextFormatter Formatter {
			get {
				if ((editor == null) || (editor.Document == null) || (editor.Document.Language == null))
					return null;
				return editor.Document.Language.GetTextFormatter() as XmlTextFormatter;
			}
		}
	}
}
