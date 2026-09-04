using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.BarsIntegration;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow {

	private ICommand? _dockWindowCommand;
	private ICommand? _floatWindowCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();

		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the active <see cref="DockingWindow"/>.
	/// </summary>
	private DockingWindow? GetActiveDockingWindow()
		=> dockSite.ActiveWindow ?? dockSite.PrimaryDocument;

	/// <summary>
	/// Occurs when the element is loaded
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLoaded(object sender, RoutedEventArgs e) {
		// Activate the first document if there is no active docking window
		if (dockSite.ActiveWindow is null)
			dockSite.DocumentWindows.FirstOrDefault()?.Activate();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command to dock a docking window.
	/// </summary>
	public ICommand DockWindowCommand {
		get => _dockWindowCommand ??= new DelegateCommand<object>(p => {
			var dockingWindow = (p as DockingWindow) ?? GetActiveDockingWindow();
			switch (dockingWindow) {
				case ToolWindow toolWindow:
					toolWindow.Dock();
					break;
				case DocumentWindow documentWindow:
					documentWindow.MoveToMdi(dockSite.PrimaryDockHost);
					documentWindow.Activate();
					break;
			}
		});
	}

	/// <summary>
	/// The command to float a docking window.
	/// </summary>
	public ICommand FloatWindowCommand {
		get => _floatWindowCommand ??= new DelegateCommand<object>(p => {
			var dockingWindow = (p as DockingWindow) ?? GetActiveDockingWindow();
			dockingWindow?.Float();
		});
	}

}
