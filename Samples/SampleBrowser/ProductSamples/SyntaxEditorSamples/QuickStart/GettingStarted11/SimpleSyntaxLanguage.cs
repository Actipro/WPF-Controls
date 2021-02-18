using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using Step10 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted10;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted11 {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step10.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//

			// Register a parameter info provider
			this.RegisterService(new SimpleParameterInfoProvider());

        }
    }
}
