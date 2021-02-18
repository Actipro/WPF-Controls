---
title: "Overview"
page-title: "DataGrid Reference"
order: 1
---
# Overview

The Microsoft WPF DataGrid can be used to display various data in a grid of rows and columns, with several options available to customize the behavior and appearance.  Actipro is giving back to the community by providing two add-ons that make using the WPF DataGrid easier than ever.

![Screenshot](images/datagrid.png)

*A ThemedDataGrid control using a customized theme provided by Actipro*

The Actipro WPF DataGrid Contrib add-on offers the [ThemedDataGrid](xref:ActiproSoftware.Windows.Controls.DataGrid.ThemedDataGrid) control, which extends the WPF DataGrid control to provide customized Aero and Office (blue, black, and silver) themes.  The themes are integrated into the Shared Library's ThemeManager, which allows for dynamic theme changing.  The add-on also provides extension methods, attached behaviors, and commands that can be used by [ThemedDataGrid](xref:ActiproSoftware.Windows.Controls.DataGrid.ThemedDataGrid) or the WPF DataGrid.

The Actipro Editors/DataGrid Interop add-on allows controls from the Actipro Editors for WPF product to be quickly and easily integrated into the WPF DataGrid or [ThemedDataGrid](xref:ActiproSoftware.Windows.Controls.DataGrid.ThemedDataGrid).  This allows the cells in a DataGrid to leverage parts-based and masked text editors, which make data entry faster and more intuitive.

## Features

### Microsoft WPF DataGrid Features

- Auto column generation based on data
- Several built-in and extensible column types
- Columns can be easily be rearranged
- Support for frozen columns that remain fixed when scrolling
- Data can be sorted programmatically or interactively
- Inline editing of cells
- Intuitive keyboard navigation
- Resizable rows and/or columns
- Interactively add or delete rows
- Display rows using alternating colors
- Support for single or multiple selections
- Several selection modes including row and cell
- Support for row details which can display additional info
- Data validation can be applied at the cell and/or row
- Fully customizable row and column headers
- Select all button can be used to select all cells
- Exposes several properties that can customize the look and feel
- Supports virtualization of containers both vertically and horizontally
- and much more...

### WPF DataGrid Contrib Add-on Features

- Includes a [routed command](commands.md) that allows a column to be easily frozen and unfrozen.
- Provides several [extension methods](extension-methods.md) for the DataGrid and relating controls.
- Customize the look and feel of the [new row](attached-behaviors.md) using an attached behavior.
- Easily track the [focus or selection](attached-behaviors.md) in the column headers (for visual customization) using attached behaviors.
- Adds [Actipro theme support](themes.md) for the DataGrid control with visually-appealing appearances.
- Completely open-source and freely available.

### Editors/DataGrid Interop Add-on Features

- Easily integrate the controls offered by the Actipro [Actipro Editors for WPF](interoperability/index.md) product.
- Quickly integrate any custom parts-based editors by leveraging reusable components.

## Licensing

See the [Licensing](licensing.md) topic for more information.
