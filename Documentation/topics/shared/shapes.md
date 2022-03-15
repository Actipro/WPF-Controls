---
title: "Shapes"
page-title: "Shapes - Shared Library Reference"
order: 10
---
# Shapes

The [ActiproSoftware.Windows.Shapes](xref:@ActiproUIRoot.Shapes) namespace contains shapes that can be used in XAML.

It can be imported with this XML namespace import:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
```

## SemiEllipse Class

The [SemiEllipse](xref:@ActiproUIRoot.Shapes.SemiEllipse) class defines a half ellipse shape.

These properties are important:

- [ApexSide](xref:@ActiproUIRoot.Shapes.SemiEllipse.ApexSide) - The side upon which the semi-ellipse apex appears.
- [IsClosed](xref:@ActiproUIRoot.Shapes.SemiEllipse.IsClosed) - Whether the side opposite the apex is closed.

```xaml
<shared:SemiEllipse Width="40" Height="20" ApexSide="Top" Fill="Red" />
```

## Triangle Class

The [Triangle](xref:@ActiproUIRoot.Shapes.Triangle) class defines a triangle shape.

These properties are important:

- [ApexSide](xref:@ActiproUIRoot.Shapes.Triangle.ApexSide) - The side upon which the triangle apex appears.
- [IsClosed](xref:@ActiproUIRoot.Shapes.Triangle.IsClosed) - Whether the side opposite the apex is closed.

```xaml
<shared:Triangle Width="20" Height="20" ApexSide="Top" Fill="Red" />
```

## Wave Class

The [Wave](xref:@ActiproUIRoot.Shapes.Wave) class defines a wave shape.

These properties are important:

- [ApexCount](xref:@ActiproUIRoot.Shapes.Wave.ApexCount) - The number of apexes in the wave.
- [ApexSide](xref:@ActiproUIRoot.Shapes.Wave.ApexSide) - The side upon which the wave apex appears.
- [IsInverted](xref:@ActiproUIRoot.Shapes.Wave.IsInverted) - Whether the 'inside' of the shape occurs on the same side as the apex.

```xaml
<shared:Wave Width="200" Height="10" ApexSide="Top" ApexCount="10" Stroke="Red" StrokeThickness="1" />
```

## ZigZag Class

The [ZigZag](xref:@ActiproUIRoot.Shapes.ZigZag) class defines a zig-zag shape.

These properties are important:

- [ApexCount](xref:@ActiproUIRoot.Shapes.ZigZag.ApexCount) - The number of apexes in the zig-zag.
- [ApexSide](xref:@ActiproUIRoot.Shapes.ZigZag.ApexSide) - The side upon which the zig-zag apex appears.
- [IsInverted](xref:@ActiproUIRoot.Shapes.ZigZag.IsInverted) - Whether the 'inside' of the shape occurs on the same side as the apex.

```xaml
<shared:ZigZag Width="200" Height="10" ApexSide="Top" ApexCount="10" Stroke="Red" StrokeThickness="1" />
```
