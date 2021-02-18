using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Tagging.Implementation;
using Step3c = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04c {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step3c.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//
			
			//
			// NOTE: Make sure that you've set up an ambient parse request dispatcher for your application
			//   (see documentation on 'Parse Requests and Dispatchers') so that this parser is called in 
			//   a worker thread as the editor is updated
			//
			
			// Register a parser
			this.RegisterParser(new SimpleParser());

        }
    }
}
