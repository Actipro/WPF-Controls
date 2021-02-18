using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using ActiproSoftware.Compatibility;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.LiveData {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private DeferrableObservableCollection<double>	data1;
		private DeferrableObservableCollection<double>	data2;
		private Random									random = new Random(Environment.TickCount);
		private DispatcherTimer							timer;

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Interval"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Interval"/> dependency property.</value>
		public static readonly DependencyProperty IntervalProperty = DependencyPropertyEx.Register("Interval",
			typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(500.0, OnIntervalPropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			this.data1 = new DeferrableObservableCollection<double>();
			this.data2 = new DeferrableObservableCollection<double>();
			for (int i = 0; i < 100; i++) {
				this.data1.Add(this.GetNextValue());
				this.data2.Add(this.GetNextValue());
			}

			this.timer = new DispatcherTimer();
			this.timer.Tick += new EventHandler(OnTimerTick);
			this.timer.Interval = TimeSpan.FromMilliseconds(this.Interval);
			this.timer.Start();

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the next value.
		/// </summary>
		/// <returns>The next value.</returns>
		private double GetNextValue() {
			return 100 + this.random.NextDouble() * 10;
		}

		/// <summary>
		/// Occurs when the <see cref="IntervalProperty"/> value is changed.
		/// </summary>
		/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnIntervalPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var control = d as MainControl;
			if (control != null)
				control.timer.Interval = TimeSpan.FromMilliseconds(control.Interval);
		}

		/// <summary>
		/// Occurs when the timer ticks.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		private void OnTimerTick(object sender, EventArgs e) {
			this.data1.BeginUpdate();
			this.data2.BeginUpdate();
			try {
				this.data1.RemoveAt(0);
				this.data1.Add(this.GetNextValue());

				this.data2.RemoveAt(0);
				this.data2.Add(this.GetNextValue());
			}
			finally {
				this.data1.EndUpdate();
				this.data2.EndUpdate();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the data used by this sample.
		/// </summary>
		public IList<double> Data1 {
			get {
				return this.data1;
			}
		}

		/// <summary>
		/// Gets the data used by this sample.
		/// </summary>
		public IList<double> Data2 {
			get {
				return this.data2;
			}
		}

		/// <summary>
		/// Gets or sets the interval at which the data is updated.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The interval at which the data is updated.
		/// The default value is <c>500.0</c> ms.
		/// </value>
		public double Interval {
			get { return (double)this.GetValue(IntervalProperty); }
			set { this.SetValue(IntervalProperty, value); }
		}
		
		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			this.timer.Stop();
		}
		
	}
}