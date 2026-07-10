namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows;

/// <summary>
/// Selects a tool item template.
/// </summary>
public class ToolItemTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use as the default.
	/// </summary>
	public DataTemplate? ToolItem1Template { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use as the default.
	/// </summary>
	public DataTemplate? ToolItem2Template { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use as the default.
	/// </summary>
	public DataTemplate? ToolItem3Template { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			ToolItem1ViewModel => ToolItem1Template,
			ToolItem2ViewModel => ToolItem2Template,
			ToolItem3ViewModel => ToolItem3Template,
			_ => null
		};
	}

}
