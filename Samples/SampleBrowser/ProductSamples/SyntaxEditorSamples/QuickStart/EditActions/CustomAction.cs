using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditActions;

/// <summary>
/// Provides a custom <see cref="IEditAction"/> implementation that inserts a <c>custom</c> tag surrounding the selected text.
/// </summary>
public class CustomAction() : Windows.Controls.SyntaxEditor.Implementation.EditActionBase("Custom") {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void Execute(IEditorView view)
		=> view.InsertSurroundingText("<custom>", "</custom>");

}
