---
title: "Code Fragments"
page-title: "Code Fragments - Parsing - SyntaxEditor Text/Parsing Framework"
order: 5
---
# Code Fragments

Documents support code fragment editing, whereby the editable document text is a code fragment that is to be surrounded by header and footer text for the purposes of parsing.  This is useful in situations where a language with parsing capabilities is used by a document, but the end user should only be able to edit the contents of a specific method, expression, etc.

## How It Works

To set the optional header and footer text for a document, use the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[SetHeaderAndFooterText](xref:ActiproSoftware.Text.ITextDocument.SetHeaderAndFooterText*) method.

When this method is called, a new [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) is created for the document with updated [HeaderText](xref:ActiproSoftware.Text.ITextSnapshot.HeaderText) and [FooterText](xref:ActiproSoftware.Text.ITextSnapshot.FooterText) properties.

Whenever the document's text is changed, a new parse request is made if the document's language has a parser service.  One component of this parse request is an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) that is used by the parser to read through the snapshot's text and parse it.  A method called [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[GetMergedBufferReader](xref:ActiproSoftware.Text.ITextSnapshot.GetMergedBufferReader*) is used to obtain an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) for this purpose.  If header/footer text are specified for the snapshot, they are included as part of that buffer reader.  The end result is that the parser thinks that the header/footer text, which surround the snapshot's text, are part of the snapshot's text, even though they really are not.  All of this happens automatically behind the scenes.

> [!IMPORTANT]
> If header text is used, the reader will start at a negative offset.  This is done so that the real snapshot text still aligns to offset `0`, and all AST nodes, syntax errors, etc. that are created by the parser will properly align with the offsets of the snapshot text.

## An Example

Say that you wanted an editor to only edit a method body, but you still want full IntelliPrompt features while editing.  This is an example of code fragment editing.

First you define the header and footer of the code fragment.  The header contains the namespace, class, and member declarations.  The footer contains the closing braces and any other members that should be accessible while editing the code fragment.  The header and footer are set via the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[SetHeaderAndFooterText](xref:ActiproSoftware.Text.ITextDocument.SetHeaderAndFooterText*) method.

This is an example of C# code fragment editing, where a class named `Foo` contains a method `Bar`, and the document's text will be editing the contents of that method.

```csharp
document.SetHeaderAndFooterText("class Foo { object Bar() {", "}}");
```

If the document's current snapshot has `return null;` in its text, the buffer reader sent to the parser for parsing will contain:

```
class Foo { object Bar() {return null;}}
```

The AST built for the document will fully contain the `Foo` class and `Bar` method, and any syntax errors will be reported correctly.  This allows advanced languages with resolvers and automated IntelliPrompt features to have their functionality work correctly, even in code fragment editing scenarios.

## Languages With Significant Line Terminators

Some languages such as Visual Basic treat line terminators as significant tokens in their parser grammars.  Namely, in VB, line terminators end statements.

Thus, in languages like this, it's very important to ensure there is a line terminator at the end of any header text specified, and probably also at the start of any footer text specified.  Since the header, snapshot, and footer text are simply appended together, this ensures that no syntax errors will be generated from missing line terminators.
