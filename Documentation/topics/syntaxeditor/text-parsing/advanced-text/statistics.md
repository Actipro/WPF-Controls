---
title: "Text Statistics"
page-title: "Text Statistics - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 4
---
# Text Statistics

Text statistics are a powerful feature that provide statistics about text in a document.  By default, there are numerous text statistics available, even including things like readability scores.  Syntax languages can also choose to customize the statistics by supplying additional ones such as commented-line counts, etc.

## Built-In Statistics

The [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) interface calculates and returns statistics.  It includes many built-in statistics such as:

- Line count
- Whitespace line count
- Sentence count
- Word count
- Syllable count
- Character count
- Whitespace character count
- Letter count
- Consonant count
- Vowel count
- Average words / sentence
- Average syllables / word
- Average letters / word
- Flesch reading ease score
- Flesch-Kincaid grade level

See the [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) class library documentation for a list of the statistic properties on the interface.

## Getting Statistics for a Snapshot

The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[GetTextStatistics](xref:ActiproSoftware.Text.ITextSnapshot.GetTextStatistics*) method can be called to retrieve the [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) for the snapshot.  If an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) is attached to the snapshot's document, it will be used to generate the statistics.  This is where custom language-specific statistics can be retrieved if they have been created.

This code shows how to get the statistics for the current snapshot of a document:

```csharp
ITextStatistics stats = document.CurrentSnapshot.GetTextStatistics();
```

## Getting Statistics for a String

Text statistics can also be generated for a `String` variable by creating an instance of the [TextStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics) class.

This code shows how to generate the built-in statistics for a `String` variable named `text`:

```csharp
ITextStatistics stats = new TextStatistics(text);
```

## Registering a Custom Statistics Factory for a Syntax Language

In some syntax languages, some of those statistics may not be useful or you may wish to add other statistics, such as comment coverage.  Adding new statistics is easy.

First, make a class that inherits [TextStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics).

Second, in your [TextStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics) class, calculate your additional statistics in the constructor.

Third, override the [GetRawStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics.GetRawStatistics*) method so that the additional statistics are added to the returned list.  You can optionally remove built-in statistics here too.  The returned list has items of type [ITextStatistic](xref:ActiproSoftware.Text.ITextStatistic).  The [TextStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics).[CreateStatistic](xref:ActiproSoftware.Text.Implementation.TextStatistics.CreateStatistic*) static method can be used to create items to add to the list.

Fourth, make a class that implements [ITextStatisticsFactory](xref:ActiproSoftware.Text.ITextStatisticsFactory) by building a [CreateStatistics](xref:ActiproSoftware.Text.ITextStatisticsFactory.CreateStatistics*) method.  This method should return an instance of the [TextStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics)-based class you created in the previous steps.

Finally, we you need to associate the [ITextStatisticsFactory](xref:ActiproSoftware.Text.ITextStatisticsFactory) with your language so that the custom text statistics can be used.  An [ITextStatisticsFactory](xref:ActiproSoftware.Text.ITextStatisticsFactory) can be associated with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) by registering it with the language:

```csharp
language.RegisterService<ITextStatisticsFactory>(myTextStatisticsFactory);
```

Your custom statistics should be ready for use after that.
