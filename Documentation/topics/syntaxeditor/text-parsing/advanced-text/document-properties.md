---
title: "Document Properties"
page-title: "Document Properties - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 99
---
# Document Properties

The [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[Properties](xref:ActiproSoftware.Text.ICodeDocument.Properties) property provides a dictionary-like object that can store any sort of custom data that should be associated with the document.

The property returns an [PropertyDictionary](xref:ActiproSoftware.Text.Utility.PropertyDictionary) object, which supports any object type as a key or value.

## Storing Data

In this code snippet we store an object referenced by the variable `someObject` into the properties dictionary under the string key `MyPropertyKey`:

```csharp
document.Properties["MyPropertyKey"] = someObject;
```

## Retrieving Data

In this code snippet we retrieve an object stored under the string key `MyPropertyKey`:

```csharp
object someObject;
if (document.Properties.TryGetValue("MyPropertyKey", out someObject)) {
	...
}
```

Alternatively the indexer could be used, however it will throw an exception if there is no object stored with the specified key, whereas [TryGetValue](xref:ActiproSoftware.Text.Utility.PropertyDictionary.TryGetValue*) will not.
