---
title: "Working with Documents"
page-title: "Working with Documents - Docking & MDI Workspace and MDI Features"
order: 5
---
# Working with Documents

All documents can be easily managed via [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) properties and methods, regardless of whether tabbed or standard MDI is in use.  This allows your document-related code to remain the same regardless of the style of MDI in use.

## Creating a New Document

A document is just a term referring to a docking window (either a tool or document window) that is in the MDI area.

See the [Lifecycle and Docking Management](../docking-window-features/lifecycle-and-docking-management.md) topic for details on how to create a new docking window via code.

## Additional Document Window Properties

Document windows have a couple additional properties that aid in their user interface presentation.

The [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow).[FileName](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow.FileName) property should be set to the full file or URL path of the document content, if appropriate.  If this doesn't apply, leave the value `null`.

The [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow).[Description](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.Description) property should be set to a description of the document type, such as: `Text document` This value gets displayed on a [standard switcher](../docking-window-features/switchers.md).

## Opening a Document

Opening a document will make it visible in the UI, but it won't necessarily be selected or have focus.  To ensure a document is opened, selected, and focused, it must be activated instead.

A call to the [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow).[Open](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.Open*) method will open the document.  Alternatively, the [IsOpen](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsOpen) property can be set to `true`.

[DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) controls will always open in the MDI area. [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) controls on the other hand will only open in the MDI area if their [State](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.State) is `Document` before the open occurs.

## Activating a Document

Activating a document will open it, select it, and focus its content as well.  There is no need to open a document before activating it, because activating includes the open behavior.

A call to the [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow).[Activate](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.Activate*) method will activate the document.  An overload of this method allows you to ensure the document is selected but not focused.  Alternatively, the [IsActive](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.IsActive) property can be set to `true`.

As described above, to ensure that a [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) opens in the MDI area, make sure its [State](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.State) property is set to `Document` before activating it.

## Enumerating Open Documents

Use the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[Documents](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.Documents) collection to enumerate through the open documents.

Sort the collection by [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow).[LastActiveDateTime](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.LastActiveDateTime) to order them by which the documents were last activated.

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[HasDocuments](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.HasDocuments) property indicates whether there are currently any open documents.

## Primary Document

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[PrimaryDocument](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrimaryDocument) property gets the document that currently is the primary document in the dock site.  The primary document is the same as [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ActiveWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ActiveWindow) if the active window is a document.  When the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[ActiveWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ActiveWindow) is not a document, then the primary document is the open document that was last activated.

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[PrimaryDocumentChanged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrimaryDocumentChanged) event fires whenever the primary document changes.

## Working with Open Documents

There are numerous [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) methods that can be used to manipulate open documents.

| Member | Description |
|-----|-----|
| [ActivateNextDocument](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ActivateNextDocument*) Method | Activates the next document. |
| [ActivatePreviousDocument](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ActivatePreviousDocument*) Method | Activates the previous document. |
| [ActivatePrimaryDocument](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ActivatePrimaryDocument*) Method | Activates the primary (currently selected) document [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow), if there is one. |
| [CascadeDocuments](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.CascadeDocuments*) Method | Cascades all open documents, if using tabbed or standard MDI. |
| [CloseAllDocuments](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.CloseAllDocuments*) Method | Closes all open documents. |
| [ClosePrimaryDocument](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.ClosePrimaryDocument*) Method | Closes the primary (currently selected) document [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) if there is one. |
| [TileDocumentsHorizontally](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.TileDocumentsHorizontally*) Method | Tiles all open documents horizontally, if using tabbed or standard MDI. |
| [TileDocumentsVertically](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.TileDocumentsVertically*) Method | Tiles all open documents vertically, if using tabbed or standard MDI. |

## Closing a Document

A document can be closed by calling its [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow).[Close](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.Close*) method.

> [!NOTE]
> For document windows this will also destroy the window by default unless the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AreDocumentWindowsDestroyedOnClose](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AreDocumentWindowsDestroyedOnClose) property is set to `false`.

## Contextual Content

Tabbed MDI tabs and standard MDI window title bars can display custom contextual content such as buttons or status indicators.

See the [Contextual Content](../docking-window-features/contextual-content.md) topic for some more information.

## Read-Only Documents

Document windows have a special feature for automatically supporting the representation of read-only states.  When a document window should be marked as read-only in the user interface, set [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow).[IsReadOnly](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow.IsReadOnly) property to `true`.  This will display a small lock context icon next to the document title.
