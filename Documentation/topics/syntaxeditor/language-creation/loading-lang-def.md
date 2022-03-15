---
title: "Loading a Language Definition (.langdef File)"
page-title: "Loading a Language Definition (.langdef File) - SyntaxEditor Language Creation Guide"
order: 4
---
# Loading a Language Definition (.langdef File)

The [Language Designer](../language-designer-tool/index.md) application can be used to optionally create files with .langdef file extensions, which are language definitions.  These language definition files can be distributed with your application, often in file form or as an embedded resource.  They contain information about a language and can be loaded at run-time to automatically create and initialize a language.

Read the Language Designer's [Getting Started](../language-designer-tool/getting-started.md) topic for more information on language definitions and when to use them.

Language definition files may be loaded directly from a file, or from a `Stream`.

## Option 1: Loading from a File

The benefit of distributing the language definition as a file with your application is that end users can modify the language definition if necessary.

This code creates a syntax language object based on a `ECMAScript.langdef` file:

```csharp
SyntaxLanguageDefinitionSerializer serializer = new SyntaxLanguageDefinitionSerializer();
string path = @"C:\ECMAScript.langdef";
ISyntaxLanguage language = serializer.LoadFromFile(path);
```

Now that an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) object has been created based on the language definition, it [can be used](using-language.md).

## Option 2: Loading from a Stream

A language definition can also be distributed in any other form, such as an embedded resource, or in a database.  Once you have a `Stream` pointing to the language definition data, it can be loaded.

This code creates a syntax language object based on a `Stream` in the variable `stream`:

```csharp
SyntaxLanguageDefinitionSerializer serializer = new SyntaxLanguageDefinitionSerializer();
ISyntaxLanguage language = serializer.LoadFromStream(stream);
```

Now that an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) object has been created based on the language definition, it [can be used](using-language.md).

## Initializing an Existing ISyntaxLanguage Instead

The two loading methods described above load a language definition file and use it to create a new [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  However there could be scenarios where you already have an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) created but wish to initialize it with language definition data.  An example of this is that maybe you wish to ship .langdef files with your app so that end users can customize them.  However you have several additional language services (such as a parser) that need to get used on this language as well.  In this scenario, you'd want the best of both worlds: to load the lexer, token tagger, and classification types from the .langdef and register your additional services for use as well.

This code snippet shows how a custom language class can be created that both initializes itself based on a .langdef file and also adds additional services.

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
EcmaScriptSyntaxLanguage language = new EcmaScriptSyntaxLanguage();
```

The [SyntaxLanguageDefinitionSerializer](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer) class has:

@if (wpf winforms) {
- An [InitializeFromFile](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.InitializeFromFile*) method for initializing an existing syntax language from a file, and 
}

- An [InitializeFromStream](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.InitializeFromStream*) method for initializing an existing syntax language from a `Stream`, and
