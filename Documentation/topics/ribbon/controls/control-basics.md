---
title: "Control Basics"
page-title: "Control Basics - Ribbon Controls"
order: 2
---
# Control Basics

All of the [built-in interactive ribbon controls](interactive/index.md) and several other ribbon controls share some common characteristics and concepts.  This topic reviews some of the core concepts for all these controls.

## Base Classes

Almost all of the controls described in the following sections inherit one of these four abstract base classes:

- [ControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase)
- [ItemsControlBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ItemsControlBase)
- [ComboBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ComboBoxBase)
- [TextBoxBase](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.TextBoxBase)

These base classes all implement a number of attached properties from classes like [RibbonControlService](xref:@ActiproUIRoot.Controls.Ribbon.UI.RibbonControlService).

## Context and Variant Size

Each control is associated with a context that describes how the control is currently being used.  This allows a control to alter its appearance and/or behavior if it is on the ribbon vs. on a menu item.

The context is available via the [Context](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.Context) property, which returns an enumeration value of type [ControlContext](xref:@ActiproUIRoot.Controls.Ribbon.Controls.ControlContext).

Many controls also support the concept of a variant.  A variant is a different look for a control that can be applied while in the same context.  For instance, [Button](interactive/button.md) controls have three variant sizes that are used for most cases, like when they are in the ribbon.  The large variant size is a tall button with a large image and text underneath it.  The medium variant size is a more normal size button with a small image and text label to the right of it.  The small variant size is like the medium variant size except that the label disappears.

The variant size is available via the [VariantSize](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.VariantSize) property, which returns an enumeration value of type [VariantSize](xref:@ActiproUIRoot.Controls.Ribbon.Controls.VariantSize).

All of the controls that support multiple variants use triggers in their styles to switch templates based on their current context and variant size settings.

## User Interface Elements

The base classes above all define some common user interface properties for how a control is displayed.  In many cases, controls on a ribbon require a text label and/or image to be set per the ribbon requirements.

These can be set on the control instance and alternatively can be set on a ribbon command (see the [Using Commands to Provide User Interface Elements](../command-model/command-ui-provider.md) topic) that the control uses.  Note that the control instance property values override those supplied by a ribbon commands.

These members on the base classes relate to user interface elements:

| Member | Description |
|-----|-----|
| [HasImage](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.HasImage) Property | Gets whether the control has an image available for display. |
| [HasLabel](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.HasLabel) Property | Gets whether the control has a label available for display. |
| [ImageSourceLarge](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.ImageSourceLarge) Property | Gets or sets the `ImageSource` for the 32x32 image to display for the control. |
| [ImageSourceSmall](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.ImageSourceSmall) Property | Gets or sets the `ImageSource` for the 16x16 image to display for the control. |
| [Label](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.Label) Property | Gets or sets the text label to display for the control. |

## Commands

Many of the interactive ribbon controls support a completely command-driven user interface.  This means that commands are able to process actions such as the pressing of the button as well as provide common user interface elements such as images and labels to any control that uses the command.

Please see the [Using Commands to Provide User Interface Elements](../command-model/command-ui-provider.md) topic for detailed information on using commands with controls and how to optionally have them provide common user interface elements.

## Popups

Several controls like [PopupButton](interactive/popupbutton.md) and [SplitButton](interactive/splitbutton.md) have popups associated with them.  The popups can contain any sort of content, including a [Menu](miscellaneous/menu.md) of menu items or one or more [custom controls](custom-controls.md).

See the [Working with Popups](working-with-popups.md) topic for more information on all the features available to you with popups.

## Key Tips

The control base classes all have built-in support for key tips, which are little popups that display when <kbd>Alt</kbd> is pressed and allow for quick keyboard access to each control.

Please see the [Key Tips](../layout-features/key-tips.md) topic for detailed information on working with key tips.

## Screen Tips

The control base classes also all have built-in support for screen tips, which are essentially tooltips that display a lot of helpful contextual information to the end user for a control.

Please see the [Screen Tips](../layout-features/screen-tips.md) topic for detailed information on working with screen tips.
