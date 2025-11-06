using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "Export" tab control within a ribbon backstage.
	/// </summary>
	public class ExportRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ExportRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public ExportRibbonBackstageTabViewModel(BarManager barManager)
			: base(BarControlKeys.BackstageTabExport, "Export") {

			this.barManager = barManager;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
		public ICommand NotImplementedCommand => barManager?.NotImplementedCommand;

	}

}
