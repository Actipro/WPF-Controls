---
title: "Edit Actions"
page-title: "Edit Actions - SyntaxEditor Input/Output Features"
order: 5
---
# Edit Actions

Edit actions are classes that implement [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction) and contain code to perform different simple tasks related to a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).  They are effectively commands with can-execute and executed handlers that perform all the work.

## Edit Actions and Commands

SyntaxEditor includes over 100 unique edit actions, covering everything such as movements, selection types, clipboard operations, and more.

The built-in edit actions are all located in the [ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions) namespace.

All commands related to the built-in edit actions are available via static properties on the [EditorCommands](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands) class.

## Executing Edit Actions

To execute an edit action, use the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[ExecuteEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.ExecuteEditAction*) method.  Simply pass it an instance of the [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction) to execute.

This code shows how to execute an [IndentAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.IndentAction) on the active view:

```csharp
editor.ActiveView.ExecuteEditAction(new IndentAction());
```

Most of the edit action classes defined by SyntaxEditor also have helper methods which make running the edit actions easier.  For instance, the [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection) interface has a method [SelectAll](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.SelectAll*), which runs the [SelectAllAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectAllAction).  Another example is the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) interface has a method [CopyToClipboard](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.CopyToClipboard*), which runs the [CopyToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CopyToClipboardAction).

## Canceling an Edit Action

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[ExecuteEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.ExecuteEditAction*) method immediately raises the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ViewActionExecuting](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ViewActionExecuting) event, which is passed an [EditActionEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActionEventArgs) that specifies the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) and [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction) being executed.  The event args has a `Cancel` property that can be set to `true` to prevent the edit action from executing.  This provides a way for certain edit actions to be externally filtered out if desired.

## Creating a Custom Edit Action

Custom edit action classes may be defined.  The only requirement is that they implement [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction).  When defining an edit action, it's easiest to inherit [EditActionBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.EditActionBase).

This code shows how to define an edit action that surrounds the selected text with `custom` tags.

```csharp
/// <summary>
/// Provides a custom <see cref="IEditAction"/> implementation that inserts a
/// <c>custom</c> tag surrounding the selected text.
/// </summary>
public class CustomAction : ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation.EditActionBase {

	/// <summary>
	/// Initializes an instance of the <c>CustomAction</c> class.
	/// </summary>
	public CustomAction() : base("Custom") {}

	/// <summary>
	/// Executes the edit action in the specified <see cref="IEditorView"/>.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> in which to execute the edit action.</param>
	public override void Execute(IEditorView view) {
		view.InsertSurroundingText("<custom>", "</custom>");
	}

}
```

@if (wpf) {

## Binding New Edit Actions to Keys

Although there are many [default key bindings](default-key-bindings.md) for edit actions in SyntaxEditor, it's easy to add new ones.

Binding of edit actions to a keyboard shortcut is done in three steps.  In the following example, we will bind the `CustomAction` edit action defined above to the <kbd>Ctrl</kbd>+<kbd>R</kbd> keyboard shortcut.

### Create a Static Command

For ease in implementation, the [EditActionBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.EditActionBase) type (base class for edit actions) implements `ICommand`.  SyntaxEditor's built-in commands are defined as static properties on a static class named [EditorCommands](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands).  This allows a single edit action instance (like [SelectAllAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectAllAction)) to be reused anywhere a command or input binding for that edit action is needed.  Therefore, it is recommended that custom edit actions also create a static instance that can be used the same way.

### Add a Command Binding

Second, we need to create a command binding so that SyntaxEditor can associate a command with executing the `CustomAction` edit action's logic. [EditActionBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.EditActionBase) defines a handy [CreateCommandBinding](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.EditActionBase.CreateCommandBinding*) method that generates a command binding.

Assume that a `CustomAction` instance is stored on a static `MyStaticCommands.Custom` property.  This code binds the edit action to the editor:

```csharp
editor.CommandBindings.Add(MyStaticCommands.Custom.CreateCommandBinding());
```

### Add an Input Binding

Finally, add an input binding so that a keyboard shortcut (<kbd>Ctrl</kbd>+<kbd>R</kbd>) will trigger the edit action:

```csharp
editor.InputBindings.Add(new KeyBinding(MyStaticCommands.Custom, Key.R, ModifierKeys.Control));
```

}

@if (wpf) {

## Binding Built-In Edit Actions to Other Keys

Changing or adding keyboard shortcuts to the built-in edit actions is easier.  In that case it is only a matter of updating the input bindings (see above) since the [EditorCommands](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands) already defines commands for the built-in edit actions and the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control has handlers for all those commands.

This code makes <kbd>Ctrl</kbd>+<kbd>R</kbd> execute the copy to clipboard edit action:

```csharp
editor.InputBindings.Add(new KeyBinding(EditorCommands.CopyToClipboard, Key.R, ModifierKeys.Control));
```

}
