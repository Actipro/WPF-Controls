---
title: "Getting Started"
page-title: "Getting Started - SyntaxEditor Irony Add-on"
order: 2
---
# Getting Started

The Irony Add-on makes it possible in just a few lines of code to create a syntax language that can automatically call an Irony parser whenever document text is modified and asynchronously return its parse tree or AST result to the document.  This topic walks through the basic concepts involved, the requirements, and includes a complete sample of how to pull everything together.

> [!NOTE]
> Due to this add-on being deprecated, the sample project that shows off this add-on is no longer installed.  Please contact our support team if you have a need to download it.

## Basic Concepts

The Irony Add-on exposes two main types that allow you to easily support automated calls to Irony for a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control.  This section talks about the types.  See the sample section later in this topic for a walkthrough of creating an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that uses the classes.

### About Core Irony Lexers/Parsers

Both the Actipro text/parsing framework and Irony use the industry standard terms *lexer* and *parser*, where a lexer tokenizes text and a parser performs syntax/semantic parsing on the text, generally producing a parse tree or an abstract syntax tree (AST) result.

Unfortunately, since Irony doesn't support incremental lexing, we can't harness Irony to drive syntax highlighting features in SyntaxEditor.  Alternative options are described in the sections below.  We can still use Irony to perform syntax/semantic parsing though.

Irony grammar classes and some framework code need to be "wrapped" so that they can be used for parsing.  That is exactly what this add-on helps you to do.

### The Irony Parser

The [IronyParser](xref:ActiproSoftware.Text.Parsing.Implementation.IronyParser) class by Actipro implements an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) that constructs an Irony lexer and parser behind the scenes and uses it to perform parsing in any document using a language that has the parser registered as a service.

The [IronyParser](xref:ActiproSoftware.Text.Parsing.Implementation.IronyParser) constructor has one parameter, an Irony Grammar.

Once the [IronyParser](xref:ActiproSoftware.Text.Parsing.Implementation.IronyParser) has been instantiated, it is [registered](../language-creation/feature-services/parser.md) with your [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  This allows the syntax language to use the `IronyParser` to call Irony for parsing.

### The Irony Parse Data

Since parsing is performed asynchronously on a worker thread to prevent the UI thread from blocking (assuming you have created an ambient parser request dispatcher per the steps in the [Parse Requests and Dispatchers](../text-parsing/parsing/parse-requests-and-dispatchers.md) topic), the result of the parse is returned upon completion to the document's [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property.

The [IronyParser](xref:ActiproSoftware.Text.Parsing.Implementation.IronyParser) implementation returns an object of type [IIronyParseData](xref:ActiproSoftware.Text.Parsing.IIronyParseData) to this property. [IIronyParseData](xref:ActiproSoftware.Text.Parsing.IIronyParseData) defines a property that contains the parse tree returned by the Irony parser, which is of the Irony type `ParseTree`.

## Assembly Requirements

The Irony Add-on references and thus requires one external assembly not written by Actipro, *Irony.dll*.  This assembly is freely available on the [Irony GitHub page](https://github.com/IronyProject/Irony).

The following list indicates the assemblies that are required by the add-on.

| Assembly | Required | Author | Distributed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Shared.Wpf.dll | Yes | Actipro | WPF Studio | Core framework for all Actipro WPF controls |
| ActiproSoftware.Text.Wpf.dll | Yes | Actipro | WPF Studio | Core text/parsing framework for SyntaxEditor |
| ActiproSoftware.SyntaxEditor.Wpf.dll | Yes | Actipro | WPF Studio | SyntaxEditor for WPF control |
| ActiproSoftware.Text.Addons.Irony.Wpf.dll | Yes | Actipro | WPF Studio | Integrates Irony-based parsers with syntax languages |
| Irony.dll | Yes | Roman Ivantsov | Irony Web Site | Contains the framework used to work with Irony parsers from .NET applications |

## Sample: Creating an ISyntaxLanguage that Uses an Irony Parser

This sample will show how to create an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that does not have syntax highlighting (see section below for more info on this) but can automatically call Irony asynchronously when text changes occur on documents using the language.

In the sample we use an example language called "SimpleLanguage". The grammar class is named `SimpleLanguageGrammar`.

### Creating an Ambient Parse Request Dispatcher

The first step when working with parsers is to ensure that the parsing operations can be executed in a separate worker thread so that they don't block the UI thread.  The Actipro text/parsing framework has features to accommodate this.

Follow the instructions in the [Parse Requests and Dispatchers](../text-parsing/parsing/parse-requests-and-dispatchers.md) topic to learn how to enable asynchronous parsing by setting a property in your application startup code.

### Create the IronyParser

Since this add-on relies on an `IronyParser` to make the appropriate calls to the Irony lexer/parser, we first need to create an instance of it.

This code creates the `IronyParser`, passing it an Irony grammar object:

```csharp
IronyParser parser = new IronyParser(new SimpleLanguageGrammar());
```

### Create and Assemble the Syntax Language

Now we need to create the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) and assemble everything together.  The [IronyParser](xref:ActiproSoftware.Text.Parsing.Implementation.IronyParser) created above will be registered with the language.

This code creates and assembles the syntax language:

```csharp
SyntaxLanguage language = new SyntaxLanguage("SimpleLanguage");
language.RegisterParser(parser);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterParser](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterParser*) method in the code snippet above is a helper extension method that get added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

### Attach to a SyntaxEditor's Document to Provide Irony Parsing

The final step is to use the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that was created above with a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).

This code applies the language that was created to a document being edited by a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) declared in the variable `editor`:

```csharp
editor.Document.Language = language;
```

That's all there is to it.  In a few lines of code, we have SyntaxEditor automatically calling an Irony parser whenever its document text changes.  The parse data result in the form of an [IIronyParseData](xref:ActiproSoftware.Text.Parsing.IIronyParseData) object is passed back asynchronously to the document's [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property when parsing is complete.  This triggers the document's [ParseDataChanged](xref:ActiproSoftware.Text.ICodeDocument.ParseDataChanged) event.

## Adding Syntax Highlighting

As mentioned above, the Irony lexer doesn't support incremental lexing, meaning that we can't have it pick up at a certain point and just give us a range of tokens.  These features are necessary to get syntax highlighting working within a SyntaxEditor.  The previous sample shows how to integrate with Irony parsers to provide parse results for a document as it is changed.  But how can we get syntax highlighting working for the language too?

There are two main options.  Both involve creating a [lexer](../text-parsing/lexing/index.md) for your language, either a [programmatic lexer](../text-parsing/lexing/programmatic-lexers.md) or a [dynamic lexer](../text-parsing/lexing/dynamic-lexers.md).  Once a [lexer has been registered with the language](../language-creation/feature-services/lexer.md), a [token tagger provider must be registered](../language-creation/provider-services/tagger-provider.md) as well so that the tokens read from the lexer can be converted into styles, and syntax highlighting can be achieved.

To sum up, you'd need to implement syntax highlighting the normal way using a custom lexer and token tagger.  Then harness the Irony parser to do the parse tree and/or AST building.

## Error Reporting

The Irony Add-on produces an [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData), which can be retrieved from the editor document's [ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property.  This data can be used to populate an output window, or if you have a [ParseErrorTagger](../text-parsing/tagging/taggers.md) registered with the language, the error(s) will be marked in the editor.
