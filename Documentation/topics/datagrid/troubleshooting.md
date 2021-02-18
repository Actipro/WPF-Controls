---
title: "Troubleshooting"
page-title: "Troubleshooting - DataGrid Reference"
order: 9
---
# Troubleshooting

This topic contains troubleshooting data specific to the WPF DataGrid.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as the WPF Studio products, please see the more general [TroubleShooting](../troubleshooting.md) topic.

## Where to get help?

The Microsoft's forums or Stack Overflow are the best places to get general help relating to Microsoft's WPF DataGrid.  For issues relating to the Actipro WPF DataGrid Contrib or Actipro Editors/DataGrid Interop libraries, see our [Support](../support.md) topic about contacting Actipro.

## Visual Studio is reporting several binding errors

When `CanUserAddRows` is set to `true`, Visual Studio will report several binding errors because the `DataGrid` uses `CollectionView.NewItemPlaceholder` as the data item for the new row. When the columns use binding, `CollectionView.NewItemPlaceholder` doesn't have any of the associated properties. This results in the binding errors. This is a known issue and is working as designed.
