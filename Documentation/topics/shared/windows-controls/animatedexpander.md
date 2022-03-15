---
title: "AnimatedExpander"
page-title: "AnimatedExpander - Shared Library Controls"
order: 4
---
# AnimatedExpander

The [AnimatedExpander](xref:@ActiproUIRoot.Controls.AnimatedExpander) is a regular `Expander` that provides optional animated expansion functionality.  The animation consists of crossfade and slide behavior.

The [AnimatedExpander](xref:@ActiproUIRoot.Controls.AnimatedExpander) class has these important members:

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

[CanMeasureCollapsedContent](xref:@ActiproUIRoot.Controls.AnimatedExpander.CanMeasureCollapsedContent) Property

</td>
<td>

Gets or sets whether the expander will use the width measurement of collapsed content when calculating its own width.  The default value is `true`.

When `true`, a consistent width will be maintained when collapsed.  When `false`, the expander will load faster (if collapsed) since less measuring is taking place.

</td>
</tr>

<tr>
<td>

[CollapseDuration](xref:@ActiproUIRoot.Controls.AnimatedExpander.CollapseDuration) Property

</td>
<td>

Gets or sets the `Duration` of the collapse animation.  The default value is `150` milliseconds.

</td>
</tr>

<tr>
<td>

`ExpandDirection` Property

</td>
<td>

Gets or sets the `ExpandDirection` of the expand animation.  The default value is `ExpandDirection.Down`.

</td>
</tr>

<tr>
<td>

[ExpandDuration](xref:@ActiproUIRoot.Controls.AnimatedExpander.ExpandDuration) Property

</td>
<td>

Gets or sets the `Duration` of the expand animation.  The default value is `150` milliseconds.

</td>
</tr>

<tr>
<td>

[HeaderContextMenu](xref:@ActiproUIRoot.Controls.AnimatedExpander.HeaderContextMenu) Property

</td>
<td>Gets or sets the context menu used for the header.</td>
</tr>

<tr>
<td>

[HeaderCornerRadius](xref:@ActiproUIRoot.Controls.AnimatedExpander.HeaderCornerRadius) Property

</td>
<td>Gets or sets the corner radius used by the header.</td>
</tr>

<tr>
<td>

[HeaderPadding](xref:@ActiproUIRoot.Controls.AnimatedExpander.HeaderPadding) Property

</td>
<td>

Gets or sets the `Thickness` of the padding around header content.

</td>
</tr>

<tr>
<td>

[IsAutoCollapseOnBlurEnabled](xref:@ActiproUIRoot.Controls.AnimatedExpander.IsAutoCollapseOnBlurEnabled) Property

</td>
<td>

Gets or sets whether the expander will auto-collapse when focus leaves it.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[IsAutoExpandOnFocusEnabled](xref:@ActiproUIRoot.Controls.AnimatedExpander.IsAutoExpandOnFocusEnabled) Property

</td>
<td>

Gets or sets whether the expander will auto-expand when focus enters it.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[IsAutoFocusOnExpandEnabled](xref:@ActiproUIRoot.Controls.AnimatedExpander.IsAutoFocusOnExpandEnabled) Property

</td>
<td>

Gets or sets whether focus will attempt to be set in the content of the expander following the completion of the expansion animation.  The default value is `false`.

</td>
</tr>

</tbody>
</table>

The [AnimatedExpanderDecorator](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator) decorator is used in the template for the [AnimatedExpander](xref:@ActiproUIRoot.Controls.AnimatedExpander) to provide the actual animation behavior.
