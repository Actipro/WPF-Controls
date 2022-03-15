---
title: "Term Definitions"
page-title: "Term Definitions - SyntaxEditor Reference"
order: 4
---
# Term Definitions

SyntaxEditor is no small product.  It has an enormous object model and feature set.  While it's easy to get started using it, it also allows for developers to do some very advanced things.

In this topic, we provide the complete list of categorized terms found in this product.  If you ever wonder what a term means, use this topic to find the definition.

## Text (General) Terms

<table>
<thead>

<tr>
<th>Term</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>Case Sensitivity</td>
<td>

Indicates how letter matching is performed when parsing, and is represented by the [CaseSensitivity](xref:ActiproSoftware.Text.CaseSensitivity) enumeration.

</td>
</tr>

<tr>
<td>Classification Type</td>
<td>

A classification type (represented by the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) interface) is a categorization that can be applied to a range of text via an [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) tagger.

</td>
</tr>

<tr>
<td>Classification Type Registry</td>
<td>

A classification type registry (represented by the [IClassificationTypeRegistry](xref:ActiproSoftware.Text.IClassificationTypeRegistry) interface) provides a registry of [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) instances.

</td>
</tr>

<tr>
<td>Code Document</td>
<td>

A code document (represented by the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) interface) is a text document that also has a syntax language associated with it, providing additional functionality.

</td>
</tr>

<tr>
<td>Example Text Provider</td>
<td>

An example text provider (represented by the [IExampleTextProvider](xref:ActiproSoftware.Text.IExampleTextProvider) interface) is an object that can stores a small code snippet showing many of the constructs for a specific language.

</td>
</tr>

<tr>
<td>Line Commenter</td>
<td>

A line commenter (represented by the [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) interface) is an object that has logic to comment out and uncomment lines of code using comment delimiters for a specific language.

</td>
</tr>

<tr>
<td>Line Terminator</td>
<td>

Indicates the character or characters used to end a line, and is represented by the [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) enumeration.

</td>
</tr>

<tr>
<td>Normalized Text Snapshot Range Collection</td>
<td>

A normalized text snapshot range collection (represented by the [NormalizedTextSnapshotRangeCollection](xref:ActiproSoftware.Text.NormalizedTextSnapshotRangeCollection) class) is a collection that ensures all contained ranges are for the same snapshot and are guaranteed to be sorted and non-overlapping.

</td>
</tr>

<tr>
<td>Offset</td>
<td>An offset is a zero-based integer index that refers to the location of a character within text.</td>
</tr>

<tr>
<td>Syntax Language</td>
<td>

A syntax language (represented by the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) interface) is an object that provides access to services and functionality related to the parsing of a code language, such as C#, VB, HTML, etc.

</td>
</tr>

<tr>
<td>Text Buffer Reader</td>
<td>

A text buffer reader (represented by the [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) interface) allows you to do very low-level fast scanning over some sort of text source, such as a string or text snapshot.

</td>
</tr>

<tr>
<td>Text Change</td>
<td>

A text change (represented by the [ITextChange](xref:ActiproSoftware.Text.ITextChange) interface) is a single transaction that modifies a document and creates a new text snapshot.  It consists of one or more operations.

</td>
</tr>

<tr>
<td>Text Change Operation</td>
<td>

A text change operation (represented by the [ITextChangeOperation](xref:ActiproSoftware.Text.ITextChangeOperation) interface) is a single atomic replace operation that is performed within a text change.  It may replace text, delete text, or insert text.

</td>
</tr>

<tr>
<td>Text Change Options</td>
<td>

Text change options (represented by the [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) interface) provide configurable options to a text change.

</td>
</tr>

<tr>
<td>Text Change Type</td>
<td>

A text change type (represented by the [ITextChangeType](xref:ActiproSoftware.Text.ITextChangeType) interface) represents the type of text change, such as `Typing`, `Paste`, etc.  Custom types can be created.

</td>
</tr>

<tr>
<td>Text Document</td>
<td>

A text document (represented by the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) interface) represents a textual document.

It always keeps a reference to its current text snapshot.  Use the current snapshot to examine the contents of the document.

</td>
</tr>

<tr>
<td>Text Formatter</td>
<td>

A text formatter (represented by the [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) interface) is an object that has logic to adjust spacing and line terminators around the code for a specific language to beautify it.

</td>
</tr>

<tr>
<td>Text Offset Tracking Mode</td>
<td>

Text offset tracking modes (represented by the [TextOffsetTrackingMode](xref:ActiproSoftware.Text.TextOffsetTrackingMode) enumeration) indicate how an offset should be translated from one text snapshot to another.

</td>
</tr>

<tr>
<td>Text Position</td>
<td>

A text position (represented by the [TextPosition](xref:ActiproSoftware.Text.TextPosition) structure) provides line and character information about a point in text.

</td>
</tr>

<tr>
<td>Text Position Range</td>
<td>

A text position range (represented by the [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) structure) represents a range of text positions, with a start and end value.

</td>
</tr>

<tr>
<td>Text Range</td>
<td>

A text range (represented by the [TextRange](xref:ActiproSoftware.Text.TextRange) structure) represents a range of offsets, with a start and end value.

</td>
</tr>

<tr>
<td>Text Range Tracking Mode</td>
<td>

Text range tracking modes (represented by the [TextRangeTrackingModes](xref:ActiproSoftware.Text.TextRangeTrackingModes) enumeration) indicate how a text range should be translated from one text snapshot to another.

</td>
</tr>

<tr>
<td>Text Snapshot</td>
<td>

A text snapshot (represented by the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) interface) is an immutable copy of a document at a certain time.  It can be scanned and parsed without the possibility of change via user edits.

All reading of a document is done on a snapshot of the document.  The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[CurrentSnapshot](xref:ActiproSoftware.Text.ITextDocument.CurrentSnapshot) property always provides the most recent snapshot.

</td>
</tr>

<tr>
<td>Text Snapshot Line</td>
<td>

A text snapshot line (represented by the [ITextSnapshotLine](xref:ActiproSoftware.Text.ITextSnapshotLine) interface) represents a text line within a snapshot.

</td>
</tr>

<tr>
<td>Text Snapshot Offset</td>
<td>

A text snapshot offset (represented by the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) structure) points to a specific offset within a snapshot.  It can be easily translated to other snapshots.

</td>
</tr>

<tr>
<td>Text Snapshot Range</td>
<td>

A text snapshot range (represented by the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) structure) points to a specific text range within a snapshot.  It can be easily translated to other snapshots.

</td>
</tr>

<tr>
<td>Text Snapshot Reader</td>
<td>

A text snapshot reader (represented by the [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) interface) is used to scan through a specific snapshot and has many useful methods for navigating by token, word, etc.

</td>
</tr>

<tr>
<td>Text Snapshot Reader Options</td>
<td>

Text snapshot reader options (represented by the [ITextSnapshotReaderOptions](xref:ActiproSoftware.Text.ITextSnapshotReaderOptions) interface) provides configurable options to a text snapshot reader.

</td>
</tr>

<tr>
<td>Text Statistics</td>
<td>

Text statistics (represented by the [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) interface) examine text and provide numerous statistics on the text such as number of lines, characters, etc.

</td>
</tr>

<tr>
<td>Text Version</td>
<td>

A text version (represented by the [ITextVersion](xref:ActiproSoftware.Text.ITextVersion) interface) is a numeric version that tracks the sequence of changes made to a document.  Each snapshot is related to a certain version.

</td>
</tr>

<tr>
<td>Text Version Range</td>
<td>

A text version range (represented by the [ITextVersionRange](xref:ActiproSoftware.Text.ITextVersionRange) interface) is an object that internally stores a text range for a certain text version.  This version range can be translated to any other version or snapshot to get the related text range in that context.

</td>
</tr>

<tr>
<td>Word-Break Finder</td>
<td>

A word-break finder (represented by the [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) interface) is used to locate word breaks for a particular syntax language.

</td>
</tr>

</tbody>
</table>

## Text (Exporters) Terms

| Term | Description |
|-----|-----|
| Text Exporter | A text exporter (represented by the [ITextExporter](xref:ActiproSoftware.Text.Exporters.ITextExporter) interface) is an object that can export text to other output formats, such as HTML or RTF. |

## Text (Lexing) Terms

<table>
<tbody>

<tr>
<td>Lexer</td>
<td>

A lexer (represented by the [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) interface) scans text and tokenizes it, meaning that a series of tokens are assigned to text ranges that identifies in detail what the text represents.

</td>
</tr>

<tr>
<td>Lexical Target</td>
<td>

A lexical target (represented by the [ILexerTarget](xref:ActiproSoftware.Text.Lexing.ILexerTarget) interface) is an object that is passed to a lexer and is updated by the lexer during its execution to provide lexing results.

</td>
</tr>

<tr>
<td>Lexical Scope</td>
<td>

A lexical scope (represented by the [ILexicalScope](xref:ActiproSoftware.Text.Lexing.ILexicalScope) interface) defines how a parent lexical state can transition to another child lexical state and back.

</td>
</tr>

<tr>
<td>Lexical State</td>
<td>

A lexical state (represented by the [ILexicalState](xref:ActiproSoftware.Text.Lexing.ILexicalState) interface) is a context in which certain text can be parsed into a set of tokens.

</td>
</tr>

<tr>
<td>Lexical State ID Provider</td>
<td>

A lexical state ID provider (represented by the [ILexicalStateIdProvider](xref:ActiproSoftware.Text.Lexing.ILexicalStateIdProvider) interface) is a class that is useful for providing all the lexical state IDs for a language.

</td>
</tr>

<tr>
<td>Lexical State Transition</td>
<td>

A lexical state transition (represented by the [ILexicalStateTransition](xref:ActiproSoftware.Text.Lexing.ILexicalStateTransition) interface) specifies a transition that can occur from one lexical state to another, typically in a separate language (i.e. HTML to CSS).

</td>
</tr>

<tr>
<td>Mergable Lexer</td>
<td>

A mergable lexer (represented by the [IMergableLexer](xref:ActiproSoftware.Text.Lexing.IMergableLexer) interface) is a lexer that supports transitioning into other languages that have mergable lexers.

</td>
</tr>

<tr>
<td>Mergable Lexical Result</td>
<td>

A mergable lexer result (represented by the [MergableLexerResult](xref:ActiproSoftware.Text.Lexing.MergableLexerResult) class) provides information for the next token that has been read by a mergable lexer.

</td>
</tr>

<tr>
<td>Mergable Token</td>
<td>

A mergable token (represented by the [IMergableToken](xref:ActiproSoftware.Text.Lexing.IMergableToken) interface) is a token that is created by a mergable lexer.

</td>
</tr>

<tr>
<td>Mergable Token Lexer Data</td>
<td>

Mergable token lexical parse data (represented by the [IMergableTokenLexerData](xref:ActiproSoftware.Text.Lexing.IMergableTokenLexerData) interface) is part of a mergable lexical parse result and gets assigned to mergable tokens.

</td>
</tr>

<tr>
<td>Token</td>
<td>

A token (represented by the [IToken](xref:ActiproSoftware.Text.Lexing.IToken) interface) is a span of text that has some sort of lexical parse data associated with it.

</td>
</tr>

<tr>
<td>Token ID Provider</td>
<td>

A token ID provider (represented by the [ITokenIdProvider](xref:ActiproSoftware.Text.Lexing.ITokenIdProvider) interface) is a class that is useful for providing all the token IDs for a language.

</td>
</tr>

<tr>
<td>Token Set</td>
<td>

A token set (represented by the [ITokenSet](xref:ActiproSoftware.Text.Lexing.ITokenSet) interface) is a group of tokens that are returned by an token tagger.

</td>
</tr>

</tbody>
</table>

## Text (Parsing) Terms

| Term | Description |
|-----|-----|
| Ambient Parse Request Dispatcher Provider | An ambient parse request dispatcher provider (represented by the [AmbientParseRequestDispatcherProvider](xref:ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider) class) specifies an object that provides the default [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) to use, which allows for multi-threaded parsing. |
| Parse Data | Parse data (represented by the [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData) interface) specifies the data that is returned by an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) to a document when parsing is completed. |
| Parse Error | A parse error (represented by the [IParseError](xref:ActiproSoftware.Text.Parsing.IParseError) interface) specifies an error that was encountered when performing parsing. |
| Parse Error Level | A parse error level (represented by the [ParseErrorLevel](xref:ActiproSoftware.Text.Parsing.ParseErrorLevel) enumeration) specifies the severity of a parse error. |
| Parse Error Provider | A parse error provider (represented by the [IParseErrorProvider](xref:ActiproSoftware.Text.Parsing.IParseErrorProvider) interface) specifies an object that can return parse errors for a parsing operation.  This interface is typically implemented by parse data objects. |
| Parser | A parser (represented by the [IParser](xref:ActiproSoftware.Text.Parsing.IParser) interface) specifies an object that can perform syntax and/or semantic parsing. |
| Parse Request | A parse request (represented by the [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest) interface) specifies an object that designates a request for a parsing operation, and can be queued by a parse request dispatcher |
| Parse Request Dispatcher | A parse request dispatcher (represented by the [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) interface) specifies an object that is capable of queuing up parse requests and performing them on worker threads. |
| Parse Error State | A parse error state (represented by the [ParseRequestState](xref:ActiproSoftware.Text.Parsing.ParseRequestState) enumeration) specifies the current state of a parse request, such as whether it is queued, executing, etc. |
| Parse Target | A parse target (represented by the [IParseTarget](xref:ActiproSoftware.Text.Parsing.IParseTarget) interface) specifies an object that is notified with parse data results when a parsing operation completes. |

## Text (Regular Expressions) Terms

| Term | Description |
|-----|-----|
| Character Class | A character class (represented by the [CharClass](xref:ActiproSoftware.Text.RegularExpressions.CharClass) class) specifies a set of character ranges. |
| Character Interval | A character interval (represented by the [CharInterval](xref:ActiproSoftware.Text.RegularExpressions.CharInterval) structure) specifies a range of characters. |

## Text (Searching) Terms

| Term | Description |
|-----|-----|
| Search Capture | A search capture (represented by the [ISearchCapture](xref:ActiproSoftware.Text.Searching.ISearchCapture) interface) denotes a single regular expression capture made during a find operation. |
| Search Options | Search options (represented by the [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) interface) contains the settings used during find/replace operations, such as the text to find, whether to match case, etc. |
| Search Pattern Provider | A search pattern provider (represented by the [ISearchPatternProvider](xref:ActiproSoftware.Text.Searching.ISearchPatternProvider) interface) is an object that converts find/replace pattern syntax to a format that is compatible with the search engine.  Built-it search pattern providers include normal, regular expression, wildcard, etc. |
| Search Result | A search result (represented by the [ISearchResult](xref:ActiproSoftware.Text.Searching.ISearchResult) interface) is a single result from a search operation that is included within a result set. |
| Search Result Set | A search result set (represented by the [ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet) interface) is an object that provides the results of a search operation. |
| Search Operation Type | Indicates a type of search operation, such as find next or replace all, and is represented by the [SearchOperationType](xref:ActiproSoftware.Text.Searching.SearchOperationType) enumeration. |

## Text (Tagging) Terms

| Term | Description |
|-----|-----|
| Classification Tag | A classification tag (represented by the [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) interface) is a tag that specifies an [IClassificationType](xref:ActiproSoftware.Text.IClassificationType), and when used in an editor, tells the editor how to syntax highlight text based on related highlighting styles. |
| Tag | A tag (represented by the [ITag](xref:ActiproSoftware.Text.Tagging.ITag) interface) is an object that can store data and be applied over a range of text via a tagger. |
| Tag Aggregator | A tag aggregator (represented by the [ITagAggregator<T>](xref:ActiproSoftware.Text.Tagging.ITagAggregator`1) interface) collects tags of a certain type from all the available taggers. |
| Tag Snapshot Range | A tag snapshot range (represented by the [TagSnapshotRange<T>](xref:ActiproSoftware.Text.Tagging.TagSnapshotRange`1) structure) specifies a text snapshot range and an [ITag](xref:ActiproSoftware.Text.Tagging.ITag) that is applied over the range. |
| Tag Version Range | A tag version range (represented by the [TagVersionRange<T>](xref:ActiproSoftware.Text.Tagging.TagVersionRange`1) structure) specifies a text version range and an [ITag](xref:ActiproSoftware.Text.Tagging.ITag) that is applied over the range. |
| Tagger | A tagger (represented by the [ITagger<T>](xref:ActiproSoftware.Text.Tagging.ITagger`1) interface) provides tag snapshot ranges that are within specified snapshot ranges. |
| Tagger Provider | A tagger provider (represented by the [ICodeDocumentTaggerProvider](xref:ActiproSoftware.Text.Tagging.ICodeDocumentTaggerProvider) and [ITextViewTaggerProvider](xref:ActiproSoftware.Text.Tagging.ITextViewTaggerProvider) interfaces) is a language service that can create taggers for documents/views that use the language. |
| Token Tag | A token tag (represented by the [ITokenTag](xref:ActiproSoftware.Text.Tagging.ITokenTag) interface) is a tag that specifies an [IToken](xref:ActiproSoftware.Text.Lexing.IToken). |
| Token Tagger | A token tagger (represented by the [TokenTagger](xref:ActiproSoftware.Text.Tagging.Implementation.TokenTagger) interface) is a special tagger that provides incremental lexing capabilities to an [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer). |

## Text (Undo) Terms

| Term | Description |
|-----|-----|
| Save Point Change Type | Indicates the save point change type (none, unsaved changes, or saved changes) for a line within the current snapshot of a document, and is represented by the [SavePointChangeType](xref:ActiproSoftware.Text.Undo.SavePointChangeType) enumeration. |
| Undoable Text Change | An undoable text change (represented by the [IUndoableTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChange) interface) is used to track information for an [ITextChange](xref:ActiproSoftware.Text.ITextChange) that has been placed into the undo history. |
| Undoable Text Change Stack | An undoable text change stack (represented by the [IUndoableTextChangeStack](xref:ActiproSoftware.Text.Undo.IUndoableTextChangeStack) interface) is a stack that stores [IUndoableTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChange) entries for either the undo or redo stacks. |
| Undo History | An undo history (represented by the [IUndoHistory](xref:ActiproSoftware.Text.Undo.IUndoHistory) interface) provides access to all the undo/redo functionality for an [ITextDocument](xref:ActiproSoftware.Text.ITextDocument). |
