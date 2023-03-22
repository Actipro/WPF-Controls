---
title: "FontSizeComboBox"
page-title: "FontSizeComboBox - Ribbon Controls"
order: 7
---
# FontSizeComboBox

The [FontSizeComboBox](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontSizeComboBox) class provides an implementation of a `ComboBox` that allows for selection of a font size.  The font size list is automatically populated.

> [!NOTE]
> See the [Control Basics](../control-basics.md) topic for many implementation details that are common to the built-in controls such as this one.

## Variants

This control supports numerous UI styles (called variants) based on its [Context](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.Context) and [VariantSize](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.VariantSize) property settings.

| Context | Variant Size | Sample UI |
|-----|-----|-----|
| MenuItem | (all) | ![Screenshot](../../images/fontsizecombobox-menu-item-medium.gif) |
| (any other) | Medium (when in StackPanel) | ![Screenshot](../../images/fontsizecombobox-medium.gif) |
| (any other) | (any other) | ![Screenshot](../../images/fontsizecombobox-small.gif) |

## Capabilities

The following table gives an overview of the capabilities of the control.

| Item | Details |
|-----|-----|
| Supports tall size (fills height of [Group](../miscellaneous/group.md)) | No.  When in the ribbon, it should be placed in a [StackPanel](../layout/stackpanel.md) that is in a `Medium` variant or smaller. |
| Supports normal size | Yes. |
| Supports use in a [Menu](../miscellaneous/menu.md) | Yes. |
| Base class | [ComboBox](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ComboBox). |
| Child items | Yes.  The items collection inherits from the native WPF `ComboBox` class and data binding to `ItemsSource` is fully supported. |
| Has popup | Yes.  The items are displayed in the popup. |
| Key tip access | Yes.  Set via the [KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.KeyTipAccessText) property. |
| Is key tip scope | No. |
| Click event trigger | When the `Enter` key is pressed while in the control, when a new `ComboBox` selection is made, or when the control loses focus and the value is changed. |
| Supports use outside of Ribbon | Yes. |
| Supports commands | Yes. |
| Supports [ICheckableCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.ICheckableCommandParameter) | No. |
| Supports [IValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.IValueCommandParameter) | Yes.  Controls the selected value of the `ComboBox` and supports live preview. |
| Default CommandParameter | [DoubleValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.DoubleValueCommandParameter). |

## Managing the Selected Value

This control is designed to use the WPF [command model](../../command-model/index.md) to maintain the selected value of the control.  By default an [DoubleValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.DoubleValueCommandParameter) is assigned as the `CommandParameter` of the control.

See the [Interaction with Value Controls](../../command-model/value-controls.md) topic for detailed information on using this command parameter to manage the control's value and support live preview.

## Layout on the Ribbon and in Menus

It is recommended that when a `ComboBox` is used in the ribbon that a specific `Width` be set.  When used within a [StackPanel](../layout/stackpanel.md), controls such as `ComboBox` (that don't draw labels inside their bounds like [Button](button.md) controls do) will be lined up and stacked vertically and their optional labels and images will be drawn to the left of them.

When used in a [Menu](../miscellaneous/menu.md), a `MinWidth` should be set on the `ComboBox`.  Since the left edge of controls like `ComboBox` will vary based on the width of its `Label`, setting a `MinWidth` ensures that the `ComboBox` itself will be at least a certain appropriate width and can grow as needed to fill the width of the menu.

## Working with the FontSizeComboBox

The ribbon `FontSizeComboBox` indirectly inherits the native WPF `ComboBox` control, so anything you can do in the native WPF `ComboBox` you can do in the ribbon `FontSizeComboBox`.

Please see the MSDN documentation on the native WPF `ComboBox` for more details on its capabilities.

You can also use the `SelectedValue` property to get/set the actual selected `Double` value.  Set the control's value manually like this:

```csharp
myFontSizeComboBox.SelectedValue = 12.0;
```

## Sample XAML

This code shows how to prototype this control in XAML-only:

```xaml
<ribbon:ComboBox MinWidth="40" Label="Font Size" KeyTipAccessText="FS" />
```

This code shows how to prototype this control in XAML but by also using a ribbon command to set up its user interface:

```xaml
<ribbon:FontSizeComboBox MinWidth="40" 
	Command="sample:ApplicationCommands.FontSize" KeyTipAccessText="FS" />
```
