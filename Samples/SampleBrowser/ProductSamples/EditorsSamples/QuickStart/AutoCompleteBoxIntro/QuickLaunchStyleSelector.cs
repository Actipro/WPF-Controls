namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.AutoCompleteBoxIntro;

/// <summary>
/// Chooses a <see cref="Style"/> based on the data object and the data-bound element.
/// </summary>
public class QuickLaunchStyleSelector : StyleSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Style"/> to use for items.
	/// </summary>
	public Style? ItemStyle { get; set; }

	/// <summary>
	/// The <see cref="Style"/> to use for separators.
	/// </summary>
	public Style? SeparatorStyle { get; set; }

	/// <inheritdoc/>
	public override Style? SelectStyle(object item, DependencyObject container) {
		return (item is QuickLaunchSeparator)
			? SeparatorStyle
			: ItemStyle;
	}

}
