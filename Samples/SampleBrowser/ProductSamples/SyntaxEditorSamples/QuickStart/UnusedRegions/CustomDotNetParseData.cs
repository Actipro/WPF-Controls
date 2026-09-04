using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions;

/// <summary>
/// Stores the results of a .NET language parsing operation.
/// </summary>
/// <param name="parseData">The original parse data.</param>
public class CustomDotNetParseData(IDotNetParseData parseData) : IDotNetParseData {

	private readonly IDotNetParseData _wrappedParseData = parseData ?? throw new ArgumentNullException(nameof(parseData));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ILLParseData.Ast"/>
	public IAstNode? Ast
		=> _wrappedParseData.Ast;

	/// <inheritdoc cref="IParseErrorProvider.Errors"/>
	public IEnumerable<IParseError>? Errors
		=> _wrappedParseData.Errors;

	/// <inheritdoc cref="IDotNetParseData.PreprocessorDirectives"/>
	public IList<PreprocessorDirective> PreprocessorDirectives
		=> _wrappedParseData.PreprocessorDirectives;

	/// <inheritdoc cref="IParseErrorProvider.Snapshot"/>
	public ITextSnapshot? Snapshot
		=> _wrappedParseData.Snapshot;

	/// <summary>
	/// A <see cref="NormalizedTextSnapshotRangeCollection"/> containing the unused regions in the snapshot.
	/// </summary>
	public NormalizedTextSnapshotRangeCollection? UnusedRanges { get; set; }

}
