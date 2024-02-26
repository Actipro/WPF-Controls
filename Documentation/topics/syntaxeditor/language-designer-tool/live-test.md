---
title: "Live Test"
page-title: "Live Test - SyntaxEditor Language Designer Tool"
order: 7
---
# Live Test

The **Language Designer Live Test** pane allows you to try various aspects of your language out before performing any code generation.

Click the **Live Test** button in the ribbon to open the **Live Test** pane.

## Features and Limitations

The **Live Test** can show syntax highlighting for a language that has a dynamic lexer and classification types defined.  It does not yet work with other lexer types.

## Status Bar Information

Assuming a supported lexer type is available (see above), as the caret moves through text, the status bar indicates the current lexical state and token keys.  An asterisk (`*`) appears next to the token key if the caret is at the start of a token.

The right side of the status bar indicates the current line, column, and character positions of the caret.

## Updating the Live Test

You can [build the language project](building-a-project.md) to update the language used in the **Live Test** pane.  Most changes made to the other configuration panes will not be picked up in **Live Test** until a project build occurs.

> [!TIP]
> If the **DefaultStyle** or **DarkStyle** of a classification type is modified after the live test is loaded, those changes should be applied in real time.  Dock the **Live Test** and **Classification Types** tool windows side-by-side to easily preview how different styles affect the sample text.
