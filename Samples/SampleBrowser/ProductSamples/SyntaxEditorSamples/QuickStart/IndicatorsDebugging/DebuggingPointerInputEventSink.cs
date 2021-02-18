using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {
	
	/// <summary>
	/// Provides a pointer event sink that is used to handle clicks in the indicator margin.
	/// </summary>
	internal class DebuggingPointerInputEventSink : IEditorViewPointerInputEventSink {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the pointer enters the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerEntered(IEditorView view, InputPointerEventArgs e) {}
		
		/// <summary>
		/// Occurs when the pointer leaves the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerExited(IEditorView view, InputPointerEventArgs e) {}
		
		/// <summary>
		/// Occurs when the pointer hovers over the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerHovered(IEditorView view, InputPointerEventArgs e) {}

		/// <summary>
		/// Occurs when the pointer moves within the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerMoved(IEditorView view, InputPointerEventArgs e) {}
		
		/// <summary>
		/// Occurs when a pointer button is pressed over the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerButtonEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerPressed(IEditorView view, InputPointerButtonEventArgs e) {
			this.OnViewPointerPressed(view, e);
		}
		
		/// <summary>
		/// Occurs when a pointer button is released over the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerButtonEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerReleased(IEditorView view, InputPointerButtonEventArgs e) {}
		
		/// <summary>
		/// Occurs when the pointer wheel is turned over the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerWheelEventArgs"/> that contains the event data.</param>
		void IEditorViewPointerInputEventSink.NotifyPointerWheel(IEditorView view, InputPointerWheelEventArgs e) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a pointer button is pressed over the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
		/// <param name="e">The <see cref="InputPointerButtonEventArgs"/> that contains the event data.</param>
		protected virtual void OnViewPointerPressed(IEditorView view, InputPointerButtonEventArgs e) {
			if ((e != null) && (!e.Handled)) {
				// Get a hit test result
				var hitTestResult = view.SyntaxEditor.HitTest(e.GetPosition(view.VisualElement));
				if ((hitTestResult.Type == HitTestResultType.ViewMargin) && (hitTestResult.ViewMargin.Key == EditorViewMarginKeys.Indicator) && (hitTestResult.ViewLine != null)) {
					// Remove all breakpoints that start on the view line
					if (view.SyntaxEditor.Document.IndicatorManager.Breakpoints.RemoveAll(
						tr => hitTestResult.ViewLine.TextRange.IntersectsWith(tr.VersionRange.Translate(view.CurrentSnapshot).StartOffset)) == 0) {

						// No breakpoints were removed so add one
						DebuggingHelper.ToggleBreakpoint(new TextSnapshotOffset(hitTestResult.Snapshot, hitTestResult.Offset), true);
					}
				}
			}
		}
		
	}

}

