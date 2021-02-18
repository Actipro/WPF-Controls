using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.Grids;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.BarStyles {

	/// <summary>
	/// Bar Charts can be modified to meet a wide variety of needs and visual styles.
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

			this.ChartData = new TimeAggregatedDataGenerator(new double[] { 19.5, 4.8, -1.3, 1.8, -9.4, -6.2, 11.2, 27.4, 11.3 });
			this.ChartData2 = new TimeAggregatedDataGenerator(new double[] { 3.5, 5, 1, 4.5, 10, 6.5, 1, 2, 8 });
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs after the value is set for an <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelValueChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridPropertyValueChanged(object sender, PropertyModelValueChangeEventArgs e) {
			chart.Refresh();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData { get; set; }

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData2 { get; set; }

	}

}
