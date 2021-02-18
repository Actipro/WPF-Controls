---
title: "Converting to v17.1"
page-title: "Converting to v17.1 - Conversion Notes"
order: 91
---
# Converting to v17.1

The 17.1 version has new lightweight implementations of the Editors and PropertyGrid controls created that are much more performant, use less memory, and still provide a wide array of features.  These infrastructure changes did require some breaking changes and some API cleanup was performed at the same time.  All of the breaking changes are detailed in the linked topics below.

> [!NOTE]
> Please read through the [Licensing](../licensing.md) topic's section on "licenses.licx" files.  In the 17.1 version we improved the "licenses.licx" file licensing mechanism to only use a single entry that covers all Actipro products you've licensed.  Now a single new entry is used, and all older Actipro entries should be removed.

## Massive Updates to Editors

The old versions of the edit boxes in Editors, while providing many features, were very complex and used many UI elements to support their functionality.  This meant that windows with a lot of editors (such as when used in property grids or grids) could be very memory intensive and take a while to load.

For the 17.1 version, we reimplemented them with a design that is code-compatible with the Universal Windows variations of Editors.  These new edit boxes are now lightweight controls that harness a native `TextBox` control in their templates.  They use far fewer elements and bindings than before and load very quickly.  New edit boxes, pickers, and other miscellaneous controls have been added as well.

While we fully encourage you to upgrade your existing applications that use the older Editors product to the new version so that you can continue to receive the latest feature additions and bug fixes, we are also shipping a legacy version of the old assembly that can temporarily be used in the interim for full backward compatibility.

> [!NOTE]
> The new Editors contains most, but not all of the features in the old version, along with many new features.  If the new version is missing a feature area that you need that was in the old version, please write our support team and we can work with you to determine if it's feasible to add back in.

### General PartEditBox and Part Updates

The part edit box infrastructure was completely rewritten in the 17.1 version to use a native `TextBox` control in templates and to be lighter weight in terms of elements.  Most of the features of the old edit boxes are still available in the new version, along with many new ones, but some lesser-used features were removed in scenarios where they didn't add much value and would have introduced extensive complexity to the controls.  A benefit of using `TextBox` as the primary input method is that now features like IME input and rich UIA are fully supported.

The old version's part edit boxes had a three tier infrastructure: edit box, part groups, and parts.  Now part edit boxes have two-tiers: edit box and parts.

Edit boxes no longer work like an items control in terms of how UI is constructed (there are no more placement slots).  Some edit boxes have templates that contain preview swatches (like for `Brush` or `Color` display) that are left-aligned.  All edit boxes have a `TextBox` that allows text entry of a string representation of the object being edited.  Many edit boxes have an optional drop-down button that shows if the [HasPopup](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.HasPopup) property is `true`.  The template of each edit box can be customized if additional buttons or UI needs to be added.

- In an effort to simplify the object model, part groups no longer exist in the new version. `ArePartGroupsDisabledWhenUnchecked`, `ArePartGroupsSelectable`, and `DefaultPartGroupVisibility` properties removed.

- Since a simpler `TextBox` is used for input, several properties no longer apply. `BackgroundEditable`, `BackgroundNonEditable`, `ForegroundEditable`, and `ForegroundNonEditable` properties removed.

- Similarly, `CenterSlotHorizontalAlignment`, `CenterSlotMargin`, `LeftSlotMargin`, and `RightSlotMargin` properties removed.  If you need to alter the alignment of the text in the `TextBox`, use the `TextAlignment` property instead.

- Nullable edit boxes no longer have a checkbox.  Instead, select all the text and delete it to null out a nullable edit box. `IsChecked`, `CheckBoxInactiveVisibility`, `CheckBoxMargin`, `CheckBoxPlacementOrder`, `CheckBoxPlacementSlot`, `CheckBoxTemplate`, and `CheckBoxVisibility` properties removed.

- The `InitialValue` property was removed since the former checkbox used to initialize it.  However numerous edit boxes have a new `DefaultValue` property that is similar and is used as the value to set when incrementing/decrementing (via spinner or arrow keys) from a null value.

- The drop-down button is now defined in each edit box control's template so placement properties no longer apply. `DropDownButtonInactiveVisibility`, `DropDownButtonMargin`, `DropDownButtonPlacementOrder`, `DropDownButtonPlacementSlotSpinnerTemplate`, `DropDownButtonTemplate`, `DropDownHorizontalAlignment`, and `DropDownStaysOpen` properties removed.

- `DropDownButtonVisibility` property removed.  Use [HasPopup](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.HasPopup) property instead.

- `DropDownClosed` and `DropDownOpened` events removed.

- `DropDownContent`, `DropDownContentTemplate`, and `DropDownContentTemplateSelector` properties removed.  Each edit box now has a related picker (i.e. [DatePicker](xref:ActiproSoftware.Windows.Controls.Editors.DatePicker) for [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox)) and its style can be set via [PopupPickerStyle](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PopupPickerStyle).  This `Style` value can set a new `Template` for the picker if needed.

- `Focus` (overload) and `SelectFirstGroup` methods removed.  Use the normal Focus method instead.

- `Hint`, `HintTemplate`, `HintTemplateSelector`, `IsHintTransitioningEnabled`, and `IsHintVisible` properties removed.  Use [PlaceholderText](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PlaceholderText) property instead.

- `IsDropDownButtonTransparencyModeEnabled` property removed.  Use [UsageContext](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.UsageContext) property instead.

- `IsDropDownOpen` property removed.  Use [IsPopupOpen](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.IsPopupOpen) property instead.

- `IsFocusMovedOnTerminalMatches` property removed.

- `MaxDropDownHeight`, `MaxDropDownWidth`, `MinDropDownHeight`, and `MinDropDownWidth` properties removed.

- `PartValueCancelTriggers` property and `PartValueCancelTriggers` type removed.

- `PartValueCommitTriggers` property renamed to `CommitTriggers`, and `PartValueCommitTriggers` type renamed to `PartEditBoxCommitTriggers`.

- `PromptIndicatorVisibility` property removed.

- `SelectFirstGroup` method removed. `SelectAll` method can be used instead.

- `SpinBehavior` property renamed to [SpinWrapping](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.SpinWrapping) and related `SpinBehavior` enum renamed to `SpinWrapping`.

- `SpinnerInactiveVisibility` property removed and `SpinnerVisibility` property return value changed to cover both former properties.  Use the revised [SpinnerVisibility](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.SpinnerVisibility) property instead.

- Spinner is now defined in each edit box control's template so placement properties no longer apply. `SpinnerMargin`, `SpinnerPlacementOrder`, `SpinnerPlacementSlot`, and `SpinnerTemplate` properties removed.

In addition to the above, some number-oriented edit boxs have had these property changes:

- `StepValue` property renamed to [SmallChange](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.SmallChange), which is used with arrow key, mouse wheel, spinner, and slider incrementing.  A new [LargeChange](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.LargeChange) property is used for `PgUp`/`PgDn` incrementing.  Number-oriented structure edit boxes like [ThicknessEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ThicknessEditBox) now use the same `Type` for the change property values, so that different increments can be given for the different parts.

- [Minimum](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.Minimum) and [Maximum](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.Maximum) now use non-nullable types instead of the same type as the `Value` property.  So for a [DoubleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox) (which has a `Value` of type `Nullable<Double>`), the type for those two properties will be `Double`.

- `AllowInfinity` and `AllowNaN` properties are now available only on [DoubleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox) and [SingleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.SingleEditBox) as [IsPositiveInfinityAllowed](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.IsPositiveInfinityAllowed), [IsNegativeInfinityAllowed](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.IsNegativeInfinityAllowed), and [IsNaNAllowed](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox.IsNaNAllowed) properties.

### PropertyGrid Integration Updates

The new "ActiproSoftware.Editors.Interop.Grids.Wpf.dll" assembly provides integration of edit boxes with the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) control.  found in the Grids product via the use of custom [property editors](../grids/propertygrid-features/property-editors.md).  This feature is similar to what was in the old "ActiproSoftware.Editors.Interop.PropertyGrid.Wpf.dll" assembly.  Several of the property editor classes (like [BrushPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.BrushPropertyEditor), etc.) have had property updates to correspond to the related edit box property changes (adds, renames, or removes) described in this topic.  Also the `MaskedTextBoxPropertyEditor` class is now named [MaskedStringPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor).

The [Editors and PropertyGrid Interoperability](../editors/interoperability/propertygrid.md) topic covers the property editors that are available, including additional ones like [DatePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor), to integrate edit boxes with PropertyGrid and how to do so in the new version.

### DataGrid Integration Updates

The new "ActiproSoftware.Editors.Interop.DataGrid.Wpf.dll" assembly provides easy integration of edit boxes with the native WPF DataGrid control.  Several of the column classes (like [DataGridBrushColumn](xref:ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.DataGridBrushColumn), etc.) have had property updates to correspond to the related edit box property changes (adds, renames, or removes) described in this topic.  Also the `DataGridMaskedTextColumn` class is now named [DataGridMaskedStringColumn](xref:ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.DataGridMaskedStringColumn).

The [Editors and DataGrid Interoperability](../editors/interoperability/datagrid.md) topic covers the column classes that are available, including additional ones like [DataGridDateColumn](xref:ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.DataGridDateColumn), to integrate edit boxes with DataGrid.

### Ribbon Integration Updates

The old "ActiproSoftware.Editors.Interop.Ribbon.Wpf.dll" assembly, which provided Ribbon control-like appearance for edit boxes via a `RibbonEditorsStyleBehavior` class is no longer used in the new version.  Instead the new edit box templates include a built-in special mode that can be activated for toolbar/Ribbon contextual usage.

The [Editors and Ribbon Interoperability](../editors/interoperability/ribbon.md) topic covers the properties that should be set on edit boxes to achieve a Ribbon control-like appearance, and to also optionally support labels.

### AnalogClock Changed to TimePicker

The `AnalogClock` control has been removed.  Use the new [TimePicker](xref:ActiproSoftware.Windows.Controls.Editors.TimePicker) control instead.

### BrushEditBox Updates

- `AllowGradientBrushes` property renamed to [IsGradientAllowed](xref:ActiproSoftware.Windows.Controls.Editors.BrushEditBox.IsGradientAllowed).
- `BrushEditorStyle` property renamed to [PopupPickerStyle](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PopupPickerStyle).
- `BrushPreviewInactiveVisibility` property removed and swatch is always visible now.
- `BrushPreviewMargin` property renamed to [SwatchMargin](xref:ActiproSoftware.Windows.Controls.Editors.BrushEditBox.SwatchMargin).
- `BrushPreviewPlacementOrder` property removed.
- `BrushPreviewPlacementSlot` property removed.
- `BrushPreviewTemplate` property removed.
- `BrushPreviewVisibility` property renamed to [HasSwatch](xref:ActiproSoftware.Windows.Controls.Editors.BrushEditBox.HasSwatch).
- `IsAlphaComponentEditable` property removed.
- `IsAlphaComponentVisible` property renamed to [IsAlphaEnabled](xref:ActiproSoftware.Windows.Controls.Editors.BrushEditBox.IsAlphaEnabled).

### BrushEditor Changed to BrushPicker

The `BrushEditor` and `BrushPreviewControl` controls have been removed.  Use the new [BrushPicker](xref:ActiproSoftware.Windows.Controls.Editors.BrushPicker) control instead.

### Calculator Updates

- `DisplayMode` property removed in favor of new [HasDisplay](xref:ActiproSoftware.Windows.Controls.Editors.Calculator.HasDisplay) and [HasMemoryButtons](xref:ActiproSoftware.Windows.Controls.Editors.Calculator.HasMemoryButtons) properties.

- `DisplayString` property renamed to [DisplayText](xref:ActiproSoftware.Windows.Controls.Editors.Calculator.DisplayText).

- `CalculatorCommands` class removed in favor of new command properties directly on [Calculator](xref:ActiproSoftware.Windows.Controls.Editors.Calculator).

### ColorEditBox Updates

- `ColorEditorStyle` property renamed to [PopupPickerStyle](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PopupPickerStyle).
- `ColorPreviewInactiveVisibility` property removed and swatch is always visible now.
- `ColorPreviewMargin` property renamed to [SwatchMargin](xref:ActiproSoftware.Windows.Controls.Editors.ColorEditBox.SwatchMargin).
- `ColorPreviewPlacementOrder` property removed.
- `ColorPreviewPlacementSlot` property removed.
- `ColorPreviewTemplate` property removed.
- `ColorPreviewVisibility` property renamed to [HasSwatch](xref:ActiproSoftware.Windows.Controls.Editors.ColorEditBox.HasSwatch).
- `EditableParts` property and related `ColorEditableParts` enumeration removed.
- `ExportFormat` property removed.
- `Format` property removed.
- `IsAlphaComponentVisible` property renamed to [IsAlphaEnabled](xref:ActiproSoftware.Windows.Controls.Editors.ColorEditBox.IsAlphaEnabled).

### ColorEditor Changed to ColorPicker

The `ColorEditor` control has been removed.  Use the new [ColorPicker](xref:ActiproSoftware.Windows.Controls.Editors.ColorPicker) control instead.

### CornerRadiusEditBox Updates

- `EditableParts` property and related `CornerRadiusEditableParts` enumeration removed.

### DateTimeEditBox Updates

- `AnalogClockStyle` and `MonthCalendarStyle` properties removed.  Use [PopupPickerStyle](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PopupPickerStyle) with a [DateTimePicker](xref:ActiproSoftware.Windows.Controls.Editors.DateTimePicker) style instead.

- `DateValue` property removed.  Use the new [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) control when doing date-only selection.

- `DefaultDropdownContentType` property removed since there are separate [DateTimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox), [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox), and [TimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeEditBox) controls now.

- `EditableParts` property and related `DateTimeEditableParts` enumeration removed.

- `ExportFormat` property removed.

### DateTimeEditor Changed to DateTimePicker

The `DateTimeEditor` control has been removed.  Use the new [DateTimePicker](xref:ActiproSoftware.Windows.Controls.Editors.DateTimePicker) control instead.

### EnumEditBox Updates

- `EnumListBoxStyle` property renamed to [PopupPickerStyle](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PopupPickerStyle).
- `UseDescriptionAttributes` property renamed to [UseDisplayAttributes](xref:ActiproSoftware.Windows.Controls.Editors.EnumEditBox.UseDisplayAttributes).  Both `DescriptionAttribute` and `DisplayAttribute` are now supported.

### EnumListBox Updates

- `DisplayMode` property and related `EnumListBoxDisplayMode` enumeration removed.
- `IsItemsSourceAutoUpdated` property removed.
- `EnumListBoxStyle` property renamed to [PopupPickerStyle](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.PopupPickerStyle).
- `UseDescriptionAttributes` property renamed to [UseDisplayAttributes](xref:ActiproSoftware.Windows.Controls.Editors.EnumListBox.UseDisplayAttributes).  Both `DescriptionAttribute` and `DisplayAttribute` are now supported.

### EnumListBoxItem Updates

- `DisplayMode` property removed.
- `UseDescriptionAttributes` property removed.

### GuidEditBox Updates

- `NewGuidButtonInactiveVisibility` property removed and swatch is always visible now.
- `NewGuidButtonMargin` property removed.
- `NewGuidButtonPlacementOrder` property removed.
- `NewGuidButtonPlacementSlot` property removed.
- `NewGuidButtonTemplate` property removed.
- `NewGuidButtonVisibility` property removed.  Set [IsReadOnly](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.PartEditBoxBase`1.IsReadOnly) to `true` to hide the button.
- `SetValueToNewGuid` property renamed to [NewGuidCommand](xref:ActiproSoftware.Windows.Controls.Editors.GuidEditBox.NewGuidCommand).

### Int32RectEditBox Updates

- `EditableParts` property and related `RectEditableParts` enumeration removed.

### MaskedTextBox Updates

- `MaskedTextBox` changed to inherit the native `TextBox` control.
- `IsMatchedChanged` event changed to be an `EventHandler`.
- `IsMatchedTerminally` property and `IsMatchedTerminallyChanged` event removed
- `MaskType` property renamed to [MaskKind](xref:ActiproSoftware.Windows.Controls.Editors.MaskedTextBox.MaskKind), and related `MaskType` enumeration renamed to [MaskKind](xref:ActiproSoftware.Windows.Controls.Editors.MaskKind).
- `PromptBrush`, `PromptGeometry`, and `PromptIndicatorType` properties removed since [MaskedTextBox](xref:ActiproSoftware.Windows.Controls.Editors.MaskedTextBox) now inherits the native `TextBox` instead of rendering prompt glyphs itself.
- `TextChanged` and `TextChanging` events changed to use the native `TextBox` versions, since `MaskedTextBox` how inherits `TextBox`.

### MonthCalendar Updates

- `ActiveViewMode` and `MaxViewMode` properties changed to return `MonthCalendarViewMode`, renamed from `CalendarViewMode`.
- `AreTransitionAnimationsEnabled` property removed and animations always occur.
- `BeginUpdate`/`EndUpdate` methods removed.
- `ClearButtonContent` property changed to a string and renamed to `ClearButtonText`.
- `ClearButtonContentTemplate` property removed.  Use the new `ClearButtonStyle` property instead.
- `DayItemStyle` and `DayItemStyleSelector` properties removed.  Use the new `DayItemTemplate` property instead.
- `DayOfWeekItemStyle` and `DayOfWeekItemStyleSelector` properties removed.  Use the new `DayNameItemContainerStyle` and `DayNameItemTemplate` properties instead.
- `DecadeItemStyle` and `DecadeItemStyleSelector` properties removed.  Use the new `DecadeItemTemplate` property instead.
- `IsDayOfWeekHeaderVisible` property removed, and header will always be visible.
- `MaxDate` property renamed to `Maximum`.
- `MinDate` property renamed to `Minimum`.
- `MonthItemStyle` and `MonthItemStyleSelector` properties removed.  Use the new `MonthItemTemplate` property instead.
- `NextViewButtonStyle` and `PreviousViewButtonStyle` properties removed.  Use the new `NavigationButtonStyle` property instead.
- `Refresh` method added a parameter.
- `SelectedDates` property changed to use a new `DateRangeCollection` implemention in the Shared Library.
- `SelectionChanged` event declaration changed.
- `SelectionChanging` event removed.
- `SelectionMode` property changed to return `MonthCalendarSelectionMode`, renamed from `CalendarSelectionMode`.
- `ViewResetMode` property changed to return `MonthCalendarViewResetMode`, renamed from `CalendarViewResetMode`.
- `WeekNumberItemStyle` and `WeekNumberItemStyleSelector` properties removed.  Use the new `WeekNumberItemContainerStyle` and `WeekNumberItemTemplate` properties instead.
- `YearItemStyle` and `YearItemStyleSelector` properties removed.  Use the new `YearItemTemplate` property instead.

### PasswordBox Removed

Use the native `PasswordBox` control instead.

### PointEditBox Updates

- `EditableParts` property and related `PointEditableParts` enumeration removed.

### Rating Updates

- `IsReadOnly` property removed.  Set the `IsEnabled` property to `false` to simulate read-only mode.

### RatingItem Updates

- `HoverGlyphTemplate`, `SelectedAlternateBackgroundGlyphTemplate`, and `SelectedAlternateForegroundGlyphTemplate` properties replaced by the [ActiveGlyphTemplate](xref:ActiproSoftware.Windows.Controls.Editors.RatingItem.ActiveGlyphTemplate) and [AverageGlyphTemplate](xref:ActiproSoftware.Windows.Controls.Editors.RatingItem.AverageGlyphTemplate) properties.

### RectEditBox Updates

- `EditableParts` property and related `RectEditableParts` enumeration removed.

### SizeEditBox Updates

- `EditableParts` property and related `SizeEditableParts` enumeration removed.

### Spinner Updates

- `CommandTarget` property removed.
- `DecrementValue` property renamed to [DecrementCommand](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.Spinner.DecrementCommand).
- `IncrementValue` property renamed to [IncrementCommand](xref:ActiproSoftware.Windows.Controls.Editors.Primitives.Spinner.IncrementCommand).

### TextBox Removed

Use the native `TextBox` control instead.

### ThicknessEditBox Updates

- `EditableParts` property and related `ThicknessEditableParts` enumeration removed.

### TimeEditor Changed to TimePicker

The `TimeEditor` control has been removed.  Use the new [TimePicker](xref:ActiproSoftware.Windows.Controls.Editors.TimePicker) control instead.

### TimeSpanEditBox Updates

- `EditableParts` property and related `TimeSpanEditableParts` enumeration removed.
- `ExportFormat` property removed.

### VectorEditBox Updates

- `EditableParts` property and related `VectorEditableParts` enumeration removed.

### Temporary Inclusion of Older Editors and Interop Legacy Assemblies

We recognize that for the 17.1 version, numerous breaking changes were made and that you might not be able to immediately update to the newer logic.  While we fully encourage you to do so since all future updates and maintenance will only be done on this newer Editors codebase, we still offer the older Editors version along with its related interop assemblies.

These older legacy assemblies are not installed by default.  You must check the 'Control Assemblies / Legacy' option in the installer to install them.  Once you do so, these Editors-related assemblies will appear in a 'Legacy' subfolder of the Actipro assembly install folder (see the [Deployment](../deployment.md) topic for the default location), but will not appear in the GAC:

- ActiproSoftware.Editors.Legacy.Wpf.dll
- ActiproSoftware.Editors.Interop.DataGrid.Legacy.Wpf.dll
- ActiproSoftware.Editors.Interop.PropertyGrid.Legacy.Wpf.dll
- ActiproSoftware.Editors.Interop.Ribbon.Legacy.Wpf.dll

You will be able to reference the `ActiproSoftware.Editors.Legacy.Wpf` assembly in place of the new `ActiproSoftware.Editors.Wpf` assembly to maintain full backward compatibility with the old version, while you work on upgrading your application to the new version.

See the [Licensing](../licensing.md) topic for info on how to add an entry to the `licenses.licx` file (updated info added in v17.1).

Do NOT reference both the `ActiproSoftware.Editors.Wpf` and `ActiproSoftware.Editors.Legacy.Wpf` assemblies (or similar for interop assemblies) in the same project, since they have many types/members named the same.

Again, these legacy assemblies will only be included temporarily.  We encourage you to upgrade your code to the latest Editors assembly as soon as it is feasible so that you can continue to receive the latest product feature additions and bug fixes.

## Massive Updates to PropertyGrid, Moved Into Grids Assembly

The old `PropertyGrid` control was very feature-rich and was very customizable.  The downside of it was that it was relatively slow to load objects and didn't make the best use of virtualization.

For the 17.1 version, we are introducing a new Grids product that replaces the old PropertyGrid product.  Customers who have active PropertyGrid license subscriptions will get Grids for free, and Grids is included in WPF Studio similar to how PropertyGrid was.  Grids first started off with implementations of tree controls (`TreeListBox` and `TreeListView`) that were built from the ground up to render tree structures in a highly-performant way, making full use of virtualization.  The new `PropertyGrid` extends these controls and loads large complex objects instantly, even in cases where the old `PropertyGrid` may have taken several seconds to load.  The entire data model and data factory design has been rewritten to be simpler and easier than ever to customize, while retaining all of the abilities found in the old `PropertyGrid`.  Integration with the new Editors controls continues to be fully supported.

While we fully encourage you to upgrade your existing applications that use the older PropertyGrid product to the new Grids version so that you can continue to receive the latest feature additions and bug fixes, we are also shipping a legacy version of the old assembly that can temporarily be used in the interim for full backward compatibility.

### Control Infrastructure Redesigned

In the 17.1 version, the entire core infrastructure of the control hierarchy for property grid was redesigned.  It now is based on our new tree controls (`TreeListBox` and `TreeListView`) and is much faster as a result.

The new tree controls allow custom columns to be inserted and the ability to resize columns can be disabled by setting the [CanColumnsResize](xref:ActiproSoftware.Windows.Controls.Grids.TreeListView.CanColumnsResize) property (formerly named `AreDefaultColumnsResizable`) to `false`.  See the [Columns](../grids/propertygrid-features/columns.md) topic for details on columns, resizing, and creating custom columns.

### SelectedObject(s) Properties Renamed to DataObject(s)

Since the new base tree controls have a number of "Select*" properties like [SelectedItem](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.SelectedItem) that generally correspond to which "row" ([PropertyGridItem](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGridItem)) is selected, we renamed the former `SelectedObject` and `SelectedObjects` properties to [DataObject](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.DataObject) and [DataObjects](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.DataObjects) respectively.

### Data Accessors and Factories Redesigned

The former data accessors (now called data models) and data factories have been redesigned.  The concepts behind them are similar to before but there are different class names used (all in the [ActiproSoftware.Windows.Controls.Grids.PropertyData](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData) namespace) and some properties on the data models may be different to accommodate new features and general API cleanup.  The [Data Models and Factories](../grids/propertygrid-features/data-models.md) topic digs into these areas in detail.

### Interface and Class Mappings

While the data model API has been redesigned, many of the new interfaces and classes generally resemble the old ones.  Data model types are now in the [ActiproSoftware.Windows.Controls.Grids.PropertyData](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData) namespace.  Here are some quick notes on conversion:

- `IDataAccessor` interface is now `IDataModel`.
- `DataAccessorBase` class is now `DataModelBase`.
- `IPropertyDataAccessor` interface is now `IPropertyModel`.
- `PropertyDataAccessorBase` class is now `PropertyModelBase`.
- `CachedPropertyDataAccessorBase` class is now `CachedPropertyModelBase`.
- `PropertyDescriptorDataAccessorBase` and `PropertyDescriptorDataAccessor` classes are now `PropertyDescriptorPropertyModel`.
- `CollectionPropertyDescriptorDataAccessor` class is now `CollectionPropertyDescriptorPropertyModel`.
- `MergedPropertyDataAccessor` class is now `MergedPropertyModel`.
- `ImmutablePropertyDescriptorDataAccessor` class is no longer needed.
- `PropertyGridPropertyItem` (a class you could use to directly define a property in XAML) is now `PropertyModel`.  Instances can be added to the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[Properties](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.Properties) collection.
- `ICategoryDataAccessor` interface is now `ICategoryModel`.
- `CategoryDataAccessor` class is now `ICategoryModel`.
- `PropertyGridCategoryItem` (a class you could use to directly define a category in XAML) is removed.  Instead, add `PropertyModel` instances to the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[Properties](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.Properties) collection and assign the proper category name to each `PropertyModel`.  The properties will be automatically categorized by the data factory.
- `ICategoryEditorDataAccessor` interface is now `CategoryModel`.
- `CategoryEditorDataAccessor` class is now `CategoryEditorModel`.

### PropertyGridDataAccessorItem Removed and Binding Changes

In the old version, each `IPropertyDataAccessor` you supplied to the property grid either via a data factory or directly as `PropertyGridPropertyItem` was "wrapped" by a `PropertyGridDataAccessorItem`.  Bindings in name and value data templates used to need to have `RelativeSource={RelativeSource AncestorType={x:Type propgridPrimitives:IPropertyDataAccessor}}`, which would locate and return the wrapper `PropertyGridDataAccessorItem`.  While the wrapper exposed most common properties to the data templates, if you needed direct access to the "wrapped" data accessor, you could look at the `DataContext` of the `PropertyGridDataAccessorItem`.  Needless to say, this design was confusing and not performant due to all bindings needing to do ancestor searching.

The new data model API is much more straightforward, faster, and the idea of `PropertyGridDataAccessorItem` has been removed.  Any `IPropertyModel` supplied to the property grid either via a data factory or directly via [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[Properties](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.Properties) is used as the data context for all name and value data templates.  This means that all control bindings work directly off the source property model with no ancestor searching needed.

### Data Accessor Properties

Some properties on the interfaces/classes mentioned above have been changed or renamed as well.  While several notable model changes are described below, see the [Data Models and Factories](../grids/propertygrid-features/data-models.md) topic for details on how to use the new data model API.

Here are some quick notes on conversion:

- `CanReset` property is now `CanResetValue`.
- `IsMergable` property is now `IsMergeable`.
- `IsReadOnly` property is now `IsValueReadOnly`. `IsReadOnly` is still there, but is now a resolved value per below.
- One `Refresh` method overload removed.
- `Reset` method is now `ResetValue`.
- `ValueName` property is now `Name`.

The property structure for property models to indicate read-only state has changed.  In the old data accessors, there was an `IsReadOnly` property that returned whether the data accessor was read-only and then in the property editor value template, triggers examined that along with a lookup to the ancestor `PropertyGrid` to see if it was read-only.  If either of those were `true`, setters would make the controls in the template read-only or disabled as appropriate.  In the new property model API, the [IPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel).[IsReadOnly](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel.IsReadOnly) property now returns a resolved result of whether the property is read-only, by looking at the [IsHostReadOnly](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel.IsHostReadOnly) property (which comes from [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[IsReadOnly](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.IsReadOnly)) and the [IsValueReadOnly](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel.IsValueReadOnly) property (which returns whether the property value itself is read-only.  This is a nicer design since the one resolved [IsReadOnly](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel.IsReadOnly) can be bound directly to controls in property editor value templates.  Any logic that used to be in old data accessor's `IsReadOnly` property should be moved to determine the [IsValueReadOnly](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel.IsValueReadOnly) property value now.

### Data Factories

In regards to data factories, the abstract `DataFactory` class is now `DataFactoryBase` and the `TypeDescriptorFactory` class is still named `TypeDescriptorFactory`, but has an updated API.  There is no need for `TypeReflectionFactory` any more, so it has been removed.  Data factory types are now in the [ActiproSoftware.Windows.Controls.Grids.PropertyData](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData) namespace.

### Property Editors Changes

Property editors have been reworked to an extent.  Instead of the old `BuiltinEditors` class that had resource key properties, there are now numerous properties on [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) like [DefaultStringNameTemplate](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.DefaultStringNameTemplate) and [DefaultStringValueTemplate](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.DefaultStringValueTemplate) that can be changed to customize the appearance of all built-in name/value editors.

The global `BuiltinEditors.PropertyEditors` collection is renamed to [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[DefaultPropertyEditors](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.DefaultPropertyEditors).  The `PropertyEditorModifier` class and its related actions have had some API changes to make them easier to use.  The former `PropertyEditorModifier` concept is now implemented through the [PropertyGridPropertyEditorsModifier](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyGridPropertyEditorsModifier) class and its updated API explained in detail in the [Property Editors](../grids/propertygrid-features/property-editors.md) topic.

In the past, a custom `DataTemplate` used for value editors would have to use FindAncestor bindings to bind to the value and other data model properties.  The actual data context for the `DataTemplate` was the [PropertyEditor](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyEditor) object.  In the new version, the data context for the `DataTemplate` is now the [IPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel), making it much easier to bind to any property on the data model.  Additionally, the [PropertyEditor](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyEditor) used to display the `DataTemplate`, if any, is available via the [IPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel).[ValuePropertyEditor](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel.ValuePropertyEditor) property.  Each `DataTemplate` is now much smaller and simpler, which helps improve property grid performance.

See the [Property Editors](../grids/propertygrid-features/property-editors.md) topic for detailed information on how property editors work in the new version and how to completely customize them.

### Integration with Editors Updated

The new "ActiproSoftware.Editors.Interop.Grids.Wpf.dll" assembly provides integration of edit boxes with the property grid via the use of custom [property editors](../grids/propertygrid-features/property-editors.md).  This feature is similar to what was in the old "ActiproSoftware.Editors.Interop.PropertyGrid.Wpf.dll" assembly.  Several of the property editor classes (like [BrushPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.BrushPropertyEditor), etc.) have had property updates to correspond to the related edit box property changes (adds, renames, or removes).  Also the `MaskedTextBoxPropertyEditor` class is now named [MaskedStringPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor).

The [Editors and PropertyGrid Interoperability](../editors/interoperability/propertygrid.md) topic covers the property editors that are available, including additional ones like [DatePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor), to integrate edit boxes with PropertyGrid and how to do so in the new version.

### Events Renamed

While we rebuilt the infrastructure, we took some time to clarify and rename events as well.

- `ItemContextMenuOpening` event renamed to [ItemMenuRequested](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.ItemMenuRequested) and now uses [TreeListBoxItemMenuEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs).

- `PropertyChanged` event renamed to [PropertyValueChanged](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyValueChanged) and now uses [PropertyModelValueChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelValueChangeEventArgs).

- `PropertyChanging` event renamed to [PropertyValueChanging](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyValueChanging) and now uses [PropertyModelValueChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelValueChangeEventArgs).

- `PropertyChildAdded` event renamed to [ChildPropertyAdded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ChildPropertyAdded) and now uses [PropertyModelChildChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelChildChangeEventArgs).

- `PropertyChildAdding` event renamed to [ChildPropertyAdding](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ChildPropertyAdding) and now uses [PropertyModelChildChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelChildChangeEventArgs).

- `PropertyChildRemoved` event renamed to [ChildPropertyRemoved](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ChildPropertyRemoved) and now uses [PropertyModelChildChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelChildChangeEventArgs).

- `PropertyChildRemoving` event renamed to [ChildPropertyRemoving](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ChildPropertyRemoving) and now uses [PropertyModelChildChangeEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.PropertyModelChildChangeEventArgs).

- `SelectedObjectsChanged` event removed.

### Modified Property Display Removed

In an effort to maximize performance and simplify the elements and logic in the name and value cell presenatation, we removed the extra logic needed to support modified property display.  The related `ModifiedPropertyDisplayMode` property was removed.  It is possible to add back in custom display by altering the [property editor](../grids/propertygrid-features/property-editors.md) templates or even adding an [additional column](../grids/propertygrid-features/columns.md) that shows modified states.

### Sorting Changes

The way data models at each level are sorted has changed.  The `AreDefaultSortDescriptionsEnabled` and `SortDescriptions` properties have been removed.  Use the [SortComparer](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.SortComparer) property instead, which requires an instance of [DataModelSortComparer](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.DataModelSortComparer) or a derived class.  The property can be set to `null` to disable default sorting.

### Filtering Changes

The filtering model has been reworked to an extent and large portions of it moved to the Shared Library.  in the [ActiproSoftware.Windows.Data.Filtering](xref:ActiproSoftware.Windows.Data.Filtering) namespace.  The property grid-specific filters (e.g. [PropertyModelStringFilter](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.PropertyModelStringFilter) and [PropertyModelBooleanFilter](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.PropertyModelBooleanFilter)) are now in the [ActiproSoftware.Windows.Controls.Grids.PropertyData](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData) namespace.

### Summary Area Changes

The `SummaryCanAutoSize` property was renamed to [CanSummaryAutoSize](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.CanSummaryAutoSize).  The `SummaryTransitionDuration` and `SummaryTransitionSelector` properties were removed, since transitions are no longer used in the summary area in an effort to simply the default templates.

### List Modifiers Removed

The list modifier features that were used in certain scenarios to help configure property editors in XAML have been removed.  There is now a new mechanism for specifying the default property editors in XAML, as described in the [Property Editors](../grids/propertygrid-features/property-editors.md) topic.

### Other Changes

These other miscellaneous changes were made to the `PropertyGrid` class:

- `AreEmptyAccessorsFiltered` property removed, as it is no longer needed.
- `BeginUpdate` and `EndUpdate` optimization methods removed, since performance is very fast in the new version even without them.
- `CollectionConverter` property removed, which was used by `CollectionPropertyDescriptorDataAccessor`.  The new virtual [CollectionPropertyDescriptorPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.CollectionPropertyDescriptorPropertyModel).[CreateExpandableCollectionConverter](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.CollectionPropertyDescriptorPropertyModel.CreateExpandableCollectionConverter*) method now creates the [ExpandableCollectionConverter](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.ExpandableCollectionConverter) to use.
- `CollectionDisplayMode` property renamed to [CollectionPropertyDisplayMode](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.CollectionPropertyDisplayMode) and related `PropertyGridCollectionDisplayMode` enum renamed to [CollectionPropertyDisplayMode](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.CollectionPropertyDisplayMode).
- `CommitPendingChanges` method renamed to [TryCommitPropertyValueEdit](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.TryCommitPropertyValueEdit*).
- `EscapeKeyDownDelegates` property renamed to [CancelPropertyValueEditHandlers](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.CancelPropertyValueEditHandlers) and the dictionary values are now of type [PropertyGridItemActionHandler](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGridItemActionHandler).
- `TextInputFocusDelegates` property renamed to [StartPropertyValueEditHandlers](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.StartPropertyValueEditHandlers) and the dictionary values are now of type [PropertyGridItemActionHandler](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGridItemActionHandler).
- `Hint`, `HintTemplate`, and `HintTemplateSelector` properties removed.  Use the new [EmptyContent](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.EmptyContent) and [EmptyContentTemplate](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.EmptyContentTemplate) properties instead.
- `IsAsynchronous` and `IsItemsSourceAutoUpdated` properties removed since they are no longer needed due to performance improvements.
- The `NameCellContainerStyle`, `NameTemplateSelector`, `ValueCellContainerStyle`, and `ValueTemplateSelector` properties have been replaced by a new infrastructure.  See the [Property Editors](../grids/propertygrid-features/property-editors.md) topic for an explanation of how things work in the new version.
- The static `ShowPropertyDialogCommand` property has been removed, which helped support dialog display buttons.  This feature is still present but in an improved design.  See the "Property Dialogs" section in the [Property Editors](../grids/propertygrid-features/property-editors.md) topic for more information on how to implement property dialogs.

### Temporary Inclusion of Older PropertyGrid and Interop Legacy Assemblies

We recognize that for the 17.1 version, numerous breaking changes were made and that you might not be able to immediately update to the newer logic.  While we fully encourage you to do so since all future updates and maintenance will only be done on this newer Grids codebase, we still offer the older PropertyGrid version along with its related interop assemblies.

These older legacy assemblies are not installed by default.  You must check the 'Control Assemblies / Legacy' option in the installer to install them.  Once you do so, these PropertyGrid-related assemblies will appear in a 'Legacy' subfolder of the Actipro assembly install folder (see the [Deployment](../deployment.md) topic for the default location), but will not appear in the GAC:

- ActiproSoftware.PropertyGrid.Legacy.Wpf.dll
- ActiproSoftware.PropertyGrid.Interop.WinForms.Legacy.Wpf.dll

You will be able to reference the `ActiproSoftware.PropertyGrid.Legacy.Wpf` assembly in place of the new `ActiproSoftware.Grids.Wpf` assembly to maintain full backward compatibility with the old version, while you work on upgrading your application to the new version.

See the [Licensing](../licensing.md) topic for info on how to add an entry to the `licenses.licx` file (updated info added in v17.1).

Do NOT reference both the `ActiproSoftware.Grids.Wpf` and `ActiproSoftware.PropertyGrid.Legacy.Wpf` assemblies (or similar for interop assemblies) in the same project, since they have many types/members named the same.

Again, these legacy assemblies will only be included temporarily.  We encourage you to upgrade your code to the latest Grids assembly as soon as it is feasible so that you can continue to receive the latest product feature additions and bug fixes.
