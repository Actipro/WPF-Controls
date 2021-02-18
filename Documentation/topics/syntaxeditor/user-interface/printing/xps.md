---
title: "Printing to XPS"
page-title: "Printing to XPS - SyntaxEditor Printing Features"
order: 5
---
# Printing to XPS

Any SyntaxEditor document content can be printed out to XPS format with a few lines of code.

## What is XPS?

XPS means XML Paper Specification and is a royalty-free document format created by Microsoft to compete with PDF.  It is vector-based and supports device and resolution independence.

WPF has built-in support for XPS via several of its controls and classes in the System.Printing assembly.

## Sample Code

This code prints the editor's content to an XPS file:

```csharp
FixedDocument document = editor.PrintSettings.CreateFixedDocument(editor);
XpsDocument xpsd = new XpsDocument(dialog.FileName, FileAccess.Write);
XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
xw.Write(document);
xpsd.Close();
```

Note that it first creates a standard WPF `FixedDocument` and then uses that to write out to an XPS file.
