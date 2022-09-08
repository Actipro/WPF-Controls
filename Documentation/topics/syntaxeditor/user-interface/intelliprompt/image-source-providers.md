---
title: "Image Source Providers"
page-title: "Image Source Providers - SyntaxEditor IntelliPrompt Features"
order: 8
---
# Image Source Providers

Image source providers are classes that provide @if (wpf winrt) {`ImageSource`}@if (winforms) {`Image`} objects on demand to IntelliPrompt sessions.  An example of their usage is providing image sources for completion list items.

## The IImageSourceProvider Interface

All image source providers implement the [IImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IImageSourceProvider) interface.  There are a couple built-in provider implementations described below.

## The DirectImageSourceProvider Class

The [DirectImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.DirectImageSourceProvider) is the simplest way to provide image sources.  Its constructor takes an @if (wpf winrt) {`ImageSource`}@if (winforms) {`Image`} instance and that is what is returned by the provider.

The downside to using this provider is that the image source must be loaded prior to creation of the provider.  If a large number of providers are needed (such as for a completion list), then it is better to use another provider or build a custom one.

This code creates a [DirectImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.DirectImageSourceProvider) using the @if (wpf winrt) {`ImageSource`}@if (winforms) {`Image`} that is already loaded in a `source` variable:

```csharp
DirectImageSourceProvider provider = new DirectImageSourceProvider(source);
```

## The CommonImageSourceProvider Class

The [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider) is a handy image source provider that comes pre-packaged with some of the most common 16x16 icons used in features like completion lists.

Its constructor takes a single parameter of type [CommonImageKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageKind), which is an enumeration that specifies one of many built-in images.  This image source provider works on-demand and only pulls the image from assembly resources as needed.

This code creates a [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider) that returns a 16x16 public method icon:

```csharp
CommonImageSourceProvider provider = new CommonImageSourceProvider(CommonImageKind.MethodPublic);
```

### Image Set Selection

@if (winforms) {

There are several image sets that are included with SyntaxEditor:

- MetroLight (default) - Best for most Metro themes.
- MetroDark - Best for the MetroDark theme.

}

@if (winrt wpf) {

There are several image sets that are included with SyntaxEditor:

- Classic - Best for non-Metro themes.
- MetroLight (default) - Best for most Metro themes.
- MetroDark - Best for the MetroDark theme.

}

> [!TIP]
> The MetroLight and MetroDark image sets are implemented with vector graphics, enabling the best clarity when used on high DPI systems.

To change the image set that is currently in effect, set the [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider).[DefaultImageSet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider.DefaultImageSet) property to a [CommonImageSet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSet) value.  Note that some UI, such as for the [NavigableSymbolSelector](navigable-symbol-selector.md), may need to be reloaded following a change to this property.

## Custom Image Source Providers

It's easy to write your own custom image source provider if none of the implementations above work for your scenario.  Simply create a class that implements [IImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IImageSourceProvider).

The [GetImageSource](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IImageSourceProvider.GetImageSource*) method that must be implemented as part of that interface returns an @if (wpf winrt) {`ImageSource`}@if (winforms) {`Image`} object.  You could have it be pulled from a resource, database, or any other storage mechanism.

Since the method is called on-demand, the image source doesn't need to be loaded up front.
