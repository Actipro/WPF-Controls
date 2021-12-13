---
title: "Troubleshooting"
page-title: "Troubleshooting - Themes Reference"
order: 99
---
# Troubleshooting

This topic contains troubleshooting data specific to the Actipro Themes product.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as other WPF Studio products, please see the more general [TroubleShooting](../troubleshooting.md) topic.

## Aero or Office 2010 Themes Are Not Working

There are two steps that must be performed in order to leverage the Aero-style (Aero and Office 2010) themes in your application.  So if you try to set the current theme to one of the supported Aero-style themes, but the colors do not update accordingly, then please ensure the following steps have been performed.

> [!NOTE]
> Tinted themes that are based on one of the Aero-style themes can also be affected, if the source theme is not registered correctly.

The Aero-style themes are located in a separate assembly, to help reduce the install base size for customers that do not use it.  Therefore, you must first ensure that your application has a reference to the `ActiproSoftware.Themes.Aero.Wpf.dll` assembly.

Once the reference has been added, the Aero theme catalog must be manually registered with the theme manager during application startup before the theme is used:

```csharp
ThemesAeroThemeCatalogRegistrar.Register();
```

> [!NOTE]
> See the [Getting Started](getting-started.md) topic for a complete example of how to register the Aero theme catalogs, as well as how to combine them with other settings.

## ListView Controls are Displaying Items Incorrectly

The native WPF `ListView` control comes in two forms.  In it's simplest form, it acts just like a `ListBox`, which is its base class.  To display tabular data, a `ListView` can be assigned a `GridView` which contains one or more column definitions.  Our native theme support for the `ListView` is limited to this latter form, due to the style extensibility of the `ListView`.

Therefore, in cases where a `ListView` is being used without an associated `GridView` would need to be replaced with an instance of `ListBox`.  There should be no loss of functionality by making this change.

## Themes Do Not Work When Hosted in Non-WPF Applications

### Ensuring Application.Current Is Initialized

Actipro's theme manager and `Image` embedded resources (such as those used in our IntelliPrompt popups) with pack:// URIs both require access to the WPF `Application.Current` object.  When hosting WPF controls in a WinForms or other non-WPF application, the `Application.Current` property will be null by default.

This effectively prevents the theme manager from dynamically changing the current theme, and any pack:// URI references to `Image` embedded resources may fail with a `NotSupportedException` and message "The URI prefix is not recognized."

Both issues can be resolved by creating a WPF `Application` object.  The WPF `Application` object can be programmatically created in your application startup before any UI is created like this:

```csharp
if (System.Windows.Application.Current == null) {
	wpfApplication = new System.Windows.Application();
	wpfApplication.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
}
```

The `wpfApplication` variable should be stored at your static application level and in your application shutdown code, the `wpfApplication.Shutdown()` method should be called to explicitly close the WPF `Application`.

The reason we need to use `ShutdownMode.OnExplicitShutdown` above is that by default, the last WPF window that closes will shut down the WPF application and it will be as if no application was created, breaking pack:// syntax and causing the `NotSupportedException` exception.  If you are still in evaluation mode, the Actipro evaluation dialog for the WPF Controls could trigger this when it closes.  By explicitly controlling the lifetime of the WPF application, you prevent that scenario from occurring.

### Ensuring ThemeManager Is Initialized

The Actipro [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager) is initialized in a `Send`-priority dispatched call when it is first used.  This initialization can be triggered manually, but also happens automatically when themed Actipro WPF Controls are first loaded.

The initialization is important because it is what makes the various Actipro theme resources like brushes available to all the themed controls.  If the initialization didn't occur yet, some WPF Controls used in interop may not render with their normal appearance.

In this interop scenario, it might be required to manually trigger the initialization to ensure that theme resources like brushes are loaded properly when the first window of the application is displayed.

This sample code used in the application startup logic (and after ensuring `Application.Current` is initialized) sets the current theme, which will tell [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager) to initialize as well, prior to UI being loaded:

```csharp
ThemeManager.CurrentTheme = ThemeNames.MetroDark
```
