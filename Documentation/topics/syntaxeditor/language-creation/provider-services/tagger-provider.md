---
title: "Tagger Provider"
page-title: "Tagger Provider - Provider Services - SyntaxEditor Language Creation Guide"
order: 2
---
# Tagger Provider

Tagger provider services allow [taggers](../../text-parsing/tagging/taggers.md) to be created for a document or view.  Each language should provide a token tagger for documents using the language.  Token taggers read tokens from the language's lexer and marks them with a logical categorization such as `Identifier`, `Keyword`, `Comment`, or anything else.  These classifications can then be translated to highlighting styles, thereby achieving syntax highlighting in SyntaxEditor.

## What are Taggers?

Taggers are objects that can apply data (in the form of tags) over various text ranges.  This data could be anything from classifications, to squiggle tags, and more.  Since each tagger can only be associated with a single document or view, tagger provider services are used to create taggers for documents/views upon request.

See the [Taggers and Tagger Providers](../../text-parsing/tagging/taggers.md) topic for details on how taggers and providers work.

Taggers are used for many features of SyntaxEditor including being involved in:

- Classification
- Syntax highlighting
- Snapshot reading by token
- Squiggle lines
- Adornment layer managers

## Token Taggers and Syntax Highlighting

As mentioned above, token taggers are special classes that are used to provide classification and token tags.  When working with an editor like SyntaxEditor, the token tagger reads tags from the language's lexer and can return associated classifications, that are mapped to highlighting styles and used to achieve syntax highlighting.

Therefore if you need syntax highlighting, your language must have a [lexer](../feature-services/lexer.md) service, a [token tagger provider](../../text-parsing/tagging/taggers.md) service, and must have set up classification types to have mapped to highlighting styles in the ambient [highlighting style registry](../../user-interface/styles/highlighting-style-registries.md).

## Registering with a Syntax Language

As described in the [Taggers and Tagger Providers](../../text-parsing/tagging/taggers.md) topic, [CodeDocumentTaggerProvider<T>](xref:ActiproSoftware.Text.Tagging.Implementation.CodeDocumentTaggerProvider`1), [TextViewTaggerProvider<T>](xref:ActiproSoftware.Text.Tagging.Implementation.TextViewTaggerProvider`1), and [TokenTaggerProvider<T>](xref:ActiproSoftware.Text.Tagging.Implementation.TokenTaggerProvider`1) objects can be used as language services to provide taggers to any [tag aggregator](../../text-parsing/tagging/tag-aggregators.md) that request them.

This code shows how to register a document-oriented tagger provider language service that provides [ParseErrorTagger](xref:ActiproSoftware.Text.Tagging.Implementation.ParseErrorTagger) objects for documents that use the language.  Note that we are also passing a "singleton" key so that the tagger that is created for any document using the language is persisted in the document's [Properties](xref:ActiproSoftware.Text.ICodeDocument.Properties) dictionary while it is active.

```csharp
language.RegisterService(new CodeDocumentTaggerProvider<ParseErrorTagger>(typeof(ParseErrorTagger)));
```

This code shows how to register a document-oriented token tagger provider language service.  Token taggers automatically persist themselves in the document's [Properties](xref:ActiproSoftware.Text.ICodeDocument.Properties) dictionary while they are active.  Only one token tagger provider service should ever be registered on a language instance at a time.

```csharp
language.RegisterService(new TokenTaggerProvider<TokenTagger>());
```
