namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	using ActiproSoftware.Text;
	using ActiproSoftware.Text.Languages.CSharp.Implementation;
	using ActiproSoftware.Text.Languages.DotNet.Implementation;
	using ActiproSoftware.Text.Lexing;
	using ActiproSoftware.Text.Tagging.Implementation;
	using System;
    
    
    /// <summary>
    /// Represents a token tagger for the <c>Parent to C# example</c> language.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v23.1.5.0 (http://www.actiprosoftware.com).
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("LanguageDesigner", "23.1.5.0")]
    public partial class ParentTokenTagger : TokenTagger {
        
        private IParentClassificationTypeProvider	classificationTypeProviderValue;
		private CSharpTokenTagger					cSharpTokenTagger;
        
        /// <summary>
        /// Initializes a new instance of the <c>ParentTokenTagger</c> class.
        /// </summary>
        /// <param name="document">The specific <see cref="ICodeDocument"/> for which this token tagger will be used.</param>
        /// <param name="classificationTypeProvider">A <see cref="IParentClassificationTypeProvider"/> that provides classification types used by this token tagger.</param>
        public ParentTokenTagger(ICodeDocument document, IParentClassificationTypeProvider classificationTypeProvider) : 
                base(document) {
            if ((classificationTypeProvider == null)) {
                throw new ArgumentNullException("classificationTypeProvider");
            }

            // Initialize
            this.classificationTypeProviderValue = classificationTypeProvider;

			// Create the token tagger for the child language
			cSharpTokenTagger = new CSharpTokenTagger(document, new DotNetClassificationTypeProvider());
        }
        
        /// <summary>
        /// Gets the <see cref="IParentClassificationTypeProvider"/> that provides classification types used by this token tagger.
        /// </summary>
        /// <value>The <see cref="IParentClassificationTypeProvider"/> that provides classification types used by this token tagger.</value>
        public IParentClassificationTypeProvider ClassificationTypeProvider {
            get {
                return this.classificationTypeProviderValue;
            }
        }
        
        /// <summary>
        /// Returns an <see cref="IClassificationType"/> for the specified <see cref="IToken"/>, if one is appropriate.
        /// </summary>
        /// <param name="token">The <see cref="IToken"/> to examine.</param>
        /// <returns>An <see cref="IClassificationType"/> for the specified <see cref="IToken"/>, if one is appropriate.</returns>
        public override IClassificationType ClassifyToken(IToken token) {
			if (ParentTokenId.IsKeywordClassificationType(token.Id))
				return classificationTypeProviderValue.Keyword;
			else if (ParentTokenId.IsTransitionDelimiterClassificationType(token.Id))
				return classificationTypeProviderValue.TransitionDelimiter;
			else {
				// Fall back to using the C# token tagger for other tokens
				return cSharpTokenTagger.ClassifyToken(token);
			}
        }
    }
}
