namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
    using ActiproSoftware.Text;
    using ActiproSoftware.Text.Tagging;
    using ActiproSoftware.Text.Tagging.Implementation;
    using System;
    
    
    /// <summary>
    /// Represents a provider of <see cref="ParentTokenTagger"/> objects for documents that use the <c>ParentTokenTagger</c> language.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v23.1.5.0 (http://www.actiprosoftware.com).
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("LanguageDesigner", "23.1.5.0")]
    public partial class ParentTokenTaggerProvider : TaggerProviderBase<ParentTokenTagger>, ICodeDocumentTaggerProvider {
        
        private IParentClassificationTypeProvider classificationTypeProviderValue;
        
        /// <summary>
        /// Initializes a new instance of the <c>ParentTokenTaggerProvider</c> class.
        /// </summary>
        /// <param name="classificationTypeProvider">A <see cref="IParentClassificationTypeProvider"/> that provides classification types.</param>
        public ParentTokenTaggerProvider(IParentClassificationTypeProvider classificationTypeProvider) {
            if ((classificationTypeProvider == null)) {
                throw new ArgumentNullException("classificationTypeProvider");
            }

            // Initialize
            this.classificationTypeProviderValue = classificationTypeProvider;
        }
        
        /// <summary>
        /// Returns a tagger for the specified <see cref="ICodeDocument"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ITag"/> created by the tagger.</typeparam>
        /// <param name="document">The <see cref="ICodeDocument"/> that requires a tagger.</param>
        /// <returns>A tagger for the specified <see cref="ICodeDocument"/>.</returns>
        public ITagger<T> GetTagger<T>(ICodeDocument document)
            where T : ITag {
            if (typeof(ITagger<T>).IsAssignableFrom(typeof(ParentTokenTagger))) {
                TaggerFactory factory = new TaggerFactory(this, document);
                return ((ITagger<T>)(document.Properties.GetOrCreateSingleton(typeof(ITagger<ITokenTag>), new ActiproSoftware.Text.Utility.PropertyDictionary.Creator<ParentTokenTagger>(factory.CreateTagger))));
            }
            else {
                return null;
            }
        }
        
        /// <summary>
        /// Implements a factory that can creates <see cref="ParentTokenTagger"/> objects for a document.
        /// </summary>
        public class TaggerFactory {
            
            private ICodeDocument documentValue;
            
            private ParentTokenTaggerProvider providerValue;
            
            /// <summary>
            /// Initializes a new instance of the <c>TaggerFactory</c> class.
            /// </summary>
            /// <param name="provider">The owner <see cref="ParentTokenTaggerProvider"/>.</param>
            /// <param name="document">The <see cref="ICodeDocument"/> that requires an <see cref="ParentTokenTagger"/>.</param>
            internal TaggerFactory(ParentTokenTaggerProvider provider, ICodeDocument document) {
                // Initialize
                this.providerValue = provider;
                this.documentValue = document;
            }
            
            /// <summary>
            /// Creates an <see cref="ParentTokenTagger"/> for the <see cref="ICodeDocument"/>.
            /// </summary>
            /// <returns>An <see cref="ParentTokenTagger"/> for the <see cref="ICodeDocument"/>.</returns>
            public ParentTokenTagger CreateTagger() {
                return new ParentTokenTagger(this.documentValue, this.providerValue.classificationTypeProviderValue);
            }
        }
    }
}
