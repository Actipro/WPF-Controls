---
title: "Overview"
page-title: "Ribbon Controls"
order: 1
---
# Overview

> [!IMPORTANT]
> This older Ribbon product will be deprecated in the future in favor of the new ribbon implementation in the [Bars product](../../../bars/index.md), which has a much-improved design and appearance, and many of the latest features currently found in Office.  It is recommended to implement new ribbons using the Bars product instead, and to [migrate away from this older Ribbon product](../../../conversion/converting-to-v23-1.md) to the newer Bars ribbon when possible.

There are a number of other miscellaneous controls used in the ribbon user interface.

| Control Type | Description |
|-----|-----|
| [Backstage / ApplicationMenu](applicationmenu.md) | The application menu displays when the application button on the ribbon is clicked.  Both the newer Backstage and traditional application menu styles are available. |
| [ContextMenu](contextmenu.md) | A context menu that inherits `ContextMenu` and through use of a contained [Menu](menu.md) control, allows any ribbon control to be used as child items.  This allows for a consistent presentation throughout your application.  Best of all, since our context menu class inherits `ContextMenu`, it can be used anywhere that a regular `ContextMenu` can. |
| [ContextualTabGroup](contextualtabgroup.md) | A contextual tab group that contains one or more [Tab](tab.md) controls and can be displayed based on document context. |
| [Group](group.md) | A group that can appear on a [Tab](tab.md). |
| [Menu](menu.md) | A control that renders its items using templates that have menu item-like functionality. |
| [Mini-ToolBar](minitoolbar.md) | The mini-toolbar can be displayed semi-transparently after a mouse selection or paired along with a context menu. |
| [QuickAccessToolBar](quickaccesstoolbar.md) | The quick access toolbar (QAT) that can be displayed above or below the ribbon. |
| [RecentDocumentMenu](recentdocumentmenu.md) | A control that can be used on a [Backstage](applicationmenu.md), or as the additional content of an [ApplicationMenu](applicationmenu.md).  It manages a sorted list of recently-opened documents and allows pinning of the documents. |
| [Tab](tab.md) | A tab within a ribbon.  The tab contains one or more [Group](group.md) controls. |
