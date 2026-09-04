using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens;

/// <summary>
/// Stores the results of a .NET language parsing operation, adding declaration data.
/// </summary>
public class CodeLensParseData : IDotNetParseData {

	private readonly List<CodeLensDeclaration> _declarations = [];
	private readonly IDotNetParseData _wrappedParseData;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="wrappedParseData">The parse data to wrap.</param>
	public CodeLensParseData(IDotNetParseData wrappedParseData) {
		#if NET
		ArgumentNullException.ThrowIfNull(wrappedParseData);
		#else
		if (wrappedParseData is null)
			throw new ArgumentNullException(nameof(wrappedParseData));
		#endif

		// Initialize
		_wrappedParseData = wrappedParseData;
		InitializeDeclarationsRecursive(Ast);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Recursively searches an AST node hierarchy for type declarations.
	/// </summary>
	/// <param name="astNode">The <see cref="IAstNode"/> to examine.</param>
	private void InitializeDeclarationsRecursive(IAstNode? astNode) {
		if (astNode is null)
			return;

		switch (astNode.Id) {
			case DotNetAstNodeId.CompilationUnit: {
				if (astNode is CompilationUnit { HasMembers: true } compUnitAstNode) {
					foreach (var childAstNode in compUnitAstNode.Members)
						InitializeDeclarationsRecursive(childAstNode);
				}
				break;
			}
			case DotNetAstNodeId.NamespaceDeclaration: {
				if (astNode is NamespaceDeclaration { Body.HasMembers: true } namespaceDeclAstNode) {
					foreach (var childAstNode in namespaceDeclAstNode.Body.Members)
						InitializeDeclarationsRecursive(childAstNode);
				}
				break;
			}
			case DotNetAstNodeId.ClassDeclaration:
			case DotNetAstNodeId.DelegateDeclaration:
			case DotNetAstNodeId.EnumerationDeclaration:
			case DotNetAstNodeId.InterfaceDeclaration:
			case DotNetAstNodeId.StructureDeclaration:
				if ((astNode.StartOffset is { } startOffset) && (Snapshot is { } snapshot)) {
					var declaration = new CodeLensDeclaration(new TextSnapshotOffset(snapshot, startOffset), astNode);
					_declarations.Add(declaration);
				}
				break;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ILLParseData.Ast"/>
	public IAstNode? Ast
		=> _wrappedParseData.Ast;

	/// <summary>
	/// The list of declarations found in the parsed AST.
	/// </summary>
	public IList<CodeLensDeclaration> Declarations
		=> _declarations;

	/// <inheritdoc cref="IParseErrorProvider.Errors"/>
	public IEnumerable<IParseError>? Errors
		=> _wrappedParseData.Errors;

	/// <inheritdoc cref="IDotNetParseData.PreprocessorDirectives"/>
	public IList<PreprocessorDirective> PreprocessorDirectives
		=> _wrappedParseData.PreprocessorDirectives;

	/// <inheritdoc cref="IParseErrorProvider.Snapshot"/>
	public ITextSnapshot? Snapshot
		=> _wrappedParseData.Snapshot;

}
