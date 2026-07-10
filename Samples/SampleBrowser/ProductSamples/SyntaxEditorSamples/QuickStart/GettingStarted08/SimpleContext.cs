using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;

/// <summary>
/// Provides information about the <c>Simple</c> context of a certain offset within an <see cref="ITextSnapshot"/>.
/// </summary>
/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> that indicates the location to examine.</param>
public class SimpleContext(TextSnapshotOffset snapshotOffset) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The index of the current argument, if known, when the context is for the containing invocation.
	/// </summary>
	public int? ArgumentIndex { get; set; }

	/// <summary>
	/// The <see cref="TextSnapshotOffset"/> of the argument list, if known, when the context is for the containing invocation.
	/// </summary>
	public TextSnapshotOffset? ArgumentListSnapshotOffset { get; set; }

	/// <summary>
	/// The <see cref="TextSnapshotOffset"/> of the current argument, if known, when the context is for the containing invocation.
	/// </summary>
	public TextSnapshotOffset? ArgumentSnapshotOffset { get; set; }

	/// <summary>
	/// The <see cref="IAstNode"/> for the function declaration that contains the offset, if any.
	/// </summary>
	public FunctionDeclaration? ContainingFunctionDeclaration { get; set; }

	/// <inheritdoc/>
	#if NET
	public override bool Equals([NotNullWhen(true)] object? obj) {
	#else
	public override bool Equals(object? obj) {
	#endif
		// Test for similar (not necessarily equal) as is necessary for IntelliPrompt parameter information
		var similar = obj is SimpleContext other
			&& InitializationSnapshotRange == other.InitializationSnapshotRange
			&& Type == other.Type;
		return similar;
	}

	/// <inheritdoc/>
	public override int GetHashCode()
		=> SnapshotOffset.GetHashCode();

	/// <summary>
	/// The <see cref="TextSnapshotRange"/> with which the context was initialized.
	/// </summary>
	public TextSnapshotRange? InitializationSnapshotRange { get; set; }

	/// <summary>
	/// The <see cref="TextSnapshotOffset"/> for which this context was created.
	/// </summary>
	public TextSnapshotOffset SnapshotOffset { get; } = snapshotOffset;

	/// <summary>
	/// An <see cref="IAstNode"/> that specifies the target function for this context, if any.
	/// </summary>
	public FunctionDeclaration? TargetFunction { get; set; }

	/// <inheritdoc/>
	public override string ToString()
		=> string.Format("SimpleContext[Type={0}]", Type);

	/// <summary>
	/// An <see cref="SimpleContextType"/> that specifies the type of context.
	/// </summary>
	public SimpleContextType Type { get; set; } = SimpleContextType.Default;

}
