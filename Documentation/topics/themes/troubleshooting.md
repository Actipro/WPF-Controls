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

## Themes Do Not Work When Hosted in WinForms Application

Actipro's theme manager requires access to the WPF `Application.Current` object.  When hosting WPF controls in a WinForms application, the `Application.Current` object will always be null.  This effectively prevents the theme manager from dynamically changing the current theme.

It is possible to work around this issue by artifically creating a WPF application object, like so:

```csharp
public static void CreateWpfApplication() {
    if (System.Windows.Application.Current == null) {
        var app = new System.Windows.Application();
		app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
	}
}
```

This method would then need to be called when your WinForms application is starting.
