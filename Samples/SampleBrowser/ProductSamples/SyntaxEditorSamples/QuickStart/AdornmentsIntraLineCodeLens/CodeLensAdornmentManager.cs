using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using AstImpl = ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {

    /// <summary>
	/// Represents an adornment manager for a view that renders intra-text notes.
    /// </summary>
    public class CodeLensAdornmentManager : IntraLineAdornmentManagerBase<IEditorView, CodeLensTag> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("CodeLens", new Ordering(AdornmentLayerDefinitions.Highlight.Key, OrderPlacement.Before));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CodeLensAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public CodeLensAdornmentManager(IEditorView view) : base(view, layerDefinition) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		protected override void AddAdornment(ITextViewLine viewLine, TagSnapshotRange<CodeLensTag> tagRange) {
			if (tagRange.Tag.Declaration.AstNode == null)
				return;

			// Build the text
			string text = null;
			switch (tagRange.Tag.Declaration.AstNode.Id) {
				case AstImpl.DotNetAstNodeId.ClassDeclaration:
					text = "class ";
					break;
				case AstImpl.DotNetAstNodeId.DelegateDeclaration:
					text = "delegate ";
					break;
				case AstImpl.DotNetAstNodeId.EnumerationDeclaration:
					text = "enum ";
					break;
				case AstImpl.DotNetAstNodeId.InterfaceDeclaration:
					text = "interface ";
					break;
				case AstImpl.DotNetAstNodeId.StructureDeclaration:
					text = "struct ";
					break;
				default:
					return;
			}
			var typeDeclAstNode = (AstImpl.TypeDeclaration)tagRange.Tag.Declaration.AstNode;
			text += typeDeclAstNode.Name.Text;
			
			// Create a link
			var link = new Hyperlink();
			link.Focusable = false;
			link.Foreground = Brushes.Gray;
			link.Inlines.Add("Documentation");
			link.Click += (e, s) => {
				MessageBox.Show("Show " + typeDeclAstNode.Name.Text + " documentation here.");
			};

			// Create the text block
			var textBlock = new TextBlock();
			textBlock.Foreground = Brushes.Gray;
			textBlock.FontFamily = new FontFamily("Segoe UI");
			textBlock.FontSize = 10;
			textBlock.Inlines.Add(text + " - ");
			textBlock.Inlines.Add(link);
			textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

			// Determine the adornment location
			var charBounds = viewLine.GetCharacterBounds(tagRange.SnapshotRange.StartOffset);
			var location = new Point(charBounds.Left, charBounds.Top - viewLine.TopMargin + ((viewLine.TopMargin - textBlock.DesiredSize.Height) / 2.0));

			// Add the adornment
			this.AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, textBlock, location, tagRange.Tag.Key, null);
		}

		/// <summary>
		/// Occurs when the manager is closed and detached from the view.
		/// </summary>
		/// <remarks>
		/// Overrides should release any event handlers set up in the manager's constructor.
		/// </remarks>
		protected override void OnClosed() {
			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

			// Call the base method
			base.OnClosed();
		}

    }
	
}
