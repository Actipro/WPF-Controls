---
title: "Shell Objects"
page-title: "Shell Objects - Shell Object Framework"
order: 2
---
# Shell Objects

Shell objects represent a folder, file, or link from a [shell service](shell-services.md).  For instance, our Windows shell service implementation creates shell objects to represent the folders, files, and links found on your PC.

Each shell object implements the [IShellObject](xref:ActiproSoftware.Shell.IShellObject) interface and provides its name and path (in several forms), kind, icons, thumbnail images, tool tip, and children.

When making your own custom shell object class, it is helpful to use the [ShellObjectBase](xref:ActiproSoftware.Shell.ShellObjectBase) class as a base class, since it implements a lot of the core required functionality.  Then you simply override properties and methods as appropriate.

## Names and Paths

Due to how shell objects can appear in multiple locations in UI such as within a file explorer, there are several name and path properties that handle various usage scenarios as explained below.

### Name

The [Name](xref:ActiproSoftware.Shell.IShellObject.Name) property returns the name of the object when it is displayed in trees and lists.

Some examples are `"Foo.txt"` for a file, or `"OneDrive"` for the system's **OneDrive** folder.

### File System Path

The [FileSystemPath](xref:ActiproSoftware.Shell.IShellObject.FileSystemPath) property returns the path to the object if it is part of a file system.  The property will return `null` for any virtual object, such as **This PC**, that is not part of the file system.

An example is `"C:\\OneDrive"` for the system's **OneDrive** folder.

### Editing Name

The [EditingName](xref:ActiproSoftware.Shell.IShellObject.EditingName) property returns the full user-friendly editing name of the shell object, if available.  This value is usually the same as [FileSystemPath](xref:ActiproSoftware.Shell.IShellObject.FileSystemPath) for file system objects, except that it can also return a special value to identify special shell objects.  Think of the editing name as the text that you see when you enter edit mode in the Windows Explorer breadcrumb.

Some examples are `"OneDrive"` for the system's **OneDrive** folder, but `"C:\\OneDrive"` for the same folder accessed through the `C:` drive.  Objects like **This PC** will return an editing name of `"This PC"`.

### Parsing Name

The [ParsingName](xref:ActiproSoftware.Shell.IShellObject.ParsingName) property returns the full parsing name of the shell object, if available.  This value is usually the same as [FileSystemPath](xref:ActiproSoftware.Shell.IShellObject.FileSystemPath) for file system objects, except that it can also return a special syntax to identify special shell objects.  This value is not ever shown in UI but is intended to reduce ambiguity when locating shell objects.

Some examples are `"::\{20D04FE0-3AEA-1069-A2D8-08002B30309D}"` for the **This PC** folder, and `"C:\\OneDrive"` for the system's **OneDrive** folder.

### Relative Parsing Name

The [RelativeParsingName](xref:ActiproSoftware.Shell.IShellObject.RelativeParsingName) property returns the parent-relative parsing name of the shell object, if available.  This value is usually the same as [Name](xref:ActiproSoftware.Shell.IShellObject.Name), except that it can also return a special syntax to identify special shell objects.  This value is not ever shown in UI but is intended to reduce ambiguity when locating shell objects.

Some examples are `"::\{018D5C66-4533-4307-9B53-224DE2ED1FE6}"` for the system's **OneDrive** folder, and `"OneDrive"` for the same folder accessed through the `C:` drive.

This property is used when building up a "full path" for Grids' [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).  It is returned as the path segment for the shell object in the [ShellObjectItemAdapter](xref:@ActiproUIRoot.Controls.Shell.ShellObjectItemAdapter).[GetPath](xref:@ActiproUIRoot.Controls.Shell.ShellObjectItemAdapter.GetPath*) method.

## Kind

Shell objects are all various kinds of folders, files, or links.  The [Kind](xref:ActiproSoftware.Shell.IShellObject.Kind) property returns an enumeration value of type [ShellObjectKind](xref:ActiproSoftware.Shell.ShellObjectKind), which covers the basic file system kinds and also things like special folders and libraries.

The [IsFolder](xref:ActiproSoftware.Shell.IShellObject.IsFolder) property returns if the shell object is any sort of folder (file system folder, library, etc.).  The [IsLink](xref:ActiproSoftware.Shell.IShellObject.IsLink) property returns if the shell object is a link to another shell object.  If both of those properties return `false`, then the shell object is a file or virtual non-folder object.

## Icons and Overlays

Shell objects provide several size variations of icons and optional related overlay icons, each of which may be used in various UI scenarios.

- [SmallIcon](xref:ActiproSoftware.Shell.IShellObject.SmallIcon) / [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[SmallIconOverlay](xref:ActiproSoftware.Shell.IShellObject.SmallIconOverlay) - The small icon, which is generally 16x16 size.

- [MediumIcon](xref:ActiproSoftware.Shell.IShellObject.MediumIcon) / [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[MediumIconOverlay](xref:ActiproSoftware.Shell.IShellObject.MediumIconOverlay) - The medium icon, which is generally 32x32 size.

- [LargeIcon](xref:ActiproSoftware.Shell.IShellObject.LargeIcon) / [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[LargeIconOverlay](xref:ActiproSoftware.Shell.IShellObject.LargeIconOverlay) - The large icon, which is generally 48x48 size.

- [ExtraLargeIcon](xref:ActiproSoftware.Shell.IShellObject.ExtraLargeIcon) / [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[ExtraLargeIconOverlay](xref:ActiproSoftware.Shell.IShellObject.ExtraLargeIconOverlay) - The extra-large icon, which is generally 256x256 size.

The overlay icon can be used to indicate a "state" that overlays the normal icon.  An example is that link shell objects show an arrow overlay.

## Thumbnail Images

Shell objects provide several size variations of thumbnail images, each of which may be used in various UI scenarios.

- [MediumThumbnail](xref:ActiproSoftware.Shell.IShellObject.MediumThumbnail) - The medium thumbnail image, which is generally 32x32 size.

- [LargeThumbnail](xref:ActiproSoftware.Shell.IShellObject.LargeThumbnail) - The large thumbnail image, which is generally 48x48 size.

- [ExtraLargeThumbnail](xref:ActiproSoftware.Shell.IShellObject.ExtraLargeThumbnail) - The extra-large thumbnail image, which is generally 256x256 size.

Thumbnail images can be used in a [ShellListView](../shelllistview.md) control when its [CanUseThumbnails](xref:@ActiproUIRoot.Controls.Shell.ShellListView.CanUseThumbnails) property is `true`.  In that case, a thumbnail image is used in certain larger layout modes when one is available.  If no thumbnail image is available, an icon of the same size is used instead.

## Tool Tip

Each shell object can return a tool tip to display when the mouse hovers over a UI item for the shell object.  The tool tip content can be specified in the [ToolTip](xref:ActiproSoftware.Shell.IShellObject.ToolTip) property.

## Children

The [Children](xref:ActiproSoftware.Shell.IShellObject.Children) collection returns the children of the shell object.  This collection is lazy-loaded on first call, using the [shell service](shell-services.md) that was passed into the shell object.

If you wish to know if the [Children](xref:ActiproSoftware.Shell.IShellObject.Children) collection has been loaded already without already triggering a load, use the [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[AreChildrenLoaded](xref:ActiproSoftware.Shell.IShellObject.AreChildrenLoaded) property.

The [RefreshChildren](xref:ActiproSoftware.Shell.IShellObject.RefreshChildren*) method can be called to refresh the [Children](xref:ActiproSoftware.Shell.IShellObject.Children) collection if it has already been loaded.

The [CanHaveChildFolders](xref:ActiproSoftware.Shell.IShellObject.CanHaveChildFolders) property returns whether the shell object can possibly contain any child folders.  This property will only return `true` for folder shell objects.  Even some folder shell objects may return `false` if they know they don't currently have any child folders without a potentially-lengthy query.

## Creating a Child Folder

The [CanCreateChildFolder](xref:ActiproSoftware.Shell.IShellObject.CanCreateChildFolder) property returns whether the shell object can create a child folder.  This property will only return `true` for folder shell objects.

The [CreateChildFolder](xref:ActiproSoftware.Shell.IShellObject.CreateChildFolder*) method can be used to create a child folder within the shell object.  It is passed the name of the folder to create and returns whether the folder was successfully created.

## Renaming

The [CanRename](xref:ActiproSoftware.Shell.IShellObject.CanRename) property returns whether the shell object can be renamed.  When this property returns `true`, some UI controls may allow features like inline renaming.

The shell object can be renamed by setting the [Name](xref:ActiproSoftware.Shell.IShellObject.Name) property to a new value.

## Watching

Watching is a feature where a shell hierarchy can receive change notifications so that it can update itself based on external factors.  This feature is not generally invoked directly since UI controls will begin watching shell objects as they enter the UI.

The [IsWatching](xref:ActiproSoftware.Shell.IShellObject.IsWatching) property indicates if the shell object's hierarchy is currently being watched.  The [StartWatching](xref:ActiproSoftware.Shell.IShellObject.StartWatching*) method starts watching and the [StopWatching](xref:ActiproSoftware.Shell.IShellObject.StopWatching*) method stops watching.

## Wrapping Another Shell Object

Sometimes you might wish to wrap another shell object for which you don't have source.  An example scenario of this is when you are using our Windows shell service to return Windows shell objects, but you want to convert a certain shell object to indicate it's a file instead of a folder.  Since the "kind" properties don't have setters, this is an instance where you'd need to wrap the [IShellObject](xref:ActiproSoftware.Shell.IShellObject) returned by the Windows shell service with an [IShellObject](xref:ActiproSoftware.Shell.IShellObject) of your own, and change the returned values of a couple properties.  This is where the [ShellObjectWrapper](xref:ActiproSoftware.Shell.ShellObjectWrapper) class proves to be extremely handy.

[ShellObjectWrapper](xref:ActiproSoftware.Shell.ShellObjectWrapper) is a full [IShellObject](xref:ActiproSoftware.Shell.IShellObject) implementation, and is passed another [IShellObject](xref:ActiproSoftware.Shell.IShellObject) in its constructor.  By default, it fully wraps the passed-in [IShellObject](xref:ActiproSoftware.Shell.IShellObject), returning the same property values, executing the wrapped object's methods, etc.  All the [ShellObjectWrapper](xref:ActiproSoftware.Shell.ShellObjectWrapper) members are declared as virtual though, which allows you to easily override specific ones while leaving the rest to operate normally.
