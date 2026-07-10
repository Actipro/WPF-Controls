namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Provides a <see cref="DataTemplateSelector"/> that selects content templates for various ribbon footer content view models,
/// generally used with a <see cref="RibbonFooterControl"/> and assigned to its <see cref="ContentControl.ContentTemplateSelector"/> property.
/// </summary>
public class RibbonFooterContentTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RibbonFooterContentTemplateSelector() {
		var dictionary = BarsMvvmResourceDictionary.Instance;

		InfoBarDataTemplate = dictionary[BarsMvvmResourceKeys.RibbonFooterContentInfoBarDataTemplate] as DataTemplate;
		SimpleDataTemplate = dictionary[BarsMvvmResourceKeys.RibbonFooterContentSimpleDataTemplate] as DataTemplate;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			RibbonFooterInfoBarContentViewModel _ => InfoBarDataTemplate,
			RibbonFooterSimpleContentViewModel _ => SimpleDataTemplate,
			_ => base.SelectTemplate(item, container)
		};
	}

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="RibbonFooterInfoBarContentViewModel"/>.
	/// </summary>
	public DataTemplate? InfoBarDataTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="RibbonFooterSimpleContentViewModel"/>.
	/// </summary>
	public DataTemplate? SimpleDataTemplate { get; set; }

}
