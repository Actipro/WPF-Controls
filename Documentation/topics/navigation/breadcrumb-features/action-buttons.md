---
title: "Action Buttons"
page-title: "Action Buttons - Breadcrumb Features"
order: 2
---
# Action Buttons

The [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control supports custom action buttons, which are presented on the right side of the control. These buttons can be used to perform any operation, however for proper usage their purpose should be related to the nodes represented in the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb).

![Screenshot](../images/breadcrumb-action-buttons-aero-normal-color.gif)

*The Breadcrumb control using the Aero theme with various action buttons*

## Adding an Action Button

Any `Button`, or `Button` derived class, can be added to the [ActionButtons](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ActionButtons) collection property using XAML (as shown below) or by calling the `Add` or `Insert` methods directly. Because the embedded [progress bar](progressbar.md) can be shown under the action buttons, it is recommended that the [PopupButton](xref:ActiproSoftware.Windows.Controls.PopupButton) control (with its transparency mode enabled) be used for action buttons.

This sample code shows how to add an action button:

```xaml
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...						
<navigation:Breadcrumb ... >
    <navigation:Breadcrumb.ActionButtons>
        <!-- Define action buttons here -->
        <shared:PopupButton Content="New PopupButton"
                            DisplayMode="ButtonOnly"
                            IsRounded="false"
                            IsTransparencyModeEnabled="true" />
    </navigation:Breadcrumb.ActionButtons>
</navigation:Breadcrumb>
```

## Supported Commands

The [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) supports the following `NavigationCommands`:

| Routed Command | Action Performed |
|-----|-----|
| `BrowseHome` | Selects the root item. |
| `FirstPage` | Selects the root item. |
| `GoToPage` | Selects the item whose path is indicated by the command parameter. |
| `LastPage` | Selects the last visible item below the currently selected item. This command is only valid when 1 or more tail items are visible. |
| `NextPage` | Selects the next visible item below the currently selected item. This command is only valid when 1 or more tail items are visible. |
| `PreviousPage` | Selects the parent item of the currently selected item. |

## Associated Members

The following [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) members are associated with action buttons:

| Member | Description |
|-----|-----|
| [ActionButtons](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.ActionButtons) Property | Gets the action buttons shown on the right side of the breadcrumb control. |
