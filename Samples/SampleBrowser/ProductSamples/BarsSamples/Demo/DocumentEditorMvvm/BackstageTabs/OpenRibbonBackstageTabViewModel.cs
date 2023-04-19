using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "Open" tab control within a ribbon backstage.
	/// </summary>
	public class OpenRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		/// <param name="recentDocumentManager">The <see cref="Windows.DocumentManagement.RecentDocumentManager"/>.</param>
		public OpenRibbonBackstageTabViewModel(BarManager barManager, RecentDocumentManager recentDocumentManager)
			: base(BarControlKeys.BackstageTabOpen, "Open") {

			this.barManager = barManager;
			this.RecentDocumentManager = recentDocumentManager;
			
			this.LargeImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabOpen, BarImageSize.Large);
			this.SmallImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabOpen, BarImageSize.Small);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
		public ICommand NotImplementedCommand => barManager?.NotImplementedCommand;

		/// <summary>
		/// Gets the <see cref="Windows.DocumentManagement.RecentDocumentManager"/>.
		/// </summary>
		/// <value>A <see cref="Windows.DocumentManagement.RecentDocumentManager"/>.</value>
		public RecentDocumentManager RecentDocumentManager { get; }

	}

}
