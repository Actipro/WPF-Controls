using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.Charts;
using System.Collections.Generic;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Legend {

	/// <summary>
	/// A legend can help make sense out of multiple series. Our chart offers a wide array of legend customization.
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

			this.ChartData1 = new TimeAggregatedDataGenerator(new double[] { 42.3, 52.0, 43.2, 37.0, 33.2, 43.0, 51.7, 41.6, 42.2, 37.0, 41.3, 50.4, 57.9, 55.5, 56.9, 60.1, 66.2, 59.6, 60.6 });
			this.ChartData2 = new TimeAggregatedDataGenerator(new double[] { 39.6, 30.3, 20.7, 23.8, 27.5, 35.8, 40.3, 38.1, 43.8, 44.6, 41.7, 49.0, 49.3, 48.6, 53.6, 50.6, 49.6, 42.8, 51.2 });
			this.ChartData3 = new TimeAggregatedDataGenerator(new double[] { 25.5, 21.2, 11.3, 17.3, 11.9, 20.7, 12.7, 21.8, 30.2, 22.0, 19.7, 10.5, 12.9, 10.9, 14.6, 20.2, 12.7, 14.7, 20.7 });
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
			chart.ClearValue(XYChart.LegendStyleProperty);
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData1 { get; set; }

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData2 { get; set; }

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData3 { get; set; }

		#endregion PUBLIC PROCEDURES
	}
}
