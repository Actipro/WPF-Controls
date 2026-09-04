using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags;

/// <summary>
/// Provides IntelliPrompt quick info data for the child <c>C#</c> language.
/// </summary>
public class TranslatedCSharpQuickInfoProvider : CSharpQuickInfoProvider {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? GetContext(IEditorView view, int offset) {
		#if NET
		ArgumentNullException.ThrowIfNull(view);
		#else
		if (view is null)
			throw new ArgumentNullException(nameof(view));
		#endif

		var parseData = view.SyntaxEditor.Document.ParseData as ParentParseData;
		if (parseData is not null) {
			// Ensure that the offset is within a child language section
			if (parseData.TranslateEditorToGenerated(new TextSnapshotOffset(view.CurrentSnapshot, offset)).HasValue) {
				return new TranslatedCSharpContextFactory(parseData.TranslateEditorToGenerated)
					.CreateContext(new TextSnapshotOffset(view.CurrentSnapshot, offset));
			}
		}

		return null;
	}

}
