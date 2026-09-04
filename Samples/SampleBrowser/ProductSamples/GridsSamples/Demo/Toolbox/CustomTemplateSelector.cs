namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Chooses a <see cref="DataTemplate"/> based on the data object and the data-bound element.
/// </summary>
public class CustomTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for categories.
	/// </summary>
	public DataTemplate? CategoryTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for controls.
	/// </summary>
	public DataTemplate? ControlTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a placeholder in an empty category.
	/// </summary>
	public DataTemplate? EmptyPlaceholderTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			ControlTreeNodeModel => ControlTemplate,
			CategoryTreeNodeModel => CategoryTemplate,
			EmptyPlaceholderTreeNodeModel => EmptyPlaceholderTemplate,
			_ => base.SelectTemplate(item, container)
		};
	}

}
