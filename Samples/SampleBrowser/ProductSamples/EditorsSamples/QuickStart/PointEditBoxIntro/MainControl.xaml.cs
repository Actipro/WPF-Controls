using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.PointEditBoxIntro;

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

		Formats = PredefinedFormats.Point;

		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The predefined formats.
	/// </summary>
	public IEnumerable<PredefinedFormat> Formats { get; set; }

}
