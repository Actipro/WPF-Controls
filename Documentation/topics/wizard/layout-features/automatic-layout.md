---
title: "Automatic Layout"
page-title: "Automatic Layout - Wizard Layout, Globalization, and Accessibility Features"
order: 2
---
# Automatic Layout

Wizard does not use any fixed sizes for control parts and makes use of automatic layout best practices.

## What is Automatic Layout?

Automatic layout is the ability for a control to resize to its contents.  This is very important when an end user uses different fonts than the developer, or in applications that are localized to multiple languages since each language's text may take up more or less room than the same text translated to another language.

When the size of text content, or any other content within Wizard for that matter, does increase, Wizard automatically expands or contracts to fit it.  This way you can ensure that long headers are always displayed.  Also, if you make visible at run-time a control that is in a layout, Wizard will expand as needed to display the new control.  The opposite will occur when hiding a control that is in a layout.

Wizard accomplishes automatic layout by using stack- and grid-based layouts, and never specifying absolute widths or heights.  Instead, minimum width and height values are used.

## Best Usability

We've found that these settings make for the most ideal end user experience:

- Set `Window.Width` on the containing `Window` to a fixed value like `600`, or at least give it a `MinWidth` and `MaxWidth` range.

- Set `Window.SizeToContent` on the containing `Window` to `Height`.

With those settings in place, a consistent wizard width will be used for each page that is displayed.  Also, the wizard will maintain a consistent height that is large enough to fit the tallest of pages in the wizard.  This means that the buttons in the wizard button container will always appear in the same location for each page, making navigation easier for the end user.
