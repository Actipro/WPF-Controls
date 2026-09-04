using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionCustomActions;

/// <summary>
/// A provider that can handle requests for display of an IntelliPrompt completion list.
/// </summary>
public class CustomCompletionProvider : CompletionProviderBase {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnSessionClosed(object? sender, CancelEventArgs e) {
		var session = (ICompletionSession)sender!;

		// Detach from session events
		session.Closed -= OnSessionClosed;
		session.Committing -= OnSessionCommitting;
	}

	private void OnSessionCommitting(object? sender, CancelEventArgs e) {
		var session = (ICompletionSession)sender!;

		// The session is about to commit.  If not cancelled and there is a selection...
		if ((!e.Cancel) && (session.Selection is not null)) {
			// Get the selected item
			var item = session.Selection.Item;

			// If the dialog item was selected...
			if ("MsgBox".Equals(item.Tag)) {
				// Cancel the auto-complete
				e.Cancel = true;

				// Show a messagebox instead
				MessageBox.Show("Show a dialog for building a URL here.  Note that auto-complete was cancelled in code-behind.", "URL Builder", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup) {

		//
		// IMPORTANT NOTE:
		//   The items for this completion list are hardcoded in this sample and are simply meant
		//   to illustrate the rich features of the SyntaxEditor completion list.  When implementing
		//   a real language, you should vary the items based on the current context of the caret.
		//

		// Create a session
		var session = new CompletionSession {
			CanCommitWithoutPopup = canCommitWithoutPopup,
			MatchOptions = CompletionMatchOptions.TargetsDisplayText
		};

		// Add some items
		session.Items.Add(new CompletionItem("actiprosoftware.com", new CommonImageSourceProvider(CommonImageKind.PropertyPublic),
			new DirectContentProvider("Inserts a URL to Actipro's web site."), "http://www.actiprosoftware.com", autoCompletePostText: null));
		session.Items.Add(new CompletionItem("microsoft.com", new CommonImageSourceProvider(CommonImageKind.PropertyPublic),
			new DirectContentProvider("Inserts a URL to Microsoft's web site."), "http://www.microsoft.com", autoCompletePostText: null));
		session.Items.Add(new CompletionItem("Open URL dialog...", new CommonImageSourceProvider(CommonImageKind.ClassPublic),
			new DirectContentProvider("Opens a URL Builder dialog."), autoCompletePreText: null, autoCompletePostText: null, tag: "MsgBox"));

		if (session.Items.Count > 0) {
			// Attach to session events
			session.Closed += OnSessionClosed;
			session.Committing += OnSessionCommitting;

			// Ensure the caret is visible
			view.Scroller.ScrollToCaret();

			// Ensure the items are sorted and open the session
			session.SortItems();
			session.Open(view);
			return true;
		}

		return false;
	}

}
