---
title: "Slide Shows"
page-title: "Slide Shows - Wizard Appearance Features"
order: 7
---
# Slide Shows

Since Actipro Wizard can completely be restyled and templated, it is possible to harness it for many non-traditional uses.  One great sample is for making a WPF-based slide show presentation.

![Screenshot](../images/slide-show-title.gif)

*A Wizard with a custom template that allows it to function property for a slide show presentation*

The screenshot above was made by applying custom styles to the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) and [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) classes.  The [WizardButtonContainer](xref:ActiproSoftware.Windows.Controls.Wizard.WizardButtonContainer) was also retemplated so that the back and next buttons appear as arrows, are the only ones present in the wizard, and hide when disabled.  The back button is hidden in the screenshot since that is the first "title" slide in the slide show.

The Shared Library's [ReflectionContentControl](xref:ActiproSoftware.Windows.Controls.ReflectionContentControl) is used to provide the reflection effect for the slide title.
