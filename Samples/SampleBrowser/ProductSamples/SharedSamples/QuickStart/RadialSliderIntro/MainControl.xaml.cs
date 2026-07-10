namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.RadialSliderIntro;

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

	private void OnFullCircleSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
		// Quit if not fully loaded yet
		if (fullCirclePositiveSlice is null)
			return;

		fullCirclePositiveSlice.Opacity = (fullCircleSlider.Value > 0.0 ? 1.0 : 0.0);
		fullCircleNegativeSlice.Opacity = (fullCircleSlider.Value < 0.0 ? 1.0 : 0.0);
	}

}
