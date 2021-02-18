using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using Step11 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted11;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted12 {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step11.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//

			// Register an indenter provider service
			this.RegisterIndentProvider(new SimpleIndentProvider());
			
        }
    }
}
