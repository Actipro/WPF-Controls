---
title: "Label and Key Tip Generation"
page-title: "Label and Key Tip Generation - Bars Controls"
order: 210
---
# Label and Key Tip Generation

Many controls have `Key`, `Label`, and `KeyTipText` properties. Since these properties are closely related, controls can auto-generate a `Label` value based on the `Key` if no other `Label` has been explicitly set.  Likewise, a `KeyTipText` value can be auto-generated from a `Label` if no other `KeyTipText` has been expicitly set.

Other contextual values (like certain `ICommand` types) can also be used when auto-generating property values.

This time-saving feature helps reduce the need to specify many `Label` and `KeyTipText` values, except in scenarios where a customized value is necessary!

> [!TIP]
> The optional MVVM Library defines multiple view models that utilize the same [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator) and [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator) classes to auto-generate property values that are not explicitly passed to an object's constructor, so the same time-saving feature is available for MVVM configurations, too.  See the [MVVM Support](../mvvm-support.md) topic for additional details on the MVVM Library.

> [!IMPORTANT]
> `Label` and `KeyTipText` are UI-based properties that may need to be localized, but the `Key` property value should not be localized. If localization of `Label` or `KeyTipText` is necessary for an application, those property values should be explicitly defined instead of relying on auto-generation from the non-localized `Key` property value.

## Label Generation

The static [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator) class defines several convenience methods that can be used to automatically generate a label from various context.

### Label from Key

The [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromKey](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromKey*) method will analyze a given `Key` to determine the best label. Supported key values must *only* contain upper- or lower-ASCII letters `A-Z`, digits `0-9`, underscore (`_`), or hypen (`-`). Any other characters in the key will result in a `null` value being returned.

When generating a label, the `Key` must be split into individual words using either word break characters or changes in character casing.

#### Word Break Characters

Any `Key` that includes an underscore (`_`) or hypen (`-`) will treat those characters as word breaks and the actual word break character is ignored. Individual words are always converted to "Title Case" and separated with spaces to form the final label.

For example, the `Key` value `"font-settings"` or `"FONT_SETTINGS"` will both generate the `Label` value `"Font Settings"`.

#### Character Casing

Any `Key` without word break characters will use either "PascalCase" or "camelCase" conventions to recognize words based on changes in character casing.

For example, the `Key` values `"OpenFile"` or `"openFile"` will recognize the words "Open" and "File".

With the exception of the first word in "camelCase", all recognized words will use the casing of the original word without converting to "Title Case", so the `Key` value `"ExportToXML"` will generate the label `"Export To XML"` with the original acronym casing unchanged.

> [!IMPORTANT]
> A limitation of "camelCase" is that the casing of the leading word is ambiguous between words and acrynoyms (e.g., `"toUpper"` vs. `"ioException"`), so the first word will always be converted to "Title Case".  For this reason, "PascalCase" is recommended instead.

### Label from Command

The [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromCommand*) method will analyze an `ICommand` to determine the best label.
- The `ApplicationCommands.NotACommand` command is ignored and will always return a value of `null`.
- Any other `RoutedUICommand` will use the `RoutedUICommand.Text` property as the label.
- Any `RoutedCommand` will use the `RoutedCommand.Name` property as the label.


A value of `null` will be returned if a label could not be determined from the command.

## Key Tip Generation

The static [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator) class defines several convenience methods that can be used to automatically generate key tip text from various contexts.

### Key Tip from Label

The [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromLabel](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromLabel*) method will analyze a string label to determine the best key tip text.  The label is analyzed against multiple rules (in the order listed below) and the first matching rule, if any, will determine the key tip text.  A value of `null` will be returned if a matching rule was not found.

| Rule | Description |
| ---- | ---- |
| Leading digits | Any label that begins with 1 or 2 digits will use those digits as the key tip text. For example, the label `"10 Column Layout"` will use `"10"` as the key tip text. |
| Significant word upper-case or digit | Any significant word (three or more characters) that starts with an upper-case letter or digit will use the leading character as the key tip text.  For example, the label `"Go To Line"` will use `"L"` as the key tip text. Even though other words start with upper-case letters, "Line" is the first *significant* word. |
| Significant word lower-case | Any significant word (three or more characters) that starts with a lower-case letter will use the leading character as the key tip text.  For example, the label `"My documents"` will use `"D"` as the key tip text. Even though "My" starts with an upper-case letter, "documents" is the more significant word by length. |
| Any word upper-case or digit | Any word that starts with an upper-case letter or digit will use the leading character as the key tip text.  For example, the label `"go to End"` will use `"E"` as the key tip text.
| Any word lower-case | Any word that starts with a lower-case letter will use the leading character as the key tip text.  For example, the label `"go up"` will use `"G"` as the key tip text.
| Everything else | No key tip text is recognized and `null` will be returned.

In the list of rules above, regular expressions are used to locate "words" where `\b` defines a word boundary and the `\w` character class defines the characters recognized as part of a word. Upper-case and lower-case letters are defined by Unicode categories `Lu` and `Ll`, respectively. Digits are defined by the `\d` character class.

### Key Tip from KeyGesture

The [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromKeyGesture](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromKeyGesture*) method will analyze a `KeyGesture` to determine the best key tip text.

If the `KeyGesture.Key` property defines a `Key` whose string representation is a single letter, that letter will be used as the key tip text.  For example, the `KeyGesture` for <kbd>Ctrl</kbd>+<kbd>V</kbd> (commonly used for the paste command), would return `"V"` as the key tip text.

A value of `null` will be returned if a value for key tip text could not be determined from the `KeyGesture`.

### Key Tip from Command

The [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromCommand*) method will analyze an `ICommand` to determine the best key tip text.

If the `ICommand` is a `RoutedCommand`, the `RoutedCommand.InputGestures` collection will be queried for gestures of type `KeyGesture`. The first `KeyGesture` that returns a non-`null` value when passed to [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromKeyGesture](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromKeyGesture*) will be used as the key tip text.

A value of `null` will be returned if a value for key tip text could not be determined from the `ICommand`.
