---
title: "Reflection Data"
page-title: "Reflection Data - SyntaxEditor .NET Languages Add-on"
order: 3
---
# Reflection Data

A large object model is included in the add-on for managing reflection data for both .NET assemblies as well as source code files.  All of this data, which consists of everything from namespaces down to member parameters, is stored in a common set of interfaces so that it may easily be queried and used by features like the resolver, thus helping to populate automated IntelliPrompt.

## Assemblies

Similar to real .NET assemblies, assemblies in this add-on, represented by the [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly) interface, store information about contained types and members.  Assemblies can be created for real pre-compiled .NET assemblies, or can be like Visual Studio projects where they are containers of source code files with references to other assemblies.  Assemblies provide access to type/member information via namespaces.

> [!NOTE]
> See the [Assemblies](assemblies.md) topic for detailed information on the types of assemblies and how to load them.

### Assembly Names

While each [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly) has a [Name](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.Name) property that returns its simple name, there also is a [GetAssemblyName](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.GetAssemblyName*) method that returns an [IAssemblyName](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssemblyName) object.  This object stores the simple name as well as some other information such as version.  It supports comparison using the `Equals` method.

### Namespaces Property

The [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly).[Namespaces](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.Namespaces) collection provides a flat list of the namespaces defined in the assembly.  Each item in the collection is a [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition).  The key of the collection used for the indexer is the full name of the namespace.

This code accesses the [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition) for `System.Linq` within the [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly) in the `assembly` variable:

```csharp
var namespaceDef = assembly.Namespaces["System.Linq", true];
```

### GlobalNamespace Property

The [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly).[GlobalNamespace](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.GlobalNamespace) property provides hierarchical access to the tree of namespaces defined in the assembly.  The [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition) returned by [GlobalNamespace](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.GlobalNamespace) is always the global namespace whose name is an empty string.

This code accesses the global (root) [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition) within the [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly) in the `assembly` variable:

```csharp
var namespaceDef = assembly.GlobalNamespace;
```

## Namespaces

Namespaces defined within an assembly are represented by the [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition) interface.  As described above, the main entry point to these objects is via [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly).[Namespaces](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.Namespaces) or [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly).[GlobalNamespace](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly.GlobalNamespace).

Each namespace definition within an assembly has a unique full name (e.g. `System.Collections.Generic`) and contains all the types that are defined within it.  The [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition).[FullName](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition.FullName) property returns the namespace's full name, and its [Name](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IReflectionDefinition.Name) property returns the simple name (last identifier of the full name).

### Nested Namespaces

Namespaces often contain other nested namespaces.  For instance the global namespace whose name is an empty string often contains a nested namespace named `System`.  These nested namespaces are available from the [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition).[Children](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition.Children) collection, which is indexed using the simple names of each nested namespace.

This code accesses the [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition) for `System` within the [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly) in the `assembly` variable:

```csharp
var namespaceDef = assembly.GlobalNamespace.Children["System", true];
```

### Types

Namespaces contain zero or more [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) object that specify the types defined within the namespace.

This code accesses the [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) for `System.Int32` within the [IAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IAssembly) in the `assembly` variable:

```csharp
var typeDef = assembly.Namespaces["System", true].Types["Int32", true];
```

### Other Collections

Namespace definitions provide access to a couple other helper collections.

First is the [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition).[ExtensionMethods](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition.ExtensionMethods) property.  This property is a collection of [ITypeMemberDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeMemberDefinition) objects where each member definition is for an extension method defined on a type which is contained by the namespace.

Second is the [INamespaceDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition).[StandardModules](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition.StandardModules) property.  This property is a collection of [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) objects where each type definition is a standard module (only used by VB).  The collection of standard modules is effectively a subset of the [Types](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.INamespaceDefinition.Types) collection.

## Types

There are three main categories of types used in the reflection data.

An [ITypeReference](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeReference) is simply a pointer to another type.  The referenced type may be defined in the same assembly or within another.  A resolver can examine its qualified name and the general context in which it is located, and uses that information to try and convert the type reference to a type definition.

An [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) is the definition of a type within an assembly.  The [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) interface inherits [ITypeReference](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeReference).  Type definitions can potentially define nested types and members.

A type definition contains all sorts of information about a type, such as its access (public, private, etc.), its kind (class, structure, etc.), its namespace, and whether it is abstract, sealed, etc.  It has a [NestedTypes](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition.NestedTypes) collection that lists all of the nested type definitions within the type.  Its [TypeParameters](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition.TypeParameters) and [TypeArguments](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeReference.TypeArguments) collections indicate the generic parameters and any related generic arguments (if a generic type has been constructed).  The [Members](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition.Members) collection contains the list of members within the type.

This code accesses the [ITypeMemberDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeMemberDefinition) overloads for `ToString` within the [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) in the `typeDef` variable:

```csharp
var memberDefs = typeDef.Members["ToString", true];
```

An [ITypeParameter](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeParameter) represents a generic parameter for a generic type definition or a generic method.  The [ITypeParameter](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeParameter) interface inherits [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition).  Type parameters include several properties that describe their constraints and variance (covariant/contravariant) modifiers.  The list of type constraints is indicated via the [BaseTypes](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition.BaseTypes) collection.

## Members

Type members, represented by the [ITypeMemberDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeMemberDefinition) interface, can be a method, property, field, operator, etc.  A type member stores information such as its access (public, private, etc.), kind (method, property, etc.), return type (if any), parameters (if any), type parameters (for generic methods), and other flag properties indicating whether it is abstract, static, etc.

This code accesses the first [IParameterDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IParameterDefinition) within the [ITypeMemberDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeMemberDefinition) in the `memberDef` variable:

```csharp
var firstParamDef = memberDef.Parameters[0];
```

## Parameters

Parameter definitions, represented by the [IParameterDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IParameterDefinition) interface, store information about parameters in members, such as their name, type, whether they are optional, by-reference, are a parameter array, etc.

## LINQ Queryable

Reflection data is fully LINQ queryable.  For instance, this query looks in the [IProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly) for an [ITypeDefinition](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ITypeDefinition) named `Foo`:

```csharp
var fooType = (from ns in project.Namespaces 
	let types = ns.Types["Foo", true]
	where types != null
	select types).FirstOrDefault();
```
