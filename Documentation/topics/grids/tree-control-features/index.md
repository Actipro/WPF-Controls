---
title: "Overview"
page-title: "Tree Control Features"
order: 1
---
# Overview

The [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox) control is a single-column tree control with many advanced features, similar to the Visual Studio Solution Explorer tree control.  The [TreeListView](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView) control is a multi-column variant of [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox) that renders similar to a standard `ListView` but has additional features.

While [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox) may initially appear similar to a native `TreeView` control, and [TreeListView](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView) may appear similar to a native `ListView` control, they offer many more features than what is found in the native controls.  See the Features list below for a summary of what they can do.

> [!NOTE]
> Since the [TreeListView](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView) control inherits [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox), all of the documentation related to [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox) also applies to [TreeListView](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView).

## Features

### TreeListBox Control

- Fully customize the appearance of each node.
- UI virtualization, allowing for hundreds of thousands of nodes to be loaded into a tree very quickly.
- No scrollbar jumpiness as seen in other virtualized tree controls when scrolling vertically.
- Use your own custom data models as the source for the tree, with no dependencies on UI or our interfaces.  An adapter class is used (and can be fully customized to fit your model) to communicate between the UI and the model for things like expansion state, getting children, etc.
- The adapter can be coded with bindings in XAML (convenient, yet can be slow in very large trees) or via method overrides (slightly more work but lightning fast).
- Optionally show the root item in the control.
- Fine-grained control over expandability and children query triggers.
- Optional async loading with busy indicator display.
- Events for expansion.
- Events for selection.
- Single or multi-selection, with Ctrl and Shift-based selection options.
- Filter selection such as only allowing sibling nodes to be multi-selected, or nodes of the same depth.
- All common tree hotkeys supported including special ones for expanding and collapsing entire branches.
- Select or ensure nodes are visible by path.
- Double-click and Return key default action handling.
- Optional checkboxes within the data templates.
- Intelligent text searching so when you start typing while the control has focus, it will auto-focus the item that matches the typed text.
- Inline editing via F2 and single-click on a selected item.
- Per-item context menus that can be constructed dynamically via an event.
- Drag items to external controls, drop data from external controls, or drag/drop items within the control itself.
- Dragged items can highlight above, on, and below drop areas for each item.
- Single and multiple item dragging is supported.
- Optional data virtualization optimization when using virtualized collections.
- Indentation of top-level and other nodes can be set independently.
- Optional tree line display.
- Customizable item filtering that supports string, boolean, or predicate-based logic.
- Filtered items can automatically have their ancestors expanded.

### TreeListView Control

- All features found in TreeListBox.
- Templates, template selectors, or text property bindings used to specify custom content for each cell.
- Column width can be a specific pixel value, auto (size to header, cells, or both), or star-sized.
- Optional minimum and maximum widths for column auto/star-sizing modes.
- Columns can optionally be resized, reordered, and have visibility toggled by the end user.
- Frozen columns that don't scroll horizontally.
- Set which column renders the indentation and expander buttons.
- Column headers have a built-in context menu, and the headers themselves can be hidden.
- Size columns to fit contents.
- Optional grid line display.
- Numerous events for column resizing, reordering, visibility changes, and header menu requests.

## Getting Started

This topic covers everything you need to get started, including high-level information about how item adapters work, how to bind to data, and how to provide customized UI for your items.

See the [Getting Started](getting-started.md) topic for more information.

## Columns (TreeListView)

The [TreeListView](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView) control supports one or more columns of cells that can be displayed within each item (row).  Columns can be resized, reordered, hidden, frozen, and much more.

See the [Columns (TreeListView)](tree-list-view-columns.md) topic for more information.

## Expandability

Everything from when children are queried to expander display is configurable, and events allow for expand/collapse operations to be canceled.  Child items can be loaded asynchronously while a progress spinner shows that activity is occurring.

See the [Expandability](expandability.md) topic for more information.

## Selection

Both single and multi-selection modes are supported.  Various properties and events allow for the selection of certain items to be blocked.

See the [Selection](selection.md) topic for more information.

## Default Actions

A default action occurs when an item is double-tapped or `Enter` is pressed.  This action can be fully customized to execute custom logic.

See the [Default Actions](default-actions.md) topic for more information.

## Item Paths

Sometimes it's handy to be able to supply a string path to obtain or work with an item.  Each item can provide its own path.  A full path can be constructed by appending a path separator delimiter and the item's path to its parent's full path.

See the [Item Paths](item-paths.md) topic for more information.

## Item CheckBoxes

`CheckBox` controls can be inserted into item templates to make items checkable, and default actions can be implemented to toggle checked states for double-taps and `Enter` key presses.

See the [Item CheckBoxes](item-checkboxes.md) topic for more information.

## Item Context Menus

Context menus can be generated dynamically upon request by items.

See the [Item Context Menus](item-context-menus.md) topic for more information.

## Inline Editing

Items support inline editing, which means that a special UI mode can be entered where their text or other properties are in an editable state.

See the [Inline Editing](inline-editing.md) topic for more information.

## Drag and Drop

This control supports the standard system drag and drop functionality and even has built-in features for displaying an optional drop indicator over items when dragging over the control.  Full control over the drag and drop capabilities and drop result is available.

See the [Drag and Drop](drag-drop.md) topic for more information.

## Text Searching

Text searching allows the end user to start typing text and for a visible (all ancestor items expanded, but not necessarily scrolled into view) item that starts with the typed text to be focused and scrolled into view.

See the [Text Searching](text-searching.md) topic for more information.

## Filtering

Filtering allows a tree to be narrowed down to only display items that match filter criteria.  A powerful data filtering mechanism is provided that uses string, boolean, and predicate-based logic to filter items.  The underlaying data model is not altered in any way, only the view is.

See the [Filtering](filtering.md) topic for more information.

## Virtualization

The tree controls are lightning-fast because they have UI virtualization on by default.  Data virtualization is an additional option for scenarios where virtualized child collections are in use.

See the [Virtualization](virtualization.md) topic for more information.

## Keyboard Shortcuts

Many keyboard shortcuts are supported that provide access to expand/collapse, focus/selection, default actions and editing mode.

See the [Keyboard Shortcuts](keyboard-shortcuts.md) topic for more information.
