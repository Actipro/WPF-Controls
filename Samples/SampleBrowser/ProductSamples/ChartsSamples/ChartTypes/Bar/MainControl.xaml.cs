using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ChartsSamples.ChartTypes.Bar;

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
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of bar group spacings.
	/// </summary>
	public IList<Unit> BarGroupSpacings {
		get {
			return [
				Unit.Percentage(15),
				Unit.Percentage(25),
				Unit.Percentage(35),
				Unit.Percentage(45)
			];
		}
	}

	/// <summary>
	/// The collection of bar spacings.
	/// </summary>
	public IList<Unit> BarSpacings {
		get {
			return [
				Unit.Pixel(0),
				Unit.Pixel(1),
				Unit.Pixel(2),
				Unit.Pixel(5)
			];
		}
	}

}
