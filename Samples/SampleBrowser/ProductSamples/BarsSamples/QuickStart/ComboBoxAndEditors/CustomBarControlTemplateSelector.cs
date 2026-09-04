using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors;

/// <summary>
/// Provides an <see cref="ItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
/// generally assigned to root bar controls, like to <see cref="Ribbon"/>'s <see cref="Ribbon.ItemContainerTemplateSelector"/> property.
/// </summary>
public class CustomBarControlTemplateSelector : BarControlTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="AutoCompleteBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? AutoCompleteBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="ColorEditBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? ColorEditBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="DateEditBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? DateEditBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="Int32EditBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? Int32EditBoxDefaultTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, ItemsControl parentItemsControl) {
		return item switch {
			AutoCompleteBoxViewModel => AutoCompleteBoxDefaultTemplate,
			ColorEditBoxViewModel => ColorEditBoxDefaultTemplate,
			DateEditBoxViewModel => DateEditBoxDefaultTemplate,
			Int32EditBoxViewModel => Int32EditBoxDefaultTemplate,
			_ => base.SelectTemplate(item, parentItemsControl)
		};
	}

}
