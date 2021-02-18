---
title: "Customize Button"
page-title: "Customize Button - NavigationBar Features"
order: 4
---
# Customize Button

A built-in `ContextMenu` is displayed when the NavigationBar's customize button is clicked.

The menu has options for showing more or less pane buttons, showing the [Options Window](options-window.md), showing/hiding individual panes, and selecting any panes whose header buttons don't fit in the main NavigationBar UI.

## Customizing the Menu

You can completely customize the default `ContextMenu` or even supply an alternate menu by handling the [NavigationBar](xref:ActiproSoftware.Windows.Controls.Navigation.NavigationBar).[CustomizeButtonClick](xref:ActiproSoftware.Windows.Controls.Navigation.NavigationBar.CustomizeButtonClick) event.  The `Item` property of the event arguments is the `ContextMenu` that will be displayed.  Add/update/remove menu items as necessary there.

## Hiding the Customize Button

Set the [NavigationBar](xref:ActiproSoftware.Windows.Controls.Navigation.NavigationBar).[IsCustomizationEnabled](xref:ActiproSoftware.Windows.Controls.Navigation.NavigationBar.IsCustomizationEnabled) property to `false` to hide the customize button.
