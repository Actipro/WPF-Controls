using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.TimeSpanEditBoxIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		Formats = PredefinedFormats.TimeSpan;
		CurrentValue = new TimeSpan(3, 15, 0);
		MinimumValue = TimeSpan.MinValue;
		MaximumValue = TimeSpan.MaxValue;

		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current value.
	/// </summary>
	public TimeSpan? CurrentValue { get; set; }

	/// <summary>
	/// The predefined formats.
	/// </summary>
	public IEnumerable<PredefinedFormat> Formats { get; set; }

	/// <summary>
	/// The maximum value.
	/// </summary>
	public TimeSpan MaximumValue { get; set; }

	/// <summary>
	/// The minimum value.
	/// </summary>
	public TimeSpan MinimumValue { get; set; }

}
