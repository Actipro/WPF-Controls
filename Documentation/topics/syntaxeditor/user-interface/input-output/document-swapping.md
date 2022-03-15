---
title: "Document Swapping"
page-title: "Document Swapping - SyntaxEditor Input/Output Features"
order: 2
---
# Document Swapping

Documents can be attached to zero or more SyntaxEditor controls at any given time, and alternate [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) instances can be swapped into a SyntaxEditor.

## UI and Text Separation

The SyntaxEditor product has been specifically designed such that the [text/parsing framework](../../text-parsing/index.md) functions completely independently from the user interface layer.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control is the root object of the product's user interface layer.  It contains multiple editor views in which text can be edited.

The [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) interface provides the requirements for a document that can be edited within a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control.  Editor documents are the root of the text/parsing framework hierarchy and specify an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that is used to parse the code in the document.

## Swapping Document Instances

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control is always editing a single [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) instance at a time.  The [Document](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.Document) property gets and sets which [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) is currently being edited.

A second [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) could be opened and swapped in place of the SyntaxEditor's current document, by setting the [Document](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.Document) property to the new document instance.  When this occurs, the SyntaxEditor control detaches from the old document, attaches to the new document, and updates its views to display the text in the new document.

So effectively you could have an application that has a single SyntaxEditor control instance, but a lot of [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) instances that are loaded in memory.  When the end user wishes to edit a particular document, you would swap that document into the editor.  This saves on overall memory since mutiple editor control instances are not required.
