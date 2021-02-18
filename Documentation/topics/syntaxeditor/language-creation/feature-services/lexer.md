---
title: "Lexer"
page-title: "Lexer - Feature Services - SyntaxEditor Language Creation Guide"
order: 2
---
# Lexer

A lexer is the lowest-level key piece of a syntax language.  It examines a stream of text and parses it into tokens that give text ranges meaning.  Tokens are then used to support many other language features such as classification.

## Creating a Lexer for a Language

The first step when creating a syntax language is to create its lexer.

Please see the text/parsing framework's [Lexing](../../text-parsing/lexing/index.md) series of topics for details on the various types of lexers and the benefits/drawbacks of each.

Once a lexer has been written, it needs to be associated with the language.

## Registering a Lexer with a Language

Associating a lexer with a language is done by registering the lexer instance as an [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) service on the language.

This code, called from a syntax language constructor, assigns a programmatic lexer named `MyLexer` to the language:

```csharp
public MySyntaxLanguage() {
	this.RegisterLexer(new MyLexer());
}
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterLexer](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterLexer*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
