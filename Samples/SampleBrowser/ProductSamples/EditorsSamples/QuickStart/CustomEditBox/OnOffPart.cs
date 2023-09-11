using ActiproSoftware.Windows.Controls.Editors.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox {
	
	/// <summary>
	/// Represents an on/off part.
	/// </summary>
	public class OnOffPart : PartBase<string>, ISpinnablePart<SwitchPowerLevel> {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies an incremental change to the part's value.
		/// </summary>
		/// <param name="request">The incremental change request.</param>
		/// <returns>
		/// <c>true</c> if an incremental change was made; otherwise, <c>false</c>.
		/// </returns>
		public bool ApplyIncrementalChange(IncrementalChangeRequest<SwitchPowerLevel> request) {
			if (request == null)
				throw new ArgumentNullException("request");

			// Quit if no value is specified
			if (request.Value == null)
				return false;

			// Toggle
			request.Value.IsOn = !request.Value.IsOn;

			return true;
		}
	
		/// <summary>
		/// Tries to parse the text starting offset and returns the offset through which parsing was completed.
		/// </summary>
		/// <param name="parts">The part collection in which this part is a member.</param>
		/// <param name="text">The text to examine.</param>
		/// <param name="startOffset">The start offset.</param>
		/// <param name="offset">Returns the offset through which parsing was completed.</param>
		/// <param name="culture">The <see cref="CultureInfo"/> to use.</param>
		/// <returns>
		/// <c>true</c> if parsing was successful; otherwise, <c>false</c>.
		/// </returns>
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

}
