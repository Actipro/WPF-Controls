---
title: "Converting to v16.1"
page-title: "Converting to v16.1 - Conversion Notes"
order: 92
---
# Converting to v16.1

All of the breaking changes are detailed or linked below.

## Massive Updates to Docking and MDI

For the 16.1 version, we completely rewrote the entire Docking/MDI product from the ground up based on our years of experience in this area.  Along the way, we added nearly all of the major features our customers have requested and the result is the most feature-rich docking window and MDI product of its kind.

While we fully encourage you to upgrade your existing applications that use the older Docking/MDI product to the new version so that you can continue to receive the latest feature additions and bug fixes, we are also shipping a legacy version of the old assembly that can temporarily be used in the interim for full backward compatibility.

The 'Complete Update Details' section in this topic gives information on the breaking changes made, such as renamed/removed types/members.  The sections before it walk through some selected larger breaking changes in detail.

### Docking Window Role in UI

In the old version, the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) and [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) classes were rendered in UI as tabs.  This wasn't the best design since sometimes they weren't visible, even though their content was.

In this version, [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) and [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) now are simple containers of the content they display.  That means that they are effectively used like a `ContentControl` and are always visible in the UI if their content is visible.  This allows both the visual and UI automation trees to be more representative of the intended hierarchy.

### Docking Window SerializationId Set in Constructor Instead of Name

This version adds a new [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) property that can optionally be used instead of the `Name` property when [serializing/deserializing layouts](../docking/layout-features/layout-serialization.md).  The new property is better than `Name` because it can hold any text data and isn't limited to .NET identifier syntax.  It also can be fully two-way data bound.

Since [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) is a better property to use than `Name` for serialization purposes, although `Name` can still be used, we have updated the constructors for both [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) and [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) that used to accept a `Name` parameter to switch that parameter to assign the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) property instead.

The only real negative side effect of this is that an indexer of the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindows) and [DocumentWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentWindows) collections works off of `Name`.  Since that property won't be set directly any more by using the constructor that used to set it, indexer calls off those collections may return a `null` value unless you manually set the `Name` property on each docking window instance.

### Programmatic Layout Updates Don't Work Until Loaded

Sometimes docking windows are programmatically added to a layout (via opening, docking, layout loading, etc.) within the contructor of a root element like a `Window` or `UserControl` after its `InitializeComponent` method executes.  In situations like this, the code to add the docking windows to the layout needs to be performed at or after the time that root element's `Loaded` event is raised.

When the root element has loaded, that means that all the templates of the control hierarchy within the dock site have been properly loaded and applied as well.  The dock site and its features are then ready for programmatic interaction.

The old version did allow some limited programmatic interaction with a layout before the dock site was fully loaded due to the heavy use of inherited dependency properties to register various docking hierarchy controls with the dock site.  This version has eliminated all use of inherited dependency properties since they slow things down, but a side effect of that is that not all docking hierarchy controls get their templates applied or are registered with the owner dock site until after the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite)'s (or one of its containers') `Loaded` event has fired.

### All Styles and Templates Rebuilt

In this version, all styles and templates for the docking controls have been rebuilt from scratch.  They are now much simpler, use better composition of primitive controls (such as the new [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl)), and are far easier to customize.

Any styles designed for the old version will NOT work with this version and will need to be reimplemented.  Several primitive controls, such as `ReverseMeasureDockPanel` that were used in the old version and are no longer needed in this version, have been removed.

### DockSite.ControlSize Attached Property Removed

In the old verison, a `DockSite.ControlSize` property could be set to containers like [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) to designate their initial size.

In this version, that property has been removed and new properties directly on docking windows themselves have been added.  The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[ContainerDockedSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerDockedSize) property can be set for docked scenarios.  It is encouraged to set the same value to all docking windows in the same container.  The [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[ContainerAutoHideSize](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.ContainerAutoHideSize) property can be set for auto-hide popup size.

There are also new [ContainerMinSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerMinSize) and [ContainerMaxSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerMaxSize) properties that allow you to set a desired minimum and maximum size to which a container can be resized.

### LastActiveDocument and LastActiveToolWindow Removed

The old `LastActiveDocument`, `LastActiveToolWindow`, and related property change events have been removed in this version.

Use the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ActiveWindow](xref:@ActiproUIRoot.Controls.Docking.DockSite.ActiveWindow) to know which window is currently active, and watch the [WindowDeactivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowDeactivated) and [WindowActivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowActivated) events to know when that changes.

Use the new [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[PrimaryDocument](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrimaryDocument) property to know which document is currently the primary document.  This value will be the same as [ActiveWindow](xref:@ActiproUIRoot.Controls.Docking.DockSite.ActiveWindow) if the active window is a document.  If the active window is a tool window outside of the MDI area instead, the primary document will refer to the last open document that was active.  The [PrimaryDocumentChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrimaryDocumentChanged) event is raised when the related property value changes.

All docking windows now have a [LastActiveDateTime](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.LastActiveDateTime) property that you can examine to sort them by when they were last active.

### Clicking a Tab Close Button Won't Focus Within Container

In the old version, if the focus was in one container (such as a docked tool window) and then the close X button on a document tab was clicked, the tab would be closed and the next document tab in that tabbed MDI container would become active.  In vNext in this same scenario, focus remains in the original container (docked tool window) instead of activating the next tabbed document.

This means that the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowActivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowActivated) event won't be raised for the next tabbed document.  If you relied on this event to know when a new document became the primary document, switch over to using the new [PrimaryDocumentChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrimaryDocumentChanged) event instead, which is described inthe previous section.  The new [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[PrimaryDocument](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrimaryDocument) returns the open document that is currently active, or was last active in the case where a non-document is currently active.

### Renamed Rafting to Floating

In the previous version, the term "rafting" was used in a number of property names.  In this version, all instances of "rafting" are now named as "floating".

An example is the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite)`.CanDocumentWindowsRaft` property is now named [CanDocumentWindowsFloat](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanDocumentWindowsFloat).

### Multiple DockSite Events Changed to Pass Multiple Docking Windows

Several [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) events have been slightly renamed to a plural name to account for the fact that they now pass multiple affected docking windows instead of a single docking window.  The renamed events are: [WindowsClosing](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsClosing), [WindowsClosed](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsClosed), [WindowsOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsOpening), [WindowsOpened](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsOpened), and [WindowsStateChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsStateChanged).

The revised [WindowsClosing](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsClosing) event is handy in particular because when the Close All Documents and Close Others menu items are used, a single dialog with all windows listed can be presented to the user to check whether to close the windows.

### DockingWindowState.Floating Value Removed

The [DockingWindowState](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState) enumeration no longer has a `Floating` value.  The reason is that now floating dock hosts can have docked, auto-hidden or document-oriented docking windows in them, so that option no longer fit.

In addition, this change means that the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowsStateChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsStateChanged) event will no longer be raised when a docking window is moved to or from a floating dock host, unless doing so is a transition between the docked, auto-hide, or document states.

### Tool Window Transitions Removed

Tool window content animated transitions have been removed since they aren't used much, are distracting, and add more visual complexity to the layout model.  Other new subtle quick animations are used throughout the UI (such as for dock guides, drop targets, etc.) that don't affect layout performance and yet give the app an even more fluid feel.

### DockSiteViewModelBehavior Removed from MVVM Samples

The old `DockSiteViewModelBehavior` class has been removed from all MVVM samples since it is no longer needed due to new MVVM-friendly features.  Two way data binding on [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen), [IsActive](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsActive), [IsSelected](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsSelected), and [State](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.State) are all supported.

In addition, there are numerous new features described in the [Lifecycle and Docking Management](../docking/docking-window-features/lifecycle-and-docking-management.md) topic that deal with adjusting (even dynamically) the default location for opened windows.

Finally, there are new built-in features for the dock site to automatically remove a view model item from the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemsSource) or [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemsSource) collections when the related container docking window is destroyed.  This is possible only if the items source property is assigned a collection that is an editable list.  If another non-editable collection is used instead, you must attach to the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowUnregistered](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowUnregistered) event and remove the related item from the items source yourself.

### DockingCommands Class Removed

The old `DockingCommands` class that contained static properties with docking window-related commands has been removed.  Command properties have been moved to their appropriate target class instead, such as the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[ActivateCommand](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ActivateCommand) property.

### Tabbed MDI Tab Title Editing Feature Removed

The tabbed MDI tab title editing feature (`DockSite.CanDocumentWindowsEditTitles`) has been removed since it is rarely used and would add much more complexity to the [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem) control template.  If you used this feature in the old version, we recommend adding a context menu item to tabs via the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[MenuOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.MenuOpening) event that when clicked, triggers a title text editing dialog.

### DockSite.FullScreen Property Removed

The dock site `FullScreen` property has been removed since its old implementation wouldn't have functioned well with the new features of floating dock hosts where complete secondary MDIs are supported in them.

### SingleTabLayoutBehavior.Stretch Option Removed

The `SingleTabLayoutBehavior.Stretch` option has been removed.  If a similar feature is added in the future, it would be to force all tabs to fill up to the available space instead of just a single tab.

### ToolWindowTabFlashBehavior Sample Class Removed

The `ToolWindowTabFlashBehavior` attached behavior class that used to be in the Sample Browser has been removed.  That class allowed for animated tinting of tabs to achieve a flash effect, but is no longer needed since new properties are now available directly on [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) that support this feature.

See the [Docking Window Capabilities](../docking/docking-window-features/docking-window-capabilities.md) topic for more information on tab flashing features.

### Header/HeaderTemplate Properties Removed

The previous version had `Header` and `HeaderTemplate` properties that could be used to provide custom content for tabs, since the tab templates themselves were the docking windows.

In this version, docking windows are no longer represented in UI as tabs themselves, and are instead simple containers around their actual child content when in UI.  The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl) primitive control is now used to render tab UI for docking windows where appropriate.  See the `Docking Window Role in UI` section above for more detail.

While properties like [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Title](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Title), [TabText](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabText), and [ImageSource](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ImageSource) can be used to indicate tab content, sometimes you also want to be able to include additional contextual content.  This could be anything from status indicators, buttons, etc.  The [Contextual Content](../docking/docking-window-features/contextual-content.md) topic talks about how you can provide various `DataTemplate` values to inject custom content into tabs in multiple locations throughout the docking UI.

### Prism Integration Changes

In the old version, we maintained and shipped a separate *ActiproSoftware.Docking.Interop.Prism.Wpf.dll* assembly that contained some Prism integration logic.  This assembly's source code was also located at [http://actipro.codeplex.com](http://actipro.codeplex.com).  The latest source code of that assembly, compiled for Prism 5.0 and Actipro WPF Controls v15.1 is still available there.

Starting in this version, we no longer ship a newer version of that assembly but do include everything you need for Prism 6.1 compatibility right in the Prism integration sample that comes with the WPF controls.  This way, you are able to easily see all the source code, copy it to your own application, and customize it as needed.  The same features as before are available in this new open source offering, and it works with the latest Prism and Actipro WPF Controls versions.

The old version had an `IDockingWindowInitializer` interface and several class implementations of that interface that allowed you to supply both capability property and default location information for a docking window.  Those types have been removed in the new version, but there are improved replacements for similar functionality.

Now that Docking/MDI is much more MVVM friendly, for property setting, all you do in the new version is bind properties in a docking window Style as seen in the [MVVM Features](../docking/mvvm-features.md) topic.

The specifying of default locations can now be done through the "Default Locations" features described in the [Lifecycle and Docking Management](../docking/docking-window-features/lifecycle-and-docking-management.md) topic.  Those powerful features give you full control over where a docking window will be located when it is first opened.

## Docking/MDI Complete Update Details

The list below is the entire detailed update log, including breaking changes.

### Layout

- Layout logic now does its best to adhere to desired, minimum, and maximum docked size hints on docking windows when space is available.
- DockSite.ControlSize attached property no longer used.  Set ContainerDockedSize property on docking windows instead.
- Minimum and maximum container docked sizes supported on docking windows.  Set to the same value to suggest fixed sizes.
- When a layout is sized very small, then grows larger again, the original docking window sizes are restored instead of keeping them minimal.
- The DockSite.UseHostedPopups property replaces UseHostedAutoHidePopups and UseHostedDockGuides.

### Splitters

- Live splitting enabled by default, but can be disabled.
- When not using live splitting and UseHostedPopups is false, splitters will appear above interop controls.

### Docking Windows

- Docking windows now are styled as simple presentation wrappers for their content that is displayed in UI, and no longer are styled to appear as tabs.  The new AdvancedTabControl is used in container templates that require tabbed UI.
- DockingWindow.WindowGroupName property added, which designates a group of windows that the window can attempt to attach to when first opened.
- DockingWindow.DefaultLocationRequested event added, which allows configutation of dynamic default location information for a window when it is first opened.
- DockSite.WindowDefaultLocationRequested event added, which provides generalized functionality similar to DockingWindow.DefaultLocationRequested event.
- DockingWindow.DefaultDockSide property added, which designates the fallback default side in the dock host to dock when first opened.
- When docking to a default side in a dock host, a window will attempt to attach to another existing container on the same side, if one is available.
- DockingWindow.TabText property added, which optionally provides specific text to only use on tabs.  If not specified, the Title is used in those locations.
- ToolWindow.GetCurrentSide method added that returns the side where the tool window is currently located and replaces the GetDirectionRelativeToWorkspace method.
- DockingWindow.IsActive property added, which is only true when the DockSite.ActiveWindow is the window.  Set it to true to activate the window.
- Various ICommand-based properties added to DockingWindow and ToolWindow, instead of the old generalized DockingCommands class.
- ToolWindow.Dock method changed to use Side parameter instead of Direction, and Attach method added for docking directly into a specified container (instead of former Direction.Content option).
- DockingWindow.CanDockLeft, CanDockTop, CanDockRight, CanDockBottom and related global DockSite/TabbedMdiHost properties (like DockSite.CanToolWindowsDockTop, TabbedMdiHost.CanDocumentsDockTop, etc.) removed and replaced with a single DockingWindow.CanDock property.  For restricting the sides upon which docking windows can dock during a drag operation, please see the "Limiting the Allowed Dock Guides While Dragging" section in the [Docking Window Capabilities](../docking/docking-window-features/docking-window-capabilities.md) topic.
- DockingWindow.CanRaft property renamed to CanFloat, and related DockSite.CanToolWindowsRaft property renamed to CanToolWindowsFloat.
- DockingWindow.TabFlashMode and TabFlashColor properties added, allowing for flash effects to be applied to a tab.
- DockingWindow.TabToolTip property added, which is what is now used to set the tooltip content for a docking window's tab.
- DockingWindow.SerializationId property added, which can be used to designate a unique ID for layout deserialization that matches layout data to a window in place of the Name property.
- DockingWindow.CanSerialize property added that can be set to false to prevent the window from being included in layout serialization.
- DockingWindow.ContextImageSource and DocumentWindow.ReadOnlyContextImageSource properties removed and replaced with a DockingWindow.TabbedMdiTabContextContentTemplate property that allows for more flexibility.
- DockingWindow.LastActiveDateTime property added, which can be used to sort docking windows by when they were last focused.
- DockingWindow.CanDrag property renamed to CanDragTab, and DockSite.CanToolWindowsDrag renamed to CanToolWindowTabsDrag and TabbedMdiHost.CanDocumentsDrag renamed to CanDocumentTabsDrag to match.
- DockingWindow.MoveToMdi method overload that targeted a TabbedMdiContainer removed.  Use the Attach or Dock methods instead.
- DockSite.LastActiveToolWindow property removed since it can be derived by looking at the open tool windows and sorting by their LastActiveDateTime properties.
- DockSite.WindowClosing event renamed to WindowsClosing and now can pass multiple e.Windows, which is useful when the Close All Documents and Close Others menu items are used since a single dialog with all windows listed can be presented to the user to check whether to close the windows.
- Several other DockSite events renamed so they now can pass multiple e.Windows, including WindowClosed to WindowsClosed, WindowDragging to WindowsDragging, WindowDragged to WindowsDragged, WindowOpening to WindowsOpening, WindowOpened to WindowsOpened, and WindowStateChanged to WindowsStateChanged.
- ToolWindow and DocumentWindow multi-parameter constructors changed to accept SerializationId parameter in place of Name to help avoid issues with having to conform Name's value to identifier syntax.
- Dockingwindow.TabBackground, TabBorderBrush, TabBorderThickness, TabForeground properties removed since they are no longer applicable.
- DockingWindow.LastState property removed since the State property can be used.
- DockingWindow.OptionsMenu property removed.  Handle the DockSite.MenuOpening event instead to change/customize the menu.
- DockingWindow.DragMove method updated to require an InputPointerButtonEventArgs argument.

### Tool Window Containers

- ToolWindowContainer uses the new AdvancedTabControl within its template, bringing all features of that control to the container's UI.
- Use the DockSite.ToolWindowsHaveOptionsButtons, ToolWindowsHaveToggleAutoHideButtons, and ToolWindowsHaveCloseButtons properties to determine if title bar buttons are visible, without affecting context menu options or capabilities.
- Use the ToolWindow.ToolWindowContainerTitleBarContextContentTemplate property to add custom buttons into the title bar.
- Add contextual UI to tool window tabs via the ToolWindow.ToolWindowContainerTabContextContentTemplate property.
- Customize the appearance of tool window container tabs via the DockSite.ToolWindowContainerTabItemContainerStyle property.
- DockSite.CanToolWindowsCloseOnMiddleClick property added, allowing for closing tool window tabs on middle-click.
- DockSite.ToolWindowsHaveImagesOnTabs property renamed to ToolWindowsHaveTabImages.
- DockSite.ToolWindowsTabPlacement property renamed to ToolWindowsTabStripPlacement and uses a different enum type, with new options for all sides.
- ToolWindowContainer.TitleFontFamily and TitleFontSize properties added, allowing for easy customization of title text appearance.
- ToolWindow.HasOptions property renamed to HasOptionsButton and DockSite.ToolWindowsHaveOptions property renamed to ToolWindowsHaveOptionsButtons.
- DockSite.ToolWindowTransitionSelector property removed since transitions are no longer supported to improve tab switching performance.
- Alt+- keyboard shortcut shows options menu.

### Auto-hide

- When UseHostedPopups is false, auto-hide popups now fully track with the dock host when crossing over screen edges.
- When not using live splitting and UseHostedPopups is false, splitters will now remain fully visible when dragged outside of the auto-hide popup.
- DockSite.AutoHidePopupOpenAnimationDuration and AutoHidePopupCloseAnimationDuration property defaults made slightly faster.
- DockSite.AutoHidePopupOpenAnimation and AutoHidePopupCloseAnimation properties removed. Auto-hide popups now always use a fade/slide animation when animation durations are greater than zero.
- Auto-hide popups now close when another docking window is activated instead of blindly watching for pointer clicks.
- DockSite.WindowsAutoHiding event added, which allows for customization of the side upon which one or more tool windows will auto-hide.
- Drag/drop of objects over an auto-hide tab opens the auto-hide popup.
- DockHost.AutoHidePopupToolWindow property added, which specifies the tool window currently open in an auto-hide popup, if any.
- DockHost.IsAutoHidePopupOpen property added, returning if the auto-hide popup is currently open.
- DockHost.CloseAutoHidePopup property added, allowing for the auto-hide popup to be closed instantly or with animation.
- Add contextual UI to auto-hidden tool window tabs via the ToolWindow.AutoHideTabContextContentTemplate property.
- Customize the appearance of auto-hide tabs via the DockSite.AutoHideTabItemTemplate and AutoHideTabItemTemplateSelector properties.
- Removed the DockSite.CanToolWindowsRestoreToAutoHideState property and now always restores to auto-hide state when appropriate.
- DockSite.AutoHideHost property removed and related functionality moved to DockSite/DockHost.
- DockSite auto-hide Duration-based properties changed to use a double representing milliseconds instead.

### Floating Windows

- Floating windows rearchitected to fully support Windows Aero snapping.
- DockSite.UseDragFloatPreviews property added that determines if float previews should be used when dragging windows instead of instantly creating floating windows.
- DockSite.FloatingWindowTitle and FloatingWindowIcon properties added, allowing for customization of floating window title bars.
- DockSite.FloatingToolWindowContainersHaveMaximizeButtons and FloatingToolWindowContainersHaveMinimizeButtons properties added, allowing for easy customization of whether maximize/minimize buttons appear on floating tool window containers.
- DockSite.UseHostedRaftingWindows property renamed to UseHostedFloatingWindows.
- DockSite.HostedRaftingWindowContainer property renamed to HostedFloatingWindowContainer.
- DockSite.IsRaftingWindowSnapToScreenEnabled property renamed to IsFloatingWindowSnapToScreenEnabled.
- DockSite.FloatingWindowSnapToScreenThreshold property added that determines what minimum amount a floating window must be visible to avoid being snapped to screen.  Partially-visible floating windows will now snap to this minimum threshold as appropriate.
- DockSite.FloatingWindowOpening event added that can alter and limit a floating window's initial size.
- The DockSite.CreateRaftingWindow method is no longer available.  Use other new properties to configure desired functionality.
- DockSite.CanRaftingWindowsCloseOnDockSiteUnload property renamed to CanFloatingDockHostsHideOnDockSiteUnload, and refactored logic for showing/hiding floating dock hosts based on DockSite visibility.
- Floating window wrappers now change close button visibility based on the CanCloseResolved property of all descendant docking windows.
- DockSite.InactiveRaftingWindowFadeDelay, InactiveRaftingWindowFadeDuration, InactiveRaftingWindowFadeOpacity, and IsInactiveRaftingWindowFadeEnabled properties renamed to use FloatingWindow in their names in place of RaftingWindow.
- DockSite.AreRaftingWindowsMaximizedOnDoubleClick property removed.
- DockingWindow.RaftingLocation property removed.  Use the Float method to specify a location instead.
- DockingWindow.Float overload that took a FloatSizingBehavior removed.  Set the DockingWindow.SizeToContentModes property with a Float value instead and then call Float() to have it size-to-content.
- DockSite.CanToolWindowsMaximize and CanDocumentWindowsMaximize properties, and DockingWindow.CanMaximize property removed due to not being appropriate for the new floating MDI support.  DockSite.FloatingToolWindowContainersHaveMaximizeButtons can be used as a replacement for all tool window containers.
- DockingWindowState enumeration no longer has a Floating value.

### Dock Guides and Previews

- Dock guides and previews restyled with a more modern appearance, and display with subtle animations.
- Animations can be disabled by setting DockSite.IsDockGuideAnimationEnabled to false.
- DockSite.DockGuideAnimationDuration property removed since animations are now forced to be quick.
- Dock guide control classes restructured and moved to the Primitives namespace.
- New DockPreview and FloatPreview controls added that show dock/float location hints in various scenarios.

### Tabbed MDI

- TabbedMdiContainer uses the new AdvancedTabControl within its template, bringing all features of that control to the container's UI.
- Three modes of tab display (normal, pinned, preview) are now available via the new DockingWindow.TabbedMdiLayoutKind property.
- New 'Close Others' and 'Close All Documents' context menu item.
- New 'Pin Tab' context menu toggle item to toggle between a pinned and normal tab.
- New 'Keep Tab Open' context menu item that converts a preview tab to a normal tab.
- The TabbedMdiHost.ContainersHaveNewTabButtons property can be set to true to show a new tab button. The DockSite.NewWindowRequested event can be handled in response to those button clicks.  New tab buttons work best when paired with a TabOverflowBehavior of Shrink or ShrinkWithMenu.
- TabbedMdiHost.EmptyContentTemplate property added that allows a custom DataTemplate to be displayed when there are no visible documents.
- TabbedMdiHost.TabPlacement property renamed to TabStripPlacement and uses a different enum type, with new options for all sides.
- TabbedMdiHost.IsCloseButtonOnTab property renamed to HasTabCloseButtons.
- TabbedMdiHost.IsImageOnTab property renamed to HasTabImages.
- TabbedMdiHost.HasTabPinButtons property added that can show a pin button on normal tabs.
- Alt+- keyboard shortcut shows selected document's context menu.
- Ctrl+Alt+DownArrow keyboard shortcut shows document list menu.
- Tab-based title editing features removed since they are rarely used and would add complexity to all UI tab items.
- TabbedMdiHost no longer inherits ContentControl so use its Child property to specify a child element instead.

### Standard MDI

- Dock guides now support attach docking over standard MDI host, which moves dragged docking windows to MDI.
- Context menu now available when clicking a standard MDI window icon.
- Context menu now available when right-clicking a standard MDI docking window title bar.
- Support for optional automatic size-to-content when adding a docking window to MDI via the new DockingWindow.SizeToContentModes property.
- Standard MDI window bounds is bound to new DockingWindow.StandardMdiBounds property.
- Standard MDI window state is bound to new DockingWindow.StandardMdiWindowState property.
- Add contextual UI to title bars via the DockingWindow.StandardMdiTitleBarContextContentTemplate property.
- StandardMdiHost.IsExternalMaximizedUIRequired property added that indicates when external maximized window button UI should be displayed, useful in scenarios when AreMaximizedWindowFramesVisible is false.

### MDI (Common)

- DockSite.MdiKind property added, allowing for easy switching of MDI kinds.  Use instead of old Workspace.SwitchToTabbedMdi and SwitchToStandardMdi methods.
- DockSite.MdiKindChanged event added.
- Added a new DockSite.SetMdiKindCommand property whose value can be wired up to menu items for easy MDI kind switching.
- Added new DockSite.CascadeDocumentsCommand, TileDocumentsHorizontallyCommand, and TileDocumentsVerticallyCommand properties whose values can be wired up to menu items for easy MDI layout adjustments.
- Added the DockSite.PrimaryDocument property and PrimaryDocumentChanged event, which replaces the LastActiveDocument property and LastActiveDocumentChanged event.
- Added several DockSite methods like ActivatePrimaryDocument, ActivateNextDocument, ActivatePreviousDocument, and ClosePrimaryDocument, along with related commands.

### MVVM

- DockingWindow.IsOpen property can be set to true via a view-model binding to open the DockingWindow container in the layout without necessarily activating it.
- DockingWindow.IsActive property can be set to true via a view-model binding to make the DockingWindow container active (focused).
- DockingWindow.IsSelected property can be set to true via a view-model binding to ensure the DockingWindow container is selected within its container.
- DockingWindow.State property setter added, which can be set via a view-model binding to alter the ToolWindow container's state (docked, auto-hide, or document).
- MVVM implementation improved to be more straightforward since docking windows are now the actual container of their content.
- When a docking window becomes unregistered, it will also attempt to remove the related item from the ToolItemsSource or DocumentItemsSource properties if they implement a non-read-only IList and DockSite.CanUpdateItemsSourceOnUnregister remains true.
- Newer, simplified samples for basic MVVM design without the need for an external behavior class as in the previous version.
- DockSite.ItemContainerRetentionMode property removed since it is no longer needed due to restructuring of DockingWindow control hierarchy.
- Docking windows can be defined in XAML and have their IsOpen property set to false to close them on initial dock site load, but leave behind a breadcrumb for future restoration.
- DockingWindowItemType enumeration renamed to DockingWindowItemKind.

### Switcher

- Reimplemented StandardSwitcher with a new, more modern user interface that animates on display and supports scrolling when there is overflow.
- Added the StandardSwitcher.HeaderTemplate and FooterTemplate properties that can be used to customize the header/footer presentation for the selected window.
- Added the StandardSwitcher.ItemContainerStyle and ItemTemplate properties that can be used to customize the appearance of window list items.  The new container class is StandardSwitcherItem.
- Added the StandardSwitcher.ScrollButtonStyle, ScrollUpButtonContentTemplate, and ScrollDownButtonContentTemplate properties that can be used to customize the appearance of scroll up/down buttons that display as needed.
- Added the StandardSwitcher.MaxRowCount property, that sets the maximum number of item rows that can be displayed.
- Renamed StandardSwitcher.MaxDocumentsColumns to MaxDocumentColumnCount.
- Moved the switcher controls into the main Docking namespace.

### WindowControl

- WindowControl.DragMove method now requires an input event args so that it can capture the related pointer referenced in the event args.
- WindowControl.RestoreBounds property renamed to RestoredBounds.
- WindowControl.WindowStyle property removed.

### Miscellaneous

- All UI control styles/templates rebuilt from scratch to be simpler and easier to customize.
- Quick subtle animations updated and added throughout the product to give it a more vibrant feel.
- Refactored context and drop-down menu customization design.  The old DockSite.WindowContextMenu event was renamed to MenuOpening.
- Refactored and centralized focus handling and setting logic.
- Refactored magnetism handling.
- DockSite.PrimaryDockHost and FloatingDockHosts properties added.
- DockSite.Child property should now be used to specify the child element, instead of Content, which has been removed.
- DockSite.Workspace property removed since floating dock hosts now support full MDI features.  Use DockSite.PrimaryDockHost.Workspace to access what used to be in DockSite.Workspace.
- DockSite.ToHierarchyString method added that returns a string containing a textual representation of the hierarchy.
- DockSite.DefaultControlSize and MinimumSlot properties removed.  Use the new DockingWindow.ContainerDockedSize and ContainerMinSize properties instead.
- DockSite.FullScreen property removed since it wouldn't work well with the new floating MDI features.
- SingleTabLayoutBehavior.Stretch property removed since another property may be added in the future to stretch all tabs when there is space, instead of only when there is one tab.
- ActiproSoftware.Windows.Controls.Docking.Preview and ActiproSoftware.Windows.Controls.Docking.Switching namespaces removed and their contents merged into other namespaces.
- The older Docking/MDI versions relied on inherited attached properties to register docking-related controls with the DockSite. This approach is no longer to used since use of inherited properties can affect performance. Now the registrations are done based on the proper control hierarchy being established, such as a MDI host being a direct child of a Workspace.
- Rewrote UIA to ensure better compatibility with Coded UI Test.

### AdvancedTabControl

- Standalone control that can be used independently of docking windows.
- Normal, pinned, and preview tabs.
- Minimum and maximum tab sizes; set the same to make fixed size tabs.
- Place tabs on any side.
- Use tab item properties to adjust tab brushes, side border slant (WPF only), and corner radius.
- Numerous overflow options.
- Animated overflow scrolling.
- Animated tab layout changes and tab insertions.
- Animated tab drag/drop support.
- New tab button, that can be optionally included and styled.
- Customizable embedded button appearances.
- Multiple tab highlight states including: none, pointer over, active selected, preview, preview pointer over, preview active selected, and inactive selected.
- Tabs can optionally always show embedded buttons.
- Options for tab close, pin, and promote support.
- Option to close tab on middle-click.
- Show any custom contextual content.
- Read-only flag can auto-set a read-only contextual content.
- Keyboard shortcuts (e.g., Ctrl+1) can directly access normal tabs by tab number.
- Keyboard shortcuts (e.g., Ctrl+Alt+1) can directly access pinned tabs by tab number.
- Optional Ctrl+Tab and Ctrl+Shift+Tab support to switch tabs.

## ActiproSoftware.Legacy.Wpf.dll No Longer Installed By Default

The very old ActiproSoftware.Legacy.Wpf.dll assembly that contains deprecated controls replaced by others is no longer installed by default.  We encourage you to port any code that used its controls to newer supported ones instead, as this assembly will cease to be included in the WPF Controls installer sometime in the future.

If you still need to use this assembly in the meantime, you must check the 'Control Assemblies / Legacy' option in the installer to install it.  Once you do so, the assembly will appear in a 'Legacy' subfolder of the Actipro assembly install folder (see the [Deployment](../deployment.md) topic for the default location), but will not appear in the GAC.

## Metro Themes Merged Into Shared Library

With the rise in popularity of Metro-like themes in general due to Windows 10/8.x adoption, we have merged the Metro themes from what used to be in the *ActiproSoftware.Themes.Metro.Wpf.dll* and placed them directly within the Shared Library.  There is no more *ActiproSoftware.Themes.Metro.Wpf.dll* assembly packaged with the WPF Controls.

Since they are located directly in the Shared Library now, Metro themes no longer need to be explicitly registered via a call to the `ThemesMetroThemeCatalogRegistrar.Register` method.

Also, now Windows 10 and 8.x systems will default to Metro Light theme unless otherwise overridden by setting the [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentTheme](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentTheme) property.  This ensures that a theme will be used on those operating systems that matches overall appearance.

To handle the breaking changes above:

- Remove any references to the *ActiproSoftware.Themes.Metro.Wpf.dll* assembly.
- Remove any calls to the `ThemesMetroThemeCatalogRegistrar.Register` method.
- Continue setting the [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentTheme](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentTheme) property to any Metro theme listed in the [Themes Getting Started](../themes/getting-started.md) topic as appropriate.  Or specifically set the current theme to another non-Metro theme if you always wish for a non-Metro theme to be used on Windows 10/8.x systems.

The updates for v16.1 also include multiple new Metro Light and Metro White accent themes that render similar to the Office 2016 apps.

## Luna/Royale Themes Split Into Separate Library

Now that Windows XP is past its Microsoft support lifetime, we have removed the XP-related themes (LunaNormalColor, LunaHomestead, LunaMetallic, and RoyaleNormalColor) from the Shared Library and placed them in a new *ActiproSoftware.Themes.Luna.Wpf.dll* assembly.

If you wish to use these deprecated XP-like themes in your application, you must perform these steps:

- Add a reference to the *ActiproSoftware.Themes.Luna.Wpf.dll* assembly.
- Call the `ThemesLunaThemeCatalogRegistrar.Register` method during app startup.
- Set the [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentTheme](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentTheme) property to an appropriate Luna theme.

It is recommended that most applications move to one of the Metro themes from Luna, since those provide a more modern appearance.

> [!NOTE]
> Now that the Luna themes are in a separate assembly, they will no longer be automatically applied to applications running on Windows XP when [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentTheme](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentTheme) hasn't been explicitly set.  Instead, the `Classic` theme will be invoked in Windows XP in that scenario.  You must write code in your application startup to see if the application is running on Windows XP, and then set [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentTheme](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentTheme) to the appropriate Luna theme.

## WindowChrome Updated to Only Allow Aero Glass on Windows 7 and Vista

In the previous version, Aero glass effects were allowed on Windows 8.x and 10.  However, in Windows 10, they don't render correctly.  Unfortunately .NET prevents you from detecting if your app is running in Windows 10, unless you specifically add a special *app.manifest*.  Since Aero glass isn't really used on Windows 8.x, we updated WindowChrome to only allow Aero glass to be enabled on Windows 7 and Vista, regardless of what the `WindowChrome.IsGlassEnabled` property value is.

## RibbonWindow.IsGlassEnabled Is No Longer Nullable and Defaults to False

With Metro themes becoming most popular with recent Windows versions and in an effort to have the [RibbonWindow](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow)'s Aero glass enabled state match that of [WindowChrome](xref:@ActiproUIRoot.Themes.WindowChrome), the `RibbonWindow.IsGlassEnabled` property is now a `bool` (instead of nullable `bool`) and defaults to `false`.

If your app uses a non-Metro theme (like `AeroNormalColor`) and you would like to continue using Aero glass on your [RibbonWindow](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow) instances, set the `RibbonWindow.IsGlassEnabled` property to `true`.  Note that even when that property is `true`, Aero glass will only be active on Windows 7 and Vista systems since Windows 8.x and later systems don't use Aero glass.

## Ribbon Button Images Auto-Convert To White Monochrome When Appropriate

Several Metro themes require that buttons in the QAT, tab panel, or status bar have white monochrome images.  New features have been added in the 16.1 version to automatically perform this conversion for you so that additional image sets aren't required for these usage scenarios.

See the [Monochrome Images](../ribbon/appearance-features/monochrome-images.md) topic for details on when this auto-conversion occurs.
