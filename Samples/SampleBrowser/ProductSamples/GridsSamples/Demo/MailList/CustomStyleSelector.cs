namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList;

/// <summary>
/// Chooses a <see cref="Style"/> based on the data object and the data-bound element.
/// </summary>
public class CustomStyleSelector : StyleSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Style"/> to use for groups.
	/// </summary>
	public Style? GroupStyle { get; set; }

	/// <summary>
	/// The <see cref="Style"/> to use for mail.
	/// </summary>
	public Style? MailStyle { get; set; }

	/// <inheritdoc/>
	public override Style? SelectStyle(object item, DependencyObject container) {
		return item switch {
			MailTreeNodeModel => MailStyle,
			_ => GroupStyle
		};
	}

}
