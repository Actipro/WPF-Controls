---
title: "Building a Custom Panel"
page-title: "Building a Custom Panel - Views Panels"
order: 20
---
# Building a Custom Panel

This topic describes step-by-step how to create a custom panel that supports animation and [SwitchPanel](xref:@ActiproUIRoot.Controls.Views.SwitchPanel).  Specifically, the panel created in this tutorial will randomly arrange its child elements.

> [!NOTE]
> The complete source code for this tutorial can be found in the associated Sample Browser.

## Step 1: Class Declaration

The panel will be named `RandomPanel` and needs to derive from [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase).

The `using` statements listed below have been included as they will all be needed to complete this tutorial.  But Visual Studio can be used to automatically add the appropriate `using` statements for unknown types.  Since this panel can be found in the associated samples, the same namespace is used here.

```csharp
using System;
using System.Collections.Generic;
using System.Windows;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Controls.Views.Primitives;

namespace ActiproSoftware.Windows.ProductSamples.ViewsSamples.QuickStart.CustomPanelRandom {

	/// <summary>
	/// Represents a custom panel that randomly arranges its child elements.
	/// </summary>
	public class RandomPanel : PanelBase {

		// SEE BELOW

	}
}
```

## Step 2: Random Field

An instance of the `Random` class will be needed to randomly generate the locations of the elements.

Add the following inside the `Random` class:

```csharp
private Random random = new Random(Environment.TickCount);
```

## Step 3: Override MeasureElements

The [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase).[MeasureElements](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.MeasureElements*) method must be overridden to measure the associated elements.

Typically, a `Panel` will measure/arrange the elements found in the `Children` collection.  In order to support [SwitchPanel](xref:@ActiproUIRoot.Controls.Views.SwitchPanel), the panel must measure/arrange the list of elements provided as a method parameter.  When the `RandomPanel` is used by a `SwitchPanel`, the list includes the elements contained in the `SwitchPanel`.  When the `RandomPanel` is used independently, the list simply references the `Children` collection.

Add the following inside the `Random` class:

```csharp
/// <summary>
/// Measures the size in layout required for the specified elements and determines a size for the <see cref="FrameworkElement"/>-derived class.
/// </summary>
/// <param name="elements">The elements to be measured.</param>
/// <param name="availableSize">The available size that this element can give to the specified elements. Infinity can be specified as a value to indicate that
/// the element will size to whatever content is available.</param>
/// <returns>
/// The size that this element determines it needs during layout, based on its calculations of the specified elements.
/// </returns>
public override Size MeasureElements(IList<UIElement> elements, Size availableSize) {

	// SEE BELOW

}
```

## Step 4: Measurement Logic

The measurement logic of the `RandomPanel` simply passes the given available size to the elements.  It returns a desired size that will fit the largest element, both horizontally and vertically.

Add the following inside the `MeasureElements` method:

```csharp
// Measure each element, and return the largest width and height needed
Size desiredSize = new Size();
foreach (UIElement element in elements) {
	if (null != element) {
		element.Measure(availableSize);

		desiredSize.Width = Math.Max(element.DesiredSize.Width, desiredSize.Width);
		desiredSize.Height = Math.Max(element.DesiredSize.Height, desiredSize.Height);
	}
}
return desiredSize;
```

## Step 5: Override ArrangeElements

The [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase).[ArrangeElements](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.ArrangeElements*) method must be overridden to arrange the associated elements.

Just like the measure phase, the specified list of elements should be arranged, not the elements in the `Children` collection.  The `RandomPanel` will always take as much space as it's given, since it returns `finalSize` unaltered.

Add the following inside the `Random` class:

```csharp
/// <summary>
/// Positions the specified elements and determines a size for a <see cref="FrameworkElement"/>-derived class.
/// </summary>
/// <param name="elements">The elements to be arranged.</param>
/// <param name="finalSize">
/// The final area within the parent that this element should use to arrange itself and the specified elements.
/// </param>
/// <returns>The actual size used.</returns>
public override Size ArrangeElements(IList<UIElement> elements, Size finalSize) {

	// SEE BELOW

	return finalSize;
}
```

## Step 6: Arrangement Logic - Check IsLayoutUpdatePending

The [PanelBase](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase).[IsLayoutUpdatePending](xref:@ActiproUIRoot.Controls.Views.Primitives.PanelBase.IsLayoutUpdatePending) property is used to flag when the layout logic of the `RandomPanel`, or the associated `SwitchPanel`, has changed.  The animations used for a change in a layout logic change can be configured differently than standard arrange rectangle changes.  These two distinct states are represented by [ArrangeStatus](xref:@ActiproUIRoot.Controls.Views.ArrangeStatus).`LayoutUpdating` and `ArrangeStatus`.`ArrangeUpdating`, respectively.

Derived panels are responsible for checking and using the `IsLayoutUpdatePending` flag, so both statuses are correctly applied.

> [!IMPORTANT]
> If the flag is not properly used, then the "layout updating" animations and settings will not be used.  Instead, the "arrange updating" animations and settings will always be used.

Add the following inside the `ArrangeElements` method above the "SEE BELOW" comment marker:

```csharp
// Cache and reset layout pending flag
bool isLayoutUpdatePending = this.IsLayoutUpdatePending;
this.IsLayoutUpdatePending = false;
```

## Step 7: Arrangement Logic - Determine Location

Using the `random` field, a unique x and y coordinate will be calculated for each element.  Determining an x and y coordinate is similar to how the native WPF `Canvas` works but can actually be applied to any panel.  In reality, the final rectangle of the element is being determined.

In addition to randomly placing the elements, this logic will ensure that the element is fully visible inside the panel, when possible.

Add the following inside the `ArrangeElements` method in place of the "SEE BELOW" comment marker:

```csharp
// Iterate over the elements and arrange
foreach (UIElement element in elements) {
	if (element != null) {
		Size desiredSize = element.DesiredSize;

		// Calculate a random x/y position that keeps the element in the view
		double x = Math.Max(random.NextDouble() * (finalSize.Width - desiredSize.Width), 0);
		double y = Math.Max(random.NextDouble() * (finalSize.Height - desiredSize.Height), 0);

		// SEE BELOW
	}
}
```

## Step 8: Arrangement Logic - Update ArrangeState

Instead of directly arranging elements, derivations of `PanelBase` update the "arrange state".  This information is captured in an instance of [ArrangeState](xref:@ActiproUIRoot.Controls.Views.ArrangeState) and is set on an element via an attached property.

The `PanelBase` will then watch for state changes and animate an element based on its state (i.e., entering the view, leaving the view, updating location, etc.) and the current animation settings.

This logic will also prevent elements from moving when one or more sibling elements are leaving the scene.

Add the following inside the `ArrangeElements` method in place of the "SEE BELOW" comment marker:

```csharp
// Update the arrange state with the new arrange rect, but if there are leaving elements then don't move
//   the element
ArrangeState state = new ArrangeState(element, false, isLayoutUpdatePending);
if (!this.HasLeavingChildren)
	state.ArrangeRect = new Rect(x, y, desiredSize.Width, desiredSize.Height);
else
	state.ArrangeRect = state.PreviousArrangeRect;

PanelBase.SetArrangeState(element, state);
```

## Step 9: Use RandomPanel

The `RandomPanel` is completed and ready to be used in your applications.

```xaml
xmlns:sample="clr-namespace:ActiproSoftware.Windows.ProductSamples.ViewsSamples.QuickStart.CustomPanelRandom"
...
<sample:RandomPanel>
	<!-- Both buttons will be randomly placed -->
	<Button Content="One" \>
	<Button Content="Two" \>
</sample:RandomPanel>
```

## Step 10: Custom Animations

The `RandomPanel` supports all same the built-in animations and settings as the built-in panels, as well as any custom animations created.  The following will change will have elements entering the scene fly in from random locations, and leaving elements will fly out.

```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
xmlns:sample="clr-namespace:ActiproSoftware.Windows.ProductSamples.ViewsSamples.QuickStart.CustomPanelRandom"
...
<sample:RandomPanel>
	<sample:RandomPanel.ArrangeAnimation>
		<views:ArrangeAnimation EnterAnimation="Fade,Rotate,Scale,Translate" LeaveAnimation="Fade,Rotate,Scale,Translate"
		                        ScaleEnterUniformScale="0" ScaleLeaveUniformScale="5" />
	</sample:RandomPanel.ArrangeAnimation>
	<!-- Both buttons will be randomly placed -->
	<Button Content="One" \>
	<Button Content="Two" \>
</sample:RandomPanel>
```
