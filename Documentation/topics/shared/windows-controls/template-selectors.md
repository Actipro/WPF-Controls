---
title: "Template Selectors"
page-title: "Template Selectors - Shared Library Controls"
order: 130
---
# Template Selectors

Template selectors (i.e., `DataTemplateSelector`) are primarily used with a `ContentPresenter` to automatically select the best `DataTemplate` for the current `Content`.

## ImageTemplateSelector Overview

The [ImageTemplateSelector](xref:@ActiproUIRoot.Controls.ImageTemplateSelector) can be used to define an image from multiple sources.  The following `DataTemplate` properties are supported:

| Property | Description |
| ----- | ----- |
| [DefaultTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.DefaultTemplate) | Defines the template to be used for `null` data values. This property is typically undefined but can be populated to insert a placeholder for undefined image data instead of leaving it empty.
| [GeometryTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.GeometryTemplate)  | Defines the template to be used with `Geometry` data values. This is typically a `Path` filled with the `TextElement.Foreground` brush. |
| [ImageSourceTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.ImageSourceTemplate)  | Defines the template to be used with `ImageSource` data values. This is typically a [DynamicImage](dynamicimage.md) control.  |

Some controls will have their image properties based on the `object` data type instead of `ImageSource`.  For those controls, the corresponding control template will use a `ContentPresenter` with the image property as `Content` and assign a default instance of [ImageTemplateSelector](xref:@ActiproUIRoot.Controls.ImageTemplateSelector) for the `ContentTemplateSelector`.


The following example demonstrates creating an instance of [ImageTemplateSelector](xref:@ActiproUIRoot.Controls.ImageTemplateSelector) using templates that are consistent with typical usage scenarios:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...

<shared:ImageTemplateSelector x:Key="ImageTemplateSelector">

	<shared:ImageTemplateSelector.GeometryTemplate>
		<DataTemplate>
			<Viewbox>
				<Path Fill="{Binding Path=(TextElement.Foreground), RelativeSource={RelativeSource Mode=Self}}" Data="{Binding}" />
			</Viewbox>
		</DataTemplate>
	</shared:ImageTemplateSelector.GeometryTemplate>

	<shared:ImageTemplateSelector.ImageSourceTemplate>
		<DataTemplate>
			<shared:DynamicImage Source="{Binding}" />
		</DataTemplate>
	</shared:ImageTemplateSelector.ImageSourceTemplate>

</shared:ImageTemplateSelector>
```

The following data values are recognized by the [ImageTemplateSelector](xref:@ActiproUIRoot.Controls.ImageTemplateSelector):

| Content | Description |
| ----- | ----- |
| `Geometry` | `Geometry` values (i.e. path data) will use [GeometryTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.GeometryTemplate) |
| `ImageSource` | `ImageSource` values will use [ImageSourceTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.ImageSourceTemplate) |
| `null` | `null` values will use [DefaultTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.DefaultTemplate). |
| `string` | `string` values will be analyzed to determine the type of data represented by the `string` and use the most appropriate template. See the "String Value Processing" section below for more details.
| `Uri` | `Uri` values will use [ImageSourceTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.ImageSourceTemplate). |

> [!NOTE]
> Non-`null` data values will return a `null` reference as the `DataTemplate` that allows the `ContentPresenter` to use default logic for presenting the data .

### String Value Processing

A `string` value is analyzed to determine the type of data represented so the best template can be used.

Any `string` that starts with `"/"` or `"pack::"` is assumed to be a `Uri` for an `ImageSource` and will use [ImageSourceTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.ImageSourceTemplate).  Set the [AllowStringUri](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.AllowStringUri) instance property to `false` to prevent processing a `string` value as a `Uri`.  The [DefaultAllowStringUri](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.DefaultAllowStringUri) static property can be set to `false` to disable `Uri` processing for all new class instances.

Any `string` that can be parsed by `Geometry.Parse` without throwing an exception will use [GeometryTemplate](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.GeometryTemplate).  Set the [AllowStringGeometry](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.AllowStringGeometry) instance property to `false` to prevent processing a `string` value as a `Geometry`.  The [DefaultAllowStringGeometry](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.DefaultAllowStringGeometry) static property can be set to `false` to disable `Geometry` processing for all new class instances.

> [!IMPORTANT]
> When [AllowStringGeometry](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.AllowStringGeometry) is `true`, a `FormatException` can be thrown when attempting to parse the `string`.  This exception is handled but may appear in the **Output** window of debuggers like Visual Studio.  If `Geometry` strings are not used by an application, set the [DefaultAllowStringGeometry](xref:@ActiproUIRoot.Controls.ImageTemplateSelector.DefaultAllowStringGeometry) static property to `false` to prevent exceptions from failed parse attempts.

The following demonstrates several common ways to define a custom icon for the [InfoBar](info-bar.md) control which utilizes [ImageTemplateSelector](xref:@ActiproUIRoot.Controls.ImageTemplateSelector):

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...

<!-- String URI will use ImageSourceTemplate -->
<shared:InfoBar ... Icon="/Images/Icons/Help16.png" />

<!-- String Geometry will use GeometryTemplate -->
<shared:InfoBar ... Icon="M 0,0 L 10,10 M 0,10 L 10,0" />

<!-- Any unrecognized non-null data type is used directly -->
<shared:InfoBar ... >
	<shared:InfoBar.Icon>
		<Border BorderThickness="1" BorderBrush="Black">
			<Image Source="/Images/Icons/Help16.png" />
		</Border>
	</shared:InfoBar.Icon>
</shared:InfoBar>
```