namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Selects a <see cref="BulletContentControl"/> template selector.
/// </summary>
public class BulletContentControlTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for strings.
	/// </summary>
	public DataTemplate? StringTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return (item is string)
			? StringTemplate
			: null;
	}

}
