---
title: "Localization"
page-title: "Localization - Ribbon Layout, Globalization, and Accessibility Features"
order: 8
---
# Localization

All user interface text properties have the `Localizability` attribute applied to them, and all strings that are displayed in the user interface can be customized and localized.

## Localizable Text Properties

The following text properties are flagged as localizable via the `Localizability` attribute:

- [ButtonBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ButtonBase).[MenuItemDescription](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ButtonBase.MenuItemDescription)

- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[Hint](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.Hint)

- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.KeyTipAccessText)

- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.Label)

- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[ScreenTipDescription](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.ScreenTipDescription)

- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.ScreenTipFooter)

- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.ScreenTipHeader)

- [ControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase).[KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.KeyTipAccessText)

- [ControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.Label)

- [ControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase).[ScreenTipDescription](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.ScreenTipDescription)

- [ControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.ScreenTipFooter)

- [ControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.ScreenTipHeader)

- [GalleryItem](xref:@ActiproUIRoot.Controls.Ribbon.Controls.GalleryItem).[ScreenTipDescription](xref:@ActiproUIRoot.Controls.Ribbon.Controls.GalleryItem.ScreenTipDescription)

- [GalleryItem](xref:@ActiproUIRoot.Controls.Ribbon.Controls.GalleryItem).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.GalleryItem.ScreenTipFooter)

- [GalleryItem](xref:@ActiproUIRoot.Controls.Ribbon.Controls.GalleryItem).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Ribbon.Controls.GalleryItem.ScreenTipHeader)

- [Group](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Group).[DialogLauncherKeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Group.DialogLauncherKeyTipAccessText)

- [ItemsControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase.KeyTipAccessText)

- [ItemsControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase.Label)

- [ItemsControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[ScreenTipDescription](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase.ScreenTipDescription)

- [ItemsControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase.ScreenTipFooter)

- [ItemsControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase.ScreenTipHeader)

- [RecentDocumentMenu](xref:@ActiproUIRoot.Controls.Ribbon.Controls.RecentDocumentMenu).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Controls.RecentDocumentMenu.Label)

- [Ribbon](xref:@ActiproUIRoot.Controls.Ribbon.Ribbon).[ApplicationButtonLabel](xref:@ActiproUIRoot.Controls.Ribbon.Ribbon.ApplicationButtonLabel)

- [RibbonCommand](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommand).[KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommand.KeyTipAccessText)

- [RibbonCommand](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommand).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommand.Label)

- [RibbonCommandUIProvider](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommandUIProvider).[KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommandUIProvider.KeyTipAccessText)

- [RibbonCommandUIProvider](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommandUIProvider).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Input.RibbonCommandUIProvider.Label)

- [RibbonWindow](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow).[ApplicationName](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow.ApplicationName)

- [RibbonWindow](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow).[DocumentName](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow.DocumentName)

- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase).[Hint](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase.Hint)

- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase).[KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase.KeyTipAccessText)

- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase).[Label](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase.Label)

- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase).[ScreenTipDescription](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase.ScreenTipDescription)

- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase.ScreenTipFooter)

- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase.ScreenTipHeader)

## Customizing String Resources

All strings that are displayed in the user interface are available from static methods on the `ActiproSoftware.Products.Ribbon.SR` class.  Use that class to customize string resources as well.

> [!NOTE]
> String resource customization is described in great detail in the general [Customizing String Resources](../../customizing-string-resources.md) topic.  Please see that topic for in-depth information related to localization of string resources.

An enumation named `SRName` includes a list of all the string resource names.  A XAML markup extension named `SRExtension` is available for use within XAML templates to access string resources.
