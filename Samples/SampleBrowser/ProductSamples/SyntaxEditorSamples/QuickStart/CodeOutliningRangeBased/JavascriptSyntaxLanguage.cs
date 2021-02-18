using System;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningRangeBased {
    
	/// <summary>
	/// Implements a <c>Javascript</c> syntax language definition that support code outlining (folding).
	/// </summary>
    public class JavascriptSyntaxLanguage : SyntaxLanguage {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>JavascriptSyntaxLanguage</c> class.
		/// </summary>
		public JavascriptSyntaxLanguage() : base("Javascript") {
			// Initialize this language from a language definition
			SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "JavaScript.langdef");

			//
			// NOTE: Make sure that you've set up an ambient parse request dispatcher for your application
			//   (see documentation on 'Parse Requests and Dispatchers') so that this parser is called in 
			//   a worker thread as the editor is updated
			//

			// Register a parser that will construct a complete range-based outlining source for the document
			//   within a worker thread after any text change... for this sample, the outlining source is returned
			//   as the parser's result to the ICodeDocument.ParseData property, where it is retrieved by
			//   the outliner below... note that if we were doing more advanced parsing
			//   by constructing an AST of the document, we'd want to use the AST result to construct the
			//   range-based outlining source instead
			this.RegisterService<IParser>(new JavascriptOutliningParser());
			
			// Register an outliner, which tells the document's outlining manager that
			//   this language supports automatic outlining, and helps drive outlining updates
			this.RegisterService<IOutliner>(new JavascriptOutliner());

			// Register a built-in service that automatically provides quick info tips 
			//   when hovering over collapsed outlining nodes
			this.RegisterService(new CollapsedRegionQuickInfoProvider());
		}
		
    }
	
}
