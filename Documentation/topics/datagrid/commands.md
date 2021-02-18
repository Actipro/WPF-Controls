---
title: "Commands"
page-title: "Commands - DataGrid Reference"
order: 5
---
# Commands

The Acitpro WPF DataGrid Contrib assembly includes routed commands defined in the [DataGridCommands](xref:ActiproSoftware.Windows.Controls.DataGrid.DataGridCommands) static class, which are described in this topic.

> [!NOTE]
> The commands described in this topic can be applied to the WPF `DataGrid` or the [ThemedDataGrid](xref:ActiproSoftware.Windows.Controls.DataGrid.ThemedDataGrid) .

## ToggleFrozenColumn Command

This command can be used to toggle the frozen state of a column. The `CommandParameter` should be set to the `DataGridColumnHeader` associated with the column that should be frozen/unfrozen.

Frozen columns must appear before any non-frozen columns. Therefore, this command will move any columns that are being frozen before any non-frozen columns. Likewise, it will move any columns that are being unfrozen after any frozen columns.
