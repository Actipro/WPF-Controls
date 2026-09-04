namespace ActiproSoftware.ProductSamples.SharedSamples.Demo.FileCopyDialog;

/// <summary>
/// Represents meta-data for a file copy operation.
/// </summary>
public class FileCopyData : DependencyObject {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="CopiedFileSize"/> property.
	/// </summary>
	public static readonly DependencyProperty CopiedFileSizeProperty
		= DependencyProperty.Register(nameof(CopiedFileSize), typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: 0.0));

	/// <summary>
	/// Defines the <see cref="RemainingFileCount"/> property.
	/// </summary>
	public static readonly DependencyProperty RemainingFileCountProperty
		= DependencyProperty.Register(nameof(RemainingFileCount), typeof(int), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: 0));

	/// <summary>
	/// Defines the <see cref="RemainingFileSize"/> property.
	/// </summary>
	public static readonly DependencyProperty RemainingFileSizeProperty
		= DependencyProperty.Register(nameof(RemainingFileSize), typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: 0.0));

	/// <summary>
	/// Defines the <see cref="Speed"/> property.
	/// </summary>
	public static readonly DependencyProperty SpeedProperty
		= DependencyProperty.Register(nameof(Speed), typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: 0.0));

	/// <summary>
	/// Defines the <see cref="TimeRemaining"/> property.
	/// </summary>
	public static readonly DependencyProperty TimeRemainingProperty
		= DependencyProperty.Register(nameof(TimeRemaining), typeof(TimeSpan), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: TimeSpan.Zero));

	/// <summary>
	/// Defines the <see cref="TotalFileCount"/> property.
	/// </summary>
	public static readonly DependencyProperty TotalFileCountProperty
		= DependencyProperty.Register(nameof(TotalFileCount), typeof(int), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: 0));

	/// <summary>
	/// Defines the <see cref="TotalFileSize"/> property.
	/// </summary>
	public static readonly DependencyProperty TotalFileSizeProperty
		= DependencyProperty.Register(nameof(TotalFileSize), typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(defaultValue: 1.0));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The size, in gigabytes, of the files already copied.
	/// </summary>
	public double CopiedFileSize {
		get => (double)GetValue(CopiedFileSizeProperty);
		set => SetValue(CopiedFileSizeProperty, value);
	}

	/// <summary>
	/// The number of files still waiting to be copied.
	/// </summary>
	public int RemainingFileCount {
		get => (int)GetValue(RemainingFileCountProperty);
		set => SetValue(RemainingFileCountProperty, value);
	}

	/// <summary>
	/// The size, in gigabytes, of the files still waiting to be copied.
	/// </summary>
	public double RemainingFileSize {
		get => (double)GetValue(RemainingFileSizeProperty);
		set => SetValue(RemainingFileSizeProperty, value);
	}

	/// <summary>
	/// The speed of the file copy operation in megabytes per second.
	/// </summary>
	public double Speed {
		get => (double)GetValue(SpeedProperty);
		set => SetValue(SpeedProperty, value);
	}

	/// <summary>
	/// The <see cref="TimeSpan"/> remaining.
	/// </summary>
	public TimeSpan TimeRemaining {
		get => (TimeSpan)GetValue(TimeRemainingProperty);
		set => SetValue(TimeRemainingProperty, value);
	}

	/// <summary>
	/// The total number of files being copied.
	/// </summary>
	public int TotalFileCount {
		get => (int)GetValue(TotalFileCountProperty);
		set => SetValue(TotalFileCountProperty, value);
	}

	/// <summary>
	/// The total size, in gigabytes, of the files being copied.
	/// </summary>
	public double TotalFileSize {
		get => (double)GetValue(TotalFileSizeProperty);
		set => SetValue(TotalFileSizeProperty, value);
	}

}
