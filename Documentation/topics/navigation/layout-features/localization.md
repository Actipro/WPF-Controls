---
title: "Localization"
page-title: "Localization - Navigation Layout, Globalization, and Accessibility Features"
order: 3
---
# Localization

All of the Navigation controls' text properties have the `Localizability` attribute applied to them, and all strings that are displayed in the user interface can be customized and localized.

## Localizable NavigationBar Properties

The following properties on the [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) control are localizable:

- [Title](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar.Title)

## Localizable NavigationPane Properties

The following properties on the [NavigationPane](xref:@ActiproUIRoot.Controls.Navigation.NavigationPane) control are localizable:

- [Title](xref:@ActiproUIRoot.Controls.Navigation.NavigationPane.Title)

## Localizable BreadcrumbItem Properties

The following properties on the [BreadcrumbItem](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem) control are localizable:

- [PathEntry](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.PathEntry)

## Customizing String Resources

All strings that are displayed in the user interface are available from static methods on the `ActiproSoftware.Products.Navigation.SR` class.  Use that class to customize string resources as well.

> [!NOTE]
> String resource customization is described in great detail in the general [Customizing String Resources](../../customizing-string-resources.md) topic.  Please see that topic for in-depth information related to localization of string resources.

An enumation named `SRName` includes a list of all the string resource names.  A XAML markup extension named `SRExtension` is available for use within XAML templates to access string resources.
