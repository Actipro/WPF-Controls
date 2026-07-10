using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsLocations;

/// <summary>
/// A custom factory implementation that creates <see cref="IEditorViewMargin"/> objects for use within an <see cref="IEditorView"/>.
/// </summary>
public class CustomMarginFactory : IEditorViewMarginFactory {

	/// <inheritdoc cref="IEditorViewMarginFactory.CreateMargins"/>
	public IEditorViewMarginCollection CreateMargins(IEditorView view) {
		return new EditorViewMarginCollection {
			// Add four margins in the scrollable area
			new CustomMargin(EditorViewMarginPlacement.ScrollableLeft),
			new CustomMargin(EditorViewMarginPlacement.ScrollableTop),
			new CustomMargin(EditorViewMarginPlacement.ScrollableRight),
			new CustomMargin(EditorViewMarginPlacement.ScrollableBottom),

			// Add four margins in the fixed area
			new CustomMargin(EditorViewMarginPlacement.FixedLeft),
			new CustomMargin(EditorViewMarginPlacement.FixedTop),
			new CustomMargin(EditorViewMarginPlacement.FixedRight),
			new CustomMargin(EditorViewMarginPlacement.FixedBottom)
		};
	}

}
