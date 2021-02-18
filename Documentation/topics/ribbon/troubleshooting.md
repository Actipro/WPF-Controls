---
title: "Troubleshooting"
page-title: "Troubleshooting - Ribbon Reference"
order: 10
---
# Troubleshooting

This topic contains troubleshooting data specific to the Ribbon product.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as other WPF Studio products, please see the more general [TroubleShooting](../troubleshooting.md) topic.

## FileNotFoundException or DirectoryNotFoundException When Specifying Bitmap Image Sources

If you use relative paths for your bitmap image sources, you might encounter a `FileNotFoundException` or `DirectoryNotFoundException` in some cases.  This exception can occur behind the scenes where the image is being reloaded outside of its original XAML context and thus can lose its contextual information for determining a relative path.  Using an absolute path via standard WPF pack syntax allows the image to be loaded properly in any scenario.

Many of the Ribbon controls like the buttons use the [DynamicImage](../shared/windows-controls/dynamicimage.md) control in their templates, which is a Shared Library control that allows for disabled buttons to render their images in grayscale.  Please see the [DynamicImage](../shared/windows-controls/dynamicimage.md) topic for more detailed information on this exception and exactly how to resolve it.

## RibbonWindow Border Bottom is Clipped in VS 2012

For some reason that we haven't been able to determine yet, RibbonWindow's bottom border may be clipped 3-4 pixels at the bottom, but only when the Visual Studio 2012 debugger is attached or when the application is targeting .NET 4.5.  If VS 2010's debugger is attached or if a .NET 4.0 application is run directly from explorer, RibbonWindow continues to render everything correctly.

We are going to be working on reimplementing RibbonWindow's code that allows it to render WPF content in the window's non-client area and hope to solve the issue in this particular scenario soon.

## Ribbon Shows in Designer but Disappears at Run-Time

This scenario could happen if you accidentally set a `Width` or `Height` on the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) control or one of its containers that causes the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) to be smaller than the [CollapseThresholdSize](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.CollapseThresholdSize).  When the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) becomes smaller than [CollapseThresholdSize](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.CollapseThresholdSize) at run-time and [IsCollapsible](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.IsCollapsible) is set to `true`, the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) will hide.  This feature meets one of the [ribbon requirements](ribbonui-guidelines.md).

This scenario is most likely to occur when you are adjusting control layouts in the designer and adjust the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) or a parent container's size accidentally.

To resolve this issue, simply remove any explicit `Width` or `Height` setting on the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) or a parent container that is causing the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon)'s size be be smaller than [CollapseThresholdSize](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.CollapseThresholdSize).  Alternatively, change [IsCollapsible](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.IsCollapsible) to `false`.

To help identify when this scenario is occurring, we have added a message that overlays the [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) in the Visual Studio designer and warns you that the current size will prevent the ribbon from displaying at run-time (since it will be collapsed).  When you see that message, please follow the steps above to resolve the issue and remove the message.

## Ribbon Menus Are Anchored in the Incorrect Location

This can occur when you are using a tablet PC or drawing tablet. The menu placement behavior will differ depending on whether the tablet support is configured for left-handed or right-handed use. This was designed so that menus do not appear under the user's hand.

To resolve this issue, simply change the tablet support configuration to left-handed.
