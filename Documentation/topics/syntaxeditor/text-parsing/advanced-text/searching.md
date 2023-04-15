---
title: "Low-Level Search Operations"
page-title: "Low-Level Search Operations - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 2
---
# Low-Level Search Operations

The text framework contains an object model for performing low-level search operations.  This low-level object model is usually called by higher-level search object models that are more view-based.

## Finding Text

Since text snapshots provide immutable views on a text document, the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) interface contains several methods for finding text:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[FindAll](xref:ActiproSoftware.Text.ITextSnapshot.FindAll*) Method

</td>
<td>

Performs a "find all" operation.

There are two overloads of this method.  One overload accepts a [TextRange](xref:ActiproSoftware.Text.TextRange) indicating the range of offsets to search.

</td>
</tr>

<tr>
<td>

[FindNext](xref:ActiproSoftware.Text.ITextSnapshot.FindNext*) Method

</td>
<td>

Performs a "find next" operation.

There are two overloads of this method.  One overload accepts a [TextRange](xref:ActiproSoftware.Text.TextRange) indicating the range of offsets to search.

</td>
</tr>

</tbody>
</table>

Both methods return an [ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet) object that contains information about the search results.  In the case of a "find all" operation, more than one result may be present within the result set.

## Replacing Text

Since text change operations typically originate from a document, the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) interface contains several methods for replacing text:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[ReplaceAll](xref:ActiproSoftware.Text.ITextDocument.ReplaceAll*) Method

</td>
<td>

Performs a "replace all" operation.

There are two overloads of this method.  One overload accepts a [TextRange](xref:ActiproSoftware.Text.TextRange) indicating the range of offsets to search.

</td>
</tr>

<tr>
<td>

[ReplaceNext](xref:ActiproSoftware.Text.ITextDocument.ReplaceNext*) Method

</td>
<td>

Performs a "replace next" operation.

There are two overloads of this method.  One overload accepts a [TextRange](xref:ActiproSoftware.Text.TextRange) indicating the range of offsets to search.

</td>
</tr>

</tbody>
</table>

Both methods return an [ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet) object that contains information about the search results.  In the case of a "replace all" operation, more than one result may be present within the result set.

## Search Results

[ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet) instances are what are returned by all the search methods.  The result set provides a deep copy of the [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) used by the search operation, a [SearchOperationType](xref:ActiproSoftware.Text.Searching.SearchOperationType) indicating the type of operation (find next, find all, etc.), whether the searching wrapped at the end of the search range, and the collection of [ISearchResult](xref:ActiproSoftware.Text.Searching.ISearchResult) objects that specify individual results.

The `ToString` method of the [ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet) provides a text representation of the search criteria and individual results, and can be useful for display in results windows.

Each [ISearchResult](xref:ActiproSoftware.Text.Searching.ISearchResult) object indicates the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) at which the result was found, and in the case of replace operations, the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) of the replaced text.  If no replace occurred, that [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) will be marked as deleted.

[ISearchResult](xref:ActiproSoftware.Text.Searching.ISearchResult) also contains a collection of captures.  Since the search engine is based on regular expressions, this collection specifies the captures that were made.  Each [ISearchCapture](xref:ActiproSoftware.Text.Searching.ISearchCapture) has a key that identifies the capture, the text that was captured, and its offset range.

## Search Options

The [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) interface contains all the settings needed to perform a search operation.  These options include the text to find/replace, whether to match case, match whole word, search up, and the type of seatch pattern provider to use (normal, regular expressions, etc.).  The maximum number of results can also be set, which is useful in "find all" scenarios.

Each core search method described above accepts an [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) instance.  Any custom implementations of [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) should implement an `Equals` method override.

## Built-In Search Pattern Providers

A search pattern format is a syntax for entering how to find and replace text within a document.  Since the search engine is regular expression-based, the [ISearchPatternProvider](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider) interface provides two methods, one for getting a regex pattern for supplied "find" text, and one for getting a regex pattern for supplied "replace" text.

The text framework ships with the following built-in pattern providers:

- [Normal](xref:ActiproSoftware.Text.Searching.SearchPatternProviders.Normal) is a plain text find and replace format.
- [RegularExpression](xref:ActiproSoftware.Text.Searching.SearchPatternProviders.RegularExpression) uses regular expressions to match text, and uses regular expression replace pattern syntax to allow for replacements at different matched capture points in the find pattern.
- [Wildcard](xref:ActiproSoftware.Text.Searching.SearchPatternProviders.Wildcard) uses simple wildcard search patterns to match text.
- [Acronym](xref:ActiproSoftware.Text.Searching.SearchPatternProviders.Acronym) matches a character at the start of a word, then every capital letter or character following an underscore.
- [Shorthand](xref:ActiproSoftware.Text.Searching.SearchPatternProviders.Shorthand) allows any non-whitespace character to be between the search pattern characters.

> [!NOTE]
> The [SearchPatternProviders](xref:ActiproSoftware.Text.Searching.SearchPatternProviders) static class provides access to each of these [ISearchPatternProvider](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider) instances.

### Normal Pattern Provider

This pattern provider searches for the "find text" exactly as specified and replaces using the exact "replace text"".

### Regular Expression Pattern Provider

Regular expression find/replace operations search for text by using .NET regular expression syntax pattern matching.  It is important to note that pattern whitespace is ignored.  Whitespace can be matched by using the `\s` character class.

The regular expression pattern provider has a feature that the others don't have.  In its replace patterns, substitutions can be specified.  Replace patterns recognize normal text characters, regular expression escape characters (`\n` meaning line feed, etc.), and substitution specifications (`$1`, etc.).

Substitutions place text from captures into the replace text.  This is best illustrated by an example.  Imagine the text to be searched is `using System;`, the "find text" is `using \s+ (\w[\w\.]+);`, and the "replace text" is `Namespace: $1;`.  This operation is looking for any C# using statement and is outputting the namespace.  So the result in this scenario is `Namespace: System;` since `System` was the value matched by the first capture group and `$1` in the "replace text" is a placeholder for the first captured value.

See the [Regular Expression Language Elements](../../regular-expressions/language-elements.md) topic for more information on the regular expression syntax.

### Wildcard Pattern Provider

Wildcard pattern providers search for text by using "wildcard" pattern matching.  The following wildcard patterns are recognized:

| Construct | Description |
|-----|-----|
| `*`  | Specifies zero or more of any character, except the line feed character. |
| `?`   | Specifies any single character, except the line feed character. |
| `#`   | Specifies any single digit. |
| `[aeiou]` | Matches any one character in the set. |
| `[!aeiou]` | Matches any one character not in the set. |

Any other character in the pattern matches the character.  An example wildcard pattern is `using *;`. This pattern will match any C# using statement.

### Acronym Pattern Provider

Acronym searching matches a character at the start of a word, then every capital letter or character following an underscore (`_`).  Space characters have special meaning to match any whitespace character.

An example acronym pattern `fr` would match the text `FindR` in the word `FindReplace`.

### Shorthand Pattern Provider

Shorthand searching is even more open in scope than acronyms.  It allows any non-whitespace character to be between the search pattern characters.  Space characters have special meaning to match any whitespace character.

An example shorthand pattern `fr` would match the text `for` in the word `therefore`.

## Creating a Custom Search Pattern Provider

Although the text framework comes packaged with the pattern providers described above, it's easy to create your own custom search pattern providers as well.

The [ISearchPatternProvider](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider) interface has several members.  One is a string `Key` that identifies it.  Another is a more verbose [Description](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider.Description) that can be displayed in user interfaces.  Then there are two methods that must be implemented.

The [GetFindPattern](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider.GetFindPattern*) method returns the regular expression match pattern to use for find operations, based on the supplied pattern that uses the provider.  Likewise, the [GetReplacePattern](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider.GetReplacePattern*) method returns the regular expression match pattern to use for replace operations, based on the supplied pattern that uses the provider.

As mentioned above, the core search engine uses our built-in regular expression engine to perform searching.  Therefore, search patterns used by the search engine must be provided in regular expression syntax.  That is essentially what an [ISearchPatternProvider](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider) does, converts a custom pattern syntax to regular expression syntax.

Finally, the [RequiresCaseSensitivity](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider.RequiresCaseSensitivity) property returns whether the find patterns being used require case sensitivity.  If this is `false`, the default, the [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions).[MatchCase](xref:ActiproSoftware.Text.Searching.ISearchOptions.MatchCase) value is used instead.
