---
title: "Converting to v23.1"
page-title: "Converting to v23.1 - Conversion Notes"
order: 86
---
# Converting to v23.1

The 23.1 version updates .NET targets, code-signs all assemblies, and makes improvements to numerous products.  The biggest piece of v23.1 is the addition of a new [Bars product](../bars/index.md) (in beta) that includes advanced fluent ribbons, toolbars, menus, and related controls.

## New .NET and .NET Framework Targets

Control assemblies for .NET have been updated to target .NET 6 (previously .NET 5) since .NET 5 is no longer supported. Any applications that target .NET 5 will still be supported using our .NET Core 3.1 targets.

Control assemblies for .NET Framework have been updated to target .NET Framework 4.6.2 (previously .NET Framework 4.5.2 or 4.6.1) since it is the most recent supported version. Any applications that reference our controls that were also on an older version of .NET Framework will need to update their targets to at least 4.6.2. Users who cannot update to .NET Framework 4.6.2 should not update to this release and continue using prior versions of these controls.

## All Assemblies Code-Signed

All product assemblies are now code-signed so that their authenticity can be verified.

We previously only shipped non-code-signed assemblies, and the installer had an option for whether to install code-signed variations of .NET Framework assemblies.  That installer option has been removed since all assemblies are now code-signed, including all assemblies in the NuGet packages.

## Grids

### TreeListBox and TreeListView Item Selection on Focus

Prior versions of `TreeListBox` and `TreeListView` used to select an item when it was focused, however this is inconsistent with native ListBox behavior.  In this version, the [TreeListBox.CanSelectItemOnFocus](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.CanSelectItemOnFocus) property has been added with a default of `false` to prevent selection by default, better aligning with `ListBox` behavior.  Whereas the `PropertyGrid` control uses a default of `true` for this new property.

If the prior version behavior is desired for `TreeListBox` or `TreeListView`, set the new [CanSelectItemOnFocus](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.CanSelectItemOnFocus) property to `true`.

## Shell

### Image Size Refactoring

Prior Shell versions aligned the various image sizes with how they were used in UI, rather than exactly with the Windows shell image sizes, which the Windows Explorer does.  This lead to the inability to retrieve 32x32 icons from the Windows image lists via the [Shell Objects](../shell/shell-objects-framework/shell-objects.md).  In updates for v23.1, the logic for a couple of the image sizes has been refactored and the UI templates for presenting shell objects have been updated appropriately.

This chart lists the pixel sizes for images (retrieved via properties such as [LargeIcon](xref:ActiproSoftware.Shell.IShellObject.LargeIcon)) for v23.1 compared to prior versions:

| Size Name  | Previous Version Size | New v23.1 Size |
|------------|-----------------------|----------------|
| Small      | 16x16                 | 16x16          |
| Medium     | 48x48                 | 32x32          |
| Large      | 96x96                 | 48x48          |
| ExtraLarge | 256x256               | 256x256        |

## Bars and Ribbon

v23.1 introduces a beta of the new [Bars product](../bars/index.md), which includes advanced fluent ribbons, toolbars, menus, and related controls.  This first release includes a powerful new `Ribbon` control that has the latest Office appearance with subtle animations, is extremely customizable, and makes it easy to build custom galleries.  A `StandaloneToolBar` control can be used as a window's main toolbar or can be enclosed within a tool window.  Future planned updates for Bars will add docking toolbar functionality.  Everything from popup/split buttons to comboboxes to galleries can be included anywhere in a ribbon, toolbar, or menu.

An [open source Bars MVVM library](../bars/mvvm-support.md) implements a complete set of view models and related UI views for building a full ribbon hierarchy in code.  It also includes multiple examples of building visually stunning galleries that are seen in Office.

The new `Ribbon` control implementation in Bars was built from the ground up with WPF best-practices and supports MVVM usage, whereas the legacy `Ribbon` did not.  The controls that can be hosted in Bars `Ribbon` are also used throughout the other toolbars and menus in Bars, making it an all-encompassing solution for everything bar-related.  For these reasons, all new ribbon, toolbar, and menu development should use the new Bars product instead of the older Ribbon product.

> [!IMPORTANT]
> The [legacy Ribbon product](../ribbon/index.md), originally introduced in 2007, was one of the first commercial WPF controls released.  It is set to be deprecated once Bars exits its beta status since the Bars product's ribbon was built from scratch to address the legacy ribbon's deficiencies.

While not a complete list of new and improved features, the subsequent sections cover many general ribbon design improvements and offer some conversion tips when migrating from legacy Ribbon to Bars.

### Layout Modes and Density

Microsoft introduced a new single-row appearance for ribbons in recent Office versions.  The legacy ribbon is unable to support this "Simplified" layout mode, but the Bars ribbon can instantly flip between Simplified and Classic layout modes by toggling a property value.

#### Layout Mode Comparison

Simplified layout mode is a more modern take on ribbons where all controls appear in a single row, usually with a spacious touch-friendly density.  Ribbons in this layout mode can still support some variant size changes as available width changes.  Any child controls that overflow will be placed on an ellipses menu.

Classic layout mode is the traditional ribbon design that is taller, supporting up to three rows of child controls, and has a more compact density.  This layout mode is best for large applications that have lots of controls.

> [!TIP]
> See the [Layout Modes and Density](../bars/ribbon-features/layout-and-density.md) topic for more information on layout modes.

#### User Interface Density

All ribbon controls have been designed to support the concept of user interface density, something that the legacy ribbon does not do.  By default, ribbons in Simplified layout mode should use a spacious density, while ribbons in Classic layout mode should use a compact density.  That being said, the three density options may be applied to a ribbon in any layout mode.  This allows for possibilities such as creating larger, more touch-friendly buttons for a ribbon in Classic layout mode.

> [!TIP]
> See the [Layout Modes and Density](../bars/ribbon-features/layout-and-density.md) topic for more information on user interface density.

### Modern Office Appearance

The new Bars ribbon has the latest modern Office appearance that isn't available in the legacy ribbon.

#### Window Backdrop

The new [RibbonWindow](../bars/ribbon-features/ribbon-window.md) class is configured by default to support a system backdrop background in the window when run in Windows 11, similar to Office.  This allows colors from the Windows desktop to show through into the ribbon window in a muted fashion.  The Bars ribbon is designed to render well, regardless of whether a system backdrop is used in the window or not.

#### Fluid Animation

More quick, subtle animations have been added throughout the Bars ribbon.  As you change tabs or toggle the ribbon's [footer](../bars/ribbon-features/footer.md) visibility, controls slide into place.  As the [ribbon resizes](../bars/ribbon-features/resizing.md), controls animate to their new location.  The [ribbon backstage](../bars/ribbon-features/backstage.md) fades and slides into place as it is opened or closed.  All of this enhances the user's experience by making the product feel more alive.

### MVVM Support

One enormous design deficiency in the legacy ribbon is that it doesn't support MVVM in most areas.  Whereas the Bars ribbon is designed with the intent of MVVM being the primary, but still optional, way to configure and work with the ribbon.

Most controls used in the Bars ribbon hierarchy derive `ItemsControl` where appropriate.  They support the concept of binding view models via `ItemsSource` properties and using item container template selectors to generate the appropriate controls for those view model items.  In practice this works extremely well and allows you to define and manage a complete ribbon hierarchy within view models.

> [!TIP]
> See the [MVVM Support](../bars/mvvm-support.md) topic for more information on how to use the library's view models and view templates to create and manage your ribbon controls with MVVM techniques.

### Improved Control Infrastructure

The legacy Ribbon product contains a lot of controls that are completely custom or don't follow best practices for use of the controls.  This has been rectified in the Bars ribbon implementation.

#### Single-Purpose Controls

Controls in the legacy Ribbon product are often capable of usage in many contexts.  For instance, by setting a context property, a `ribbon:Button` can alter its template so its appearance completely changes for use in a ribbon, menu, backstage, status bar, etc.  This leads to very complex control styles and templates that are tedious to alter.

Controls in Bars have been designed to have a single purpose and intention for their usage.  This keeps the control infrastructure and the control styles/templates much purer.

#### Native Control Derivation

Many controls in the legacy Ribbon product are completely custom controls.  While this is all right for some control types, it makes more sense for many controls to derive from and augment features from similar native WPF controls instead.

The controls in Bars try to derive from related native WPF controls wherever it makes sense to do so.  This helps ensure that well-known patterns are followed for various control types.  For instance, the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) control inherits native `Button`, and the [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) control inherits native `MenuItem`.

#### Menu Infrastructure Changes

The legacy Ribbon product used some complex control hierarchies to allow controls such as `ribbon:Button` to be used in a menu context.  The button would have to be wrapped by a `ribbon:Menu` and that would help it to alter its template by using a different ribbon context.  While the result was a working menu with Office appearance, the entire design was less than ideal.

The Bars controls on the other hand have specific controls that derive `MenuItem` or are otherwise (as in the case of [menu galleries](../bars/controls/gallery.md)) intended for use within menu contexts.  These controls can be used directly within any control that has a popup menu, or in any [context menu](../bars/menu-features/context-menus.md).

#### UIA Peers (Accessibility)

While the legacy Ribbon product has a number of UI automation peers configured for various controls, UI automation peers are configured for all controls in Bars.  The Bars UIA peers allow for a very nice peer tree to be visible for accessibility tools, and support many UIA patterns.

### Resizing and Variants

The legacy ribbon has excellent support for resizing various controls through the use of variants as the ribbon's available width changes.

The same sort of resizing and variant size features are still available in Bars ribbon, but the implementation is a bit different, and easier to use.  The Simplified and Classic layout modes each have their own distinct ways of specifying variant size transitions, allowing you to tailor the ribbon's resizing capabilities separately for each layout mode.  And some new options are available in general for ribbon resizing.

> [!TIP]
> See the [Resizing and Variants](../bars/ribbon-features/resizing.md) topic for more information on how a ribbon should apply variants to child controls when resizing.

### Commands

Commands are an area where many changes have been made.  The legacy Ribbon product uses WPF commands in ways in which they aren't really intended to be used, such as providing UI elements, managing checked state, managing values, etc.

The Bars controls revert to using commands primarily for processing a primary action for a control.  Galleries can also optionally use special commands that support live preview.  Other than that, all the extended features previously supported by legacy ribbon's commands should now be managed with a MVVM infrastructure instead.

> [!TIP]
> See the [Using Commands](../bars/controls/using-commands.md) topic for more information on how to use commands with Bars controls.

### Galleries

Galleries are one of the best parts of a ribbon since they provide a graphically-rich way to make a selection from a list.  The legacy Ribbon product supported galleries, but the Bars product implements galleries in a slightly different way that is more centered around MVVM techniques.  Note that even if you define your ribbon in XAML, you must still use MVVM techniques for the items in any gallery controls.

The Bars galleries have more customization features that facilitate presenting gallery items in different ways, compared to legacy Ribbon's galleries.  Bars galleries can also be used in any toolbar (if a menu gallery is hosted within a popup/split button's menu), or directly within any menu.

The optional companion [MVVM Library](../bars/mvvm-support.md) includes numerous built-in galleries that can be reused in your applications, and the Bars samples in the sample project show many examples of other galleries that can be created as well.

> [!TIP]
> See the [Gallery](../bars/controls/gallery.md) topic for more information on galleries.

### Screen Tips

At a high level, Bars screen tips work like the screen tips in the legacy Ribbon product.  They inherit the native `ToolTip` control and may be used anywhere a native `ToolTip` can be used.  They no longer have some rarely-used configuration properties like `ImageSource` or `HelpUri`, but there are properties for fully configuring a header, content area, and optional footer area with whatever content you wish.

> [!TIP]
> See the [Screen Tips](../bars/ribbon-features/screen-tips.md) topic for more information on screen tips.

### Key Tips

Key tips have similar runtime features to the legacy Ribbon product, but Bars improves them in numerous ways.

First, key tips now support any character that can be typed into a keyboard, and not just Latin characters.

Second, key tips can optionally be activated for other root bar controls like [standalone toolbars](../bars/toolbar-features/standalone-toolbars.md).

Third, key tips now use attached events for certain things.  One event has numerous options that allow for a key tip to be precisely positioned relative to a target control.  Another event allows a target control to handle a key tip invocation.

> [!TIP]
> See the [Key Tips](../bars/ribbon-features/key-tips.md) topic for more information on key tips.

### Recent Documents

The entire mechanism for displaying recent documents has been redone in Bars.  The new [recent documents control](../bars/ribbon-features/recent-documents.md) supports two display modes.  One is larger and ideal for display in [backstage](../bars/ribbon-features/backstage.md).  The other is smaller and works well in the additional content area of an [application menu](../bars/ribbon-features/application-menu.md).

### Mini-Toolbar

The Bars product does not currently have an implementation of the legacy Ribbon product's mini-toolbar, which is a control that could display near a document selection or above context menus.

### Application Button

The [application button](../bars/ribbon-features/application-button.md) is implemented a bit differently in Bars ribbon, but still supports customizing the button's content, and can either show an [application menu](../bars/ribbon-features/application-menu.md) or [backstage](../bars/ribbon-features/backstage.md) when clicked.




