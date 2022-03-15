---
title: "UI Automation"
page-title: "UI Automation - SyntaxEditor Globalization and Accessibility"
order: 5
---
# UI Automation

SyntaxEditor follows the Microsoft accessibility model for UI automation.

## What is UI Automation?

Microsoft UI Automation is the new accessibility framework for Microsoft Windows.  It addresses the needs of assistive technology products and automated test frameworks by providing programmatic access to information about the user interface (UI).

SyntaxEditor implements automation peers for the following classes:

- The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) class, which provides access to the current document text@if (wpf) {, current selection}, and various other abilities.

- The [EditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.EditorView) class, which provides simple scrolling abilities.
