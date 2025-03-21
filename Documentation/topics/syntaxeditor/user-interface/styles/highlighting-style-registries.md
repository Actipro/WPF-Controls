---
title: "Highlighting Style Registries"
page-title: "Highlighting Style Registries - SyntaxEditor Theme and Highlighting Style Features"
order: 3
---
# Highlighting Style Registries

Highlighting style registries provide a mapping from classification types to highlighting styles.  They tell SyntaxEditor how the various ranges of classified text should appear within the text area.

## Basic Concepts

A [classification type](../../text-parsing/tagging/basic-concepts.md) (which could represent an identifier, comment, etc.) is a logical category that can be assigned to ranges of text via a [tagger](../../text-parsing/tagging/taggers.md) for the [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) type.  Classifications are made within the [text/parsing framework](../../text-parsing/index.md) and therefore are not tied to the user interface.

A highlighting style registry takes these classification types and maps them to highlighting styles.  Highlighting styles define formatting options that should be applied to text.  Therefore, via the use of highlighting style registries allows the editor to determine how classified text should be rendered.

Highlighting style registries are implementations of the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) interface.  The [HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleRegistry) class implements this interface.

## The Ambient Registry

The ambient registry is a static registry that is always available and is generally what is used by the editor.  It is available via the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) class.

This code shows how to get an instance of the ambient registry:

```csharp
IHighlightingStyleRegistry registry = AmbientHighlightingStyleRegistry.Instance;
```

## Registering and Unregistering Entries

A registry entry consists of an [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) and [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) pair.

Registrations can be made by calling the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[Register](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.Register*) method.  Registrations can be removed by calling the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[Unregister](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.Unregister*) method.

This code registers a highlighting style in the `style` variable for the common `Comment` classification type in the ambient registry:

```csharp
AmbientHighlightingStyleRegistry.Instance.Register(ClassificationTypes.Comment, style);
```

An overload for the [Register](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.Register*) method indicates whether any existing entry for the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) should be overwritten.  The overload in the sample code above will not override an existing entry if one is found.

## Enumerating Classification Types and Highlighting Styles

The [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[ClassificationTypes](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.ClassificationTypes) property returns the collection of [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) objects that are currently registered.

The [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[HighlightingStyles](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.HighlightingStyles) property returns the collection of [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) objects that are currently registered.

## Classification Type Sort Order

The [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[ClassificationTypes](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.ClassificationTypes) collection is sorted in a special order.  This allows you to be able to bind the collection to a list in an application options dialog.

The base sorting is performed on the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType).[Description](xref:ActiproSoftware.Text.IClassificationType.Description) property.  If a [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) implements [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable), it is sorted before any [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) that doesn't implement [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable).  The [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable) implementation can be used to indicate the keys of other [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) after which the classification type should be sorted.

## Special Classification Types (Plain Text, Line Numbers, Indicator Margin, etc.)

There are several classification types that have special meaning for SyntaxEditor, all of which are available as properties on the [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) class.  These classification types are special cased to allow for end user customization of things such as the default text format, line number margin, indicator margin, visible whitespace, etc.

The classification types on [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) are not used unless they are registered.

Registration into the ambient highlighting style registry can be performed with this code:

```csharp
new DisplayItemClassificationTypeProvider().RegisterAll();
```

These are the special [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) properties on [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider):

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[BreakpointDisabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.BreakpointDisabled) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for a disabled breakpoint.

Only the border is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[BreakpointEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.BreakpointEnabled) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for an enabled breakpoint.

Only the foreground and background are editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CaretPrimary](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CaretPrimary) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the primary caret when multiple carets are active.

Only the foreground is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CaretSecondary](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CaretSecondary) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the secondary caret when multiple carets are active.

Only the foreground is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CodeSnippetDependentField](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CodeSnippetDependentField) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for a code snippet dependent field.

Only the border is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CodeSnippetField](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CodeSnippetField) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for a code snippet field.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CollapsedText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CollapsedText) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for collapsed text.

Only the foreground is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CollapsibleRegion](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CollapsibleRegion) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for collapsible regions.

Only the foreground and background are editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[ColumnGuides](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.ColumnGuides) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for column guides when [IColumnGuide](xref:@ActiproUIRoot.Controls.SyntaxEditor.IColumnGuide).[Color](xref:@ActiproUIRoot.Controls.SyntaxEditor.IColumnGuide.Color) is not explicitly set.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CurrentLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CurrentLine) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the current line highlight.

Only the background and border are editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[CurrentStatement](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CurrentStatement) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for a current statement.

Only the foreground and background are editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[DelimiterMatching](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.DelimiterMatching) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the delimiter matching highlight.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[FindMatchHighlight](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.FindMatchHighlight) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for a find match highlight.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[FindScopeHighlight](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.FindScopeHighlight) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for a find scope highlight.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[InactiveSelectedText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.InactiveSelectedText) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the selected text background when the view doesn't have focus.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[IndentationGuides](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.IndentationGuides) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for indentation guides.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[IndicatorMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.IndicatorMargin) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the indicator margin.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[LineNumberCurrent](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.LineNumberCurrent) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the current line in the line number margin when [IsCurrentLineNumberHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsCurrentLineNumberHighlightingEnabled) is active.

</td>
</tr>

<tr>
<td>

[LineNumbers](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.LineNumbers) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the line number margin.

</td>
</tr>

<tr>
<td>

[OutliningMarginSquare](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.OutliningMarginSquare) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for outlining margin squares.

Only the foreground and background are editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[OutliningMarginVerticalRule](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.OutliningMarginVerticalRule) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for outlining margin vertical rules.

Only the foreground is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[PlainText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.PlainText) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for default text in the text area.

Only the foreground and background are editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[RevertedChangesMark](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.RevertedChangesMark) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for reverted changes marks.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[SavedChangesMark](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.SavedChangesMark) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for saved changes marks.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[SelectedText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.SelectedText) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for the selected text background when the view has focus.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[UnsavedChangesMark](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.UnsavedChangesMark) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for unsaved changes marks.

Only the background is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[VisibleWhitespace](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.VisibleWhitespace) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for visible whitespace.

Only the foreground is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

<tr>
<td>

[WordWrapGlyph](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.WordWrapGlyph) Property

</td>
<td>

Gets the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) to use for word wrap glyphs.

Only the foreground is editable in the default [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) that is registered for this classification type.

</td>
</tr>

</tbody>
</table>

## Highlighting Styles Options Dialog

Please see the samples for a QuickStart on how to create a highlighting styles options dialog that allows the end user to customize all aspects of the editor's display.

## Importing Visual Studio Settings

The [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) allows for importing of Visual Studio settings files (*\*.vssettings*) to theme SyntaxEditor.

![Screenshot](../../images/vs-settings.png)

*SyntaxEditor after importing some custom Visual Studio settings*

You can see in the screenshot above that everything from basic text to the line number margin is themed.  This is all done using the highlighting style registry and the special classification types in [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) (see above).  All of these colors, etc. can be configured by the end user as well.

This code imports a Visual Studio settings file (pre-loaded in a `Stream` variable named `stream`) to theme the control:

```csharp
AmbientHighlightingStyleRegistry.Instance.ImportHighlightingStyles(stream);
```

## Defining and Using an Alternate Default Registry

In most cases, the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) is the only registry you need to use.  However, in some scenarios, you may wish to support more than one highlighting style registry, for instance one for text editors and one for console windows.  Each registry can have different style settings for its known classification types.  In these scenarios, you can use the ambient registry for text editors and can create a custom [HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleRegistry) to use for console windows.  In fact, any number of custom registries can be created as needed.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.HighlightingStyleRegistry) property is used to indicate which [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) to use when mapping classification types to [highlighting styles](highlighting-styles.md).  If the property is left unassigned, then the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) is used.

Back to the example scenario, for any [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) that will be used as a console window, you would assign the custom [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) for console windows to that editor instance.

This code creates a custom registry and assigns it to a console window SyntaxEditor:

```csharp
// Create a custom registry
IHighlightingStyleRegistry consoleWindowRegistry = new HighlightingStyleRegistry();
consoleWindowRegistry.Description = "Console Window";

// Optionally manage the registry for automatic changes in light/dark themes
SyntaxEditorThemeManager.Manage(consoleWindowRegistry);

// Configure the editor to use the custom registry
SyntaxEditor console = new SyntaxEditor();
console.HighlightingStyleRegistry = consoleWindowRegistry;
```

You must populate the entries into the custom registry.

Note that the [SyntaxLanguageDefinitionSerializer](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer) class has a [HighlightingStyleRegistry](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.HighlightingStyleRegistry) property where the registry it should use can be specified.  A [TextExporterFactory](xref:ActiproSoftware.Text.Exporters.TextExporterFactory) (used when [exporting text](../../text-parsing/advanced-text/exporting.md)) constructor overload also accepts a custom [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).  In all cases, if no custom registry is specified, the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) is used.

Another alternate registry can be designated for use in printer output only.  See the [Print Options](../printing/print-options.md) topic for more information.

## Classification Tags Providing an Alternate Registry

Although most of the time, the classification types used by classification taggers are registered into the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) instance used by the editor, there are cases where you may wish to not add certain classification types into that registry.

One possible scenario is if you wish to have some classification types that should not be included in the list of classification types that are configurable by the end user.  Another is if your classification tagger just keeps its types private.

In these scenarios, it's possible to have an [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) specify an alternate [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) to use.  This is done by having the [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) implement the [IHighlightingStyleRegistryProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistryProvider) interface.  The classification tag returns the custom registry via the [HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistryProvider.HighlightingStyleRegistry) property.  Classification taggers can use the [StyleRegistryClassificationTag](xref:ActiproSoftware.Text.Tagging.Implementation.StyleRegistryClassificationTag) class for this purpose.  When the normal highlighting style registry is fine for use, use the smaller [ClassificationTag](xref:ActiproSoftware.Text.Tagging.Implementation.ClassificationTag) class instead.

## Switching to a Dark Theme

See the [Dark Themes](dark-themes.md) topic for details on how to support switching to a dark theme.