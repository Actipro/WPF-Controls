---
title: "Dynamic Lexers"
page-title: "Dynamic Lexers - Lexing - SyntaxEditor Text/Parsing Framework"
order: 4
---
# Dynamic Lexers

Dynamic lexers are mergable lexers and use our custom pattern-based regular expression engine to match and tokenize text.  It is recommended that developers new to the text/parsing framework as well as developers who do not require much advanced parsing use dynamic lexers since they make it simple to get up and running quickly.

## How do they work?

Dynamic lexers, represented by the [DynamicLexer](xref:ActiproSoftware.Text.Lexing.Implementation.DynamicLexer) class, harness a specialized pattern-based engine to tokenize document text.  This means that no handwritten lexing code needs to be written for dynamic lexers, such as you would write for a [programmatic lexer](programmatic-lexers.md).  Instead, you provide the patterns that the lexer engine will use to match the text against.  The patterns can be straight text (called explicit) or regular expressions (called regex).  They also support case senstivity settings.

The lexer's patterns can be defined either by using a language definition file or by constructing the lexer programmatically via its object model.  If a language definition file is used, the [Loading a Language Definition](../../language-creation/loading-lang-def.md) topic explains how to load it at runtime.

> [!NOTE]
> The [Language Designer](../../language-designer-tool/index.md) tool has extensive features for building a dynamic lexer.  A new dynamic lexer wizard asks you several questions about your language and builds an initial dynamic lexer for you, which you can then modify as needed.  Use that wizard to get started fast.

There are a number of pieces that make up a dynamic lexer, all described in greater detail below:

- Lexical states
- Lexical scopes
- Lexical pattern groups
- Lexical macros
- Lexical state transitions

## Free Samples

There are a number of free open source syntax language samples that use dynamic lexers.  These [free samples](../../free-languages.md) are included in the sample project and cover everything from C# to Perl.

## Lexical States

Dynamic lexical states, represented by the [DynamicLexicalState](xref:ActiproSoftware.Text.Lexing.Implementation.DynamicLexicalState) class, provide states into which different patterns may be grouped.  The lexer only recognizes patterns that are valid within the current lexical state.

For instance, all lexers must have a default lexical state, but then languages such as C# may have another lexical state for XML documentation comments.  As you know, XML documentation comment tags are not valid in normal code.  The code must have a `///` delimiter on a line (thereby entering the XML documentation comment lexical state) and then tags are valid.  While in this state, keywords such as `void` are not recognized and all text other than tags is treated as comment text.  The lexical state ends at the line terminator and flips back to its parent default lexical state where again, keywords like `void` are recognized.

Dynamic lexical states have several default properties that can inherit down to lexical pattern groups inside the state.  Defaults are available for case sensitivity, classification type, and token ID/key.

Each dynamic lexical state has a collection of child lexical states and lexical scopes.  The child lexical states collection specifies the lexical states that can be entered from the lexical state.  When the dynamic lexer engine runs, it looks at each child lexical state to see if any of their lexical scope start lexical pattern groups match at the current offset in the text.  If so, that child lexical state is entered.  When the child lexical state's end lexical pattern group is recognized at a later point, the parent lexical state is re-entered.

Dynamic lexical states also have a lexical pattern groups collection, which provide the list of patterns that can be recognized while in the lexical state.

## Lexical Scopes

Dynamic lexical states are represented by the [DynamicLexicalScope](xref:ActiproSoftware.Text.Lexing.Implementation.DynamicLexicalScope) class.  As mentioned above, lexical scopes control when a child lexical state is entered and exited.  Dynamic lexical scopes have a start lexical pattern group and end lexical pattern group property that provide the patterns recognized for entry and exit.

## Lexical Pattern Groups

A lexical pattern group, represented by the [DynamicLexicalPatternGroup](xref:ActiproSoftware.Text.Lexing.Implementation.DynamicLexicalPatternGroup) class, is a group of patterns that share common attributes.  For instance, in a dynamic lexer there is often one explcit lexical pattern group in the default lexical state that contains a list of keywords, where each pattern is the keyword text.

Lexical pattern groups can be an explicit or regex pattern type.  When using the explicit pattern type, the pattern is matched exactly as specified, including whitespace.  When using the regex pattern type, the pattern is matched using our regular expression engine.  See the [Regular Expression Guide](../../regular-expressions/index.md) for the regex pattern syntax.

Lexical pattern groups have options for case sensitivity, classification type, and token ID/key assigned.

In addition, a look-behind and look-ahead pattern can be set.  These patterns are always specified in the regex pattern type and apply to all patterns in the lexical pattern group.  When the dynamic lexer engine attempts to match a pattern, it also checks the look-behind pattern and look-ahead pattern to see if they match.  They are treated as zero-width assertions, however if either fail, then no patterns in the lexical pattern group can match.

A common look-ahead pattern used for things like keywords is `{NonWord}|\z`.  That look-ahead pattern ensures that patterns in the pattern group will match if the text following the match starts with a non-word character or is at the end of the document.  Thus, if matching the keyword `int`, the document text `int foo;` will match the `int` at the start while the document text `integer foo;` will not match.

## Lexical Macros

Lexical macros are optional and provide a nice way to define reusable patterns for a dynamic lexer.  Custom patterns can be defined as macros, but you can also redefine built-in macros for your lexer.

For instance, in the CSS language the `Word` macro is redefined as `[a-zA-Z_0-9\-]`, which includes the hyphen character since that is a valid word character in CSS.  Likewise, the `NonWord` is redefined as `[^a-zA-Z_0-9\-]`.

Lexical macros can be referenced by key within `{` and `}` characters in any regex pattern.  For instance this pattern specifies a C# identifier: `(_ | {Alpha})({Word})*`

The [Dynamic Lexical Macros](../../regular-expressions/dynamic-lexical-macros.md) topic talks in more detail about the built-in macros that are available and how to use them in regex patterns.

## Lexical State Transitions (Language Transitions)

There are two types of lexical state transitions to different languages that are supported.

### Direct Lexical State Transitions

Sometimes it is necessary for a single pattern match to transition into another language.  An instance of that is an ASP directive within an HTML document.  In that case the `<%` directive changes the language to VBScript.  Likewise, the `%>` directive exits back to HTML.

This type of transition is called a direct lexical state transition.  You essentially create a dummy lexical state in the root HTML language.  You give it a lexical start and end scope, which are the ASP directive patterns.

You then set the dummy lexical state's [DynamicLexicalState](xref:ActiproSoftware.Text.Lexing.Implementation.DynamicLexicalState).[Transition](xref:ActiproSoftware.Text.Lexing.Implementation.LexicalStateBase.Transition) property to an [LexicalStateTransition](xref:ActiproSoftware.Text.Lexing.Implementation.LexicalStateTransition) object that indicates the language and lexical state to transition into.  When the dummy state's start scope is recognized by the lexer, it transitions to the child language you indicated.

### Scope Lexical State Transitions

A more complicated scenario is where a lexical state end scope is recognized and at that point, a lexical state transition needs to be made to another language.  This occurs in HTML where a `<style>` tag is recognized, and a transition needs to be made to CSS.

This can be accomplished via a scope lexical state transition.  You define a lexical state transition for a lexical scope (in the [DynamicLexicalScope](xref:ActiproSoftware.Text.Lexing.Implementation.DynamicLexicalScope).[Transition](xref:ActiproSoftware.Text.Lexing.Implementation.LexicalScopeBase.Transition) property) and you give the transition a child lexical scope with end pattern that it should look for to exit the child lexical state.

At run-time when the end scope pattern of the lexical state is recognized, the language transition is made.  Later on when the transition's child lexical scope end pattern is recognized, the child language is exited and the parent lexical state of the original lexical state is entered.

## Lexing Sequence

The lexer coordinator used by mergable lexers matches patterns in sequential order within four sequential groups:

- Parent scopes (outside of the current lexer) to see if one ends the current state.
- Child state start scopes to see if one starts a child state.
- Pattern groups within the current state.
- Parent scopes (inside of the current language) to see if one ends the current state.

In some scenarios you may wish to not check any ancestor scope to see if it ends the current state.  For these scenarios, you can set the [ILexicalScope](xref:ActiproSoftware.Text.Lexing.ILexicalScope).[IsAncestorEndScopeCheckEnabled](xref:ActiproSoftware.Text.Lexing.ILexicalScope.IsAncestorEndScopeCheckEnabled) property to `false`.  This prevents the lexer coordinator from looking at any scope above the current one when checking to see if they end the state.

## Importance of Pattern Order

It is extremely important to optimize your dynamic lexer design.  By making certain design decisions you can affect the run-time scanning speed of the dynamic lexer.  Use the techniques described below to get the most efficiency out of a dynamic lexer.

### Most Frequently Matched Patterns Should Be Defined First

Dynamic lexers use our custom NFA regular expression engine to parse text and find a pattern that matches the text.  It does this by looping through all of the patterns in the lexer in the order in which they are defined.  If a pattern is found to match the current text, the code breaks out of the loop and creates a [mergable token](tokens.md) containing the pattern's lexical parse information.

The key here is that the looping is done in the order in which the patterns groups are defined.  Therefore, lexical parsing will be much faster if you place the most frequently-matched pattern groups first.

For instance, in C#, tokens like `{` and `(` occur much more often than the keyword `volatile`.  The patterns for the `{` and `(` tokens should be defined at the beginning of a lexical state and the pattern for the infrequently-matched keyword `volatile` should be near the end of a lexical state.

Similar to how frequently-matched pattern groups should be placed first within a lexical state, frequently-matched patterns should be placed first within their parent pattern group.  This will help when there are a number of keywords that start with the same character.  SyntaxEditor has optimizations to help speed up which patterns to match however every bit of good language design helps speed up lexical parsing.

### Be Careful of Pattern Order

When defining patterns, remember that the first match is accepted.  Say you have two number pattern groups, one that looks for a series of digits followed by a period and more digits, marked as a real number and another patter than looks for a series of digits that ends with a non-digit, marked as an integer.  If the integer pattern is defined first, when the lexical parser encounters a number it will start using the integer pattern.  Say the number has a period afterwards and a number and should be defined as a real, the lexical parser would get to the period, see it as a non-digit and assign it as an integer.  The real number pattern would never even be reached.  Therefore, in situations like this, it's important to place the more complicated patterns first.  If the real number pattern had been defined first, then the parser would have tokenized the text appropriately.

The [Language Designer](../../language-designer-tool/index.md) has several helpful features for sorting explicit pattern group patterns appropriately to avoid this scenario when conflicting patterns are in the same lexical pattern group.

## Optimization

For best memory and performance optimization, try and develop patterns that use as long of matches as possible.  For instance, in comments, try and consume all comment text in a line in one token.  This will perform better than if there is one token for each letter or word.

When using a lexer in SyntaxEditor, it is also beneficial to try and keep tokens on one line.  For instance, in a multi-line comment, you should have a state with the text for each line in its own token, with a comment line terminator token for the line terminator.  That sort of design helps with the performance of incremental classification, versus possible scenarios where a single token is potentially a thousand lines long.

## Importing SyntaxEditor 4.0 for WinForms Dynamic Language XML Definitions

If you are a customer of our SyntaxEditor 4.0 for WinForms product and have created dynamic language XML definitions there, they can easily be converted over to the new language definition format used in this product.  Use the [Language Designer](../../language-designer-tool/index.md) to import the XML definition and automatically create a language project that can export a language definition file.  The Language Designer's [Getting Started](../../language-designer-tool/getting-started.md) topic talks about this feature.

The language definition format used by this product is very similar in structure to the SyntaxEditor 4.0 dynamic language XML definition format, but the new format is more extensible and works for all languages, not just those that use a dynamic lexer.
