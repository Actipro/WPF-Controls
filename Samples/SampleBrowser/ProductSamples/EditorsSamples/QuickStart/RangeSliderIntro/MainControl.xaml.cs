using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.RangeSliderIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private DelegateCommand<object>? _addMultiValueCommand;
	private ICommand? _clearMultiValuesCommand;
	private DelegateCommand<double?>? _removeMultiValueCommand;

	#if NET
	private static readonly Random _random = Random.Shared;
	#else
	private static readonly Random _random = new();
	#endif

	public static readonly DependencyProperty MultiValuesProperty
		= DependencyProperty.Register(nameof(MultiValues), typeof(ObservableCollection<double>), typeof(MainControl));

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		// Initialize the collection of multiple values
		MultiValues = [25, 50, 75];
		MultiValues.CollectionChanged += (_, _) => {
			// Allow the add/remove commands to be disabled if max/min values have been reached
			_addMultiValueCommand?.RaiseCanExecuteChanged();
			_removeMultiValueCommand?.RaiseCanExecuteChanged();
		};

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command to add a random new value to <see cref="MultiValues"/>.
	/// </summary>
	public ICommand AddMultiValueCommand
		=> _addMultiValueCommand ??= new DelegateCommand<object>(
			executeAction: _ => {
				// Create a new value that is within the allowed range
				double newValue = _random.Next(
					(int)multiSlider.Minimum.Round(RoundMode.Ceiling),
					(int)multiSlider.Maximum.Round(RoundMode.Floor));

				MultiValues.Add(newValue);
			},
			canExecuteFunc: _ => MultiValues.Count < multiSlider.RangeEditMaximumValueCount
		);

	/// <summary>
	/// The command to clear all values from <see cref="MultiValues"/>.
	/// </summary>
	public ICommand ClearMultiValuesCommand
		=> _clearMultiValuesCommand ??= new DelegateCommand<object>(_ => MultiValues.Clear());

	/// <summary>
	/// A collection of all the values used in the multi values sample.
	/// </summary>
	public ObservableCollection<double> MultiValues {
		get => (ObservableCollection<double>)GetValue(MultiValuesProperty);
		set => SetValue(MultiValuesProperty, value);
	}

	/// <summary>
	/// The command to remove a value <see cref="MultiValues"/>.
	/// </summary>
	public ICommand RemoveMultiValueCommand {
		get => _removeMultiValueCommand ??= new DelegateCommand<double?>(
			executeAction: param => {
				if (param.HasValue)
					MultiValues.Remove(param.Value);
			},
			canExecuteFunc: param => param.HasValue && (MultiValues.Count > multiSlider.RangeEditMinimumValueCount)
		);
	}

}
