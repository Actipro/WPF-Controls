using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Analysis.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId
using Step13 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted13;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted14 {
    
    /// <summary>
    /// Represents a <c>Simple</c> syntax language definition.
    /// </summary>
    public partial class SimpleSyntaxLanguage : Step13.SimpleSyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleSyntaxLanguage</c> class.
        /// </summary>
        public SimpleSyntaxLanguage() {

			//
			// NOTE: This language inherits the language class defined in a previous step and thus
			//   automatically inherits all of its registered services
			//

			// Register a structure matcher 
			this.RegisterStructureMatcher(new SimpleStructureMatcher());

			// Register a tagger that can identify delimiters to highlight from a structure matcher
			this.RegisterService(new TextViewTaggerProvider<DelimiterHighlightTagger>(typeof(DelimiterHighlightTagger)));

			// Register a delimiter auto-completer
			this.RegisterDelimiterAutoCompleter(new DelimiterAutoCompleter() {
				CanCompleteSquareBraces = false,
				OpenCurlyBraceTokenId = SimpleTokenId.OpenCurlyBrace,
				OpenParenthesisTokenId = SimpleTokenId.OpenParenthesis,
			});
        }
    }
}
