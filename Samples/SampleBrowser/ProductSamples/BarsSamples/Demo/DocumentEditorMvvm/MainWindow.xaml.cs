using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Input;
using System;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm {

	/// <summary>
	/// The main window for the MVVM-based document editor demo.
	/// </summary>
	public partial class MainWindow {

		private ICommand toggleFlowDirectionCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			var barManager = new BarManager();

			// Configure recent documents with a default collection of document references for demo purposes
			var recentDocuments = new RecentDocumentManager();
			DocumentReferenceGenerator.BindRecentDocumentManager(recentDocuments);

			// Create the view model for the ribbon used by this demo
			var ribbonViewModel = new DocumentEditorRibbonViewModel(barManager, recentDocuments);

			// Create the view model for ribbon-based window to edit rich text documents
			var windowViewModel = new RichTextEditorRibbonWindowViewModel(barManager, ribbonViewModel);

			// Make sure composite commands are registered
			windowViewModel.RegisterCommands();

			// Initialize the window with a new editor document
			windowViewModel.NewDefaultDocumentCommand?.Execute(parameter: null);

			// Register commands handled by this window
			barManager.FlowDirectionCommand.RegisterCommand(this.ToggleFlowDirectionCommand);

			this.ViewModel = windowViewModel;

			this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (Action)(() => {
				documentView.Focus();
			}));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command that will toggle the window's FlowDirection.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		private ICommand ToggleFlowDirectionCommand {
			get {
				if (toggleFlowDirectionCommand is null) {
					toggleFlowDirectionCommand = new DelegateCommand<object>(param => {
						this.FlowDirection = (this.FlowDirection == System.Windows.FlowDirection.LeftToRight)
							? System.Windows.FlowDirection.RightToLeft
							: System.Windows.FlowDirection.LeftToRight;
					});
				}
				return toggleFlowDirectionCommand;
			}
		}

		/// <summary>
		/// Gets or sets the view model for the window through the DataContext.
		/// </summary>
		/// <value>A <see cref="WindowViewModel"/>.</value>
		private WindowViewModel ViewModel {
			get => this.DataContext as WindowViewModel;
			set => this.DataContext = value;
		}

	}

}
