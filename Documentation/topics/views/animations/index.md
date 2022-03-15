---
title: "Overview"
page-title: "Views Animations"
order: 1
---
# Overview

The animation of the child elements of a [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase) is delegated to an instance of [IArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.IArrangeAnimation). The `PanelBase`.[ArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeAnimation) property determines the `IArrangeAnimation` used. By default, this property is initialized to an instance of [ArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation).

## IArrangeAnimation

The [IArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.IArrangeAnimation) interface defines the base requirements for an object that can animate the arrangement of a PanelBase's children. The interface defines a single method, [GetStoryboard](xref:@ActiproUIRoot.Controls.Views.IArrangeAnimation.GetStoryboard*), which is used to get a `Storyboard` used to animate one or more properties.

The [ArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation), described below, provides a default implementation of the `IArrangeAnimation`.

## ArrangeAnimation

Represents a default implementation of `IArrangeAnimation` that allows the animation to be easily configured and/or customized.

See the [ArrangeAnimation](arrange-animation.md) topic for more information.

> [!NOTE]
> `ArrangeAnimation` is a dependency object and uses dependency properties, so it's properties can participate in bindings.
