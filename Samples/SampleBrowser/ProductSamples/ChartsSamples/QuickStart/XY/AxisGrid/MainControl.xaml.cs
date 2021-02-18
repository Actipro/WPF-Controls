using ActiproSoftware.SampleBrowser.SampleData;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.AxisGrid {

	/// <summary>
	/// Axis grid lines and stripes can be used to easily trace data points to their values on the axis.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			this.DataContext = this;

			InitializeComponent();

			this.ChartData = new TimeAggregatedDataGenerator(new double[] { 10.5, 19.5, 14.3, 4.8, 8.4, -1.3, 7.7, 1.8, -1.8, -9.4, -9.7, -6.2, 2.0, 11.2, 18.6, 27.4, 18.7, 11.3, 9.2 });
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData { get; set; }

	}

}
