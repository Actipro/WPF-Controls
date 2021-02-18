using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
    
    /// <summary>
    /// Represents a <c>Parent to C# example</c> syntax language definition.
    /// </summary>
    public partial class ParentSyntaxLanguage : SyntaxLanguage {
        
        /// <summary>
        /// Initializes a new instance of the <c>ParentSyntaxLanguage</c> class.
        /// </summary>
		/// <param name="projectAssembly">The project assembly to use.</param>
        public ParentSyntaxLanguage(IProjectAssembly projectAssembly) : 
                base("Parent to C# example") {

            // Create a classification type provider and register its classification types
            ParentClassificationTypeProvider classificationTypeProvider = new ParentClassificationTypeProvider();
            classificationTypeProvider.RegisterAll();

			// Create a C# child language
			var childLanguage = new CSharpSyntaxLanguage();
			childLanguage.RegisterProjectAssembly(projectAssembly);
			this.RegisterProjectAssembly(projectAssembly);

			// Register core parent language services
			this.RegisterLexer(new ParentLexer(childLanguage));
			this.RegisterService(new ParentTokenTaggerProvider(classificationTypeProvider));
			this.RegisterParser(new ParentParser(childLanguage));

			// Register proxy IntelliPrompt services
			this.RegisterService(new TranslatedCSharpCompletionProvider());
			this.RegisterService(new TranslatedCSharpParameterInfoProvider());
			this.RegisterService(new TranslatedCSharpQuickInfoProvider());

			// Register a tagger provider for showing parse errors
			this.RegisterService(new CodeDocumentTaggerProvider<ParseErrorTagger>(typeof(ParseErrorTagger)));

			// Register a squiggle tag quick info provider
			this.RegisterService(new SquiggleTagQuickInfoProvider());
        }
    }
}
