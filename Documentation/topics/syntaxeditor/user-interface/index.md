---
title: "Overview"
page-title: "SyntaxEditor User Interface Features"
order: 1
---
# Overview

The SyntaxEditor user interface implements many of the features you'd expect to see in a powerful code editor.

## Editor View Features

Editor views are where end users primarily interact with documents and are customizable in a number of ways.

See the [Editor View Features](editor-view/index.md) topic for more information.

## Input/Output Features

In addition to the input/output features found in the [text framework](../text-parsing/index.md) such as file load/save, exporting to HTML/RTF, etc., SyntaxEditor has a lot of other i/o features built-in.

See the [Input/Output](input-output/index.md) topic for more information.

## Printing Features

SyntaxEditor's print features include easy dialog access, many print options, and customizable margins.

See the [Printing Features](printing/index.md) topic for more information.

## Searching (Find/Replace) Features

SyntaxEditor's view search model interacts with the view's selection and provides extensive find/replace functionality.  A built-in search overlay pane makes this functionality accessible to end users.

@if (winrt wpf) {

A standalone [EditorSearchView](searching/editor-search-view.md) control can be used to instantly recreate a classic Visual Studio-like search experience for end users. 

}

See the [Searching (Find/Replace) Features](searching/index.md) topic for more information.

## IntelliPrompt Features

SyntaxEditor includes some extremely powerful IntelliPrompt features that help enhance end user productivity.

See the [IntelliPrompt](intelliprompt/index.md) topic for more information.

## Theme and Highlighting Style Features

Highlighting styles govern how SyntaxEditor text is rendered and can be used to create entire themes for various languages.

See the [Theme and Highlighting Style Features](styles/index.md) topic for more information.

## Adornment Features

SyntaxEditor has a fully-extensible adornment layer framework that allows for any sort of custom content to be inserted within a view's text area.  This can include everything from alternating row highlights, to animated text decorations, or anything else you can imagine.

All the built-in text area rendering is done using adornment layers, such as the text itself, selection, caret, and more.

See the [Adornment Features](adornment/index.md) topic for more information.

## Outlining and Collapsing Features

SyntaxEditor fully supports automatic (language-driven) and manual (user-driven) code outlining/folding features similar to those found in Visual Studio.

Various text regions can even be collapsed (hidden) independent of the outlining features.

See the [Outlining and Collapsing](outlining/index.md) topic for more information.

## Globalization and Accessibility Features

SyntaxEditor has full support for editing @if (wpf winforms) {bi-directional} Unicode text, is completely localizable, and implements UI automation.

See the [Globalization and Accessibility Features](globalization/index.md) topic for more information.
