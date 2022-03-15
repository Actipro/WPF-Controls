---
title: "Standalone Control Usage"
page-title: "Standalone Control Usage - Ribbon Controls"
order: 10
---
# Standalone Control Usage

Actipro has created the ribbon controls with WPF control design best practices in mind and so that they can be used outside of the ribbon, in other parts of your application as well.

For instance, perhaps you want to use a ribbon [PopupButton](interactive/popupbutton.md) on a `Window` that doesn't have a [Ribbon](xref:@ActiproUIRoot.Controls.Ribbon.Ribbon) on it.  No problem!  You can even set the button's properties like [VariantSize](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.ControlBase.VariantSize) to get it to display in the desired variant.

## Ribbon Controls Designated for Standalone Use

These ribbon controls are designated as being appropriate for standalone use if you need them:

- [Button](interactive/button.md)
- [CheckBox](interactive/checkbox.md)
- [ComboBox](interactive/combobox.md)
- [ContextMenu](miscellaneous/contextmenu.md)
- [FontFamilyComboBox](interactive/fontfamilycombobox.md)
- [FontSizeComboBox](interactive/fontsizecombobox.md)
- [Group](miscellaneous/group.md)
- [GroupPresenter](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.GroupPresenter)
- [Menu](miscellaneous/menu.md)
- [PopupButton](interactive/popupbutton.md)
- [PopupGallery](interactive/popupgallery.md)
- [RadioButton](interactive/radiobutton.md)
- [RibbonGallery](interactive/ribbongallery.md)
- [Separator](interactive/separator.md)
- [SplitButton](interactive/splitbutton.md)
- [TextBox](interactive/textbox.md)

All of them may be restyled however you need.

## Samples

A QuickStart in the sample project shows the standalone usage of the ribbon controls.
