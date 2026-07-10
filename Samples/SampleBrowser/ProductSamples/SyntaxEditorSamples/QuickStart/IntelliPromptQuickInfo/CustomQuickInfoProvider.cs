using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptQuickInfo;

/// <summary>
/// Implements a quick info provider that demonstrates automated quick info session management when
/// hovering over text in the editor and also when hovering over the line number margin.
/// </summary>
public class CustomQuickInfoProvider() : QuickInfoProviderBase("Custom") {

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	#region CustomQuickInfoSession

	/// <summary>
	/// Represents an IntelliPrompt quick info session.
	/// </summary>
	/// <param name="context">An object that describes the context in which the quick info popup is displayed.</param>
	private class CustomQuickInfoSession(object context) : QuickInfoSession(context) {

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		#pragma warning disable CA1822 // Mark members as static
		/// <summary>
		/// Returns the placement rectangle for the line number margin quick info session.
		/// </summary>
		/// <param name="view">The containing <see cref="IEditorView"/>.</param>
		/// <param name="marginContext">The margin context.</param>
		public Rect? GetLineNumberMarginPlacementRectangle(IEditorView? view, LineNumberMarginContext marginContext) {
			if ((view is not null) && (marginContext is not null)) {
				// Get the margin
				var margin = view.Margins[EditorViewMarginKeys.LineNumber];

				// Get the view line that contains the line
				var viewLine = view.GetViewLine(new TextPosition(marginContext.LineIndex, character: 0));
				if ((margin is not null) && (viewLine is { Visibility: not TextViewLineVisibility.Hidden })) {
					// Get line bounds relative to the margin
					var bounds = view.TransformFromTextArea(viewLine.Bounds);
					bounds.X = 0;
					bounds.Width = margin.VisualElement.ActualWidth;
					return bounds;
				}
			}

			return null;
		}
		#pragma warning restore CA1822

		/// <inheritdoc/>
		public override void Reposition() {
			if (IsOpen && (Context is LineNumberMarginContext marginContext)) {
				// Get the placement rectangle
				var placementRectangle = GetLineNumberMarginPlacementRectangle(View, marginContext);
				if (placementRectangle.HasValue) {
					// Update the session's placement rectangle
					PlacementRectangle = placementRectangle.Value;
				}
				else {
					// Cancel if no placement rectangle is available
					Close(cancelled: true);
					return;
				}
			}

			base.Reposition();
		}

	}

	#endregion

	#region TextRangeContext

	/// <summary>
	/// Contains context information for the text area.
	/// </summary>
	private class TextRangeContext {

		public TextRange Range { get; set; }

		/// <inheritdoc/>
		#if NET
		public override bool Equals([NotNullWhen(true)] object? obj) {
		#else
		public override bool Equals(object? obj) {
		#endif
			return obj is TextRangeContext other
				&& Range.Equals(other.Range);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
			=> Range.GetHashCode();

	}

	#endregion

	#region LineNumberMarginContext

	/// <summary>
	/// Contains context information for the line number margin.
	/// </summary>
	private class LineNumberMarginContext {

		public int LineIndex { get; set; }

		/// <inheritdoc/>
		#if NET
		public override bool Equals([NotNullWhen(true)] object? obj) {
		#else
		public override bool Equals(object? obj) {
		#endif
			return obj is LineNumberMarginContext other
				&& LineIndex == other.LineIndex;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
			=> LineIndex.GetHashCode();

	}

	#endregion

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IEnumerable<Type> ContextTypes
		=> [typeof(TextRangeContext), typeof(LineNumberMarginContext)];

	/// <inheritdoc/>
	public override object? GetContext(IHitTestResult hitTestResult) {
		switch (hitTestResult.Type) {

			case HitTestResultType.ViewTextAreaOverCharacter when hitTestResult.View is not null:
				// Over a character... this is what the default base method implementation does:
				return GetContext(hitTestResult.View, hitTestResult.Offset);

			case HitTestResultType.ViewMargin when hitTestResult.ViewMargin is not null:
				// Over a margin... test if over the line number margin
				if (
					hitTestResult.ViewMargin.Key == EditorViewMarginKeys.LineNumber
					&& hitTestResult.Position is { } position
				) {
					return new LineNumberMarginContext() { LineIndex = position.Line };
				}
				break;
		}

		// No context
		return null;
	}

	/// <inheritdoc/>
	public override object? GetContext(IEditorView view, int offset) {
		// Get the range of the current word
		return new TextRangeContext {
			Range = view.CurrentSnapshot.GetWordTextRange(offset)
		};
	}

	/// <inheritdoc/>
	protected override bool RequestSession(IEditorView view, object context) {
		// Create a session and assign a context that can be used to identify it... a custom session type is used to support non-text range contexts
		var session = new CustomQuickInfoSession(context);

		switch (context) {
			case TextRangeContext textRangeContext: {
				// Get a reader initialized to the offset
				var reader = view.CurrentSnapshot.GetReader(textRangeContext.Range.StartOffset);
				var token = reader.Token;
				if (token is not null) {
					// Create some marked-up content indicating the token at the offset and the line it's on
					session.Content = new HtmlContentProvider(
						string.Format("Target word: <b>{0}</b><br/>Token: <b>{1}</b><br/><span style=\"color: Green;\">Found on line {2}.</span>",
							HtmlContentProvider.Escape(view.CurrentSnapshot.GetSubstring(textRangeContext.Range)),
							token.Key,
							view.OffsetToPosition(textRangeContext.Range.StartOffset).Line + 1
						),
						view.DefaultBackgroundColor
					).GetContent();

					// Open the session
					session.Open(view, textRangeContext.Range);
					return true;
				}
				break;
			}
			case LineNumberMarginContext marginContext: {
				// Create some marked-up content indicating the line number
				session.Content = new HtmlContentProvider(string.Format("Line number: <b>{0}</b>", marginContext.LineIndex + 1), view.DefaultBackgroundColor).GetContent();

				// Get the placement rectangle
				var placementRectangle = session.GetLineNumberMarginPlacementRectangle(view, marginContext);
				if (placementRectangle.HasValue) {
					// Open the session
					session.Open(view, PlacementMode.Bottom, view.VisualElement, placementRectangle.Value);
					return true;
				}
				break;
			}
		}

		return false;
	}

}
