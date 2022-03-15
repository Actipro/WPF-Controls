---
title: "Adornment Manager Provider"
page-title: "Adornment Manager Provider - Provider Services - SyntaxEditor Language Creation Guide"
order: 3
---
# Adornment Manager Provider

Adornment manager provider services allow [adornment layers](../../user-interface/adornment/adornment-layers.md) to be created for a view.

## What are Adornment Managers?

Adornment managers are objects that create an [adornment layer](../../user-interface/adornment/adornment-layers.md) and add/update/remove adornment objects within the layer.  Layers can be ordered around any other layer (text foreground, selection, etc.) and adornments can consist of any element.  This allows for any sort of custom content to be rendered within the text area.  Since each adornment manager can only be associated with a single view, adornment manager provider services are used to create adornment managers for views upon request.

See the [Adornment Layers](../../user-interface/adornment/adornment-layers.md) topic for details on how adornments, adornment layers, and providers work.

Adornment managers are used for rendering many built-in features of SyntaxEditor such as:

- Caret
- Selection
- Squiggle lines

## Registering with a Syntax Language

As described in the [Adornment Layers](../../user-interface/adornment/adornment-layers.md) topic, [AdornmentManagerProvider<T>](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.Implementation.AdornmentManagerProvider`1) objects can be used as language services to provide adornment managers to any text view that requests them.

This code shows how to register an adornment manager provider language service that returns `AlternatingRowsAdornmentManager` objects (defined in a QuickStart to provide alternating row highlights).  for views that use the language.  Note that we are also passing a "singleton" key so that the adornment manager that is created for any view using the language is persisted in the views's [Properties](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.Properties) dictionary while it is active.

```csharp
language.RegisterService(new AdornmentManagerProvider<AlternatingRowsAdornmentManager>(typeof(AlternatingRowsAdornmentManager)));
```
