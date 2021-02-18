---
title: "Grids Foundation and Item Adapter"
page-title: "Grids Foundation and Item Adapter - Shell Reference"
order: 7
---
# Grids Foundation and Item Adapter

Both the [ShellTreeListBox](shelltreelistbox.md) and [ShellListView](shelllistview.md) controls directly or indirectly inherit the [TreeListBox](../grids/tree-control-features/index.md) control found in the Actipro Grids product.  This base control is very extensible, performant, and provides an excellent foundation for the functionality of the two shell controls.

## Assembly Reference and Licensing

Since the Shell product does rely on types defined in the Grids and Shared libraries, all three of those assemblies must be included in your project references when using the Shell product.

Please note that although a licensed Shell product does allow you to redistribute the Grids assembly, you must still obtain proper developer licenses for the Grids product if you plan on using controls like [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox) or [TreeListView](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView) independently of how they are utilized in the Shell product.

## Item Adapter

The Grids tree controls have the concept of [item adapters](../grids/tree-control-features/getting-started.md).  An item adapter is a class that provides a bridge between the Grids tree controls and the data that is being displayed in them.  The item adapter is necessary because it allows you to use data objects that are completely unknown to the Grids product, meaning the data objects can be defined in libraries that have no dependencies at all on UI frameworks or Actipro products.

Since some of the controls in the Shell product inherit Grids' tree controls, a [ShellObjectItemAdapter](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter) class has been created to provide the bridge between the [shell objects](shell-objects-framework/shell-objects.md) and the shell UI controls.  An instance of this class is automatically set to the [ItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.ItemAdapter) property of the applicable shell UI controls.

It's important to note that since the core shell objects aren't really UI-related, the item adapter wraps them in a [ShellObjectViewModel](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectViewModel) instance as they are added to a shell UI control.  Therefore properties inherited from the Grids tree controls like [RootItem](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.RootItem), [SelectedItem](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.SelectedItem), and [SelectedItems](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.SelectedItems) will reference the view-model objects that wrap the core [IShellObject](xref:ActiproSoftware.Shell.IShellObject) instances.

If any features of the [ShellObjectItemAdapter](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter) need to be overridden, such as for features described below, create a custom class that inherits that class and set it to the [ItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.ItemAdapter) property of the shell UI control.

## View-Model Creation

The [CreateShellObjectViewModel](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter.CreateShellObjectViewModel*) method wraps the specified [IShellObject](xref:ActiproSoftware.Shell.IShellObject) with a [ShellObjectViewModel](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectViewModel).

This view-model class has properties on it for tracking expansion, editing, selection states, and has a [Model](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectViewModel.Model) property that returns the wrapped [IShellObject](xref:ActiproSoftware.Shell.IShellObject).

It also has features for auto-returning an upsized icon in scenarios where higher DPI is detected.  For instance when in 200% DPI, the [ShellObjectViewModel](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectViewModel).[SmallIcon](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectViewModel.SmallIcon) property will actually return the [IShellObject](xref:ActiproSoftware.Shell.IShellObject).[MediumIcon](xref:ActiproSoftware.Shell.IShellObject.MediumIcon) value so that the image remains crisp.

While the [CreateShellObjectViewModel](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter.CreateShellObjectViewModel*) method doesn't generally need to be overridden, it is a good place to filter out shell objects at a view-level instead of at the model-level (via a shell service).  This could be useful when sharing shell objects in multiple shell UI controls, but wanting to filter out certain kinds of shell objects for a specific shell UI control.  To filter out a shell object, simply override this method and return `null`.

## Context Menu Creation

The [ShellObjectItemAdapter](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter) class has a couple methods for returning a context menu within a shell UI control.  Override the methods to customize the context menus.

If a [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) is in use, then the default implementations of these methods will attempt to create context menu using data from the Windows shell.  The context menus will generally have menu items whose `Command` is of type [WindowsShellMenuItemCommand](xref:ActiproSoftware.Shell.WindowsShellMenuItemCommand).  This type lists the target shell objects and includes any string verb related to the command that was provided by the system.

### GetItemContextMenu Method

The [GetItemContextMenu](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter.GetItemContextMenu*) method returns the `ContextMenu` for the specified collection of view-models.  The collection is usually a single view-model.

### GetBackgroundContextMenu Method

The [GetBackgroundContextMenu](xref:ActiproSoftware.Windows.Controls.Shell.ShellObjectItemAdapter.GetBackgroundContextMenu*) method returns the `ContextMenu` for the background of a control with the specified root view-model.  The "background" area is considered to be the control's whitespace area where no items are rendered.
