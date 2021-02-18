---
title: "Parser"
page-title: "Parser - Feature Services - SyntaxEditor Language Creation Guide"
order: 4
---
# Parser

A parser is something that performs syntax and/or semantic analysis on code and often returns parse data such as an abstract syntax tree (AST), list of syntax errors, symbol table, etc.  Syntax languages can have parsers registered with them that get called automatically via worker threads when text changes in code documents occur.

## Creating a Parser for a Language

Please see the text/parsing framework's [Parsing](../../text-parsing/parsing/index.md) series of topics for details on how to create a parser.

Once a parser has been written, it needs to be associated with the language.

## Registering a Parser with a Language

Associating a parser with a language is done by registering the parser instance as an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) service on the language.

This code, called from a syntax language constructor, assigns a parser named `MyParser` to the language:

```csharp
public MySyntaxLanguage() {
	this.RegisterParser(new MyParser());
}
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterParser](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterParser*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
