---
title: "Delimiter Auto-Completion"
page-title: "Delimiter Auto-Completion - SyntaxEditor Input/Output Features"
order: 10
---
# Delimiter Auto-Completion

Delimiter auto-completion occurs when the end user types a start delimiter and a related end delimiter is auto-inserted after the caret.  This improves code editing productivity since it means less overall typing is required to output the same code.

## Activating Delimiter Auto-Completion

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsDelimiterAutoCompleteEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsDelimiterAutoCompleteEnabled) property can be set to `true` to allow SyntaxEditor to perform delimiter auto-completion, as long as the current syntax language supports the feature.  Delimiter auto-completion is enabled on SyntaxEditor by default.

## Auto-Completion Logic

Delimiter auto-completers are language services that implement the [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) interface.  A built-in [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter) class is included that fully implements [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) and makes it easy for a language to support auto-completion of these delimiter pairs:

- Angle braces, `<` and `>` (disabled by default)
- Curly braces, `{` and `}`
- Parenthesis, `(` and `)`
- Square braces, `[` and `]`
- Double quotes, `"`
- Single quotes, `'`  (disabled by default)

The various supported brace match options can be turned on or off with properties such as [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter).[CanCompleteSquareBraces](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter.CanCompleteSquareBraces).

By default, this auto-completer will just perform basic text scanning when determining if delimiters are valid for auto-completion.  However, this can cause issues when examining an open parenthesis and the character is within a string or comment, meaning it's not really a code-oriented parenthesis.  Thus, the auto-completer also provides optional token IDs that can be set for each character.  When a potential open delimiter is found and a related token ID is specified, it will double-check that the token at the offset has that token ID.  This eliminates the problem.  Properties such as [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter).[OpenParenthesisTokenId](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter.OpenParenthesisTokenId) are available for indicating the token IDs.

## Registering a Delimiter Auto-Completer with a Syntax Language

An [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) can be associated with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) by registering it with the language.  This example shows how to use a [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter) that only has curly brace and parenthesis support:

```csharp
language.RegisterService<IDelimiterAutoCompleter>(new DelimiterAutoCompleter() {
	CanCompleteDoubleQuotes = false,
	CanCompleteSquareBraces = false,
	OpenCurlyBraceTokenId = SimpleTokenId.OpenCurlyBrace,
	OpenParenthesisTokenId = SimpleTokenId.OpenParenthesis,
});
```

Note how token IDs are specified to ensure start delimiters are properly recognized.

## Working with Dynamic Lexers and String Token Keys

In some lexers such as [dynamic lexers](../../text-parsing/lexing/dynamic-lexers.md), token ID's might not be known and string-based token keys might only be set.  In these cases, it's easy to handle.  Instead of specifying values to the various token ID properties on [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter), override its [IsValidStartDelimiter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter.IsValidStartDelimiter*) method.

This example shows how to make a customized delimiter auto-completer that adds parenthesis-only auto-completion to the free SQL language:

```csharp
public class SqlDelimiterAutoCompleter : DelimiterAutoCompleter {

	public SqlDelimiterAutoCompleter() {
		this.CanCompleteDoubleQuotes = false;
		this.CanCompleteCurlyBraces = false;
		this.CanCompleteSquareBraces = false;
	}

	protected override bool IsValidStartDelimiter(IToken token, char startDelimiter) {
		switch (startDelimiter) {
			case '(':
				return (token != null) && (token.Key == "OpenParenthesis");
			default:
				return false;
		}
	}

}
```

## Single Token String Delimiter Auto-Completion

Some languages have a single token to represent a string, and the same character is used to delimit the string start and end.  In this sort of case, the [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter).[IsValidStartDelimiter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter.IsValidStartDelimiter*) method can be overridden with some custom logic.

This example shows how we accomplish basic string literal auto-completion in our .NET Languages Add-on's C# language:

```csharp
public class CSharpDelimiterAutoCompleter : DelimiterAutoCompleter {

	...

	protected override bool IsValidStartDelimiter(ITextSnapshotReader reader, char startDelimiter) {
		if (base.IsValidStartDelimiter(reader, startDelimiter))
			return true;

		if ((reader != null) && (startDelimiter == '"')) {
			var token = ((reader.IsAtTokenStart || reader.IsAtSnapshotEnd) ? reader.PeekTokenReverse() : reader.Token);
			if ((token != null) && (token.Id == CSharpTokenId.LiteralString))
				return (token.StartOffset == reader.Offset - 1);
		}

		return false;
	}

}
```
