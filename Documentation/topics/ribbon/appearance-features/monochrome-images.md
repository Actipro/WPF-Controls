---
title: "Monochrome Images"
page-title: "Monochrome Images - Ribbon Appearance Features"
order: 7
---
# Monochrome Images

Several Actipro themes require the use of white monochrome images in specific portions of the ribbon user interface.  Ribbon has special logic that will automatically convert `BitmapSource` and `DrawingImage` images to a pure white monochrome equivalent for proper display in these scenarios.  This allows you to use a single image set for your entire UI while still supporting modern UI.

## QuickAccessToolBar and Tab Panel Buttons

![Screenshot](../images/ribbon.png)

*Ribbon in the OfficeColorfulIndigo theme*

When one of the Office Colorful themes is used, such as OfficeColorfulIndigo above, any button images in the QAT or in the tab panel must be switched over to monochrome variations.  Ribbon has logic in it to automatically perform this conversion on a ribbon button with a supplied normal image.

See the [Themes' Getting Started](../../themes/getting-started.md) page for a complete list of themes.

## Status Bar Buttons

Numerous Metro themes feature a high contrast status bar background, such as a bright blue color.  In these themes, button images need to be switched over to monochrome variations.  Ribbon has logic in it to automatically perform this conversion on a ribbon button with a supplied normal image.

## Optimal Image Design

The algorithm that the conversion-to-monochrome logic uses is based on the lightness of each pixel.  Dark and medium lightness pixels will be converted to white, while light pixels will be converted to transparent.

It is best to use Metro-themed images that don't contain gradients as a basis for this feature.

> [!NOTE]
> Be careful of images that were already light/white to begin with, as they may end up as appearing completely transparent unless dark colors are used in the image.

## Preventing Portions of a DrawingImage from Converting

In some cases, such as for a vector icon that has a portion showing a selected color, you may not wish to convert the selected color portion to monochrome.  Yet you may wish for the entire rest of the image to be converted.

This can be achieved by setting the attached [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).`CanAdapt` property to `false` on the portion for the selected color.  That will tell the converter to skip over converting colors within that portion of the image.
