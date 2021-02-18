using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {
    
	/// <summary>
	/// Stores the results of a .NET language parsing operation, adding declaration data.
	/// </summary>
	public class CodeLensParseData : IDotNetParseData {

		private List<CodeLensDeclaration>	declarations		= new List<CodeLensDeclaration>();
		private IDotNetParseData			wrappedParseData;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CodeLensParseData</c> class.
		/// </summary>
		/// <param name="wrappedParseData">The parse data to wrap.</param>
		public CodeLensParseData(IDotNetParseData wrappedParseData) {
			if (wrappedParseData == null)
				throw new ArgumentNullException("wrappedParseData");

			// Initialize
			this.wrappedParseData = wrappedParseData;
			this.InitializeDeclarationsRecursive(this.Ast);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Recursively searches an AST node hierarchy for type declarations.
		/// </summary>
		/// <param name="astNode">The <see cref="IAstNode"/> to examine.</param>
		private void InitializeDeclarationsRecursive(IAstNode astNode) {
			if (astNode != null) {
				switch (astNode.Id) {
					case DotNetAstNodeId.CompilationUnit: {
						var compUnitAstNode = astNode as CompilationUnit;
						if ((compUnitAstNode != null) && (compUnitAstNode.HasMembers)) {
							foreach (var childAstNode in compUnitAstNode.Members)
								this.InitializeDeclarationsRecursive(childAstNode);
						}
						break;
					}
					case DotNetAstNodeId.NamespaceDeclaration: {
						var namespaceDeclAstNode = astNode as NamespaceDeclaration;
						if ((namespaceDeclAstNode != null) && (namespaceDeclAstNode.Body != null) && (namespaceDeclAstNode.Body.HasMembers)) {
							foreach (var childAstNode in namespaceDeclAstNode.Body.Members)
								this.InitializeDeclarationsRecursive(childAstNode);
						}
						break;
					}
					case DotNetAstNodeId.ClassDeclaration:
					case DotNetAstNodeId.DelegateDeclaration:
					case DotNetAstNodeId.EnumerationDeclaration:
					case DotNetAstNodeId.InterfaceDeclaration:
					case DotNetAstNodeId.StructureDeclaration:
						if (astNode.StartOffset.HasValue) {
							var declaration = new CodeLensDeclaration(new TextSnapshotOffset(this.Snapshot, astNode.StartOffset.Value), astNode);
							declarations.Add(declaration);
						}
						break;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the object that contains the abstract syntax tree result.
		/// </summary>
		/// <value>The object that contains the abstract syntax tree result.</value>
		public IAstNode Ast {
			get {
				return wrappedParseData.Ast;
			}
		}
		
		/// <summary>
		/// Gets the list of declarations found in the parsed AST.
		/// </summary>
		/// <value>The list of declarations found in the parsed AST.</value>
		public IList<CodeLensDeclaration> Declarations {
			get {
				return declarations;
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="IParseError"/> objects that specify parse errors.
		/// </summary>
		/// <value>The collection of <see cref="IParseError"/> objects that specify parse errors.</value>
		public IEnumerable<IParseError> Errors {
			get {
				return wrappedParseData.Errors;
			}
		}

		/// <summary>
		/// Gets the collection of preprocessor directives.
		/// </summary>
		/// <value>The collection of preprocessor directives.</value>
		public IList<PreprocessorDirective> PreprocessorDirectives {
			get {
				return wrappedParseData.PreprocessorDirectives;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ITextSnapshot"/>, if known, from which the parse errors were created.
		/// </summary>
		/// <value>The <see cref="ITextSnapshot"/>, if known, from which the parse errors were created.</value>
		public ITextSnapshot Snapshot {
			get {
				return wrappedParseData.Snapshot;
			}
		}
		
	}
	
}
