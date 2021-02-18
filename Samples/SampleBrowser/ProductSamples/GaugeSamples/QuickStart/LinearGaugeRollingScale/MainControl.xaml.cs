using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		#endregion // OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDecreaseButtonClick(object sender, RoutedEventArgs e) {
			var heading = headingMarker.Value;
			heading--;

			var requiresWrap = (heading < -180);
			if (requiresWrap)
				heading += 360;

			headingMarker.Value = heading;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnIncreaseButtonClick(object sender, RoutedEventArgs e) {
			var heading = headingMarker.Value;
			heading++;

			var requiresWrap = (heading > 180);
			if (requiresWrap)
				heading -= 360;

			headingMarker.Value = heading;
		}

		#endregion // NON-PUBLIC PROCEDURES
	}
}