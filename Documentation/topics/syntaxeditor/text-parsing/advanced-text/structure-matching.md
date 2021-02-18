---
title: "Structure Matching"
page-title: "Structure Matching - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 8
---
# Structure Matching

The text framework includes support for locating matching structural text delimiters, such as brackets.

Once the language service is implemented for structure matching, SyntaxEditor features such as move to matching bracket and select to matching bracket are automatically supported.  Structure matching is also a requirement for the [delimiter highlighting](../../user-interface/editor-view/delimiter-highlighting.md) feature.

## Matching Logic

Structure matchers implement the [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) interface, which defines a single method.  The [Match](xref:ActiproSoftware.Text.Analysis.IStructureMatcher.Match*) method accepts the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) to examine and an [IStructureMatchOptions](xref:ActiproSoftware.Text.Analysis.IStructureMatchOptions) object that provides match options.  The goal of this method is to look around the offset and built a set of results that indicate if there are structural text delimiters (generally parentheses and braces) around it.  For instance if the caret is next to a parenthesis, then the method would return results indicating the range of the parenthesis and also the range of its matching parenthesis, if found.

The [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset).[Snapshot](xref:ActiproSoftware.Text.TextSnapshotOffset.Snapshot) property gives access to its [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  From the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot), an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) can be obtained.

A structure matcher generally does text scanning, token scanning, or even AST examination to build its results.  The [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) allows for easy text and token scanning of a snapshot.  If an AST is needed for examination and the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) is an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument), the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property may also contain AST or other language-specific information that can be useful when performing matching logic.

### Options

The [IStructureMatchOptions](xref:ActiproSoftware.Text.Analysis.IStructureMatchOptions) interface defines a [IsNavigationRequest](xref:ActiproSoftware.Text.Analysis.IStructureMatchOptions.IsNavigationRequest) property that can be set to true when the request is being made for navigation purposes, such as SyntaxEditor's move to matching bracket feature.  When this property is true, the match logic should be a bit more liberal and allow any possible bracket surrounding or next to the source offset to be a valid result.

When this property is false, the request is more likely for a feature such as delimiter highlighting.  And in that case, the logic for finding a match next to the source offset should be a bit more strict.

### Results

The [Match](xref:ActiproSoftware.Text.Analysis.IStructureMatcher.Match*) method returns an [IStructureMatchResultSet](xref:ActiproSoftware.Text.Analysis.IStructureMatchResultSet) object that contains a collection of [IStructureMatchResult](xref:ActiproSoftware.Text.Analysis.IStructureMatchResult) objects.  Each [IStructureMatchResult](xref:ActiproSoftware.Text.Analysis.IStructureMatchResult) indicates its [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) and also an optional [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) if the match is something that can be navigated to when SyntaxEditor's move to matching bracket feature is utilized.

If a structural delimiter is next to the source offset, a result for that delimiter should be included in the result set, and its [IsSource](xref:ActiproSoftware.Text.Analysis.IStructureMatchResult.IsSource) property needs to be set to `true`.  Any other "matching" structural delimiters should also be included in the collection.

For example, if parenthesis structure matching is supported, the match logic would first use an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) to see if a parenthesis is next to the source offset.  If there is one, a result (with [IsSource](xref:ActiproSoftware.Text.Analysis.IStructureMatchResult.IsSource) indication) is added to the result set for that parenthesis.  Then the reader is used to scan forward or backward as appropriate to look for a matching parenthesis.  If one is found, a second result is added to the result set.

Structure matching also supports multiple results.  Say that the structure matching should support `#if...#else...#endif` blocks.  If the source offset is within the `#else` then the result set would include three results (one for each directive text range) and only the one for `#else` would have its [IsSource](xref:ActiproSoftware.Text.Analysis.IStructureMatchResult.IsSource) property set to `true`.

Each result also has a [MatchEdges](xref:ActiproSoftware.Text.Analysis.IStructureMatchResult.MatchEdges) property that should be set to a [StructureMatchEdges](xref:ActiproSoftware.Text.Analysis.StructureMatchEdges) value indicating which edges (none, start, end, or both) of the result's range can cause the structure matcher to match the result again.  This information is used for optimizations by the [delimiter highlighting](../../user-interface/editor-view/delimiter-highlighting.md) feature.

## Registering a Structure Matcher with a Syntax Language

An [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) can be associated with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) by registering it with the language:

```csharp
language.RegisterService<IStructureMatcher>(myStructureMatcher);
```

Registering structure matchers with a language is optional, but recommended.

## Default Structure Matcher Implementation

A built-in [StructureMatcher](xref:ActiproSoftware.Text.Analysis.Implementation.StructureMatcher) class is included that implements [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) and makes it easy for a language to support matching of these bracket pairs:

- Angle braces (disabled by default)
- Curly braces
- Parenthesis
- Square braces

The various supported brace match options can be turned on or off with properties such as [StructureMatcher](xref:ActiproSoftware.Text.Analysis.Implementation.StructureMatcher).[CanMatchSquareBraces](xref:ActiproSoftware.Text.Analysis.Implementation.StructureMatcher.CanMatchSquareBraces).

By default, this matcher will just perform basic text scanning to match brackets.  However this can cause issues when scanning for a pair of parentheses and there is a parenthesis character within a contained string.  Thus the matcher also provides optional token IDs that can be set for each character.  When a potential text match is found and a related token ID is specified, it will double-check that the token at the offset has that token ID.  This eliminates the problem.  Properties such as [StructureMatcher](xref:ActiproSoftware.Text.Analysis.Implementation.StructureMatcher).[OpenParenthesisTokenId](xref:ActiproSoftware.Text.Analysis.Implementation.StructureMatcher.OpenParenthesisTokenId) are available for indicating the token IDs.
