using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Input;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

public class RichTextEditorRibbonWindowViewModel : RibbonWindowViewModel {

	private ICommand? _newBlankDocumentCommand;
	private ICommand? _newDefaultDocumentCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

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

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a view model for a rich text document.
	/// </summary>
	/// <param name="useDefaultContent"><c>true</c> to populate the document with default content; otherwise, <c>false</c> for an empty document.</param>
	private RichTextEditorDocumentViewModel CreateDocumentViewModel(bool useDefaultContent) {
		var document = SampleViewModelFactory.CreateFlowDocument(
			BarManager,
			useDefaultContent
				? "This is an editor application sample that demonstrates a Ribbon within a RibbonWindow."
				: string.Empty
		);

		var viewModel = new RichTextEditorDocumentViewModel(BarManager, document) {
			// Synchronize the documents ItemContainerTemplateSelector with the ribbon
			//   so view models of context menus can be properly displayed
			ItemContainerTemplateSelector = Ribbon.ItemContainerTemplateSelector,
		};

		return viewModel;
	}

	/// <summary>
	/// Processes the selection of a new document.
	/// </summary>
	/// <param name="documentViewModel">The view model of the document to select.</param>
	private void SelectNewDocument(RichTextEditorDocumentViewModel documentViewModel) {
		Ribbon?.Backstage?.Close();
		SelectedDocument = documentViewModel ?? throw new ArgumentNullException(nameof(documentViewModel));
		OnRequestFocusSelectedDocument();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
		return base.GetCommandMappings(barManager)
			.Concat(new Dictionary<CompositeCommand, ICommand>() {
				{ barManager.NewBlankDocumentCommand, NewBlankDocumentCommand },
				{ barManager.NewDefaultDocumentCommand, NewDefaultDocumentCommand },
			});
	}

	/// <summary>
	/// The command to create and select a new, blank document.
	/// </summary>
	public ICommand NewBlankDocumentCommand {
		get => _newBlankDocumentCommand ??= new DelegateCommand<object>(_ =>
			SelectNewDocument(CreateDocumentViewModel(useDefaultContent: false))
		);
	}

	/// <summary>
	/// The command to create and select a new document with default content.
	/// </summary>
	public ICommand NewDefaultDocumentCommand {
		get => _newDefaultDocumentCommand ??= new DelegateCommand<object>(_ => 
			SelectNewDocument(CreateDocumentViewModel(useDefaultContent: true))
		);
	}

	/// <summary>
	/// The command to create and select a open a document.
	/// </summary>
	public void Open(FlowDocument document) {
		if (document is null)
			throw new ArgumentNullException(nameof(document));

		var viewModel = new RichTextEditorDocumentViewModel(BarManager, document) {
			// Synchronize the documents ItemContainerTemplateSelector with the ribbon
			//   so view models of context menus can be properly displayed
			ItemContainerTemplateSelector = Ribbon.ItemContainerTemplateSelector,
		};

		SelectNewDocument(viewModel);
	}

	/// <summary>
	/// The view model of the selected document.
	/// </summary>
	public new RichTextEditorDocumentViewModel? SelectedDocument {
		get => (RichTextEditorDocumentViewModel?)base.SelectedDocument;
		set => base.SelectedDocument = value;
	}

}
