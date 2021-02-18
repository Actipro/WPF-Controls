---
title: "AST Nodes"
page-title: "AST Nodes - Parsing - SyntaxEditor Text/Parsing Framework"
order: 4
---
# AST Nodes

An abstract syntax tree (AST) is a structural tree representation of source code, without the specific details of the language, such as punctuation.  An AST is comprised of AST Nodes, each which represent a particular construct in the concrete syntax.

## An Example

Here is an example of an AST for two simple expressions.  Notice how the constructs of the original expressions are communicated through the AST, but language-specific syntactical details (such as operator precedence and punctuation) are excluded.  The AST is correct for both expressions.

Sample expression 1:

```
1 + 4 / 7
```

 Sample expression 2:

```
1 + (4 / 7)
```

 Sample AST:

```
"+" [
    1
    "/" [
        4
        7
    ]
]
```

## Introduction to IAstNode

The [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) interface provides the base requirements for an AST node.  It provides a standardized way for your syntax language's services to communicate or consume the syntactical structure of the document.  Any custom or third-party AST node classes can be made to implement the interface.

[IParser](xref:ActiproSoftware.Text.Parsing.IParser) implementations generally create some sort of AST (a tree of [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) objects) for the document being parsed and provide the resulting AST as a property on the [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData) that is returned.  The parse data as a whole is stored in the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property, which it is available to other language services.

For instance, a code outliner could consume the [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) tree by searching for function declaration nodes and making the text range collapsible.

### IAstNode Members

| Member | Description |
|-----|-----|
| [Children](xref:ActiproSoftware.Text.Parsing.IAstNode.Children) Property | Gets the list containing the child AST nodes of this AST node. |
| [Contains](xref:ActiproSoftware.Text.Parsing.IAstNode.Contains*) Method | Returns whether the AST node contains the specified offset. |
| [EndOffset](xref:ActiproSoftware.Text.Parsing.IAstNode.EndOffset) Property | Gets or sets the end offset of the AST node, if known. |
| [FindChildNode](xref:ActiproSoftware.Text.Parsing.IAstNode.FindChildNode*) Method | Searches through the child nodes for a node that contains the specified offset. |
| [FindDescendantNode](xref:ActiproSoftware.Text.Parsing.IAstNode.FindDescendantNode*) Method | Recursively searches through the descendant nodes for a node that contains the specified offset. |
| [HasChildren](xref:ActiproSoftware.Text.Parsing.IAstNode.HasChildren) Property | Gets whether the AST node contains any child AST nodes. |
| [Length](xref:ActiproSoftware.Text.Parsing.IAstNode.Length) Property | Gets the character length of this AST node, if known. |
| [StartOffset](xref:ActiproSoftware.Text.Parsing.IAstNode.StartOffset) Property | Gets or sets the start offset of the AST node, if known. |
| [ToTreeString](xref:ActiproSoftware.Text.Parsing.IAstNode.ToTreeString*) Method | Outputs the contents of the AST node in tree form. |

## The DefaultAstNode Class

The [DefaultAstNode](xref:ActiproSoftware.Text.Parsing.Implementation.DefaultAstNode) class provides a completely generic implementation of the [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) interface, where each node has a string [Value](xref:ActiproSoftware.Text.Parsing.IAstNode.Value) and can contain any other node in its [Children](xref:ActiproSoftware.Text.Parsing.IAstNode.Children) collection.

This class is good to use when first prototyping out a grammar, or in scenarios where type-specific AST nodes are not required.  It is the default AST node type used by the [LL(*) Parser Framework's tree constructors](../../ll-parser-framework/tree-constructors.md) that don't designate a specific AST node type.

## Type-Specific AST Nodes

Sometimes it is beneficial to have specific types of AST nodes, where a distinct .NET class is created for each type of AST node.  Take this snippet of C# for example, a simple class declaration:

```
class Foo {}
```

With the default AST nodes discussed above, a resulting AST would be something like:

```
ClassDeclaration[
	Name[
		"Foo"
	]
]
```

It's easy to imagine how large the AST grows as you get into much more complex code since additional AST nodes are typically used to describe the context of their contained nodes, such as with the `Name` node above.

In contrast, assume we build a class called `ClassDeclaration` like this:

```csharp
public class ClassDeclaration : AstNodeBase {
	public string Name { get; set; }
	...
}
```

The `ClassDeclaration` class is a type-specific AST node, where instead of wrapping our `Foo` value node with another `Name` node, we simply set a string property called `Name` to the value `Foo`.  The snippet that originally required three AST nodes to represent, now just uses one AST node with a property set.  Thus the overall complexity of the AST is reduced.

Another major benefit of using type-specific AST nodes is that since they are .NET classes, you can fully extend them with partial classes, etc.  This makes it easy to add helper methods/properties or to override the default `ToString` result.

The only downside of using type-specific AST nodes is that they can take more time to develop, since you need to define a class for each type of AST node your language grammar can generate.  Luckily, we've made this almost a non-issue since the [Language Designer](../../language-designer-tool/ast-nodes-config-pane.md) tool has full type-specific AST node code generation features.  All you have to do is indicate a few settings for each AST node type and its properties, and the code will be generated for you.

## The AstNodeBase Class

The abstract [AstNodeBase](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase) class is available to use as a base class for any type-specific AST nodes.  It fully implements the [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) interface.

The only requirement of inheritors is that they override its [GetChildrenEnumerator](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase.GetChildrenEnumerator*) method to return an enumerator for any [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) objects that are children of the node.  This allows the node's [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode).[Children](xref:ActiproSoftware.Text.Parsing.IAstNode.Children) property enumerate child nodes.

For instance, a `ClassDeclaration` class might define a `Members` property where each member is represented by another AST node.  In this scenario, the [GetChildrenEnumerator](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase.GetChildrenEnumerator*) method should be overridden to return those AST nodes.

If your type-specific AST nodes are generated by the [Language Designer](../../language-designer-tool/ast-nodes-config-pane.md) tool, all this work is done for you.
