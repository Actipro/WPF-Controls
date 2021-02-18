---
title: "Shapes"
page-title: "Shapes - Shared Library Reference"
order: 10
---
# Shapes

The [ActiproSoftware.Windows.Shapes](xref:ActiproSoftware.Windows.Shapes) namespace contains shapes that can be used in XAML.

It can be imported with this XML namespace import:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
```

## Available Shapes

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

[SemiEllipse](xref:ActiproSoftware.Windows.Shapes.SemiEllipse)

</td>
<td>

A half ellipse shape.

These properties are important:

- [ApexSide](xref:ActiproSoftware.Windows.Shapes.SemiEllipse.ApexSide) - The side upon which the semi-ellipse apex appears.
- [IsClosed](xref:ActiproSoftware.Windows.Shapes.SemiEllipse.IsClosed) - Whether the side opposite the apex is closed.

```xaml
<shared:SemiEllipse Width="40" Height="20" ApexSide="Top" Fill="Red" />
```

</td>
</tr>

<tr>
<td>

[Triangle](xref:ActiproSoftware.Windows.Shapes.Triangle)

</td>
<td>

A triangle shape.

These properties are important:

- [ApexSide](xref:ActiproSoftware.Windows.Shapes.Triangle.ApexSide) - The side upon which the triangle apex appears.
- [IsClosed](xref:ActiproSoftware.Windows.Shapes.Triangle.IsClosed) - Whether the side opposite the apex is closed.

```xaml
<shared:Triangle Width="20" Height="20" ApexSide="Top" Fill="Red" />
```

</td>
</tr>

<tr>
<td>

[Wave](xref:ActiproSoftware.Windows.Shapes.Wave)

</td>
<td>

A wave shape.

These properties are important:

- [ApexCount](xref:ActiproSoftware.Windows.Shapes.Wave.ApexCount) - The number of apexes in the wave.
- [ApexSide](xref:ActiproSoftware.Windows.Shapes.Wave.ApexSide) - The side upon which the wave apex appears.
- [IsInverted](xref:ActiproSoftware.Windows.Shapes.Wave.IsInverted) - Whether the 'inside' of the shape occurs on the same side as the apex.

```xaml
<shared:Wave Width="200" Height="10" ApexSide="Top" ApexCount="10" Stroke="Red" StrokeThickness="1" />
```

</td>
</tr>

<tr>
<td>

[ZigZag](xref:ActiproSoftware.Windows.Shapes.ZigZag)

</td>
<td>

A zig-zag shape.

These properties are important:

- [ApexCount](xref:ActiproSoftware.Windows.Shapes.ZigZag.ApexCount) - The number of apexes in the zig-zag.
- [ApexSide](xref:ActiproSoftware.Windows.Shapes.ZigZag.ApexSide) - The side upon which the zig-zag apex appears.
- [IsInverted](xref:ActiproSoftware.Windows.Shapes.ZigZag.IsInverted) - Whether the 'inside' of the shape occurs on the same side as the apex.

```xaml
<shared:ZigZag Width="200" Height="10" ApexSide="Top" ApexCount="10" Stroke="Red" StrokeThickness="1" />
```

</td>
</tr>

</tbody>
</table>
