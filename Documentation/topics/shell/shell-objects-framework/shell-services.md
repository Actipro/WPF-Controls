---
title: "Shell Services"
page-title: "Shell Services - Shell Object Framework"
order: 4
---
# Shell Services

A shell service creates and interacts with shell objects, and is the main customization point for interfacing with a file system such as the Windows shell.

All shell services implement the [IShellService](xref:ActiproSoftware.Shell.IShellService) interface, which defines the members needed to drive the user interface and behavior functionality in UI controls like [ShellTreeListBox](../shelltreelistbox.md).

## Built-In Windows Shell Service

The Shell Object Framework comes with a pre-built [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) class for the Windows operating system shell.  This shell service wraps up all the native Win32 API and COM calls needed to interface with the full Windows shell.  This functionality provides much more than what you can achieve with standard .NET `System.IO` namespace features, including full access to virtual folders/objects and libraries in the shell, system icons, etc.

An instance of the [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) class is set as the default shell service for the [ShellTreeListBox](../shelltreelistbox.md) and [ShellListView](../shelllistview.md) controls.  This allows those UI controls to support working with the Windows shell out of the box.

All of the methods on [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) are virtual, allowing you to override and customize their results as appropriate.

## Using Independently of UI

While shell services are harnessed to drive the user interface of the shell controls, they and the related shell objects can be used completely independently of any user interfaces.

They fully support calls to any of their members, allowing you to programmatically examine shell folder and file hierarchies, or properties of shell objects.  This is expecially useful for working with the [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService), since its implementation provides shell data not available in the `System.IO` namespace.

## Creating an Object's Children

The [CreateObjectChildren](xref:ActiproSoftware.Shell.IShellService.CreateObjectChildren*) method creates the collection of child [shell objects](shell-objects.md) for the specified parent shell object.

This method is invoked when the [ShellObjectBase](xref:ActiproSoftware.Shell.ShellObjectBase).[Children](xref:ActiproSoftware.Shell.ShellObjectBase.Children) property is first accessed, which often occurs as the parent shell object is being expanded in a tree control, or is set as the root shell folder of a list control.  The method's results are used to construct the collection returned by that property.

Only shell folders (not files or links) should ever return children.

## Creating an Object for a Parsing Name

The [CreateObjectForParsingName](xref:ActiproSoftware.Shell.IShellService.CreateObjectForParsingName*) method creates an [IShellObject](xref:ActiproSoftware.Shell.IShellObject) based on the specified full parsing name.

A full parsing name is a unique string that can identify a shell object, often the same as a file system path.  See the [Shell Objects](shell-objects.md) topic for more information on parsing names.

The [AreNamesCaseSensitive](xref:ActiproSoftware.Shell.IShellService.AreNamesCaseSensitive) property returns whether shell object names like parsing names are case-sensitive.  The [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) implementation returns `false` for this property.

## Getting an Object's Full Path

The [Grids tree controls](../../grids/tree-control-features/index.md) like [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) have their own concept of "paths."  Those tree controls provide the foundation of several shell UI controls and thus inherit that "path" concept, which can be different from a shell file system path.

First, a tree control "path" segment is returned for each item in the tree.  These paths can be combined with a path separator delimiter (often "\\") to construct what is called a "full path." Thus a "full path" becomes a delimited string where each "path" segment in it, can be examined to walk down the tree hierarchy.

The [Shell Objects](shell-objects.md) topic has more information on paths.  In general though, the [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[RelativeParsingName](xref:ActiproSoftware.Shell.IShellObject.RelativeParsingName) is what is returned as the path segment for each shell object, used to build up a "full path."

## Creating an Object's Properties

The [CreateProperties](xref:ActiproSoftware.Shell.IShellService.CreateProperties*) method creates a collection of [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty) objects for the specified shell objects.

A [ShellPropertyRequestKind](xref:ActiproSoftware.Shell.ShellPropertyRequestKind) enum value is also passed, specifying the kind of properties to return.  These values are supported:

- **DefaultColumns** - Return the properties that should show in a [ShellListView](../shelllistview.md) control.
- **AllColumns** - Isn't really used in UI but allows you to examine all available properties.

The [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty) interface defines a property, such as its key, canonical name (non-localized name), display name, default column width, column sort direction, column horizontal alignment, etc.  The column-related properties are specifically for use within a [ShellListView](../shelllistview.md) control.

If you wish to supply your own shell property results, make a class that implements the [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty) interface and return instances of that.

The most common properties returned for file system folders have these canonical names:

- System.ItemNameDisplay
- System.DateModified
- System.ItemType
- System.Size

## Getting an Object's Property Value

The [GetPropertyValue](xref:ActiproSoftware.Shell.IShellService.GetPropertyValue*) method examines an [IShellObject](xref:ActiproSoftware.Shell.IShellObject) to return a value for the specified [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty).

This method is called when getting values to insert into [ShellListView](../shelllistview.md) control cells.

## Comparing Objects Using a Property

The [CompareObjectsUsingProperty](xref:ActiproSoftware.Shell.IShellService.CompareObjectsUsingProperty*) method compares two [IShellObject](xref:ActiproSoftware.Shell.IShellObject) instances using the specified [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty).

This method is called when sorting a [ShellListView](../shelllistview.md) control by a column, since each column represents an [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty).  The integer result from numerous calls to the method for various shell object pairs is used to determine their final sort order.
