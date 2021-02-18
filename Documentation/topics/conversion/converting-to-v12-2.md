---
title: "Converting to v12.2"
page-title: "Converting to v12.2 - Conversion Notes"
order: 93
---
# Converting to v12.2

All of the breaking changes are detailed or linked below.

## Using the New Metro Themes

New Metro Light and Metro White themes have been added in this version.  Similar to the Office themes, they are implemented in a separate optional assembly that must be referenced and initialized.

The [Themes' Getting Started](../themes/getting-started.md) topic walks through how to do this.

## Renamed Assets

These [AssetResourceKeys](xref:ActiproSoftware.Windows.Themes.AssetResourceKeys) have been renamed:

- `RaftingWindowBorderNormalBrushKey` renamed to `RaftingWindowBorderInactiveBrushKey`.

> [!NOTE]
> Convert any references to the old name to use the new name instead.
