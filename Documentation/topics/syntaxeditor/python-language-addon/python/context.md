---
title: "Context"
page-title: "Context - SyntaxEditor Python Language Add-on"
order: 6
---
# Context

The Python language can return detailed context information about any offset in a document.  The context includes data such as containing AST node, target expression, and more.  This sort of information is essential in passing to the resolver to determine what to display in automated IntelliPrompt.

## Obtaining Context Information

Context information can be retrieved via the use of the [PythonContextFactory](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonContextFactory) class.  Its [CreateContext](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonContextFactory.CreateContext*) method accepts a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and returns the [IPythonContext](xref:ActiproSoftware.Text.Languages.Python.IPythonContext) for that offset.

This code retrieves an [IPythonContext](xref:ActiproSoftware.Text.Languages.Python.IPythonContext) for the caret's offset (the end selection offset in the active view):

```csharp
TextSnapshotOffset snapshotOffset = syntaxEditor.ActiveView.Selection.EndSnapshotOffset;
IPythonContext context = new PythonContextFactory().CreateContext(snapshotOffset);
```

A context kind (via the [PythonContextKind](xref:ActiproSoftware.Text.Languages.Python.PythonContextKind) enumeration) can also be passed to a [CreateContext](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonContextFactory.CreateContext*) method overload.  The default value is `Self` but you can also pass `SelfAndSiblings` to indicate that alternate logic should be used when building the context.  When `SelfAndSiblings` is passed, the context factory will return a context object that focuses on the "parent" of the offset.  This allows resolver operations that use the context to enumerate a list of accessible members.

## .NET Context

[IPythonContext](xref:ActiproSoftware.Text.Languages.Python.IPythonContext) objects provide .NET language-related context information for a specific snapshot offset, such as the containing AST node, target expression, and more.

These [IPythonContext](xref:ActiproSoftware.Text.Languages.Python.IPythonContext) members are important:

| Member | Description |
|-----|-----|
| [ContainingAstNode](xref:ActiproSoftware.Text.Languages.Python.IPythonContext.ContainingAstNode) Property | Gets the [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) that contains the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset). |
| [InitializationSnapshotRange](xref:ActiproSoftware.Text.Languages.Python.IPythonContext.InitializationSnapshotRange) Property | Gets the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) with which the context was initialized. |
| [Project](xref:ActiproSoftware.Text.Languages.Python.IPythonContext.Project) Property | Gets the [IProject](xref:ActiproSoftware.Text.Languages.Python.Reflection.IProject) with which this context is associated. |
| [SnapshotOffset](xref:ActiproSoftware.Text.Languages.Python.IPythonContext.SnapshotOffset) Property | Gets the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) for which this context was created. |
| [TargetExpression](xref:ActiproSoftware.Text.Languages.Python.IPythonContext.TargetExpression) Property | Gets the target AST [Expression](xref:ActiproSoftware.Text.Languages.Python.Ast.Implementation.Expression) for the context, if known. |
