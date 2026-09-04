using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningRangeBased;

/// <summary>
/// Provides a <c>Javascript</c> language outliner service.
/// </summary>
public class JavascriptOutliner : IOutliner {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IOutliner.GetOutliningSource"/>
	public IOutliningSource? GetOutliningSource(ITextSnapshot snapshot) {
		// Get the outlining source, which should be in the code document's parse data (as assigned by JavascriptOutliningParser.Parse implementation)
		if (snapshot.Document is ICodeDocument { ParseData: JavascriptOutliningSource source }) {
			// Translate the data to the desired snapshot, which could be slightly newer than the parsed source
			source.TranslateTo(snapshot);
			return source;
		}
		return null;
	}

	/// <inheritdoc cref="IOutliner.UpdateTrigger"/>
	public AutomaticOutliningUpdateTrigger UpdateTrigger
		=> AutomaticOutliningUpdateTrigger.ParseDataChanged;

}
