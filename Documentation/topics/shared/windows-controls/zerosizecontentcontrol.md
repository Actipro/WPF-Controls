---
title: "ZeroSizeContentControl"
page-title: "ZeroSizeContentControl - Shared Library Controls"
order: 120
---
# ZeroSizeContentControl

The [ZeroSizeContentControl](xref:@ActiproUIRoot.Controls.ZeroSizeContentControl) is a regular `ContentControl` that can return a zero width or height during its measuring pass.  This is useful in scenarios where the content should take up some space, but you don't want the WPF layout routines to consider the space that it will take up during its measuring pass.

The [ZeroSizeContentControl](xref:@ActiproUIRoot.Controls.ZeroSizeContentControl) class has these important members:

| Member | Description |
|-----|-----|
| [HasHeight](xref:@ActiproUIRoot.Controls.ZeroSizeContentControl.HasHeight) Property | Gets or sets whether the content's height should be used in the measuring pass.  The default value is `true`. |
| [HasWidth](xref:@ActiproUIRoot.Controls.ZeroSizeContentControl.HasWidth) Property | Gets or sets whether the content's width should be used in the measuring pass.  The default value is `true`. |
| [IdealSize](xref:@ActiproUIRoot.Controls.ZeroSizeContentControl.IdealSize) Property | Gets the ideal `Size` of the control, since the `DesiredSize` will not represent the true desired size. |

Use the `HorizontalContentAlignment` and `VerticalContentAlignment` properties to determine how content is aligned when arranged.
