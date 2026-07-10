using ActiproSoftware.Extensions;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;
using System.Collections.Specialized;

namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Dynamically generates random data to be used with various samples.
/// Normally, data would come from sources such as database instead.
/// </summary>
public class TimeAggregatedDataGenerator : DeferrableObservableCollection<TimeAggregatedData>, ICollection, IEnumerable {

	private bool _allowNegativeNumbers;
	private int? _dataPointCount;
	private ICommand? _generateCommand;
	private bool _isInitialized;
	private int _partitionMaxCount;
	private IList<double>? _presetAmounts;
	private IList<string>? _presetTitles;
	private int? _randomSeed;
	private double _startAmount = 120000;
	private double _stepRange = 30000;
	private TimePeriod _timePeriod = TimePeriod.Year;
	private Trend _trend = Trend.Random;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TimeAggregatedDataGenerator() { }

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="amounts">The specific amounts to use, instead of generating amounts.</param>
	public TimeAggregatedDataGenerator(IList<double> amounts) {
		_presetAmounts = amounts;
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	int ICollection.Count {
		get {
			if (!_isInitialized)
				Generate();

			return Count;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() {
		if (!_isInitialized)
			Generate();

		return GetEnumerator();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a data instance.
	/// </summary>
	/// <param name="random">The <see cref="Random"/> instance to use.</param>
	/// <param name="index">The data item index.</param>
	/// <param name="timePeriod">The time period.</param>
	/// <param name="date">The time period start date for which the amount is specified.</param>
	/// <param name="amount">The sales amount.</param>
	private TimeAggregatedData CreateData(Random random, int index, TimePeriod timePeriod, DateTime date, double amount) {
		var data = new TimeAggregatedData(index, timePeriod, date, amount);

		if ((_presetTitles is not null) && (index < _presetTitles.Count))
			data.Title = _presetTitles[index];

		if (_partitionMaxCount >= 2) {
			var partitionCount = random.Next(2, _partitionMaxCount);

			var partitions = new NumericData[partitionCount];
			for (var partitionIndex = 0; partitionIndex < partitionCount; partitionIndex++)
				partitions[partitionIndex] = new NumericData(random.Next(1, 100));

			data.Partitions = partitions;
		}

		return data;
	}
	/// <summary>
	/// The default data point count.
	/// </summary>
	private int DefaultDataPointCount {
		get => _timePeriod switch {
			TimePeriod.Month => 12,
			TimePeriod.Year or _ => 10
		};
	}

	/// <summary>
	/// Invalidates the data points.
	/// </summary>
	private void Invalidate() {
		if (_isInitialized)
			Generate();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether to allow negative numbers.
	/// </summary>
	public bool AllowNegativeNumbers {
		get => _allowNegativeNumbers;
		set {
			if (_allowNegativeNumbers != value) {
				_allowNegativeNumbers = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The number of data points to generate in each data set.
	/// </summary>
	public int DataPointCount {
		get => _presetAmounts?.Count ?? _dataPointCount ?? DefaultDataPointCount;
		set {
			value = Math.Max(1, value);

			if (_dataPointCount != value) {
				_dataPointCount = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Generates data points.
	/// </summary>
	public void Generate() {
		// Create a random number generator
		var resolvedRandomSeed = _randomSeed ?? Environment.TickCount;
		var random = new Random(resolvedRandomSeed);

		// Initialize the date
		DateTime date;
		var resolvedDataPointCount = DataPointCount;
		switch (_timePeriod) {
			case TimePeriod.Month:
				date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
					.AddMonths(-resolvedDataPointCount);
				break;
			case TimePeriod.Week:
				date = DateTime.Today
					.AddDays(-resolvedDataPointCount * 7);
				break;
			case TimePeriod.Year:
			default:
				date = new DateTime(DateTime.Today.Year, 1, 1)
					.AddYears(-resolvedDataPointCount);
				break;
		}

		// Initialize the results with the first data item
		BeginUpdate();
		try {
			Clear();

			// Quit if there are no data points
			if (resolvedDataPointCount == 0)
				return;

			// Determine the trend percentage
			var trendPercentage = _trend switch {
				Trend.Upward => 0.3,
				Trend.Downward => 0.7,
				Trend.Random or _ => 0.5
			};

			// Get the first amount
			var delta = Convert.ToDouble(trendPercentage) * StepRange;
			var step = Convert.ToDouble(random.NextDouble()) * StepRange;
			var firstAmount = (_presetAmounts is not null ? _presetAmounts[0] : StartAmount + step - delta);
			Add(CreateData(random, index: 0, _timePeriod, date, firstAmount));

			// Define a function for advancing a date by a given time period
			static DateTime AdvanceDate(DateTime date, TimePeriod timePeriod) {
				return timePeriod switch {
					TimePeriod.Month => date.AddMonths(1),
					TimePeriod.Week => date.AddDays(7),
					TimePeriod.Year or _ => date.AddYears(1)
				};
			}

			date = AdvanceDate(date, _timePeriod);

			for (var index = 1; index < resolvedDataPointCount; index++) {
				step = Convert.ToDouble(random.NextDouble()) * StepRange;
				var amount = (_presetAmounts is not null ? _presetAmounts[index] : this[index - 1].Amount + step - delta);

				if (!AllowNegativeNumbers)
					amount = amount.ClampToNonnegative();

				Add(CreateData(random, index, _timePeriod, date, amount));

				date = AdvanceDate(date, _timePeriod);
			}
		}
		finally {
			EndUpdate();
		}
	}

	/// <summary>
	/// The <see cref="ICommand"/> that can be used to generate new data points.
	/// </summary>
	public ICommand GenerateCommand
		=> _generateCommand ??= new DelegateCommand<object>(_ => Generate());

	/// <inheritdoc/>
	protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
		if (_isInitialized)
			base.OnCollectionChanged(e);
		else if (!IsPropertyChangeSuspended)
			_isInitialized = true;
	}

	/// <summary>
	/// The partition maximum count (for nested data).
	/// </summary>
	public int PartitionMaxCount {
		get => _partitionMaxCount;
		set {
			if (_partitionMaxCount != value) {
				_partitionMaxCount = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The preset amounts.
	/// </summary>
	[TypeConverter(typeof(DelimitedDoubleListTypeConverter))]
	public IList<double>? PresetAmounts {
		get => _presetAmounts;
		set {
			if (_presetAmounts != value) {
				_presetAmounts = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The preset titles.
	/// </summary>
	[TypeConverter(typeof(DelimitedStringListTypeConverter))]
	public IList<string>? PresetTitles {
		get => _presetTitles;
		set {
			if (_presetTitles != value) {
				_presetTitles = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The random number seed.
	/// </summary>
	public int? RandomSeed {
		get => _randomSeed;
		set {
			if (_randomSeed != value) {
				_randomSeed = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The start amount.
	/// </summary>
	public double StartAmount {
		get => _startAmount;
		set {
			if (_startAmount != value) {
				_startAmount = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The range over which any amount can change from the previous amount.
	/// </summary>
	public double StepRange {
		get => _stepRange;
		set {
			if (_stepRange != value) {
				_stepRange = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The time period.
	/// </summary>
	public TimePeriod TimePeriod {
		get => _timePeriod;
		set {
			if (_timePeriod != value) {
				_timePeriod = value;
				Invalidate();
			}
		}
	}

	/// <summary>
	/// The sets the step trend for amounts.
	/// </summary>
	public Trend Trend {
		get => _trend;
		set {
			if (_trend != value) {
				_trend = value;
				Invalidate();
			}
		}
	}

}
