using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.GuidEditBoxIntro;

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

		CurrentValue = Guid.NewGuid();
		Formats = PredefinedFormats.Guid;

		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current value.
	/// </summary>
	public Guid? CurrentValue { get; set; }

	/// <summary>
	/// The predefined formats.
	/// </summary>
	public IEnumerable<PredefinedFormat> Formats { get; set; }

}
