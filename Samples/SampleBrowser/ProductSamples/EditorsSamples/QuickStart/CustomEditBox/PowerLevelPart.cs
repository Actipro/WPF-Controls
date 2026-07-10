using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox;

/// <summary>
/// Represents a power level part.
/// </summary>
public class PowerLevelPart : NumberPart<Int32>, ISpinnablePart<SwitchPowerLevel> {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Applies an incremental change to the part's value.
	/// </summary>
	/// <param name="request">The incremental change request.</param>
	/// <returns>
	/// <c>true</c> if an incremental change was made; otherwise, <c>false</c>.
	/// </returns>
	public bool ApplyIncrementalChange(IncrementalChangeRequest<SwitchPowerLevel> request) {
		if (request is null)
			throw new ArgumentNullException(nameof(request));

		// Quit if no value is specified
		if (request.Value is null)
			return false;

		// Apply incremental change
		var oldValue = request.Value.PowerLevel;
		var smallChange = request.SmallChange?.PowerLevel ?? 1;
		var largeChange = request.LargeChange?.PowerLevel ?? 1;
		switch (request.Kind) {
			case IncrementalChangeRequestKind.Decrease:
				request.Value.PowerLevel -= smallChange;
				break;
			case IncrementalChangeRequestKind.Increase:
				request.Value.PowerLevel += smallChange;
				break;
			case IncrementalChangeRequestKind.MultipleDecrease:
				request.Value.PowerLevel -= largeChange;
				break;
			case IncrementalChangeRequestKind.MultipleIncrease:
				request.Value.PowerLevel += largeChange;
				break;
		}

		return (oldValue != request.Value.PowerLevel);
	}

	/// <inheritdoc/>
	protected override bool IsComposited
		=> true;

}
