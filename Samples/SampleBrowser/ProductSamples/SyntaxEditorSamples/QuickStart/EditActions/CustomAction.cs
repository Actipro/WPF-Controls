using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditActions {

	/// <summary>
	/// Provides a custom <see cref="IEditAction"/> implementation that inserts a <c>custom</c> tag surrounding the selected text.
	/// </summary>
	public class CustomAction : ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation.EditActionBase {
        
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>CustomAction</c> class.
		/// </summary>
		public CustomAction() : base("Custom") {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Executes the edit action in the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> in which to execute the edit action.</param>
		public override void Execute(IEditorView view) {
			view.InsertSurroundingText("<custom>", "</custom>");
		}
		
	}
}