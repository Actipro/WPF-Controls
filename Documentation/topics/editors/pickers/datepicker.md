---
title: "DatePicker"
page-title: "DatePicker - Editors Pickers"
order: 8
---
# DatePicker

The [DatePicker](xref:@ActiproUIRoot.Controls.Editors.DatePicker) control allows for the input of a `DateTime` value's date component.  It uses a month calendar with animated view changes and its design is similar to the layout of a standard calendar, making it instantly approachable by end users.

![Screenshot](../images/datepicker.png)

Use the previous/next buttons in the calendar's header to navigate to a nearby month, or tap the current view's title to 'zoom out' to year, decade, or century views.

The [DatePicker](xref:@ActiproUIRoot.Controls.Editors.DatePicker) directly embeds a [MonthCalendar](../other-controls/monthcalendar.md) control to provide the functionality mentioned above.  See the [MonthCalendar](../other-controls/monthcalendar.md) topic for more information on that control's capabilities.

## Time Retention

When using separate [DatePicker](xref:@ActiproUIRoot.Controls.Editors.DatePicker) and [TimePicker](xref:@ActiproUIRoot.Controls.Editors.TimePicker) controls that bind to the same `DateTime` object, it's sometimes helpful to have the [DatePicker](xref:@ActiproUIRoot.Controls.Editors.DatePicker) retain the time portion as selections are made, instead of reverting the time to midnight.  The [CanRetainTime](xref:@ActiproUIRoot.Controls.Editors.DatePicker.CanRetainTime) property can be set to `true` to enable time retention.

Note that a [DateTimePicker](xref:@ActiproUIRoot.Controls.Editors.DateTimePicker) can be used in place of separate [DatePicker](xref:@ActiproUIRoot.Controls.Editors.DatePicker) and [TimePicker](xref:@ActiproUIRoot.Controls.Editors.TimePicker) controls if dates and times require input.

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:@ActiproUIRoot.Controls.Editors.DatePicker.Maximum) and [Minimum](xref:@ActiproUIRoot.Controls.Editors.DatePicker.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:DatePicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
