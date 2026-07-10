using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm;

/// <summary>
/// Represents a view model for a the "Export" tab control within a ribbon backstage.
/// </summary>
/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
public class ExportRibbonBackstageTabViewModel(BarManager barManager) : RibbonBackstageTabViewModel(BarControlKeys.BackstageTabExport, "Export") {

	private readonly BarManager _barManager = barManager ?? throw new ArgumentNullException(nameof(barManager));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
	public ICommand NotImplementedCommand
		=> _barManager.NotImplementedCommand;

}
