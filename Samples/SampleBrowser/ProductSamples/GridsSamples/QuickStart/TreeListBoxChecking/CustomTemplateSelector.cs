namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxChecking;

/// <summary>
/// Chooses a <see cref="DataTemplate"/> based on the data object and the data-bound element.
/// </summary>
public class CustomTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for fields.
	/// </summary>
	public DataTemplate? FieldTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for folders.
	/// </summary>
	public DataTemplate? FolderTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			FieldTreeNodeModel => FieldTemplate,
			_ => FolderTemplate
		};
	}

}
