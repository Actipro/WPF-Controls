---
title: "Schema Resolver"
page-title: "Schema Resolver - XML Language - SyntaxEditor Web Languages Add-on"
order: 4
---
# Schema Resolver

The schema resolver is an essential piece of the XML language since it is what loads the XML schema data that is used to drive features such as automated IntelliPrompt and validation.

The [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) interface provides the base requirements for this functionality.  It is installed into a language as a "feature" language service.

The [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) class is the default implementation of the [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) interface.

## Managing the XSD Schemas Used

The schema resolver can manage one to many XSD schemas at a time.  These XSDs are used to provide automated IntelliPrompt completion lists and quick info and are also used to validate XML documents.

The [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) class has numerous methods for loading XML schema data directly, or from files, streams, strings, etc.

| Member | Description |
|-----|-----|
| [AddSchema](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.AddSchema*) Method | Adds another `XmlSchema` to the existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet). |
| [AddSchemaFromFile](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.AddSchemaFromFile*) Method | Adds another `XmlSchema` from a file to the existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet). |
| [AddSchemaFromStream](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.AddSchemaFromStream*) Method | Adds another `XmlSchema` from a `Stream` to the existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet). |
| [AddSchemaFromString](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.AddSchemaFromString*) Method | Adds another `XmlSchema` from a string to the existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet). |
| [LoadSchema](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.LoadSchema*) Method | Clears any existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet) and loads a new single `XmlSchema`. |
| [LoadSchemaFromFile](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.LoadSchemaFromFile*) Method | Clears any existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet) and loads a new single `XmlSchema` from a file. |
| [LoadSchemaFromStream](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.LoadSchemaFromStream*) Method | Clears any existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet) and loads a new single `XmlSchema` from a `Stream`. |
| [LoadSchemaFromString](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.LoadSchemaFromString*) Method | Clears any existing [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet) and loads a new single `XmlSchema` from a string. |
| [SchemaSet](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver.SchemaSet) Method | Gets or sets the `XmlSchemaSet` to use.  This property can be set if you already have a `XmlSchemaSet` loaded that should directly be used. |

## Using Default Namespace Prefix Mappings

The [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver).[DefaultNamespace](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver.DefaultNamespace) property gets or sets the default namespace to use for any XML documents where the root element doesn't have an `xmlns` specified.  The result of specifying a default namespace is the same as if a `xmlns` attribute was set on the document's root element.  Automated IntelliPrompt, validation, etc. will treat it as such.

The [DefaultNamespace](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver.DefaultNamespace) property actually just gets/sets the entry in the [DefaultNamespacePrefixMappings](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver.DefaultNamespacePrefixMappings) dictionary property for an empty string, meaning default namespace prefix.

Other namespace prefixes can be mapped by default in the [DefaultNamespacePrefixMappings](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver.DefaultNamespacePrefixMappings) property too, and similar to the processing of the [DefaultNamespace](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver.DefaultNamespace) property, automated IntelliPrompt, validation, etc. will use these mappings.

This code creates a schema resolver, sets the default namespace to XHTML and also maps a `foo` namespace prefix, similar to if the root element had `xmlns="http://www.w3.org/1999/xhtml" xmlns:foo="http://tempuri.org/bar.xsd"` :

```csharp
XmlSchemaResolver resolver = new XmlSchemaResolver();
resolver.DefaultNamespace = "http://www.w3.org/1999/xhtml";
resolver.DefaultNamespacePrefixMappings.Add("foo", "http://tempuri.org/bar.xsd");
```

Use of these properties is entirely optional and should only be used in cases where `xmlns` attributes are not required to be specified in the document's root element. `xmlns` attribute settings in the XML that have the same namespace prefixes as keys in the [DefaultNamespacePrefixMappings](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver.DefaultNamespacePrefixMappings) property will take precedence over the default mappings.

## Schema Data Helper Methods

Schema resolvers have a number of helper methods that provide data based on the XML schemas that are loaded.  This data is consumed by other language services such as the built-in functionality and services that provide context and automated IntelliPrompt, and do not typically need to be consumed in your code.

## Registering with a Syntax Language

Any object that implements [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) can be associated with a syntax language by registering it as an [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) service on the language.

> [!NOTE]
> No [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) is installed on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default.  To enable support of automated IntelliPrompt and validation, an [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) instance must be configured and registered on the language.

This code creates a schema resolver, initializes it with a schema from the file specified in the `path` variable and registers it with the syntax language that is already declared in the `language` variable:

```csharp
XmlSchemaResolver resolver = new XmlSchemaResolver();
resolver.LoadSchemaFromFile(path);
language.RegisterXmlSchemaResolver(resolver);
```

> [!NOTE]
> The [XmlSyntaxLanguageExtensions](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions).[RegisterXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.XmlSyntaxLanguageExtensions.RegisterXmlSchemaResolver*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text.Languages.Xml` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
