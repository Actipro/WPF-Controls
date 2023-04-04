---
title: "ComboBox"
page-title: "ComboBox - Ribbon Controls"
order: 5
---
# ComboBox

The [ComboBox](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ComboBox) class provides an implementation of native WPF `ComboBox` that has enhanced functionality for use in a ribbon.

> [!NOTE]
> See the [Control Basics](../control-basics.md) topic for many implementation details that are common to the built-in controls such as this one.

## Variants

This control supports numerous UI styles (called variants) based on its [Context](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.Context) and [VariantSize](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.VariantSize) property settings.

| Context | Variant Size | Sample UI |
|-----|-----|-----|
| MenuItem | (all) | ![Screenshot](../../images/combobox-menu-item-medium.gif) |
| (any other) | Medium (when in StackPanel) | ![Screenshot](../../images/combobox-medium.gif) |
| (any other) | (any other) | ![Screenshot](../../images/combobox-small.gif) |

## Capabilities

The following table gives an overview of the capabilities of the control.

| Item | Details |
|-----|-----|
| Supports tall size (fills height of [Group](../miscellaneous/group.md)) | No.  When in the ribbon, it should be placed in a [StackPanel](../layout/stackpanel.md) that is in a `Medium` variant or smaller. |
| Supports normal size | Yes. |
| Supports use in a [Menu](../miscellaneous/menu.md) | Yes. |
| Base class | [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase). |
| Child items | Yes.  The items collection inherits from the native WPF `ComboBox` class and data binding to `ItemsSource` is fully supported. |
| Has popup | Yes.  The items are displayed in the popup. |
| Key tip access | Yes.  Set via the [KeyTipAccessText](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase.KeyTipAccessText) property. |
| Is key tip scope | No. |
| Click event trigger | When the <kbd>Enter</kbd> key is pressed while in the control, when a new `ComboBox` selection is made, or when the control loses focus and the value is changed. |
| Supports use outside of Ribbon | Yes. |
| Supports commands | Yes. |
| Supports [ICheckableCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.ICheckableCommandParameter) | No. |
| Supports [IValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.IValueCommandParameter) | Yes.  Controls the selected value of the `ComboBox` and supports live preview. |
| Default CommandParameter | [ObjectValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.ObjectValueCommandParameter). |

## Managing the Selected Value

This control is designed to use the WPF [command model](../../command-model/index.md) to maintain the selected value of the control.  By default an [ObjectValueCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.ObjectValueCommandParameter) is assigned as the `CommandParameter` of the control.

See the [Interaction with Value Controls](../../command-model/value-controls.md) topic for detailed information on using this command parameter to manage the control's value and support live preview.

## Supporting Live Preview

When the mouse moves over an item, the item becomes the [ActiveItem](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ComboBox.ActiveItem).  Many applications use the currently "active" item to supply a live preview to the end user of what would happen if the item was clicked.  Live preview for comboboxes can be implemented in one of two ways.

If you are using commands, the [Interaction with Value Controls](../../command-model/value-controls.md) topic explains how to support live preview using command can-execute handlers.

Alternatively, if you would rather use events, the [ActiveItemChanged](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ComboBox.ActiveItemChanged) event fires whenever the [ActiveItem](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ComboBox.ActiveItem) property is changed.

## Text to Value Conversion

When the ComboBox is flagged as editable (via `IsEditable`), the [TextToValueConverter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ComboBox.TextToValueConverter) property should be set to a converter that is capable of converting from a string to your target item type like a `Brush`, etc.  This allows you to type in values that don't necessarily exist in the ComboBox's list and use the new value.

Note that when the items in the ComboBox are strings, no converter is necessary.

## Layout on the Ribbon and in Menus

It is recommended that when a `ComboBox` is used in the ribbon that a specific `Width` be set.  When used within a [StackPanel](../layout/stackpanel.md), controls such as `ComboBox` (that don't draw labels inside their bounds like [Button](button.md) controls do) will be lined up and stacked vertically and their optional labels and images will be drawn to the left of them.

When used in a [Menu](../miscellaneous/menu.md), a `MinWidth` should be set on the `ComboBox`.  Since the left edge of controls like `ComboBox` will vary based on the width of its `Label`, setting a `MinWidth` ensures that the `ComboBox` itself will be at least a certain appropriate width and can grow as needed to fill the width of the menu.

## Working with the ComboBox

The ribbon `ComboBox` indirectly inherits the native WPF `ComboBox` control, so anything you can do in the native WPF `ComboBox` you can do in the ribbon `ComboBox`.

Please see the MSDN documentation on the native WPF `ComboBox` for more details on its capabilities.

## Hint Text

Hint text is a faded out blurb of text that appears when a `TextBox` or `ComboBox` is empty.  It usually gives some simple instruction for related to the data that should be input to the control, or a description of that data that should be entered.

The hint text starts out somewhat opaque when there is no content in the control.  If the control gains focus, the hint text becomes slightly more transparent.  If content is entered, the hint text disappears completely.

![Screenshot](../../images/textbox-hint-text.gif)

*The same TextBox in three hint text states... the top TextBox doesn't have focus or content, the middle TextBox has focus but no content, and the bottom TextBox has focus and content*

Transitions between the various hint text states use smooth animations for visual appeal.

This code shows how to set hint text:

```xaml
<ribbon:ComboBox Width="100" HintText="Type here" />
```

## Sample XAML

This code shows how to prototype this control in XAML-only:

```xaml
<ribbon:ComboBox Width="140" ImageSourceSmall="/Images/Find16.png" Label="Find" KeyTipAccessText="F" />
```

This code shows how to prototype this control in XAML but by also using a ribbon command to set up its user interface:

```xaml
<ribbon:ComboBox Width="140" Command="ApplicationCommands.Find" KeyTipAccessText="F" />
```
