using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted02 {
    
    /// <summary>
	/// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public class SimpleSyntaxLanguage : SyntaxLanguage {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
		/// </summary>
		public SimpleSyntaxLanguage() : base("Simple") {
			// Initialize the language using the lexer and token tagger in the .langdef file
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "Simple-Basic.langdef");

			//
			// NOTE: Make sure that you've set up an ambient parse request dispatcher for your application
			//   (see documentation on 'Parse Requests and Dispatchers') so that this parser is called in 
			//   a worker thread as the editor is updated
			//
			this.RegisterParser(new SimpleParser());
		}
		
    }
}
