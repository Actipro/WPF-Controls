---
title: "Overview"
page-title: "Wizard Page and Button Features"
order: 1
---
# Overview

Wizard defines several types of pages and makes it easy to work with the wizard buttons.

Some of the more major features include:

- Multiple built-in [page types](page-types.md):
  
  - Exterior - Welcome/finish with left watermark area.
  - Interior - Step with top header area.
  - Blank - Customizable for a unique user interface.

- [Page captions and descriptions](page-titles.md) that display in the header area of the page.

- [Page titles](page-titles.md) that can be used to update the title bar of the containing Window.

- Multiple options for automated updating of the containing [Window title bar text](page-titles.md).

- Global default settings for all [button visibility and enabled states](button-states.md) that can be overridden by page-specific settings.

- Toggle [button visibility and enabled states](button-states.md) at run-time via code.

- Uses WPF [command model](../navigation-features/command-model.md) for handling all button clicks.

- Multiple options for setting the [default and cancel buttons](window-default-cancel-buttons.md) on the containing Window to wizard buttons.

- The Cancel and Finish buttons can set the appropriate Window.DialogResult and [close the containing Window](window-close.md).  This behavior can be changed.

- Lazily initialize pages if needed.
