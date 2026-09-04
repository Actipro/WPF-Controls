using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags;

/// <summary>
/// Provides the result of a parsing operation.
/// </summary>
/// <param name="snapshot">The <see cref="ITextSnapshot"/> from which the parse errors were created.</param>
public class ParentParseData(ITextSnapshot snapshot) : IDotNetParseData {

	private List<IParseError>? _errors;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ILLParseData.Ast"/>
	public IAstNode? Ast
		=> GeneratedParseData?.Ast;

	/// <inheritdoc cref="IParseErrorProvider.Errors"/>
	public IEnumerable<IParseError> Errors {
		get {
			if (_errors is null) {
				_errors = [];

				if (GeneratedParseData is { Errors: not null, Snapshot: not null }) {
					// Loop through errors and translate to the editor snapshot
					foreach (var parseError in GeneratedParseData.Errors) {
						if (parseError?.PositionRange.HasValue == true) {
							var generatedSnapshotRange = new TextSnapshotRange(GeneratedParseData.Snapshot, GeneratedParseData.Snapshot.PositionRangeToTextRange(parseError.PositionRange.Value));
							var editorSnapshotOffset = TranslateGeneratedToEditor(new TextSnapshotOffset(GeneratedParseData.Snapshot, generatedSnapshotRange.StartOffset));
							if (editorSnapshotOffset.HasValue) {
								// Add the error
								var positionRange = Snapshot.TextRangeToPositionRange(TextRange.FromSpan(editorSnapshotOffset.Value, Math.Max(1, generatedSnapshotRange.Length)));
								_errors.Add(new ParseError(parseError.Level, parseError.Description, positionRange));
							}
						}
					}
				}
			}

			return _errors;
		}
	}

	/// <summary>
	/// The generated parse data.
	/// </summary>
	public ILLParseData? GeneratedParseData { get; set; }

	/// <inheritdoc cref="IDotNetParseData.PreprocessorDirectives"/>
	public IList<PreprocessorDirective> PreprocessorDirectives { get; } = [];

	/// <inheritdoc cref="IParseErrorProvider.Snapshot"/>
	public ITextSnapshot Snapshot { get; } = snapshot ?? throw new ArgumentNullException(nameof(snapshot));

	/// <summary>
	/// The collection of text range mappings from the editor snapshot to the generated snapshots.
	/// </summary>
	public IList<Tuple<TextRange, TextRange>> TextRangeMappings { get; } = [];

	/// <summary>
	/// Translates from an editor snapshot to a generated snapshot.
	/// </summary>
	/// <param name="snapshotOffset">The snapshot offset.</param>
	/// <returns>The translated snapshot, if within a child language section.</returns>
	public TextSnapshotOffset? TranslateEditorToGenerated(TextSnapshotOffset snapshotOffset) {
		if (GeneratedParseData?.Snapshot is not null) {
			// Translate back to the editor snapshot
			snapshotOffset = snapshotOffset.TranslateTo(Snapshot, TextOffsetTrackingMode.Negative);

			foreach (var mapping in TextRangeMappings) {
				if (mapping.Item1.IntersectsWith(snapshotOffset.Offset)) {
					var generatedSnapshotOffset = new TextSnapshotOffset(GeneratedParseData.Snapshot, mapping.Item2.StartOffset + (snapshotOffset.Offset - mapping.Item1.StartOffset));
					return generatedSnapshotOffset;
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Translates from a generated snapshot to an editor snapshot.
	/// </summary>
	/// <param name="snapshotOffset">The snapshot offset.</param>
	/// <returns>The translated snapshot, if within a child language section.</returns>
	public TextSnapshotOffset? TranslateGeneratedToEditor(TextSnapshotOffset snapshotOffset) {
		if (GeneratedParseData?.Snapshot is not null) {
			// Translate back to the generated parse data's snapshot
			snapshotOffset = snapshotOffset.TranslateTo(GeneratedParseData.Snapshot, TextOffsetTrackingMode.Negative);

			foreach (var mapping in TextRangeMappings) {
				if (mapping.Item2.IntersectsWith(snapshotOffset.Offset)) {
					var editorSnapshotOffset = new TextSnapshotOffset(Snapshot, mapping.Item1.StartOffset + (snapshotOffset.Offset - mapping.Item2.StartOffset));
					return editorSnapshotOffset;
				}
			}
		}

		return null;
	}

}
