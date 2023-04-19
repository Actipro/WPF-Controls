using ActiproSoftware.Windows.Controls.Bars;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ScreenTips {

	/// <summary>
	/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
	/// </summary>
	public partial class SampleXamlControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleXamlControl"/> class.
		/// </summary>
		public SampleXamlControl() {
			InitializeComponent();

			// Listen for ScreenTip opening for controls that notify the ScreenTipService
			ScreenTipService.Current.ScreenTipOpening += this.OnScreenTipOpening;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when a <see cref="ScreenTip"/> is opening.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		/// <remarks>This event is only raised for controls that notify <see cref="ScreenTipService"/> when their <see cref="ToolTip"/> is opening.</remarks>
		private void OnScreenTipOpening(object sender, ToolTipEventArgs e) {
			if ((sender is DependencyObject element) && (ToolTipService.GetToolTip(element) is ScreenTip screenTip)) {
				var key = BarControlService.GetKey(element);

				// Store the key on the ScreenTip for use with contextual help
				screenTip.Tag = new ScreenTipData() { Key = key };

				// Customize the ScreenTip for specific controls
				if (key == "XamlDynamicScreenTip") {
					// Dynamically include the time stamp in the footer
					screenTip.Footer = $"Displayed at: {DateTime.Now}";
				}
			}
		}

		/// <summary>
		/// Occurs when a <see cref="ToolTip"/> is opening on a control.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		private void OnTextBoxToolTipOpening(object sender, ToolTipEventArgs e) {
			// Custom controls that display a ScreenTip will need to notify the ScreenTipService
			// when a ToolTip is opening if you intend to use the corresponding
			// ScreenTipService.ScreenTipOpening event to manage ScreenTips like shown in this sample.
			ScreenTipService.Current.NotifyToolTipOpening(sender, e);
		}

	}

}
