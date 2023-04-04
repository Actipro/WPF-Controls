using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Provides factory methods for creating view models used by samples.
	/// </summary>
	public static class SampleViewModelFactory {

		/// <summary>
		/// Creates the default <see cref="RichTextEditorRibbonWindowViewModel"/> used by various samples and
		/// pre-populated with common controls used for working with rich text documents.
		/// </summary>
		/// <returns>A new <see cref="RichTextEditorRibbonWindowViewModel"/>.</returns>
		public static RichTextEditorRibbonWindowViewModel CreateDefaultRichTextEditorRibbonWindowViewModel() {
			var barManager = new BarManager();

			// Configure recent documents with a default collection of document references for demo purposes
			var recentDocuments = new RecentDocumentManager();
			DocumentReferenceGenerator.BindRecentDocumentManager(recentDocuments);

			// Create the view model for ribbon-based window to edit rich text documents
			var windowViewModel = new RichTextEditorRibbonWindowViewModel(barManager, recentDocuments);

			// Make sure composite commands are registered
			windowViewModel.RegisterCommands();

			// Initialize the window with a new editor document
			windowViewModel.NewDefaultDocumentCommand?.Execute(parameter: null);

			return windowViewModel;
		}

		/// <summary>
		/// Creates a new <see cref="FlowDocument"/>, typically used for editing or displaying rich text.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> which might define a <see cref="TextStyleBarGalleryItemViewModel"/> to use as the default style.</param>
		/// <param name="initialText">Optionally defines the initial text of the document.</param>
		/// <returns>A new <see cref="FlowDocument"/>.</returns>
		public static FlowDocument CreateFlowDocument(BarManager barManager, string initialText) {
			var paragraph = string.IsNullOrEmpty(initialText)
				? new Paragraph()
				: new Paragraph(new Run(initialText));

			// Lookup the "Normal" text style and apply settings to the new document
			var defaultStyle = (barManager?.ControlViewModels[BarControlKeys.QuickStylesGallery] as BarGalleryViewModel)?
				.Items.OfType<TextStyleBarGalleryItemViewModel>()
				.FirstOrDefault();
			if (defaultStyle != null) {
				if (!string.IsNullOrEmpty(defaultStyle.FontFamilyName))
					paragraph.FontFamily = new FontFamily(defaultStyle.FontFamilyName);
				if (!double.IsNaN(defaultStyle.FontSize))
					paragraph.FontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(defaultStyle.FontSize);
			}

			var document = new FlowDocument();
			document.Blocks.Add(paragraph);

			return document;
		}

	}

}
