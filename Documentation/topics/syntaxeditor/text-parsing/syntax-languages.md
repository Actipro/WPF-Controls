---
title: "Syntax Languages"
page-title: "Syntax Languages - SyntaxEditor Text/Parsing Framework"
order: 3
---
# Syntax Languages

Syntax languages are objects that can be assigned to any [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) that provide advanced functionality for working with the text content of the document.  This functionality can be simple such as determining where word breaks are, to more complex such as tokenization (lexing) of text, or parsing the text into an abstract syntax tree.

## The ISyntaxLanguage Interface (Service Locator Architecture)

The [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) interface, implemented by any syntax language is designed to be completely open and extensible.  The main requirement it has is that a string-based language key is provided that identifies the language.  The other requirement is that it supports the service locator design pattern.

The service locator design pattern is somewhat like a dictionary object.  Any object can be assigned as a "service" to the language using a `Type`-based key.  There are a number of built-in feature service types that the text/parsing framework and products like SyntaxEditor look for, such as [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer), [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter), [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder), etc.  Then there are provider services that create/return various objects on-demand, and event sink services that allow you to be notified when various document and UI events occur that can be intercepted and handled by the language.

> [!NOTE]
> A complete list of the built-in service types, along with a more in-depth look at how the service locator architecture works, is given in the [Service Locator Architecture](../language-creation/service-locator-architecture.md) topic.

### A Real Service Type Example

Each one of these services provide some functionality that can be requested at any time.  As a real example, say we have created an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) (see the [Scanning Text Using a Reader](core-text/scanning-text.md) topic for details), and want to jump to the start of a word.  The implementation of the [GoToCurrentWordStart](xref:ActiproSoftware.Text.ITextSnapshotReader.GoToCurrentWordStart*) method checks with the language currently assigned to the document to see if an [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) service has been registered.  If so, it uses that object to find the start of the word.  Otherwise it falls back to a default word break finder.

Thus via the use fo the service locator architecture, we can register (or customize) built-in functionality for a particular language.

### A Custom Service Type Example

Any custom service types can be registered too.  Say you made a service that returns an object which has a method indicating if a passed filename is supported by the language.  If our language was Java and the passed filename had a *.java* extension, we'd return true.  You could register similar functionality on all the languages you use and publish this service as a type called `IFilenameSupported`.  Then in your application where you maintained a list of available languages, when a file was opened you could enumerate your list of languages, get the `IFilenameSupported` service, pass the filename to its method, and see if `true` was returned.  When you encounter a `true` return value, you know that language supports the file.

## Creating, Loading, and Using a Language

The [Language Creation Guide](../language-creation/index.md) has topics that cover how to create, load, and use syntax languages.  It also goes into detail about the service locator architecture, event sinks, the available built-in service types, and more.

In addition, we provide a [Language Designer](../language-designer-tool/index.md) application that makes it easy to get started building a custom syntax language.  That tool lets you configure information about your language and then it generates language-related code that can be used in your applications.  With the application's help, you can be up and running in a few minutes.

## Notification of Document Language Changes

The [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[LanguageChanged](xref:ActiproSoftware.Text.ICodeDocument.LanguageChanged) event fires whenever the document's [Language](xref:ActiproSoftware.Text.ICodeDocument.Language) property changes.  Its event arguments pass the old and newly-assigned languages.

## Reusing Language Instances

It is encouraged to reuse [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) instances among multiple documents that are of the same language.  For instance, if your application has multiple instances of a SyntaxEditor control and your IDE edits XML documents, load your XML syntax language one time and apply the same instance to each document.  This saves on memory and reduces load time.
