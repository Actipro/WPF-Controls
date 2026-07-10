using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using Unity.Lifetime;

namespace ActiproSoftware.Windows.PrismIntegration;

/// <summary>
/// Represents the main Prism module for the sample.
/// </summary>
public class MainModule : IModule {

	private readonly IUnityContainer _container;
	private readonly IRegionViewRegistry _regionViewRegistry;
	private readonly IRegionManager _regionManager;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="container">The container.</param>
	/// <param name="registry">The registry.</param>
	/// <param name="regionManager">The region manager.</param>
	public MainModule(IUnityContainer container, IRegionViewRegistry registry, IRegionManager regionManager) {
		_container = container ?? throw new ArgumentNullException(nameof(container));
		_regionViewRegistry = registry ?? throw new ArgumentNullException(nameof(registry));
		_regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IModule.OnInitialized"/>
	public void OnInitialized(IContainerProvider containerProvider) {
		// Register view models that last the app lifetime
		_container.RegisterType<SolutionExplorerToolItemViewModel>(new ContainerControlledLifetimeManager());
		_container.RegisterType<ClassViewToolItemViewModel>(new ContainerControlledLifetimeManager());
		_container.RegisterType<ToolboxToolItemViewModel>(new ContainerControlledLifetimeManager());

		// Create the WelcomeDocument.rtf view model
		var welcomeDocumentItemViewModel = new WelcomeDocumentItemViewModel();

		// Create the ReadOnlyDocument.txt view model
		var textDocumentItemViewModel = new TextDocumentItemViewModel {
			FileName = @"C:\Users\Actipro\My Documents\ReadOnlyDocument.txt",
			IsReadOnly = true,
			Text = "This plain text document is in a read-only state.",
			Title = "ReadOnlyDocument.txt"
		};

		// Use view injection for documents
		var mainRegion = _regionManager.Regions[ShellViewModel.MainRegionName];
		mainRegion.Add(welcomeDocumentItemViewModel);
		mainRegion.Add(textDocumentItemViewModel);

		// Use view discovery for tools
		_regionViewRegistry.RegisterViewWithRegion(ShellViewModel.MainRegionName, typeof(SolutionExplorerToolItemViewModel));
		_regionViewRegistry.RegisterViewWithRegion(ShellViewModel.MainRegionName, typeof(ClassViewToolItemViewModel));
		_regionViewRegistry.RegisterViewWithRegion(ShellViewModel.MainRegionName, typeof(ToolboxToolItemViewModel));

		// Activate the welcome document
		mainRegion.Activate(welcomeDocumentItemViewModel);
	}

	/// <inheritdoc cref="IModule.RegisterTypes"/>
	public void RegisterTypes(IContainerRegistry containerRegistry) { /* no-op */ }

}
