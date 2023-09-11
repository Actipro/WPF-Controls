using ActiproSoftware.Windows.Controls.Editors.Primitives;
using System;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox {

	/// <summary>
	/// Represents a power level part.
	/// </summary>
	public class PowerLevelPart : Int32Part, ISpinnablePart<SwitchPowerLevel> {
		
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
				throw new ArgumentNullException(nameof(request));

			// Quit if no value is specified
			if (request.Value == null)
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
	
		/// <summary>
		/// Gets whether the part is composited with other parts in an exit box.
		/// </summary>
		/// <value>
		/// <c>true</c> if the part is composited with other parts in an exit box; otherwise, <c>false</c>.
		/// </value>
		protected override bool IsComposited {
			get {
				return true;
			}
		}
	
	}

}
