using ActiproSoftware.Windows;
using System;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox {

	/// <summary>
	/// Indicates whether a switch is on, and its power level when on.
	/// </summary>
	public class SwitchPowerLevel : ObservableObjectBase {

		private bool isOn;
		private int powerLevel = 5;

		public const int MinPowerLevel = 1;
		public const int MaxPowerLevel = 10;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the switch is on.
		/// </summary>
		/// <value><c>true</c> if the switch is on; otherwise <c>false</c>.</value>
		public bool IsOn {
			get => isOn;
			set {
				if (isOn != value) {
					isOn = value;
					NotifyPropertyChanged(nameof(IsOn));
				}
			}
		}

		/// <summary>
		/// Gets or sets the power level, between <c>1</c> and <c>10</c>.
		/// </summary>
		/// <value>The power level.</value>
		public int PowerLevel {
			get => powerLevel;
			set {
				value = Math.Max(MinPowerLevel, Math.Min(MaxPowerLevel, value));

				if (powerLevel != value) {
					powerLevel = value;
					NotifyPropertyChanged(nameof(PowerLevel));
				}
			}
		}

	}
}
