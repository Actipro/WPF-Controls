---
title: "Resizing and Variants"
page-title: "Resizing and Variants - Ribbon Features"
order: 15
---
# Resizing and Variants

Ribbon is designed to dynamically adjust the layout of controls based on the available width using techniques optimized for either the Simplified or Classic layout modes.

Several controls support multiple variant sizes, where each variant alters the appearance of the control. As the variant size moves from larger to smaller, the change in appearance generally involves a reduction in the amount of information displayed in the control's content to save on overall space usage.  For example, a button may display with both an icon and a label when space is available but only display an icon when space is limited.

Each ribbon layout mode uses a different resizing strategy.  See the [Layout Modes and Density](layout-and-density.md) topic for more details on working with layout modes.

## Simplified Layout Strategy

The primary focus of the Simplified layout mode is overflowing controls to a popup menu.

![Screenshot](../images/ribbon-tab-overflow-simplified.png)

*The tab overflow menu displayed*

When there is not enough room to display all the controls, the **Tab Overflow** button will be displayed, and controls are moved one-by-one to a popup menu displayed when clicking the button.  A header will be displayed in the popup menu that corresponds to the [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).[Label](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup.Label) of the group where the control is defined, so overflowed controls maintain the same level of organization they had on the ribbon.

By default, controls are overflowed starting from the far side of the ribbon.  See the "Explicit Variant Sizing" section below for more details on controlling the order in which items reduce their variant size.

### Group Overflow

An individual group on the ribbon can be configured to define its own **Group Overflow** button instead of using the default **Tab Overflow** button. To enable this functionality, set [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).[ChildOverflowTarget](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup.ChildOverflowTarget) = [RibbonGroupChildOverflowTarget](xref:@ActiproUIRoot.Controls.Bars.RibbonGroupChildOverflowTarget).[Group](xref:@ActiproUIRoot.Controls.Bars.RibbonGroupChildOverflowTarget.Group) as shown in the following example:

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonGroup Key="Clipboard" ... ChildOverflowTarget="Group" />
```

When enabled and controls in a group need to overflow, a **Group Overflow** button will appear as the last item in the group. Overflowed controls will appear in the popup menu just like they appear in the **Tab Overflow** menu except there is no need to display the group's label as a header.

![Screenshot](../images/ribbon-group-overflow-simplified.png)

*The group overflow menu displayed*

> [!TIP]
> Use [Group](xref:@ActiproUIRoot.Controls.Bars.RibbonGroupChildOverflowTarget.Group) overflow for important groups where overflowed controls can be kept close to the other controls in the same group.

### Variants

As previously mentioned, several controls support multiple variant sizes, where each variant alters the appearance of the control.  As the variant size moves from larger to smaller, the change in appearance generally involves a reduction in the amount of information displayed in the control's content to save on overall space usage.

The following variant sizes are supported by the Simplified layout mode in order from largest to smallest:

| Variant Size | Description |
| --- | --- |
| [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) | Most controls display as an icon with a label. |
| [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) (Default) | Most controls display only as an icon. |
| [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed) | Controls are moved to the **Overflow** menu. |

A ribbon will always try to display a control at the largest variant size for the available space.

> [!NOTE]
> The [VariantSize](xref:@ActiproUIRoot.Controls.Bars.VariantSize).[Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) value has no effect for a ribbon using the Simplified layout mode.

### Maximum Variant Size

Most controls define a `MaxSimplifiedVariantSize` property (e.g., [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[MaxSimplifiedVariantSize](xref:@ActiproUIRoot.Controls.Bars.BarButton.MaxSimplifiedVariantSize)) that determines the maximum variant size of a control when it is displayed in a ribbon with the Simplified layout mode.

Since the default variant size is [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small), most controls will appear without a label.  By setting `MaxSimplifiedVariantSize` = [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium), most controls will display with a label when space is available.

The same `MaxSimplifiedVariantSize` property can also be used to prevent a control from appearing on the ribbon at all. Setting `MaxSimplifiedVariantSize` = [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed) will cause the control to always appear on the corresponding **Overflow** menu, even if space is available.

The following example shows two buttons using the `MaxSimplifiedVariantSize` property:

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...

<!-- Medium variant size to show label when space available -->
<bars:BarSplitButton Key="Open" ... MaxSimplifiedVariantSize="Medium" />

<!-- Collapsed variant size to always overflow -->
<bars:BarButton Key="IncreaseFontSize" ... MaxSimplifiedVariantSize="Collapsed" />
```

## Classic Layout Strategy

In the Classic layout mode, controls are generally available in sizes from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) and will render using the largest size possible for the available width. As the ribbon size changes, controls dynamically adapt to the available space and eventually collapse to a popup menu for each group when space is too limited.

By default, controls are collapsed starting from the far side of the ribbon.  See the "Explicit Variant Sizing" section below for more details on controlling the order in which items reduce their variant size.

### Control Groups

A [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) is used to group multiple controls and resize them together. By grouping them, multiple controls can be arranged together to produce an ideal layout for a variant size.

For example, a `BarButton` in its [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) variant size places a large icon over a label. When shifting to the [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) variant size, it uses a smaller icon with the label to the right. In most cases, this style is actually wider than the [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) variant, but the height of each button is greatly reduced. By grouping multiple buttons together in a [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup), two or three shorter buttons can be stacked vertically to reduce the overall width of all the buttons compared to displaying them side-by-side horizontally.

The following defines the layout of a control group at different variant sizes:

| Variant Size | Description |
| --- | --- |
| [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) | All controls displayed in their respective `Large` variant style and arranged horizontally. |
| [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) | All controls displayed in their respective `Medium` variant style and arranged vertically with up to three controls per vertical stack. |
| [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) | All controls displayed in their respective `Small` variant style and arranged vertically with up to three controls per vertical stack. |

By default, a control group will attempt all variant sizes from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small), but the [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup).[ItemVariantBehavior](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup.ItemVariantBehavior) property can used to limit the variant sizes which are supported by the group. The following values are available:

| Item Variant Behavior | Description |
| --- | --- |
| [Default](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.Default) | The default behavior allows a group to progress through variant sizes from `Large` to `Medium` to `Small`. |
| [AlwaysLarge](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.AlwaysLarge) | The group will always display as the `Large` variant size. |
| [AlwaysMedium](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.AlwaysMedium) | The group will always display as the `Medium` variant size. |
| [AlwaysSmall](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.AlwaysSmall) | The group will always display as the `Small` variant size. |
| [NeverLarge](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.NeverLarge) | The group supports variant sizes `Medium` to `Small`, skipping the `Large` size.
| [NeverMedium](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.NeverMedium) | The group supports variant sizes `Large` to `Small`, skipping the `Medium` size.
| [NeverSmall](xref:@ActiproUIRoot.Controls.Bars.ItemVariantBehavior.NeverSmall) | The group supports variant sizes `Large` to `Medium`, skipping the `Small` size.

The following example defines three buttons that will always appear in a single vertical stack because they are defined in a control group that does not support the [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) variant size. Labels will be visible when the control group is `Medium`, and only icons will display when the control group is `Small`.

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonGroup ... >
	<bars:RibbonControlGroup ItemVariantBehavior="NeverLarge">
		<bars:BarButton Key="Cut" Command="Cut" SmallImageSource="/Images/Cut16.png" />
		<bars:BarButton Key="Copy" Command="Copy" SmallImageSource="/Images/Copy16.png" />
		<bars:BarButton Key="Paste" Command="Paste" SmallImageSource="/Images/Paste16.png" />
	</bars:RibbonControlGroup>
	...
</bars:RibbonGroup>
...
```

### Multi-Row Layouts

The highest density of commands can be achieved using multi-row layouts where all the controls in a ribbon group are arranged in either two or three rows using the [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) variant of each control. To enable multi-row layouts, set [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).[CanUseMultiRowLayout](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup.CanUseMultiRowLayout) = `true`.

When using multi-row layouts, a [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) is used to group related controls.  Control groups will be arranged into two rows at the largest size and adjust to three rows when width is limited.

![Screenshot](../images/ribbon-group-two-rows.png)
![Screenshot](../images/ribbon-group-three-rows.png)

*The same multi-row ribbon group using two rows (left) compared to three rows (right)*

When arranged in two rows, control groups are sorted in the order they are defined.  In three-row mode, the default behavior will sort groups so each row is as narrow as possible.  When desired, the [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).[ThreeRowItemSortOrder](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup.ThreeRowItemSortOrder) property is used to set an explicit sort order where the zero-based index of each control group is listed in the preferred sort order, separated by spaces (e.g., `0 1 3 4 2`).  The following example demonstrates defining a three-row sort order.


```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonGroup ... CanUseMultiRowLayout="True" ThreeRowItemSortOrder="0 1 3 4 2">

	<bars:RibbonControlGroup Key="Index0"> ... </bars:RibbonControlGroup>
	<bars:RibbonControlGroup Key="Index1"> ... </bars:RibbonControlGroup>

	<!-- In 3-row layout, this group will sort at the end based on ThreeRowItemSortOrder -->
	<bars:RibbonControlGroup Key="Index2"> ... </bars:RibbonControlGroup>

	<bars:RibbonControlGroup Key="Index3"> ... </bars:RibbonControlGroup>
	<bars:RibbonControlGroup Key="Index4"> ... </bars:RibbonControlGroup>

	...
</bars:RibbonGroup>
...
```

### Separators

Separators can be automatically rendered between neighboring [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) instances based on the current layout of the containing [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup); i.e., stacking (default) or multi-row.

#### Stacking Layout Separators

By default, separators are not displayed between neighboring control groups in a stacking layout. Some configurations, however, might benefit from displaying separators. For more exact control, the [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup).[SeparatorMode](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup.SeparatorMode) property can be set to any of the following values to control when a separator is displayed:

| Separator Mode | Description |
| --- | --- |
| [Default](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Default) or [Never](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Never) | Never show surrounding separators. |
| [Always](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Always) | Always show surrounding separators when there are neighbors that allow them. |
| [StackingNeighborSameVariantSize](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.StackingNeighborSameVariantSize) | Only show surrounding separators when a neighboring control group has the same variant size. |
| [StackingNeighborDifferentVariantSize](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.StackingNeighborDifferentVariantSize) | Only show surrounding separators when a neighboring control group has a different variant size. |

> [!NOTE]
> There are other values defined for the [RibbonControlGroupSeparatorMode](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode) enumeration that do not apply to stacking layouts. If used in a stacking layout, the value will be treated the same as [Default](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Default).

The following example defines four buttons, where the last three are part of a control group. The control group is configured to only show a separator when the group of three buttons is the same variant size as the first button (which, since it is not in a control group, will always be [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large)). A separator will not be displayed if the control group is [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) or [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small).

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonGroup ... >
	<bars:BarButton Key="LargeButton" ... />
	<bars:RibbonControlGroup SeparatorMode="StackingNeighborSameVariantSize">
		<bars:BarButton Key="VariantButton1" ... />
		<bars:BarButton Key="VariantButton2" ... />
		<bars:BarButton Key="VariantButton3" ... />
	</bars:RibbonControlGroup>
	...
</bars:RibbonGroup>
```

#### Multi-Row Layout Separators

By default when a [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) is configured for multi-row layout, separators are inserted between adjacent control groups on the same row to help visually differentiate the groups. For more exact control, the [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup).[SeparatorMode](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup.SeparatorMode) property can be set to any of the following values to control when a separator is displayed:

| Separator Mode | Description |
| --- | --- |
| [Default](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Default) or [Always](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Always) | Always show surrounding separators when there are neighbors that allow them. |
| [Never](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Never) | Never show surrounding separators, which is ideal when the control group contains controls with visible borders such as [combobox](../controls/combobox.md) controls. |
| [MultiRowTwoRowOnly](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.MultiRowTwoRowOnly) | Only show surrounding separators when in a [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) that currently has a two-row layout. |
| [MultiRowThreeRowOnly](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.MultiRowThreeRowOnly) | Only show surrounding separators when in a [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) that currently has a three-row layout. |

> [!NOTE]
> There are other values defined for the [RibbonControlGroupSeparatorMode](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode) enumeration that do not apply to multi-row layouts. If used in a multi-row layout, the value will be treated the same as [Default](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroupSeparatorMode.Default).

## Explicit Variant Sizing

If you want more control over variant sizing, you can explicitly define the order in which items collapse for both Simplified and Classic layout modes. Multiple items can even be configured to change their variant size at the same time.

> [!NOTE]
> It is good application design to define a prioritized set of variants so that the least used groups/controls shrink and collapse before the more-often used groups/controls do.  For instance, `Clipboard` groups/controls are most often the last item to collapse on a tab since they are highly used.

### Understanding Variant Collections

When all items cannot be displayed on the ribbon at the same time, the [VariantCollection](xref:@ActiproUIRoot.Controls.Bars.VariantCollection) (when defined) is used to dictate the order in which items will adjust to a smaller variant.  The collection is processed starting with the first defined item and includes both [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) and/or [VariantSet](xref:@ActiproUIRoot.Controls.Bars.VariantSet) entries.

A [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) defines a [TargetKey](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.TargetKey) property that identifies the `Key` of the item being affected  and a [Size](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.Size) property that indicates the [VariantSize](xref:@ActiproUIRoot.Controls.Bars.VariantSize) to be applied to the item.  When reducing the size of the ribbon, a [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) defined directly in the [VariantCollection](xref:@ActiproUIRoot.Controls.Bars.VariantCollection) will be applied to a single item at a time before moving to the next entry in the collection.

A [VariantSet](xref:@ActiproUIRoot.Controls.Bars.VariantSet) is simply a collection of [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) instances. When reducing the size of the ribbon, a [VariantSet](xref:@ActiproUIRoot.Controls.Bars.VariantSet) will apply each [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) in the set at the same time before moving on to the next entry in the collection. This allows the size of multiple items to be affected in a single step instead of one-by-one.

### Applying to Simplified Layout Mode

In the Simplified layout mode, a [VariantCollection](xref:@ActiproUIRoot.Controls.Bars.VariantCollection) is assigned to the [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).[ControlVariants](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem.ControlVariants) property. The [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant).[TargetKey](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.TargetKey) should identify the `Key` of a control whose variant size will be applied (e.g., [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[Key](xref:@ActiproUIRoot.Controls.Bars.BarButton.Key)).

> [!WARNING]
> Be sure to populate the [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).[ControlVariants](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem.ControlVariants) property and not the [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).[GroupVariants](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem.GroupVariants) property used by the Classic layout mode.

Normally, items are overflowed one-by-one starting at the far side of the ribbon, but the following sample explicitly defines the order in which items are initially overflowed:

1. Both the `Undo` and `Redo` buttons should collapse first and at the same time.
2. Two of the buttons from the `Font` group (`Subscript` and `Superscript`) will collapse at the same time, leaving `IncreaseFontSize` and `DecreaseFontSize` in that group.
3. All buttons in the `Clipboard` group will collapse at the same time (`FormatPainter`, `Cut`, `Copy`, and `Paste`). Note that the individual controls must be identified by the [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant).[TargetKey](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.TargetKey), not the ribbon group.
4. The `New` button will change size from [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small). Normally items in the Simplified ribbon start at the [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) size, but that particular control explicitly set its max size to [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium).
5. The remaining buttons from the `Font` group collapse at the same time (`IncreaseFontSize` and `DecreaseFontSize`).
6. Once all explicitly defined [VariantSet](xref:@ActiproUIRoot.Controls.Bars.VariantSet) and [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) items are processed, the remaining items are implicitly overflowed one-by-one starting at the far side of the ribbon in the following order: `Italic`, `Underline`, `Bold`, `Save`, `Open`, and finally `New`.

> [!NOTE]
> In the Simplified layout mode, [VariantSize](xref:@ActiproUIRoot.Controls.Bars.VariantSize).[Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed) indicates a control should appear on the appropriate **Overflow Menu**.

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonTabItem ... >

	<!-- Define the ControlVariants -->
	<bars:RibbonTabItem.ControlVariants>
		<bars:VariantCollection>
			<!-- All controls in the Undo group overflow at the same time -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="Undo" Size="Collapsed" />
				<bars:SizeVariant TargetKey="Redo" Size="Collapsed" />
			</bars:VariantSet>

			<!-- Subscript and Superscript in the Font group overflow at the same time, other Font controls remain -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="Subscript" Size="Collapsed" />
				<bars:SizeVariant TargetKey="Superscript" Size="Collapsed" />
			</bars:VariantSet>

			<!-- All controls in the Clipboard group overflow at the same time -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="FormatPainter" Size="Collapsed" />
				<bars:SizeVariant TargetKey="Cut" Size="Collapsed" />
				<bars:SizeVariant TargetKey="Copy" Size="Collapsed" />
				<bars:SizeVariant TargetKey="Paste" Size="Collapsed" />
			</bars:VariantSet>

			<!-- The New button moves from Medium to Small -->
			<bars:SizeVariant TargetKey="New" Size="Small" />

			<!-- All remaining controls in the Font group overflow at the same time -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="IncreaseFontSize" Size="Collapsed" />
				<bars:SizeVariant TargetKey="DecreaseFontSize" Size="Collapsed" />
			</bars:VariantSet>

			<!-- All remining controls (those in File and Format groups) will implicitly overflow one-by-one from far side of the ribbon -->

		</bars:VariantCollection>
	</bars:RibbonTabItem.ControlVariants>

	<!-- Define Groups/Controls -->
	<bars:RibbonGroup Label="Undo">
		<bars:BarButton Key="Undo" ... />
		<bars:BarButton Key="Redo" ... />
	</bars:RibbonGroup>
	<bars:RibbonGroup Label="File">
		<bars:BarButton Key="New" ... MaxSimplifiedVariantSize="Medium" />
		<bars:BarButton Key="Open" ... />
		<bars:BarButton Key="Save" ... />
	</bars:RibbonGroup>
	<bars:RibbonGroup Label="Clipboard" ChildOverflowTarget="Group">
		<bars:RibbonControlGroup>
			<bars:BarButton Key="Cut" ... />
			<bars:BarButton Key="Copy" ... />
			<bars:BarButton Key="Paste" ... />
			<bars:BarButton Key="FormatPainter" ... />
		</bars:RibbonControlGroup>
	</bars:RibbonGroup>
	<bars:RibbonGroup Label="Format">
		<bars:BarButton Key="Bold" ... />
		<bars:BarButton Key="Underline" ... />
		<bars:BarButton Key="Italic" ... />
	</bars:RibbonGroup>
	<bars:RibbonGroup Label="Font">
		<bars:BarButton Key="IncreaseFontSize" ... />
		<bars:BarButton Key="DecreaseFontSize" ... />
		<bars:BarButton Key="Subscript" ... />
		<bars:BarButton Key="Superscript" ... />
	</bars:RibbonGroup>

</bars:RibbonTabItem>
...
```

### Applying to Classic Layout Mode

In the Classic layout mode, a [VariantCollection](xref:@ActiproUIRoot.Controls.Bars.VariantCollection) is assigned to the [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).[GroupVariants](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem.GroupVariants) property.  The [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant).[TargetKey](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.TargetKey) should identify the [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).[Key](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup.Key) of the group whose variant size will be applied.

> [!WARNING]
> Be sure to populate the [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).[GroupVariants](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem.GroupVariants) property and not the [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).[ControlVariants](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem.ControlVariants) property used by the Simplified layout mode.

Normally, group sizes are reduced from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) before collapsing and the variants are applied to groups one-by-one from the far side of the ribbon.  The following sample defines a ribbon tab with explicit sizing applied to multiple groups in the tab for complete control over the order in which variant sizes are reduced:

1. `GroupA1` and `GroupA2` both move from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) at the same time.
2. `GroupB1` moves immediately from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed).
3. `GroupC` moves from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small), skipping [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium).
4. `GroupA1` and `GroupA2` both move from [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) at the same time.
5. `GroupA1` and `GroupA2` both move from [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) to [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed) at the same time.
6. `GroupC` moves from [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) to [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed).
7. Once all explicitly defined [VariantSet](xref:@ActiproUIRoot.Controls.Bars.VariantSet) and [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant) items are processed, the remaining groups are implicitly processed one-by-one starting at the far side of the ribbon in the following order:
   a. `GroupE` moves from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium).
   b. `GroupD` moves from [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium).
   a. `GroupE` moves from [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small).
   b. `GroupD` moves from [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small).
   a. `GroupE` moves from [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) to [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed).
   b. `GroupD` moves from [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) to [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed).

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonTabItem ... >

	<!-- Define the Group Variants -->
	<bars:RibbonTabItem.GroupVariants>
		<bars:VariantCollection>

			<!-- Both GroupA1 and GroupA2 move to the Medium size at the same time -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="GroupA1" Size="Medium" />
				<bars:SizeVariant TargetKey="GroupA2" Size="Medium" />
			</bars:VariantSet>

			<!-- GroupB collapses -->
			<bars:SizeVariant TargetKey="GroupB" Size="Collapsed" />

			<!-- GroupC moves to the Small size -->
			<bars:SizeVariant TargetKey="GroupC" Size="Small" />

			<!-- Both GroupA1 and GroupA2 move to the Small size at the same time -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="GroupA1" Size="Small" />
				<bars:SizeVariant TargetKey="GroupA2" Size="Small" />
			</bars:VariantSet>

			<!-- Both GroupA1 and GroupA2 collapse at the same time -->
			<bars:VariantSet>
				<bars:SizeVariant TargetKey="GroupA1" Size="Collapsed" />
				<bars:SizeVariant TargetKey="GroupA2" Size="Collapsed" />
			</bars:VariantSet>

			<!-- GroupC collapses -->
			<bars:SizeVariant TargetKey="GroupC" Size="Collapsed" />

			<!-- All remaining groups (GroupD and GroupE) will implicitly collapse -->

		</bars:VariantCollection>
	</bars:RibbonTabItem.GroupVariants>

	<!-- Define the Groups -->
	<bars:RibbonGroup Key="GroupA1" ... > ... </bars:RibbonGroup>
	<bars:RibbonGroup Key="GroupA2" ... > ... </bars:RibbonGroup>
	<bars:RibbonGroup Key="GroupB" ... > ... </bars:RibbonGroup>
	<bars:RibbonGroup Key="GroupC" ... > ... </bars:RibbonGroup>
	<bars:RibbonGroup Key="GroupD" ... > ... </bars:RibbonGroup>
	<bars:RibbonGroup Key="GroupE" ... > ... </bars:RibbonGroup>

</bars:RibbonTabItem>
...
```

### Implicit Sizing Fallback

The [VariantCollection](xref:@ActiproUIRoot.Controls.Bars.VariantCollection) does not have to define sizing for all items. Any item whose key *is not* explicitly defined by at least one [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant).[TargetKey](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.TargetKey) entry will implicitly reduce in size using the default logic (e.g., [Large](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Large) to [Medium](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Medium) to [Small](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Small) before collapsing/overflowing).

Any item that *is* explicitly defined by at least one [SizeVariant](xref:@ActiproUIRoot.Controls.Bars.SizeVariant).[TargetKey](xref:@ActiproUIRoot.Controls.Bars.SizeVariant.TargetKey) entry will still implicitly collapse as the next stage (skipping all other variant sizes) even if a [Collapsed](xref:@ActiproUIRoot.Controls.Bars.VariantSize.Collapsed) variant size was not explicitly set.  In the Classic layout mode only, this implicit auto-collapse behavior can be preventing by setting [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).[CanAutoCollapse](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup.CanAutoCollapse) = `false`.

## RibbonGallery

The [RibbonGallery](xref:@ActiproUIRoot.Controls.Bars.RibbonGallery) control will, by default, collapse to a popup button when the parent group's [VariantSize](xref:@ActiproUIRoot.Controls.Bars.VariantSize) is `Small`.  It will expand to show a range of items depending on space when the parent group's [VariantSize](xref:@ActiproUIRoot.Controls.Bars.VariantSize) is `Medium`.  Finally, when the parent group's [VariantSize](xref:@ActiproUIRoot.Controls.Bars.VariantSize) is `Large`, it will expand to fill all remaining space in the tab.  All this behavior is completely configurable.

See the [Gallery](../controls/gallery.md) topic for more details on how to define variant behavior.

> [!TIP]
> See the "Variant Sizing" Bars Ribbon QuickStart of the Sample Browser application for a full demonstration of working with variant sizing.