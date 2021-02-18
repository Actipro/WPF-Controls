using System;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	
	/// <summary>
	/// Provides IntelliPrompt completion data for the child <c>C#</c> language.
	/// </summary>
	public class TranslatedCSharpCompletionProvider : CSharpCompletionProvider {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates an <see cref="IDotNetContext"/> for the caret's offset in the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> to examine.</param>
		/// <returns>An <see cref="IDotNetContext"/> for the caret's offset in the specified <see cref="IEditorView"/>.</returns>
		protected override IDotNetContext CreateContext(IEditorView view) {
			if (view == null)
				throw new ArgumentNullException("view");

			var parseData = view.SyntaxEditor.Document.ParseData as ParentParseData;
			if (parseData != null) {
				// Ensure that the offset is within a child language section
				if (parseData.TranslateEditorToGenerated(view.Selection.EndSnapshotOffset).HasValue) {
					return new TranslatedCSharpContextFactory(parseData.TranslateEditorToGenerated).CreateContext(
						view.Selection.EndSnapshotOffset, DotNetContextKind.SelfAndSiblings);
				}
			}

			return null;
		}
	
	}

}
