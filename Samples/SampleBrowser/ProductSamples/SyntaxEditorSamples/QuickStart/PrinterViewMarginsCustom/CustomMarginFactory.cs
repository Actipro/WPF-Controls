using System;
using ActiproSoftware.Products.SyntaxEditor;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrinterViewMarginsCustom {

	/// <summary>
	/// A custom factory implementation that creates <see cref="IPrinterViewMargin"/> objects for use within an <see cref="IPrinterView"/>.
	/// </summary>
	public class CustomMarginFactory : IPrinterViewMarginFactory {

		/// <summary>
		/// Creates the collection of <see cref="IPrinterViewMargin"/> objects to place within a <see cref="IPrinterView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IPrinterView"/> that will host the margins.</param>
		/// <returns>A <see cref="IPrinterViewMarginCollection"/> containing the <see cref="IPrinterViewMargin"/> objects that were created.</returns>
		public IPrinterViewMarginCollection CreateMargins(IPrinterView view) {
			IPrinterViewMarginCollection margins = new PrinterViewMarginCollection();
			margins.Add(new CustomMargin(view));
			return margins;
		}
		
	}
}
