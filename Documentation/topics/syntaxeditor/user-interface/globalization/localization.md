---
title: "Localization"
page-title: "Localization - SyntaxEditor Globalization and Accessibility"
order: 4
---
# Localization

All of the strings that are displayed in the user interface can be customized and localized.

## Customizing String Resources

All strings that are displayed in the user interface are available from static methods on the `ActiproSoftware.Products.SyntaxEditor.SR` and `ActiproSoftware.Products.Text.SR` classes.  The optional add-on products have their own related `SR` classes too.

> [!NOTE]
> String resource customization is described in great detail in the general [Customizing String Resources](../../../customizing-string-resources.md) topic.  Please see that topic for in-depth information related to localization of string resources.

An enumeration named `SRName` includes a list of all the string resource names.  A XAML markup extension named `SRExtension` is available for use within XAML templates to access string resources.
