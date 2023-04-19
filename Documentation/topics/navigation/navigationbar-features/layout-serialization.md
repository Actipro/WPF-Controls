---
title: "Layout Serialization"
page-title: "Layout Serialization - NavigationBar Features"
order: 9
---
# Layout Serialization

A `NavigationBar` layout is defined as the selected pane, buttons visible, pane order, and pane visibility for a [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) control.

Sometimes it is useful to save the layout state of a `NavigationBar` and restore it later.  This allows end users to retain their customizations across multiple application executions.  The way to do this is to persist the `NavigationBar` layout data.

> [!IMPORTANT]
> When persisting layouts, each [NavigationPane](xref:@ActiproUIRoot.Controls.Navigation.NavigationPane) must have a unique value assigned to its `Name` property, otherwise the layout cannot be created.

## Saving Layout Data

`NavigationBar` layout data can be persisted in XML format and loaded at a later time.  Probably the two most common ways to store layouts are in files and in a database.

The [NavigationBarLayoutSerializer](xref:@ActiproUIRoot.Controls.Navigation.Serialization.NavigationBarLayoutSerializer) class fully implements the XML object hierarchy serialization framework described in the [Serialization](../../shared/windows-serialization.md) topic.  Please see that topic for a list of methods that can be called for saving to files, string, etc.

This sample code shows how to save a [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) layout to an XML string:

```csharp
string layout = new NavigationBarLayoutSerializer().SaveToString(navBar);
```

## Loading Layout Data

When loaded, `NavigationBar` layout data restores the order and visibility states of the panes being managed by the `NavigationBar` control.  It also determines the number of large buttons to display and selects the pane that was selected when the layout was saved.

The layout data loading code matches the `Name` of panes that were stored in the layout data to the `Name` of panes contained in the [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar).

The [NavigationBarLayoutSerializer](xref:@ActiproUIRoot.Controls.Navigation.Serialization.NavigationBarLayoutSerializer) class fully implements the XML object hierarchy serialization framework described in the [Serialization](../../shared/windows-serialization.md) topic.  Please see that topic for a list of methods that can be called for loading from files, string, etc.

This sample code shows how to load a [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) layout from the XML string:

```csharp
new NavigationBarLayoutSerializer().LoadFromString(layout, navBar);
```

## Persisting Custom Data in the Layout Data

One benefit of our XML object hierarchy serialization framework is that you can insert and later retrieve information anywhere within the serialized data.  Any time an object is serialized or deserialized, an event is raised.  You can intercept this event and save/load custom data.

The [Serialization](../../shared/windows-serialization.md) topic explains how to do this.

## Optimal Memory Utilization when Using Layout Serializers

The layout serializer uses an `XmlSerializer` as the core .NET object that reads/writes XML data.  One issue that has been discovered in the Microsoft .NET framework is that `XmlSerializer` is capable of creating memory leaks.  The leak occurs whenever a new instance of `XmlSerializer` is created.

To combat this leak we've implemented some caching code on our end, but also highly recommend that instead of creating a new layout serializer any time you do a layout serialization, you instead keep a reference to a single app-wide instance of the layout serializer and use that for each layout serialization.
