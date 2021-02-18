using System;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionDescriptionTips {
	
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
			CompletionSession session = new CompletionSession();
			session.CanCommitWithoutPopup = canCommitWithoutPopup;

			// Add some items
			var highlightingStyleRegistry = view.HighlightingStyleRegistry;
			session.Items.Add(new CompletionItem("bool", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(bool))));
			session.Items.Add(new CompletionItem("byte", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(byte))));
			session.Items.Add(new CompletionItem("char", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(char))));
			session.Items.Add(new CompletionItem("double", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(double))));
			session.Items.Add(new CompletionItem("float", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(float))));
			session.Items.Add(new CompletionItem("int", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(int))));
			session.Items.Add(new CompletionItem("long", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(long))));
			session.Items.Add(new CompletionItem("short", new CommonImageSourceProvider(CommonImageKind.StructurePublic), new CustomContentProvider(highlightingStyleRegistry, typeof(short))));
			session.Items.Add(new CompletionItem("string", new CommonImageSourceProvider(CommonImageKind.ClassPublic), new CustomContentProvider(highlightingStyleRegistry, typeof(string))));

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

