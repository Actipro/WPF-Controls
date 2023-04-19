---
title: "Validator"
page-title: "Validator - XML Language - SyntaxEditor Web Languages Add-on"
order: 5
---
# Validator

A Validator executes immediately after the main XML parser runs (still in the parser worker thread) if there are no parser syntax errors found.  The validator then scans the XML to ensure that it adheres to the loaded schemas and/or DTDs, and places any validation errors in the [IXmlParseData](xref:ActiproSoftware.Text.Languages.Xml.Implementation.IXmlParseData).[Errors](xref:ActiproSoftware.Text.Parsing.IParseErrorProvider.Errors) collection that is returned by the parser.

The [IXmlValidator](xref:ActiproSoftware.Text.Languages.Xml.IXmlValidator) interface provides the base requirements for this functionality.  It is installed into a language as a "feature" language service.

The [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) class is the default implementation of the [IXmlValidator](xref:ActiproSoftware.Text.Languages.Xml.IXmlValidator) interface.  By default, it is not set up to allow DTD-based validation since DTDs sometimes access external resources.

## Integration with Schema Resolver

The default validator is set up to use the XML schema data in the [schema resolver](schema-resolver.md) to ensure that the XML conforms to the schemas.

## Enabling DTD Validation

As mentioned above, the [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) doesn't allow DTD validation by default since DTD validation can sometimes access external resources.  To enable support for DTDs, set the [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator).[Mode](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator.Mode) property to `XmlValidationMode.Dtd` to only use DTDs (no schemas) or to `XmlValidationMode.SchemaWithDtd` to use schemas but allow DTDs for entities, and register the updated validator object on the language as a "feature" service (see example below).

### DTD Path

Sometimes DTDs attempt to look for files in the current application folder.  The [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator).[DtdPath](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator.DtdPath) property can optionally be set to a folder that contains DTD-referenced files.

When this property is set, prior to validation and if DTDs are allowed, the current directory will be changed to the path.  When validation is complete, the current directory will be restored.

## Registering with a Syntax Language

Any object that implements [IXmlValidator](xref:ActiproSoftware.Text.Languages.Xml.IXmlValidator) can be associated with a syntax language by registering it as an [IXmlValidator](xref:ActiproSoftware.Text.Languages.Xml.IXmlValidator) service on the language.

The [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) class automatically registers a [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) with itself when it is created, so normally validators never need to be set on an XML language unless a custom one is made or DTD validation support is desired.

This code adds DTD validation support to the language that is already declared in the `language` variable by registering a new [XmlValidator](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator) language service with the [Mode](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlValidator.Mode) property set to `XmlValidationMode.Dtd`:

```csharp
XmlValidator validator = new XmlValidator();
validator.Mode = XmlValidationMode.Dtd;
language.RegisterXmlValidator(validator);
```

> [!NOTE]
> The [XmlSyntaxLanguageExtensions](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions).[RegisterXmlValidator](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions.RegisterXmlValidator*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text.Languages.Xml` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
