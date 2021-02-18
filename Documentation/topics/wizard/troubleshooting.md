---
title: "Troubleshooting"
page-title: "Troubleshooting - Wizard Reference"
order: 9
---
# Troubleshooting

This topic contains troubleshooting data specific to the Wizard product.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as other WPF Studio products, please see the more general [TroubleShooting](../troubleshooting.md) topic.

## Page Transition Animation is Choppy

When you are running a project in debug mode from Visual Studio, the debug process affects WPF's performance for things like animations and layouts.  Therefore, the page transition animation effects may appear slightly choppy when running the application in debug mode.

If you run a standalone compiled application without any attached debugger you will notice a dramatic increase in performance.
