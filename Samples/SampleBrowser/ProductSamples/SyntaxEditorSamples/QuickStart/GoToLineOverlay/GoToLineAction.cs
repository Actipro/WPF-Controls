using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLineOverlay {

	/// <summary>
	/// Provides a custom <see cref="IEditAction"/> implementation that displays a 'Go To Line' overlay pane.
	/// </summary>
	public class GoToLineAction : EditActionBase {
        
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public GoToLineAction()
			: base("Go To Line") { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Executes the edit action in the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> in which to execute the edit action.</param>
		public override void Execute(IEditorView view) {
			GoToLineOverlayPane.Show(view);
		}

	}
}