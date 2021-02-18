using ActiproSoftware.SampleBrowser.SampleData;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.DataPointLabels {

	/// <summary>
	/// Data point labels can be used to easily identify the value of a particular data point.
	/// </summary>
	public partial class MainControl {

		private readonly Func<object, object, object, object, object, string> customLabelFunc = GetCustomLabel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.ChartData = new TimeAggregatedDataGenerator(new double[] { 19.5, 4.8, -1.3, 1.8, -9.4, -6.2, 11.2, 27.4, 11.3 });
			this.ChartData2 = new TimeAggregatedDataGenerator(new double[] { -6200, 9200, 18500, 4800, -1300, 4000, 12000, 9000, 1800 });
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

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public IEnumerable<TimeAggregatedData> ChartData2 { get; set; }

		/// <summary>
		/// Gets the custom label function.
		/// </summary>
		/// <value>The custom label function.</value>
		public Func<object, object, object, object, object, string> CustomLabelFunc {
			get { return customLabelFunc; }
		}

		/// <summary>
		/// Gets the custom label.
		/// </summary>
		/// <param name="primaryValue">The primary value.</param>
		/// <param name="secondaryValue">The secondary value.</param>
		/// <param name="xValue">The x value.</param>
		/// <param name="yValue">The y value.</param>
		/// <param name="originalValue">The original value.</param>
		/// <returns>System.String.</returns>
		public static string GetCustomLabel(object primaryValue, object secondaryValue, object xValue, object yValue, object originalValue) {
			return string.Format("X: {0}{1}Y: {2}", xValue, Environment.NewLine, yValue);
		}

		#endregion PUBLIC PROCEDURES
	}
}
