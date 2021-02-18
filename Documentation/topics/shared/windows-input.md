---
title: "Input"
page-title: "Input - Shared Library Reference"
order: 8
---
# Input

The [ActiproSoftware.Windows.Input](xref:ActiproSoftware.Windows.Input) namespace contains several classes that are helpful for working with input.

## Mouse Wheel Gesture

The [MouseWheelGesture](xref:ActiproSoftware.Windows.Input.MouseWheelGesture) can be used to bind mouse wheel movement to a command using the `InputBindings` collection. The gesture supports positive or negative deltas (i.e. up or down scrolling of the mouse wheel), with optional modifier keys.

The [MouseWheelBinding](xref:ActiproSoftware.Windows.Input.MouseWheelBinding) can be used from XAML to easily add a mouse wheel gesture.

This code shows how to add a mouse wheel gesture in XAML:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"     
...
<MyControl>
	<MyControl.InputBindings>
		<shared:MouseWheelBinding Command="{x:Static ScrollBar.LineUpCommand}" Gesture="PositiveDelta" />
		<shared:MouseWheelBinding Command="{x:Static ScrollBar.LineDownCommand}" Gesture="NegativeDelta" />
		<shared:MouseWheelBinding Command="{x:Static ScrollBar.LineLeftCommand}" Gesture="Shift+PositiveDelta" />
		<shared:MouseWheelBinding Command="{x:Static ScrollBar.LineRightCommand}" Gesture="Shift+NegativeDelta" />
	</MyControl.InputBindings>
</MyControl>
```
