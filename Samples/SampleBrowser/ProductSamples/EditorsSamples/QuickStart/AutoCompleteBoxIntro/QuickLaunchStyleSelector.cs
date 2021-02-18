#if WINRT
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.AutoCompleteBoxIntro {

	/// <summary>
	/// Chooses a <see cref="Style"/> based on the data object and the data-bound element.
	/// </summary>
	public class QuickLaunchStyleSelector : StyleSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="Style"/> to use for items.
		/// </summary>
		/// <value>The <see cref="Style"/> to use for items.</value>
		public Style ItemStyle { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="Style"/> to use for separators.
		/// </summary>
		/// <value>The <see cref="Style"/> to use for separators.</value>
		public Style SeparatorStyle { get; set; }
		
		/// <summary>
		/// Returns a <see cref="Style"/> based on custom logic.
		/// </summary>
		/// <param name="item">The data object.</param>
		/// <param name="container">The data-bound element.</param>
		/// <returns>The <see cref="Style"/> to use.</returns>
		#if WINRT
		protected override Style SelectStyleCore(object item, DependencyObject container) {
		#else
		public override Style SelectStyle(object item, DependencyObject container) {
		#endif
			if (item is QuickLaunchSeparator)
				return this.SeparatorStyle;
			else
				return this.ItemStyle;
		}

	}

}
