---
title: "Context"
page-title: "Context - XML Language - SyntaxEditor Web Languages Add-on"
order: 9
---
# Context

The XML language can return detailed context information about any offset in a document.  The context includes data such as declared namespaces, containing element hierarchy, current element/attribute, and more.  This sort of information is essential in determining what to display in automated IntelliPrompt.

## Obtaining Context Information

Context information can be retrieved via the use of the [XmlContextFactory](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlContextFactory) class.  Its [CreateContext](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlContextFactory.CreateContext*) method accepts a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and returns the [IXmlContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext) for that offset.

This code retrieves an [IXmlContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext) for the caret's offset (the end selection offset in the active view):

```csharp
TextSnapshotOffset snapshotOffset = syntaxEditor.ActiveView.Selection.EndSnapshotOffset;
IXmlContext context = new XmlContextFactory().CreateContext(snapshotOffset);
```

## XML Context

[IXmlContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext) objects provide XML-related context information for a specific snapshot offset, such as the element hierarchy that contains the offset, the available namespace declarations, current element/attribute, and more.

These [IXmlContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext) members are important:

| Member | Description |
|-----|-----|
| [ElementHierarchy](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.ElementHierarchy) Property | Gets a read-only list of [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext) objects that describes the element hierarchy.  The element hierarchy is the list of parent elements that contain the current offset. |
| [InitializationSnapshotRange](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.InitializationSnapshotRange) Property | Gets the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) with which the context was initialized. |
| [NamespaceDeclarations](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.NamespaceDeclarations) Property | Gets the mapping dictionary of namespace to namespace prefixes that are declared at the context.  The dictionary keys are namespaces and the values are namespace prefixes. |
| [SchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.SchemaResolver) Property | Gets the [IXmlSchemaResolver](xref:ActiproSoftware.Text.Languages.Xml.IXmlSchemaResolver) to use. |
| [SnapshotOffset](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.SnapshotOffset) Property | Gets the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) for which this context was created. |
| [TargetAttribute](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.TargetAttribute) Property | Gets the [IXmlAttributeContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext) that contains context information related to the target attribute.  This property is only populated if the context is related to an attribute. |
| [TargetElement](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.TargetElement) Property | Gets the [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext) that contains context information related to the target element.  This property is only populated if the context is related to an element or attribute. |
| [Type](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.Type) Property | Gets an [XmlContextType](xref:ActiproSoftware.Text.Languages.Xml.XmlContextType) that specifies the type of context. |

## Element Context

[IXmlContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext) objects can return contextual data about XML elements via their [ElementHierarchy](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.ElementHierarchy) and [TargetElement](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.TargetElement) properties.  Both of these properties return objects of type [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext) that have element contextual information.

Element contexts contain their namespace/name information, included attributes, resolved schema element data, and various snapshot ranges.

These [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext) members are important:

| Member | Description |
|-----|-----|
| [Attributes](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.Attributes) Property | Gets the dictionary of attributes for this element.  The dictionary keys are the attribute qualified names and the values are [IXmlAttributeContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext) objects. |
| [EndTagSnapshotRange](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.EndTagSnapshotRange) Property | Gets the nullable [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) that specifies the range of the end tag, if there is one. |
| [Name](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.Name) Property | Gets the name of the element. |
| [Namespace](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.Namespace) Property | Gets the namespace that defines the element. |
| [NamespacePrefix](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.NamespacePrefix) Property | Gets the prefix of the namespace that defines the element. |
| [QualifiedName](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.QualifiedName) Property | Gets the `XmlQualifiedName` that contains the element's qualified name. |
| [SchemaElement](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.SchemaElement) Property | Gets the resolved `XmlSchemaElement` for this element, if known. |
| [StartTagNameSnapshotRange](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.StartTagNameSnapshotRange) Property | Gets the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) that specifies the range of the start tag name. |

## Attribute Context

[IXmlAttributeContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext) objects specify contextual information about XML attributes.  Instances are returned by the [IXmlContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext).[TargetAttribute](xref:ActiproSoftware.Text.Languages.Xml.IXmlContext.TargetAttribute) and [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext).[Attributes](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.Attributes) properties.

Attribute contexts contain their namespace/name information, owner element, resolved schema attribute data and name snapshot range.

These [IXmlAttributeContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext) members are important:

| Member | Description |
|-----|-----|
| [Element](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.Element) Property | Gets the [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext) upon which this attribute is located. |
| [Name](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.Name) Property | Gets the name of the attribute. |
| [NameSnapshotRange](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.NameSnapshotRange) Property | Gets the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) that specifies the range of the attribute name. |
| [Namespace](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.Namespace) Property | Gets the namespace that defines the attribute. |
| [NamespacePrefix](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.NamespacePrefix) Property | Gets the prefix of the namespace that defines the attribute. |
| [QualifiedName](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.QualifiedName) Property | Gets the `XmlQualifiedName` that contains the attribute's qualified name. |
| [SchemaAttribute](xref:ActiproSoftware.Text.Languages.Xml.IXmlAttributeContext.SchemaAttribute) Property | Gets the resolved `XmlSchemaAttribute` for this attribute, if known. |
