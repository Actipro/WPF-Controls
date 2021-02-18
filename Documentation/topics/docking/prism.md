---
title: "Prism Integration"
page-title: "Prism Integration - Docking & MDI Reference"
order: 11
---
# Prism Integration

Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in WPF.  It is now hosted [open source on GitHub](https://github.com/PrismLibrary/).

The Docking & MDI for WPF product can easily be integrated into an application that leverages Prism using the product's powerful [MVVM features](mvvm-features.md) and several additional open source integration classes we provide.

## Requirements

The open source Prism integration functionality is located in the 'PrismIntegration' sample that ships with the Actipro WPF controls.  This sample project contains our code that integrates Docking & MDI with Prism and also shows a small real-world usage example.

These are the requirements for the sample:

- Targets .NET 4.5 (or later)
- References Prism 6.1 via NuGet

> [!NOTE]
> While a number of the external web site Prism documentation links below refer to an older Prism version, the same general info should still apply.

Prior to the 2016.1 version of our WPF controls, portions of the Prism integration code were included in a separate pre-compiled assembly, whose code was also made available as open source.  In the 2016.1 version, we decided to include the full source of the Prism integration features directly in our Prism sample so that it's easier to work with and customize as needed.

## Reusable Open Source Code

As mentioned above, the Prism integration functionality is contained in the 'PrismIntegration' sample.  The open source functionality is distributed among several folders and code files as described in the list below.  These code files can generally be copied verbatim to your own Prism applications and further customized as needed.

- **\\ViewModels** - Folder containing all view-models.
  
  - **\\Docking** - Folder containing docking-related view-models.
    
    - **ClassViewToolItemViewModel, etc.** - View-model implementation classes that configure view-models for various container docking windows and inherit base clases in the 'Core' folder.  A view-model class should generally be created for each kind of docking window in your application.
    
    - **\\Core** - Folder containing code that should be used as base classes for your document and tool window view-models.  This code has no references at all to the Actipro Docking & MDI assembly.
      
      - **DockingItemViewModelBase** - The base class for all docking item view-models.  While the default implementation covers many appearance and layout features, you can easily add support for other [capabilities](docking-window-features/docking-window-capabilities.md) like whether windows can close, etc.
      
      - **DocumentItemViewModel** - Implements a document item view-model.  Instances of this class can be used directly as view-models, or derived view-model classes can be made from it.
      
      - **ToolItemViewModel** - Implements a tool item view-model.  Instances of this class can be used directly as view-models, or derived view-model classes can be made from it.
      
      - **ToolItemDefaultLocation** - Specifies a tool item view's default location.
      
      - **ToolItemDockSide** - Specifies a dock side for a tool item's view.  This is analogous to the [Side](xref:ActiproSoftware.Windows.Controls.Side) enumeration.
      
      - **ToolItemState** - Specifies the state of a tool item's view.  This is analogous to the [DockingWindowState](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindowState) enumeration.

- **\\Views** - Folder containing all views.
  
  - **ImplicitTemplates** - A XAML file that is included in the `Application.Resources` and implicitly applies view `DataTemplate`s to container docking windows based on the view-model type.
  
  - **\\Docking** - Folder containing docking-related views.
    
    - **ClassViewToolItemView, etc.** - View implementations for each of the related view-models.  A view class should be created for each view-model in your application and it should be wired up via the 'ImplicitTemplates.xaml' file.

- **\\Regions** - Folder containing code that ties the view-models to generated containers (docking windows) and manages Prism integration.
  
  - **DockSiteRegionAdapter** - Implements a [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) region adapter.
  
  - **DockSiteRegionViewKinds** - An enumeration indicating the kinds of view kinds (document, tool, or both) that are permitted.  Both is the default.
  
  - **DockingWindowBindingExtensions** - A static class with methods that can bind/unbind the core view-model properties to related container properties.  This class needs to be updated any time additional view-model properties are added that should be bound to a container.
  
  - **ToolItemDockSideConverter** - A value converter used by `DockingWindowBindingExtensions`.
  
  - **ToolItemStateConverter** - A value converter used by `DockingWindowBindingExtensions`.
  
  - **\\Behaviors** - Folder containing Prism behaviors.
    
    - **DockSiteRegionBehavior** - Implements a [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) region behavior, which provides the main integration logic with Prism.

> [!NOTE]
> When copying the classes in the folders above, be sure to update the namespaces to match your application's naming scheme.

## Register Region Adapter

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) can be configured as a named region, and thus allow views to be added to it.  To support this, the `DockSiteRegionAdapter` class, a custom region adapter, was created.  In order to use a `DockSite` as a region, the region adapter must be registered.

The [Create a Custom Region Adapter (external)](http://msdn.microsoft.com/en-us/library/dd458901.aspx) MSDN article describes how a custom region adapter can be registered.  Effectively, the `DockSite` type must be mapped to an instance of `DockSiteRegionAdapter`.

This code shows an example of mapping the `DockSite` type to an instance of `DockSiteRegionAdapter` that does not use inversion of control/dependency injection.  This code must be included in the application's bootstrapper:

```csharp
protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {
    RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
    mappings.RegisterMapping(typeof(DockSite), new DockSiteRegionAdapter());
    return mappings;
}
```

If using Unity, or some other IoC/DI product, then the registration code above may differ slightly.

## Define DockSite Region

After registering the region adapter, instances of [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) can be setup as regions.  In Prism, a region is simply a placeholder whichs allows a developer to specify where views will be displayed in the application's user interface.  Therefore, one or more objects (such as view-models or UIElements) can be added to the `DockSite` via Prism's view discovery or injection.  These objects will then be presented as either document (default) or tool windows.

The [Add a Region (external)](http://msdn.microsoft.com/en-us/library/ff921164(PandP.20).aspx) MSDN article describes how to define a control as a region.  Effecitvely, the `RegionManager.RegionNameProperty` attached property must be set on the `DockSite` using a constant/known string name.

This code shows how to specify a `DockSite`, which will use a tabbed-MDI interface for documents, as a region:

```xaml
xmlns:prism="http://prismlibrary.com/"
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...
<docking:DockSite x:Name="dockSite" prism:RegionManager.RegionName="MyDockSiteRegion">
    <docking:Workspace>
        <docking:TabbedMdiHost />
    </docking:Workspace>
</docking:DockSite>
```

## Add Views To DockSite

After defining the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) as a region, one or more views can be added to it.  By default, these views will become document windows. In order to create tool windows, see the "Working with View-Models" section below.

The [Show a View in a Region Using View Discovery UI Composition (external)](http://msdn.microsoft.com/en-us/library/ff921103(v=PandP.20).aspx) and [Show a View in a Region Using View Injection UI Composition (external)](http://msdn.microsoft.com/en-us/library/ff921076(v=PandP.20).aspx) MSDN articles describe how to add a view to a region.  When using view discovery, the view object's type (whether it be a view-model or a WPF visual) is mapped the the constant/known region name used previously.  When using view injection, instances of the view are added directly to the region, which is obtained using the constant/known region name used previously.

Views are added by one or more "modules". This topic will not cover creating and registering modules, but more information can be found at:

- [Create a Module](http://msdn.microsoft.com/en-us/library/ff921089(v=PandP.20).aspx)
- [Populate the Module Catalog from Code](http://msdn.microsoft.com/en-us/library/ff921077(v=PandP.20).aspx)
- [Populate the Module Catalog from XAML](http://msdn.microsoft.com/en-us/library/ff921151(v=PandP.20).aspx)
- [Populate the Module Catalog from a Configuration File or a Directory in WPF](http://msdn.microsoft.com/en-us/library/ff921095(v=PandP.20).aspx)

This code shows an example of a module that uses view discovery to add a view to the `DockSite` region:

```csharp
public class MyModule : IModule {

    private readonly IRegionViewRegistry regionViewRegistry;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainModule"/> class.
    /// </summary>
    /// <param name="registry">The registry.</param>
    public MainModule(IRegionViewRegistry registry) {
        this.regionViewRegistry = registry;
    }

    /// <summary>
    /// Initializes the module with the associated registry.
    /// </summary>
    public void Initialize() {
        this.regionViewRegistry.RegisterViewWithRegion("DockSiteRegion", typeof(MyViewModel));
    }
}
```

This code shows an example of a module that uses view injection to add a view to the `DockSite` region:

```csharp
public class MyModule : IModule {

    private readonly IRegionManager regionManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainModule"/> class.
    /// </summary>
    /// <param name="manager">The manager.</param>
    public MainModule(IRegionManager manager) {
        this.regionManager = manager;
    }

    /// <summary>
    /// Initializes the module with the associated registry.
    /// </summary>
    public void Initialize() {
        IRegion mainRegion = this.regionManager.Regions[RegionNames.MainRegion];
        mainRegion.Add(new MyViewModel());
    }
}
```

> [!TIP]
> The `DockSite` region name was hard-coded above as "DockSiteRegion", but a better design would be to define this as a static property.  The static property can then be referenced from the module and also used when setting the `RegionManager.RegionNameProperty` attached property, thus ensuring that any changes to the region name will be propagated to all places it is used.

## Working with View-Models

### Selecting the Appropriate View Model

The `DockSiteRegionAdapter` is set up to generate a container [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) for any view-models that inherit `DocumentItemViewModel`.  Likewise, it will generate a container [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) for any view-models that inherit `ToolItemViewModel`.  Thus it is important for you to select an appropriate base view-model depending on which type of container docking window you wish to generate.

If you aren't using our base classes for your view-models, then the `DockSiteRegionAdapter.GetDockingWindowItemKind` method would need to be updated with custom logic for selecting the kind of container to generate.

> [!NOTE]
> All view-model items will assume to be documents unless one of the above steps is taken.

### Adding More Configuration Properties

The core view-model classes included in the sample include the most common properties that would need to be bound to generated container docking windows.  In scenarios where other appearance or behavior properties need to be added (such as for whether a docking window can close), follow these steps:

- Add a new property to the appropriate view-model class (e.g. `DockingItemViewModelBase` for all docking windows, `DocumentItemViewModel` for just document windows, or `ToolItemViewModel` for just tool windows).

- Update the `DockingWindowBindingExtensions` class to create a binding from the view-model property to the related container docking window property in its `PrepareContainerBindings` method, and clear the binding in its `ClearContainerBindings` method.

### Linking Views with View-Models

The `DockingWindowBindingExtensions` class has methods the set and clear two-way bindings from view-model properties to related container docking window properties.  It is important to update this class whenever new general properties are added to any of our core docking view-model classes.

In our sample, the `ImplicitTemplates.xaml` file contains implicit `DataTemplate`s that are applied to the container docking windows based on the view-model type in use.  This sort of file needs to be included in your `Application.Resources`.

Another way to select templates is by using the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemTemplateSelector) and [ToolItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemTemplateSelector) properties, and assigning an appropriate template selector class to them.

### Views and Container Open States

The core view-models we provide include properties like `IsOpen`, `IsSelected`, and `IsActive` that bind to the related [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) properties.  These properties may be checked at any time to learn the current state of a view-model's generated container.  Open means the view is in the layout.  Selected means the view is a selected tab in the layout when open.  Active means the view is currently focused in the layout.

While a docking window is open in the layout, its view-model should be present in the containing region's `Views` collection.  Code in the Prism integration attempts to keep this collection and the `ActiveViews` collection in sync with the layout.
