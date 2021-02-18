---
title: "Data Binding"
page-title: "Data Binding - SyntaxEditor Input/Output Features"
order: 8
---
# Data Binding

SyntaxEditor has the capability of allowing two-way binding of its text to any other data source.  This feature is disabled by default for performance reasons, but can be enabled via a property setting.

## Why is Data Binding Disabled by Default?

Data binding to the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[Text](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.Text) property is disabled by default for several reasons.

First, supporting the [Text](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.Text) dependency property means that two full copies of the document text are stored in memory instead of one, which can increase memory a lot for large documents.

Second, the full text must be set to the [Text](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.Text) property after each text change, meaning performance could suffer depending on the size of the document.

Therefore, databinding is disabled by default.

## Configuring Data Binding on Text

To enable data binding on the [Text](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.Text) property, simply set the [IsTextDataBindingEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsTextDataBindingEnabled) property to `true`.

When that property is `true`, the [Text](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.Text) property is updated when changes are made to the document, and thus binding to other objects can be achieved.

This code demonstrates how to bind the SyntaxEditor's text to another TextBox:

```xaml
<editor:SyntaxEditor AreLineModificationMarksVisible="False" 
					 IsTextDataBindingEnabled="True"
					 Text="{Binding ElementName=boundTextBox, Path=Text, Mode=TwoWay}" />
```

Since data binding replaces the complete text of the document with each change in the data source, it is best to set the [AreLineModificationMarksVisible](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.AreLineModificationMarksVisible) property to `false`.
