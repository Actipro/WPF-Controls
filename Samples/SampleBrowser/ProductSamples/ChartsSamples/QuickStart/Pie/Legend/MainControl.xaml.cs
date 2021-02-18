using ActiproSoftware.Windows.Controls.Charts;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.Pie.Legend {

	/// <summary>
	/// A legend can help make sense out of multiple slices. Our chart offers a wide array of legend customization.
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

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the custom style checkbox is checked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
		private void CustomStyleChecked(object sender, RoutedEventArgs args) {
			 chart.LegendStyle = (Style)Resources["CustomStyle"];
		}

		/// <summary>
		/// Occurs when the custom style checkbox is unchecked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
		private void CustomStyleUnchecked(object sender, RoutedEventArgs args) {
			chart.ClearValue(PieChart.LegendStyleProperty);
		}

		#endregion NON-PUBLIC PROCEDURES

	}
}
