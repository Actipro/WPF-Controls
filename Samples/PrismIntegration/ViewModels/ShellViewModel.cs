using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;
using Unity;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents the shell view-model.
	/// </summary>
	public class ShellViewModel : ObservableObjectBase {

		private IUnityContainer container;
		private int documentIndex;
		private IRegionManager regionManager;

		public const string MainRegionName = "MainRegion";
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ShellViewModel"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="regionManager">The region manager.</param>
		public ShellViewModel(IUnityContainer container, IRegionManager regionManager) {
			this.container = container;
			this.regionManager = regionManager;

			this.CreateCommands();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Activates the specified view model type.
		/// </summary>
		/// <param name="viewModelType">The view model type.</param>
		private void ActivateView(Type viewModelType) {
			if (viewModelType == null)
				return;

			// Get the view model
			var viewModel = container.Resolve(viewModelType);

			// Use view injection
			var mainRegion = regionManager.Regions[ShellViewModel.MainRegionName];
			if (!mainRegion.Views.Contains(viewModel))
				mainRegion.Add(viewModel);

			// Activate the view model
			mainRegion.Activate(viewModel);
		}

		/// <summary>
		/// Creates the command properties.
		/// </summary>
		private void CreateCommands() {
			this.ActivateViewCommand = new DelegateCommand<object>(
				param => {
					this.ActivateView(param as Type);
				});

			this.NewTextDocumentCommand = new DelegateCommand<object>(
				param => {
					this.CreateNewTextDocument();
				});
		}

		/// <summary>
		/// Creates a new text document.
		/// </summary>
		private void CreateNewTextDocument() {
			// Create the view model
			var viewModel = new TextDocumentItemViewModel();
			viewModel.Title = string.Format("Document{0}.txt", ++documentIndex);

			// Use view injection
			var mainRegion = regionManager.Regions[ShellViewModel.MainRegionName];
			mainRegion.Add(viewModel);

			// Activate the view model
			mainRegion.Activate(viewModel);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Gets the command.
		/// </summary>
		/// <value>The command.</value>
		public ICommand ActivateViewCommand { get; private set; }
		
		/// <summary>
		/// Gets the command.
		/// </summary>
		/// <value>The command.</value>
		public ICommand NewTextDocumentCommand { get; private set; }
			
	}

}
