using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "New" tab control within a ribbon backstage.
	/// </summary>
	public class NewRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="NewRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public NewRibbonBackstageTabViewModel(BarManager barManager)
			: base(BarControlKeys.BackstageTabNew, "New") {

			this.barManager = barManager;
			
			this.LargeImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabNew, BarImageSize.Large);
			this.SmallImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabNew, BarImageSize.Small);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NewBlankDocumentCommand"/>
		public ICommand NewBlankDocumentCommand => barManager?.NewBlankDocumentCommand;

		/// <inheritdoc cref="BarManager.NewDefaultDocumentCommand"/>
		public ICommand NewDefaultDocumentCommand => barManager?.NewDefaultDocumentCommand;

	}

}
