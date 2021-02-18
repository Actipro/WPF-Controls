using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptQuickInfo {
	
	/// <summary>
	/// Implements a quick info provider that demonstrates automated quick info session management when
	/// hovering over text in the editor and also when hovering over the line number margin.
	/// </summary>
	public class CustomQuickInfoProvider : QuickInfoProviderBase {
			
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomQuickInfoProvider</c> class.
		/// </summary>
		public CustomQuickInfoProvider() : base("Custom") {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
		/// <summary>
		/// Contains context information for the text area.
		/// </summary>
		private class TextRangeContext {

			public TextRange Range;
				
			/// <summary>
			/// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="Object"/>.
			/// </summary>
			/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Object"/>.</param>
			/// <returns>
			/// true if the specified <see cref="Object"/> is equal to the current <see cref="Object"/>; otherwise, false.
			/// </returns>
			/// <exception cref="NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
			public override bool Equals(object obj) {
				TextRangeContext context = obj as TextRangeContext;
				if (context != null)
					return context.Range.Equals(this.Range);
				else
					return false;
			}
			
			/// <summary>
			/// Serves as a hash function for a particular type.
			/// </summary>
			/// <returns>
			/// A hash code for the current <see cref="Object"/>.
			/// </returns>
			public override int GetHashCode() {
				return this.Range.GetHashCode();
			}

		}

		/// <summary>
		/// Contains context information for the line number margin.
		/// </summary>
		private class LineNumberMarginContext {

			public int LineIndex;
				
			/// <summary>
			/// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="Object"/>.
			/// </summary>
			/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Object"/>.</param>
			/// <returns>
			/// true if the specified <see cref="Object"/> is equal to the current <see cref="Object"/>; otherwise, false.
			/// </returns>
			/// <exception cref="NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
			public override bool Equals(object obj) {
				LineNumberMarginContext context = obj as LineNumberMarginContext;
				if (context != null)
					return context.LineIndex.Equals(this.LineIndex);
				else
					return false;
			}
			
			/// <summary>
			/// Serves as a hash function for a particular type.
			/// </summary>
			/// <returns>
			/// A hash code for the current <see cref="Object"/>.
			/// </returns>
			public override int GetHashCode() {
				return this.LineIndex.GetHashCode();
			}

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
					
		/// <summary>
		/// Gets the context <c>Type</c> objects that are supported by this provider, which are the list of custom types
		/// that are possibly returned by the <see cref="GetContext(IHitTestResult)"/> methods.
		/// </summary>
		/// <value>The context <c>Type</c> objects that are supported by this provider.</value>
		protected override IEnumerable<Type> ContextTypes { 
			get {
				return new Type[] { typeof(TextRangeContext), typeof(LineNumberMarginContext) };
			}
		}

		/// <summary>
		/// Returns an object describing the quick info context for the specified <see cref="IHitTestResult"/>, if any.
		/// </summary>
		/// <param name="hitTestResult">The <see cref="IHitTestResult"/> to examine.</param>
		/// <returns>
		/// An object describing the quick info context for the specified <see cref="IHitTestResult"/>, if any.
		/// A <see langword="null"/> value indicates that no context is available.
		/// </returns>
		/// <remarks>
		/// This method is called in response to mouse events.
		/// </remarks>
		public override object GetContext(IHitTestResult hitTestResult) {
			switch (hitTestResult.Type) {
				case HitTestResultType.ViewTextAreaOverCharacter:
					// Over a character... this is what the default base method implementation does:
					return this.GetContext(hitTestResult.View, hitTestResult.Offset);
				case HitTestResultType.ViewMargin:
					// Over a margin
					if (hitTestResult.ViewMargin.Key == EditorViewMarginKeys.LineNumber) {
						// Over the line number margin
						LineNumberMarginContext context = new LineNumberMarginContext();
						context.LineIndex = hitTestResult.Position.Line;
						return context;
					}
					break;
			}

			// No context
			return null;
		}
		
		/// <summary>
		/// Returns an object describing the quick info context for the specified text offset, if any.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> in which the offset is located.</param>
		/// <param name="offset">The text offset to examine.</param>
		/// <returns>
		/// An object describing the quick info context for the specified text offset, if any.
		/// A <see langword="null"/> value indicates that no context is available.
		/// </returns>
		/// <remarks>
		/// This method is called in response to keyboard events.
		/// </remarks>
		public override object GetContext(IEditorView view, int offset) {
			// Get the range of the current word
			TextRangeContext context = new TextRangeContext();
			context.Range = view.CurrentSnapshot.GetWordTextRange(offset);
			return context;
		}
		
		/// <summary>
		/// Requests that an <see cref="IQuickInfoSession"/> be opened for the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that will host the session.</param>
		/// <param name="context">A context object returned by <see cref="GetContext"/>.</param>
		/// <returns>
		/// <c>true</c> if a session was opened; otherwise, <c>false</c>.
		/// </returns>
		protected override bool RequestSession(IEditorView view, object context) {
			// Create a session and assign a context that can be used to identify it
			QuickInfoSession session = new QuickInfoSession();
			session.Context = context;

			TextRangeContext textRangeContext = context as TextRangeContext;
			if (textRangeContext != null) {
				// Get a reader initialized to the offset
				ITextSnapshotReader reader = view.CurrentSnapshot.GetReader(textRangeContext.Range.StartOffset);
				IToken token = reader.Token;
				if (token != null) {
					// Create some marked-up content indicating the token at the offset and the line it's on
					session.Content = new HtmlContentProvider(
						String.Format("Target word: <b>{0}</b><br/>Token: <b>{1}</b><br/><span style=\"color: Green;\">Found on line {2}.</span>",
						HtmlContentProvider.Escape(view.CurrentSnapshot.GetSubstring(textRangeContext.Range)),
						token.Key,
						view.OffsetToPosition(textRangeContext.Range.StartOffset).Line + 1), view.DefaultBackgroundColor).GetContent();

					// Open the session
					session.Open(view, textRangeContext.Range);
					return true;
				}
			}
			else {
				LineNumberMarginContext marginContext = context as LineNumberMarginContext;
				if (marginContext != null) {
					// Create some marked-up content indicating the line number
					session.Content = new HtmlContentProvider(String.Format("Line number: <b>{0}</b>", marginContext.LineIndex + 1), view.DefaultBackgroundColor).GetContent();

					// Get the margin
					IEditorViewMargin margin = view.Margins[EditorViewMarginKeys.LineNumber];

					// Get the view line that contains the line
					ITextViewLine viewLine = view.GetViewLine(new TextPosition(marginContext.LineIndex, 0));
					if ((margin != null) && (viewLine != null)) {
						// Get line bounds relative to the margin
						Rect bounds = view.TransformFromTextArea(viewLine.Bounds);
						bounds.X = 0;
						bounds.Width = margin.VisualElement.RenderSize.Width;

						// Open the session
						session.Open(view, PlacementMode.Bottom, view.VisualElement, bounds);
						return true;
					}
				}
			}
			return false;
		}
	}


}
