---
title: "String Resource Browser"
page-title: "String Resource Browser - Utilities"
order: 3
---
# String Resource Browser

The String Resource Browser allows you to view all the string resources that are defined for the various Actipro WPF control products.  It generates code to customize or localize any of the string resources in our products.

It is available from the View menu within the Sample Browser's title bar.

![Screenshot](images/string-resource-browser.png)

*The String Resource Browser looking at Docking & MDI's string resources*

## Using the String Resource Browser

Simply select the desired product in the Product drop-down to view the string resources for a product.

The list of resources shows each resource name and its default value.

## Customizing String Resources

You may wish to change the default text to your own text, and in many other cases, you may simply wish to create localized versions of our default text.

The String Resource Browser can create C# statements that override our default text for a specific string resource.  To do this, select the target string resource in the list, enter some customized text, and click the Copy button to copy the code to the clipboard.

The copied value looks like this:

```csharp
ActiproSoftware.Products.Docking.SR.SetCustomString(ActiproSoftware.Products.Docking.SRName.UICommandActivateNextDocumentText.ToString(), "Customized Text");
```

> [!NOTE]
> You should paste this code in your application startup so that it is run before any user interface has been loaded.
