using System;
using System.Collections.ObjectModel;
using ActiproSoftware.ProductSamples.Charts.Common;
#if WINRT
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.ChartTypes.Scatter {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private ObservableCollection<Point> primaryChartPoints1 = new ObservableCollection<Point>();
		private ObservableCollection<Point> primaryChartPoints2 = new ObservableCollection<Point>();
		private readonly Random random = new Random();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			this.DataContext = this;

			InitializeComponent();
			InitializeSampleDataContext();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the sample data context.
		/// </summary>
		private void InitializeSampleDataContext() {
			for (int i = 0; i < 1000; i++) {
				int modulus = i % 2;
				double xm = i / (20.0d + 3 * modulus);
				double ym = 10.0d + 2 * modulus;
				double x = random.NextDouble() * xm + 1;
				double y = Math.Log(ym * (x - 1.0) + 1.0) * (random.NextDouble() + 0.9);

				if (modulus == 0)
					primaryChartPoints1.Add(new Point(x, y));
				else
					primaryChartPoints2.Add(new Point(x, y));
			}
		}

		#endregion NON-PUBLIC PROCEDURES
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the primary chart's points for series 1.
		/// </summary>
		/// <value>The primary chart's points for series 1.</value>
		public ObservableCollection<Point> PrimaryChartPoints1 {
			get {
				return primaryChartPoints1;
			}
		}

		/// <summary>
		/// Gets the primary chart's points for series 2.
		/// </summary>
		/// <value>The primary chart's points for series 2.</value>
		public ObservableCollection<Point> PrimaryChartPoints2 {
			get {
				return primaryChartPoints2;
			}
		}

		#endregion PUBLIC PROCEDURES

	}
}