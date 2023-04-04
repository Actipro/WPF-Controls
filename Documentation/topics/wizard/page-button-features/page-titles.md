---
title: "Page Captions, Descriptions, and Titles"
page-title: "Page Captions, Descriptions, and Titles - Wizard Page and Button Features"
order: 3
---
# Page Captions, Descriptions, and Titles

Wizard pages all have a unique a caption, description, and title.  Each property is designed for a certain purpose.

Wizard also makes it simple to automatically update the containing `Window`'s title based on the current page selection.  There are even numerous options for formatting the title.

## Page Captions and Descriptions

Each page has [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Caption](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Caption) and [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Description](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Description) properties.  A page's caption is a small phrase that is typically displayed in a bold or large font as header text for the page.  A page's description is one or more sentences that give a brief description of the page.  The description typically appears directly under the page's caption.

See the [Page Types](page-types.md) topic for details on where the page's caption and description appear in the interior and exterior page templates.

## Page Titles

Each page also has a [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Title](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Title) property.  The title of a wizard page typically is not directly visible within a page; however it can be used to construct the title for the containing `Window`.

See the section below on modifying `Window` titles for more information on how to use the page's title.

## Modifying the Containing Window's Title

Wizard has numerous features to automatically update the containing `Window`'s title based on the selected page.

The first step in choosing how to automatically modify the `Window` title is to choose the title behavior.  The [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBehavior](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBehavior) property gets and sets an enumeration value of type [WizardWindowTitleBehavior](xref:@ActiproUIRoot.Controls.Wizard.WizardWindowTitleBehavior) that determines the title behavior.  These are the values of that enumeration:

| Value | Description |
|-----|-----|
| `None` | Do not modify the containing `Window` control's title. |
| `PageTitle` | Displays the text in the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBaseText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBaseText) property appended by a dash and the text in the current page's [Title](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Title) property. |
| `PageCaption` | Displays the text in the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBaseText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBaseText) property appended by a dash and the text in the current page's [Caption](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Caption) property. |
| `Custom` | Displays the text in the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBaseText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBaseText) property appended by the text in the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleCustomPageText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleCustomPageText) property, which defaults to `"- Step {0} of {1}"`.  If using step numbers, this title behavior is only useful for wizards that do not have multiple execution paths.  See below for more information on the various settings for the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleCustomPageText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleCustomPageText) property. |

Note that several of the title behaviors append some text onto the base text in the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBaseText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBaseText) property.  Therefore, the text you normally would have set to the `Window.Title` property should be set to this property.

When a `Custom` title behavior is chosen, the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleCustomPageText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleCustomPageText) is used.  It is passed as the format string to a `String.Format` call.  Multiple format items are passed in as parameters to this `String.Format` call:

| Index | Description |
|-----|-----|
| `0` | The current page index incremented by `1` for display purposes.  This item is only useful for wizard that have a single linear execution path. |
| `1` | The total number of pages.  This item is only useful for wizard that have a single linear execution path. |
| `2` | The page title. |
| `3` | The page caption. |
| `4` | The page description. |

Say that you were designing a SQL connection wizard and you wanted your wizard's containing `Window` title to display a title in this format: `"SQL Connection Wizard : (WizardPage Title) - Step (Page #) of (Page Count)"`.  To achieve that you would use these settings:

| Member | Value |
|-----|-----|
| [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBehavior](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBehavior) Property | `Custom` |
| [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleBaseText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleBaseText) Property | `"SQL Connection Wizard"` |
| [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[WindowTitleCustomPageText](xref:@ActiproUIRoot.Controls.Wizard.Wizard.WindowTitleCustomPageText) Property | `": {2} - Step {0} of {1}"` |

This XAML code shows how to use simple page title-based Window titles for a registration wizard:

```xaml
<wizard:Wizard x:Name="wizard" WindowTitleBehavior="PageTitle" WindowTitleBaseText="Registration Wizard"></wizard:Wizard>
```
