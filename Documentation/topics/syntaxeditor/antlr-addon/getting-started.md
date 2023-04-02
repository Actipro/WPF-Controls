---
title: "Getting Started"
page-title: "Getting Started - SyntaxEditor ANTLR Add-on"
order: 2
---
# Getting Started

The ANTLR Add-on makes it possible in just a few lines of code to create a syntax language that can automatically call an ANTLR parser whenever document text is modified and asynchronously return its AST result to the document.  This topic walks through the basic concepts involved, the requirements, and includes a complete sample of how to pull everything together.

> [!NOTE]
> Due to this add-on being deprecated, the sample project that shows off this add-on is no longer installed.  Please contact our support team if you have a need to download it.

## Basic Concepts

The ANTLR Add-on exposes two main types that allow you to easily support automated calls to ANTLR for a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control.  This section talks about the types.  See the sample section later in this topic for a walkthrough of creating an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that uses the classes.

### About Core ANTLR Lexers/Parsers

Both the Actipro text/parsing framework and ANTLR use the industry standard terms "lexer" and "parser", where a lexer tokenizes text and a parser performs syntax/semantic parsing on the text, generally producing an AST (abstract syntax tree) result.

Unfortunately since ANTLR doesn't support incremental lexing, we can't harness ANTLR to drive syntax highlighting features in SyntaxEditor.  Alternative options are described in sections below.  We can still use ANTLR to perform syntax/semantic parsing though.

ANTLR grammars, generally via the ANTLRWorks application, can be edited and used to generate what for our purposes will be called "core" ANTLR lexer and parser classes.  Since these classes are external to the Actipro text/parsing framework, we need to "wrap" so that they can be called.  That is exactly what this add-on helps you to do.

### The ANTLR Parser

The [AntlrParser](xref:ActiproSoftware.Text.Parsing.Implementation.AntlrParser) class by Actipro implements an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) that constructs a core ANTLR lexer and parser behind the scenes and uses it to perform parsing in any document using a language that has the parser registered as a service.

The [AntlrParser](xref:ActiproSoftware.Text.Parsing.Implementation.AntlrParser) constructor has four parameters that enable it to wrap the core ANTLR lexer/parser.  The first parameter is string-based key that idenfities the parser, generally the same as the language's key.  The second parameter is a `Type` indicating the core ANTLR lexer type.  The third parameter is a `Type` indicating the core ANTLR parser type.  The add-on uses reflection to look for constructors on those types so that it can instantiate them as needed.  Again these two types are what is output from ANTLR.  The final parameter is a string indicating the name of the root rule in your ANTLR grammar to execute on the parser.  For instance, if the root rule was called `program` then you would pass that as the name, since ANTLR would have generated a method named `program` on its parser class.

Once the [AntlrParser](xref:ActiproSoftware.Text.Parsing.Implementation.AntlrParser) has been instantiated, it is [registered](../language-creation/feature-services/parser.md) with your [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  This allows the syntax language to use the `AntlrParser` to call ANTLR for parsing.

### The ANTLR Parse Data

Since parsing is performed asynchronously on a worker thread to prevent the UI thread from blocking (assuming you have created an ambient parser request dispatcher per the steps in the [Parse Requests and Dispatchers](../text-parsing/parsing/parse-requests-and-dispatchers.md) topic), the result of the parse is returned upon completion to the document's [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property.

The [AntlrParser](xref:ActiproSoftware.Text.Parsing.Implementation.AntlrParser) implementation returns an object of type [IAntlrParseData](xref:ActiproSoftware.Text.Parsing.IAntlrParseData) to this property. [IAntlrParseData](xref:ActiproSoftware.Text.Parsing.IAntlrParseData) defines a property that contains the root AST node returned by the core ANTLR parser, which is of the ANTLR type `ITree`.

## Requirements

### Application Development Language

Since ANTLR only has a C# target option and not a VB target output option, this add-on is only useful with applications being developed in C#.  However if you are using VB and still wish to use the add-on, you could target the output of the core ANTLR lexer/parser to C#, put the classes in a standalone library .dll and reference that library from your VB application project.

To enable C# target output by ANTLR, set the `language=CSharp3;` option in your ANTLR grammar file.

### Assemblies Required for the Add-on

The ANTLR Add-on references and thus requires one external assembly not written by Actipro, *Antlr3.Runtime.dll*, which interfaces with the ANTLR framework.  This assembly is freely available on the [ANTLR C# runtime download page](http://www.antlr.org/download/CSharp).

The following list indicates the assemblies that are required by the add-on.

| Assembly | Required | Author | Distributed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Shared.Wpf.dll | Yes | Actipro | WPF Studio | Core framework for all Actipro WPF controls |
| ActiproSoftware.Text.Wpf.dll | Yes | Actipro | WPF Studio | Core text/parsing framework for SyntaxEditor |
| ActiproSoftware.SyntaxEditor.Wpf.dll | Yes | Actipro | WPF Studio | SyntaxEditor for WPF control |
| ActiproSoftware.Text.Addons.Antlr.Wpf.dll | Yes | Actipro | WPF Studio | Integrates ANTLR-based parsers with syntax languages |
| Antlr3.Runtime.dll | Yes | Terence Parr / Sam Harwell | ANTLR Web Site | Contains the framework used to work with ANTLR parsers from .NET applications |

## Sample: Creating an ISyntaxLanguage that Uses an ANTLR Parser

This sample will show how to create an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that does not have syntax highlighting (see section below for more info on this) but can automatically call ANTLR asynchronously when text changes occur on documents using the language.

In the sample we will assume the language is for a simple calculator language called "Calc".  We will assume that ANTLR generated a class named `AntlrCalcLexer` that is its core lexer and a class named `AntlrCalcParser` that is its core parser.  Also we will assume that the root rule in the core ANTLR parser that should be called is named `expr`.

### Creating an Ambient Parse Request Dispatcher

The first step when working with parsers is to ensure that the parsing operations can be executed in a separate worker thread so that they don't block the UI thread.  The Actipro text/parsing framework has features to accommodate this.

Follow the instructions in the [Parse Requests and Dispatchers](../text-parsing/parsing/parse-requests-and-dispatchers.md) topic to learn how to enable asynchronous parsing by setting a property in your application startup code.

### Create the AntlrParser

Since this add-on relies on an `AntlrParser` to make the appropriate calls to the core ANTLR lexer/parser, we first need to create an instance of it.

This code creates the `AntlrParser`, passing it a string key that identifies the parser's language, the `Type` of core ANTLR lexer and parser, and the root rule name in the core ANTLR parser to execute:

```csharp
AntlrParser parser = new AntlrParser("Calc", typeof(AntlrCalcLexer), typeof(AntlrCalcParser), "expr");
```

### Create and Assemble the Syntax Language

Now we need to create the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) and assemble everything together.  The [AntlrParser](xref:ActiproSoftware.Text.Parsing.Implementation.AntlrParser) created above will be registered with the language.

This code creates and assembles the syntax language:

```csharp
SyntaxLanguage language = new SyntaxLanguage("Calc");
language.RegisterParser(parser);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterParser](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterParser*) method in the code snippet above is a helper extension method that get added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

### Attach to a SyntaxEditor's Document to Provide ANTLR Parsing

The final step is to use the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that was created above with a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).

This code applies the language that was created to a document being edited by a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) declared in the variable `editor`:

```csharp
editor.Document.Language = language;
```

That's all there is to it.  In a few lines of code we have SyntaxEditor automatically calling an ANTLR parser whenever its document text changes.  The parse data result in the form of an [IAntlrParseData](xref:ActiproSoftware.Text.Parsing.IAntlrParseData) object is passed back asynchronously to the document's [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property when parsing is complete.  This triggers the document's [ParseDataChanged](xref:ActiproSoftware.Text.ICodeDocument.ParseDataChanged) event.

## Adding Syntax Highlighting

As mentioned above, the ANTLR lexer doesn't support incremental lexing, meaning that we can't have it pick up at a certain point and just give us a range of tokens.  These features are necessary to get syntax highlighting working within a SyntaxEditor.  The previous sample shows how to intergrate with ANTLR parsers to provide parse results for a document as it is changed.  But how can we get syntax highlighting working for the language too?

There are two main options.  Both involve creating a [lexer](../text-parsing/lexing/index.md) for your language, either a [programmatic lexer](../text-parsing/lexing/programmatic-lexers.md) or a [dynamic lexer](../text-parsing/lexing/dynamic-lexers.md).  Once a [lexer has been registered with the language](../language-creation/feature-services/lexer.md), a [token tagger provider must be registered](../language-creation/provider-services/tagger-provider.md) as well so that the tokens read from the lexer can be converted into styles, and syntax highlighting can be achieved.

To sum up, you'd need to implement syntax highlighting the normal way using a custom lexer and token tagger.  Then harness the ANTLR parsers you've generated to do the AST building.
