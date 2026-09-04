using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.PrismIntegration.Regions;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using ActiproSoftware.Windows.PrismIntegration.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Unity;

namespace ActiproSoftware.Windows.PrismIntegration;

/// <summary>
/// Represents the application.
/// </summary>
public partial class App : PrismApplication {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
		base.ConfigureModuleCatalog(moduleCatalog);

		moduleCatalog.AddModule(typeof(MainModule));
	}

	/// <inheritdoc/>
	protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings) {
		base.ConfigureRegionAdapterMappings(regionAdapterMappings);

		regionAdapterMappings.RegisterMapping(typeof(DockSite), Container.Resolve<DockSiteRegionAdapter>());
	}

	/// <inheritdoc/>
	protected override Window CreateShell() {
		var shellView = Container.Resolve<Shell>();
		shellView.DataContext = Container.Resolve<ShellViewModel>();
		return shellView;
	}

	/// <inheritdoc/>
	protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

}
