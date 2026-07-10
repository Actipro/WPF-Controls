---
title: "Extension Methods"
page-title: "Extension Methods - Shared Library Reference"
order: 13
---
# Extension Methods

Various extension methods are provided for several common .NET types.

> [!IMPORTANT]
> The `ActiproSoftware.Windows.Extensions` namespace must be imported for the extensions described below to be available.

> [!TIP]
> The Core Library defines many additional [Extension Methods](../core/extension-methods.md) for common types that are not associated with a UI framework.

## DependencyObject Extensions

The [DependencyObjectExtensions](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions) type contains extension methods for the `DependencyObject` type.  Some of the most frequently used extension methods are highlighted below.  Refer to the API documentation for additional methods.

> [!IMPORTANT]
> The WPF platform defines both `Visual` and `Visual3D` as visual objects, but `DependencyObject` (which is not explicitly a visual) is the only common base class.  Any extension methods that interact with the visual tree must accept a `DependencyObject` but will ignore the object if it is not a `Visual` or `Visual3D` instance.  For example, calling [GetVisualParent](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.GetVisualParent*) on an object that is not a `Visual` or `Visual3D` instance will always return `null`.

| Member | Description |
|-----|-----|
| [FindAncestorOfType&lt;T&gt;](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.FindAncestorOfType*) | Finds the first ancestor of the given type in the **visual tree**. |
| [FindDescendantOfType&lt;T&gt;](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.FindDescendantOfType*) | Finds the first descendant of the given type in the **visual tree**. |
| [FindLogicalAncestorOfType&lt;T&gt;](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.FindLogicalAncestorOfType*) | Finds the first ancestor of the given type in the **logical tree**. |
| [FindLogicalDescendantOfType&lt;T&gt;](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.FindLogicalDescendantOfType*) | Finds the first descendant of the given type in the **logical tree**. |
| [GetVisualAncestors](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.GetVisualAncestors*) | Enumerates the ancestors of a visual in the **visual tree** and can be easily combined with LINQ queries. |
| [GetVisualDescendants](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.GetVisualDescendants*) | Enumerates the descendants of a visual in the **visual tree** and can be easily combined with LINQ queries. |
| [GetLogicalAncestors](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.GetLogicalAncestors*) | Enumerates the ancestors of an object in the **logical tree** and can be easily combined with LINQ queries. |
| [GetLogicalDescendants](xref:@ActiproUIRoot.Extensions.DependencyObjectExtensions.GetLogicalDescendants*) | Enumerates the descendants of an object in the **logical tree** and can be easily combined with LINQ queries. |

