---
title: "Item Display"
page-title: "Item Display - Breadcrumb Features"
order: 8
---
# Item Display

The [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control allows the look of the represented data to be customized using various properties.

## Customizing the Item Look

The items in the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control use the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).[ItemTemplate](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemTemplate) as their `DataTemplate`. In addtion, the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).[ItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemContainerStyle) can be used to further customize the look of an item.

Typically, these properties are set on the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control for use by all the items displayed. But the associated properties on the [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) control can be used to change the look for its descendents. Therefore, if desired, each item in the trail could be given a custom look.

## Setting Images

The image associated with the selected item is displayed on the left side of the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control. This image is obtained from the [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem).[ImageSource](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem.ImageSource) property.

> [!TIP]
> The [ImageConverter](xref:ActiproSoftware.Windows.Controls.ImageConverter) value converter can be used to create an `Image` element from a `BitmapSource`, allowing you to use the [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem).[ImageSource](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem.ImageSource) property to assign the `MenuItem.Icon` property for use in the context menus.

In addition, [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).[ImageMinWidth](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ImageMinWidth) can be used to set the minimum width of the image area. By default, this property is set to `16.0`.

## Associated Members

The following [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) members can be used to customize the display of items:

| Member | Description |
|-----|-----|
| [ImageMinWidth](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ImageMinWidth) Property | Gets or sets the minimum width of the image displayed on the left side of the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).  The default value is `16.0`. |
| [ItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemContainerStyle) Property | Gets or sets the `Style` that is applied to [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) elements generated for items in the trail.  The default value is `null`. |
| [ItemContainerStyleSelector](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemContainerStyleSelector) Property | Gets or sets custom logic for choosing a `Style` that can be applied to [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) elements generated.  The default value is `null`. |
| [ItemTemplate](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemTemplate) Property | Gets or sets the `DataTemplate` used to display each item in the trail.  The default value is `null`. |
| [ItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ItemTemplateSelector) Property | Gets or sets the custom logic for choosing a template used to display each item in the trail.  The default value is `null`. |

The following [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) members can be used to customize the display of child items:

| Member | Description |
|-----|-----|
| [ImageSource](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem.ImageSource) Property | Gets or sets the image that appears in the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) when the [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) is selected.  The default value is `null`. |
| `ItemContainerStyle` Property | Gets or sets the `Style` that is applied to [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) elements generated for items in the trail.  The default value is `null`. |
| `ItemContainerStyleSelector` Property | Gets or sets custom logic for choosing a `Style` that can be applied to [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) elements generated.  The default value is `null`. |
| `ItemTemplate` Property | Gets or sets the `DataTemplate` used to display each item in the trail.  The default value is `null`. |
| `ItemTemplateSelector` Property | Gets or sets the custom logic for choosing a template used to display each item in the trail.  The default value is `null`. |
