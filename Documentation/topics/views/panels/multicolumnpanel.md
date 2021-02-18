---
title: "MultiColumnPanel"
page-title: "MultiColumnPanel - Views Panels"
order: 14
---
# MultiColumnPanel

This panel can render child elements in multiple columns, collapsing columns down as available space decreases.  It's a space-efficient and visually-appealing way to render lists of items, or to break paragraphs of text up.

![Screenshot](../images/multicolumnpanel.png)

*MultiColumnPanel with contact cards*

## Overview

The [MultiColumnPanel](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel) control provides a way to arrange child elements in one or more columns based on the available width.  You specify the maximum number of columns to support, the minimum width of each column, and the margin between each column.  Given those parameters, the panel will determine how many columns can fit within the available space.

When arranging elements, the panel will first determine the number of columns that can display in the available width.  Once that is determined, the columns are made equal size with the margin space between them.  Then elements are arranged downward in order until an average height is reached.  Subsequent elements are arranged on the next column and the process repeats.

The measure/arrange algorithm works best when the child elements are all relatively close to the same height.

## Important Members

The following [MultiColumnPanel](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel) members are key to its use:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>


</thead>
<tbody>

<tr>
<td>

[CanRemoveEmptyColumns](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel.CanRemoveEmptyColumns) Property

</td>
<td>

Gets or sets whether columns without any items can be removed, with remaining columns filling in, thereby maximizing use of available width.  The default value is `false`.

When using multiple [MultiColumnPanel](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel) instances in a stack where columns should always be aligned, leave this property its default of `false`.  Alternatively when wanting to maximize use of available width, set this property to `true`.

</td>
</tr>

<tr>
<td>

[ColumnMargin](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel.ColumnMargin) Property

</td>
<td>

Gets or sets the margin width between columns.  The default value is `50`.

</td>
</tr>

<tr>
<td>

[ColumnMinWidth](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel.ColumnMinWidth) Property

</td>
<td>

Gets or sets the minimum column width needed for another column to be added.  The default value is `300`.

</td>
</tr>

<tr>
<td>

[MaxColumnCount](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel.MaxColumnCount) Property

</td>
<td>

Gets or sets maximum column count.  The default value is `2`.

</td>
</tr>

</tbody>
</table>
