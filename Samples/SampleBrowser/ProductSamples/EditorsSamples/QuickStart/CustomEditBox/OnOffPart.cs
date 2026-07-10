using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox;

/// <summary>
/// Represents an on/off part.
/// </summary>
public class OnOffPart : PartBase<string>, ISpinnablePart<SwitchPowerLevel> {

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

		// Toggle
		request.Value.IsOn = !request.Value.IsOn;

		return true;
	}

	/// <inheritdoc/>
	public override bool TryParseText(IList<IPart> parts, string text, int startOffset, CultureInfo culture, out int offset) {
		offset = startOffset;

		if (!string.IsNullOrEmpty(text)) {
			text = text.Substring(startOffset);

			var commaIndex = text.IndexOf(',');
			if (commaIndex != -1)
				text = text.Substring(0, commaIndex);

			var partText = text.Trim().ToUpperInvariant();
			switch (partText) {
				case "FALSE":
				case "OFF":
				case "ON":
				case "TRUE":
					offset = startOffset + partText.Length;
					return true;
			}
		}

		return false;
	}

}
