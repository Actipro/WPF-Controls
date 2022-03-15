---
title: "Service Locator Architecture"
page-title: "Service Locator Architecture - SyntaxEditor Language Creation Guide"
order: 5
---
# Service Locator Architecture

Syntax languages are architected using the Service Locator design pattern, which keeps the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) interface small while still allowing for powerful extensibility of features.

Language features such as lexing, token tagger providers, line commenters, etc. can be registered as services on each language instance.  Certain service types are well known and extension methods provide easy access to the built-in feature services.  Any sort of custom service can be registered with a language and retrieved later as well.

## What is the Service Locator Design Pattern?

Our implementation of the Service Locator designer pattern contains four types of methods, two types for registering and unregistering services with a language, and two types for retrieving registered services from a language.

The [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).[RegisterService](xref:ActiproSoftware.Text.Utility.IServiceLocator.RegisterService*) and [UnregisterService](xref:ActiproSoftware.Text.Utility.IServiceLocator.UnregisterService*) methods are used to register and unregister services with a language.  Once a service has been registered with a language, it can be retrieved via the [GetService](xref:ActiproSoftware.Text.Utility.IServiceLocator.GetService*) method.  That method requires you to pass the service type you are trying to retrieve (see built-in service types table below).  To get a collection of all service types that have been registered with a language, use the [GetAllServiceTypes](xref:ActiproSoftware.Text.Utility.IServiceLocator.GetAllServiceTypes*) method.

The methods described above are generic and allow for any sort of service to be registered with a language.  Helper extension methods are available that wrap these generic calls and make it easy to work with the built-in feature service types.  Examples of both the extension method calls and generic service method calls are included later in this topic.

> [!NOTE]
> If no type parameter is passed to the [RegisterService](xref:ActiproSoftware.Text.Utility.IServiceLocator.RegisterService*) method, then the service being registered will be registered under its own `Type` as the key.

## Built-In Service Types

While you can customize a syntax language with any custom services you like, there are a number of built-in service types are that recognized, and they come in three varieties.

### Feature Service Types

Many service types require that a service providing a certain function are registered using a specific `Type` key.  These service types are referred to as "feature services", and only one instance of these service types can be registered on a language.

For instance, in order for our code to locate the lexer for the language, we require that the service implementing [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) is registered with the service `Type` key [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer).  If the service is registered under any other key, then our code can't locate it.

This table lists the built-in single-instance service types that can be used when adding features to an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  The Service Type column indicates the service `Type` key that must be used when registering them.  Only one of these services may be registered per language instance.

| Service Type | Description |
|-----|-----|
| [IAutoCorrector](xref:ActiproSoftware.Text.Analysis.IAutoCorrector) | Designates the [auto-corrector](feature-services/auto-corrector.md) that can perform additional edits after text changes, such as auto-case correcting language keywords. |
| [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) | Designates the [delimiter auto-completer](feature-services/delimiter-auto-completer.md) that can insert end delimiters when a start delimiter is typed by the end user. |
| [IExampleTextProvider](xref:ActiproSoftware.Text.IExampleTextProvider) | Designates the [example text provider](feature-services/example-text.md) that returns a code snippet which can be displayed in an application to show sample code syntax. |
| [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) | Designates the [indent provider](feature-services/indent-provider.md) that performs automatic indentation of text when the Enter key is pressed. |
| [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) | Designates the [lexer](feature-services/lexer.md) that tokenizes text for the language. |
| [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) | Designates the [line commenter](feature-services/line-commenter.md) that is capable of commenting and uncommenting lines or a range of text. |
| [INavigableSymbolProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider) | Designates the [navigable symbol provider](feature-services/navigable-symbol-provider.md) that lists the accessible symbols within a document. |
| [IOutliner](xref:@ActiproUIRoot.Controls.SyntaxEditor.Outlining.IOutliner) | Designates the [outliner](feature-services/outliner.md) that allows a language to provide automatic outlining (code folding) for a document. |
| [IParser](xref:ActiproSoftware.Text.Parsing.IParser) | Designates the [parser](feature-services/parser.md) that performs syntax and/or semantic analysis on code and often returns parse data such as an abstract syntax tree (AST), list of syntax errors, symbol table, etc. |
| [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) | Designates the [structure matcher](feature-services/structure-matcher.md) that can locate matching structural text delimiters, such as brackets. |
| [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) | Designates the [text formatter](feature-services/text-formatter.md) that adjusts whitespace and other symbols such as braces on a specified text range to make code more readable. |
| [ITextStatisticsFactory](xref:ActiproSoftware.Text.ITextStatisticsFactory) | Designates the [text statistics factory](feature-services/text-statistics.md) that is capable of creating customized [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) objects for a language. |
| [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) | Designates the [word break finder](feature-services/word-break-finder.md) that locates word starts and ends for the language. |

### Provider Service Types

The next variety of service types is related to being able to provide data for a language.  Unlike "feature" services, more than one "provider" service can be registered on a language.

It doesn't matter which service `Type` key a service is registered under.  Therefore if any registered service implements one of these interfaces, it will be used.  But be careful to not register more than one service with the same `Type` key, or else the existing service that with `Type` key will be removed.

This table lists the built-in multiple-instance service types that can be used when adding providers to an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  The Service Type column indicates the interface that must be implemented by the service for it to be used, however as mentioned above, the service can be registered using any `Type` key.

| Service Type | Description |
|-----|-----|
| [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider) | Designates the [code snippet provider](provider-services/code-snippet-provider.md) that can open display available code snippets for selection and activate a [code snippet template session](../user-interface/intelliprompt/code-snippets.md) within an editor view. |
| [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) | Designates the [completion provider](provider-services/completion-provider.md) that can open an [IntelliPrompt completion session](../user-interface/intelliprompt/completion-list.md) within an editor view, generally initiated by a `Ctrl+Space`. |
| [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider) | Designates the [parameter info provider](provider-services/parameter-info-provider.md) that can open an [IntelliPrompt parameter info session](../user-interface/intelliprompt/parameter-info.md) within an editor view. |
| [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) | Designates the [quick info provider](provider-services/quick-info-provider.md) that can open an [IntelliPrompt quick info session](../user-interface/intelliprompt/quick-info.md) within an editor view, generally initiated by mouse hovering. |

### Event Sink Service Types

The final variety of service types is called "event sinks" and these service types receive various event notifications and process them.  Events could be anything from editor view input events to document text change events.

Event sink service types are registered similarly to the rules described in the Provider Service Types section above, where multiple event sinks that have the same interfaces can be registered on a language.  To sum up, if any object registered as a service implements one or more of the event sink interfaces, it will be notified when related events occur.

> [!NOTE]
> See the [Event Sinks](event-sinks.md) topic for a list of the built-in event sink service types and what each can do.

## Helper Extension Methods, and Requirements for Usage

The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions) static class contains numerous extension methods that are available on any [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) instance when the `ActiproSoftware.Text` namespace is imported.  These extension methods wrap the generic service locator methods and implement Register, Unregister, and Get methods for each of the built-in "feature" service types described above.

While it is not necessary to use them since you can just use the core generic service locator methods, they keep your code cleaner and give you some prompting via the Visual Studio member list for which service types are available.

## Service Add/Remove Events

The [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) interface defines two events that provide notifications of when services are added to or removed from a language.

The [ServiceAdded](xref:ActiproSoftware.Text.Utility.IServiceLocator.ServiceAdded) event fires whenever a service is added to the language.  Likewise the [ServiceRemoved](xref:ActiproSoftware.Text.Utility.IServiceLocator.ServiceRemoved) event fires whenever a service is removed from the language.

## Sample: Registering a Lexer

This section shows some examples of registering a lexer feature service with a language, both via an extension method and generically.

This code registers a lexer in the `myLexer` variable with a language using a helper extension method:

```csharp
language.RegisterLexer(myLexer);
```

This code registers a lexer in the `myLexer` variable with a language generically:

```csharp
language.RegisterService<ILexer>(myLexer);
```

## Sample: Retrieving a Lexer

This section shows some examples of retrieving a registered lexer service from a language, both via an extension method and generically.

This code retrieves a lexer from a language using a helper extension method:

```csharp
ILexer lexer = language.GetLexer();
```

This code retrieves a lexer from a language generically:

```csharp
ILexer lexer = language.GetService<ILexer>();
```

## Sample: Registering a Completion Provider

This section shows how to register an object as a service that implements [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider).  Note that since languages can have mutiple objects registered that implement this interface, we will register the service using the object's type.

This code registers an object of type `CustomCompletionProvider` in the `myCompletionProvider` variable with a language, using its type:

```csharp
language.RegisterService<CustomCompletionProvider>(myCompletionProvider);
```

This code does the same thing but the `Type` key is automatically pulled via reflection, based on the `Type` of the object referenced by `myCompletionProvider`.  Note that this shortcut cannot be used with "feature" services because those must be registered using the specific interface types described in the table above.

```csharp
language.RegisterService(myCompletionProvider);
```

## Sample: Retrieving a Completion Provider

This section shows how to retrieve the `CustomCompletionProvider` class instance that was registered in the previous sample.

```csharp
CustomCompletionProvider provider = language.GetService<CustomCompletionProvider>();
```
