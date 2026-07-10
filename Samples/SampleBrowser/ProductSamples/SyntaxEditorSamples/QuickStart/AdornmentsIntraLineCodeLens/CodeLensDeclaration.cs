using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens;

/// <summary>
/// Stores information about a parsed declaration and its snapshot offset.
/// </summary>
public class CodeLensDeclaration {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="snapshotOffset">The snapshot offset at which the declaration occurs.</param>
	/// <param name="astNode">The <see cref="IAstNode"/> for the declaration.</param>
	public CodeLensDeclaration(TextSnapshotOffset snapshotOffset, IAstNode astNode) {
		#if NET
		ArgumentNullException.ThrowIfNull(astNode);
		#else
		if (astNode is null)
			throw new ArgumentNullException(nameof(astNode));
		#endif

		// Initialize
		VersionRange = new TextSnapshotRange(snapshotOffset).ToVersionRange(TextRangeTrackingModes.Default);
		AstNode = astNode;
		Key = CreateKey();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the <see cref="Key"/>.
	/// </summary>
	private string CreateKey() {
		var builder = new StringBuilder();

		var currentAstNode = AstNode;
		while (currentAstNode is not null) {
			switch (currentAstNode.Id) {
				case DotNetAstNodeId.NamespaceDeclaration: {
					if (builder.Length > 0)
						builder.Insert(0, '.');
					var name = ((NamespaceDeclaration)currentAstNode).Name;
					builder.Insert(0, name?.ToString() ?? "?");
					break;
				}

				case DotNetAstNodeId.ClassDeclaration:
				case DotNetAstNodeId.DelegateDeclaration:
				case DotNetAstNodeId.EnumerationDeclaration:
				case DotNetAstNodeId.InterfaceDeclaration:
				case DotNetAstNodeId.StructureDeclaration: {
					if (builder.Length > 0)
						builder.Insert(0, '+');
					var name = ((TypeDeclaration)currentAstNode).Name;
					builder.Insert(0, name?.Text ?? "?");
					break;
				}
			}

			currentAstNode = currentAstNode.Parent;
		}

		builder.Insert(0, " ");
		builder.Insert(0, AstNode.GetType().Name);

		return builder.ToString();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IAstNode"/> for the declaration.
	/// </summary>
	public IAstNode AstNode { get; }

	/// <summary>
	/// The string key that identifies the declaration.
	/// </summary>
	public string Key { get; }

	/// <summary>
	/// The offset version range at which the declaration occurs.
	/// </summary>
	public ITextVersionRange VersionRange { get; }

}
