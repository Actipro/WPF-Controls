using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;  // For context-related types
using ActiproSoftware.Text;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09;

/// <summary>
/// Provides IntelliPrompt quick info data for the <c>Simple</c> language.
/// </summary>
public class SimpleQuickInfoProvider : QuickInfoProviderBase {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleQuickInfoProvider() : base("Simple",
		new Ordering(QuickInfoProviderKeys.CollapsedRegion, OrderPlacement.After), new Ordering(QuickInfoProviderKeys.SquiggleTag, OrderPlacement.After)) {

		//
		// NOTE: Notice the Orderings that were passed into the base constructor... since we are using other
		//   quick info providers, this ensures that those take precedence over this one
		//

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IEnumerable<Type> ContextTypes
		=> [typeof(SimpleContext)];

	/// <inheritdoc/>
	public override object? GetContext(IEditorView view, int offset) {
		// Get the context factory service
		var contextFactory = view.SyntaxEditor.Document.Language.GetService<SimpleContextFactory>();

		// Get a context
		return contextFactory?.CreateContext(new TextSnapshotOffset(view.CurrentSnapshot, offset), includeArgumentInfo: false);
	}

	/// <inheritdoc/>
	protected override bool RequestSession(IEditorView view, object context) {
		if (context is SimpleContext { InitializationSnapshotRange: not null } languageContext) {
			// Create a session with a context that can be used to identify it
			var session = new QuickInfoSession(context);

			switch (languageContext.Type) {
				case SimpleContextType.FunctionReference:
					// When hovering over a function reference...
					if (languageContext.TargetFunction is not null)
						session.Content = new FunctionContentProvider(view.HighlightingStyleRegistry, languageContext.TargetFunction, includeImage: true, view.DefaultBackgroundColor).GetContent();
					break;
			}

			// If content was created...
			if (session.Content is not null) {
				// Ensure the caret is visible (only if not tracking pointer input)
				if (!CanTrackPointerInput)
					view.Scroller.ScrollToCaret();

				// Open the session
				session.Open(view, languageContext.InitializationSnapshotRange);
				return true;
			}
		}
		return false;
	}

}
