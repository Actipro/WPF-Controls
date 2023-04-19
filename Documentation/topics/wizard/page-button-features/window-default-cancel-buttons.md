---
title: "Window Default/Cancel Buttons"
page-title: "Window Default/Cancel Buttons - Wizard Page and Button Features"
order: 5
---
# Window Default/Cancel Buttons

Wizard makes it easy to set the default or cancel buttons of a containing `Window` to one of the wizard's buttons.

## Window Default Button

The `Window`'s default button is clicked whenever the <kbd>Enter</kbd> key is pressed on the keyboard.

The default button can be set to one of the wizard buttons by setting the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowDefaultButton](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowDefaultButton) property to one of these [WizardWindowDefaultButton](xref:@ActiproUIRoot.Controls.Wizard.WizardWindowDefaultButton) values:

| Value | Description |
|-----|-----|
| `None` | No wizard button is the default button. |
| `Next` | The **Next** button is the default button. |
| `Finish` | The **Finish** button is the default button. |
| `FinishThenNext` | The **Finish** button is the default button if the **Finish** button is visible and enabled.  Otherwise, the **Next** button is the default button. |
| `NextThenFinish` | The **Next** button is the default button if the **Next** button is visible and enabled.  Otherwise, the **Finish** button is the default button. |

The default value is `FinishThenNext`.

This XAML code shows how to turn off the automatic wizard default button so that another `Button` may be set as the default button on the containing `Window`:

```xaml
<wizard:Wizard WizardWindowDefaultButton="None"> ... </wizard:Wizard>
```

## Window Cancel Button

The `Window`'s cancel button is clicked whenever the <kbd>Esc</kbd> key is pressed on the keyboard.

The cancel button can be set to one of the wizard buttons by setting the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowCancelButton](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowCancelButton) property to one of these [WizardWindowCancelButton](xref:@ActiproUIRoot.Controls.Wizard.WizardWindowCancelButton) values:

| Value | Description |
|-----|-----|
| `None` | No wizard button is the cancel button. |
| `Cancel` | The **Cancel** button is the cancel button. |

The default value is `Cancel`.

This XAML code shows how to turn off the automatic wizard cancel button so that another `Button` may be set as the cancel button on the containing `Window`:

```xaml
<wizard:Wizard WizardWindowCancelButton="None"> ... </wizard:Wizard>
```
