using ActiproSoftware.Windows.Themes;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides a <see cref="DataTemplateSelector"/> that selects content templates for various ribbon footer content view models,
	/// generally used with a <see cref="RibbonFooterControl"/> and assigned to its <see cref="ContentControl.ContentTemplateSelector"/> property.
	/// </summary>
	public class RibbonFooterContentTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="RibbonFooterContentTemplateSelector"/> class.
		/// </summary>
		public RibbonFooterContentTemplateSelector() {
			var dictionary = BarsMvvmResourceDictionary.Instance;

			this.SimpleDataTemplate = dictionary[BarsMvvmResourceKeys.RibbonFooterContentSimpleDataTemplate] as DataTemplate;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
			=> item switch {
				RibbonFooterSimpleContentViewModel _ => this.SimpleDataTemplate,
				_ => base.SelectTemplate(item, container)
			};

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="RibbonFooterSimpleContentViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate SimpleDataTemplate { get; set; }

	}

}
