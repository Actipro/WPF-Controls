using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm;

/// <summary>
/// Represents a view model for a the "New" tab control within a ribbon backstage.
/// </summary>
public class NewRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

	private readonly BarManager _barManager;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
	public NewRibbonBackstageTabViewModel(BarManager barManager) : base(BarControlKeys.BackstageTabNew, "New") {
		_barManager = barManager ?? throw new ArgumentNullException(nameof(barManager));

		LargeImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabNew, BarImageSize.Large);
		SmallImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabNew, BarImageSize.Small);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarManager.NewBlankDocumentCommand"/>
	public ICommand NewBlankDocumentCommand
		=> _barManager.NewBlankDocumentCommand;

	/// <inheritdoc cref="BarManager.NewDefaultDocumentCommand"/>
	public ICommand NewDefaultDocumentCommand
		=> _barManager.NewDefaultDocumentCommand;

}
