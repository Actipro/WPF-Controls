---
title: "Dynamic Lexical Macros"
page-title: "Dynamic Lexical Macros - SyntaxEditor Regular Expression Guide"
order: 3
---
# Dynamic Lexical Macros

The built-in regular expression engine, when used with [dynamic lexers](../text-parsing/lexing/dynamic-lexers.md), allows for the definition of macros that represent regular expression elements.  These macros are valid for use in any regular expression within a dynamic lexer and promotes reusability of common patterns.

## Usage

Using defined macros is easy.  To reference a macro, simply type its name within curly braces; e.g., `{MacroName}`.

This regular expression uses a macro that represents the character class `[0-9]` to build a decimal number regular expression.

`{Digit}+ (\. {Digit}+)?`

This regular expression builds a C# identifier using two macros.

`(_ | {Alpha})({Word})*`

## Built-In Macros

The regular expression engine recognizes a number of built-in macros.  If a [dynamic lexer](../text-parsing/lexing/dynamic-lexers.md) defines a lexical macro of the same key as a built-in lexical macro, the user's definition will override the system definition.

The following table summarizes all of the built-in macros:

| Name | Description |
|-----|-----|
| `{All}` | Contains all Unicode characters.  This is the same as: `[\u0000-\uFFFF]`. |
| `{Alpha}` | Contains all Unicode alphabetic digits.  This is the same as `\p{L}` (all letters). |
| `{Digit}` | Contains all Unicode decimal digits.  This is the same as `\d` and `\p{Nd}`. |
| `{HexDigit}` | Contains all Unicode hexadecimal digits.  This is the same as `[0-9a-fA-F]`. |
| `{LineTerminator}` | Contains all Unicode line terminators.  This is the same as `[\n\r\p{Zl}\p{Zp}]`. |
| `{LineTerminatorWhitespace}` | Contains all Unicode line terminators and whitespace characters.  This is the same as `\s` or `[\f\n\r\t\v\x85\p{Z}]`. |
| `{NonAlpha}` | Contains the inverse of `Alpha`. |
| `{NonDigit}` | Contains the inverse of `Digit`. |
| `{None}` | Contains no characters. |
| `{NonHexDigit}` | Contains the inverse of `HexDigit`. |
| `{NonLineTerminator}` | Contains the inverse of `LineTerminator`. |
| `{NonLineTerminatorWhitespace}` | Contains the inverse of `LineTerminatorWhitespace`. |
| `{NonWhitespace}` | Contains the inverse of `Whitespace`. |
| `{NonWord}` | Contains the inverse of `Word`. |
| `{Whitespace}` | Contains all Unicode whitespace characters.  This is the same as `[\f\t\v\x85\p{Zs}]`. |
| `{Word}` | Contains all Unicode word characters.  This is the same as `\w` and `[\p{L}\p{Nd}\p{Pc}]` (all letters, decimal digits, and connectors like underscore). |
