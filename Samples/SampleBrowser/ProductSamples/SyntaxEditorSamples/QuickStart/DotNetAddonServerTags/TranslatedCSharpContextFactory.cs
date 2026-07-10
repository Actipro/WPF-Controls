using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags;

/// <summary>
/// Creates child <c>C#</c> language <see cref="IDotNetContext"/> objects for a <see cref="TextSnapshotOffset"/>.
/// </summary>
/// <param name="translateFunc">The snapshot offset translation function.</param>
public class TranslatedCSharpContextFactory(Func<TextSnapshotOffset, TextSnapshotOffset?> translateFunc) : CSharpContextFactory {

	private readonly Func<TextSnapshotOffset, TextSnapshotOffset?> _translateFunc = translateFunc ?? throw new ArgumentNullException(nameof(translateFunc));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override TextSnapshotOffset TranslateToParseDataSnapshot(IDotNetParseData parseData, TextSnapshotOffset snapshotOffset) {
		var generatedSnapshotOffset = _translateFunc(snapshotOffset);
		return (generatedSnapshotOffset.HasValue)
			? base.TranslateToParseDataSnapshot(parseData, generatedSnapshotOffset.Value)
			: base.TranslateToParseDataSnapshot(parseData, snapshotOffset);
	}

}
