using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;  // For context-related types

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09 {
	
	/// <summary>
	/// Provides IntelliPrompt quick info data for the <c>Simple</c> language.
	/// </summary>
	public class SimpleQuickInfoProvider : QuickInfoProviderBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleQuickInfoProvider</c> class.
		/// </summary>
		public SimpleQuickInfoProvider() : base("Simple", 
			new Ordering(QuickInfoProviderKeys.CollapsedRegion, OrderPlacement.After), new Ordering(QuickInfoProviderKeys.SquiggleTag, OrderPlacement.After)) {

			//
			// NOTE: Notice the Orderings that were passed into the base constructor... since we are using other
			//   quick info providers, this ensures that those take precedence over this one
			//

		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
					
		/// <summary>
		/// Gets the context <c>Type</c> objects that are supported by this provider, which are the list of custom types
		/// that are possibly returned by the <see cref="QuickInfoProviderBase.GetContext(IHitTestResult)"/> methods.
		/// </summary>
		/// <value>The context <c>Type</c> objects that are supported by this provider.</value>
		protected override IEnumerable<Type> ContextTypes { 
			get {
				return new Type[] { typeof(SimpleContext) };
			}
		}
		
		/// <summary>
		/// Returns an object describing the quick info context for the specified text offset, if any.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> in which the offset is located.</param>
		/// <param name="offset">The text offset to examine.</param>
		/// <returns>
		/// An object describing the quick info context for the specified text offset, if any.
		/// A <c>null</c> value indicates that no context is available.
		/// </returns>
		/// <remarks>
		/// This method is called in response to keyboard events.
		/// </remarks>
		public override object GetContext(IEditorView view, int offset) {
			// Get the context factory service
			SimpleContextFactory contextFactory = view.SyntaxEditor.Document.Language.GetService<SimpleContextFactory>();
			if (contextFactory != null) {
				// Get a context
				return contextFactory.CreateContext(new TextSnapshotOffset(view.CurrentSnapshot, offset), false);
			}
			return null;
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
			SimpleContext languageContext = context as SimpleContext;
			if ((languageContext != null) && (languageContext.InitializationSnapshotRange.HasValue)) {
				// Create a session and assign a context that can be used to identify it
				QuickInfoSession session = new QuickInfoSession();
				session.Context = context;

				switch (languageContext.Type) {
					case SimpleContextType.FunctionReference:
						// When hovering over a function reference...
						if (languageContext.TargetFunction != null)
							session.Content = new FunctionContentProvider(view.HighlightingStyleRegistry, languageContext.TargetFunction, true, view.DefaultBackgroundColor).GetContent();
						break;
				}

				// If content was created...
				if (session.Content != null) {
					// Ensure the caret is visible (only if not tracking pointer input)
					if (!this.CanTrackPointerInput)
						view.Scroller.ScrollToCaret();

					// Open the session
					session.Open(view, languageContext.InitializationSnapshotRange.Value);
					return true;
				}
			}
			return false;
		}
	}

}

