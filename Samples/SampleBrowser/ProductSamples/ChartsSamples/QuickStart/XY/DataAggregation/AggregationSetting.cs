using System;
#if WINRT
using ActiproSoftware.UI.Xaml.Controls.Charts;
#else
using ActiproSoftware.Windows.Controls.Charts;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.DataAggregation {

	/// <summary>
	/// Stores an aggregation setting.
	/// </summary>
	public class AggregationSetting {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the aggregation factor used.
		/// </summary>
		/// <value>
		/// The aggregation factor used.
		/// </value>
		public double Factor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether aggregation is enabled.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if aggregation is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsEnabled { get; set; }

		/// <summary>
		/// Gets or sets the kind of aggregation used.
		/// </summary>
		/// <value>
		/// The kind of aggregation used.
		/// </value>
		public AggregationKind Kind { get; set; }

		/// <summary>
		/// Returns a <see cref="String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			if (!IsEnabled)
				return "No aggregation";
			return string.Format("Factor {0:F2}", Factor);
		}

		#endregion PUBLIC PROCEDURES
	}

}
