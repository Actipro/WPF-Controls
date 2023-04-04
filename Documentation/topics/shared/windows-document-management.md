---
title: "Document Management"
page-title: "Document Management - Shared Library Reference"
order: 7
---
# Document Management

The [ActiproSoftware.Windows.DocumentManagement](xref:@ActiproUIRoot.DocumentManagement) namespace contains several classes that can be used for storing document references and maintaining recent document lists.

These classes are used by controls in our [Bars](../bars/index.md) ([RecentDocumentControl](../bars/ribbon-features/recent-documents.md)) and [Ribbon](../ribbon/index.md) ([RecentDocumentMenu](../ribbon/controls/miscellaneous/recentdocumentmenu.md)) products to manage recent documents within an application menu.

## Document References

The [IDocumentReference](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference) interface provides the base requirements for a document reference.  A document reference is simply a reference to a document that contains the `Uri` to the document, along with the document's display name, the `DateTime` that it was last opened, and whether it is a pinned recent document.

Some extended properties are also available for specifying icons (large/small) and a description of the reference.  These properties may be used by products such as Bars's [RecentDocumentControl](../bars/ribbon-features/recent-documents.md) when on a [Backstage](../bars/ribbon-features/backstage.md).

The [DocumentReference](xref:@ActiproUIRoot.DocumentManagement.DocumentReference) class is a simple implementation of the [IDocumentReference](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference) interface.

## Recent Document Management

The [RecentDocumentManager](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager) is a class that can manage a list of document references that point to recently-opened documents.

The manager class filters down the contained document references from its [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) collection into its [FilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.FilteredDocuments) collection.

The filtered collection will contain up to a maximum number of document references that you specify.  That collection will also be sorted in descending date/time of the last time the document was opened, by examining the [IDocumentReference](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference).[LastOpenedDateTime](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference.LastOpenedDateTime) property.  Further logic is applied so that if the document reference's [IsPinnedRecentDocument](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference.IsPinnedRecentDocument) property is `true`, it will have higher priority for staying on the list and not being filtered out, even if another more recently-opened document reference that wasn't pinned would normally have caused it to be filtered out.

The [RecentDocumentManager](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager) class has these important members:

| Member | Description |
|-----|-----|
| [Deserialize](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Deserialize*) Method | Deserializes the [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) collection from the data in an XML string.  The XML string must have been created by a previous call to [Serialize](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Serialize*). |
| [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) Property | Gets the collection of [IDocumentReference](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference) objects that are being managed.  Add/remove the document references to be managed via this collection. |
| [FilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.FilteredDocuments) Property | Gets the read-only collection of filtered documents that should be used for a recent documents list.  The collection sorts the contained documents by date/time and pinned states and returns up to [MaxFilteredDocumentCount](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.MaxFilteredDocumentCount) documents. |
| [GetDocumentReference](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.GetDocumentReference*) Method | Returns an existing [IDocumentReference](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference) from the [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) collection, if any, that matches the specified `Uri`. |
| [MaxDocumentCount](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.MaxDocumentCount) Property | Gets or sets the maximum number of documents to return in the [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) collection.  This collection is how many documents total are being tracked. |
| [MaxFilteredDocumentCount](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.MaxFilteredDocumentCount) Property | Gets or sets the maximum number of documents to return in the [FilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.FilteredDocuments) collection. |
| [NotifyDocumentOpened](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.NotifyDocumentOpened*) Method | Provides a helper method for easily updating an existing document reference's last-opened date/time.  If no existing document reference with the specified `Uri` exists, a new [DocumentReference](xref:@ActiproUIRoot.DocumentManagement.DocumentReference) is created.  There are three overloads for this method, which each take incrementally more information for populating a new [DocumentReference](xref:@ActiproUIRoot.DocumentManagement.DocumentReference) if needed. |
| [RebindFilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.RebindFilteredDocuments*) Method | Rebinds the [FilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.FilteredDocuments) collection.  The [FilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.FilteredDocuments) collection is automatically re-bound when the [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) collection is changed.  However if you update the last-opened date/time or pinned flag for an [IDocumentReference](xref:@ActiproUIRoot.DocumentManagement.IDocumentReference), you must call this method manually to rebind the [FilteredDocuments](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.FilteredDocuments) collection. |
| [Serialize](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Serialize*) Method | Serializes the [Documents](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Documents) collection to an XML string.  Use the [Deserialize](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Deserialize*) method to load the serialized data later. |

In many cases where a simple [DocumentReference](xref:@ActiproUIRoot.DocumentManagement.DocumentReference) is used to track a document or if you don't wish to track the reference at all and just let the [RecentDocumentManager](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager) do it completely for you, the use of the [NotifyDocumentOpened](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.NotifyDocumentOpened*) method is recommended.  That method provides a way to update the last-opened date time for a document in one line of code and will create a new reference as needed.

## Persisting Recent Document Lists

The recent document list managed by a [RecentDocumentManager](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager) can be easily persisted to an XML string.  This XML string in turn can be saved to a settings file, database, or any other sort of storage mechanism you use for keeping user data.

Call [RecentDocumentManager](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager).[Serialize](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Serialize*) to obtain the serialized data and later on, call [RecentDocumentManager](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager).[Deserialize](xref:@ActiproUIRoot.DocumentManagement.RecentDocumentManager.Deserialize*) to restore the serialized data.
