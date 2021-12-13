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

## SemiEllipse Class

The [SemiEllipse](xref:ActiproSoftware.Windows.Shapes.SemiEllipse) class defines a half ellipse shape.

These properties are important:

- [ApexSide](xref:ActiproSoftware.Windows.Shapes.SemiEllipse.ApexSide) - The side upon which the semi-ellipse apex appears.
- [IsClosed](xref:ActiproSoftware.Windows.Shapes.SemiEllipse.IsClosed) - Whether the side opposite the apex is closed.

```xaml
<shared:SemiEllipse Width="40" Height="20" ApexSide="Top" Fill="Red" />
```

## Triangle Class

The [Triangle](xref:ActiproSoftware.Windows.Shapes.Triangle) class defines a triangle shape.

These properties are important:

- [ApexSide](xref:ActiproSoftware.Windows.Shapes.Triangle.ApexSide) - The side upon which the triangle apex appears.
- [IsClosed](xref:ActiproSoftware.Windows.Shapes.Triangle.IsClosed) - Whether the side opposite the apex is closed.

```xaml
<shared:Triangle Width="20" Height="20" ApexSide="Top" Fill="Red" />
```

## Wave Class

The [Wave](xref:ActiproSoftware.Windows.Shapes.Wave) class defines a wave shape.

These properties are important:

- [ApexCount](xref:ActiproSoftware.Windows.Shapes.Wave.ApexCount) - The number of apexes in the wave.
- [ApexSide](xref:ActiproSoftware.Windows.Shapes.Wave.ApexSide) - The side upon which the wave apex appears.
- [IsInverted](xref:ActiproSoftware.Windows.Shapes.Wave.IsInverted) - Whether the 'inside' of the shape occurs on the same side as the apex.

```xaml
<shared:Wave Width="200" Height="10" ApexSide="Top" ApexCount="10" Stroke="Red" StrokeThickness="1" />
```

## ZigZag Class

The [ZigZag](xref:ActiproSoftware.Windows.Shapes.ZigZag) class defines a zig-zag shape.

These properties are important:

- [ApexCount](xref:ActiproSoftware.Windows.Shapes.ZigZag.ApexCount) - The number of apexes in the zig-zag.
- [ApexSide](xref:ActiproSoftware.Windows.Shapes.ZigZag.ApexSide) - The side upon which the zig-zag apex appears.
- [IsInverted](xref:ActiproSoftware.Windows.Shapes.ZigZag.IsInverted) - Whether the 'inside' of the shape occurs on the same side as the apex.

```xaml
<shared:ZigZag Width="200" Height="10" ApexSide="Top" ApexCount="10" Stroke="Red" StrokeThickness="1" />
```
