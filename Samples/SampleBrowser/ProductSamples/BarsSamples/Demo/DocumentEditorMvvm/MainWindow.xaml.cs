using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Input;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demo.DocumentEditorMvvm;

/// <summary>
/// The main window for the MVVM-based document editor demo.
/// </summary>
public partial class MainWindow {

	private ICommand? _toggleFlowDirectionCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
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
		var document = Application.LoadComponent(new Uri("/ProductSamples/BarsSamples/Demo/DocumentEditorMvvm/FeaturesDocument.xaml", UriKind.Relative)) as FlowDocument;
		if (document is not null)
			windowViewModel.Open(document);

		// Register commands handled by this window
		barManager.FlowDirectionCommand.RegisterCommand(ToggleFlowDirectionCommand);

		ViewModel = windowViewModel;

		Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, () => {
			// Focus the document
			documentView.Focus();
		});

		Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, () => {
			// Focusing the document above might scroll vertically a bit, reset the scroll
			scrollViewer.ScrollToTop();
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command that will toggle the window's FlowDirection.
	/// </summary>
	/// <value>An <see cref="ICommand"/>.</value>
	private ICommand ToggleFlowDirectionCommand {
		get => _toggleFlowDirectionCommand ??= new DelegateCommand<object>(_ => {
			FlowDirection = (FlowDirection == FlowDirection.LeftToRight)
				? FlowDirection.RightToLeft
				: FlowDirection.LeftToRight;
		});
	}

	/// <summary>
	/// The view model for the window through the DataContext.
	/// </summary>
	private WindowViewModel? ViewModel {
		get => DataContext as WindowViewModel;
		set => DataContext = value;
	}

}
