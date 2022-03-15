---
title: "FontFamilyComboBox"
page-title: "FontFamilyComboBox - Ribbon Controls"
order: 6
---
# FontFamilyComboBox

The [FontFamilyComboBox](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontFamilyComboBox) class provides an implementation of a `ComboBox` that allows for selection of a font family.  The font family list is automatically populated.

> [!NOTE]
> See the [Control Basics](../control-basics.md) topic for many implementation details that are common to the built-in controls such as this one.

## Variants

This control supports numerous UI styles (called variants) based on its [Context](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.Context) and [VariantSize](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.VariantSize) property settings.

| Context | Variant Size | Sample UI |
|-----|-----|-----|
| MenuItem | (all) | ![Screenshot](../../images/fontfamilycombobox-menu-item-medium.gif) |
| (any other) | Medium (when in StackPanel) | ![Screenshot](../../images/fontfamilycombobox-medium.gif) |
| (any other) | (any other) | ![Screenshot](../../images/fontfamilycombobox-small.gif) |

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
| Default CommandParameter | [FontFamilyValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.FontFamilyValueCommandParameter). |

## Managing the Selected Value

This control is designed to use the WPF [command model](../../command-model/index.md) to maintain the selected value of the control.  By default an [FontFamilyValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.FontFamilyValueCommandParameter) is assigned as the `CommandParameter` of the control.

See the [Interaction with Value Controls](../../command-model/value-controls.md) topic for detailed information on using this command parameter to manage the control's value and support live preview.

## Layout on the Ribbon and in Menus

It is recommended that when a `ComboBox` is used in the ribbon that a specific `Width` be set.  When used within a [StackPanel](../layout/stackpanel.md), controls such as `ComboBox` (that don't draw labels inside their bounds like [Button](button.md) controls do) will be lined up and stacked vertically and their optional labels and images will be drawn to the left of them.

When used in a [Menu](../miscellaneous/menu.md), a `MinWidth` should be set on the `ComboBox`.  Since the left edge of controls like `ComboBox` will vary based on the width of its `Label`, setting a `MinWidth` ensures that the `ComboBox` itself will be at least a certain appropriate width and can grow as needed to fill the width of the menu.

## Working with the FontFamilyComboBox

The ribbon `FontFamilyComboBox` indirectly inherits the native WPF `ComboBox` control, so anything you can do in the native WPF `ComboBox` you can do in the ribbon `FontFamilyComboBox`.

Please see the MSDN documentation on the native WPF `ComboBox` for more details on its capabilities.

You can also use the `SelectedValue` property to get/set the actual selected `FontFamily` value.  The static [FontFamilyComboBox](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontFamilyComboBox).[GetFontFamily](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontFamilyComboBox.GetFontFamily*) method can be used to retrieve a `FontFamily` from a font family name, allowing you to set the control's value manually like this:

```csharp
myFontFamilyComboBox.SelectedValue = FontFamilyComboBox.GetFontFamily("Courier New");
```

## Setting Whether Items Display Text Using the Font Family

By default each item in the drop-down will draw its font family name in the actual `FontFamily` that it represents.  This behavior can be disabled by setting the [FontFamilyComboBox](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontFamilyComboBox).[UsePreviewInItemRendering](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontFamilyComboBox.UsePreviewInItemRendering) property to `false`.

When this feature is disabled, each item will draw the font family name in the standard UI font.

## Validating Typefaces

The control has a [IsTypefaceValidationEnabled](xref:@ActiproUIRoot.Controls.Ribbon.Controls.FontFamilyComboBox.IsTypefaceValidationEnabled) property that defaults to `false`.  When set to `true`, it will examine each font family's typefaces as the font family is being added to the combobox.  If the font family's typefaces cannot be loaded for some reason (which can occur with some Adobe fonts), it will not be added to the control.  The downside of this feature is that it can potentially cause a couple second delay during window load time, thus it is disabled by default.

## Sample XAML

This code shows how to prototype this control in XAML-only:

```xaml
<ribbon:FontFamilyComboBox Width="135" Label="Font Family" KeyTipAccessText="FF" />
```

This code shows how to prototype this control in in XAML but by also using a ribbon command to set up its user interface:

```xaml
<ribbon:FontFamilyComboBox Width="135" 
	Command="sample:ApplicationCommands.FontFamily" KeyTipAccessText="FF" />
```
