using System.Windows;
using Prism.Regions;

namespace ActiproSoftware.Windows.PrismIntegration.Views {

	/// <summary>
	/// Represents the main window view.
	/// </summary>
	public partial class Shell : Window {

		private readonly IRegionManager regionManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="Shell"/> class.
		/// </summary>
		/// <param name="regionManager">The region manager.</param>
		public Shell(IRegionManager regionManager) {
			this.regionManager = regionManager;

			InitializeComponent();

			this.Loaded += this.OnLoaded;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to the event.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			if (regionManager != null) {
				// NOTE: If you need to access regions within docking windows explicitly defined in XAML (this sample doesn't have any), 
				//   then you must manually set the region manager like this once the dock site has loaded,
				//   where 'myToolWindow' references a ToolWindow with x:Name="myToolWindow" that would have been in XAML

				// RegionManager.SetRegionManager(myToolWindow, regionManager);
				// RegionManager.UpdateRegions();
			}
		}

	}
}
