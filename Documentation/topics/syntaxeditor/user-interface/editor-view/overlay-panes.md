---
title: "Overlay Panes"
page-title: "Overlay Panes - SyntaxEditor Editor View Features"
order: 17
---
# Overlay Panes

Overlay panes are user interface elements that are displayed in the upper-right corner of an [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).  While any content can be displayed, most content will typically be relatively small and can host interactive controls.

The [Search Overlay Pane](../searching/search-overlay-pane.md) is an example of an overlay pane implementation.

## Creating an Overlay Pane

Any control can be an overlay pane, but it must implement the [IOverlayPane](xref:@ActiproUIRoot.Controls.SyntaxEditor.IOverlayPane) interface.

@if (wpf winforms) {

### Inherit OverlayPaneBase

When possible, inheriting a custom pane from [OverlayPaneBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase) will simplify the development effort. This abstract class has a default implementation for most members of the [IOverlayPane](xref:@ActiproUIRoot.Controls.SyntaxEditor.IOverlayPane) interface, and individual members can be overridden, as needed, to extend functionality for the custom pane.

}

@if (wpf) {

### Special Consideration for Key Presses

> [!WARNING]
> When typing in an overlay pane, key presses are still sent to the ancestor [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control and could result in edit actions being performed.

Pressing keys like <kbd>Tab</kbd> and <kbd>Shift</kbd>+<kbd>Tab</kbd>, which normally move focus, may result in text being indented or outdented in the editor.

To prevent these keys (or any other key combination) from reaching the `SyntaxEditor` control, override the overlay pane control's `OnKeyDown` method and set `KeyEventArgs.Handled = true`.

> [!TIP]
> The [OverlayPaneBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase) control has built-in support for handling key presses. By default, <kbd>Tab</kbd> and <kbd>Shift</kbd>+<kbd>Tab</kbd> will be handled and used to move keyboard focus. Override the [OnMoveFocus](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase.OnMoveFocus*) method to customize which elements receive focus.  Set [AllowTabKeyToMoveFocus](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase.AllowTabKeyToMoveFocus) = `false` to disable the automatic handling of these keys.
>
>To customize the handling of additional keys, override the [ProcessKeyDown](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase.ProcessKeyDown*) method and return `true` for any keys that were handled by the overlay pane.

}

@if (winforms) {

> [!TIP]
> The [OverlayPaneBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase) control has built-in support for handling key presses. By default, <kbd>Esc</kbd> will be handled and used to close the overlay pane but can be disabled by setting [AllowEscKeyToClose](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase.AllowEscKeyToClose) = `false`.
>
>To customize the handling of additional keys, override the [ProcessKeyDown](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.OverlayPaneBase.ProcessKeyDown*) method and return `true` for any keys that were handled by the overlay pane.  For example, the <kbd>Tab</kbd> and <kbd>Shift</kbd>+<kbd>Tab</kbd> keys can optionally be handled to keep keyboard focus within the pane.

}

## Control Key Down Opacity

Overlay panes can become semi-transparent when the <kbd>Ctrl</kbd> key is held down, thereby allowing the end user to see the text behind it. The [IOverlayPane](xref:@ActiproUIRoot.Controls.SyntaxEditor.IOverlayPane).[ControlKeyDownOpacity](xref:@ActiproUIRoot.Controls.SyntaxEditor.IOverlayPane.ControlKeyDownOpacity) property specifies the opacity when the popup is semi-transparent.

The property value `0.25` is recommended for transparency. Set this property to `1.0` to prevent the pane from becoming semi-transparent.

## Single or Multiple Views

The [IOverlayPane](xref:@ActiproUIRoot.Controls.SyntaxEditor.IOverlayPane).[InstanceKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.IOverlayPane.InstanceKind) property specifies how many instances of the overlay pane are permitted across all views of the same `SyntaxEditor` control.

Set this property to [Single](xref:@ActiproUIRoot.Controls.SyntaxEditor.OverlayPaneInstanceKind.Single) to allow only one instance at a time for any view in the editor.  Set this property to [SinglePerView](xref:@ActiproUIRoot.Controls.SyntaxEditor.OverlayPaneInstanceKind.SinglePerView) to allow one instance per view.

## Showing and Activating an Overlay Pane

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[OverlayPanes](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.OverlayPanes) collection is used to manage which overlay panes are currently shown for a particular view.

An overlay pane will be displayed when it is added to the [OverlayPanes](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.OverlayPanes) collection.  If an overlay pane of the same key is already displayed, the existing pane can typically be reused instead of creating a new one.

Most overlay panes are interactive and should be activated when they are shown.  Each overlay pane can provide its own implementation for what it means to be activated, but this will typically involve setting keyboard focus to a specific element on the pane.

A good practice is to define a static `Show` method on the class that defines an overlay pane. The following code example demonstrates how a `Show` method could be defined on a `CustomOverlayPane` class.


```csharp
/// <summary>
/// Shows the overlay pane within the specified <paramref name="view"/>.
/// </summary>
/// <param name="view">The editor view where the overlay pane will be displayed.</param>
public static void Show(IEditorView view) {
	if (view is null)
		throw new ArgumentNullException(nameof(view));

	// Get or create the custom overlay pane
	var overlayPanes = view.OverlayPanes;
	var customPane = overlayPanes[Key] as CustomOverlayPane;
	if (customPane is null) {
		// Close any existing overlay panes before adding a new one
		overlayPanes.Clear();

		// Add a new custom overlay pane
		customPane = new CustomOverlayPane(view);
		overlayPanes.Add(customPane);
	}

	// Activate the pane
	customPane.Activate();
}
```

> [!TIP]
> Use the [SyntaxEditor.ActiveView](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ActiveView) property to determine which editor view is currently active.