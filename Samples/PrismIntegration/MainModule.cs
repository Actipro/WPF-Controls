using System;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using Unity.Lifetime;

namespace ActiproSoftware.Windows.PrismIntegration {
	
	/// <summary>
	/// Represents the main Prism module for the sample.
	/// </summary>
	public class MainModule : IModule {

		private readonly IUnityContainer container;
		private readonly IRegionViewRegistry regionViewRegistry;
		private readonly IRegionManager regionManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainModule"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="registry">The registry.</param>
		/// <param name="regionManager">The region manager.</param>
		public MainModule(IUnityContainer container, IRegionViewRegistry registry, IRegionManager regionManager) {
			this.container = container;
			this.regionViewRegistry = registry;
			this.regionManager = regionManager;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the module with the associated registry.
		/// </summary>
		/// <param name="containerProvider">The container provider.</param>
		public void OnInitialized(IContainerProvider containerProvider) {
			// Register view models that last the app lifetime
			container.RegisterType<ViewModels.SolutionExplorerToolItemViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<ViewModels.ClassViewToolItemViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<ViewModels.ToolboxToolItemViewModel>(new ContainerControlledLifetimeManager());

			// Create the WelcomeDocument.rtf view model
			var welcomeDocumentItemViewModel = new WelcomeDocumentItemViewModel();

			// Create the ReadOnlyDocument.txt view model
			var textDocumentItemViewModel = new TextDocumentItemViewModel();
			textDocumentItemViewModel.FileName = @"C:\Users\Actipro\My Documents\ReadOnlyDocument.txt";
			textDocumentItemViewModel.IsReadOnly = true;
			textDocumentItemViewModel.Text = "This plain text document is in a read-only state.";
			textDocumentItemViewModel.Title = "ReadOnlyDocument.txt";

			// Use view injection for documents
			var mainRegion = regionManager.Regions[ShellViewModel.MainRegionName];
			mainRegion.Add(welcomeDocumentItemViewModel);
			mainRegion.Add(textDocumentItemViewModel);
			
			// Use view discovery for tools
			regionViewRegistry.RegisterViewWithRegion(ShellViewModel.MainRegionName, typeof(SolutionExplorerToolItemViewModel));
			regionViewRegistry.RegisterViewWithRegion(ShellViewModel.MainRegionName, typeof(ClassViewToolItemViewModel));
			regionViewRegistry.RegisterViewWithRegion(ShellViewModel.MainRegionName, typeof(ToolboxToolItemViewModel));

			// Activate the welcome document
			mainRegion.Activate(welcomeDocumentItemViewModel);
		}
		
		/// <summary>
		/// Registers types.
		/// </summary>
		/// <param name="containerRegistry">The container registry.</param>
		public void RegisterTypes(IContainerRegistry containerRegistry) {
		}

	}

}
