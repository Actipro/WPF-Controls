using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.BadgeIntro;

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

	private void OnRuntimeSampleToggleBadgeButtonClick(object sender, RoutedEventArgs e) {
		var badge = BadgeService.GetBadge(runtimeAdornedSample);
		if (badge is null) {
			// Add badge
			badge = new Badge() {
				Content = DateTime.Now.ToLongTimeString(),
			};
			BadgeService.SetBadge(runtimeAdornedSample, badge);
		}
		else {
			// Remove badge
			BadgeService.SetBadge(runtimeAdornedSample, null);
		}
	}
}
