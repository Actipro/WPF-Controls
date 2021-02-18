using System;
using ActiproSoftware.Products.SyntaxEditor;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsCustom {

	/// <summary>
	/// A custom factory implementation that creates <see cref="IEditorViewMargin"/> objects for use within an <see cref="IEditorView"/>.
	/// </summary>
	public class CustomMarginFactory : IEditorViewMarginFactory {

		/// <summary>
		/// Creates the collection of <see cref="IEditorViewMargin"/> objects to place within a <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that will host the margins.</param>
		/// <returns>A <see cref="IEditorViewMarginCollection"/> containing the <see cref="IEditorViewMargin"/> objects that were created.</returns>
		public IEditorViewMarginCollection CreateMargins(IEditorView view) {
			IEditorViewMarginCollection margins = new EditorViewMarginCollection();
			margins.Add(new CustomMargin(view));
			return margins;
		}
		
	}
}
