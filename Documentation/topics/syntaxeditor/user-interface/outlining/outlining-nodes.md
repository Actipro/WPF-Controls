---
title: "Outlining Nodes and Definitions"
page-title: "Outlining Nodes and Definitions - SyntaxEditor Outlining and Collapsing Features"
order: 3
---
# Outlining Nodes and Definitions

Outlining nodes are part of a nested hierarchy of text ranges, stored within an outlining manager for a document.  They can each be assigned various characteristics via outlining node definitions, such as what they render when collapsed, if they are collapsed automatically when a document is loaded, etc.

## Outlining Nodes

The [IOutliningManager](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningManager) for a document stores a complete hierarchy of outlining nodes.  Nodes are created either by a language (when in automatic mode) or by the end user (when in manual mode).  Each node is created using an outlining node definition, which provides some characteristics for the node.  Node definitions are described in detail below.

Outlining nodes are represented by the [IOutliningNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode) interface.  This interface gives access to various node data properties and also lets you traverse to other nodes.

### Root Node

The root node, which is never visible and always contains the entire document, can be retrieved via the [IOutliningManager](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningManager).[RootNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningManager.RootNode) property.  The [IsRoot](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.IsRoot) property of any node returns whether it is the root node or not.

### Node Definition

The [Definition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.Definition) property returns the [IOutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition) used to create the node.  See below for more information on node definitions.

### Collapsed State

The [IsCollapsed](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.IsCollapsed) property returns whether the node is collapsed.  It can be toggled to expand or collapse the node.

### Snapshot Range and Open/Closed Nodes

The [SnapshotRange](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.SnapshotRange) property returns the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) that is covered by the node.

If a node is "closed", it has a definite end at the end offset specified in the snapshot range.  If a node is "open", it does not have an end and inherits the end offset of the nearest "closed" ancestor node.  The [IsOpen](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.IsOpen) property indicates if a node is open.

### Traversing Nodes

The [Count](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.Count) property indicates how many child nodes are in the node, and the [IOutliningNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode) indexer property provides access to the child nodes.  The [ParentNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.ParentNode) property provides access to the parent node.

The [FindNodeRecursive](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.FindNodeRecursive*) method does a recursive search to find the lowest level node that contains the specified offset.  This should generally only be called from the root node.

> [!NOTE]
> The [IOutliningNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode) instances are just wrappers around the core data structure used to track outlining node data.  Therefore doing a reference equality check on two nodes that should be the same will always return `false`, since a new [IOutliningNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode) instance is created each time you traverse to a new node.

## Outlining Node Definitions

Outlining node definitions, represented by the [IOutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition) interface, are used to define the characteristics of an outlining node.  Node definitions are used by [outlining sources](outlining-sources.md) when in automatic outlining mode, and can optionally be passed to the [IOutliningManager](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningManager).[AddManualNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningManager.AddManualNode*) when adding a manual node.

> [!TIP]
> You can create static instances of your outlining node definitions and reuse them when creating outlining nodes.  For example you may wish to support curly brace and multi-line comment outlining nodes in your language.  In this scenario, just define two static outlining node definitions (one for curly braces and one for multi-line comments), and use the appropriate one as you create outlining nodes.

### OutliningNodeDefinition Class

The [OutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation.OutliningNodeDefinition) class implements the [IOutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition) interface and is generally used wherever an [IOutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition) is needed.

### Collapsed Content

The [IOutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition).[GetCollapsedContent](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition.GetCollapsedContent*) method allows you to return some content to render when an outlining node is collapsed.  Right now, only text strings can be returned by that method.  If a null value is returned, the text "..." will be rendered in collapsed nodes.  Comment nodes may wish to return a default of "/\*\*/" instead.

The [OutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation.OutliningNodeDefinition) class defines an additional [DefaultCollapsedContent](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation.OutliningNodeDefinition.DefaultCollapsedContent) property that can be set to the content to return in the [GetCollapsedContent](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition.GetCollapsedContent*) method.  This makes it so that for most cases, inheriting [OutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation.OutliningNodeDefinition) is not necessary.

### Is Default Collapsed

Sometimes certain node types should be collapsed by default when a document is loaded.  #region nodes are good examples of this sort of node.

The [IsDefaultCollapsed](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition.IsDefaultCollapsed) property should return `true` if nodes created using the definition should be collapsed by default.

### Is Implementation

There is a common edit action called "Collapse to Definitions" that can be executed by the end user.  In this scenario, the outlining tree is searched for any nodes with definitions that return `true` from their [IsImplementation](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition.IsImplementation) property.  These nodes are then collapsed.

### Is Collapsible

Sometimes you may wish to define nodes that do not allow the end user to alter their state from the UI.  In these scenarios, a definition's [IsCollapsible](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNodeDefinition.IsCollapsible) property should return `false`.

## Advanced Collapsed Content

As mentioned above, collapsed content can be set on a node definition so that collapsed nodes can render something other than the default "..." when collapsed.  Once you get automatic outlining working for your language, you may wish to have certain node types some alternate content that dynamically changes based on the content of the node.  For instance, a collapsed #region node may wish to show the text label that follows the #region preprocessor keyword.

This can be achieved by creating a class that inherits [OutliningNodeDefinition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation.OutliningNodeDefinition).  Use this new node definition wherever you wish to have this advanced collapse content.  In the new node definition class, override the [GetCollapsedContent](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation.OutliningNodeDefinition.GetCollapsedContent*) method.  The method is passed an [IOutliningNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode) instance.  This instance is a node that was created using the definition.

The [IOutliningNode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode).[SnapshotRange](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.IOutliningNode.SnapshotRange) property returns the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) covered by the node.  Snapshot ranges provide access to the complete [text snapshot](../../text-parsing/core-text/documents-snapshots-versions.md), via their [Snapshot](xref:ActiproSoftware.Text.TextSnapshotRange.Snapshot) property.  Then from that, an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) can be obtained so that [scanning through text and tokens](../../text-parsing/core-text/scanning-text.md) can be performed.

Thus by using the properties on the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) and/or by using an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader), you can gather text/token data and display customized content for any nodes that use this definition.
