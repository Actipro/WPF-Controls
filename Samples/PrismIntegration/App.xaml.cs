using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Windows;
using Unity;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.PrismIntegration.Views;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using ActiproSoftware.Windows.PrismIntegration.Regions;
using Prism.Modularity;

namespace ActiproSoftware.Windows.PrismIntegration {

	/// <summary>
	/// Represents the application.
	/// </summary>
	public partial class App : PrismApplication {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Configures the module catalog.
		/// </summary>
		/// <param name="moduleCatalog">The <see cref="IModuleCatalog"/> instance containing all the modules.</param>
		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
			base.ConfigureModuleCatalog(moduleCatalog);
			moduleCatalog.AddModule(typeof(MainModule));
		}

		/// <summary>
		/// Configures the default region adapter mappings to use in the application, 
		/// in order to adapt UI controls defined in XAML to use a region and register it automatically. 
		/// </summary>
		/// <param name="regionAdapterMappings">The <see cref="RegionAdapterMappings"/> instance containing all the mappings.</param>
		protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings) {
			base.ConfigureRegionAdapterMappings(regionAdapterMappings);

			regionAdapterMappings.RegisterMapping(typeof(DockSite), this.Container.Resolve<DockSiteRegionAdapter>());
		}

		/// <summary>
		/// Creates the shell or main window of the application.
		/// </summary>
		/// <returns>The shell of the application.</returns>
		protected override Window CreateShell() {
            var shellView = this.Container.Resolve<Shell>();
			shellView.DataContext = this.Container.Resolve<ShellViewModel>();
			return shellView;
		}

		/// <summary>
		/// Registers types.
		/// </summary>
		/// <param name="containerRegistry">The container registry.</param>
		protected override void RegisterTypes(IContainerRegistry containerRegistry) {}

	}

}
