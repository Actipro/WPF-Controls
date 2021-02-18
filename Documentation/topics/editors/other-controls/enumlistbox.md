---
title: "EnumListBox"
page-title: "EnumListBox - Other Editors Controls"
order: 10
---
# EnumListBox

The [EnumListBox](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox) control automatically presents the values of an enumeration with a more intuitive interface.

![Screenshot](../images/enumlistbox-non-flags.png)

## Enumeration Value and Type

The [EnumValue](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.EnumValue) property provides a quick and easy way to setup to the list box.  The [EnumType](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.EnumType) property is used to to build the items presented by the `EnumListBox`. The items are constructed by reflecting the enumeration type, with full support for the `FlagsAttribute`.

When the [EnumValue](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.EnumValue) property is bound/set to a non-null enumeration value, then the enumeration type will be automatically set (if it has not been explicitly set).  Therefore, the [EnumType](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.EnumType) only needs to be set when [EnumValue](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.EnumValue) is bound to a nullable enumeration type.

By default, the items are presented in a standard single selection list box style.  When the enumeration type is marked with the `FlagsAttribute`, then the items are presented using a `CheckBox` control and the list box allows multiple selection. In addition, flag enumerations are separate into three sections.  The first section contains values that are equal to zero.  The second section contains union values, or values that have more than one bit set.  The third section contains the remaing values, which have one and only one bit set.

![Screenshot](../images/enumlistbox-flags.png)

*EnumListBox bound to a flags enum*

## Using Display Attributes

Sometimes it is helpful to display an alternate text version of an enumeration value, especially when the values are made of multiple concatenated words.  For instance, an enumeration value named "FooBar" might appear nicer as "Foo Bar".

This scenario is fully supported by [EnumListBox](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox).  A `System.ComponentModel.DataAnnotations.DisplayAttribute` can be applied to a value to give it an alternate textual description.  Then as long as the [UseDisplayAttributes](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.UseDisplayAttributes) property is set to `true`, that alternate text will be used.

If the `DisplayAttribute.ResourceType` property is left blank, it will use the direct value specified by the `Name` property.  Otherwise, it will look in the specified resource `Type` for a localized resource value within the property indicated by `Name`.

In this example, the `DisplayAttribute` will look for a property named `MyFirstValue` in the string resources type `MyResources` and use that property's value:

```csharp
public enum SampleEnum {

	[Display(ResourceType = typeof(MyResources), Name = "MyFirstValue")]
	MyFirstValue = 1

	...
}
```

> [!NOTE]
> The WPF version of [EnumListBox](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox) also supports `System.ComponentModel.DescriptionAttribute` to supply textual descriptions in place of `System.ComponentModel.DataAnnotations.DisplayAttribute`.

## Custom Sorting

By default, values are listed in the order they are defined.  The exception is that in flags enums, group values get placed together.

Sorting can be altered by implementing a custom `IComparer<Enum>` class and assigning it to the [EnumSortComparer](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.EnumSortComparer) property.  The [EnumValueNameSortComparer](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.EnumValueNameSortComparer).[Instance](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.EnumValueNameSortComparer.Instance) static property provides access to a pre-built comparer for listing enumeration values alphabetically by name.

## Hiding Enumeration Values

By default, all values are listed in the control.  If you wish to hide a specific value from the end user, use `EditorBrowsableAttribute` on the value with `EditorBrowsableState.Never`.

In this example, the `EditorBrowsableAttribute` hides the `MyFirstValue` value from the user interface:

```csharp
public enum SampleEnum {

	[EditorBrowsable(EditorBrowsableState.Never)]
	MyFirstValue = 1

	...
}
```
