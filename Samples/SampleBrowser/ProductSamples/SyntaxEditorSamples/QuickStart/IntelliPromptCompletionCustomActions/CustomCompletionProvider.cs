using System;
using System.ComponentModel;
using System.Windows;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionCustomActions {
	
	/// <summary>
	/// A provider that can handle requests for display of an IntelliPrompt completion list.
	/// </summary>
	public class CustomCompletionProvider : CompletionProviderBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the session is closed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CancelEventArgs"/> that contains the event data.</param>
		private void OnSessionClosed(object sender, CancelEventArgs e) {
			var session = (ICompletionSession)sender;

			// Detach from session events
			session.Closed -= OnSessionClosed;
			session.Committing -= OnSessionCommitting;
		}

		/// <summary>
		/// Occurs when the session is about to commit.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CancelEventArgs"/> that contains the event data.</param>
		private void OnSessionCommitting(object sender, CancelEventArgs e) {
			var session = (ICompletionSession)sender;
			
			// If not cancelled and there is a selection...
			if ((!e.Cancel) && (session.Selection != null)) {
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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Requests that an <see cref="ICompletionSession"/> be opened for the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that will host the session.</param>
		/// <param name="canCommitWithoutPopup">Whether the session can immediately commit if a single match is made when the session is opened, commonly known as "complete word" functionality.</param>
		/// <returns>
		/// <c>true</c> if a session was opened; otherwise, <c>false</c>.
		/// </returns>
		public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup) {

			//
			// IMPORTANT NOTE:
			// The items for this completion list are hardcoded in this sample and
			// are simply meant to illustrate the rich features of the SyntaxEditor completion list.
			// When implementing a real language, you should vary the items based
			// on the current context of the caret.
			//

			// Create a session
			var session = new CompletionSession();
			session.CanCommitWithoutPopup = canCommitWithoutPopup;
			session.MatchOptions = CompletionMatchOptions.TargetsDisplayText;

			// Add some items
			session.Items.Add(new CompletionItem("actiprosoftware.com", new CommonImageSourceProvider(CommonImageKind.PropertyPublic), 
				new DirectContentProvider("Inserts a URL to Actipro's web site."), "http://www.actiprosoftware.com", null));
			session.Items.Add(new CompletionItem("microsoft.com", new CommonImageSourceProvider(CommonImageKind.PropertyPublic), 
				new DirectContentProvider("Inserts a URL to Microsoft's web site."), "http://www.microsoft.com", null));
			session.Items.Add(new CompletionItem("Open URL dialog...", new CommonImageSourceProvider(CommonImageKind.ClassPublic), 
				new DirectContentProvider("Opens a URL Builder dialog."), null, null, "MsgBox"));

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

}

