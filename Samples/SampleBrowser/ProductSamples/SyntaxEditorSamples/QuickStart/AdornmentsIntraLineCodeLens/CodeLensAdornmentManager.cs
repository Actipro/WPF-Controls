using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using System.Windows.Documents;
using AstImpl = ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens;

/// <summary>
/// Represents an adornment manager for a view that renders intra-text notes.
/// </summary>
/// <param name="view">The view to which this manager is attached.</param>
public class CodeLensAdornmentManager(IEditorView view) : IntraLineAdornmentManagerBase<IEditorView, CodeLensTag>(view, _layerDefinition) {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("CodeLens", new Ordering(AdornmentLayerDefinitions.Highlight.Key, OrderPlacement.Before));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(ITextViewLine viewLine, TagSnapshotRange<CodeLensTag> tagRange) {
		if (tagRange.Tag.Declaration.AstNode is not AstImpl.TypeDeclaration { Name: not null } typeDeclAstNode)
			return;

		// Build the text
		var text = typeDeclAstNode.Id switch {
			AstImpl.DotNetAstNodeId.ClassDeclaration => "class ",
			AstImpl.DotNetAstNodeId.DelegateDeclaration => "delegate ",
			AstImpl.DotNetAstNodeId.EnumerationDeclaration => "enum ",
			AstImpl.DotNetAstNodeId.InterfaceDeclaration => "interface ",
			AstImpl.DotNetAstNodeId.StructureDeclaration => "struct ",
			_ => null
		};
		if (text is null)
			return;

		text += typeDeclAstNode.Name.Text;

		// Create a link
		var link = new Hyperlink {
			Focusable = false,
			Foreground = Brushes.Gray
		};
		link.Inlines.Add("Documentation");
		link.Click += (_, _) => {
			MessageBox.Show($"Show {typeDeclAstNode.Name.Text} documentation here.");
		};

		// Create the text block
		var textBlock = new TextBlock {
			Foreground = Brushes.Gray,
			FontFamily = new FontFamily("Segoe UI"),
			FontSize = 10
		};
		textBlock.Inlines.Add(text + " - ");
		textBlock.Inlines.Add(link);
		textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

		// Determine the adornment location
		var charBounds = viewLine.GetCharacterBounds(tagRange.SnapshotRange.StartOffset)
			?? new TextBounds();
		var location = new Point(
			charBounds.Left,
			charBounds.Top - viewLine.TopMargin + ((viewLine.TopMargin - textBlock.DesiredSize.Height) / 2.0)
		);

		// Add the adornment
		AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, textBlock, location, tagRange.Tag.Key, removedCallback: null);
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
