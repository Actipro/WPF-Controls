---
title: "Documentation Comment Auto-Completer"
page-title: "Documentation Comment Auto-Completer - Visual Basic Language - SyntaxEditor .NET Languages Add-on"
order: 8
---
# Documentation Comment Auto-Completer

A documentation comment auto-completer is an object that can attempt to auto-generate stub documentation comments for a type or member when `'''` is typed by the end user, can auto-complete end tags, and can insert continuation delimiters when pressing `Enter` in a documentation comment.

The [IDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.DotNet.IDocumentationCommentAutoCompleter) interface provides the base requirements for this functionality.  It is installed into a language as a "feature" language service.

The [VBDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBDocumentationCommentAutoCompleter) class is the default implementation of the [IDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.DotNet.IDocumentationCommentAutoCompleter) interface.

## Capabilities

Auto-generation of stub documentation comments is made when `'''` is typed by the end user as long as the [VBDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBDocumentationCommentAutoCompleter).[IsStubGenerationEnabled](xref:ActiproSoftware.Text.Languages.DotNet.Implementation.DocumentationCommentAutoCompleterBase.IsStubGenerationEnabled) property is set to `true`.  Depending on which type or member follows, the generated comments will contain summary, typeparam, param, and returns tags as appropriate.

When a `>` character is typed by the end user to end a documentation comment start tag, the appropriate end tag will be inserted as long as the [VBDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBDocumentationCommentAutoCompleter).[IsEndTagCompletionEnabled](xref:ActiproSoftware.Text.Languages.DotNet.Implementation.DocumentationCommentAutoCompleterBase.IsEndTagCompletionEnabled) property is set to `true`.

When the end user presses `Enter` while in a documentation comment, `'''` is added to the next line as long as the [VBDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBDocumentationCommentAutoCompleter).[IsNewLineGenerationEnabled](xref:ActiproSoftware.Text.Languages.DotNet.Implementation.DocumentationCommentAutoCompleterBase.IsNewLineGenerationEnabled) property is set to `true`.

## Registering with a Syntax Language

Any object that implements [IDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.DotNet.IDocumentationCommentAutoCompleter) can be associated with a syntax language by registering it as an [IDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.DotNet.IDocumentationCommentAutoCompleter) service on the language.

The [VBSyntaxLanguage](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage) class automatically registers a [VBDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBDocumentationCommentAutoCompleter) with itself when it is created, so normally documentation comment auto-completers never need to be set on an Visual Basic language unless a custom one is made.

This code creates a custom documentation comment auto-completer (defined in a make-believe `CustomDocumentationCommentAutoCompleter` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterDocumentationCommentAutoCompleter(new CustomDocumentationCommentAutoCompleter());
```

> [!NOTE]
> The [DotNetSyntaxLanguageExtensions](xref:ActiproSoftware.Text.Languages.DotNet.DotNetSyntaxLanguageExtensions).[RegisterDocumentationCommentAutoCompleter](xref:ActiproSoftware.Text.Languages.DotNet.DotNetSyntaxLanguageExtensions.RegisterDocumentationCommentAutoCompleter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text.Languages.DotNet` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [VBSyntaxLanguage](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
