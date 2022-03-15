---
title: "Context Menus"
page-title: "Context Menus - Breadcrumb Features"
order: 4
---
# Context Menus

The [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control displays context menus when the [overflow button](overflow.md) is clicked and when the [popup button](item-selection.md) on the individual items is clicked. These context menus are automatically populated by the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control, but can be customized during runtime.

## Customizing the Menu

You can completely customize the default `ContextMenu`s or even supply an alternate menu by handling the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb).[OverflowButtonClick](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.OverflowButtonClick) or [BreadcrumbItem](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem).[NavigateButtonClick](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.NavigateButtonClick) events. The `Item` property of the event arguments is the `ContextMenu` that will be displayed.  Add/update/remove menu items as necessary there.

In addition, there are numereous properties listed below, which allow the look of the context menus to be customized as well.

## Menu and Text Alignment

The menus displayed by the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control are offset from their associated drop-down button for better alignment. This offset varies based on the theme loaded and defaults to values appropriate for `ContextMenu`s using the same theme. Therefore, the Aero theme for the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control assumes that the menus will also be using the Aero theme and adjusts the offset accordingly.

The menu offset can be customized as needed by setting [MenuHorizontalOffset](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuHorizontalOffset) or [MenuVerticalOffset](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuVerticalOffset), either on the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control or the individual [BreadcrumbItem](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem) controls. Typically, only the horizontal offset should be adjusted.

When defining a custom `DataTemplate` for [MenuItemTemplate](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemTemplate) or [MenuItemExpandedTemplate](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemExpandedTemplate), a 2-pixel margin should typically be included on the left side. This 2-pixel margin allows the text in the menu to line up with the text of the next `BreadcrumbItem`, which is more visually appealing. When using the default `DataTemplate`s, the 2-pixel margin is already defined.

## Associated Members

The following [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) members are associated with the context menus:

| Member | Description |
|-----|-----|
| [MenuHorizontalOffset](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuHorizontalOffset) Property | Gets or sets the horizontal distance between the target origin and the content menu alignment point. |
| [MenuItemContainerExpandedStyle](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemContainerExpandedStyle) Property | Gets or sets the `Style` that is applied to the `MenuItem` elements generated for expanded items in context menus. |
| [MenuItemContainerExpandedStyleSelector](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemContainerExpandedStyleSelector) Property | Gets or sets custom logic for choosing a `Style` that can be applied to `MenuItem` elements generated for expanded items in context menus. |
| [MenuItemContainerStyle](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemContainerStyle) Property | Gets or sets the `Style` that is applied to the `MenuItem` elements generated for items in context menus. |
| [MenuItemContainerStyleSelector](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemContainerStyleSelector) Property | Gets or sets custom logic for choosing a `Style` that can be applied to `MenuItem` elements generated for items in context menus. |
| [MenuItemExpandedTemplate](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemExpandedTemplate) Property | Gets or sets the `DataTemplate` used to display expanded items in a `MenuItem`. |
| [MenuItemExpandedTemplateSelector](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemExpandedTemplateSelector) Property | Gets or sets the custom logic for choosing a template used to display expanded items in a `MenuItem`. |
| [MenuItemTemplate](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemTemplate) Property | Gets or sets the `DataTemplate` used to display items in a `MenuItem`. |
| [MenuItemTemplateSelector](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuItemTemplateSelector) Property | Gets or sets the custom logic for choosing a template used to display items in a `MenuItem`. |
| [MenuVerticalOffset](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.MenuVerticalOffset) Property | Gets or sets the vertical distance between the target origin and the content menu alignment point. |
| [OverflowButtonClick](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.OverflowButtonClick) Event | Occurs when the overflow button is clicked, allowing for customization of the `ContextMenu` it will display. |

The following [BreadcrumbItem](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem) members are associated with the context menus:

| Member | Description |
|-----|-----|
| [MenuHorizontalOffset](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuHorizontalOffset) Property | Gets or sets the horizontal distance between the target origin and the content menu alignment point. |
| [MenuItemContainerExpandedStyle](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemContainerExpandedStyle) Property | Gets or sets the `Style` that is applied to the `MenuItem` elements generated for expanded items in context menus. |
| [MenuItemContainerExpandedStyleSelector](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemContainerExpandedStyleSelector) Property | Gets or sets custom logic for choosing a `Style` that can be applied to `MenuItem` elements generated for expanded items in context menus. |
| [MenuItemContainerStyle](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemContainerStyle) Property | Gets or sets the `Style` that is applied to the `MenuItem` elements generated for items in context menus. |
| [MenuItemContainerStyleSelector](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemContainerStyleSelector) Property | Gets or sets custom logic for choosing a `Style` that can be applied to `MenuItem` elements generated for items in context menus. |
| [MenuItemExpandedTemplate](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemExpandedTemplate) Property | Gets or sets the `DataTemplate` used to display expanded items in a `MenuItem`. |
| [MenuItemExpandedTemplateSelector](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemExpandedTemplateSelector) Property | Gets or sets the custom logic for choosing a template used to display expanded items in a `MenuItem`. |
| [MenuItemTemplate](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemTemplate) Property | Gets or sets the `DataTemplate` used to display items in a `MenuItem`. |
| [MenuItemTemplateSelector](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuItemTemplateSelector) Property | Gets or sets the custom logic for choosing a template used to display items in a `MenuItem`. |
| [MenuVerticalOffset](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.MenuVerticalOffset) Property | Gets or sets the vertical distance between the target origin and the content menu alignment point. |
| [NavigateButtonClick](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.NavigateButtonClick) Event | Occurs when the navigate button is clicked, allowing for customization of the `ContextMenu` it will display. |
