---
title: "Property Events"
page-title: "Property Events - PropertyGrid Features"
order: 27
---
# Property Events

The property grid has several property events that get raised in various scenarios.

> [!NOTE]
> Since [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) indirectly inherits [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox), all of the events described for that control such as those related to [selection](../tree-control-features/selection.md), [expansion](../tree-control-features/expandability.md), etc. apply to property grid as well.

## Property Change Events

The [OnPropertyValueChanging](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.OnPropertyValueChanging*) and [PropertyValueChanged](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyValueChanged) events are raised before and after, respectively, a property's value is updated through the property grid.  The event arguments for both events include the [PropertyModelValueChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelValueChangeEventArgs).[PropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelValueChangeEventArgs.PropertyModel) being changed and the new [Value](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelValueChangeEventArgs.Value).

To cancel an update to the property value, handle the [OnPropertyValueChanging](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.OnPropertyValueChanging*) event and set `e.Cancel` to `true`.  This will prevent the new value from being assigned and will not raise the [PropertyValueChanged](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyValueChanged) event.

## Collection Change Events

When a property model represents a collection property, there are several events relating to the adding and removing of child items.

See the [Collections](collections.md) topic for a description of each of these events and when they occur.
