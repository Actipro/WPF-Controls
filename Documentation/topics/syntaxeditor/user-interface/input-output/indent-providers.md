---
title: "Indent Providers (Auto-Indent)"
page-title: "Indent Providers (Auto-Indent) - SyntaxEditor Input/Output Features"
order: 9
---
# Indent Providers (Auto-Indent)

Indent providers are classes that implement [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) and contain code to perform automatic indentation when the <kbd>Enter</kbd> key is pressed.

## Indent Mode

SyntaxEditor has been designed such that customized auto-indent behavior can be implemented to suit the language being edited.

If a class that implements [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) is registered with a syntax language as an [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) service, the indent provider's [Mode](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider.Mode) property will be queried by SyntaxEditor to determine the type of indentation it should be performing when <kbd>Enter</kbd> is pressed.

There are three mode options:

| Mode | Description |
|-----|-----|
| None | When selected, no automatic indenting occurs when the end user presses <kbd>Enter</kbd> to move to a new line of text, and the cursor is placed at the first column of the next line. |
| Block | When selected and the end user presses <kbd>Enter</kbd>, the new line of text is automatically indented to the same indentation as the line preceding it. |
| Smart | When selected and the end user presses <kbd>Enter</kbd>, the new line of text is automatically indented based on what the current language's [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) service determines the context to be. |

## Default Auto-Indent Behavior

If do not you have an indent provider registered with your syntax language, a default "Block" mode will be used. When the end user presses <kbd>Enter</kbd>, the new line of text is automatically indented to the same indentation as the line preceding it.

## Smart Indent Logic

When an [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) is registered with the language and the [Smart](xref:@ActiproUIRoot.Controls.SyntaxEditor.IndentMode.Smart) indent mode is set, the [GetIndentAmount](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider.GetIndentAmount*) method will be invoked whenever the <kbd>Enter</kbd> key is pressed.

The method is passed a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and the default "block" indent amount.  The result of this method should always be the number of columns (spaces) to indent the line, as opposed to tabs.  Upon returning from the method, SyntaxEditor will automatically indent the first non-whitespace character on the line to the desired column.

The [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset).[Snapshot](xref:ActiproSoftware.Text.TextSnapshotOffset.Snapshot) property gives access to its [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  Likewise, the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[Document](xref:ActiproSoftware.Text.ITextSnapshot.Document) provides access to its owner [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).  The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[TabSize](xref:ActiproSoftware.Text.ITextDocument.TabSize) property tells how many spaces are in a tab, in case that information is useful while determining the amount to indent.

If the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) is an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument), the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property may also contain AST or other language-specific information that can be useful when determining the indent amount.

Indent providers are free to use any custom logic in the [GetIndentAmount](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider.GetIndentAmount*) implementation to perform the indentation.  They typically will create an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) (see the [Scanning Text Using a Reader](../../text-parsing/core-text/scanning-text.md) topic), will scan backwards from the indicated offset and will determine how the passed-in "block" indent amount value should be altered, if at all.

For instance, in C#, if while scanning backwards the first non-whitespace character encountered is a `{`, then the indent amount returned should be the "block" indent amount plus an additional tab stop (usually calculated by adding the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[TabSize](xref:ActiproSoftware.Text.ITextDocument.TabSize) value).

## Registering with a Language

This code demonstrates registering an `XmlIndentProvider` service with the syntax language that is already declared in the `language` variable:

```csharp
XmlIndentProvider indentProvider = new XmlIndentProvider();
language.RegisterService<IIndentProvider>(indentProvider);
```

## Curly Brace Auto-Indent

Some languages like C# and JavaScript use curly braces as delimiters for code blocks.  When the end user presses <kbd>Enter</kbd> while the caret is immediately between a curly brace pair, it is handy for the caret to be moved to the next line, indented, and for the close curly brace to be moved to the line following the caret.  This aids in typing efficiency.

This example, where the `|` character denotes the caret, demonstrates the before and after what is described above:

```
function Foo() {|}
```

```
function Foo() {
	|
}
```

A pre-built [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider) class is included that implements [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider).  While it doesn't actually do anything other than block indent, it does have the code necessary to support curly brace auto-indent, as long as its [CanAutoIndentCurlyBraces](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.CanAutoIndentCurlyBraces) property is set to `true` (the default).

> [!NOTE]
> This class is ideal for use as a base class for your own indent providers, as long as your language should have curly brace auto-indent features.

By default, [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider) will just perform basic text scanning when determining if delimiters are valid for auto-indent.  However, this can cause issues when examining an open curly brace and the character is within a string or comment, meaning it's not really a code-oriented brace.  Thus, the indent provider also provides optional token IDs that can be set for each character.  When a potential open/close delimiter pair is found and related token IDs are specified, it will double-check that the tokens have those token IDs.  This eliminates the problem.  Properties such as [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider).[OpenCurlyBraceTokenId](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.OpenCurlyBraceTokenId) and [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider).[CloseCurlyBraceTokenId](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.CloseCurlyBraceTokenId) are available for indicating the token IDs.

In some lexers such as [dynamic lexers](../../text-parsing/lexing/dynamic-lexers.md), token ID's might not be known and string-based token keys might only be set.  In these cases, it's easy to handle.  Instead of specifying values to the various token ID properties on [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider), override its [IsValidStartDelimiter](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.IsValidStartDelimiter*) and [IsValidEndDelimiter](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.IsValidEndDelimiter*) methods.  An example of this concept is given in the [Delimiter Auto-Completion](delimiter-auto-completion.md) topic.

## Square Brace Auto-Indent

The pre-built [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider) class described above also fully supports square brace auto-indent.  This feature can be activated by setting the [CanAutoIndentSquareBraces](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.CanAutoIndentSquareBraces) property is set to `true`.

As with curly braces, [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider) will just perform basic text scanning when determining if delimiters are valid for auto-indent.  However, this can cause issues when examining an open curly brace and the character is within a string or comment, meaning it's not really a code-oriented brace.  Thus, the indent provider also provides optional token IDs that can be set for each character.  When a potential open/close delimiter pair is found and related token IDs are specified, it will double-check that the tokens have those token IDs.  This eliminates the problem.  Properties such as [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider).[OpenSquareBraceTokenId](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.OpenSquareBraceTokenId) and [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider).[CloseSquareBraceTokenId](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.CloseSquareBraceTokenId) are available for indicating the token IDs.

In some lexers such as [dynamic lexers](../../text-parsing/lexing/dynamic-lexers.md), token ID's might not be known and string-based token keys might only be set.  In these cases, it's easy to handle.  Instead of specifying values to the various token ID properties on [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider), override its [IsValidStartDelimiter](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.IsValidStartDelimiter*) and [IsValidEndDelimiter](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.IsValidEndDelimiter*) methods.  An example of this concept is given in the [Delimiter Auto-Completion](delimiter-auto-completion.md) topic.

## Close Delimiter Indentation Level

When using the [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider) class described above, the close delimiter will normally flow to the same indent level as the line upon which <kbd>Enter</kbd> was pressed, such as:

```
function Foo() {|}
```

```
function Foo() {
	|
}
```

If the [DelimiterIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider).[CloseDelimiterIndentLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DelimiterIndentProvider.CloseDelimiterIndentLevel) property is set to `1`, the close delimiter indent an additional level:

```
function Foo() {|}
```

```
function Foo() {
	|
	}
```
