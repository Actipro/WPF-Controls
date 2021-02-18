---
title: "Specifying Items"
page-title: "Specifying Items - XY Chart Features"
order: 13
---
# Specifying Items

The chart control can be bound to any custom data source and supports several data types.

## Supported Quantifiable Axis Types

The chart control supports a fixed set of quantifiable data types along each axis.

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

When the X or Y axis values are of these data types, then the values can be mapped on an axis according to their relative position within the axis `Minimum` and `Maximum` values.

## Other Axis Types

For all other types of data, including complex objects and `String`, they will be mapped using [XYGroupedAxis](xref:ActiproSoftware.Windows.Controls.Charts.XYGroupedAxis).

When using [XYGroupedAxis](xref:ActiproSoftware.Windows.Controls.Charts.XYGroupedAxis), items will be grouped based on object equality by default. Items can be grouped using custom logic by specifying [XYGroupedAxis](xref:ActiproSoftware.Windows.Controls.Charts.XYGroupedAxis).[GroupingFunc](xref:ActiproSoftware.Windows.Controls.Charts.XYGroupedAxis.GroupingFunc).

## Setting ItemsSource

Each series in a chart control can be bound to a separate data source, or to different properties on the same data source.  The [ItemsSource](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.SeriesBase.ItemsSource) property on each series can be set to any collection of objects, including custom objects, as long as the collection implements `IEnumerable`.

In this case, the [XPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.XPath) and/or [YPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.YPath) properties must be set to the name of a property on the objects in the collection.

> [!NOTE]
> You can traverse complex hierarchies to get to the value you need by using a property path that is delimited by a period (".").  For example you can set [XPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.XPath) or [YPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.YPath) to something like "MyProperty.MyOtherProperty.MyDoubleValue", similar to how binding paths work in XAML.

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

We can specify that the Y values should be pulled from the `Amount` property by setting [YPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.YPath) to `"Amount"`.  If we want to use the index in the collection as our X values, then we do not need to set [XPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.XPath).

If instead we want the X values to be pulled from the `Date` property, then we would need to set [XPath](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.XYSeriesBase.XPath) to `"Date"`.
