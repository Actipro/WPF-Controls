using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Lexing.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.LanguageTransitions;

/// <summary>
/// Represents a <see cref="SyntaxLanguage"/> for XML that has an ASP directive-style language transition to C#.
/// </summary>
public class AspStyleTransitionSyntaxLanguage : SyntaxLanguage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AspStyleTransitionSyntaxLanguage() : base("Xml") {
		// Initialize this root language with the XML language definition
		Common.SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "Xml.langdef");

		// Load the C# child language
		var cSharpLanguage = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

		// Get the lexer for the parent language
		var parentLexer = (DynamicLexer)this.GetLexer()!;

		// Get the lexer for the child language
		var childLexer = (DynamicLexer)cSharpLanguage.GetLexer()!;

		// Get the classification types that will be used (create and register if necessary)
		const string serverSideScriptKey = "ServerSideScript";
		var serverSideScriptClassificationType = AmbientHighlightingStyleRegistry.Instance[serverSideScriptKey];
		if (serverSideScriptClassificationType is null) {
			// Ensure Light/Dark color palettes are configured for the highlighting style
			var lightColors = AmbientHighlightingStyleRegistry.Instance.LightColorPalette;
			var darkColors = AmbientHighlightingStyleRegistry.Instance.DarkColorPalette;
			lightColors?.SetForeground(serverSideScriptKey, UIColor.FromWebColor("#000000"));
			lightColors?.SetBackground(serverSideScriptKey, UIColor.FromWebColor("#ffff00"));
			darkColors?.SetForeground(serverSideScriptKey, UIColor.FromWebColor("#000000"));
			darkColors?.SetBackground(serverSideScriptKey, UIColor.FromWebColor("#ffffb3"));

			// Register the classification type with a default highlighting style and the current color palette will be applied
			serverSideScriptClassificationType = new ClassificationType(serverSideScriptKey, "Server-Side Script");
			AmbientHighlightingStyleRegistry.Instance.Register(serverSideScriptClassificationType, new HighlightingStyle());
		}

		// Since we will be dynamically modifying the parent lexer, wrap it with a change batch
		using (parentLexer.CreateChangeBatch()) {
			// Create a new transition lexical state in the parent language that will serve as the bridge between the two languages
			var lexicalState = new DynamicLexicalState(0, "ASPDirective") {
				DefaultTokenKey = "ASPDirectiveText"
			};
			parentLexer.LexicalStates.Add(lexicalState);

			// Insert the transition lexical state at the beginning of the parent language's
			//   default state's child states list so that it has top matching priority
			parentLexer.DefaultLexicalState?.ChildLexicalStates.Insert(0, lexicalState);

			// Create the lexical scope for the transition lexical state
			var lexicalScope = new DynamicLexicalScope();
			lexicalState.LexicalScopes.Add(lexicalScope);
			lexicalScope.StartLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Explicit, "ASPDirectiveStartDelimiter", serverSideScriptClassificationType);
			lexicalScope.StartLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"<%"));
			lexicalScope.EndLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Explicit, "ASPDirectiveEndDelimiter", serverSideScriptClassificationType);
			lexicalScope.EndLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"%>"));

			// Set up a direct transition on the lexical state so that when it is entered,
			//   it will transition directly to the child language's default lexical state
			lexicalState.Transition = new LexicalStateTransition(cSharpLanguage, childLexer.DefaultLexicalState!, childLexicalScope: null);
		}
	}

}
