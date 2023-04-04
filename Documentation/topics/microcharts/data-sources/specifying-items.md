---
title: "Specifying Items"
page-title: "Specifying Items - Micro Charts Data Sources"
order: 2
---
# Specifying Items

The chart control can be bound to any custom data source and supports several data types.

## Supported Axis Types

The chart control supports a fixed set of data types along each axis.

The supported data types include:

- `Byte`
- `DateTime`
- `Decimal`
- `Double`
- `Int16`
- `Int32`
- `Int64`
- `Float`
- `SByte`
- `UInt16`
- `UInt32`
- `UInt64`

An exception will be thrown if an unsupported data type is specified.

## Setting ItemsSource

Each series in a chart control can be bound to a separate data source, or to different properties on the same data source.  The [ItemsSource](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroSeriesBase.ItemsSource) property on each series can be set to any collection of objects, including custom objects, as long as the collection implements `IEnumerable`.

There are two types of collections that can be used, simple (such as list of `Double`) and complex (such as a list of custom objects).

### Simple Collections

A collection of supported axis data types (shown above) can be used as-is without specifying a path to a source property.  The index in the collection will be used as the secondary value (which is typically the X value) and the actual value in the collection will be used as the primary value (which is typically the Y value).

In this example, the line series is set to a collection of doubles, defined inline in XAML. Therefore, the X values will range from 0 to 11 (since there are 12 entries) and the actual values shown will be used along the Y axis:

```xaml
<microcharts:MicroLineSeries ItemsSource="7;3;5;1;1;3;10;5;2;8;1;14" />
```

Assuming the `Counts` binding below in the ItemsSource referenced an `IEnumerable` of the same numeric data values as above, this code would produce identical output:

```xaml
<microcharts:MicroLineSeries ItemsSource="{Binding Counts}" />
```

### Complex Collections

Collections of custom complex objects can also be used.  In this case, the [XPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.XPath) and/or [YPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.YPath) properties must be set to the name of a property on the objects in the collection.

For example, consider the following custom object:

```csharp
public class SalesData {

	private decimal amount;
	private DateTime date;
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////
	// OBJECT
	/////////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Initializes a new instance of the <c>SalesData</c> class.
	/// </summary>
	/// <param name="date">The date for which the amount is specified.</param>
	/// <param name="amount">The sales amount.</param>
	public SalesData(DateTime date, decimal amount) {
		this.date = date;
		this.amount = amount;
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////
	// PUBLIC PROCEDURES
	/////////////////////////////////////////////////////////////////////////////////////////////////////
	
	/// <summary>
	/// Gets the sales amount.
	/// </summary>
	/// <value>The sales amount.</value>
	public decimal Amount { 
		get {
			return amount;
		}
	}

	/// <summary>
	/// Gets the date for which the amount is specified.
	/// </summary>
	/// <value>The date for which the amount is specified.</value>
	public DateTime Date { 
		get {
			return date;
		}
	}

}
```

We can specify that the Y values should be pulled from the `Amount` property by setting [YPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.YPath) to `"Amount"`.  If we want to use the index in the collection as our X values, then we do not need to set [XPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.XPath).

If instead we want the X values to be pulled from the `Date` property, then we would need to set [XPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.XPath) to `"Date"`.

> [!NOTE]
> You can traverse complex hierarchies to get to the value you need by using a property path that is delimited by a period (".").  For example you can set [XPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.XPath) or [YPath](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.YPath) to something like "MyProperty.MyOtherProperty.MyDoubleValue", similar to how binding paths work in XAML.
