---
title: "Overview"
page-title: "SyntaxEditor Outlining and Collapsing Features"
order: 1
---
# Overview

SyntaxEditor fully supports automatic (language-driven) and manual (user-driven) code outlining/folding features similar to those found in Visual Studio.

Various text regions can even be collapsed (hidden) independent of the outlining features.

## Code Outlining (Folding)

Code outlining, also commonly referred to as code folding, is a mechanism by which a hierarchy of outlining nodes is maintained and visually rendered in an outlining margin.  Nodes can be expanded and collapsed by the end user.

Automatic outlining mode is used when a syntax language defines an outlining source that tells the outlining manager how to incrementally update nodes as text is changed.  Manual outlining mode is used when the end user wishes to define where outlining nodes occur.

A number of built-in commands provide easy access to all the outlining functionality found in products like Visual Studio.

See the [Code Outlining (Folding)](outlining-general.md) topic for more information.

## Outlining Nodes and Definitions

Outlining nodes are part of a nested hierarchy of text ranges, stored within an outlining manager for a document.  They can each be assigned various characteristics via outlining node definitions, such as what they render when collapsed, if they are collapsed automatically when a document is loaded, etc.

See the [Outlining Nodes and Definitions](outlining-nodes.md) topic for more information.

## Outlining Sources and Outliners

Outlining sources are objects that an outlining manager can call upon to direct language-specific automatic outlining updates when document text changes.  Several helpful base classes are included to make the creation of a syntax language's outlining source a simple task.

An outliner is a language service that returns a language-specific outlining source for a particular [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  Having an outliner service registered on a language is what tells SyntaxEditor that the language supports automatic outlining.

See the [Outlining Sources and Outliners](outlining-sources.md) topic for more information.

## Collapsing Regions without Outlining

The code outlining feature described in earlier topics supports the collapsing of outlining nodes, where contained text becomes hidden.  SyntaxEditor's low-level collapsed region management feature is harnessed to achieve this functionality.  This same collapsed regions mechanism can be used independently of code outlining too.

Various text regions can be collapsed (hidden) independent of the outlining features.  And in these cases, you can even optionally replace the collapsed region with an adornment.

See the [Collapsing Regions without Outlining](collapsed-regions.md) topic for more information.
