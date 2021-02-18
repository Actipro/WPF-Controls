---
title: "Using Commands to Provide User Interface Elements"
page-title: "Using Commands to Provide User Interface Elements - Ribbon Command Model"
order: 4
---
# Using Commands to Provide User Interface Elements

Commands can provide user interface elements such as labels, images, and screen tip data to any control that uses them.  Doing this requires the implementation of the [IRibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.IRibbonCommandUIProvider) interface.

If your commands inherit [RibbonCommand](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand) (which inherits `RoutedCommand`), they are already set up with properties and methods that support user interface provisioning.  Properties such as [Label](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand.Label), [ImageSourceLarge](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand.ImageSourceLarge), etc.  can be used to supply any attached controls with those user interface elements.

Now sometimes there are existing commands that you want to use which don't inherit [RibbonCommand](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand).  No problem, by registering an existing command with the [RibbonCommandUIManager](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIManager), any command can provide user interface elements.

Say that you wanted the WPF framework `EditingCommands.ToggleBold` command to provide user interface elements to any control that uses it.  You would register the command at the startup of your application like this:

```csharp
RibbonCommandUIManager.Register(EditingCommands.ToggleBold, 
	new RibbonCommandUIProvider("Bold", null, "pack://application:,,,/SampleBrowser;component/Images/Bold16.png", "Make the selected text bold."));
```

What happens is that [RibbonCommandUIManager](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIManager) ties the command to the [RibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIProvider) that you pass.  Note that the [RibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommandUIProvider) implements [IRibbonCommandUIProvider](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.IRibbonCommandUIProvider) just like [RibbonCommand](xref:ActiproSoftware.Windows.Controls.Ribbon.Input.RibbonCommand) does.

Be sure to use full pack:// syntax so that the created `BitmapImage` can be frozen to prevent possible memory leaks.
