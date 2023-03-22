---
title: "Custom Shell Objects"
page-title: "Custom Shell Objects - Shell Object Framework"
order: 10
---
# Custom Shell Objects

The built-in Windows shell functionality can be customized/extended.  Or in other cases, completely custom shell objects and services can be written to support non-Windows shells (i.e., a remote file system in a FTP client).

## Customizing the Windows Shell

The built-in [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) class interacts with the Windows shell, attempting to return the same data you find in Windows Explorer.

There may be cases where you wish to filter out certain shell objects, change certain shell object properties, etc. from what is returned by default.  In these cases, you need to make a class that inherits [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) and set the new instance to your shell UI control's [DefaultShellService](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DefaultShellService) property.

### Filtering Out Shell Objects

To filter out a shell object, override the [CreateObjectChildren](xref:ActiproSoftware.Shell.WindowsShellService.CreateObjectChildren*) method and call its base method, which is a collection result.  Iterate backwards through the collection and remove any [IShellObject](xref:ActiproSoftware.Shell.IShellObject) that you don't wish to have in the results.  This change will affect all shell UI controls that use this shell service.  If you wish to filter out shell objects for a single shell UI control, use the method described in the [Grids Foundation and Item Adapter](../grids-foundation.md) topic instead, since that only affects a single view.

### Appending Shell Objects

To insert additional shell objects as the children of a parent shell object, override the [CreateObjectChildren](xref:ActiproSoftware.Shell.WindowsShellService.CreateObjectChildren*) method and call its base method, which is a collection result.  Make a class that inherits [ShellObjectBase](xref:ActiproSoftware.Shell.ShellObjectBase) and will represent your additional shell object(s).  Add one or more instances of that class to the list results and the additional shell object(s) should appear in all UI controls, unless they are filtered out at a view-level, as described in the [Grids Foundation and Item Adapter](../grids-foundation.md) topic.

### Wrapping a Shell Object

If you wish to change actual properties of a Windows [IShellObject](xref:ActiproSoftware.Shell.IShellObject) (not the [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty) objects for a shell object), you must wrap the shell object.  A handy wrapper [ShellObjectWrapper](xref:ActiproSoftware.Shell.ShellObjectWrapper) class is provided specifically for this scenario.  That class allows you to pass the [IShellObject](xref:ActiproSoftware.Shell.IShellObject) and it wraps all the calls to the shell object's members in virtual members of its own.  For instance if you wanted to customize the icon returned for a shell object, you could override the appropriate icon properties.  Or if you wanted to treat a certain shell folder as a file instead, you could override its [IsFolder](xref:ActiproSoftware.Shell.ShellObjectWrapper.IsFolder) and [Kind](xref:ActiproSoftware.Shell.ShellObjectWrapper.Kind) properties.

To insert this wrapper shell object into the shell object hierarchy, override the [CreateObjectChildren](xref:ActiproSoftware.Shell.WindowsShellService.CreateObjectChildren*) method and call its base method, which is a collection result.  Replace the original [IShellObject](xref:ActiproSoftware.Shell.IShellObject) in that collection with the related wrapper shell object.

### Adding a Column to ShellListView

To add a column to the [ShellListView](../shelllistview.md) control, create a new class that implements [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty).  Override the [CreateProperties](xref:ActiproSoftware.Shell.WindowsShellService.CreateProperties*) method and call its base method, which is a collection result, and add it to the collection.

Be sure to also override the [GetPropertyValue](xref:ActiproSoftware.Shell.IShellService.GetPropertyValue*) method to handle returning values for the new [IShellProperty](xref:ActiproSoftware.Shell.IShellProperty) if the property is a custom one.

## Making a Completely Custom Shell

A custom-written shell service allows for working with any file system.  A great example of this is the remote file system for a FTP client application.  By using the Shell Object Framework to implement a custom shell service, the same shell UI controls can be used to render a Windows file system as well as another kind of custom file system.

To author a custom shell service, make a class that implements [IShellService](xref:ActiproSoftware.Shell.IShellService) and set a new instance to your shell UI control's [DefaultShellService](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DefaultShellService) property.

All of the members described in the [Shell Services](shell-services.md) documentation topic should be implemented to ensure that the attached shell UI controls can behave as expected.

Use the [ShellObjectBase](xref:ActiproSoftware.Shell.ShellObjectBase) class as a base class of your custom shell objects that are returned by your custom shell service.  This base class helps reduce the amount of work you need to do to implement the [IShellObject](xref:ActiproSoftware.Shell.IShellObject) interface.
