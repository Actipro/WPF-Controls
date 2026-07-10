using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Provides an abstract base class for a random data generator, to be used with various samples.
/// Normally, data would come from sources such as database instead.
/// </summary>
/// <typeparam name="TOptions">The options type.</typeparam>
/// <typeparam name="TData">The data type.</typeparam>
[ContentProperty(nameof(Options))]
public abstract class DataGeneratorBase<TOptions, TData> where TOptions : class {

	private bool _allowNegativeNumbers;
	private int _dataSetCount = 1;
	private TOptions? _options;

	private static readonly Random _random = new(Environment.TickCount);

	// --------------------------------------------------------------------------------------------------//
	// OBJECT
	// --------------------------------------------------------------------------------------------------//

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DataGeneratorBase() {
		RegenerateDataSets();
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
			if (_allowNegativeNumbers == value)
				return;

			_allowNegativeNumbers = value;

			RegenerateDataSets();
		}
	}

	/// <summary>
	/// The data sets that can be bound to one or more series.
	/// </summary>
	public DeferrableObservableCollection<ICollection<TData>> DataSets { get; } = [];

	/// <summary>
	/// The data set count.
	/// </summary>
	public int DataSetCount {
		get => _dataSetCount;
		set {
			if (_dataSetCount == value)
				return;

			_dataSetCount = Math.Max(1, value);

			RegenerateDataSets();
		}
	}

	/// <summary>
	/// Generates a single data set based on the current options.
	/// </summary>
	protected abstract ICollection<TData> Generate();

	/// <summary>
	/// The options to use.
	/// </summary>
	public TOptions? Options {
		get => _options;
		set {
			if (_options == value)
				return;

			_options = value;

			RegenerateDataSets();
		}
	}

	/// <summary>
	/// The random number generator to use.
	/// </summary>
	protected Random Random
		=> _random;

	/// <summary>
	/// Regenerates all data sets.
	/// </summary>
	public void RegenerateDataSets() {
		DataSets.BeginUpdate();
		try {
			DataSets.Clear();
			for (var index = 0; index < _dataSetCount; index++)
				DataSets.Add(Generate());
		}
		finally {
			DataSets.EndUpdate();
		}
	}

}
