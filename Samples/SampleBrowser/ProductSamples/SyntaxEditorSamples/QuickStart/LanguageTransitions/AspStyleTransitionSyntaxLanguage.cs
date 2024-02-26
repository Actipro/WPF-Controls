using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Lexing.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.LanguageTransitions {

	/// <summary>
	/// Represents a <see cref="SyntaxLanguage"/> for XML that has an ASP directive-style language transition to C#.
	/// </summary>
	public class AspStyleTransitionSyntaxLanguage : SyntaxLanguage {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="AspStyleTransitionSyntaxLanguage"/> class.
		/// </summary>
		public AspStyleTransitionSyntaxLanguage() : base("Xml") {
			// Initialize this root language with the XML language definition
			ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "Xml.langdef");

			// Load the C# child language
			ISyntaxLanguage cSharpLanguage = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

			// Get the lexer for the parent language
			DynamicLexer parentLexer = this.GetLexer() as DynamicLexer;

			// Get the lexer for the child language
			DynamicLexer childLexer = cSharpLanguage.GetLexer() as DynamicLexer;
			
			// Get the classification types that will be used (create and register if necessary)
			const string serverSideScriptKey = "ServerSideScript";
			IClassificationType serverSideScriptClassificationType = AmbientHighlightingStyleRegistry.Instance[serverSideScriptKey];
			if (serverSideScriptClassificationType == null) {
				// Ensure Light/Dark color palettes are configured for the highlighting style
				var lightColors = AmbientHighlightingStyleRegistry.Instance.LightColorPalette;
				var darkColors = AmbientHighlightingStyleRegistry.Instance.DarkColorPalette;
				lightColors?.SetForeground(serverSideScriptKey, UIColor.FromWebColor("#000000"));
				lightColors?.SetBackground(serverSideScriptKey, UIColor.FromWebColor("#ffff00"));
				darkColors?.SetForeground(serverSideScriptKey, UIColor.FromWebColor("#000000"));
				darkColors?.SetBackground(serverSideScriptKey, UIColor.FromWebColor("#ffffb3"));

				// Register the classification type with a default highlighting style and the current color palette will be applied
				serverSideScriptClassificationType = new ClassificationType(serverSideScriptKey, "Server-Side Script");
				AmbientHighlightingStyleRegistry.Instance.Register(
					serverSideScriptClassificationType,
					new HighlightingStyle());
			}
			
			// Since we will be dynamically modifying the parent lexer, wrap it with a change batch
			using (IDisposable batch = parentLexer.CreateChangeBatch()) {
				// Create a new transition lexical state in the parent language that will serve as the bridge between the two languages
				DynamicLexicalState lexicalState = new DynamicLexicalState(0, "ASPDirective");
				lexicalState.DefaultTokenKey = "ASPDirectiveText";
				parentLexer.LexicalStates.Add(lexicalState);

				// Insert the transition lexical state at the beginning of the parent language's 
				//   default state's child states list so that it has top matching priority
				parentLexer.DefaultLexicalState.ChildLexicalStates.Insert(0, lexicalState);

				// Create the lexical scope for the transition lexical state
				DynamicLexicalScope lexicalScope = new DynamicLexicalScope();
				lexicalState.LexicalScopes.Add(lexicalScope);
				lexicalScope.StartLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Explicit, "ASPDirectiveStartDelimiter", serverSideScriptClassificationType);
				lexicalScope.StartLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"<%"));
				lexicalScope.EndLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Explicit, "ASPDirectiveEndDelimiter", serverSideScriptClassificationType);
				lexicalScope.EndLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"%>"));

				// Set up a direct transition on the lexical state so that when it is entered, 
				//   it will transition directly to the child language's default lexical state
				lexicalState.Transition = new LexicalStateTransition(cSharpLanguage, childLexer.DefaultLexicalState, null);
			}
		}

	}
}
