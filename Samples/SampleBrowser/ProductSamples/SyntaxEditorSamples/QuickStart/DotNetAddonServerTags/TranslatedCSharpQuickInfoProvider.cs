using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	
	/// <summary>
	/// Provides IntelliPrompt quick info data for the child <c>C#</c> language.
	/// </summary>
	public class TranslatedCSharpQuickInfoProvider : CSharpQuickInfoProvider {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
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
			if (view == null)
				throw new ArgumentNullException("view");

			var parseData = view.SyntaxEditor.Document.ParseData as ParentParseData;
			if (parseData != null) {
				// Ensure that the offset is within a child language section
				if (parseData.TranslateEditorToGenerated(new TextSnapshotOffset(view.CurrentSnapshot, offset)).HasValue) {
					return new TranslatedCSharpContextFactory(parseData.TranslateEditorToGenerated).CreateContext(
						new TextSnapshotOffset(view.CurrentSnapshot, offset));
				}
			}
			
			return null;
		}
	
	}

}
