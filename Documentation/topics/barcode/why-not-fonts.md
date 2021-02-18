---
title: "Why Not Bar Code Fonts?"
page-title: "Why Not Bar Code Fonts? - Bar Code Reference"
order: 2
---
# Why Not Bar Code Fonts?

## Top 10 Reasons To Use Actipro Bar Code Instead of a Bar Code Font

1. **Bar code fonts (a.k.a. fontware) usually require licensing** - While you may be able to scour the web to find a royalty-free implementation of a bar code font for a simple bar code symbology such as Code 39, most font creators charge licensing fees even in excess of $100 for the use of a single bar code font. Beware because even some free bar code fonts don't permit distribution with an application. With Actipro Bar Code, you get a large number of bar code symbology implementations at a low price and royalty-free distribution.

1. **Bar code fonts require registration in the Windows Fonts folder** - When you distribute a bar code font with your application, you must update your installer to register the font with the target system's Windows Fonts folder, which is not a straightforward process. Also if the end user removes the font manually from the Fonts folder, the bar codes in your application are suddenly broken. With Actipro Bar Code, there is no font registration necessary since we natively vector draw each bar code.

1. **Actipro Bar Code can render the character value under the bar code lines, bar code fonts can't** - Bar code fonts are incapable of rendering the represented value below the bar code. Actipro Bar Code can do this and even provides a number of display style options for the displayed value, including the ability to have the value's characters intrude into the bars such as in UPC-A.

1. **Actipro Bar Code can alter the height of the bar code lines, bar code fonts can't** - Bar code fonts only support a fixed height of lines, whereas Actipro Bar Code allows you via a property setting to exactly specify a desired line height.

1. **Actipro Bar Code auto-computes checksums, bar code fonts can't** - A font can't do math. A major benefit of using Actipro Bar Code is that each symbology automatically computes any appropriate checksum values for you so there is no math that needs to be done manually by you.

1. **Actipro Bar Code supports bar width ratios, bar code fonts don't** - In some symbologies such as Code 39, you are permitted to have dark lines be anywhere from two to three times as wide as narrow lines. Actipro Bar Code provides an option for controlling this. Bar code fonts are always fixed bar widths.

1. **Actipro Bar Code is written in WPF, for WPF developers** - Actipro Bar Code is written in 100% C# and is designed for use with any WPF application. All Actipro Bar Code drawing is done in vectors, so it cleanly scales to any size and always looks crisp on your printouts. The bar code itself is displayed by a BarCode control, which is a Visual that can be used in a FixedDocument, FlowDocument, or added to an XPS document.

1. **Actipro Bar Code supports borders and a caption** - Actipro Bar Code can support the rendering of a border around the bar code (with total control over the roundness of the corners), along with an optional caption that is rendered above the bar code. You can't do this with bar code fonts.

1. **Actipro Bar Code supports WPF scaling and rotation** - Bar codes produced by Actipro Bar Code can be resized or rotated however you like via the use of WPF layout transforms. You could even use a WPF Viewbox to have the bar code auto scale to fit a certain area.

1. **Actipro Bar Code can render to a WPF DrawingContext** - In one line of code, render any bar code onto a DrawingContext via a call to the BarCodeSymbology.Render method.
