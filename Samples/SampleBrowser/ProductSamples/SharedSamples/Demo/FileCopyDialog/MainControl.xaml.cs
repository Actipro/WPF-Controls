using ActiproSoftware.Windows.Controls;
using System.Windows.Threading;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SharedSamples.Demo.FileCopyDialog;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	/// <summary>
	/// Holds a <see cref="DispatchTimer"/> used to simulate a file copy operation.
	/// </summary>
	private DispatcherTimer? _fileCopyTimer;

	/// <summary>
	/// Holds a random number generator.
	/// </summary>
	private readonly Random _random = new();

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="CanSimulateAnError"/> property.
	/// </summary>
	public static readonly DependencyProperty CanSimulateAnErrorProperty
		= DependencyProperty.Register(nameof(CanSimulateAnError), typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: true));

	/// <summary>
	/// Defines the <see cref="CanSimulateAPause"/> property.
	/// </summary>
	public static readonly DependencyProperty CanSimulateAPauseProperty
		= DependencyProperty.Register(nameof(CanSimulateAPause), typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: true));

	/// <summary>
	/// Defines the <see cref="FileCopyData"/> property.
	/// </summary>
	public static readonly DependencyProperty FileCopyDataProperty
		= DependencyProperty.Register(nameof(FileCopyData), typeof(FileCopyData), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		FileCopyData = new FileCopyData();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="FileCopyData"/> with test values.
	/// </summary>
	private static FileCopyData CreateSampleFileCopyData() {
		var data = new FileCopyData();
		data.TotalFileCount = data.RemainingFileCount = 51;
		data.TotalFileSize = data.RemainingFileSize = 59.3;
		data.TimeRemaining = new TimeSpan(0, 15, 45);
		return data;
	}

	private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
		if (_fileCopyTimer is not null) {
			_fileCopyTimer.Stop();
			_fileCopyTimer.Tick -= OnFileCopyTimerTick;
			_fileCopyTimer = null;
		}
	}

	private void OnFileCopyTimerTick(object? sender, EventArgs e) {
		if ((_fileCopyTimer is not null) && (FileCopyData.RemainingFileCount == 0)) {
			_fileCopyTimer.Stop();
			_fileCopyTimer.Tick -= OnFileCopyTimerTick;
			_fileCopyTimer = null;
		}
		else {
			SimulateFileCopy();
		}
	}

	private void OnStartButtonClick(object sender, RoutedEventArgs e) {
		FileCopyData = CreateSampleFileCopyData();

		_fileCopyTimer = new DispatcherTimer {
			Interval = TimeSpan.FromMilliseconds(100)
		};
		_fileCopyTimer.Tick += OnFileCopyTimerTick;
		_fileCopyTimer.IsEnabled = true;
	}

	/// <summary>
	/// Simulates the file copy.
	/// </summary>
	private void SimulateFileCopy() {
		var fileCopyData = FileCopyData;
		if (fileCopyData.RemainingFileCount != 0) {
			if (CanSimulateAPause && (fileCopyData.RemainingFileCount == 40)) {
				animatedProgressBar.State = OperationState.Paused;
				MessageBox.Show("The file 'xyz.txt' already exists, would you like to overwrite it?", "File Copy (Simulated)", MessageBoxButton.YesNo, MessageBoxImage.Question);
				animatedProgressBar.State = OperationState.Normal;
			}
			else if (CanSimulateAnError && (fileCopyData.RemainingFileCount == 30)) {
				animatedProgressBar.State = OperationState.Error;
				MessageBox.Show("An error occurred while copying the file 'abc.txt'.", "File Copy (Simulated)", MessageBoxButton.OK, MessageBoxImage.Error);
				animatedProgressBar.State = OperationState.Normal;
			}

			var fileTime = fileCopyData.TimeRemaining.TotalSeconds / fileCopyData.RemainingFileCount;
			var fileSize = fileCopyData.RemainingFileSize / fileCopyData.RemainingFileCount;
			fileCopyData.RemainingFileCount--;
			fileCopyData.RemainingFileSize -= fileSize;
			fileCopyData.CopiedFileSize = fileCopyData.TotalFileSize - fileCopyData.RemainingFileSize;
			fileCopyData.Speed = ((double)_random.Next(6000) + 3000) / 100.0;
			fileCopyData.TimeRemaining -= TimeSpan.FromSeconds(fileTime);
		}
		FileCopyData = fileCopyData;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether a pause can be simulated.
	/// </summary>
	public bool CanSimulateAPause {
		get => (bool)GetValue(CanSimulateAPauseProperty);
		set => SetValue(CanSimulateAPauseProperty, value);
	}

	/// <summary>
	/// Indicates whether an error can be simulated.
	/// </summary>
	public bool CanSimulateAnError {
		get => (bool)GetValue(CanSimulateAnErrorProperty);
		set => SetValue(CanSimulateAnErrorProperty, value);
	}

	/// <summary>
	/// The file copy meta-data associated with this control.
	/// </summary>
	public FileCopyData FileCopyData {
		get => (FileCopyData)GetValue(FileCopyDataProperty);
		set => SetValue(FileCopyDataProperty, value);
	}

}
