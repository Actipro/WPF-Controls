---
title: "Resolver"
page-title: "Resolver - SyntaxEditor .NET Languages Add-on"
order: 5
---
# Resolver

The resolver is an object that examines an expression and its containing contextual information.  It uses language-specific rules to investigate what is returned by the expression, such as a namespace, type, member, variable, etc.  This information is often passed to automated IntelliPrompt for display.

## Obtaining a Resolver

The resolver for a language, represented by the [IResolver](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolver) interface, can be obtained from the [IProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly).[Resolver](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly.Resolver) property.  As described in the [Assemblies](assemblies.md) topic, an [IProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly) is registered as a service on the .NET syntax languages in this add-on.

## Creating a Request

The first step in using the resolver is to create an [IResolverRequest](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverRequest).  An implementation of this interface is in the [ResolverRequest](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.Implementation.ResolverRequest) This class can accept an AST [Expression](xref:ActiproSoftware.Text.Languages.DotNet.Ast.Implementation.Expression) or a qualified name as parameters into its constructor.  The argument passed in will be what is resolved by the resolver.

The request's [IResolverRequest](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverRequest).[Context](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverRequest.Context) property can be set to an [IDotNetContext](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext) object to provide contextual information about the request.  This is very useful information for the resolver.  Say that a simple name `foo` is trying to be resolved.  If the context of the request occurs within a method, the resolver will first look for variables named `foo` within the method, then parameters, etc.  Context objects can be retrieved via the information in the [C# Context](csharp/context.md) and [Visual Basic Context](vb/context.md) topics.

## Performing a Resolve Operation

The resolver has a [Resolve](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolver.Resolve*) method that accepts an [IResolverRequest](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverRequest) parameter and returns an [IResolverResultSet](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverResultSet).

The result set consists of zero or more [IResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverResult) objects.  There can be more than one result in the set if there is ambiguity of the resolution result, such as when there are multiple overloads of a method that can match the expression to resolve.  In this scenario, the results are sorted by which match best.  So the first result is always the best match.

Each [IResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverResult) includes a string [Name](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverResult.Name) property and a [Type](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IResolverResult.Type) property that returns the resulting [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) of the result if one is available (not applicable for namespaces).  For instance, if the result is for a type, this property returns the type.  If the result is for a member, this property returns the member's return type.

The kinds of resolver results include:

- [INamespaceResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.INamespaceResolverResult) - Specifies the full name of a namespace that was matched and includes the [INamespaceDeclaration](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDeclaration) objects from the various loaded assemblies that match that full name.

- [ITypeResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.ITypeResolverResult) - Specifies the [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) that was matched.

- [ITypeMemberResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.ITypeMemberResolverResult) - Specifies the [ITypeMemberDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeMemberDefinition) that was matched.

- [IParameterResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IParameterResolverResult) - Specifies the [IParameterDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IParameterDefinition) that was matched.

- [IVariableResolverResult](xref:ActiproSoftware.Text.Languages.DotNet.Resolution.IVariableResolverResult) - Specifies the [IVariableDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IVariableDefinition) that was matched.

## A Resolve Sample

This code gets the current caret offset in a SyntaxEditor that is using a [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage), creates a context for it, builds a new resolver request, and calls the resolver to get a result set for what is under the caret:

```csharp
// Create a context
var snapshotOffset = syntaxEditor.ActiveView.Selection.EndSnapshotOffset;
var context = new CSharpContextFactory().CreateContext(snapshotOffset);

if (context.ProjectAssembly != null) {
	// Create a request
	var request = new ResolverRequest(context.TargetExpression);
	request.Context = context;
	
	// Resolve
	var resolver = context.ProjectAssembly.Resolver;
	var resultSet = resolver.Resolve(request);
}
```

This code does the same thing as above but uses a [IDotNetContext](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext).[Resolve](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetContext.Resolve*) helper method to perform a bulk of the work:

```csharp
var snapshotOffset = syntaxEditor.ActiveView.Selection.EndSnapshotOffset;
var context = new CSharpContextFactory().CreateContext(snapshotOffset);
var resultSet = context.Resolve();
```

## Enumerating Members for an Offset

The resolver is normally used to tell what is at an offset but it also can enumerate accessible namespaces, types, etc. for a given offset.  To do this, simply create the context whose kind is `DotNetContextKind.SelfAndSiblings`.

This code shows how to obtain a result set that includes all of the accessible namespaces, types, etc. for the current caret offset in a SyntaxEditor that is using a [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage):

```csharp
var snapshotOffset = syntaxEditor.ActiveView.Selection.EndSnapshotOffset;
var context = new CSharpContextFactory().CreateContext(snapshotOffset, DotNetContextKind.SelfAndSiblings);
var resultSet = context.Resolve();
```
