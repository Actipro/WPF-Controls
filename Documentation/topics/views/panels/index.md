---
title: "Overview"
page-title: "Views Panels"
order: 1
---
# Overview

There are several panels provided, with some that replicate and enhance the layout logic provided by native WPF panels and others that provide unique layout logic. This topic briefly describes the panels.

## PanelBase

All animated panels derive from [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase).  This includes most of the animation framework, leaving the derived classes to simply implement their layout logic.

See the [PanelBase](panelbase.md) topic for more information.

## AnimatedCanvas

Allows child elements to be precisely positioned using coordinates that are relative to the panel's bounds.  This panel can be used as a drop-in replacement for the native `Canvas` panel.

See the [AnimatedCanvas](animatedcanvas.md) topic for more information.

## AnimatedDockPanel

Allows child elements to be positioned either horizontally or vertically, relative to each other.  This panel can be used as a drop-in replacement for the native WPF `DockPanel` panel.

See the [AnimatedDockPanel](animateddockpanel.md) topic for more information.

## AnimatedStackPanel

Allows child elements to be positioned in sequential order, either horizontally or vertically.  This panel can be used as a drop-in replacement for the native `StackPanel` panel.

See the [AnimatedStackPanel](animatedstackpanel.md) topic for more information.

## AnimatedWrapPanel

Allows child elements to be positioned in sequential order, breaking content to the next row or column at the edge of the panels bounds.  Subsequent ordering happens sequentially from top to bottom or from right to left, depending on the orientation.  This panel can be used as a drop-in replacement for the native WPF `WrapPanel` panel.

See the [AnimatedWrapPanel](animatedwrappanel.md) topic for more information.

## SwitchPanel

Delegates the positioning of child elements to one or more child panels, which allows the layout logic to be dynamically changed during runtime.

See the [SwitchPanel](switchpanel.md) topic for more information.

## FanPanel

This panel allows child elements to be positioned in sequential order along the z-axis, while keeping a focal item centered in the view.

See the [FanPanel](fanpanel.md) topic for more information.

## MultiColumnPanel

This panel can render child elements in multiple columns, collapsing columns down as available space decreases.  It's a space-efficient and visually-appealing way to render lists of items, or to break paragraphs of text up.

See the [MultiColumnPanel](multicolumnpanel.md) topic for more information.

## ZapPanel

Allows child elements to be positioned in sequential order, either horizontally or vertically, while keeping a focal item centered in the view.  Includes support for wrapping items around, to produce a circular effect.

See the [ZapPanel](zappanel.md) topic for more information.

## Building a Custom Panel

Custom panels can easily be created that support animations and can be used in [SwitchPanel](xref:@ActiproUIRoot.Controls.Views.SwitchPanel).

See the [Building a Custom Panel](building-a-custom-panel.md) topic for more information.
