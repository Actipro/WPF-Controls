---
title: "Data"
page-title: "Data - Shared Library Reference"
order: 6
---
# Data

The [ActiproSoftware.Windows.Data](xref:@ActiproUIRoot.Data) namespace contains several classes that are helpful for working with data.

## Value Converters

There are numerous value converter classes in this namespace.

All of the available value converters from this namespace and others in the Shared Library are described in the [Value Converters](value-converters.md) topic.

## The MathHelper Class

The [MathHelper](xref:@ActiproUIRoot.Data.MathHelper) class provides static methods that provide additional math operations not found in the `Math` class.

| Member | Description |
|-----|-----|
| [Max](xref:@ActiproUIRoot.Data.MathHelper.Max*) Method | Returns the largest value from the specified numbers. |
| [Min](xref:@ActiproUIRoot.Data.MathHelper.Min*) Method | Returns the smallest value from the specified numbers. |
| [Range](xref:@ActiproUIRoot.Data.MathHelper.Range*) Method | Returns the specified value constrained to the specified minimum and maximum values. |
| [Round](xref:@ActiproUIRoot.Data.MathHelper.Round*) Method | Rounds a double value based on the specified [RoundMode](xref:@ActiproUIRoot.Controls.RoundMode), which supports several methods of rounding the value. |

## The NullableExtension Class

The [NullableExtension](xref:@ActiproUIRoot.Data.NullableExtension) class is a markup extension that can be used to define an instance of `Nullable<T>` in XAML.

## The ValidationHelper Class

The [ValidationHelper](xref:@ActiproUIRoot.Data.ValidationHelper) class provides a number of static methods that can be called in the validation argument of a dependency property registration.

| Member | Description |
|-----|-----|
| [ValidateDoubleIsBetweenInclusive](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsBetweenInclusive*) Method | Validates that the object is a `Double` whose value is inclusively between two numbers. |
| [ValidateDoubleIsGreaterThan](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsGreaterThan*) Method | Validates that the object is a `Double` greater than the specified number. |
| [ValidateDoubleIsGreaterThanOrEqual](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsGreaterThanOrEqual*) Method | Validates that the object is a `Double` greater than or equal to the specified number. |
| [ValidateDoubleIsNumber](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsNumber*) Method | Validates that the object is a `Double` is not `NaN` or infinity. |
| [ValidateDoubleIsNumberOrNaN](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsNumberOrNaN*) Method | Validates that the object is a `Double` is not infinity. |
| [ValidateDoubleIsPercentage](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsPercentage*) Method | Validates that the object is a `Double` between `0` and `1` inclusive. |
| [ValidateDoubleIsPositive](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsPositive*) Method | Validates that the object is a `Double` greater than or equal to `0`. |
| [ValidateDoubleIsPositiveOrNaN](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsPositiveOrNaN*) Method | Validates that the object is a `Double` greater than or equal to `0` or `NaN`. |
| [ValidateDurationIsPositiveTimeSpan](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDurationIsPositiveTimeSpan*) Method | Validates that the object is a `Duration` is a positive `TimeSpan`. |
| [ValidateInt32IsBetweenInclusive](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateInt32IsBetweenInclusive*) Method | Validates that the object is an `Int32` whose value is inclusively between two numbers. |
| [ValidateInt32IsGreaterThanOrEqual](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateInt32IsGreaterThanOrEqual*) Method | Validates that the object is an `Int32` greater than or equal to the specified number. |
| [ValidateInt32IsPositive](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateInt32IsPositive*) Method | Validates that the object is an `Int32` greater than or equal to `0`. |
| [ValidateInt64IsBetweenInclusive](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateInt64IsBetweenInclusive*) Method | Validates that the object is an `Int64` whose value is inclusively between two numbers. |
| [ValidateInt64IsGreaterThanOrEqual](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateInt64IsGreaterThanOrEqual*) Method | Validates that the object is an `Int64` greater than or equal to the specified number. |
| [ValidateInt64IsPositive](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateInt64IsPositive*) Method | Validates that the object is an `Int64` greater than or equal to `0`. |

This sample code shows how to use the [ValidateDoubleIsPercentage](xref:@ActiproUIRoot.Data.ValidationHelper.ValidateDoubleIsPercentage*) method to validate that the dependency property value is a valid percentage.

```csharp
public static DependencyProperty PercentProperty = 
	DependencyProperty.Register("Percent", typeof(double), typeof(MyClass), new FrameworkPropertyMetadata(0.0), 
	new ValidateValueCallback(ValidationHelper.ValidateDoubleIsPercentage));
```
