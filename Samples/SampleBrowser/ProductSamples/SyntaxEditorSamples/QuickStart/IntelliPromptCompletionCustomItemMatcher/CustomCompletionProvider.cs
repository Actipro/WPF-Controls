using System;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionCustomItemMatcher {
	
	/// <summary>
	/// A provider that can handle requests for display of an IntelliPrompt completion list.
	/// </summary>
	public class CustomCompletionProvider : CompletionProviderBase {
		
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
			// The items for this completion list are hard coded in this sample and
			// are simply meant to illustrate the rich features of the SyntaxEditor completion list.
			// When implementing a real language, you should vary the items based
			// on the current context of the caret.
			//

			// Create a session
			var session = new CompletionSession();
			session.AllowedCharacters.Add('.');
			session.CanCommitWithoutPopup = canCommitWithoutPopup;

			// Insert the custom completion item matcher as the second thing in the list so that starts-with continues to match first
			session.ItemMatchers.Insert(1, new CustomCompletionItemMatcher());

			// Add some items
			session.Items.Add(new CompletionItem("CUSTOMER.FIRST_NAME", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			session.Items.Add(new CompletionItem("CUSTOMER.LAST_NAME", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			session.Items.Add(new CompletionItem("EMPLOYEE.CELL_NUMBER", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			session.Items.Add(new CompletionItem("EMPLOYEE.FIRST_NAME", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			session.Items.Add(new CompletionItem("EMPLOYEE.LAST_NAME", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			session.Items.Add(new CompletionItem("EMPLOYEE.ID", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			session.Items.Add(new CompletionItem("LOCATION.ID", new CommonImageSourceProvider(CommonImageKind.PropertyPublic)));
			
			if (session.Items.Count > 0) {
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

