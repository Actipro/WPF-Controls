---
title: "Context Menus"
page-title: "Context Menus - Menu Features"
order: 10
---
# Context Menus

The [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu) control implements a context menu that can host any [Bars controls](../controls/index.md) intended for menu contexts, including [split menu items](../controls/split-button.md) or graphically-rich [menu galleries](../controls/gallery.md), and supports other advanced features like MVVM configuration of its items.

![Screenshot](../images/context-menu.png)

*A sample context menu used for an edit control*

## Native ContextMenu Compatibility

Since the [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu) class inherits the native `ContextMenu` class and its features, [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu) may be used anywhere that a regular context menu can be used.

## Defining a Context Menu

A context menu is most often assigned to a control via its `ContextMenu` property.  The control will automatically show the context menu when it is right-clicked or the keyboard's context menu key is pressed.

This sample code shows how to define a context menu for a `TextBox`:

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<TextBox>
	<TextBox.ContextMenu>
		<bars:BarContextMenu>
			<!-- Labels are auto-generated from Key -->
			<bars:BarMenuItem Command="ApplicationCommands.Undo" SmallImageSource="/Images/Icons/Undo16.png" />
			<bars:BarMenuItem Command="ApplicationCommands.Redo" SmallImageSource="/Images/Icons/Redo16.png" />
			<bars:BarMenuSeparator />
			<bars:BarMenuItem Command="ApplicationCommands.Cut" SmallImageSource="/Images/Icons/Cut16.png" />
			<bars:BarMenuItem Command="ApplicationCommands.Copy" SmallImageSource="/Images/Icons/Copy16.png" />
			<bars:BarMenuGallery
				Key="PasteOptions"
				AreSurroundingSeparatorsAllowed="False"
				CanCategorize="True"
				CategorizedItemsSource="{Binding PasteOptions}"
				CategoryHeaderTemplate="{StaticResource PasteOptionGalleryCategoryTemplate}"
				CategoryPropertyName="Category"
				Command="{Binding PasteSpecialCommand}"
				IsSynchronizedWithCurrentItem="False"
				ItemContainerStyle="{StaticResource BarGalleryItemStyle}"
				ItemTemplate="{StaticResource PasteOptionGalleryItemTemplate}"
				MaxColumnCount="6"
				UseMenuItemIndent="True" />
			<bars:BarMenuSeparator />
			<bars:BarMenuItem Command="ApplicationCommands.SelectAll" />
		</bars:BarContextMenu>
	</TextBox.ContextMenu>
</TextBox>
```

## Stay Open on Menu Item Click

Each control that derives from native `MenuItem` has a `StaysOpenOnClick` property that governs whether the containing menu closes when the menu item is clicked.

The default value for this property is `false` but can optionally be set to `true` to try and keep the containing menu open following a menu item click.

## Input Gesture Text

Input gestures are keyboard shortcuts that provide access to a control's command.  For instance, `Ctrl+C` is commonly associated with the clipboard `Copy` command.

Input gesture text is a textual representation of a keyboard shortcut, allowing the end user to learn which keyboard shortcut executes a command.  Input gesture text is shown in menu items and can be customized or hidden completely.

> [!TIP]
> See the [Control Basics](../controls/control-basics.md) topic for more information on input gesture text.

## MVVM Support

By setting the [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu).`ItemContainerTemplateSelector` property to a template selector that can construct controls intended for a menu context, the context menu's `ItemsSource` can be bound to a collection of view-models.

The optional companion [MVVM Library](../mvvm-support.md) includes a [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector) class that is designed for use with the `ItemContainerTemplateSelector` property, when the control view models from the library are the items being bound via the control's `ItemsSource` property.

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<TextBox>
	<TextBox.ContextMenu>
		<bars:BarContextMenu
			ItemContainerTemplateSelector="{Binding ItemContainerTemplateSelector}"
			ItemsSource="{Binding ContextMenuItems}" />
	</TextBox.ContextMenu>
</TextBox>
```

> [!TIP]
> See the [MVVM Support](../mvvm-support.md) topic for more information on how to use the library's view models and view templates to create and manage your application's bars controls with MVVM techniques.
