using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using Step6 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted06;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted07 {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step6.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//
			
			// Register a line commenter
			this.RegisterService<ILineCommenter>(new LineBasedLineCommenter() { StartDelimiter = "//" });
			
        }
    }
}
