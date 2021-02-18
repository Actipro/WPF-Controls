using System;
using System.Text;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {
    
	/// <summary>
	/// Stores information about a parsed declaration and its snapshot offset.
	/// </summary>
	public class CodeLensDeclaration {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CodeLensDeclaration</c> class.
		/// </summary>
		/// <param name="snapshotOffset">The snapshot offset at which the declaration occurs.</param>
		/// <param name="astNode">The <see cref="IAstNode"/> for the declaration.</param>
		public CodeLensDeclaration(TextSnapshotOffset snapshotOffset, IAstNode astNode) {
			if (astNode == null)
				throw new ArgumentNullException("astNode");

			// Initialize
			this.VersionRange = new TextSnapshotRange(snapshotOffset).ToVersionRange(TextRangeTrackingModes.Default);
			this.AstNode = astNode;
			this.CreateKey();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates the <see cref="Key"/>.
		/// </summary>
		private void CreateKey() {
			var builder = new StringBuilder();

			var currentAstNode = this.AstNode;
			while (currentAstNode != null) {
				switch (currentAstNode.Id) {
					case DotNetAstNodeId.NamespaceDeclaration: {
						if (builder.Length > 0)
							builder.Insert(0, '.');
						var name = ((NamespaceDeclaration)currentAstNode).Name;
						builder.Insert(0, (name != null ? name.ToString() : "?"));
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
						builder.Insert(0, (name != null ? name.Text : "?"));
						break;
					}
				}

				currentAstNode = currentAstNode.Parent;
			}

			builder.Insert(0, " ");
			builder.Insert(0, this.AstNode.GetType().Name);

			this.Key = builder.ToString();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="IAstNode"/> for the declaration.
		/// </summary>
		/// <value>The <see cref="IAstNode"/> for the declaration.</value>
		public IAstNode AstNode { get; private set; }

		/// <summary>
		/// Gets the string key that identifies the declaration.
		/// </summary>
		/// <value>The string key that identifies the declaration.</value>
		public string Key { get; private set; }

		/// <summary>
		/// Gets the offset version range at which the declaration occurs.
		/// </summary>
		/// <value>The offset version range at which the declaration occurs.</value>
		public ITextVersionRange VersionRange { get; private set; }

	}
	
}
