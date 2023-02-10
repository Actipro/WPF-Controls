using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm {

	/// <inheritdoc/>
	internal class DocumentEditorRibbonViewModel : RichTextEditorRibbonViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="RichTextEditorRibbonViewModel(BarManager, RecentDocumentManager)"/>
		public DocumentEditorRibbonViewModel(BarManager barManager, RecentDocumentManager recentDocuments)
			: base(barManager, recentDocuments) { }


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override RibbonBackstageViewModel CreateBackstage(BarManager barManager, RecentDocumentManager recentDocuments) {
			return new RibbonBackstageViewModel() {
				Items = {
					new InfoRibbonBackstageTabViewModel(barManager),
					new NewRibbonBackstageTabViewModel(barManager),
					new OpenRibbonBackstageTabViewModel(barManager, recentDocuments),
					new RibbonBackstageHeaderSeparatorViewModel(),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonSave, "Save", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonSaveAs, "Save As", "A", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonPrint, "Print", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonShare, "Share", "Z", barManager.NotImplementedCommand),
					new ExportRibbonBackstageTabViewModel(barManager),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonClose, "Close", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderSeparatorViewModel(RibbonBackstageHeaderAlignment.Bottom),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonAccount, "Account", "D", barManager.NotImplementedCommand) {
						HeaderAlignment = RibbonBackstageHeaderAlignment.Bottom
					},
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonOptions, "Options", "T", barManager.NotImplementedCommand) {
						HeaderAlignment = RibbonBackstageHeaderAlignment.Bottom
					},
				},
			};
		}

	}
}
