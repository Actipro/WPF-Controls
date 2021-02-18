---
title: "Troubleshooting"
page-title: "Troubleshooting - SyntaxEditor Web Languages Add-on"
order: 99
---
# Troubleshooting

This topic provides answers to common questions and problems related to the add-on.

## How Do I Get Started With XML Language?

The [Getting Started](xml/getting-started.md) topic walks through all the requirements for using the XML language.  Be sure to follow all the steps in that topic so that the syntax language operates correctly.

## Typing Is Slow

If you notice a lag when typing, it is likely due to an ambient parse request dispatcher not being set up.  Each time text changes occur, such as while typing, a parse request is fired off for the parser to parse the document.  To ensure that these parsing operations are offloaded into a worker thread that won't affect the UI performance, you must set up an ambient parse request dispatcher for your application.

When no ambient parse request dispatcher has been configured for your application, the parsing will occur in the main UI thread, thus severely affecting typing performance, especially in larger documents.

See the [Getting Started](xml/getting-started.md) topic for more information on how to set up an ambient parse request dispatcher for your application.

## No IntelliPrompt Completion List

The XML language supports automated IntelliPrompt completion lists and extended information in IntelliPrompt quick info as long as an XML schema has been properly loaded.

If you are not seeing these extended features working, make sure you have created a [schema resolver](xml/schema-resolver.md), loaded schemas into it, and have registered it as a language service.

See the [Getting Started](xml/getting-started.md) topic for more information on how to load an [XmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSchemaResolver) and register it as a language service.
