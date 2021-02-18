---
title: "MVVM Features"
page-title: "MVVM Features - Docking & MDI Reference"
order: 10
---
# MVVM Features

The Docking & MDI product has been designed to support the popular MVVM pattern.  The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) control's item container generation features and infrastructure are similar to a standard `ItemsControl`, but geared towards the generation of docking windows.

> [!TIP]
> Many of the concepts below are implemented in the MVVM-related samples in the Sample Browser, so be sure to examine those samples.

## Specifying Document and Tool Items

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) includes several properties and methods that mimic the design of the native `ItemsControl`.  Using these properties and methods, the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) can be bound to a collection of view-models for documents and/or tool windows.

### Document Windows

Document windows can be defined by binding the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemsSource](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemsSource) property to a collection of items, such as view-models.  The items will be wrapped in an instance of [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow), which is the container used to present the item.

A `Style` can be applied to the generated [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) instances by using the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemContainerStyle) or [DocumentItemContainerStyleSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemContainerStyleSelector) properties. Alternatively, an implicit `Style` for the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) type can be defined and placed in the `Application.Resources`.  The `Style` can be used to bind various properties of the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) to the item.

The `DataTemplate` used by the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) instances to present the associated item can be specified using the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemTemplate](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemTemplate) or [DocumentItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemTemplateSelector) properties. Alternatively, an implicit `DataTemplate` for the item's type can be defined and placed in the `Application.Resources`.

### Tool Windows

Tool windows can be defined by binding the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ToolItemsSource](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemsSource) property to a collection of items, such as view-models.  The items will be wrapped in an instance of [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow), which is the container used to present the item.

A `Style` can be applied to the generated [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) instances by using the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ToolItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemContainerStyle) or [ToolItemContainerStyleSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemContainerStyleSelector) properties. Alternatively, an implicit `Style` for the [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) type can be defined and placed in the `Application.Resources`.  The `Style` can be used to bind various properties of the [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) to the item.

The `DataTemplate` used by the [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) instances to present the associated item can be specified using the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ToolItemTemplate](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemTemplate) or [ToolItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemTemplateSelector) properties. Alternatively, an implicit `DataTemplate` for the item's type can be defined and placed in the `Application.Resources`.

### Container Customization

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) includes several virtual methods that can be overridden to customize the containers created based on the bound items. These methods mimic the native `ItemsControl` and includes [ClearContainerForItemOverride](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ClearContainerForItemOverride*), [GetContainerForItemOverride](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.GetContainerForItemOverride*), [IsItemItsOwnContainerOverride](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.IsItemItsOwnContainerOverride*), and [PrepareContainerForItemOverride](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrepareContainerForItemOverride*).  Each of these methods works like it's `ItemsControl` counterpart, with one exception. Each method includes an additional parameter that specifies whether the container is for a document or tool item.

The [PrepareContainerForItemOverride](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrepareContainerForItemOverride*) method override could be used to bind your view model's properties to the containing docking window's related properties.  The [ClearContainerForItemOverride](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ClearContainerForItemOverride*) method override would clear those bindings. You don't need to override these methods if you are setting up your bindings in a `Style` instead that gets used by [DocumentItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemContainerStyle), [ToolItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemContainerStyle), etc.

### Content and Template

As with a standard `ItemsControl` bound to a collection of view model items, each item will be assigned to the `Content` property of the generated container docking window.

These view model items require that a content template be specified to properly render their data.  As mentioned above, the [DocumentItemTemplate](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemTemplate), [DocumentItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemTemplateSelector), [ToolItemTemplate](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemTemplate), and [ToolItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemTemplateSelector) properties can be used to designate the `DataTemplate` to use for each item.

## Styling the Document and Tool Windows

Custom styles can easily be applied to the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) containers created for the bound items.  The [DocumentItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemContainerStyle), [DocumentItemContainerStyleSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemContainerStyleSelector), [ToolItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemContainerStyle), or [ToolItemContainerStyleSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemContainerStyleSelector) properties can be used to apply a custom `Style`. Alternatively, an implicit `Style` for the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) types can be defined.  The `Style` can be used to bind various properties of the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) to the item.

These styles pulled from our MVVM samples in the Sample Browser show how you can bind various view model properties to the docking windows.  The resulting `DocumentWindowStyle` and `ToolWindowStyle` properties could then be set to the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemContainerStyle) and [ToolItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemContainerStyle) properties respectively:

```xaml
xmlns:common="clr-namespace:ActiproSoftware.ProductSamples.DockingSamples.Common"
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...

<common:ToolItemDockSideConverter x:Key="ToolItemDockSideConverter" />
<common:ToolItemStateConverter x:Key="ToolItemStateConverter" />

<Style x:Key="DockingWindowStyle" TargetType="docking:DockingWindow">
    <Setter Property="Description" Value="{Binding Path=Description, Mode=TwoWay}" />
    <Setter Property="ImageSource" Value="{Binding Path=ImageSource, Mode=TwoWay}" />
    <Setter Property="IsActive" Value="{Binding Path=IsActive, Mode=TwoWay}" />
    <Setter Property="IsOpen" Value="{Binding Path=IsOpen, Mode=TwoWay}" />
    <Setter Property="IsSelected" Value="{Binding Path=IsSelected, Mode=TwoWay}" />
    <Setter Property="SerializationId" Value="{Binding Path=SerializationId, Mode=TwoWay}" />
    <Setter Property="Title" Value="{Binding Path=Title, Mode=TwoWay}" />
    <Setter Property="WindowGroupName" Value="{Binding Path=WindowGroupName, Mode=TwoWay}" />
</Style>

<Style x:Key="DocumentWindowStyle" TargetType="docking:DocumentWindow" BasedOn="{StaticResource DockingWindowStyle}">
    <Setter Property="FileName" Value="{Binding Path=FileName, Mode=TwoWay}" />
    <Setter Property="IsReadOnly" Value="{Binding Path=IsReadOnly, Mode=TwoWay}" />
</Style>

<Style x:Key="ToolWindowStyle" TargetType="docking:ToolWindow" BasedOn="{StaticResource DockingWindowStyle}">
    <Setter Property="DefaultDockSide" Value="{Binding Path=DefaultDockSide, Mode=TwoWay, Converter={StaticResource ToolItemDockSideConverter}}" />
    <Setter Property="State" Value="{Binding Path=State, Mode=TwoWay, Converter={StaticResource ToolItemStateConverter}}" />
</Style>
```

## Layout Serialization Support

If you plan on [serializing your layout](layout-features/layout-serialization.md) between application execution sessions, you must assign your docking windows a unique `Name` or [SerializationId](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.SerializationId).  It is recommended that you use the [SerializationId](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.SerializationId) property since unlike the `Name` property, it can be bound easier and allows any character to be used in the value.

## Opening and Positioning the Docking Windows

The [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) containers created for the bound items will initially be closed and must be opened to appear in the visible layout.

Numerous bindable properties are available on [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) for determining the state and position of docking windows, along with whether they are open and active.  For instance, the [State](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.State) property can be set to designate which state the window should open in.  This property must remain `Document` for any [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow).  To open a docking window and add it to the layout, set its [IsOpen](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsOpen) property to `true`.  To open a docking window (if it isn't already open) and ensure it's selected and focused, set its [IsActive](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsActive) property to `true`.  This can be done instead of setting [IsOpen](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsOpen).  Finally, to make sure an already-open docking window's tab is selected, set its [IsSelected](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsSelected) property to `true`.

When a docking window is opened via its [IsOpen](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsOpen) or [IsActive](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsActive) properties, it will open in a default location.  There are numerous properties and events available that allow you to customize this default location prior to opening the docking window.  Some examples are [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow).[DefaultDockSide](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow.DefaultDockSide), [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow).[WindowGroupName](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.WindowGroupName), [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[WindowDefaultLocationRequested](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowDefaultLocationRequested), etc.

If you prefer to open, activate, dock, etc. a docking window in code-behind instead, this can be easily accomplished by adding a handler for the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[WindowRegistered](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowRegistered) event.  This event is fired once per [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) when it is initially registered with the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).

The following code shows an example of a `WindowRegistered` handler:

```csharp
dockSite.WindowRegistered += this.OnDockSiteWindowRegistered;

// ...

/// <summary>
/// Handles the <c>WindowRegistered</c> event of the <c>DockSite</c> control.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The <see cref="DockingWindowEventArgs"/> instance containing the event data.</param>
private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
	DockSite dockSite = sender as DockSite;
	if (dockSite == null)
		return;

	// Ensure the DockingWindow exists and is generated for an item
	DockingWindow dockingWindow = e.Window;
	if (dockingWindow == null || !dockingWindow.IsContainerForItem)
		return;

	// Open the DockingWindow, if it's not already open
	if (!dockingWindow.IsOpen) {
		// ** Open window in desired location here
	}
}
```

## Item Containers

The [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow).[IsContainerForItem](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsContainerForItem) property returns `true` when a docking window is a generated container for an item in the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemsSource](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemsSource) or [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ToolItemsSource](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemsSource) collections.

The [ContainerFromItem](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ContainerFromItem*) method can be used to retrieve the [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) or [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) associated with a given item.  If the type of the item is known, then the [ContainerFromDocumentItem](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ContainerFromDocumentItem*) or [ContainerFromToolItem](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ContainerFromToolItem*) methods can be used instead.

## Item Container Unregistration Triggering Items Source Update

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[CanUpdateItemsSourceOnUnregister](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.CanUpdateItemsSourceOnUnregister) property indicates whether to watch the [WindowUnregistered](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowUnregistered) event for an item container [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) to be unregistered.  If that property is `true`, the dock site will try to automatically remove the related item from the appropriate [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[DocumentItemsSource](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.DocumentItemsSource) or [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ToolItemsSource](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ToolItemsSource) collection.

The functionality will only succeed if the items source is an `IList` that is not read-only or fixed size.

In scenarios where the above conditions are not met, it is up to you to watch the [WindowUnregistered](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowUnregistered) event and remove the related item from its items source.

## Troubleshooting

See the [Troubleshooting](troubleshooting.md) topic for more information, with some specific details on configuring data contexts properly.
