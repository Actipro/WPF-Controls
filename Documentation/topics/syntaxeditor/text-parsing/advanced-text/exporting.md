---
title: "Exporting to HTML / RTF"
page-title: "Exporting to HTML / RTF - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 5
---
# Exporting to HTML / RTF

Text from a snapshot can easily be exported to a `String` or file in various HTML and RTF (rich text) formats.

The [ITextExporter](xref:ActiproSoftware.Text.Exporters.ITextExporter) interface provides a unified model that supports exporting to any custom output format, although the text framework only includes built-in implementations for HTML and RTF.

## Built-in HTML Exporters

The text framework has four formats for exporting HTML, all available as methods on the [TextExporterFactory](xref:ActiproSoftware.Text.Exporters.TextExporterFactory) class:

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

[CreateHtmlClassBased](xref:ActiproSoftware.Text.Exporters.TextExporterFactory.CreateHtmlClassBased*) Method

</td>
<td>

Returns the [IHtmlTextExporter](xref:ActiproSoftware.Text.Exporters.IHtmlTextExporter) that can export text to `HTML`, using `CSS` classes for syntax highlighting.

`CSS` class references will be used for highlighting.  This type adds a `STYLE` tag block to the exported `HTML` where `CSS` classes are defined.  The text in the code block references these `CSS` classes, therefore allowing color settings to easily be altered for each highlighting style (i.e. keywords, comments, etc.).  Root `HTML`, `BODY` and other tags will be added to the output.

</td>
</tr>

<tr>
<td>

[CreateHtmlClipboard](xref:ActiproSoftware.Text.Exporters.TextExporterFactory.CreateHtmlClipboard*) Method

</td>
<td>

Returns the [IHtmlTextExporter](xref:ActiproSoftware.Text.Exporters.IHtmlTextExporter) that can export text to `HTML`, in a format that is compatible with the Windows clipboard HTML data format.

The output of this exporter is the same as [CreateHtmlInlineFragment](xref:ActiproSoftware.Text.Exporters.TextExporterFactory.CreateHtmlInlineFragment*), except that some additional information is added to make the exported text compatible with the Windows clipboard HTML data format.

</td>
</tr>

<tr>
<td>

[CreateHtmlInline](xref:ActiproSoftware.Text.Exporters.TextExporterFactory.CreateHtmlInline*) Method

</td>
<td>

Returns the [IHtmlTextExporter](xref:ActiproSoftware.Text.Exporters.IHtmlTextExporter) that can export text to `HTML`, using inline `CSS` for syntax highlighting.

Inline `CSS` styles will be used for highlighting.  This type allows for the code block to be directly copied out of the exported document and pasted into another.  Root `HTML`, `BODY` and other tags will be added to the output.

</td>
</tr>

<tr>
<td>

[CreateHtmlInlineFragment](xref:ActiproSoftware.Text.Exporters.TextExporterFactory.CreateHtmlInlineFragment*) Method

</td>
<td>

Returns the [IHtmlTextExporter](xref:ActiproSoftware.Text.Exporters.IHtmlTextExporter) that can export text to `HTML`, using inline `CSS` for syntax highlighting and no root `HTML` or `BODY` tags.

Inline `CSS` styles will be used for highlighting.  This type allows for the code block to be directly copied out of the exported document and pasted into another.  Root `HTML`, `BODY` and other tags will not be added to the output.

</td>
</tr>

</tbody>
</table>

The appropriate exporter should be used based on your HTML output needs.

## Built-in RTF Exporter

The [TextExporterFactory](xref:ActiproSoftware.Text.Exporters.TextExporterFactory) class also has a static [CreateRtf](xref:ActiproSoftware.Text.Exporters.TextExporterFactory.CreateRtf*) method for creating a RTF exporter.

## Using a Custom IHighlightingStyleRegistry

The constructor of the [TextExporterFactory](xref:ActiproSoftware.Text.Exporters.TextExporterFactory) class optionally takes an [IHighlightingStyleRegistry](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) argument.  If specified, that registry will be used for converting classification types to styles.  Otherwise, the [AmbientHighlightingStyleRegistry](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) will be used.

## Exporting to a String

Exporting to a string is done via the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[Export](xref:ActiproSoftware.Text.ITextSnapshot.Export*) method.

This code shows how to export the contents of an [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) to a string in RTF format:

```csharp
string text = document.CurrentSnapshot.Export(new TextExporterFactory().CreateRtf());
```

## Exporting to a File

Exporting to a file is done via the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[ExportToFile](xref:ActiproSoftware.Text.ITextSnapshot.ExportToFile*) method.

This code shows how to export the contents of an [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) to a file in RTF format:

```csharp
string path = @"C:\output.rtf";
document.CurrentSnapshot.ExportToFile(new TextExporterFactory().CreateRtf(), path);
```

## Exporting Selected Text in a SyntaxEditor View

Exporting to the selected text in a SyntaxEditor view to a string is done via the [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView).[ExportSelectedText](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView.ExportSelectedText*) method.

This code shows how to export the selected text in the active view to a string in RTF format:

```csharp
string text = editor.ActiveView.ExportSelectedText(new TextExporterFactory().CreateRtf());
```

## Exporting a Snapshot Text Range

The [ITextExporter](xref:ActiproSoftware.Text.Exporters.ITextExporter).[Export](xref:ActiproSoftware.Text.Exporters.ITextExporter.Export*) method allows for the exporting of any custom snapshot text range.

This code shows how to export a snapshot text range to a string in RTF format:

```csharp
string text = new TextExporterFactory().CreateRtf().Export(snapshot, new TextRange[] { textRange });
```
