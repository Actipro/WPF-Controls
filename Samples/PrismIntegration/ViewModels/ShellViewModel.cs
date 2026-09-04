using Prism.Commands;
using Prism.Regions;
using Unity;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Represents the shell view-model.
/// </summary>
public class ShellViewModel : ObservableObjectBase {

	private readonly IUnityContainer _container;
	private int _documentIndex;
	private readonly IRegionManager _regionManager;

	private ICommand? _activateViewCommand;
	private ICommand? _newTextDocumentCommand;

	public const string MainRegionName = "MainRegion";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="container">The container.</param>
	/// <param name="regionManager">The region manager.</param>
	public ShellViewModel(IUnityContainer container, IRegionManager regionManager) {
		_container = container ?? throw new ArgumentNullException(nameof(container));
		_regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Activates the specified view model type.
	/// </summary>
	/// <param name="viewModelType">The view model type.</param>
	private void ActivateView(Type? viewModelType) {
		if (viewModelType is null)
			return;

		// Get the view model
		var viewModel = _container.Resolve(viewModelType);

		// Use view injection
		var mainRegion = _regionManager.Regions[MainRegionName];
		if (!mainRegion.Views.Contains(viewModel))
			mainRegion.Add(viewModel);

		// Activate the view model
		mainRegion.Activate(viewModel);
	}

	/// <summary>
	/// Creates a new text document.
	/// </summary>
	private void CreateNewTextDocument() {
		// Create the view model
		var viewModel = new TextDocumentItemViewModel {
			Title = string.Format("Document{0}.txt", ++_documentIndex)
		};

		// Use view injection
		var mainRegion = _regionManager.Regions[MainRegionName];
		mainRegion.Add(viewModel);

		// Activate the view model
		mainRegion.Activate(viewModel);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the command.
	/// </summary>
	/// <value>The command.</value>
	public ICommand ActivateViewCommand {
		get {
			return _activateViewCommand ??= new DelegateCommand<object>(
				param => ActivateView(param as Type)
			);
		}
	}

	/// <summary>
	/// Gets the command.
	/// </summary>
	/// <value>The command.</value>
	public ICommand NewTextDocumentCommand {
		get {
			return _newTextDocumentCommand ??= new DelegateCommand<object>(
				_ => CreateNewTextDocument()
			);
		}
	}

}
