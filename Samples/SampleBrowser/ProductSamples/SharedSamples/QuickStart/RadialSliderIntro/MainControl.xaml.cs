using System;
using System.Windows;
using System.Windows.Data;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.RadialSliderIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when a slider value is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnFullCircleSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			// Quit if not fully loaded yet
			if (fullCirclePositiveSlice == null)
				return;

			fullCirclePositiveSlice.Opacity = (fullCircleSlider.Value > 0.0 ? 1.0 : 0.0);
			fullCircleNegativeSlice.Opacity = (fullCircleSlider.Value < 0.0 ? 1.0 : 0.0);
		}

	}
}