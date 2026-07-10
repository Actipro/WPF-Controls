using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging;

/// <summary>
/// Provides a pointer event sink that is used to handle clicks in the indicator margin.
/// </summary>
internal class DebuggingPointerInputEventSink : IEditorViewPointerInputEventSink {

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	void IEditorViewPointerInputEventSink.NotifyPointerEntered(IEditorView view, InputPointerEventArgs e) { /* no-op */ }

	void IEditorViewPointerInputEventSink.NotifyPointerExited(IEditorView view, InputPointerEventArgs e) { /* no-op */ }

	void IEditorViewPointerInputEventSink.NotifyPointerHovered(IEditorView view, InputPointerEventArgs e) { /* no-op */ }

	void IEditorViewPointerInputEventSink.NotifyPointerMoved(IEditorView view, InputPointerEventArgs e) { /* no-op */ }

	void IEditorViewPointerInputEventSink.NotifyPointerPressed(IEditorView view, InputPointerButtonEventArgs e)
		=> OnViewPointerPressed(view, e);

	void IEditorViewPointerInputEventSink.NotifyPointerReleased(IEditorView view, InputPointerButtonEventArgs e) { /* no-op */ }

	void IEditorViewPointerInputEventSink.NotifyPointerWheel(IEditorView view, InputPointerWheelEventArgs e) { /* no-op */ }

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a pointer button is pressed over the specified <see cref="IEditorView"/>.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnViewPointerPressed(IEditorView view, InputPointerButtonEventArgs e) {
		if (!e.Handled) {
			// Get a hit test result
			var hitTestResult = view.SyntaxEditor.HitTest(e.GetPosition(view.VisualElement));
			if ((hitTestResult is { Type: HitTestResultType.ViewMargin, ViewLine: not null }) && (hitTestResult.ViewMargin?.Key == EditorViewMarginKeys.Indicator)) {
				// Remove all breakpoints that start on the view line
				var removedCount = (view.SyntaxEditor.Document.IndicatorManager.Breakpoints.RemoveAll(tr => {
					var translatedBreakpointRange = tr.VersionRange.Translate(view.CurrentSnapshot);
					return !translatedBreakpointRange.HasValue || hitTestResult.ViewLine.TextRange.IntersectsWith(translatedBreakpointRange.Value.StartOffset);
				}));
				if (removedCount == 0) {
					// No breakpoints were removed so add one
					if (hitTestResult.Snapshot is not null)
						DebuggingHelper.ToggleBreakpoint(new TextSnapshotOffset(hitTestResult.Snapshot, hitTestResult.Offset), isEnabled: true);
				}
			}
		}
	}

}
