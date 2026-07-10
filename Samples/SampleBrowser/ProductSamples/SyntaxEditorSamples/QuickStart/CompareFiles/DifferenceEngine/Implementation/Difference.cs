using System.Threading;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine.Implementation;

/// <summary>
/// Tracks a difference between a source and destination.
/// </summary>
/// <param name="kind">The kind of difference.</param>
/// <param name="position">The zero-based position of the difference within the source or <c>null</c> if the difference is imaginary.</param>
[DebuggerDisplay("Difference[{Position}]; Kind={Kind}; Length={Length}")]
public class Difference(DifferenceKind kind, int? position = null) : IDifference {

	private readonly Lazy<DifferenceCollection> _lazyChildren = new(() => [], LazyThreadSafetyMode.PublicationOnly);

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	IDifferenceCollection IDifference.Children
		=> Children;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IDifference.Children"/>
	public DifferenceCollection Children
		=> _lazyChildren.Value;

	/// <inheritdoc cref="IDifference.Kind"/>
	public DifferenceKind Kind { get; } = kind;

	/// <inheritdoc cref="IDifference.Length"/>
	public int Length { get; set; }

	/// <inheritdoc cref="IDifference.Position"/>
	public int? Position { get; set; } = position;

}
