---
title: "Overview"
page-title: "SyntaxEditor Input/Output Features"
order: 1
---
# Overview

In addition to the input/output features found in the [text framework](../../text-parsing/index.md) such as file load/save, exporting to HTML/RTF, etc., SyntaxEditor has a lot of other i/o features built-in.

## Document Swapping

Documents can be attached to zero or more SyntaxEditor controls at any given time, and alternate [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) instances can be swapped into a SyntaxEditor.

See the [Document Swapping](document-swapping.md) topic for more information.

## Clipboard Operations

SyntaxEditor makes use the Windows clipboard as a temporary repository for data for cut/copy/paste operations. SyntaxEditor places text on the clipboard using the `DataFormats.UnicodeText` and `DataFormats.Text` formats.

See the [Clipboard Operations](clipboard-operations.md) topic for more information.

## Drag and Drop

SyntaxEditor supports drag and drop operations within itself, as well as with external controls.

See the [Drag and Drop](drag-drop.md) topic for more information.

## Edit Actions

Edit actions are classes that implement [IEditAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditAction) and contain code to perform different simple tasks related to a [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).  They are effectively commands with can-execute and executed handlers that perform all the work.

See the [Edit Actions](edit-actions.md) topic for more information.

## Default Key Bindings

Many keyboard shortcuts are automatically bound to allow keyboard access to most of the built-in [edit actions](edit-actions.md).

See the [Default Key Bindings](default-key-bindings.md) topic for more information.

## Macro Recording and Playback

SyntaxEditor has full macro recording and playback capabilities.  It can record any [edit action](edit-actions.md) that is executed and can play back the set of edit actions at a later time.  During macro recording mode, mouse input to the editor is ignored.

See the [Macro Recording and Playback](macro-recording.md) topic for more information.

## Data Binding

SyntaxEditor has the capability of allowing two-way binding of its text to any other data source.  This feature is disabled by default for performance reasons, but can be enabled via a property setting.

See the [Data Binding](data-binding.md) topic for more information.

## Indent Providers (Auto-Indent)

Indent providers are classes that implement [IIndentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IIndentProvider) and contain code to perform automatic indentation when the Enter key is pressed.

See the [Indent Providers (Auto-Indent)](indent-providers.md) topic for more information.

## Delimiter Auto-Completion

Delimiter auto-completion occurs when the end user types a start delimiter and a related end delimiter is auto-inserted after the caret.  This improves code editing productivity since it means less overall typing is required to output the same code.

See the [Delimiter Auto-Completion](delimiter-auto-completion.md) topic for more information.
