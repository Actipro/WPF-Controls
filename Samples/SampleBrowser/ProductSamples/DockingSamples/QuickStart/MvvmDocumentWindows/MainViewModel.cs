using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmDocumentWindows;

/// <summary>
/// Represents the main view-model.
/// </summary>
public class MainViewModel : ObservableObjectBase {

	private int _documentIndex = 1;
	private readonly DeferrableObservableCollection<DocumentItemViewModel> _documentItems = [];

	private DelegateCommand<object>? _activateNextDocumentCommand;
	private DelegateCommand<object>? _closeActiveDocumentCommand;
	private DelegateCommand<object>? _createNewImageDocumentCommand;
	private DelegateCommand<object>? _createNewTextDocumentCommand;
	private DelegateCommand<object>? _selectFirstDocumentCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainViewModel() {
		CreateNewTextDocument(activate: false);
		CreateNewTextDocument(activate: false);
		CreateNewImageDocument(activate: false);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The activate next document command.
	/// </summary>
	public ICommand ActivateNextDocumentCommand {
		get => _activateNextDocumentCommand ??= new DelegateCommand<object>(_ => {
			if (_documentItems.Count > 0) {
				var index = 0;
				var activeDocumentItem = _documentItems.FirstOrDefault(d => d.IsActive);
				if (activeDocumentItem is not null)
					index = _documentItems.IndexOf(activeDocumentItem) + 1;
				if (index >= _documentItems.Count)
					index = 0;

				_documentItems[index].IsActive = true;
			}
		});
	}

	/// <summary>
	/// The close active document command.
	/// </summary>
	/// <value>The close active document command.</value>
	public ICommand CloseActiveDocumentCommand {
		get => _closeActiveDocumentCommand ??= new DelegateCommand<object>(_ => {
			var activeDocumentItem = _documentItems.FirstOrDefault(d => d.IsActive);
			if (activeDocumentItem is not null)
				_documentItems.Remove(activeDocumentItem);
		});
	}

	/// <summary>
	/// Creates a new image document.
	/// </summary>
	/// <param name="activate">Whether to activate the document.</param>
	public void CreateNewImageDocument(bool activate) {
		var viewModel = new ImageDocumentItemViewModel {
			// NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			SerializationId = string.Format("Document{0}.png", _documentIndex),
			FileName = string.Format("Document{0}.png", _documentIndex++)
		};
		viewModel.Title = viewModel.FileName;
		viewModel.Uri = new Uri("/Images/Icons/Save32.png", UriKind.Relative);

		_documentItems.Add(viewModel);

		if (activate)
			viewModel.IsActive = true;
		else
			viewModel.IsOpen = true;
	}

	/// <summary>
	/// Creates a new text document.
	/// </summary>
	/// <param name="activate">Whether to activate the document.</param>
	public void CreateNewTextDocument(bool activate) {
		var viewModel = new TextDocumentItemViewModel {
			// NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			SerializationId = string.Format("Document{0}.txt", _documentIndex),
			FileName = string.Format("Document{0}.txt", _documentIndex++)
		};
		viewModel.Title = viewModel.FileName;
		viewModel.Text = string.Format("Dynamically created at {0}.", DateTime.Now);

		_documentItems.Add(viewModel);

		if (activate)
			viewModel.IsActive = true;
		else
			viewModel.IsOpen = true;
	}

	/// <summary>
	/// The create new image document command.
	/// </summary>
	/// <value>The create new image document command.</value>
	public ICommand CreateNewImageDocumentCommand
		=> _createNewImageDocumentCommand ??= new DelegateCommand<object>(_ => CreateNewImageDocument(activate: true));

	/// <summary>
	/// The create new text document command.
	/// </summary>
	public ICommand CreateNewTextDocumentCommand
		=> _createNewTextDocumentCommand ??= new DelegateCommand<object>(_ => CreateNewTextDocument(activate: true));

	/// <summary>
	/// The document items associated with this view-model.
	/// </summary>
	public IList<DocumentItemViewModel> DocumentItems
		=> _documentItems;

	/// <summary>
	/// The select first document command.
	/// </summary>
	/// <value>The select first document command.</value>
	public ICommand SelectFirstDocumentCommand {
		get => _selectFirstDocumentCommand ??= new DelegateCommand<object>(_ => {
			var documentItem = _documentItems.FirstOrDefault();
			if (documentItem is not null)
				documentItem.IsSelected = true;
		});
	}

}
