---
title: "Macro Recording and Playback"
page-title: "Macro Recording and Playback - SyntaxEditor Input/Output Features"
order: 7
---
# Macro Recording and Playback

SyntaxEditor has full macro recording and playback capabilities.  It can record any [edit action](edit-actions.md) that is executed and can play back the set of edit actions at a later time.  During macro recording mode, mouse input to the editor is ignored.

## Controlling Macro Recording

All macro recording functionality is accessible via the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[MacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MacroRecording) property.  The [IMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording) interface has a number of important members:

| Member | Description |
|-----|-----|
| [Cancel](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Cancel*) Method | Cancels recording a macro.  When macro recording is cancelled, the old value of the [LastMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.LastMacroAction) property is not overwritten. |
| [CurrentMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.CurrentMacroAction) Property | Gets the [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) that is currently being recorded.  This method returns `null` if no macro recording is currently taking place. |
| [LastMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.LastMacroAction) Property | Gets the last [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) that was recorded.  After macro recording is stopped, this property contains what was recorded. |
| [Pause](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Pause*) Method | Pauses recording a macro.  Use the [Resume](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Resume*) method to resume recording. |
| [Resume](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Resume*) Method | Resumes recording a macro from a paused state.  This method should be called after a [Pause](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Pause*) to resume recording. |
| [Record](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Record*) Method | Starts recording a new macro.  Use the [Stop](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Stop*) method to end recording and save the macro that was recorded. |
| [State](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.State) Property | Gets a [MacroRecordingState](xref:@ActiproUIRoot.Controls.SyntaxEditor.MacroRecordingState) indicating the current state of macro recording. |
| [Stop](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.Stop*) Method | Stops recording a macro.  The [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) that is recorded is stored in the [LastMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.LastMacroAction) property.  If no commands were recorded during this recording session, the old value of the [LastMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.LastMacroAction) property will not be overwritten. |

All of the methods above raise the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[MacroRecordingStateChanged](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MacroRecordingStateChanged) event.

## IMacroAction

Once a macro has been recorded, it is stored in a [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction)-based object.  The [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) interface is an [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction) itself, with [MacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MacroAction) being the default implementation of the interface.  It and the other macro-related edit actions override the [CanRecordInMacro](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction.CanRecordInMacro) property to prevent them from being recorded in other macros.

To play back a [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction), execute code like this:

```csharp
editor.ActiveView.ExecuteEditAction(macroAction);
```

The [MacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MacroAction) class is an enumerable class of child [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction) objects.

## Adding Macro Edit Action Key Bindings

SyntaxEditor ships with several [edit actions](edit-actions.md) that can be added as key bindings to the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).`InputBindings` collection:

| Command | Description |
|-----|-----|
| [CancelMacroRecordingAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CancelMacroRecordingAction) | Cancels recording a macro. |
| [PauseResumeMacroRecordingAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.PauseResumeMacroRecordingAction) | Pauses or resumes recording a macro, depending on the current state of macro recording. |
| [RunMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RunMacroAction) | Runs the macro that was last recorded. |
| [ToggleMacroRecordingAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ToggleMacroRecordingAction) | Starts or stops recording a macro, depending on the current state of macro recording. |

By default, these edit actions are not added to the [default key bindings](default-key-bindings.md).

## Macro Commands

The [EditorCommands](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands) class has a number of static properties containing commands that are handled by [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).

These commands are related to macros and can be used by toolbar buttons, etc. to control macro recording:

- [CancelMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands.CancelMacroRecording)
- [PauseResumeMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands.PauseResumeMacroRecording)
- [RunMacro](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands.RunMacro)
- [ToggleMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands.ToggleMacroRecording)

The commands use the related edit actions described in the previous section.

## Serializing and Deserializing MacroActions to XML

SyntaxEditor can serialize a macro to XML by using the [MacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MacroAction).[WriteToXml](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MacroAction.WriteToXml*) method.  To deserialize a macro later, create an instance of the [MacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MacroAction) and call its [ReadFromXml](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MacroAction.ReadFromXml*) method.  All the child edit actions of the macro should load into the macro so that it is ready to be run.

## Injecting Custom Macro Action Implementations

It is possible to use your own custom [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) implementation when you wish to create even more advanced macro functionality.  This could be done in scenarios where you wish to implement additional functionality like conditionals, looping, or nesting.  The first step is to build out a custom class that implements [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction).

Next, create a class that implements [IMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording).  This [IMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording) object is responsible for managing macro recording within the editor and also creates a new [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) when recording begins.  The easiest way to do this is to inherit our default [MacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.MacroRecording) class and override its virtual [CreateMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.MacroRecording.CreateMacroAction*) method to create your custom class that implements [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction).  The [NotifyEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording.NotifyEditAction*) method is called whenever an [IEditAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditAction) is about to execute in a view, allowing it to be recorded if appropriate.

The final step is to override the virtual [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CreateMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CreateMacroRecording*) and return an instance of your custom [IMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording) class.

Now that your custom [IMacroRecording](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroRecording) and [IMacroAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.IMacroAction) objects are in use, you have full control over the macro recording process.
