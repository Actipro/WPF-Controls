---
title: "Overview"
page-title: "Ribbon Command Model"
order: 1
---
# Overview

Actipro Ribbon is designed to be tightly integrated with the great command model found in WPF.

Every ribbon control implements `ICommandSource`, meaning that it has [Command](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.Command), [CommandParameter](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.CommandParameter), and [CommandTarget](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.CommandTarget) properties.  The `ICommand` that is assigned to a ribbon control can be any command, including those defined in the WPF framework in the `System.Windows.Input` namespace.

Ribbon controls also can use commands to provide several user interface elements for the controls that use them.  These user interface elements include labels, images and screen tip data.  Any command can provide this functionality although an extra step is needed for commands that don't implement [IRibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.IRibbonCommandUIProvider).

Once you read through this topic, please see these topics for more an explanation of some more advanced things you can do with commands:

- [Interaction with Checkable Controls](checkable-controls.md)
- [Interaction with Value Controls](value-controls.md)
- [Using Commands to Provide User Interface Elements](command-ui-provider.md)

## Prototype in XAML, Use Commands Later

We encourage you to prototype your ribbon user interface in XAML and then change to a command-driven model later once your ribbon user interface design is approved.  Note that switching to a command-driven interface doesn't mean that you stop using XAML.  Not at all!  In XAML you would simply insert a `Command` attribute on your controls and pass it the appropriate command you defined in code.  If your command also defines user interface elements, you can remove properties like `Label`, etc. from the XAML definition for the control.

This subject is explained in more detail later in the [Using Commands to Provide User Interface Elements](command-ui-provider.md) topic.

## Processing Command Executions

Since Ribbon uses the WPF command model, all the Microsoft documentation on commands applies.  But we'll give a quick overview anyhow.

A command is a class implementation of `ICommand` (typically inheriting from `RoutedCommand`) that is exposed statically on another class.  An example command is `System.Windows.Input.EditingCommands.ToggleBold`.

There is currently no WPF framework command for toggling strike-through so let's add one to our own `ApplicationCommands` class.

```csharp
public class ApplicationCommands {
				
	private static RibbonCommand toggleStrikethrough;

	public static RoutedCommand ToggleStrikethrough {
		get {
			if (toggleStrikethrough == null)
				toggleStrikethrough = new RibbonCommand("ToggleStrikethrough", typeof(Ribbon), 
					"Strikethrough", null, "/Images/Strikethrough16.png", 
					"Draw a line through the middle of the selected text.");
			return toggleStrikethrough;
		}
	}
}
```

Note that the command is of type [RibbonCommand](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand) (which implements [IRibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.IRibbonCommandUIProvider)) and therefore is capable of providing some user interface elements to any control that uses it.  In the example, we have provided a label, small image, and a screen tip description.

Now say that we have a `RichTextBox` class override named `RichTextBoxExtended`.  In the override, we want to add a handler for a custom `ToggleStrikethrough` command like so:

```csharp
this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleStrikethrough, 
	OnToggleStrikethroughExecute, OnToggleStrikethroughCanExecute));
```

The `OnToggleStrikethroughExecute` implementation should look something like this:

```csharp
private void OnToggleStrikethroughExecute(object sender, ExecutedRoutedEventArgs e) {
	this.SelectionStrikethrough = !this.SelectionStrikethrough;
	e.Handled = true;
}
```

The `OnToggleStrikethroughCanExecute` implementation can be made to update the checked state of any attached ribbon control that supports working with [ICheckableCommandParameter](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.ICheckableCommandParameter).  For a sample of this type of code, see the [Interaction with Checkable Controls](checkable-controls.md) topic.
