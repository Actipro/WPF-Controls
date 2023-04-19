---
title: "IntelliPrompt Quick Info Provider"
page-title: "IntelliPrompt Quick Info Provider - Provider Services - SyntaxEditor Language Creation Guide"
order: 8
---
# IntelliPrompt Quick Info Provider

Languages can choose to support [IntelliPrompt quick info](../../user-interface/intelliprompt/quick-info.md) popups that are automatically displayed when the pointer moves over text and/or other parts of the editor, such as margins.

## Basic Concepts

### Context Retrieval

The [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) defines two [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) method overloads, both of quick are expected to return a context object given a certain parameter.  One overload accepts an [IHitTestResult](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult) (from a pointer event) and the other overload accepts a zero-based text offset.

A context object can be anything, but it should be some custom type that gives enough information to provide to the [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.RequestSession*) method later and have that method be able to create a quick info session based on the context.  It also must implement value equality via an `Equals` method override.

Implementors of these methods should use code to examine the context of the hit test result or offset in the view.  This may involve token scanning and/or AST examination if available.  The context data (in a method declaration, in a type declaration, in a documentation comment, etc.) should be wrapped within the context object retuned by the [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) methods.

### How Contexts Are Used

Context objects should always be stored in the [IQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession).[Context](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.Context) property.  As pointer movements occur, a quick info provider repeatedly calls the [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) method with related pointer hit test results.  If the context of an open quick info session matches (value equality) the [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) return value, the provider knows that no new quick info session needs to be displayed.  If [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) returns a `null` value, then any existing open quick info session should be closed.  If [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) returns a context that doesn't match the existing session's context, or there is no existing open session, the [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.RequestSession*) method is called, passing along the new context to use.

> [!IMPORTANT]
> All context objects must implement an `Equals` method override so that value equality functions properly.

### Requesting a Session

The final method in the [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) interface is [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.RequestSession*).  This method is automatically called whenever a new context object has been retrieved that should be used to create and open a new quick info session.

Implementations of this method should create a new quick info session, store the passed context in its [Context](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.Context) property, build its content based on the data in the context object, and open the quick info session (show it).

## The QuickInfoProviderBase Base Class

The [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) interface implements [IEditorViewPointerInputEventSink](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewPointerInputEventSink) and thus is able to process pointer (e.g., pointer) input from within a SyntaxEditor.  There is some complicated code needed to do this processing, which is wrapped up in the handy abstract [QuickInfoProviderBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase) class.

The easiest way to implement an [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) for a language is to inherit the [QuickInfoProviderBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase) class.  Then just override its abstract members.

By default, its virtual [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) overload that takes an [IHitTestResult](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult) is designed to see if the hit is over a character in the text area and if so, call the other abstract [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) overload that accepts an offset.  This is done since most of the time, a quick info provider will be used for showing quick info popups related to text content.  To support quick info popups for other parts of the editor, override the [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) overload that takes an [IHitTestResult](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult).

A [QuickInfoProviderBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase)-based provider will automatically close a quick info session it created when appropriate.  However, to do this, it needs to know which `Type` of context objects can be returned by its [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*) methods.  The [ContextTypes](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase.ContextTypes) property must be implemented to provide this information.  For instance, if a quick info provider can return a context object of type `KeywordQuickInfoContext` from [GetContext](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider.GetContext*), then the [ContextTypes](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase.ContextTypes) should return the `KeywordQuickInfoContext` type.

## Registering with a Language

Any object that implements [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) can be associated with a syntax language by registering it as an [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) service on the language.

This code registers a quick info provider in the `quickInfoProvider` variable with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterService(quickInfoProvider);
```

## Ordering Providers

The [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) interface inherits the [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable) interface.  When multiple language services are registered that implement [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider), they can be ordered.  The highest priority provider will be used first.  If it indicates that a session was opened, no other providers will be used.  If it indicates that a session was not opened, the next provider is checked, and so on.  This allows for layering of multiple providers.

## Showing Quick Info Based on a Button Click

Many applications provide some sort of **Display Quick Info** button in their application toolbar.  When displaying quick info popups in this mode, pointer move/hover input should be ignored until the next quick info session is requested.

This code handles a button click event and shows how to properly focus the editor, get the current [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider), create a context based on the offset of the caret, and request a quick info session:

```csharp
private void OnShowQuickInfoButtonClick(object sender, RoutedEventArgs e) {
	// Focus the editor
	editor.Focus();

	// Get the IQuickInfoProvider that is registered with the language
	IQuickInfoProvider provider = editor.Document.Language.GetService<IQuickInfoProvider>();
	if (provider != null) {
		// Create a context
		object context = provider.GetContext(editor.ActiveView, editor.Caret.Offset);

		if (context != null) {
			// Request that a session is created based on the context, and disable pointer tracking since
			//   this request is initiated from a button click
			provider.RequestSession(editor.ActiveView, context, false);
		}
	}
}
```

## Built-In Quick Info Provider for Collapsed Outlining Nodes

When using the [code outlining](../../user-interface/outlining/outlining-general.md) features, sometimes the end user will collapse outlining nodes.  By default, nothing happens when the pointer is hovered over a collapsed outlining node adornment (normally a box with `"..."` in it).  However, there is a built-in [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) implementation called [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider) that watches for pointer hovers over collapsed regions and will automatically show a quick info tip with the contained content on pointer hovers.

To enable this feature simply register a [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider) instance as a service on your language.  Note that if you register other [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) services, make sure they are not handling scenarios that would normally be picked up by the [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider).  You could order them after the [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider) (see Ordering Providers section above) to ensure they occur after it is already checked.

This code registers the built-in [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider) service with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterService(new CollapsedRegionQuickInfoProvider());
```

The content displayed in the quick info tip will be syntax highlighted by default.  Set the [IsSyntaxHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider.IsSyntaxHighlightingEnabled) property to `false` to display plain text without syntax highlighting.

If you wish to customize the content of the popup from its default, create a class that inherits [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider) and override its [GetContent](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider.GetContent*) method.  That method is passed the source [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) and [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange), and returns the content to display for that snapshot range.  Then register an instance of this custom class as the service instead.

## Built-In Quick Info Provider for Squiggle Tags

When using the [parsing](../../text-parsing/parsing/index.md) framework and the [ParseErrorTagger](xref:ActiproSoftware.Text.Tagging.Implementation.ParseErrorTagger) (or other custom tagger that uses [SquiggleTag](xref:ActiproSoftware.Text.Tagging.Implementation.SquiggleTag) tags), there will be squiggle adornments in the document window. By default, nothing happens when the pointer is hovered over these adornments. However, there is a built-in [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) implementation called [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) that watches for pointer hovers over squiggle adorned regions and will automatically show a quick info tip with the parse error description on pointer hovers.

To enable this feature, simple register a [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) instance as a service on your language. Note that if you register other [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) services, make sure they are not handling scenarios that would normally be picked up by the [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider).  You could order them after the [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) (see Ordering Providers section above) to ensure they occur after it is already checked.

This code registers the built-in [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) service with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterService(new SquiggleTagQuickInfoProvider());
```

### Implementation Notes

[SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) works by looking for any tags that are at the text offset under the pointer when a pointer hover event is detected.  It uses an [ITagAggregator<T>](xref:ActiproSoftware.Text.Tagging.ITagAggregator`1) for tag type [ISquiggleTag](xref:ActiproSoftware.Text.Tagging.ISquiggleTag) that queries each appropriate tagger with a single snapshot range containing the target offset.

Since nothing within SyntaxEditor caches displayed squiggle tags, taggers must return at least one tag that contains the target offset for quick info to show.

Collection taggers are often a good choice for marking errors.  They work like a collection where you can add/remove tags like [SquiggleTag](xref:ActiproSoftware.Text.Tagging.Implementation.SquiggleTag).  In fact, our very own [ParseErrorTagger](xref:ActiproSoftware.Text.Tagging.Implementation.ParseErrorTagger) is a [CollectionTagger<T>](xref:ActiproSoftware.Text.Tagging.Implementation.CollectionTagger`1).  Collection taggers are described in detail in the [Taggers and Tagger Providers](../../text-parsing/tagging/taggers.md) topic and support the [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) requirements well.
