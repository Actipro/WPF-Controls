---
title: "Memory Management"
page-title: "Memory Management - Shell Reference"
order: 12
---
# Memory Management

Shell objects and services often have to deal with unmanaged resources that need cleanup when they are no longer used.  For instance, Windows shell objects internally maintain a system-specified PIDL reference.

The shell types used in this product are all disposable, and it's a best practice to manually dispose them after use.  This helps keep memory from building up until those objects would later be automatically finalized.

The shell object framework can be used independently of shell UI controls, and shell objects can even be shared across multiple shell UI controls.  Thus it's not really possible for us to know when to automatically dispose shell objects and services, and the objects must rely on you the developer to trigger disposal.

## Shell Objects

A root shell object can be set to controls like [ShellTreeListBox](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox) when its [RootShellFolder](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.RootShellFolder) property is set directly, or its [RootShellFolderParsingName](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.RootShellFolderParsingName) or [RootSpecialFolderKind](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.RootSpecialFolderKind) properties are set.  In these cases, the shell object hierarchy likely has unmanaged resources being maintained.

When the UI control is no longer in use, the object that was set to the [RootShellFolder](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.RootShellFolder) property directly or indirectly should be disposed, and that property should be set to `null`.

## Shell Services

A [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) instance is set by default to the [DefaultShellService](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DefaultShellService) property of controls like [ShellTreeListBox](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox).  The [WindowsShellService](xref:ActiproSoftware.Shell.WindowsShellService) doesn't create any unmanaged resources until it's actually used, but it's important to dispose the service.

When the UI control is no longer in use, the object that was set to the [DefaultShellService](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DefaultShellService) property should be disposed, and that property should be set to `null`.

## Dispose Helper Methods on UI Controls

Both the [ShellTreeListBox](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox) and [ShellListView](xref:@ActiproUIRoot.Controls.Shell.ShellListView) controls have a [DisposeShellInstances](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DisposeShellInstances*) helper method that takes the recommended actions for you.

This method takes whichever [IShellObject](xref:ActiproSoftware.Shell.IShellObject) was set as the root shell folder and disposes it, while also resetting the control's [RootShellFolder](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.RootShellFolder) property back to `null`.  Additionally, it takes the current [DefaultShellService](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DefaultShellService) instance and disposes it, while also setting the [DefaultShellService](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DefaultShellService) property to `null`.  These steps should help ensure that unmanaged resources aren't memory-leaked.

> [!NOTE]
> The [DisposeShellInstances](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox.DisposeShellInstances*) method should be called whenever that control and any paired controls will no longer be used.
