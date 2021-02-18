using ActiproSoftware.Windows;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.ChartsSamples.ChartTypes.Bar {

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
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of bar group spacings.
		/// </summary>
		/// <value>The collection of bar group spacings.</value>
		public IList<Unit> BarGroupSpacings {
			get {
				return new[] {
					Unit.Percentage(15),
					Unit.Percentage(25),
					Unit.Percentage(35),
					Unit.Percentage(45)
				};
			}
		}

		/// <summary>
		/// Gets the collection of bar spacings.
		/// </summary>
		/// <value>The collection of bar spacings.</value>
		public IList<Unit> BarSpacings {
			get {
				return new[] {
					Unit.Pixel(0),
					Unit.Pixel(1),
					Unit.Pixel(2),
					Unit.Pixel(5)
				};
			}
		}

	}

}