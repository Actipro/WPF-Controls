namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Selects an application overlay content template.
/// </summary>
public class ApplicationOverlayContentTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use as the default.
	/// </summary>
	public DataTemplate? ExternalSampleTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a Backstage.
	/// </summary>
	public DataTemplate? HomeBackstageOverlay { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a Backstage.
	/// </summary>
	public DataTemplate? ProductItemInfoBackstageOverlay { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a Backstage.
	/// </summary>
	public DataTemplate? ReleaseHistoryBackstageOverlay { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		switch (item) {
			case string:
				return ExternalSampleTemplate;
			case ApplicationViewModel { ViewItemInfo: { } viewItemInfo }:
				if (viewItemInfo.IsReleaseHistory)
					return ReleaseHistoryBackstageOverlay;
				else if (viewItemInfo.IsUtility)
					return UtilitiesBackstageOverlay;
				else
					return ProductItemInfoBackstageOverlay;
			default:
				return HomeBackstageOverlay;
		}
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a Backstage.
	/// </summary>
	public DataTemplate? UtilitiesBackstageOverlay { get; set; }

}
