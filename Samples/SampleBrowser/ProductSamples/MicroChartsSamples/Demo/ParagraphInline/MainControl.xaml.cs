using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.ParagraphInline {

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
			this.DataContext = this;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the production data.
		/// </summary>
		/// <value>The production data.</value>
		public IEnumerable<double> MonthlyProduction {
			get {
				return new double[] { 5, 6, 8, 8.5, 7.6, 7.4, 8.3, 9.5, 9.7, 8.3, 7.2, 11.3, 10.5, 10.4, 10.9, 11.3, 11.5, 11.7 };
			}
		}

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		/// <value>The sales data.</value>
		public IEnumerable<double> MonthlySales {
			get {
				return new double[] { 3.7, 3.5, 3.8, 3.9, 4.1, 4.2, 4.4, 4.3, 4.5, 4.6, 4.6, 4.7 };
			}
		}

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		/// <value>The sales data.</value>
		public IEnumerable<double> PreviousMonthlySales {
			get {
				return new double[] { 3.4, 3.3, 3.2, 3.6, 3.7, 3.8, 3.7, 3.7, 3.8, 3.9, 4.1, 4.0 };
			}
		}

	}
}