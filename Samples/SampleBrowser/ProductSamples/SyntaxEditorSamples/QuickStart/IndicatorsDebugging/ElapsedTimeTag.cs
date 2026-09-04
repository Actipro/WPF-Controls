using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging;

/// <summary>
/// Provides an <see cref="IIntraTextSpacerTag"/> implementation that reserves intra-text space for a note.
/// </summary>
/// <param name="timeSpan">The elapse time.</param>
public class ElapsedTimeTag(TimeSpan timeSpan) : IIntraTextSpacerTag {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IIntraTextSpacerTag.Baseline"/>
	public double Baseline => 0.0;

	/// <inheritdoc cref="IIntraTextSpacerTag.IsSpaceBefore"/>
	public bool IsSpacerBefore => false;

	/// <inheritdoc cref="IIntraTextSpacerTag.Key"/>
	public object Key => this;

	/// <inheritdoc cref="IIntraTextSpacerTag.Size"/>
	public Size Size { get; set; }

	/// <summary>
	/// The elapsed time.
	/// </summary>
	public TimeSpan TimeSpan { get; } = timeSpan;

	/// <summary>
	/// The text to display.
	/// </summary>
	public string TimeSpanText
		=> $"≤ {TimeSpan.TotalMilliseconds:N0}ms elapsed";

}
