---
title: "Text Formatter"
page-title: "Text Formatter - XML Language - SyntaxEditor Web Languages Add-on"
order: 8
---
# Text Formatter

The built-in XML text formatter can adjust whitespace between elements and attributes to make code more readable.

The [Text Formatting](../../text-parsing/advanced-text/text-formatting.md) topic talks about text formatters in general and how to register them as a "feature" language service.

The [XmlTextFormatter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter) class is the default implementation of an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service for this language.

## Registering with a Syntax Language

Any object that implements [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) can be associated with a syntax language by registering it as an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service on the language.

The [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) class automatically registers a [XmlTextFormatter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter) with itself when it is created, so normally text formatters never need to be set on a XML language unless a custom one is made.

This code creates a custom text formatter (defined in a make-believe `CustomXmlTextFormatter` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterTextFormatter(new CustomXmlTextFormatter());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextFormatter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextFormatter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.

## Attribute Spacing Mode

The [XmlTextFormatter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter) has an [AttributeSpacingMode](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter.AttributeSpacingMode) property that can be set to alter the behavior of the formatter when dealing with whitespace around attributes within an element.  None of the modes affect the [TagWrapLength](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter.TagWrapLength) feature (described below).  The [XmlAttributeSpacingMode](xref:ActiproSoftware.Text.Languages.Xml.XmlAttributeSpacingMode) enumeration defines these three mode options:

| Mode | Formatter Behavior |
|-----|-----|
| `Preserve` | The formatter will not make changes to the whitespace around attributes. This is the default mode. |
| `NormalizeWhitespace` | The formatter will adjust any whitespace around attributes to be exactly one space. |
| `SeparateLines` | The formatter will adjust any whitespace such that each attribute is on a separate line. |

## Element Spacing Mode

The [XmlTextFormatter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter) has an [ElementSpacingMode](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter.ElementSpacingMode) property that can be set to alter the behavior of the formatter when dealing with whitespace in element content.  The [XmlElementSpacingMode](xref:ActiproSoftware.Text.Languages.Xml.XmlElementSpacingMode) enumeration defines these three mode options:

| Mode | Formatter Behavior |
|-----|-----|
| `Preserve` | The formatter will not make changes to the whitespace in element content. This is the default mode. |
| `NormalizeEmptyLines` | The formatter will adjust any whitespace around element content so that there is no more than a single empty line between the content and the containing element. |
| `RemoveEmptyLines` | The formatter will remove extra whitespace around element content. The actual result depends on the type of content, which can be text-only, one or more elements, or mixed content. |

## Tag Wrap Length

The [XmlTextFormatter](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter) has a [TagWrapLength](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter.TagWrapLength) property that can be set to cause the formatter to wrap an attribute to the next line when the element length on a particular line exceeds the specified length. This feature will operate regardless of the [AttributeSpacingMode](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlTextFormatter.AttributeSpacingMode).

Setting the value to `0` (zero) will disable the feature, which is the default value.  When enabling this wrap feature, a common setting is `120`.
