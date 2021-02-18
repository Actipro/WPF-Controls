---
title: "CustomDrawElement"
page-title: "CustomDrawElement - Shared Library Controls"
order: 10
---
# CustomDrawElement

The [CustomDrawElement](xref:ActiproSoftware.Windows.Controls.CustomDrawElement) class inherits `FrameworkElement` and provides a [CustomDraw](xref:ActiproSoftware.Windows.Controls.CustomDrawElement.CustomDraw) event.

By handling this event, you can draw the content of the element as if you had created a subclass of `FrameworkElement` and overrode the `OnRender` method.

This element is useful for placement in item templates where you may wish to custom draw items in an event handler.

This element is recommended for use in the [Ribbon](../../ribbon/index.md) product when wanting to programmatically draw [RibbonGallery](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.RibbonGallery) or [PopupGallery](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.PopupGallery) items.
