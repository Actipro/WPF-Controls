namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale;

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
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDecreaseButtonClick(object? sender, RoutedEventArgs e) {
		var heading = headingMarker.Value;
		heading--;

		var requiresWrap = (heading < -180);
		if (requiresWrap)
			heading += 360;

		headingMarker.Value = heading;
	}

	private void OnIncreaseButtonClick(object? sender, RoutedEventArgs e) {
		var heading = headingMarker.Value;
		heading++;

		var requiresWrap = (heading > 180);
		if (requiresWrap)
			heading -= 360;

		headingMarker.Value = heading;
	}

}
