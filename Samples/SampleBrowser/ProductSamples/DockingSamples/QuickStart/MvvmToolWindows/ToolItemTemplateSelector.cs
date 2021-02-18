using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows {

	/// <summary>
	/// Selects a tool item template.
	/// </summary>
	public class ToolItemTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use as the default.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use as the default.</value>
		public DataTemplate ToolItem1Template { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use as the default.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use as the default.</value>
		public DataTemplate ToolItem2Template { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use as the default.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use as the default.</value>
		public DataTemplate ToolItem3Template { get; set; }

		/// <summary>
		/// Selects a data template.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container to examine.</param>
		/// <returns>The <see cref="DataTemplate"/> that was selected.</returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is ToolItem1ViewModel)
				return this.ToolItem1Template;
			else if (item is ToolItem2ViewModel)
				return this.ToolItem2Template;
			else if (item is ToolItem3ViewModel)
				return this.ToolItem3Template;
			else
				return null;
		}

	}

}
