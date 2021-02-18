---
title: "Localization"
page-title: "Localization - Ribbon Layout, Globalization, and Accessibility Features"
order: 8
---
# Localization

All user interface text properties have the `Localizability` attribute applied to them, and all strings that are displayed in the user interface can be customized and localized.

## Localizable Text Properties

The following text properties are flagged as localizable via the `Localizability` attribute:

- [ButtonBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ButtonBase).[MenuItemDescription](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ButtonBase.MenuItemDescription)

- [ComboBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[Hint](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase.Hint)

- [ComboBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[KeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase.KeyTipAccessText)

- [ComboBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase.Label)

- [ComboBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[ScreenTipDescription](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase.ScreenTipDescription)

- [ComboBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[ScreenTipFooter](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase.ScreenTipFooter)

- [ComboBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[ScreenTipHeader](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ComboBoxBase.ScreenTipHeader)

- [ControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase).[KeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.KeyTipAccessText)

- [ControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.Label)

- [ControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase).[ScreenTipDescription](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.ScreenTipDescription)

- [ControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase).[ScreenTipFooter](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.ScreenTipFooter)

- [ControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase).[ScreenTipHeader](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.ScreenTipHeader)

- [GalleryItem](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.GalleryItem).[ScreenTipDescription](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.GalleryItem.ScreenTipDescription)

- [GalleryItem](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.GalleryItem).[ScreenTipFooter](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.GalleryItem.ScreenTipFooter)

- [GalleryItem](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.GalleryItem).[ScreenTipHeader](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.GalleryItem.ScreenTipHeader)

- [Group](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Group).[DialogLauncherKeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Group.DialogLauncherKeyTipAccessText)

- [ItemsControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[KeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase.KeyTipAccessText)

- [ItemsControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase.Label)

- [ItemsControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[ScreenTipDescription](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase.ScreenTipDescription)

- [ItemsControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[ScreenTipFooter](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase.ScreenTipFooter)

- [ItemsControlBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[ScreenTipHeader](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ItemsControlBase.ScreenTipHeader)

- [RecentDocumentMenu](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.RecentDocumentMenu).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.RecentDocumentMenu.Label)

- [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon).[ApplicationButtonLabel](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.ApplicationButtonLabel)

- [RibbonCommand](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand).[KeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand.KeyTipAccessText)

- [RibbonCommand](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand.Label)

- [RibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIProvider).[KeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIProvider.KeyTipAccessText)

- [RibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIProvider).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIProvider.Label)

- [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow).[ApplicationName](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.ApplicationName)

- [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow).[DocumentName](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.DocumentName)

- [TextBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase).[Hint](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase.Hint)

- [TextBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase).[KeyTipAccessText](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase.KeyTipAccessText)

- [TextBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase).[Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase.Label)

- [TextBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase).[ScreenTipDescription](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase.ScreenTipDescription)

- [TextBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase).[ScreenTipFooter](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase.ScreenTipFooter)

- [TextBoxBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase).[ScreenTipHeader](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.TextBoxBase.ScreenTipHeader)

## Customizing String Resources

All strings that are displayed in the user interface are available from static methods on the `ActiproSoftware.Products.Ribbon.SR` class.  Use that class to customize string resources as well.

> [!NOTE]
> String resource customization is described in great detail in the general [Customizing String Resources](../../customizing-string-resources.md) topic.  Please see that topic for in-depth information related to localization of string resources.

An enumation named `SRName` includes a list of all the string resource names.  A XAML markup extension named `SRExtension` is available for use within XAML templates to access string resources.
