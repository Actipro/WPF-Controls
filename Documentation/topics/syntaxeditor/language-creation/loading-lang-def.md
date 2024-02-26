---
title: "Loading a Language Definition (.langdef File)"
page-title: "Loading a Language Definition (.langdef File) - SyntaxEditor Language Creation Guide"
order: 4
---
# Loading a Language Definition (.langdef File)

The [Language Designer](../language-designer-tool/index.md) application can be used to optionally create files with `.langdef` file extensions, which are language definitions.  These language definition files can be distributed with your application, often in file form or as an embedded resource.  They contain information about a language and can be loaded at run-time to automatically create and initialize a language.

Read the Language Designer's [Getting Started](../language-designer-tool/getting-started.md) topic for more information on language definitions and when to use them.

Language definition files may be loaded directly from a file, or from a `Stream`.

## Option 1: Loading from a File

The benefit of distributing the language definition as a file with your application is that end users can modify the language definition if necessary.

This code creates a syntax language object based on an *ECMAScript.langdef* file:

```csharp
var serializer = new SyntaxLanguageDefinitionSerializer();
string path = @"C:\ECMAScript.langdef";
ISyntaxLanguage language = serializer.LoadFromFile(path);
```

Now that an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) object has been created based on the language definition, it [can be used](using-language.md).

## Option 2: Loading from a Stream

A language definition can also be distributed in any other form, such as an embedded resource, or in a database.  Once you have a `Stream` pointing to the language definition data, it can be loaded.

This code creates a syntax language object based on a `Stream` in the variable `stream`:

```csharp
var serializer = new SyntaxLanguageDefinitionSerializer();
ISyntaxLanguage language = serializer.LoadFromStream(stream);
```

Now that an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) object has been created based on the language definition, it [can be used](using-language.md).

## Initializing an Existing ISyntaxLanguage Instead

The two loading methods described above load a language definition file and use it to create a new [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  However, there could be scenarios where you already have an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) created but wish to initialize it with language definition data.  An example of this is that maybe you wish to ship *\*.langdef* files with your app so that end users can customize them.  However, you have several additional language services (such as a parser) that need to get used on this language as well.  In this scenario, you'd want the best of both worlds: to load the lexer, token tagger, and classification types from the *\*.langdef* file and register your additional services for use as well.

This code snippet shows how a custom language class can be created that both initializes itself based on a *.langdef* file and also adds additional services.

```csharp
public class EcmaScriptSyntaxLanguage : SyntaxLanguage {
	public EcmaScriptSyntaxLanguage() : base("ECMAScript") {
		// Use a SyntaxLanguageDefinitionSerializer to load a lexer, token tagger, etc. from a .langdef file
		new SyntaxLanguageDefinitionSerializer().InitializeFromFile("C:\ECMAScript.langdef");

		// Register additional services
		this.RegisterParser(new EcmaScriptParser());
		this.RegisterService(new CodeDocumentTaggerProvider<ParseErrorTagger>(typeof(ParseErrorTagger)));
	}
}
```

The language can then be instantiated like this:

```csharp
var language = new EcmaScriptSyntaxLanguage();
```

The [SyntaxLanguageDefinitionSerializer](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer) class has:

@if (wpf winforms) {
- An [InitializeFromFile](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.InitializeFromFile*) method for initializing an existing syntax language from a file
}

- An [InitializeFromStream](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.InitializeFromStream*) method for initializing an existing syntax language from a `Stream`

## Using Built-in Classification Types

When a language definition is loaded, the classification types in the *.langdef* are registered with their associated [highlighting styles](../user-interface/styles/highlighting-styles.md).  If a classification type with the same key has not already been registered, the styles defined in the *.langdef* will be used.  If a classification type was already defined, the previously registered styles will be used instead.

There are several built-in classification types (e.g., [Comment](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider.Comment) or [String](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider.String)) available from the [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider) class that are pre-defined with common styles supporting light and [dark themes](../user-interface/styles/dark-themes.md), but these classification types are only used, by default, if they were previously registered.  When loading a *.langdef*, you can instruct the serializer to use built-in classification types, if available, for any of the classification types defined by the language even if that classification type was not previously registered.  Simply set the [UseBuiltInClassificiationTypes](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.UseBuiltInClassificiationTypes) property to `true` and the built-in classification types and their styles will be preferred over those defined in the *.langdef* file.

> [!TIP]
> Language definition files created before version 24.1 will not have the `DarkStyle` data used to define foreground and background colors for use with a dark theme, so using built-in classification types will allow older files to use pre-defined dark colors for certain classification types and improve their appearance in a dark theme.

The following code demonstrates loading a language from a file using built-in classification types, when available:

```csharp
var serializer = new SyntaxLanguageDefinitionSerializer();
serializer.UseBuiltInClassificiationTypes = true;
ISyntaxLanguage language = serializer.LoadFromFile(@"C:\MyFile.langdef");
```

> [!IMPORTANT]
> To use built-in classification types, make sure the keys used in the *.langdef* file match those defined by [ClassificationTypeKeys](xref:ActiproSoftware.Text.ClassificationTypeKeys).