using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging;

/// <summary>
/// Provides IntelliPrompt popup content for a breakpoint indicator tag.
/// </summary>
/// <param name="tagRange">The tag range.</param>
internal class BreakpointIndicatorTagContentProvider(TagVersionRange<BreakpointIndicatorTag> tagRange) : IContentProvider {

	private readonly TagVersionRange<BreakpointIndicatorTag> _tagRange = tagRange ?? throw new ArgumentNullException(nameof(tagRange));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IContentProvider.GetContent"/>
	public object? GetContent() {
		// Get the snapshot range relative to the current snapshot (in case the document changed since the provider was created)
		var snapshotRange = _tagRange.VersionRange.Translate(_tagRange.VersionRange.Document.CurrentSnapshot);
		if (snapshotRange.HasValue) {
			var htmlSnippet = string.Format(
				"At line <b>{0}</b>, character <b>{1}</b>{2}",
				snapshotRange.Value.StartPosition.DisplayLine,
				snapshotRange.Value.StartPosition.DisplayCharacter,
				(_tagRange.Tag.IsEnabled ? string.Empty : " <i>(disabled)</i>")
			);

			return new HtmlContentProvider(htmlSnippet).GetContent();
		}

		return null;
	}

}
