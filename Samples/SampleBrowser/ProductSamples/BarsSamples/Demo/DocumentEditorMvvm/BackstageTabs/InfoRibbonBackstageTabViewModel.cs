using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "Info" tab control within a ribbon backstage.
	/// </summary>
	public class InfoRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="InfoRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public InfoRibbonBackstageTabViewModel(BarManager barManager)
			: base(BarControlKeys.BackstageTabInfo, "Info") {

			this.barManager = barManager;

			this.LargeImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabInfo, BarImageSize.Large);
			this.SmallImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabInfo, BarImageSize.Small);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
		public ICommand NotImplementedCommand => barManager?.NotImplementedCommand;

	}

}
