using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags;

/// <summary>
/// Provides IntelliPrompt completion data for the child <c>C#</c> language.
/// </summary>
public class TranslatedCSharpCompletionProvider : CSharpCompletionProvider {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IDotNetContext? CreateContext(IEditorView view) {
		#if NET
		ArgumentNullException.ThrowIfNull(view);
		#else
		if (view is null)
			throw new ArgumentNullException(nameof(view));
		#endif

		var parseData = view.SyntaxEditor.Document.ParseData as ParentParseData;
		if (parseData is not null) {
			// Ensure that the offset is within a child language section
			if (parseData.TranslateEditorToGenerated(view.Selection.EndSnapshotOffset).HasValue) {
				return new TranslatedCSharpContextFactory(parseData.TranslateEditorToGenerated)
					.CreateContext(view.Selection.EndSnapshotOffset, DotNetContextKind.SelfAndSiblings);
			}
		}

		return null;
	}

}
