---
title: "Overview"
page-title: "SyntaxEditor Adornment Features"
order: 1
---
# Overview

SyntaxEditor has a fully-extensible adornment layer framework that allows for any sort of custom content to be inserted within a view's text area.  This can include everything from alternating row highlights to animated text decorations, or anything else you can imagine.

All the built-in text area rendering is done using adornment layers, such as the text itself, selection, caret, and more.

## Adornment Layers

Adornment layers are panels that are stacked within a view's text area.  Each layer can be ordered to appear before or after other specified layers.  Layers can contain any sort of adornment element.

See the [Adornment Layers](adornment-layers.md) topic for more information.

## Syntax Highlighting

The document text rendered within a view is drawn using two adornment layers, one for the text itself and normal decorations (underlines, etc.), and one for any text backgrounds.  Classification taggers can be applied to a document (via language services) to override the default syntax highlighting provided by a language's lexer.  This adds support for features like changing the foreground color of certain words, marking search result ranges in a different background color, etc.

See the [Syntax Highlighting](syntax-highlighting.md) topic for more information.

## Squiggle Lines

There is a built-in adornment layer that can render squiggle lines.  Squiggle lines are generally displayed under parse errors or spelling mistakes.

A built-in tagger can also be installed to watch for errors in the parse data returned by a parser, and automatically render squiggle lines based on those ranges.

See the [Squiggle Lines](squiggle-lines.md) topic for more information.

## Intra-Text Adornments

SyntaxEditor supports the ability to insert intra-text spacers, or space between text characters, and have adornments render within the space that was created.

This feature can be used to insert any sort of UI element in-line with the text of a view and is different than normal adornments in that normal adornments don't alter text positioning.  This feature allows for inserting in-line images, in-line buttons that can be used to take action on nearby document text, etc.

See the [Intra-Text Adornments](intra-text-adornments.md) topic for more information.

## Intra-Line Adornments

SyntaxEditor supports the ability to insert intra-line spacers, or margin space around view lines, and have adornments render within the space that was created.

This feature can be used to insert any sort of UI element in-line with a view line and is different than normal adornments in that normal adornments don't alter text positioning.  This feature allows for functionality similar to Code Lens and Peek Definition within the Visual Studio code editor.

See the [Intra-Line Adornments](intra-line-adornments.md) topic for more information.
