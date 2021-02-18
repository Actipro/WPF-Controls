---
title: "Getting Started"
page-title: "Getting Started - Editors Reference"
order: 2
---
# Getting Started

Getting started with Editors is simple. Follow the steps below to build your first chart.

## Add Assembly References

First, add references to the `ActiproSoftware.Shared.Wpf.dll` and `ActiproSoftware.Editors.Wpf.dll` assemblies.  They should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## Add Any Editor Control

This 'xmlns' declaration in your root XAML control allows access to the various controls in this product:

```xaml
xmlns:editors="http://schemas.actiprosoftware.com/winfx/xaml/editors"
```

Then find the parent element that will contain the editor.  This could be a `UserControl` or any other type of `UIElement`.

Next, add an editor control to the desired parent element.  In this sample we will add a [Int32EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int32EditBox):

```xaml
<editors:Int32EditBox Minimum="1" Maximum="100" />
```

Next configure the control's options, set up a binding to the [Value](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.Value) property, and you are all done.

![Screenshot](images/int32editbox-opened.png)

There are over 30 editor controls contained in this product.  Be sure to check them all out.

## Further Study

To familiarize yourself with all of the features in Editors, take a look at the feature documentation and also look at the numerous QuickStarts located in the sample project.

If you require further assistance after looking through those, please visit our Editors support forum.
