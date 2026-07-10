using Prism.Regions;

namespace ActiproSoftware.Windows.PrismIntegration.Views;

/// <summary>
/// Represents the main window view.
/// </summary>
public partial class Shell : Window {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="regionManager">The region manager.</param>
	public Shell(IRegionManager? regionManager) {
		RegionManager = regionManager;

		InitializeComponent();

		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the control is loaded.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLoaded(object sender, RoutedEventArgs e) {
		if (RegionManager is not null) {
			// NOTE: If you need to access regions within docking windows explicitly defined in XAML (this sample doesn't have any), 
			//   then you must manually set the region manager like this once the dock site has loaded,
			//   where 'myToolWindow' references a ToolWindow with x:Name="myToolWindow" that would have been in XAML

			// RegionManager.SetRegionManager(myToolWindow, regionManager);
			// RegionManager.UpdateRegions();
		}
	}

	private IRegionManager? RegionManager { get; }

}
