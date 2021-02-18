using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Selects an application overlay content template.
	/// </summary>
	public class ApplicationOverlayContentTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use as the default.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use as the default.</value>
		public DataTemplate ExternalSampleTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a Backstage.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for a Backstage.</value>
		public DataTemplate HomeBackstageOverlay { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a Backstage.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for a Backstage.</value>
		public DataTemplate ProductItemInfoBackstageOverlay { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a Backstage.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for a Backstage.</value>
		public DataTemplate ReleaseHistoryBackstageOverlay { get; set; }
		
		/// <summary>
		/// Selects a data template.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container to examine.</param>
		/// <returns>The <see cref="DataTemplate"/> that was selected.</returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is string)
				return this.ExternalSampleTemplate;

			var appViewModel = item as ApplicationViewModel;
			if (appViewModel?.ViewItemInfo != null) {
				if (appViewModel.ViewItemInfo.IsReleaseHistory)
					return this.ReleaseHistoryBackstageOverlay;
				else if (appViewModel.ViewItemInfo.IsUtility)
					return this.UtilitiesBackstageOverlay;
				else
					return this.ProductItemInfoBackstageOverlay;
			}
			else
				return this.HomeBackstageOverlay;
		}
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a Backstage.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for a Backstage.</value>
		public DataTemplate UtilitiesBackstageOverlay { get; set; }
		
	}

}
