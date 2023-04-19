/*

RIBBON GETTING STARTED SERIES - STEP 9

STEP SUMMARY:

	This class defines DataTemplate properties for each tab that will be displayed
	on the Backstage and allow a RibbonBackstageTabViewModel to be mapped to the
	appropriate content.

	The view will create an instance of this type and define the DataTemplate for
	each tab.

CHANGES SINCE LAST STEP:

	This is the first step containing this file.

*/

using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step09 {

	/// <summary>
	/// Gets a <see cref="DataTemplateSelector"/> for view models that define a
	/// tab on the Backstage of the Ribbon.
	/// </summary>
	public class SampleBackstageTabContentTemplateSelector : DataTemplateSelector {

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> for the "Home" tab.
		/// </summary>
		/// <value>A <see cref="DataTemplate"/>.</value>
		public DataTemplate HomeTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> for the "New" tab.
		/// </summary>
		/// <value>A <see cref="DataTemplate"/>.</value>
		public DataTemplate NewTemplate { get; set; }

		/// <inheritdoc />
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is RibbonBackstageTabViewModel viewModel) {
				// Determine which DataTemplate to use based on the view model key
				switch (viewModel.Key) {
					case SampleBarControlKeys.BackstageTabHome:
						return this.HomeTemplate;
					case SampleBarControlKeys.BackstageTabNew:
						return this.NewTemplate;
				}
			}

			return base.SelectTemplate(item, container);
		}

	}

}
