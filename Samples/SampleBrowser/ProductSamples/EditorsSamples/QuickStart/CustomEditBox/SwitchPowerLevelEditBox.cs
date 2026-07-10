using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox;

/// <summary>
/// A custom part edit box implementation for the <see cref="SwitchPowerLevel"/> type.
/// </summary>
public class SwitchPowerLevelEditBox : PartEditBoxBase<SwitchPowerLevel> {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SwitchPowerLevelEditBox() {
		DefaultStyleKey = typeof(SwitchPowerLevelEditBox);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override string ConvertToString(SwitchPowerLevel? valueToConvert) {
		return (valueToConvert is not null)
			? $"{(valueToConvert.IsOn ? "On" : "Off")}, {valueToConvert.PowerLevel}"
			: string.Empty;
	}

	/// <inheritdoc/>
	protected override IncrementalChangeRequest<SwitchPowerLevel> CreateIncrementalChangeRequest(IncrementalChangeRequestKind kind) {
		var request = new IncrementalChangeRequest<SwitchPowerLevel> {
			Kind = (IntermediateValue is not null ? kind : IncrementalChangeRequestKind.None),
			LargeChange = new SwitchPowerLevel() { PowerLevel = 2 },
			Maximum = new SwitchPowerLevel() { PowerLevel = SwitchPowerLevel.MaxPowerLevel },
			Minimum = new SwitchPowerLevel() { PowerLevel = SwitchPowerLevel.MinPowerLevel },
			SmallChange = new SwitchPowerLevel() { PowerLevel = 1 },
			SpinWrapping = SpinWrapping,
			Value = IntermediateValue ?? new SwitchPowerLevel()
		};
		return request;
	}

	/// <inheritdoc/>
	protected override IList<IPart> GenerateParts() {
		return [
			new OnOffPart(),
			new LiteralPart(", "),
			new PowerLevelPart(),
		];
	}

	/// <inheritdoc/>
	protected override bool IsValidValue(SwitchPowerLevel? value) {
		if (value is not null)
			return (value.PowerLevel >= SwitchPowerLevel.MinPowerLevel) && (value.PowerLevel <= SwitchPowerLevel.MaxPowerLevel);
		else
			return true;
	}

	/// <inheritdoc/>
	protected override void ResetValue() {
		// Ensure the text is in sync again with the current value
		UpdateIntermediateValueAndTextFromValue();

		// Set the new value
		SetCurrentValue(ValueProperty, null);
	}

	/// <inheritdoc/>
	protected override bool TryConvertFromString(string? textToConvert, bool canCoerce, out SwitchPowerLevel? value) {
		value = new SwitchPowerLevel();

		if (!string.IsNullOrEmpty(textToConvert?.Trim())) {
			var segments = textToConvert!.Split([',']);
			if (segments?.Length >= 1) {
				var status = segments[0].Trim().ToUpperInvariant();
				value.IsOn = (status == "ON") || (status == "TRUE");

				if ((segments.Length >= 2) && int.TryParse(segments[1], out var powerLevel))
					value.PowerLevel = powerLevel;

				return true;
			}
		}

		return false;
	}

}
