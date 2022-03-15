---
title: "ReflectionContentControl"
page-title: "ReflectionContentControl - Shared Library Controls"
order: 31
---
# ReflectionContentControl

The [ReflectionContentControl](xref:@ActiproUIRoot.Controls.ReflectionContentControl) is a regular `ContentControl` however it also renders a reflection effect of the content below the actual content.  This reflection effect has become quite popular in recent user interfaces.

![Screenshot](../images/reflectioncontentcontrol.png)

*The ReflectionContentControl control providing reflection effects on the Actipro logo*

You have total control over the size, distance and visual characteristics of the reflection.  You can even skew the reflection to an angle.

> [!TIP]
> Animate the [ReflectionMargin](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionMargin) property along with the `RenderTransform` of the [ReflectionContentControl](xref:@ActiproUIRoot.Controls.ReflectionContentControl) to create a bounce effect.  This is demoed in the sample project.

A reflection effect like what is provided in this control can be implemented from scratch in XAML however it takes many lines of code to do so.  Instead, just wrap your content with a [ReflectionContentControl](xref:@ActiproUIRoot.Controls.ReflectionContentControl) and you're done.

The [ReflectionContentControl](xref:@ActiproUIRoot.Controls.ReflectionContentControl) class has these important members:

| Member | Description |
|-----|-----|
| [ReflectionEndOffset](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionEndOffset) Property | Gets or sets the offset at which the end opacity (least opaque) is located in the reflection.  The default value is `0.9`. |
| [ReflectionEndOpacity](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionEndOpacity) Property | Gets or sets the opacity of the reflection at its ending (least opaque) point.  The default value is `0.0`, meaning transparent. |
| [ReflectionHeight](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionHeight) Property | Gets or sets the height of the reflection.  The default value is `NaN`, meaning that the height will be the same height as the content. |
| [ReflectionMargin](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionMargin) Property | Gets or sets the margin distance between the content and the reflection.  The default value is `0`. |
| [ReflectionSkewAngleX](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionSkewAngleX) Property | Gets or sets the skew angle of the reflection in the x-plane.  The default value is `0`, meaning no skew is applied. |
| [ReflectionStartOpacity](xref:@ActiproUIRoot.Controls.ReflectionContentControl.ReflectionStartOpacity) Property | Gets or sets the opacity of the reflection at its starting (most opaque) point.  The default value is `0.4`, meaning `40%` opaque. |
