using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using Step4d = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted05 {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step4d.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//
			
			// Register a tagger provider for showing parse errors
			this.RegisterService(new CodeDocumentTaggerProvider<ParseErrorTagger>(typeof(ParseErrorTagger)));

			// Register a squiggle tag quick info provider
			this.RegisterService(new SquiggleTagQuickInfoProvider());

        }
    }
}
