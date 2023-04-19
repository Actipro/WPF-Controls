using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	public class RichTextEditorRibbonWindowViewModel : RibbonWindowViewModel {

		private ICommand newBlankDocumentCommand;
		private ICommand newDefaultDocumentCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class and automatically create a <see cref="RichTextEditorRibbonViewModel"/>
		/// to associate with the view model.
		/// </summary>
		/// <inheritdoc cref="WindowViewModel(BarManager)" />
		/// <param name="recentDocuments">The <see cref="RecentDocumentManager"/> to be associated with the ribbon view model.</param>
		public RichTextEditorRibbonWindowViewModel(BarManager barManager, RecentDocumentManager recentDocuments)
			: this(barManager, new RichTextEditorRibbonViewModel(barManager, recentDocuments)) { }

		/// <inheritdoc cref="RibbonWindowViewModel(BarManager, RibbonViewModel)"/>
		public RichTextEditorRibbonWindowViewModel(BarManager barManager, RichTextEditorRibbonViewModel ribbonViewModel)
			: base(barManager, ribbonViewModel) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a view model for a rich text document.
		/// </summary>
		/// <param name="useDefaultContent"><c>true</c> to populate the document with default content; otherwise, <c>false</c> for an empty document.</param>
		/// <returns>A new <see cref="RichTextEditorDocumentViewModel"/>.</returns>
		private RichTextEditorDocumentViewModel CreateDocumentViewModel(bool useDefaultContent) {
			var document = SampleViewModelFactory.CreateFlowDocument(BarManager,
				useDefaultContent
					? "This is an editor application sample that demonstrates a Ribbon within a RibbonWindow."
					: string.Empty);

			var viewModel = new RichTextEditorDocumentViewModel(this.BarManager, document) {
				// Synchronize the documents ItemContainerTemplateSelector with the ribbon
				// so view models of context menus can be properly displayed
				ItemContainerTemplateSelector = Ribbon.ItemContainerTemplateSelector,
			};

			return viewModel;
		}

		/// <summary>
		/// Processes the selection of a new document.
		/// </summary>
		/// <param name="documentViewModel">The view model of the document to select.</param>
		private void SelectNewDocument(RichTextEditorDocumentViewModel documentViewModel) {
			if (documentViewModel is null)
				throw new ArgumentNullException(nameof(documentViewModel));

			this.Ribbon?.Backstage?.Close();
			this.SelectedDocument = documentViewModel;
			this.OnRequestFocusSelectedDocument();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
			return base.GetCommandMappings(barManager).Concat(new Dictionary<CompositeCommand, ICommand>() {
				{ barManager.NewBlankDocumentCommand, this.NewBlankDocumentCommand },
				{ barManager.NewDefaultDocumentCommand, this.NewDefaultDocumentCommand },
			});
		}

		/// <summary>
		/// Gets the command to create and select a new, blank document.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NewBlankDocumentCommand {
			get {
				if (newBlankDocumentCommand is null) {
					newBlankDocumentCommand = new DelegateCommand<object>(
						p => this.SelectNewDocument(this.CreateDocumentViewModel(useDefaultContent: false))
					);
				}
				return newBlankDocumentCommand;
			}
		}

		/// <summary>
		/// Gets the command to create and select a new document with default content.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NewDefaultDocumentCommand {
			get {
				if (newDefaultDocumentCommand is null) {
					newDefaultDocumentCommand = new DelegateCommand<object>(
						p => this.SelectNewDocument(this.CreateDocumentViewModel(useDefaultContent: true))
					);
				}
				return newDefaultDocumentCommand;
			}
		}
		
		/// <summary>
		/// Gets the command to create and select a open a document.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public void Open(FlowDocument document) {
			if (document == null)
				throw new ArgumentNullException(nameof(document));

			var viewModel = new RichTextEditorDocumentViewModel(this.BarManager, document) {
				// Synchronize the documents ItemContainerTemplateSelector with the ribbon
				// so view models of context menus can be properly displayed
				ItemContainerTemplateSelector = Ribbon.ItemContainerTemplateSelector,
			};

			this.SelectNewDocument(viewModel);
		}

		/// <summary>
		/// Gets or sets the view model of the selected document.
		/// </summary>
		/// <value>A <see cref="RichTextEditorDocumentViewModel"/>.</value>
		public new RichTextEditorDocumentViewModel SelectedDocument {
			get => (RichTextEditorDocumentViewModel)base.SelectedDocument;
			set => base.SelectedDocument = value;
		}

	}

}
