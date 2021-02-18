using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Tagging.Implementation;
using Step3b = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step3b.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//

			// Register the programmatic mergable lexer defined in this step, which will replace
			//   the ILexer feature service defined in the inherited language class
            this.RegisterLexer(new SimpleLexer(true));

			// Unregister the token tagger provider defined in the inherited language class
			//   since a new one will be registered that knows how to convert the programmatic
			//   lexer's tokens to classifications
            this.UnregisterService<Step3b.SimpleTokenTaggerProvider>();
            this.RegisterService(new SimpleTokenTaggerProvider(new Step3b.SimpleClassificationTypeProvider()));

        }
    }
}
