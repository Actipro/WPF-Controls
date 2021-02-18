---
title: "Using a Language"
page-title: "Using a Language - SyntaxEditor Language Creation Guide"
order: 9
---
# Using a Language

Once your syntax language has been configured properly, it's ready for use.

## Using with an ICodeDocument or SyntaxEditor

### Using with an ICodeDocument

The [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) interface designates the requirements for a document that can by parsed by a syntax language.  It defines a [Language](xref:ActiproSoftware.Text.ICodeDocument.Language) property that accepts any [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) instance.  Therefore, to have a document use a syntax language, simply assign it to that property.

This code creates a new [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) (which inherits [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument)) and assigns an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that is already defined in the `language` variable:

```csharp
IEditorDocument document = new EditorDocument();
document.Language = language;
```

The [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[LanguageChanged](xref:ActiproSoftware.Text.ICodeDocument.LanguageChanged) event fires whenever the document's [Language](xref:ActiproSoftware.Text.ICodeDocument.Language) property changes.  Its event arguments pass the old and newly-assigned languages.

### Using with a SyntaxEditor

Since [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) is the property type for the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[Document](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.Document) property, assigning an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) to be used for a [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor) control is done the same way.

In that scenario, the SyntaxEditor's document is assigned the language like this:

```csharp
editor.Document.Language = language;
```

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[DocumentLanguageChanged](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.DocumentLanguageChanged) event wraps the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[LanguageChanged](xref:ActiproSoftware.Text.ICodeDocument.LanguageChanged) event and is an easy way to be notified whenever a new language is assigned to the document currently being edited in a SyntaxEditor.

## Packaging Options

There are a few options for packaging the custom languages you've created with your application.  It is completely up to you as to how you package things, since any of these options work fine.

### Included in Application Assembly

The easiest way is to include a custom language and its related classes right in your main application assembly.

### Included in Library Assembly

For languages that tend to be more complex and use a lot of optional features, it's probably better design to separate them out into a single library assembly.  This allows you to maintain your language in a project that is independent of your UI project.

### Included in Library Assemblies with Text/UI Separation

This option is similar to the previous one where the language is in its own assembly, however in this option the language and its classes are split into two assemblies: one for text/parsing and one for UI.  This option is how we deploy our optional language add-ons.  It is generally only used in scenarios where you plan on using the same text/parsing code across multiple platforms (WPF, WinForms, etc.).

The first assembly contains only the core text and parsing related classes.  This keeps it truly platform independent since there are no references to any UI platforms (WPF, WinForms, etc.) in the code.

The second assembly is platform dependent and includes code specifically for working with SyntaxEditor for WPF, etc.  Language functionality that interacts with IntelliPrompt would be included here.
