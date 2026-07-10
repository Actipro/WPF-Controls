using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrinterViewMarginsLocations;

/// <summary>
/// A custom factory implementation that creates <see cref="IPrinterViewMargin"/> objects for use within an <see cref="IPrinterView"/>.
/// </summary>
public class CustomMarginFactory : IPrinterViewMarginFactory {

	/// <inheritdoc cref="IPrinterViewMarginFactory.CreateMargins" />
	public IPrinterViewMarginCollection CreateMargins(IPrinterView view) {
		return new PrinterViewMarginCollection {
			// Add four margins
			new CustomMargin(view, PrinterViewMarginPlacement.Left),
			new CustomMargin(view, PrinterViewMarginPlacement.Top),
			new CustomMargin(view, PrinterViewMarginPlacement.Right),
			new CustomMargin(view, PrinterViewMarginPlacement.Bottom)
		};
	}

}
