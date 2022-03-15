---
title: "Overview"
page-title: "SyntaxEditor Globalization and Accessibility"
order: 1
---
# Overview

SyntaxEditor has full support for editing bi-directional Unicode text, is completely localizable, and implements UI automation.

@if (wpf winforms) {

## IME (Input Method Editor) Support

SyntaxEditor has complete support for editing text using IME (Input Method Editor).

IME is a Window feature that allows users to enter characters and symbols not found on their input device.  It allows Western keyboards to enter Chinese, Japanese, etc. characters.

See the [IME (Input Method Editor) Support](ime.md) topic for more information.

}

## Bi-Directional Input Support

SyntaxEditor is fully natively compatible with extended Unicode characters and even supports bi-directional editing.

See the [Bi-Directional Input Support](bi-di.md) topic for more information.

@if (winrt wpf) {

## Localization

All of the strings that are displayed in the user interface can be customized and localized.

See the [Localization](localization.md) topic for more information.

}

@if (winrt wpf) {

## UI Automation

SyntaxEditor follows the Microsoft accessibility model for UI automation.

See the [UI Automation](ui-automation.md) topic for more information.

}
