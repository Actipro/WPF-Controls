using ActiproSoftware.Windows.Controls.Editors.Primitives;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox {

	/// <summary>
	/// A custom part edit box implementation for the <see cref="SwitchPowerLevel"/> type.
	/// </summary>
	public class SwitchPowerLevelEditBox : PartEditBoxBase<SwitchPowerLevel> {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs after the <c>Value</c> property value changes.
		/// </summary>
		/// <eventdata>
		/// The event handler receives an argument of type <c>EventArgs</c> containing data related to this event.
		/// </eventdata>
		public event EventHandler ValueChanged;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>SwitchPowerLevelEditBox</c> class.
		/// </summary>
		public SwitchPowerLevelEditBox() {
			this.DefaultStyleKey = typeof(SwitchPowerLevelEditBox);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Converts the specified value to a string representation.
		/// </summary>
		/// <param name="valueToConvert">The value.</param>
		/// <returns>The string representation of the specified value.</returns>
		protected override string ConvertToString(SwitchPowerLevel valueToConvert) {
			if (valueToConvert != null)
				return $"{(valueToConvert.IsOn ? "On" : "Off")}, {valueToConvert.PowerLevel}";
			else
				return string.Empty;
		}

		/// <summary>
		/// Creates an incremental change (spin) request.
		/// </summary>
		/// <param name="kind">The kind of request.</param>
		/// <returns>The incremental change (spin) request that was created.</returns>
		protected override IncrementalChangeRequest<SwitchPowerLevel> CreateIncrementalChangeRequest(IncrementalChangeRequestKind kind) {
			var request = new IncrementalChangeRequest<SwitchPowerLevel>();
			request.Kind = (this.IntermediateValue != null ? kind : IncrementalChangeRequestKind.None);
			request.LargeChange = new SwitchPowerLevel() { PowerLevel = 2 };
			request.Maximum = new SwitchPowerLevel() { PowerLevel = SwitchPowerLevel.MaxPowerLevel };
			request.Minimum = new SwitchPowerLevel() { PowerLevel = SwitchPowerLevel.MinPowerLevel };
			request.SmallChange = new SwitchPowerLevel() { PowerLevel = 1 };
			request.SpinWrapping = this.SpinWrapping;
			request.Value = this.IntermediateValue ?? new SwitchPowerLevel();
			return request;
		}

		/// <summary>
		/// Generates the parts for the edit box.
		/// </summary>
		/// <returns>The parts that were generated.</returns>
		protected override IList<IPart> GenerateParts() {
			return new IPart[] {
				new OnOffPart(),
				new LiteralPart() { Text = ", " },
				new PowerLevelPart(),
			};	
		}

		/// <summary>
		/// Returns whether the specified value is valid.
		/// </summary>
		/// <param name="value">The value to examine.</param>
		/// <returns>
		/// <c>true</c> if the value is valid; otherwise, <c>false</c>.
		/// </returns>
		protected override bool IsValidValue(SwitchPowerLevel value) {
			if (value != null)
				return (value.PowerLevel >= SwitchPowerLevel.MinPowerLevel) && (value.PowerLevel <= SwitchPowerLevel.MaxPowerLevel);
			else
				return true;
		}
		
		/// <summary>
		/// Raises the <see cref="ValueChanged"/> event.
		/// </summary>
		protected override void RaiseValueChangedEvent() {
			var handler = this.ValueChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// Resets the value to a default value.
		/// </summary>
		protected override void ResetValue() {
			// Ensure the text is in sync again with the current value
			this.UpdateIntermediateValueAndTextFromValue();

			// Set the new value
			this.SetCurrentValue(ValueProperty, null);
		}

		/// <summary>
		/// Tries to convert the specified text to a value.
		/// </summary>
		/// <param name="textToConvert">The text.</param>
		/// <param name="canCoerce">Whether the returned value should be coerced to fall within the allowed value range.</param>
		/// <param name="value">Returns the value for the specified text.</param>
		/// <returns>
		/// <c>true</c> if the text was converted to a value successfully; otherwise, <c>false</c>.
		/// </returns>
		protected override bool TryConvertFromString(string textToConvert, bool canCoerce, out SwitchPowerLevel value) {
			value = new SwitchPowerLevel();

			if (!string.IsNullOrEmpty(textToConvert?.Trim())) {
				var segments = textToConvert.Split(new char[] { ',' });
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
}
