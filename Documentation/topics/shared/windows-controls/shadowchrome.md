---
title: "ShadowChrome"
page-title: "ShadowChrome - Shared Library Controls"
order: 34
---
# ShadowChrome

The [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome) control draws a modern shadow around its content, using native WPF rendering or shader effects.

![Screenshot](../images/shadowchrome.png)

*A ShadowChrome providing a soft shadow around a contact card*

## Render Mode

The mode by which to render the shadow can be set via the [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome).[RenderMode](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.RenderMode) property.  The `Default` mode uses fast WPF rendering and is intended for child elements that have a rectangular shape.

Shader effects can alternatively be used to render the shadow.  Shader effects provide a very realistic shadow effect for contained content, but they are more resource intensive and should be used sparingly throughout UI.

If the child element isn't a rectangular shape, use the `ShaderEffectsRequired` mode.  This mode ensures that shader effects are used if graphics hardware acceleration is available.  If graphics hardware acceleration is not available, no shadow is rendered.

A third `ShaderEffectsPreferred` mode option tries to use shader effects if graphics hardware acceleration is available.  Otherwise, the `Default` mode with fast WPF rendering is used.

## Elevation

The [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome).[Elevation](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.Elevation) property determines how large of a shadow to render.  The elevation value can be from `0` (no shadow) to `24`, and defaults to `0`.  A larger-than-zero value must be specified for a shadow to be rendered.

## Enabled

The [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome).[IsShadowEnabled](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.IsShadowEnabled) property can be set to `false` to prevent the shadow from rendering.  When set to `false`, the elevation value is effectively coerced to `0`.

## Direction

The shadow is cast out towards a certain direction, specified by the [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome).[Direction](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.Direction) property.  The direction value `0` is towards the right and increases counterclockwise.  The default direction value is `270`, which is downward.  Another commonly-used direction value is `315`, which is down-and-right.

> [!NOTE]
> When using fast WPF rendering and for the downward `270` direction only, special algorithms kick in to seamlessly render the shadow in a slightly more natural way that nearly matches how the more complex shader effects would render the shadow.

## Opacity

The [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome).[ShadowOpacity](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.ShadowOpacity) property determines how dark to make the shadow.  The default value is `0.3`, meaning 30% opacity.  Darker themes may wish to use a more opaque opacity, such as `1.0` (100%).

## Resulting Thickness

The [ShadowChrome](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome).[ShadowThickness](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.ShadowThickness) property returns the margin around the shadow chrome's child element that is required to fully render the shadow in its current configuration.

Note that when using the shadow chrome in a `Popup`, the `ShadowChrome.Margin` should be set to the [ShadowThickness](xref:@ActiproUIRoot.Controls.Primitives.ShadowChrome.ShadowThickness) property value to ensure the popup is large enough to render the semi-transparent shadow.
