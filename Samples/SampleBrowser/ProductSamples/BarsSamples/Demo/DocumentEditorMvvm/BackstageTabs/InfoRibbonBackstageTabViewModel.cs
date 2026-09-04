using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm;

/// <summary>
/// Represents a view model for a the "Info" tab control within a ribbon backstage.
/// </summary>
public class InfoRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

	private readonly BarManager _barManager;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
	public InfoRibbonBackstageTabViewModel(BarManager barManager) : base(BarControlKeys.BackstageTabInfo, "Info") {
		_barManager = barManager ?? throw new ArgumentNullException(nameof(barManager));

		LargeImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabInfo, BarImageSize.Large);
		SmallImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabInfo, BarImageSize.Small);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
	public ICommand NotImplementedCommand
		=> _barManager.NotImplementedCommand;

}
