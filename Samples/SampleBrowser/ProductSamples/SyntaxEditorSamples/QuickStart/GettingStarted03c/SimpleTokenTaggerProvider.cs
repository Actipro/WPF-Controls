namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c {
    using ActiproSoftware.Text;
    using ActiproSoftware.Text.Tagging;
    using ActiproSoftware.Text.Tagging.Implementation;
    using System;
	using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;


	//
	// NOTE: Here we just cloned the SimpleTokenTaggerProvider defined in step 3b and put it in the namespace
	//   for step 3c so that it will create step 3c's token tagger
	//
    
    
    /// <summary>
    /// Represents a provider of <see cref="SimpleTokenTagger"/> objects for documents that use the <c>SimpleTokenTagger</c> language.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v10.2.530.0 (http://www.actiprosoftware.com).
    /// Generated code is based on input created by Actipro Software LLC.
    /// Copyright (c) 2001-2025 Actipro Software LLC.  All rights reserved.
    /// </remarks>
    public partial class SimpleTokenTaggerProvider : TaggerProviderBase<SimpleTokenTagger>, ICodeDocumentTaggerProvider {
        
        private ISimpleClassificationTypeProvider classificationTypeProviderValue;
        
        /// <summary>
        /// Initializes a new instance of the <c>SimpleTokenTaggerProvider</c> class.
        /// </summary>
        /// <param name="classificationTypeProvider">A <see cref="ISimpleClassificationTypeProvider"/> that provides classification types.</param>
        public SimpleTokenTaggerProvider(ISimpleClassificationTypeProvider classificationTypeProvider) {
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
            if (typeof(ITagger<T>).IsAssignableFrom(typeof(SimpleTokenTagger))) {
                TaggerFactory factory = new TaggerFactory(this, document);
                return ((ITagger<T>)(document.Properties.GetOrCreateSingleton(typeof(ITagger<ITokenTag>), new ActiproSoftware.Text.Utility.PropertyDictionary.Creator<SimpleTokenTagger>(factory.CreateTagger))));
            }
            else {
                return null;
            }
        }
        
        /// <summary>
        /// Implements a factory that can creates <see cref="SimpleTokenTagger"/> objects for a document.
        /// </summary>
        public class TaggerFactory {
            
            private ICodeDocument documentValue;
            
            private SimpleTokenTaggerProvider providerValue;
            
            /// <summary>
            /// Initializes a new instance of the <c>TaggerFactory</c> class.
            /// </summary>
            /// <param name="provider">The owner <see cref="SimpleTokenTaggerProvider"/>.</param>
            /// <param name="document">The <see cref="ICodeDocument"/> that requires an <see cref="SimpleTokenTagger"/>.</param>
            internal TaggerFactory(SimpleTokenTaggerProvider provider, ICodeDocument document) {
                // Initialize
                this.providerValue = provider;
                this.documentValue = document;
            }
            
            /// <summary>
            /// Creates an <see cref="SimpleTokenTagger"/> for the <see cref="ICodeDocument"/>.
            /// </summary>
            /// <returns>An <see cref="SimpleTokenTagger"/> for the <see cref="ICodeDocument"/>.</returns>
            public SimpleTokenTagger CreateTagger() {
                return new SimpleTokenTagger(this.documentValue, this.providerValue.classificationTypeProviderValue);
            }
        }
    }
}
