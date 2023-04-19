---
title: "Context"
page-title: "Context - C# Language - SyntaxEditor .NET Languages Add-on"
order: 6
---
# Context

The C# language can return detailed context information about any offset in a document.  The context includes data such as containing AST node, target expression, and more.  This sort of information is essential in passing to the resolver to determine what to display in automated IntelliPrompt.

## Obtaining Context Information

Context information can be retrieved via the use of the [CSharpContextFactory](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpContextFactory) class.  Its [CreateContext](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpContextFactory.CreateContext*) method accepts a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and returns the [IDotNetContext](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext) for that offset.

This code retrieves an [IDotNetContext](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext) for the caret's offset (the end selection offset in the active view):

```csharp
TextSnapshotOffset snapshotOffset = syntaxEditor.ActiveView.Selection.EndSnapshotOffset;
IDotNetContext context = new CSharpContextFactory().CreateContext(snapshotOffset);
```

A context kind (via the [DotNetContextKind](xref:ActiproSoftware.Text.Languages.DotNet.DotNetContextKind) enumeration) can also be passed to a [CreateContext](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpContextFactory.CreateContext*) method overload.  The default value is `Self`, but you can also pass `SelfAndSiblings` to indicate that alternate logic should be used when building the context.  When `SelfAndSiblings` is passed, the context factory will return a context object that focuses on the "parent" of the offset.  This allows resolver operations that use the context to enumerate a list of accessible members.

## .NET Context

[IDotNetContext](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext) objects provide .NET language-related context information for a specific snapshot offset, such as the containing AST node, target expression, and more.

These [IDotNetContext](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext) members are important:

| Member | Description |
|-----|-----|
| [ContainingAstNode](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.ContainingAstNode) Property | Gets the [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) that contains the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset). |
| [ContainingAstTypeDeclaration](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.ContainingAstTypeDeclaration) Property | Gets the AST [TypeDeclaration](xref:ActiproSoftware.Text.Languages.DotNet.Ast.Implementation.TypeDeclaration) that contains the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset), if any. |
| [InitializationSnapshotRange](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.InitializationSnapshotRange) Property | Gets the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) with which the context was initialized. |
| [ProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.ProjectAssembly) Property | Gets the [IProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly) with which this context is associated. |
| [SnapshotOffset](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.SnapshotOffset) Property | Gets the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) for which this context was created. |
| [TargetExpression](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.TargetExpression) Property | Gets the target AST [Expression](xref:ActiproSoftware.Text.Languages.DotNet.Ast.Implementation.Expression) for the context, if known. |
