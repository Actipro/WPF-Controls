using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiproSoftware.Text.Implementation;
using System.IO;
using System.Resources;
using System.Reflection;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.LanguageTransitions {

	/// <summary>
	/// Represents a <see cref="SyntaxLanguage"/> for XML that has a tag-style language transition to C#.
	/// </summary>
	public class TagStyleTransitionSyntaxLanguage : SyntaxLanguage {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ASPStyleTransitionSyntaxLanguage"/> class.
		/// </summary>
		public TagStyleTransitionSyntaxLanguage() : base("Xml") {
			// Initialize this root language with the XML language definition
			ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.InitializeLanguageFromResourceStream(this, "Xml.langdef");

			// Load the C# child language
			ISyntaxLanguage cSharpLanguage = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

			// Get the lexer for the parent language
			DynamicLexer parentLexer = this.GetLexer() as DynamicLexer;

			// Get the lexer for the child language
			DynamicLexer childLexer = cSharpLanguage.GetLexer() as DynamicLexer;
			
			// Get the classification types that will be used (they were already registered by the XML language load)
			IClassificationType xmlNameClassificationType = AmbientHighlightingStyleRegistry.Instance["XmlName"];
			IClassificationType xmlDelimiterClassificationType = AmbientHighlightingStyleRegistry.Instance["XmlDelimiter"];

			// Since we will be dynamically modifying the parent lexer, wrap it with a change batch
			using (IDisposable batch = parentLexer.CreateChangeBatch()) {
				// Create a new transition lexical state in the parent language that will serve as the bridge between the two languages...
				//   Add child states similar to the XML's 'StartTag' state so that attributes are allowed in the 'script' tag
				DynamicLexicalState lexicalState = new DynamicLexicalState(0, "ScriptStartTag");
				lexicalState.DefaultTokenKey = "StartTagText";
				lexicalState.DefaultClassificationType = xmlNameClassificationType;
				lexicalState.ChildLexicalStates.Add(parentLexer.LexicalStates["StartTagAttributeDoubleQuoteValue"]);
				lexicalState.ChildLexicalStates.Add(parentLexer.LexicalStates["StartTagAttributeSingleQuoteValue"]);
				parentLexer.LexicalStates.Add(lexicalState);

				// Insert the transition lexical state at the beginning of the parent language's 
				//   default state's child states list so that it has top matching priority
				parentLexer.DefaultLexicalState.ChildLexicalStates.Insert(0, lexicalState);

				// Create the lexical scope for the transition lexical state
				DynamicLexicalScope lexicalScope = new DynamicLexicalScope();
				lexicalState.LexicalScopes.Add(lexicalScope);
				lexicalScope.StartLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Explicit, "StartTagStartDelimiter", xmlDelimiterClassificationType);
				lexicalScope.StartLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"<"));
				lexicalScope.StartLexicalPatternGroup.LookAheadPattern = @"script";
				lexicalScope.EndLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Regex, "StartTagEndDelimiter", xmlDelimiterClassificationType);
				lexicalScope.EndLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"/? >"));

				// Create a second lexical scope that will be transitioned into as soon as the first lexical scope
				//   is exited (after the '<script>' tag)... this second scope indicates the child language's
				//   lexical state to transition into along with the pattern group that will be used to exit
				//   back out to the parent language
				DynamicLexicalScope transitionLexicalScope = new DynamicLexicalScope();
				transitionLexicalScope.EndLexicalPatternGroup = new DynamicLexicalPatternGroup(DynamicLexicalPatternType.Explicit, "EndTagStartDelimiter", xmlDelimiterClassificationType);
				transitionLexicalScope.EndLexicalPatternGroup.Patterns.Add(new DynamicLexicalPattern(@"</"));
				transitionLexicalScope.EndLexicalPatternGroup.LookAheadPattern = @"script";
				lexicalScope.Transition = new LexicalStateTransition(cSharpLanguage, childLexer.DefaultLexicalState, transitionLexicalScope);
			}
		}
	}
}
