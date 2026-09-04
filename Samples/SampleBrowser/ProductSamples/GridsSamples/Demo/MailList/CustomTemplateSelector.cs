namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList;

/// <summary>
/// Chooses a <see cref="DataTemplate"/> based on the data object and the data-bound element.
/// </summary>
public class CustomTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for groups.
	/// </summary>
	public DataTemplate? GroupTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for mail.
	/// </summary>
	public DataTemplate? MailTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			MailTreeNodeModel => MailTemplate,
			_ => GroupTemplate
		};
	}

}
