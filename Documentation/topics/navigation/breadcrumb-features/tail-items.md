---
title: "Tail Items"
page-title: "Tail Items - Breadcrumb Features"
order: 12
---
# Tail Items

By default, the last item displayed is the selected item. However, the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control can be configured to display any number of tail items.

Tail items are items that were part of the last selected branch, meaning they were either selected or contained the selection, and are below the currently selected item. In the image below, the *Actipro Software* item was selected and then the selection was changed to the *Desktop* item. In this scenario, the selection moved up the current branch. By retaining the tail items in the current branch, the user can quickly navigate back down.

If the selection is changed to a different branch, then the tail items below the selected item will be reset.

![Screenshot](../images/breadcrumb-tail-items-aero-normal-color.gif)

*The Breadcrumb control using the Aero theme with several tails items*

## Setting Tail Item Count

The number of tail items is controlled by the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).[MaxTailItemCount](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.MaxTailItemCount) property. This number counts down from the selected item.

For example, if [MaxTailItemCount](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.MaxTailItemCount) is set to `1` then only one tail item will ever be displayed.

## Customizing Tail Items

By default, tail items are display with an `Opacity` of `0.5`, or 50%, but this look can be fully customized using various properties.

The [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).[TailItemOpacity](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.TailItemOpacity) property, can be used to quickly customize the opacity and more complex customization can be accomplish using a `Style`.

This sample code shows how more complex looks can be achieved by using the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).[ItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemContainerStyle) property:

```xaml
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"
...
<Style x:Key="BreadcrumbItemStyleProgressiveFade70"
       TargetType="{x:Type navigation:BreadcrumbItem}">
    ...
    <Style.Triggers>
        <!-- This trigger will progressively dim the tail items -->
        <Trigger Property="IsTailItem"
                 Value="true">
            <Setter Property="Opacity"
                    Value="0.7" />
        </Trigger>
    </Style.Triggers>
</Style>
...
<navigation:Breadcrumb ItemContainerStyle="{StaticResource BreadcrumbItemStyleProgressiveFade70}"
                       MaxTailItemCount="10"
                       ... />
```

## Associated Members

The following [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) members are associated with tail items:

| Member | Description |
|-----|-----|
| [MaxTailItemCount](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.MaxTailItemCount) Property | Gets or sets the number of items that will be automatically minimized, starting from root item.  The default is `1`. |
| [TailItemOpacity](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.TailItemOpacity) Property | Gets or sets the opacity of the tail items.  The default is `0.5`. |

The following [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) members are associated with tail items:

| Member | Description |
|-----|-----|
| [IsTailItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem.IsTailItem) Property | Gets a value indicating whether the item is a tail item. |
| [TailIndex](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem.TailIndex) Property | Gets the position of the tail item below the selected item. If the item is selected or contains the selection, then this index will be -1. |
