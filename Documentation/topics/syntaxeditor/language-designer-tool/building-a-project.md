---
title: "Building a Project"
page-title: "Building a Project - SyntaxEditor Language Designer Tool"
order: 6
---
# Building a Project

Once you have configured your language project and are ready to start testing it or generating code, it's time to build the project.  Building a project examines the project and reports whether there are any errors, warnings, or messages that you should be aware of.

Project builds can be initiated from the ribbon, or by pressing the shortcut <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>B</kbd>.

## Error List and Status Bar

The **Error List** tool window indicates the errors, warnings, and messages from the last build.  Each item in the list shows a general category, which relates to a configuration pane, along with a description.

In addition, the **Status Bar** will show a project build result summary and at what time the last build succeeded or failed.

## Fixing Errors

In the event that there are errors or warnings that appear, you can double-click the item in the **Error List** and the related configuration pane should open.  In most cases, the appropriate item will be selected or focused as well.

This makes it easy for you to quickly jump to the problem location and get the issue resolved.

## Notes Related to Live Test

Project builds are what trigger updates to the language used in the [Live Test](live-test.md) pane.  If there are any build errors, no update to the live test language are made.

## Notes Related to Code Generation

Project builds are what update the list of code files that can be generated in the [Code Generation Configuration](code-generation.md) pane.  No code previewing or generation can occur if there are build errors.
