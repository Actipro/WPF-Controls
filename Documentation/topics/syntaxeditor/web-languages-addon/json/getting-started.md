---
title: "Getting Started"
page-title: "Getting Started - JSON Language - SyntaxEditor Web Languages Add-on"
order: 2
---
# Getting Started

This topic covers how to get started using the JSON language from the Web Languages Add-on, implemented by the [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) class, and lists its requirements for supporting advanced features like parsing and code outlining.

It is very important to follow the steps in this topic to configure the language correctly so that its advanced features operate as expected.

## Configure the Ambient Parse Request Dispatcher

The language's parser does a lot of processing when text changes occur.  To ensure that these parsing operations are offloaded into a worker thread that won't affect the UI performance, you must set up a parse request dispatcher for your application.

The ambient parse request dispatcher should be set up in your application startup code as described in the [Parse Requests and Dispatchers](../../text-parsing/parsing/parse-requests-and-dispatchers.md) topic.

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	AmbientParseRequestDispatcherProvider.Dispatcher = new ThreadedParseRequestDispatcher();
	...
}
```

Likewise it should be shut down on application exit, also as described in the [Parse Requests and Dispatchers](../../text-parsing/parsing/parse-requests-and-dispatchers.md) topic.

```csharp
protected override void OnExit(ExitEventArgs e) {
	...
	var dispatcher = AmbientParseRequestDispatcherProvider.Dispatcher;
	if (dispatcher != null) {
		AmbientParseRequestDispatcherProvider.Dispatcher = null;
		dispatcher.Dispose();
	}
	...
}
```

> [!NOTE]
> Failure to set up an ambient parse request dispatcher when using the language will result in unnecessary UI slowdown since parse operations will be performed in the UI thread instead of in a worker thread.

## Configure the JsonSyntaxLanguage

The next step is to create an instance of the [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) class.

This code creates a [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage):

```csharp
var language = new JsonSyntaxLanguage();
```

> [!NOTE]
> If your JSON data needs to support JavaScript style comments (such as when editing JSONC files), pass `true` to the [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) constructor overload that indicates whether comments should be supported by the lexer.

## Use the JsonSyntaxLanguage

Next, use the language on the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) instances that will be editing JSON code.

This code applies the language to a document in a [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor), whose instance is in the `editor` variable:

```csharp
editor.Document.Language = language;
```

> [!NOTE]
> We recommend reusing your [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) instance among all the documents in your application that are editing JSON code.  This saves on overall memory usage and reduces load times.

## Assembly Requirements

The following list indicates the assemblies that are used with the JSON syntax language implementation in this add-on.

| Assembly | Required | Author | Licensed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Text.Wpf.dll | Yes | Actipro | SyntaxEditor | Core text/parsing framework for WPF |
| ActiproSoftware.Text.LLParser.Wpf.dll | Yes | Actipro | SyntaxEditor | LL parser framework implementation |
| ActiproSoftware.Shared.Wpf.dll | Yes | Actipro | SyntaxEditor | Core framework for all Actipro WPF controls |
| ActiproSoftware.SyntaxEditor.Wpf.dll | Yes | Actipro | SyntaxEditor | SyntaxEditor for WPF control |
| ActiproSoftware.Text.Addons.JavaScript.Wpf.dll | Yes | Actipro | Web Languages Add-on | Core text/parsing for the JavaScript and JSON languages |
| ActiproSoftware.SyntaxEditor.Addons.JavaScript.Wpf.dll | Yes | Actipro | Web Languages Add-on | SyntaxEditor for WPF advanced JavaScript and JSON syntax language implementations |
