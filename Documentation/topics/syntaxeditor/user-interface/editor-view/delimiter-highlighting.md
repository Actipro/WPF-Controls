---
title: "Delimiter Highlighting"
page-title: "Delimiter Highlighting - SyntaxEditor Editor View Features"
order: 12
---
# Delimiter Highlighting

SyntaxEditor supports highlighting of the delimiter pairs that are currently next to the caret, also known as bracket highlighting.

While delimiter pairs such as `{...}`, and `(...)` are most commonly highlighted, delimiter highlighting also supports the highlighting of more than two delimiters at a time, such as when there are `#if...#else...#endif` blocks.

## Activating Delimiter Highlighting

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[IsDelimiterHighlightingEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsDelimiterHighlightingEnabled) property can be set to `true` to allow SyntaxEditor to render delimiter highlighting, as long as the current syntax language supports the feature.  Delimiter highlighting is enabled on SyntaxEditor by default.

## Required Language Services

The current syntax language must have two services registered so that delimiter highlighting can render: a structure matcher and a delimiter highlight tagger.

### Structure Matcher

A structure matcher service contains the core logic for examining a document snapshot to see if any delimiters are next to a given offset.  The delimiter highlighting feature calls this structure matcher and passes it the caret's offset.  The matcher's results are passed back and if there are valid delimiter results next to the caret, they can be highlighted.

See the [Structure Matching](../../text-parsing/advanced-text/structure-matching.md) topic for detail on how structure matchers work and how to register one with the language.

### Delimiter Highlight Tagger

A special [DelimiterHighlightTagger](xref:ActiproSoftware.Text.Tagging.Implementation.DelimiterHighlightTagger) class has been implemented that watches for text/selection changes within SyntaxEditor and calls the language's structure matcher to look for delimiter pairs as appropriate.  When matcher results are found, the ranges are tagged and highlights are rendered by an internal adornment manager.

Without the tagger being registered on the language, no highlights will show for delimiters.

This code shows how to register the tagger with the language:

```csharp
language.RegisterService(new TextViewTaggerProvider<DelimiterHighlightTagger>(typeof(DelimiterHighlightTagger)));
```

## Performance Optimizations

The delimiter highlight mechanism can take time to execute when editing larger documents.  This can directly have a negative performance impact on caret movement and typing speed.  Thus this feature has been designed to do nearly all the work in worker threads, and utilizes the multi-threaded parser capabilities described in the [Parse Requests and Dispatchers](../../text-parsing/parsing/parse-requests-and-dispatchers.md) topic.

It is very important that an ambient parse request dispatcher is configured per the instructions in that topic, otherwise the delimiter highlighting feature, when enabled, will run in the UI thread and negatively affect performance.

## Changing the Highlight Brush

The brush used to render the highlight can be adjusted by the end user since it is exposed via a special classification type's style in the highlighting style registry.

See the "Special Classification Types" section in the [Highlighting Style Registries](../styles/highlighting-style-registries.md) topic for more information on how to modify the style.
