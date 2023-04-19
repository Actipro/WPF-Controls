---
title: "Getting Started"
page-title: "Getting Started - XML Language - SyntaxEditor Web Languages Add-on"
order: 2
---
# Getting Started

This topic covers how to get started using the XML language from the Web Languages Add-on, implemented by the [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) class, and lists its requirements for supporting advanced features like parsing and automated IntelliPrompt.

It is very important to follow the steps in this topic to configure the language correctly so that its advanced features operate as expected.

## Configure the Ambient Parse Request Dispatcher

The language's parser does a lot of processing when text changes occur.  To ensure that these parsing operations are offloaded into a worker thread that won't affect the UI performance, you must set up a parse request dispatcher for your application.

The ambient parse request dispatcher should be set up in your application startup code as described in the [Parse Requests and Dispatchers](../../text-parsing/parsing/parse-requests-and-dispatchers.md) topic.

@if (winrt) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	AmbientParseRequestDispatcherProvider.Dispatcher = new TaskBasedParseRequestDispatcher();
	...
}
```

}

@if (wpf winforms) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	AmbientParseRequestDispatcherProvider.Dispatcher = new ThreadedParseRequestDispatcher();
	...
}
```

}

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

> [!IMPORTANT]
> Failure to set up an ambient parse request dispatcher when using the language will result in unnecessary UI slowdown since parse operations will be performed in the UI thread instead of in a worker thread.

## Configure the XmlSchemaResolver

Next, create a [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) instance and use the methods described in the [Schema Resolver](schema-resolver.md) topic to initialize it with one or more XSD files.  If you use DTDs for validation instead, you can ignore this step.

The [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) is used to provide data consumed for supporting automated IntelliPrompt and validation, so it is essential that you configure it with a schema data that describes the XML that will be edited.

XSD files can be found on the Internet for common XML formats, and custom XSDs can be created for any other XML format.  The Internet has a lot of great information on how to create XSD files.  Please do Internet searches if you need assistance building your own XSD files.

This code creates a [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) and loads an XSD file that is located on the hard drive at the path specified by the `path` variable:

```csharp
XmlSchemaResolver resolver = new XmlSchemaResolver();
resolver.LoadSchemaFromFile(path);
```

## Configure DTD Support

We recommend that if you have the option, you use XML schemas (XSD files) instead of DTDs to provide validation for your XML.  XML schemas also enable automated IntelliPrompt features like completion lists that aren't available when using DTDs.

If you do wish to support DTDs for validation, you must set an option on the [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) language service and then register it on the language.  It is not enabled by default since DTDs can sometimes attempt to access external resources.

This code creates a [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) that allows DTD validation support:

```csharp
XmlValidator validator = new XmlValidator();
validator.Mode = XmlValidatorMode.Dtd;
```

> [!NOTE]
> A [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) language service is registered on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default but it has the default [Mode](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator.Mode) property value of `XmlValidatorMode.Schema`.

## Configure the XmlSyntaxLanguage

The next step is to create an instance of the [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) class and register the [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) as a "feature" service on it so that other language services can consume the schema data.  If DTD validation support is desired, you can also register the [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) as a "feature" service.

This code creates a [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) and registers the [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) service:

```csharp
XmlSyntaxLanguage language = new XmlSyntaxLanguage();
language.RegisterXmlSchemaResolver(resolver);  // If XML schema support is desired
language.RegisterXmlValidator(validator);  // If DTD validation support is desired
```

> [!NOTE]
> The [XmlSyntaxLanguageExtensions](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions).[RegisterXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions.RegisterXmlSchemaResolver*) and [XmlSyntaxLanguageExtensions](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions).[RegisterXmlValidator](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions.RegisterXmlValidator*) methods in the code snippet above are helper extension methods that get added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text.Languages.Xml` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Use the XmlSyntaxLanguage

The final step is to use the language on the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) instances that will be editing XML.

This code applies the language to a document in a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor), whose instance is in the `editor` variable:

```csharp
editor.Document.Language = language;
```

## Assembly Requirements

The following list indicates the assemblies that are used with the XML syntax language implementation in this add-on.

| Assembly | Required | Author | Licensed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Text.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core text/parsing framework for @@PlatformName |
| ActiproSoftware.Text.LLParser.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | LL parser framework implementation |
| ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core framework for all Actipro @@PlatformName controls |
| ActiproSoftware.SyntaxEditor.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | SyntaxEditor for @@PlatformName control |
| ActiproSoftware.Text.Addons.Xml.@@PlatformAssemblySuffix.dll | Yes | Actipro | Web Languages Add-on | Core text/parsing for the XML language |
| ActiproSoftware.SyntaxEditor.Addons.Xml.@@PlatformAssemblySuffix.dll | Yes | Actipro | Web Languages Add-on | SyntaxEditor for @@PlatformName advanced XML syntax language implementation |
