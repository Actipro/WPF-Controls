using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.TimeEditBoxIntro;

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

		Formats = PredefinedFormats.Time;
		CurrentValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 35, 0);
		MinimumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
		MaximumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current value.
	/// </summary>
	public DateTime? CurrentValue { get; set; }

	/// <summary>
	/// The predefined formats.
	/// </summary>
	public IEnumerable<PredefinedFormat> Formats { get; set; }

	/// <summary>
	/// The maximum value.
	/// </summary>
	public DateTime MaximumValue { get; set; }

	/// <summary>
	/// The minimum value.
	/// </summary>
	public DateTime MinimumValue { get; set; }

}
