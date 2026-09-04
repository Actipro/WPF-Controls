using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm;

/// <summary>
/// Represents a view model for a the "Open" tab control within a ribbon backstage.
/// </summary>
public class OpenRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

	private readonly BarManager _barManager;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
	/// <param name="recentDocumentManager">The <see cref="Windows.DocumentManagement.RecentDocumentManager"/>.</param>
	public OpenRibbonBackstageTabViewModel(BarManager barManager, RecentDocumentManager recentDocumentManager)
		: base(BarControlKeys.BackstageTabOpen, "Open") {

		_barManager = barManager ?? throw new ArgumentNullException(nameof(barManager));
		RecentDocumentManager = recentDocumentManager ?? throw new ArgumentNullException(nameof(recentDocumentManager));

		LargeImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabOpen, BarImageSize.Large);
		SmallImageSource = barManager.ImageProvider.GetImageSource(BarControlKeys.BackstageTabOpen, BarImageSize.Small);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
	public ICommand NotImplementedCommand
		=> _barManager.NotImplementedCommand;

	/// <summary>
	/// The <see cref="Windows.DocumentManagement.RecentDocumentManager"/>.
	/// </summary>
	public RecentDocumentManager RecentDocumentManager { get; }

}
