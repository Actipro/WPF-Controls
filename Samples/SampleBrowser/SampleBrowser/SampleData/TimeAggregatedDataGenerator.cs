using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Dynamically generates random data to be used with various samples.
	/// Normally, data would come from sources such as database instead.
	/// </summary>
	public class TimeAggregatedDataGenerator : DeferrableObservableCollection<TimeAggregatedData>, ICollection, IEnumerable {

		private bool			allowNegativeNumbers;
		private int?			dataPointCount;
		private ICommand		generateCommand;
		private bool			isInitialized;
		private int				partitionMaxCount;
		private IList<double>	presetAmounts;
		private IList<string>	presetTitles;
		private int?			randomSeed;
		private double			startAmount				= 120000;
		private double			stepRange				= 30000;
		private TimePeriod		timePeriod				= TimePeriod.Year;
		private Trend			trend					= Trend.Random;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeAggregatedDataGenerator" /> class.
		/// </summary>
		public TimeAggregatedDataGenerator() {}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeAggregatedDataGenerator" /> class.
		/// </summary>
		/// <param name="amounts">The specific amounts to use, instead of generating amounts.</param>
		public TimeAggregatedDataGenerator(IList<double> amounts) {
			this.presetAmounts = amounts;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the number of items in the collection.
		/// </summary>
		/// <value>The number of items in the collection.</value>
		int ICollection.Count {
			get {
				if (!isInitialized)
					this.Generate();

				return base.Count;
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() {
			if (!isInitialized)
				this.Generate();

			return base.GetEnumerator();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a data instance.
		/// </summary>
		/// <param name="random">The <see cref="Random"/> instance to use.</param>
		/// <param name="index">The data item index.</param>
		/// <param name="timePeriod">The time period.</param>
		/// <param name="date">The time period start date for which the amount is specified.</param>
		/// <param name="amount">The sales amount.</param>
		/// <returns></returns>
		private TimeAggregatedData CreateData(Random random, int index, TimePeriod timePeriod, DateTime date, double amount) {
			var data = new TimeAggregatedData(index, timePeriod, date, amount);

			if ((presetTitles != null) && (index < presetTitles.Count))
				data.Title = presetTitles[index];

			if (partitionMaxCount >= 2) {
				var partitionCount = random.Next(2, partitionMaxCount);

				var partitions = new NumericData[partitionCount];
				for (var partitionIndex = 0; partitionIndex < partitionCount; partitionIndex++)
					 partitions[partitionIndex] = new NumericData(random.Next(1, 100));

				data.Partitions = partitions;
			}

			return data;
		}
		/// <summary>
		/// Gets the default data point count.
		/// </summary>
		/// <value>The default data point count.</value>
		private int DefaultDataPointCount {
			get {
				switch (timePeriod) {
					case TimePeriod.Month:
						return 12;
					default:  // Year
						return 10;
				}
			}
		}

		/// <summary>
		/// Invalidates the data points.
		/// </summary>
		private void Invalidate() {
			if (isInitialized)
				this.Generate();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether to allow negative numbers.
		/// </summary>
		/// <value>
		/// <c>true</c> if negative numbers are allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowNegativeNumbers {
			get {
				return allowNegativeNumbers;
			}
			set {
				if (allowNegativeNumbers != value) {
					allowNegativeNumbers = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets the number of data points to generate in each data set.
		/// </summary>
		/// <value>The number of data points to generate in each data set.</value>
		public int DataPointCount {
			get {
				return presetAmounts?.Count ?? dataPointCount ?? this.DefaultDataPointCount;
			}
			set {
				value = Math.Max(1, value);

				if (dataPointCount != value) {
					dataPointCount = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Generates data points.
		/// </summary>
		public void Generate() {
			// Create a random number generator
			var resolvedRandomSeed = randomSeed ?? Environment.TickCount;
			var random = new Random(resolvedRandomSeed);

			// Initialize the date
			DateTime date;
			var resolvedDataPointCount = this.DataPointCount;
			switch (timePeriod) {
				case TimePeriod.Month:
					date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
					date = date.AddMonths(-resolvedDataPointCount);
					break;
				case TimePeriod.Week:
					date = DateTime.Today;
					date = date.AddDays(-resolvedDataPointCount * 7);
					break;
				default:  // Year
					date = new DateTime(DateTime.Today.Year, 1, 1);
					date = date.AddYears(-resolvedDataPointCount);
					break;
			}

			// Initialize the results with the first data item
			this.BeginUpdate();
			try {
				this.Clear();

				// Quit if there are no data points
				if (resolvedDataPointCount == 0)
					return;
				
				// Determine the trend percentage
				double trendPercentage;
				switch (trend) {
					case Trend.Upward:
						trendPercentage = 0.3;
						break;
					case Trend.Downward:
						trendPercentage = 0.7;
						break;
					default:  // Random
						trendPercentage = 0.5;
						break;
				}

				// Get the first amount
				var delta = Convert.ToDouble(trendPercentage) * this.StepRange;
				var step = Convert.ToDouble(random.NextDouble()) * this.StepRange;
				var firstAmount = (presetAmounts != null ? presetAmounts[0] : this.StartAmount + step - delta);
				this.Add(this.CreateData(random, 0, timePeriod, date, firstAmount));
				
				switch (timePeriod) {
					case TimePeriod.Month:
						date = date.AddMonths(1);
						break;
					case TimePeriod.Week:
						date = date.AddDays(7);
						break;
					default:  // Year
						date = date.AddYears(1);
						break;
				}

				for (int index = 1; index < resolvedDataPointCount; index++) {
					step = Convert.ToDouble(random.NextDouble()) * this.StepRange;
					var amount = (presetAmounts != null ? presetAmounts[index] : this[index - 1].Amount + step - delta);

					if (!this.AllowNegativeNumbers)
						amount = Math.Max(0, amount);

					this.Add(this.CreateData(random, index, timePeriod, date, amount));

					switch (timePeriod) {
						case TimePeriod.Month:
							date = date.AddMonths(1);
							break;
						case TimePeriod.Week:
							date = date.AddDays(7);
							break;
						default:  // Year
							date = date.AddYears(1);
							break;
					}
				}
			}
			finally {
				this.EndUpdate();
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that can be used to generate new data points.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that can be used to generate new data points.
		/// </value>
		public ICommand GenerateCommand {
			get {
				if (generateCommand == null) {
					generateCommand = new DelegateCommand<object>((param) => {
						this.Generate();
					});
				}

				return generateCommand;
			}
		}

		/// <summary>
		/// Raises the <c>CollectionChanged</c> event with the provided arguments.
		/// </summary>
		/// <param name="e">A <see cref="NotifyCollectionChangedEventArgs"/> that contains the event data.</param>
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
			if (isInitialized) { 
				// Call the base method
				base.OnCollectionChanged(e);
			}
			else if (!this.IsPropertyChangeSuspended)
				isInitialized = true;
		}

		/// <summary>
		/// Gets the partition maximum count (for nested data).
		/// </summary>
		/// <value>The partition maximum count.</value>
		public int PartitionMaxCount {
			get {
				return partitionMaxCount;
			}
			set {
				if (partitionMaxCount != value) {
					partitionMaxCount = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the preset amounts.
		/// </summary>
		/// <value>The preset amounts.</value>
		[TypeConverter(typeof(DelimitedDoubleListTypeConverter))]
		public IList<double> PresetAmounts {
			get {
				return presetAmounts;
			}
			set {
				if (presetAmounts != value) {
					presetAmounts = value;
					this.Invalidate();
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the preset titles.
		/// </summary>
		/// <value>The preset titles.</value>
		[TypeConverter(typeof(DelimitedStringListTypeConverter))]
		public IList<string> PresetTitles {
			get {
				return presetTitles;
			}
			set {
				if (presetTitles != value) {
					presetTitles = value;
					this.Invalidate();
				}
			}
		}
		
		/// <summary>
		/// Gets the random number seed.
		/// </summary>
		/// <value>The random number seed.</value>
		public int? RandomSeed {
			get {
				return randomSeed;
			}
			set {
				if (randomSeed != value) {
					randomSeed = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets the start amount.
		/// </summary>
		/// <value>The start amount.</value>
		public double StartAmount {
			get {
				return startAmount;
			}
			set {
				if (startAmount != value) {
					startAmount = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets the range over which any amount can change from the previous amount.
		/// </summary>
		/// <value>The range over which any amount can change from the previous amount.</value>
		public double StepRange {
			get {
				return stepRange;
			}
			set {
				if (stepRange != value) {
					stepRange = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the time period.
		/// </summary>
		/// <value>The time period.</value>
		public TimePeriod TimePeriod {
			get {
				return timePeriod;
			}
			set {
				if (timePeriod != value) {
					timePeriod = value;
					this.Invalidate();
				}
			}
		}
		
		/// <summary>
		/// Gets the sets the step trend for amounts.
		/// </summary>
		/// <value>The step trend for amounts.</value>
		public Trend Trend {
			get {
				return trend;
			}
			set {
				if (trend != value) {
					trend = value;
					this.Invalidate();
				}
			}
		}

	}

}
