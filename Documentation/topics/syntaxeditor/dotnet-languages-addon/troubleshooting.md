---
title: "Troubleshooting"
page-title: "Troubleshooting - SyntaxEditor .NET Languages Add-on"
order: 99
---
# Troubleshooting

This topic provides answers to common questions and problems related to the add-on.

## How Do I Get Started with C# / Visual Basic Languages?

The [C# Getting Started](csharp/getting-started.md) topic walks through all the requirements for using the C# language.  Be sure to follow all the steps in that topic so that the syntax language operates correctly.

The [Visual Basic Getting Started](vb/getting-started.md) topic walks through all the requirements for using the Visual Basic language.  Be sure to follow all the steps in that topic so that the syntax language operates correctly.

## Typing Is Slow

If you notice a lag when typing, it is likely due to an ambient parse request dispatcher not being set up.  Each time text changes occur, such as while typing, a parse request is fired off for the parser to parse the document.  To ensure that these parsing operations are offloaded into a worker thread that won't affect the UI performance, you must set up an ambient parse request dispatcher for your application.

When no ambient parse request dispatcher has been configured for your application, the parsing will occur in the main UI thread, thus severely affecting typing performance, especially in larger documents.

See the [C# Getting Started](csharp/getting-started.md) or [Visual Basic Getting Started](vb/getting-started.md) topics for more information on how to set up an ambient parse request dispatcher for your application.

## Loading of Binary Assembly References Blocks the UI Thread

When .NET assemblies are added to a project assembly as references, we use reflection to iterate over all the namespaces, types, members, etc. in the assembly to build up our own reflection data model for the assembly.  Using .NET reflection does take time so referencing assemblies will block the UI if it is done in the main UI thread.

A project assembly's references should be loaded using a `BackgroundWorker` or another similar asynchronous mechanism.  That way, it will not block the UI while loading.

The add-on also supports caching of binary assembly data, so that the second and later times that the same assembly is loaded (even in future application sessions), it will load ten or more times faster than its initial load.  This feature is implemented via an ambient assembly repository, which helps reduce overall memory and improves performance.

See the [C# Getting Started](csharp/getting-started.md) or [Visual Basic Getting Started](vb/getting-started.md) topics for more information on how to load project assembly references in a worker thread and configure an ambient assembly repository for caching.

## No IntelliPrompt for System Types

If automated IntelliPrompt features like quick info are not working for system types or for the language's native keywords, make sure that the project assembly in use has the proper assembly references to allow those types to be resolved.  For instance, at a minimum the `MsCorLib` assembly should always be added as a reference.

See the [C# Getting Started](csharp/getting-started.md) or [Visual Basic Getting Started](vb/getting-started.md) topics for more information on how load project assembly references.
