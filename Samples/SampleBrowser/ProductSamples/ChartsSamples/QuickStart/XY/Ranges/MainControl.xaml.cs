using ActiproSoftware.SampleBrowser.SampleData;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Ranges {

	/// <summary>
	/// The chart control supports any number of ranges, which can be used to highlight areas of interest along its associated series.
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

			this.ChartData = new TimeAggregatedDataGenerator(new double[] { -3.8, -9.7, -11.9, -10.0, -11.5, -5.8, -5.5, -6.4, -2.5, -4.5, 4.0, 9.2, 7.1, -3.0, 7.0, 2.8, 11.2, 12.6, 11.8 });
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData { get; set; }

		#endregion PUBLIC PROCEDURES
	}
}