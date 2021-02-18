---
title: "Troubleshooting"
page-title: "Troubleshooting - SyntaxEditor Python Language Add-on"
order: 99
---
# Troubleshooting

This topic provides answers to common questions and problems related to the add-on.

## How Do I Get Started With Python Language?

The [Getting Started](python/getting-started.md) topic walks through all the requirements for using the Python language.  Be sure to follow all the steps in that topic so that the syntax language operates correctly.

## Typing Is Slow

If you notice a lag when typing, it is likely due to an ambient parse request dispatcher not being set up.  Each time text changes occur, such as while typing, a parse request is fired off for the parser to parse the document.  To ensure that these parsing operations are offloaded into a worker thread that won't affect the UI performance, you must set up an ambient parse request dispatcher for your application.

When no ambient parse request dispatcher has been configured for your application, the parsing will occur in the main UI thread, thus severely affecting typing performance, especially in larger documents.

See the [Getting Started](python/getting-started.md) topic for more information on how to set up an ambient parse request dispatcher for your application.
