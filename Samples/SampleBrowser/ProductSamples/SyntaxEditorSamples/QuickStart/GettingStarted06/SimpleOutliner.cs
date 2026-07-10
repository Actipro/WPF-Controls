using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted06;

/// <summary>
/// Provides a <c>Simple</c> language outliner service.
/// </summary>
public class SimpleOutliner : IOutliner {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IOutliner.GetOutliningSource"/>
	public IOutliningSource? GetOutliningSource(ITextSnapshot snapshot) {
		// Get the parse data
		if (snapshot?.Document is ICodeDocument { ParseData: ILLParseData parseData }) {
			// Create an outlining source based on the parse data
			var source = new SimpleOutliningSource(snapshot, parseData);

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
