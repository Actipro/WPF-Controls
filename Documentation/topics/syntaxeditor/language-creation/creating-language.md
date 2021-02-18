---
title: "Creating an ISyntaxLanguage"
page-title: "Creating an ISyntaxLanguage - SyntaxEditor Language Creation Guide"
order: 3
---
# Creating an ISyntaxLanguage

The first step in building a language is the creation of a class that implements [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).

There are three main options for doing this, each of which are described below.

## Option 1: Building a SyntaxLanguage Externally

For languages that only require a couple simple property settings, you may wish to construct the language on the fly.  The [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) class implements the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) and can be used to accomplish this.

This code creates a language that uses a [line commenter](feature-services/line-commenter.md) which can comment and uncomment lines with the text `//`.

```csharp
SyntaxLanguage language = new SyntaxLanguage("Dynamic");
// NOTE: Register lexer, parser, or other language services here
language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterLineCommenter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterLineCommenter*) method in the code snippet above is a helper extension method that get added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

Since this language only has a couple options set, there is nothing wrong with defining it this way and [using it](using-language.md).  Generally, it is better design to use option 2 (below) for more complex languages.

## Option 2: Creating a Custom Class that Inherits SyntaxLanguage

Many times language implementations are more complex that the sample illustrated in the previous option.  It could be a case of additional configuration/processing needed or you may simply want to have your language completely encapsulated with a dedicated class.

> [!NOTE]
> The [Language Designer](../language-designer-tool/index.md) tool allows you to quickly configure information about your language.  It can generate a lot of the C#/VB code needed to have a dedicated syntax language class and related features.  Use of that application is the fastest way to get started making a syntax language.

In any event, if those scenarios apply to you, making a custom language class that inherits [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) is what you'd want to do. [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) implements [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) and is a perfect base class for any custom language implementation.

This code defines a custom language class that inherits [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage):

```csharp
public class MyCustomSyntaxLanguage : SyntaxLanguage {
	public MyCustomSyntaxLanguage() : base("My Custom Language") {}
}
```

The next step would be to add code into your constructor that registers all the [language services](feature-services/index.md) you wish for you language to provide (lexer, token tagger, line commenter, etc.).

```csharp
public class MyCustomSyntaxLanguage : SyntaxLanguage {
	public MyCustomSyntaxLanguage() : base("My Custom Language") {
		this.RegisterLexer(new MyCustomLexer());
		this.RegisterService(new TokenTaggerProvider<MyCustomTokenTagger>());
		this.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
	}
}
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterLexer](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterLexer*) and [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterLineCommenter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterLineCommenter*) methods in the code snippet above are helper extension methods that get added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

After that is complete, the language can be completely instantiated in one line of code:

```csharp
MyCustomSyntaxLanguage language = new MyCustomSyntaxLanguage();
```

## Option 3: Loading a Language Definition (.langdef File)

The [Language Designer](../language-designer-tool/index.md) application can be used to optionally create files with .langdef file extensions, which are language definitions.  These language definition files can be distributed with your application, often in file form or as an embedded resource.  They contain information about a language and can be loaded at run-time to automatically create and initialize a language.

Language definitions can be used to either:

- Create a new [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) from scratch that is initialized with data from the language definition, which is focus of this Option 3.

- Initialize an existing [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage), which means it can be used in both Option 1 and 2 scenarios listed above to supply the lexer, etc. instead of manually registering code-based ones

See the [Loading a Language Definition (.langdef File)](loading-lang-def.md) topic for details on how to load one of these files at run-time.
