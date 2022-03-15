---
title: "Indent Provider"
page-title: "Indent Provider - XML Language - SyntaxEditor Web Languages Add-on"
order: 7
---
# Indent Provider

An indent provider enables support for smart indent features when pressing ENTER.

The [Indent Providers](../../user-interface/input-output/indent-providers.md) topic talks about indent providers in general and how to register them as a "feature" language service.

The [XmlIndentProvider](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlIndentProvider) class is the default implementation of an [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) service for this language.

## Feature Summary

### Smart Indent

The built-in indent provider will normally use block indent (same indent level as the previous line) except when it detects that ENTER is pressed on a line that ends with a start tag.  In this case, the caret is indented another level on the next line.

### Block Element Handling

Also consider this scenario, where the caret is at `|`:

```xml
<tag>|</tag>
```

If the end user presses ENTER, the indent provider will make this the result:

```xml
<tag>
	|
</tag>
```

Note how the end tag is moved to the line below the caret and is properly indented to the same indent level of the start tag.  However the caret is indented on the line in the middle of the tags.

## Registering with a Syntax Language

Any object that implements [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) can be associated with a syntax language by registering it as an [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) service on the language.

The [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) class automatically registers a [XmlIndentProvider](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlIndentProvider) with itself when it is created, so normally indent providers never need to be set on an XML language unless a custom one is made.

This code creates a custom indent provider (defined in a make-believe `CustomXmlIndentProvider` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterIndentProvider(new CustomXmlIndentProvider());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterIndentProvider](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterIndentProvider*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
