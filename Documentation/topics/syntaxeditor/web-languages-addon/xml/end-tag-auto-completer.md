---
title: "End Tag Auto-Completer"
page-title: "End Tag Auto-Completer - XML Language - SyntaxEditor Web Languages Add-on"
order: 6
---
# End Tag Auto-Completer

An end-tag auto-completer is an object that can be called to attempt to insert an end tag such as when `>` is typed by the end user.

The [IXmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.IXmlEndTagAutoCompleter) interface provides the base requirements for this functionality.  It is installed into a language as a "feature" language service.

The [XmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlEndTagAutoCompleter) class is the default implementation of the [IXmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.IXmlEndTagAutoCompleter) interface.

## Integration with Schema Resolver

The auto-completer uses contextual information to determine whether an element should be closed without content or whether an end tag is appropriate.  This contextual information is found by examining resolved schema element data passed in the [XML context](context.md).  Therefore a [schema resolver](schema-resolver.md) should be configured and installed on the lanugage to achieve proper results.

If not schema data is found for the current element being closed, then an end tag will insert.

## Registering with a Syntax Language

Any object that implements [IXmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.IXmlEndTagAutoCompleter) can be associated with a syntax language by registering it as an [IXmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.IXmlEndTagAutoCompleter) service on the language.

The [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) class automatically registers a [XmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlEndTagAutoCompleter) with itself when it is created, so normally end tag auto-completers never need to be set on an XML language unless a custom one is made.

This code creates a custom end tag auto-completer (defined in a make-believe `CustomXmlEndTagAutoCompleter` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterXmlEndTagAutoCompleter(new CustomXmlEndTagAutoCompleter());
```

> [!NOTE]
> The [XmlSyntaxLanguageExtensions](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions).[RegisterXmlEndTagAutoCompleter](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions.RegisterXmlEndTagAutoCompleter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text.Languages.Xml` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
