---
title: "PanelBase"
page-title: "PanelBase - Views Panels"
order: 2
---
# PanelBase

This is the base class for all the panels provided by the Views product. It allows custom panels to be built that animate the arrangement of the child elements, and can be used in the [SwitchPanel](xref:@ActiproUIRoot.Controls.Views.SwitchPanel).

## Overview

The [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase) provides base functionality of all animated panels. It provides attached properties for storing state information, as well as the current animation rectangle.

When building a custom panel that derives from the native `Panel`, the `ArrangeOverride` and `MeasureOverride` methods are overridden.  Similarly, when building a custom panel that derives from the `PanelBase`, the `ArrangeElements` and `MeasureElements` methods should be overridden. Additionally, the panel should not reference the `Children` collection, but instead arrange the list of elements specified. This allows the panel's layout logic to be leveraged by the `SwitchPanel`.

See the [Building a Custom Panel](building-a-custom-panel.md) topic for more information.

## Arrange State

The [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase) uses the [ArrangeStateProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeStateProperty) attached property to storage state information about its child elements.  The state is represented by an instance of [ArrangeState](xref:@ActiproUIRoot.Controls.Views.ArrangeState), and captures the current status, current arrange rectangle, previous arrange rectangle, and an optional "from" point.

The [ArrangeState](xref:@ActiproUIRoot.Controls.Views.ArrangeState).[ArrangeStatus](xref:@ActiproUIRoot.Controls.Views.ArrangeState.ArrangeStatus) indicates the current status of the element. The valid statuses are: `Entering`, `Leaving`, `ArrangeUpdating`, and `LayoutUpdating`.

| Member | Description |
|-----|-----|
| `Entering` Field | Indicates that the element was just added to the panel and is "entering" the view. |
| `Leaving` Field | Indicates that the element was just removed from the panel and is "leaving" the view. |
| `ArrangeUpdating` Field | Indicates that the element is changing position or size in the panel due to the standard arrange phase. |
| `LayoutUpdating` Field | Indicates that the element is changing position or size because the layout logic of the panel changed (e.g., the orientation is changed). |

The current and previous arrange rectangels are captured in the [ArrangeRect](xref:@ActiproUIRoot.Controls.Views.ArrangeState.ArrangeRect) and [PreviousArrangeRect](xref:@ActiproUIRoot.Controls.Views.ArrangeState.PreviousArrangeRect) properties, respectively.

Finally, the [ArrangeFromPoint](xref:@ActiproUIRoot.Controls.Views.ArrangeState.ArrangeFromPoint) can be used to optionally change the location that an element starts it's "move" animation from. This is usually leveraged by custom panels to customize the movement of elements.

## Disabling Animations Per Element

Using the [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase).[IsAnimatedProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.IsAnimatedProperty) attached property, animations can be selectively disabled for individual elements.  This property defaults to `true` and is supported by all panels that derive from `PanelBase`.

## Important Members

The following [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase) members are key to its use:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[ArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeAnimation) Property

</td>
<td>

Gets or sets the [IArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.IArrangeAnimation) that controls the animation of elements when arranging.

</td>
</tr>

<tr>
<td>

[ArrangeElement](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeElement*) Method

</td>
<td>

Arranges the specified element using the current arrange state.

This can be used to force an element to arrange using it's current arrange location, but does not typically need to be called directly.

</td>
</tr>

<tr>
<td>

[ArrangeElements](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeElements*) Method

</td>
<td>

Positions the specified elements and determines a size for a `FrameworkElement`-derived class.

This method must be implemented by derivations of `PanelBase`, and determines the arrangement logic of the panel.

</td>
</tr>

<tr>
<td>

[ArrangeHeightAnimatedProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeHeightAnimatedProperty) Attached Property

</td>
<td>

Gets or sets the height used when arranging the associated element.

> [!NOTE]
> Typically, the property should never be set directly, as it is animated when the state of the element is changed.

</td>
</tr>

<tr>
<td>

[ArrangeStateProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeStateProperty) Attached Property

</td>
<td>Gets or sets the current state of the associated element, which can include an updated arrange rectangle.</td>
</tr>

<tr>
<td>

[ArrangeWidthAnimatedProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeWidthAnimatedProperty) Attached Property

</td>
<td>

Gets or sets the width used when arranging the associated element.

> [!NOTE]
> Typically, the property should never be set directly, as it is animated when the state of the element is changed.

</td>
</tr>

<tr>
<td>

[ArrangeXAnimatedProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeXAnimatedProperty) Attached Property

</td>
<td>

Gets or sets the position along the X-axis of the parent panel used when arranging the associated element.

> [!NOTE]
> Typically, the property should never be set directly, as it is animated when the state of the element is changed.

</td>
</tr>

<tr>
<td>

[ArrangeYAnimatedProperty](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeYAnimatedProperty) Attached Property

</td>
<td>

Gets or sets the position along the Y-axis of the parent panel used when arranging the associated element.

> [!NOTE]
> Typically, the property should never be set directly, as it is animated when the state of the element is changed.

</td>
</tr>

<tr>
<td>

[IsLayoutUpdatePending](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.IsLayoutUpdatePending) Property

</td>
<td>

Gets a value indicating whether a change to the layout logic has changed and the elements have yet to be arranged.

This is typically used to distingush between the `ArrangeStatus.ArrangeUpdating` and `ArrangeStatus.LayoutUpdating` statues, where the latter is due to a change in the logical layout (i.e., changing the orientation).

</td>
</tr>

<tr>
<td>

[MeasureElements](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.MeasureElements*) Method

</td>
<td>

Measures the size in layout required for child elements and determines a size for the `FrameworkElement`-derived class.

This method must be implemented by derivations of `PanelBase`, and determines the measurement logic of the panel.

</td>
</tr>

</tbody>
</table>
