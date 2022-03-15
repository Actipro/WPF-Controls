---
title: "AdvancedTabControl"
page-title: "AdvancedTabControl - Docking & MDI Reference"
order: 21
---
# AdvancedTabControl

Docking & MDI includes a very full-featured tab control named [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl) that is extremely customizable and can be used independently of docking windows in your apps. @if (wpf) {[AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl) inherits the native WPF `TabControl` and extends it with many helpful features. }

@if (wpf) {

![Screenshot](images/advanced-tab-control.png)

*An AdvancedTabControl with curved, slanted, overlapping tabs*

}

The type of item for the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl) is [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem).  Make sure to use that type or a derived class whenever specifying tab items.

## Selection

Just like with standard `ItemsControl` and `Selector` controls, the `SelectedItem` property specifies the item that is currently selected in the tab control.  Note that in MVVM scenarios, this will return the item that is selected and not the [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem).

## Tab Layout Kinds

Tabs can be displayed in three layout kinds within the tab control: pinned, normal, and preview.  Each tab's layout kind is set via the [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem).[LayoutKind](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem.LayoutKind) property, which defaults to `Normal`.

Normal tabs appear normally and left-aligned within the tab strip.  If the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[CanTabsPin](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.CanTabsPin) is `true` they will show a pin button that allows them to change to the `Pinned` layout kind when clicked.

Pinned tabs always appear to the left of all normal tabs.  They always show an unpin button that when clicked, will return them to `Normal` layout kind.

Preview tabs show up right-aligned in the tab strip.  They are meant to show temporary contextual information.  As long as the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[CanTabsPromote](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.CanTabsPromote) property remains its default `true` value, preview tabs will have a button on them that when clicked, will change the tab to a `Normal` layout kind.

## Tab Images

Tabs can show images on them if the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[HasTabImages](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.HasTabImages) property is set to `true` and the [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem).[ImageSource](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem.ImageSource) property is set to a valid `ImageSource`.

## Tab Overflow Behavior

There are numerous options from the [TabOverflowBehavior](xref:@ActiproUIRoot.Controls.Docking.TabOverflowBehavior) enumeration for selecting what happens when the tabs overflow the available tab strip space.

By default, the tabs shrink to fit within the available tab strip space.  This can be changed via the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabOverflowBehavior](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabOverflowBehavior) property.

## Tab Strip Placement

The tab strip can be located on any side (left, top, right, bottom) of the tab control.  Use the `TabStripPlacement` property to adjust the side as needed.

## Tab Content Orientation

Tabs will normally always display their content in a horizontal orientation, even when the tab strip placement is on the left or right.

Tab content can be rotated to a vertical orientation by setting the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[IsTabContentHorizontal](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.IsTabContentHorizontal) property to `false`.

## Tab Control Appearance

The tab control has several properties that adjust its own appearance.

### Tab Strip Visibility

The tab strip can be hidden by setting the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[IsTabStripVisible](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.IsTabStripVisible) property to `false`.

### Highlight

The tab strip can display an optional highlight border around its outside.  The border thickness is set by the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[HighlightThickness](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.HighlightThickness) property and the brushes used to render the highlight are specified via the [HighlightBrushInactive](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.HighlightBrushInactive) and [HighlightBrushActive](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.HighlightBrushActive) properties.  Which one is used depends on if the tab control is marked active.

### Flagging as Active

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[IsActive](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.IsActive) property flags the tab control as whether it is currently active or not.  This flag is generally set to `true` when the focus moves within the tab control.  The active setting affects which brushes are used for highlight effects and tab rendering.

## Tab Appearance

Numerous properties allow you to completely customize the appearance of tabs in the tab control.

### Background, Border, and Foreground

There are numerous variations of properties like [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabBackground](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabBackground), such as [TabBackgroundPointerOver](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabBackgroundPointerOver), etc. that determine the background brush used to render tabs in various layout kinds and states.

Likewise, similar sets of layout kind and state properties are available for [TabBorderBrush](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabBorderBrush) and [TabForeground](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabForeground).

### Corner Radius

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabCornerRadius](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabCornerRadius) property sets the corner radius of tab borders, relative to a top-facing tab.

### Padding

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabPadding](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabPadding) property sets the tab padding, relative to a top-facing tab.

### Spacing

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabSpacing](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabSpacing) property, which defaults to `0`, determines the amount of space between each tab.  Set it to a negative value to have tabs overlap each other.

### Minimum and Maximum Extents

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[MinTabExtent](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.MinTabExtent) and [MaxTabExtent](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.MaxTabExtent) properties set the minimum and maximum extent (length) that a tab can be.  Set these properties to the same value to create fixed size tabs.

@if (wpf) {

### Side Slant

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabNearSlantExtent](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabNearSlantExtent) and [TabFarSlantExtent](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabFarSlantExtent) properties set the amount of slant that tabs have on their near (left) and far (right) sides.

}

### Highlighting Inactive Tabs On Pointer Over

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[CanTabsHighlightOnPointerOverWhenInactive](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.CanTabsHighlightOnPointerOverWhenInactive) property sets whether tabs can highlight when the pointer is over inactive ones.  The default value is `false`.

## New Tab Button

A new tab button can be displayed by setting the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[HasNewTabButton](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.HasNewTabButton) property to `true`.  The new tab button appears just to the right of the normal tabs.  It is recommended that one of the shrink-related overflow behaviors is used when a new tab button is present.

The style of the button can be set with the [NewTabButtonStyle](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.NewTabButtonStyle) property.

When the button is clicked, the [NewTabRequested](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.NewTabRequested) event fires.  Handlers of that event should add a new tab to the control in response.

## Embedded Buttons

The tab control and its tabs can embed several buttons.  Tab control embedded buttons generally display in response to the tab overflow behavior setting.  Several tab embedded buttons depend on the tab's layout kind and other options like [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[HasTabCloseButtons](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.HasTabCloseButtons).

Some tab embedded buttons are only visible when the tab is selected or in a pointer over state.  The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[AreTabEmbeddedButtonsAlwaysVisible](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.AreTabEmbeddedButtonsAlwaysVisible) property sets whether tab embedded buttons are always visible, even when the tab is unselected.

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[EmbeddedButtonStyle](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.EmbeddedButtonStyle) property allows you to customize the `Style` of tab control embedded buttons, while the [TabEmbeddedButtonStyle](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabEmbeddedButtonStyle) property allows you to customize the `Style` of tab embedded buttons,

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[MenuButtonContentTemplate](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.MenuButtonContentTemplate), [OverflowMenuButtonContentTemplate](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.OverflowMenuButtonContentTemplate), [ScrollBackwardButtonContentTemplate](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.ScrollBackwardButtonContentTemplate), and [ScrollForwardButtonContentTemplate](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.ScrollForwardButtonContentTemplate) properties allow you to customize the `DataTemplate` of the related tab control embedded buttons.

## Tab Closing

Tabs can close when their embedded close button is clicked or also on middle-click if the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[CanTabsCloseOnMiddleClick](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.CanTabsCloseOnMiddleClick) property is set to `true`.  These features also require that the [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem).[CanClose](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem.CanClose) property is `true`.

When a tab is about to close, it raises the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[TabClosing](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.TabClosing) event.  If the `Cancel` flag is set to `true` in the event arguments, the close will not occur.  This event is a good place to prompt the end user about unsaved changes, etc.

## Tab Dragging to Reorder

While tabs do support dragging to reorder themselves, the feature is not enabled by default.  To enable this behavior, set the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[CanTabsDrag](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.CanTabsDrag) property to `true`.

@if (wpf) {

## Tab Selection on System Drag Over

By default, performing a system (e.g. text, file) drag over a tab will select the tab so that the dragged content can be dropped on a control on the tab's content area.  To disable this feature, set the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[CanTabsSelectOnDragOver](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.CanTabsSelectOnDragOver) property to `false`.

}

## Layout Animation

By default, animation effects are applied during layout, such as when tabs are added or removed from the tab control.  To disable these animation effects, set the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[IsLayoutAnimationEnabled](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.IsLayoutAnimationEnabled) property to `false`.

## Menus

The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[MenuOpening](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.MenuOpening) event fires before a context or drop-down menu is opening.  Its event arguments specify the related [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem), if any, along with the default menu that will be displayed.  You can fully customize or change the menu to suit your needs.

## Keyboard Shortcuts

When the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[IsTabKeyboardSwitchingEnabled](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.IsTabKeyboardSwitchingEnabled) property is `true` (the default), the `Ctrl+Tab` shortcut will switch to the next tab and the `Ctrl+Shift+Tab` shortcut will switch to the previous tab.

When the [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl).[IsTabKeyboardAccessEnabled](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl.IsTabKeyboardAccessEnabled) property is `true` (the default), keyboard access to directly select a tab is enabled. `Ctrl+1` will activate the first tab in a normal state, with `Ctrl+2` activating the second normal tab, and so on. `Ctrl+Alt+1` will activate the first tab in a pinned state, with `Ctrl+Alt+2` activating the second pinned tab, and so on.
