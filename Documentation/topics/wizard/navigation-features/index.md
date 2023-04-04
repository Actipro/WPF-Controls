---
title: "Overview"
page-title: "Wizard Navigation Features"
order: 1
---
# Overview

Wizard makes it so easy to navigate between pages, with both declarative settings and the ability to modify navigation programmatically at run-time.

Some of the more major features include:

- [WPF commands](command-model.md) defined for going to the next or previous pages.

- [WPF commands](command-model.md) for jumping forward to or backwards to a specific page, as indicated by its direct reference, name, or index.

- Any [wizard command](command-model.md) can be assigned to custom controls such as `Buttons`, `Hyperlinks`, or `MenuItems`.

- Default [page sequencing](page-sequencing.md) which visits pages in their order by index can be overridden to support decision-based branching of page execution paths.

- The [next and previous pages for each page](page-sequencing.md) can be explicitly set by a direct reference, name, or index.

- Two types of [backwards progress page sequencing](page-sequencing.md), normal and stack-based.

- [Disable pages](page-sequencing.md) to have Wizard skip over them when using default page sequencing.

- Ability to programmatically get/set the [selected page](selection-changes.md) by reference or index.

- Process [selection changing/changed events](selection-changes.md) at the general `Wizard` or `WizardPage`-specific levels.

- Selection change events specify what caused the change, which pages are changing, and allow you to [cancel the change](selection-changes.md) or [choose a new destination page](selection-changes.md).

- [Validate data](selection-changes.md) on a page in the selection changing event.

- Differentiation between [forward and backward progress](selection-changes.md) through wizard pages.

- Easily navigate through pages using the [mouse and/or keyboard forward/back buttons](navigation-commands.md).
