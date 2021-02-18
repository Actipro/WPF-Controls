---
title: "Code Block Selection"
page-title: "Code Block Selection - SyntaxEditor Editor View Features"
order: 13
---
# Code Block Selection

SyntaxEditor supports code block selection expansion and contraction, meaning that the view's selection can be expanded to include containing code blocks and contracted all the way back down to the caret as appropriate.

For instance, in C# the first time you expand the selection, it may select the containing identifier.  By expanding it again, it may select the containing expression, then the containing statement, then the containing method.  And so on up the compilation unit.

By contracting the selection, it goes back and selects the previously selected block.  Contracting can occur recursively to go back to the original selection.

## Activating Code Block Selection

Code block selection is enabled on SyntaxEditor by default, and can be executed via the default `Ctrl+Shift+Num+` (expand) and `Ctrl+Shift+Num-` (contract) [key bindings](../input-output/default-key-bindings.md).

However note that code block selection only functions if the required language service ([ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) interface) is registered.

## Finder Logic

The [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) interface has a single [FindContaining](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder.FindContaining*) method on it that accepts a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) (which is the current selection range in an editor) and returns a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange), which should be the text range of the containing code block.  The code block could be determined by examining an AST or doing simple [text/token scanning](../../text-parsing/core-text/scanning-text.md).

## Registering a Code Block Finder with a Syntax Language

An [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) can be associated with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) by registering it with the language:

```csharp
language.RegisterService<ICodeBlockFinder>(myCodeBlockFinder);
```

Registering a code block finder with a language is optional, but recommended.

## LLParseDataCodeBlockFinder Implementation

The built-in [LLParseDataCodeBlockFinder](xref:ActiproSoftware.Text.Analysis.Implementation.LLParseDataCodeBlockFinder) class, which is part of the [LL(*) Parser Framework](../../ll-parser-framework/index.md), implements [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) and can be used as a code block finder service on any language that uses a grammar-based parser from our framework.

It is designed to examine the resulting AST (abstract syntax tree) that was constructed from the parser and walk up the tree as calls are made to the code block finder.

The class has a special [Filter](xref:ActiproSoftware.Text.Analysis.Implementation.LLParseDataCodeBlockFinder.Filter) property that can optionally be assigned to a predicate delegate.  The delegate is passed an [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) that the finder is currently examining and considering returning.  If the AST node should be used, return `true`, otherwise return `false` and the finder will examine the node's parent instead.  This mechanism allows for certain AST nodes to be skipped over.

## Programmatic Execution

The [IEditorViewSelection](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewSelection) interface defines two methods ([CodeBlockExpand](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewSelection.CodeBlockExpand*) and [CodeBlockContract](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewSelection.CodeBlockContract*)) that can be used to programmatically invoke code block selection functionality.

Again, these methods only function if an [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) service is registered on the current language.
