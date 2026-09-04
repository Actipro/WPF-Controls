using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsCustom;

/// <summary>
/// A custom factory implementation that creates <see cref="IEditorViewMargin"/> objects for use within an <see cref="IEditorView"/>.
/// </summary>
public class CustomMarginFactory : IEditorViewMarginFactory {

	/// <inheritdoc cref="IEditorViewMarginFactory.CreateMargins"/>
	public IEditorViewMarginCollection CreateMargins(IEditorView view) {
		return new EditorViewMarginCollection {
			new CustomMargin(view)
		};
	}

}
