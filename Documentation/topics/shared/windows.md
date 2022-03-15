---
title: "Windows"
page-title: "Windows - Shared Library Reference"
order: 2
---
# Windows

The [ActiproSoftware.Windows](xref:@ActiproUIRoot) namespace contains numerous useful classes and event argument types that may be used in WPF.

## General

These general types are found in the namespace:

<table>
<thead>

<tr>
<th>Type</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[DisposableObjectBase](xref:@ActiproUIRoot.DisposableObjectBase)

</td>
<td>

A simple object that implements `IDisposable`.

</td>
</tr>

<tr>
<td>

[ILogicalParent](xref:@ActiproUIRoot.ILogicalParent)

</td>
<td>

Provides the base requirements for an element that can add and remove logical child elements.

> [!NOTE]
> This interface is used in conjunction with [LogicalChildrenCollection<T>](xref:@ActiproUIRoot.LogicalChildrenCollection`1).

</td>
</tr>

<tr>
<td>

[LogicalTreeHelperExtended](xref:@ActiproUIRoot.LogicalTreeHelperExtended)

</td>
<td>Contains static methods that are useful for performing common tasks with nodes in a logical tree.</td>
</tr>

<tr>
<td>

[ObservableObjectBase](xref:@ActiproUIRoot.ObservableObjectBase)

</td>
<td>

A simple object that implements `INotifyPropertyChanged`.

</td>
</tr>

<tr>
<td>

[Unit](xref:@ActiproUIRoot.Unit)

</td>
<td>Represents a length measurement in either pixels, inches, centimeters, points, or as a percentage.</td>
</tr>

<tr>
<td>

[UnitType](xref:@ActiproUIRoot.UnitType)

</td>
<td>

Specifies the unit of measurement represented by an instance of [Unit](xref:@ActiproUIRoot.Unit).

</td>
</tr>

</tbody>
</table>

## Collections

These collection types are found in the namespace:

| Type | Description |
|-----|-----|
| [DeferrableObservableCollection<T>](xref:@ActiproUIRoot.DeferrableObservableCollection`1) | Provides an `ObservableCollection` that is capable of suspending its property change notifications until a bulk update is complete. |
| [EnumerableView<T>](xref:@ActiproUIRoot.EnumerableView`1) | Represents a read-only view on top of an `IEnumerable<T>` with support for filtering, sorting, and change notifications. |
| [LogicalChildrenCollection<T>](xref:@ActiproUIRoot.LogicalChildrenCollection`1) | Provides a `DeferrableObservableCollection` that is capable of adding/removing the logical parent, and optionally the visual parent, of the items in the collection. |

## Event Arguments

These event argument types are found in the namespace:

<table>
<thead>

<tr>
<th>Type</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[CancelRoutedEventArgs](xref:@ActiproUIRoot.CancelRoutedEventArgs)

</td>
<td>Provides event arguments for a cancelable routed event.</td>
</tr>

<tr>
<td>

[ItemRoutedEventArgs<T>](xref:@ActiproUIRoot.ItemRoutedEventArgs`1)

</td>
<td>

Provides event arguments for an item-related routed event.

The Shared Library also includes some constructed implementations of this generic type:

- [ContextMenuItemRoutedEventArgs](xref:@ActiproUIRoot.Controls.ContextMenuItemRoutedEventArgs)
- [DependencyObjectItemRoutedEventArgs](xref:@ActiproUIRoot.DependencyObjectItemRoutedEventArgs)
- [ObjectItemRoutedEventArgs](xref:@ActiproUIRoot.ObjectItemRoutedEventArgs)

</td>
</tr>

<tr>
<td>

[PropertyChangedRoutedEventArgs<T>](xref:@ActiproUIRoot.PropertyChangedRoutedEventArgs`1)

</td>
<td>

Provides event arguments for a property change routed event, indicating the old and new values.

The Shared Library also includes some constructed implementations of this generic type:

- [BooleanPropertyChangedRoutedEventArgs](xref:@ActiproUIRoot.BooleanPropertyChangedRoutedEventArgs)
- [DateTimePropertyChangedRoutedEventArgs](xref:@ActiproUIRoot.DateTimePropertyChangedRoutedEventArgs)
- [DoublePropertyChangedRoutedEventArgs](xref:@ActiproUIRoot.DoublePropertyChangedRoutedEventArgs)
- [NullableDateTimePropertyChangedRoutedEventArgs](xref:@ActiproUIRoot.NullableDateTimePropertyChangedRoutedEventArgs)
- [ObjectPropertyChangedRoutedEventArgs](xref:@ActiproUIRoot.ObjectPropertyChangedRoutedEventArgs)
- [StringPropertyChangedRoutedEventArgs](xref:@ActiproUIRoot.StringPropertyChangedRoutedEventArgs)

</td>
</tr>

<tr>
<td>

[PropertyChangingRoutedEventArgs<T>](xref:@ActiproUIRoot.PropertyChangingRoutedEventArgs`1)

</td>
<td>

Provides event arguments for a cancelable property change routed event, indicating the old and new values.

The Shared Library also includes some constructed implementations of this generic type:

- [StringPropertyChangingRoutedEventArgs](xref:@ActiproUIRoot.StringPropertyChangingRoutedEventArgs)

</td>
</tr>

</tbody>
</table>
