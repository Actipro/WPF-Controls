using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLineOverlay;

/// <summary>
/// Provides a custom <see cref="IEditAction"/> implementation that displays a 'Go To Line' overlay pane.
/// </summary>
public class GoToLineAction() : EditActionBase("Go To Line") {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void Execute(IEditorView view)
		=> GoToLineOverlayPane.Show(view);

}
