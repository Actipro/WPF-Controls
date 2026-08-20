---
title: "Converting to v26.1"
page-title: "Converting to v26.1 - Conversion Notes"
order: 83
---
# Converting to v26.1

The 26.1 version made a number of infrastructure updates and improvements.

## .NET Targets Changes

.NET 6 and 7 are out of support, so targets for `net6.0` and `net6.0-windows` have been removed.  Windows-based projects must target .NET 8 or higher.

This release adds new targets for `net10.0` and `net10.0-windows`.

## Nullable Reference Type (NRT) Annotations

This release adds full nullable reference type annotations to the public API.  These annotations improve static analysis and help callers understand when values may be null. Existing behavior is unchanged at runtime, but you may see new compiler warnings when upgrading. Review your code for null‑safety where needed.

Every attempt should be made to avoid warnings that are flagged by static code analysis, but do not be discouraged if a lot of warnings are displayed for existing code after the upgrade.  Just because something *can* be `null` does not mean it *will* be `null`.  For example, several class properties that are not normally `null` still had to be flagged at nullable just because the property or its backing field were set to `null` when disposed.

Even though there may not be issues, we strongly encourage all warnings to be researched and addressed as soon as possible to avoid throwing `NullReferenceException` at run-time.

> [!TIP]
> If your environment is configured to treat warnings as errors, you may want to temporarily disable that configuration and resolve all true errors before addressing the nullable-aware changes.

## Licensing Updates

The licensing infrastructure and the license dialog have been refactored and improved in this version.  The license dialog now has a simpler design that makes it easier to understand what triggered the dialog display and allows copying of that information for submission to Actipro support when needed.  Licensing-related types have been moved to the new `ActiproSoftware.Licensing` namespace.

### RegisterLicense Calls

Any calls to the [ActiproLicenseManager](xref:ActiproSoftware.Licensing.ActiproLicenseManager).[RegisterLicense](xref:ActiproSoftware.Licensing.ActiproLicenseManager.RegisterLicense*) method need their namespace changed to avoid this this compile error:

```
error CS0234: The type or namespace name 'ActiproLicenseManager' does not exist in the namespace 'ActiproSoftware.Products'
```

> [!IMPORTANT]
> Change the namespace by updating `ActiproSoftware.Products.ActiproLicenseManager.RegisterLicense(...)` calls to `ActiproSoftware.Licensing.ActiproLicenseManager.RegisterLicense(...)`.

The old [RegisterLicense](xref:ActiproSoftware.Licensing.ActiproLicenseManager.RegisterLicense*) method had two overloads.  One overload allowed for an `AssemblyInfo` to be specified when the license information being set only applied to a single product.  This is useful for an app plugin that uses Actipro controls, so that it won't interfere with any other license registration calls made by the app itself, or other plugins.  In the new API, there is a single [RegisterLicense](xref:ActiproSoftware.Licensing.ActiproLicenseManager.RegisterLicense*) method, but the [AssemblyInfoBase](xref:ActiproSoftware.Properties.AssemblyInfoBase) parameter is optional at the end.  Leave it `null` to register a single license for all used Actipro products, which is the default usage scenario.

### Licenses.licx Files

If the older `licenses.licx` way of licensing is used (classic .NET Framework apps only), the token type referenced in the `licenses.licx` file entry needs its namespace changed.

> [!IMPORTANT]
> Change the namespace by updating the `ActiproSoftware.Products.ActiproLicenseToken, ...` line to `ActiproSoftware.Licensing.ActiproLicenseToken, ...`.

> [!TIP]
> This may be a good time to convert over to the `RegisterLicense` way of licensing from the older `licenses.licx` way of licensing.  Please see the [Licensing](../licensing.md) topic for more information on converting.

### Licenses.licx Assembly Scanning Logic Changes

In the past when looking for stored license information in an assembly context, the licensing logic would scan the entry (application) assembly first, then any hint assemblies (via [ActiproLicenseManager](xref:ActiproSoftware.Licensing.ActiproLicenseManager).`AddHintAssemblyName` calls), and finally all assemblies in the app domain.  Since licensing is migrating more towards [RegisterLicense](xref:ActiproSoftware.Licensing.ActiproLicenseManager.RegisterLicense*) calls and away from `licenses.licx` files, we simplified this logic to exclude the final step of scanning all assemblies in the app domain.  From now on, when using `licenses.licx` licensing, if the `licenses.licx` file is not in the entry (application) assembly, you must use an [ActiproLicenseManager](xref:ActiproSoftware.Licensing.ActiproLicenseManager).`AddHintAssemblyName` call to designate an additional assembly to examine.

> [!IMPORTANT]
> If you use `licenses.licx` licensing, check the deployment of your app on a clean end user machine to verify licensing is working as intended after making necessary changes.

## Core Library

A new Core library has been added in this version to contain fundamental classes used throughout our product line, and all the Actipro assemblies reference it.  A number of core types used throughout our UI and non-UI assemblies have been migrated into this new Core library.

> [!IMPORTANT]
> If your application uses assembly references to Actipro products, you must add a reference to the `ActiproSoftware.Core.@@PlatformAssemblySuffix.dll` assembly to ensure that all Actipro types are properly resolved.  Customers using Actipro's NuGet packages for references will not need to make any changes.

### ObservableObjectBase Migrated

The [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) class was moved from its previous namespace `ActiproSoftware.Windows` to the Core library in the `ActiproSoftware` namespace.  The class now implements both the `IPropertyChanging` and `IPropertyChanged` interfaces.

The older implementation of the class used a `NotifyPropertyChanged` method to notify of property changes.  A new [SetProperty](xref:ActiproSoftware.ObservableObjectBase.SetProperty*) method replaces that method and performs these tasks:

- Checks for equality between the existing backing field value and the new value being set.  The method return value is `true` if the value was changed.
- Raises the [PropertyChanging](xref:ActiproSoftware.ObservableObjectBase.PropertyChanging) event.
- Updates the field value with the new value.
- Raises the [PropertyChanging](xref:ActiproSoftware.ObservableObjectBase.PropertyChanged) event.

This code shows how a class that inherits [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) can use the [SetProperty](xref:ActiproSoftware.ObservableObjectBase.SetProperty*) method to update the backing field and raise events:

```csharp
public class WindowModel : ObservableObjectBase {

	private bool _canClose;

	public bool CanClose {
		get => _canClose;
		set => SetProperty(ref _canClose, value);
	}
}
```

> [!TIP]
> Migrate any prior `ObservableObjectBase` usage to the [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) class in the Core library.
> Update any usage of the `NotifyPropertyChanged` method to the new [SetProperty](xref:ActiproSoftware.ObservableObjectBase.SetProperty*) method.
>
> For any scenarios where updating a backing field is not required, call the [OnPropertyChanged](xref:ActiproSoftware.ObservableObjectBase.OnPropertyChanged*) method instead of the `NotifyPropertyChanged` method.
>
> To ensure a consistent API, all classes that implement `INotifyPropertyChanged` that do not derive from `ObservableObjectBase` have also deprecated the `NotifyPropertyChanged` method in favor of new `OnPropertyChanged` and `SetProperty` methods that match `ObservableObjectBase`.

### DisposableObjectBase Migrated

The [DisposableObjectBase](xref:ActiproSoftware.DisposableObjectBase) class was moved from its previous namespace `ActiproSoftware.Windows` to the Core library in the `ActiproSoftware` namespace.  Its [Dispose](xref:ActiproSoftware.DisposableObjectBase.Dispose*) method is now `abstract` instead of being `virtual` with an empty implementation.

For further consolidation, the `ActiproSoftware.Text.Utility.DisposableObject` class has been removed from the Text assembly and [DisposableObjectBase](xref:ActiproSoftware.DisposableObjectBase) is now used as the base class for all objects that previously used `DisposableObject`.

> [!TIP]
> Migrate any prior `DisposableObjectBase` or `DisposableObject` usage to the [DisposableObjectBase](xref:ActiproSoftware.DisposableObjectBase) class in the Core library.

### Extension Method Classes Migrated

Several extension method classes that apply to non-UI .NET types were moved from their previous `ActiproSoftware.Windows.Extensions` namespace to the Core library in the `ActiproSoftware.Extensions` namespace.  These extension method classes are not commonly used outside of Actipro-written code.

Moved classes include:

- [DateTimeExtensions](xref:ActiproSoftware.Extensions.DateTimeExtensions)
  - Also moved the [DateTimeFormatPattern](xref:ActiproSoftware.DateTimeFormatPattern) enumeration used by the extension methods to the Core library in the `ActiproSoftware` namespace.
- [DayOfWeekExtensions](xref:ActiproSoftware.Extensions.DayOfWeekExtensions)
  - Also moved the [DayOfWeekFormatPattern](xref:ActiproSoftware.DayOfWeekFormatPattern) enumeration used by the extension methods to the Core library in the `ActiproSoftware` namespace.
- [DoubleExtensions](xref:ActiproSoftware.Extensions.DoubleExtensions)
  - Some methods were renamed:
    - `IsEffectivelyEqual` renamed to [IsCloseTo](xref:ActiproSoftware.Extensions.DoubleExtensions.IsCloseTo*).
    - `IsEffectivelyGreaterThan` renamed to [IsGreaterThan](xref:ActiproSoftware.Extensions.DoubleExtensions.IsGreaterThan*).
    - `IsEffectivelyGreaterThanOrEqual` renamed to [IsGreaterThanOrCloseTo](xref:ActiproSoftware.Extensions.DoubleExtensions.IsGreaterThanOrCloseTo*).
    - `IsEffectivelyLessThan` renamed to [IsLessThan](xref:ActiproSoftware.Extensions.DoubleExtensions.IsLessThan*).
    - `IsEffectivelyLessThanOrEqual` renamed to [IsLessThanOrCloseTo](xref:ActiproSoftware.Extensions.DoubleExtensions.IsLessThanOrCloseTo*).
    - `IsEffectivelyZero` renamed to [IsZero](xref:ActiproSoftware.Extensions.DoubleExtensions.IsZero*).
    - `Range` renamed to [IsZero](xref:ActiproSoftware.Extensions.DoubleExtensions.ClampToRange*).
- [Int32Extensions](xref:ActiproSoftware.Extensions.Int32Extensions)
  - Some methods were renamed:
    - `Range` renamed to [IsZero](xref:ActiproSoftware.Extensions.Int32Extensions.ClampToRange*).
- [ListExtensions](xref:ActiproSoftware.Extensions.ListExtensions)

> [!TIP]
> Migrate any prior extension method usage to the extension methods in the `ActiproSoftware.Extensions` namespace in the Core library.

### WeakEventListener Class Migrated

The [WeakEventListener\<T,U\>](xref:ActiproSoftware.WeakEventListener`2) class, not commonly used outside of Actipro-written code, was moved to the Core library in the `ActiproSoftware` namespace.

### Logging Types Namespace Changes

The logging-related types in the `ActiproSoftware.Products.Logging` namespace, not commonly used outside of Actipro-written code, have been moved to the Core library in the `ActiproSoftware.Logging` namespace.

> [!TIP]
> Find `ActiproSoftware.Products.Logging` and replace with `ActiproSoftware.Logging` to convert any references to affected types.

### RoundMode Migrated

The [RoundMode](xref:ActiproSoftware.RoundMode) enum was moved to the Core library in the `ActiproSoftware` namespace.

The following values were also renamed:
- `Round` renamed to [Nearest](xref:ActiproSoftware.RoundMode.Nearest).
- `RoundEven` renamed to [NearestEven](xref:ActiproSoftware.RoundMode.NearestEven).
- `RoundOdd` renamed to [NearestOdd](xref:ActiproSoftware.RoundMode.NearestOdd).

## SyntaxEditor

This release includes several fundamental changes to the SyntaxEditor product.  Most changes are breaking and will result in compiler errors until they are resolved, but others, like line terminator processing, will not be exposed by the compiler.  The following summaries the changes in this release, and each concept is explained in more detail below:

- Various structures have replaced special "deleted" or "empty" concepts with nullable values instead.  Some have also been streamlined or updated.
- Line terminators are now preserved instead of normalized to line feeds.
- Several types renamed to fix "mergable" misspelling.
- The [PlainText](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage.PlainText) syntax language is now a shared instance.
- `DisplayItemClassificationTypeProvider` merged into [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider).
- [AstNodeBase](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase).[Children](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase.Children) changed from `IList<IAstNode>` to `IEnumerable<IAstNode>`.
- `ITextBufferReader` split into two interfaces with several members renamed for clarity.
- Several types moved to new Core assembly.

### Refactoring of How Various Structures Implement Deleted and Empty Concepts

Past versions of SyntaxEditor had important structures with special properties to indicate if they were "deleted" or "empty".  Part of v26.1 SyntaxEditor refactoring involved removing those properties and moving to a modern nullable concept instead.  The benefit of this is that the compiler can warn when possible "deleted" or "empty" values are returned, forcing better code to be written.

#### Modified Structures and Their Original Properties

This list provides details on the structures that were modified as part of the update and how their now-removed properties behaved.

- [Range](xref:ActiproSoftware.Text.Utility.Range) struct
  - `Empty` property returned a `-1, -1` pair of offsets.
  - `IsEmpty` property returned if the range was a `-1, -1` pair.
- [TextBounds](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextBounds) struct
  - `Empty` property was based on the bounds of @if (winforms) { `Rectangle.Empty` }@if (wpf) { `Rect.Empty` } and [IsRightToLeft](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextBounds.IsRightToLeft) of `false`.
  - `IsEmpty` property returned if the [Width](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextBounds.Width) was less than `0`.
- [TextPosition](xref:ActiproSoftware.Text.TextPosition) struct
  - `Empty` property returned a `-1, -1` pair of line/char.
  - `IsEmpty` property returned if the line/char was a `-1, -1` pair.
- [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) struct
  - `Empty` property returned an `Empty, Empty` pair of `TextPosition` objects.
  - `IsEmpty` property returned if the `TextPosition` objects were an `Empty, Empty` pair.
- [TextRange](xref:ActiproSoftware.Text.TextRange) struct
  - `Deleted` property returned a `-1, -1` pair of offsets.
  - `IsDeleted` property returned if the range was a `-1, -1` pair.
- [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) struct
  - `Deleted` property returned a `null` snapshot and `-1` offset.
  - `IsDeleted` property returned if the struct had a `null` snapshot or negative offset.
- [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) struct
  - `Deleted` property returned a `null` snapshot and `TextRange.Deleted`.
  - `IsDeleted` property returns if the struct had a `null` snapshot or `TextRange.Deleted`.

After the code updates, a `null` value indicates a "deleted" or "empty" state for the structures above.  For instance, a variable that is declared with type `TextRange?` (effectively `Nullable<TextRange>`) will be non-`null` when it has valid values and will be `null` when it is considered deleted.

#### Breaking Changes

The following list of breaking changes were made for this update.

- [CodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetProvider) - [GetPossibleShortcutSnapshotRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetProvider.GetPossibleShortcutSnapshotRange*) method return value is now nullable.
- [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) - [FindContaining](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder.FindContaining*) method and related methods on implementation classes now have nullable return values.
- [ICollapsedRegionManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager) - [GetCollapsedRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager.GetCollapsedRange*) method and related methods on implementation classes now have nullable return values.
- [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) - [LocationToPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.LocationToPosition*) method and related methods on implementation classes now return a nullable value.
- [IHitTestResult](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult) - [Position](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Position) property and related properties on implementation classes are now nullable.
- [IIntelliPromptSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession) - [SnapshotRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.SnapshotRange) property and related properties on implementation classes are now nullable.
- [IParseError](xref:ActiproSoftware.Text.Parsing.IParseError) - [PositionRange](xref:ActiproSoftware.Text.Parsing.IParseError.PositionRange) property and related properties on implementation classes are now nullable.
- [ITextVersionRange](xref:ActiproSoftware.Text.ITextVersionRange) - [Translate](xref:ActiproSoftware.Text.ITextVersionRange.Translate*) method and related methods on implementation classes now have nullable return values.
- [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) - [LocationToPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.LocationToPosition*) method and related methods on implementation classes now return a nullable value.
- [ITextViewLineCollection](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineCollection) - [SnapshotRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineCollection.SnapshotRange) property and related properties on implementation classes are now nullable.
- [Range](xref:ActiproSoftware.Text.Utility.Range)
  - Removed the `Empty` and `IsEmpty` properties and updated APIs to use `null` as a replacement for the "empty" concept.
  - [Intersect](xref:ActiproSoftware.Text.Utility.Range.Intersect*) method now has a nullable return value.
- [TextBounds](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextBounds) - Removed the `Empty` and `IsEmpty` properties and updated APIs to use `null` as a replacement for the "empty" concept.
- [TextPosition](xref:ActiproSoftware.Text.TextPosition) - Removed the `Empty` and `IsEmpty` properties and updated APIs to use `null` as a replacement for the "empty" concept.
- [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) - Removed the `Empty` and `IsEmpty` properties and updated APIs to use `null` as a replacement for the "empty" concept.
- [TextRange](xref:ActiproSoftware.Text.TextRange)
  - Removed the `Deleted` and `IsDeleted` properties and updated APIs to use `null` as a replacement for the "deleted" concept.
  - [Intersect](xref:ActiproSoftware.Text.TextRange.Intersect*) method now has a nullable return value.
  - [Translate](xref:ActiproSoftware.Text.TextRange.Translate*) method now has a nullable return value.
- [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) - Removed the `Deleted` and `IsDeleted` properties and updated APIs to use `null` as a replacement for the "deleted" concept.
- [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange)
  - Removed the `Deleted` and `IsDeleted` properties and updated APIs to use `null` as a replacement for the "deleted" concept.
  - [TranslateTo](xref:ActiproSoftware.Text.TextSnapshotRange.TranslateTo*) method now has a nullable return value.

##### .NET Languages Add-on

- [ISourceFileLocation](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ISourceFileLocation) - [TextRange](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.ISourceFileLocation.TextRange) property and related properties on implementation classes are now nullable.

##### Web Language Add-on

- [IXmlElementContext](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext) - [StartTagNameSnapshotRange](xref:ActiproSoftware.Text.Languages.Xml.IXmlElementContext.StartTagNameSnapshotRange) property and related properties on implementation classes are now nullable.

##### Python Language Add-on

- [IFieldDefinition](xref:ActiproSoftware.Text.Languages.Python.Reflection.IFieldDefinition) - [TypeExpressionTextRange](xref:ActiproSoftware.Text.Languages.Python.Reflection.IFieldDefinition.TypeExpressionTextRange) property and related properties on implementation classes are now nullable.
- [IFunctionDefinition](xref:ActiproSoftware.Text.Languages.Python.Reflection.IFunctionDefinition) - [ReturnTypeExpressionTextRange](xref:ActiproSoftware.Text.Languages.Python.Reflection.IFunctionDefinition.ReturnTypeExpressionTextRange) and [TextRange](xref:ActiproSoftware.Text.Languages.Python.Reflection.IFunctionDefinition.TextRange) properties and related properties on implementation classes are now nullable.
- [IParameterDefinition](xref:ActiproSoftware.Text.Languages.Python.Reflection.IParameterDefinition) - [TypeExpressionTextRange](xref:ActiproSoftware.Text.Languages.Python.Reflection.IParameterDefinition.TypeExpressionTextRange) property and related properties on implementation classes are now nullable.
- [ITypeDefinition](xref:ActiproSoftware.Text.Languages.Python.Reflection.ITypeDefinition) - [TextRange](xref:ActiproSoftware.Text.Languages.Python.Reflection.ITypeDefinition.TextRange) property and related properties on implementation classes are now nullable.
- [IVariableDefinition](xref:ActiproSoftware.Text.Languages.Python.Reflection.IVariableDefinition) - [TypeExpressionTextRange](xref:ActiproSoftware.Text.Languages.Python.Reflection.IVariableDefinition.TypeExpressionTextRange) property and related properties on implementation classes are now nullable.

#### Checking for Valid Values

In cases where a nullable result is now in a return value, use a `HasValue` check to see if a value is provided, and the `Value` property to access the value if it is there.

```csharp
public void PrintParseError(IParseError error) {
	if (error.PositionRange.HasValue)
		Debug.WriteLine($"Error at {error.PositionRange.Value}");
}
```

### Line Terminator Updates

Past versions of SyntaxEditor would normalize all document text to a single character LF (line feed or `"\n"`) only.  This made lexing, parsing, and other operations more efficient since it avoided the need for complex logic to watch for possible two-character line terminators like CRLF (`"\r\n"`).  Overloads for document snapshot methods like [GetText](xref:ActiproSoftware.Text.ITextSnapshot.GetText*) and [GetSubstring](xref:ActiproSoftware.Text.ITextSnapshot.GetSubstring*) allowed you to convert the line terminators back to CRLF form, or something else if desired.  All offsets were consistent zero-based integers relative to the document character positions where LF-only was used for line terminators.

While this has worked for many years, there are some cases where this behavior is not ideal.  For instance, if an offset is provided by an external source for CRLF line terminated document text, the offset would not be the same as the SyntaxEditor document offsets, due to the normalization of CRLF into LF within SyntaxEditor.  When opening a file, it's always helpful to know if the document text has consistent line terminators throughout, and if so, what they are.  The application may wish to display the document's line terminator kind in a status bar, or show "Mixed" when multiple line terminators are used, and allow the document text to be normalized to another line terminator kind.  These are some of the reasons that we decided to overhaul the internals of how SyntaxEditor processes line terminators for v26.1.

#### No More LF Normalization

SyntaxEditor will no longer normalize document text to LF-only line terminators, and instead will now retain whatever line terminator was used when opening a file.  If an opened file had CRLF line terminators, the document text will now have a CR (`"\r"`) character followed by a LF (`"\n"`) character at each line terminator.  Other single character line terminators and even a mix of multiple line terminators are supported.

The [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) enumeration has been updated with all the supported line terminators:

- [CRLF](xref:ActiproSoftware.Text.LineTerminator.CRLF) (`"\r\n"`) - Carriage return and line feed sequence.  This format is typically used on Windows machines.
- [LF](xref:ActiproSoftware.Text.LineTerminator.LF) (`"\n"`) - Line feed.  This format is typically used on UNIX and macOS machines.
- [CR](xref:ActiproSoftware.Text.LineTerminator.CR) (`"\r"`) - Carriage return.  Not commonly used.

> [!IMPORTANT]
> Any custom document character-scanning logic such as in a programmatic lexer or parser must be updated to support all line terminator characters now, and possible CRLF sequences.

> [!NOTE]
> A new [ExperimentalFeatures](xref:ActiproSoftware.Properties.Text.ExperimentalFeatures).[AllLineTerminatorsPreserved](xref:ActiproSoftware.Properties.Text.ExperimentalFeatures.AllLineTerminatorsPreserved) property that defaults to `true` has been added.  Set this property to `false` to force line terminators to be normalized to LF within document snapshots.

#### Runtime Regular Expression Searching

Regular expression searching used to find line terminators with `\n` only.  Now that multiple kinds of line terminators can be present in document text, regular expression searching must specify the line terminator characters present in document text for matches to be made.

#### LineTerminator Member Name Updates

These original [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) values are still present, but have been deprecated and map over to the newer, shorter related values.  Update your code to switch to the new values:

- `CarriageReturnNewline` - Use [CRLF](xref:ActiproSoftware.Text.LineTerminator.CRLF) instead.
- `Newline` - Use [LF](xref:ActiproSoftware.Text.LineTerminator.LF) instead.
- `CarriageReturn` - Use [CR](xref:ActiproSoftware.Text.LineTerminator.CR) instead.

#### Document Snapshot Line Terminator Uniformity

The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[HasUniformLineTerminators](xref:ActiproSoftware.Text.ITextSnapshot.HasUniformLineTerminators) property returns whether all of the line terminators in the document snapshot are the same.  When this property returns `false`, the line terminators are considered mixed.

#### Document Snapshot Inferred Line Terminator

The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[InferredLineTerminator](xref:ActiproSoftware.Text.ITextSnapshot.InferredLineTerminator) property returns a [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) value indicating which line terminator should be used in the document.  The value returned is based on what is identified in the document text.

In the case of mixed line terminators, priority is given to line terminators in the order of the list above.  A document with one CRLF line terminator and two LF line terminators in its text will return CRLF as the inferred line terminator, since the presence of any CRLF has a higher priority than LF.

When a document is empty, there are no line terminators from which to infer a result.  The system's line terminator via `Environment.NewLine` will be used to infer a result in that case.

#### Dynamic Lexer Line Terminator Matching

An [IDynamicLexer](xref:ActiproSoftware.Text.Lexing.Implementation.IDynamicLexer).[CanLineFeedMatchAnyLineTerminator](xref:ActiproSoftware.Text.Lexing.Implementation.IDynamicLexer.CanLineFeedMatchAnyLineTerminator) property was added with a default of `true` to allow `\n` specifications to match any line terminator and minimize breaking changes with previous versions. Change it to `false` to only match LF with `\n`.

#### Breaking Changes

The following list of breaking changes were made for this update.

- [ITextDocument](xref:ActiproSoftware.Text.ITextDocument)
  - [LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) method no longer returns a [LineTerminator](xref:ActiproSoftware.Text.LineTerminator). Use the new [InferredLineTerminator](xref:ActiproSoftware.Text.ITextSnapshot.InferredLineTerminator) property from the document's current snapshot following load instead.
  - [SaveFile](xref:ActiproSoftware.Text.ITextDocument.SaveFile*) method now has an optional [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument that should only be specified when line ends should be normalized to a certain line terminator in the saved file.
- [ITextExporter](xref:ActiproSoftware.Text.Exporters.ITextExporter).[LineTerminator](xref:ActiproSoftware.Text.Exporters.ITextExporter.LineTerminator) property now is nullable, where the default `null` value means to use the inferred line terminator from the source snapshot.
- [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot)
  - [GetSubstring](xref:ActiproSoftware.Text.ITextSnapshot.GetSubstring*) methods now have an optional [LineTerminator](xref:ActiproSoftware.Text.LineTerminator). If left `null`, the default, no line end normalization will take place.
  - [GetText](xref:ActiproSoftware.Text.ITextSnapshot.GetText*) method now has an optional [LineTerminator](xref:ActiproSoftware.Text.LineTerminator). If left `null`, the default, no line end normalization will take place.
  - [Text](xref:ActiproSoftware.Text.ITextSnapshot.Text) property effectively calls `GetText(null)`, returning text without any sort of line terminator normalization, whereas the property used to normalize to CRLF line terminators.
- [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.Text) property no longer normalizes any contained line terminators to LF.
- [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader).[GetTokenText](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader.GetTokenText*) method no longer normalizes line terminators to LF.
- [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) enum updated with shorter value names and additional values. Old longer value names are retained temporarily, but are obsolete and should be replaced with the newer shorter names. The type has moved to Core assembly.

### Mergeable Misspelling Fix

Numerous lexer-related types in SyntaxEditor were originally misspelled with the name "mergable" (not a word) instead of the proper word "mergeable".  This originated due to .NET itself having a `System.ComponentModel.MergablePropertyAttribute` class, and us going with that term assuming it was correct spelling when adding our own classes.  After release, we realized that it wasn't a proper spelling of the word, however we avoided making any changes to correct it since it would lead to multiple breaking changes.

Years later and as we deep dive into making some core SyntaxEditor infrastructure improvements, we feel that v26.1 is an appropriate time to correct the instances of the misspelled word.  All types, members, comments, and documentation have been updated to the proper spelling in this version.

> [!IMPORTANT]
> This set of updates will cause compilation errors in any code that references the term "mergable", such as type or member not found errors.  The simplest way to migrate code is to do a solution-wide search for `"ergable"` and replace it with `"ergeable"` where appropriate.

Be careful not to update any usage of .NET's `MergablePropertyAttribute`, since that will continue to use the misspelled term.

The following types and members have been affected by this update:

- `IMergableLexer` interface renamed to [IMergeableLexer](xref:ActiproSoftware.Text.Lexing.IMergeableLexer).
- `IMergableToken` interface renamed to [IMergeableToken](xref:ActiproSoftware.Text.Lexing.IMergeableToken).
- `IMergableTokenLexerData` interface renamed to [IMergeableTokenLexerData](xref:ActiproSoftware.Text.Lexing.IMergeableTokenLexerData).
- `MergableLexerBase` class renamed to [MergeableLexerBase](xref:ActiproSoftware.Text.Lexing.Implementation.MergeableLexerBase).
- `MergableLexerCoordinator` class renamed to [MergeableLexerCoordinator](xref:ActiproSoftware.Text.Lexing.Implementation.MergeableLexerCoordinator).
- `MergableLexerFlags` enum renamed to [MergeableLexerFlags](xref:ActiproSoftware.Text.Lexing.MergeableLexerFlags).
- `MergableLexerResult` class renamed to [MergeableLexerResult](xref:ActiproSoftware.Text.Lexing.MergeableLexerResult).
- `MergableToken` class renamed to [MergeableToken](xref:ActiproSoftware.Text.Lexing.Implementation.MergeableToken).
- `MergableTokenReader` class renamed to [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergeableTokenReader).
- `SRName.ExNoMergableLexer` enum value renamed to [SRName.ExNoMergeableLexer](xref:ActiproSoftware.Properties.Text.SRName.ExNoMergeableLexer).
- `SRName.ExNoMergableToken` enum value renamed to [SRName.ExNoMergeableToken](xref:ActiproSoftware.Properties.Text.SRName.ExNoMergeableToken).

### SyntaxLanguage.PlainText Updates

The [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage).[PlainText](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage.PlainText) property previously created a new instance of an empty [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) each time it was called.  However, it is better design to have properties return a cached instance instead.

In this version, the property now returns a cached instance.  This means that if you add language services to the [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage).[PlainText](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage.PlainText) instance, the same services will appear on any other document language that was previously assigned from that property.  This can be useful in scenarios where you may wish to assign a custom [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) or other service for plain text.

Note that [CodeDocument](xref:ActiproSoftware.Text.Implementation.CodeDocument) instances continue to be assigned a new syntax language instance that is not the value returned by the [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage).[PlainText](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage.PlainText) property.

### DisplayItemClassificationTypeProvider Merged into BuiltInClassificationTypeProvider

Previous versions had two predefined classification type provider classes that would register known classification types with default styles in a target [highlighting style registry](../syntaxeditor/user-interface/styles/highlighting-style-registries.md):

- `DisplayItemClassificationTypeProvider` - Would register classification types with keys from [DisplayItemClassificationTypeKeys](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeKeys).
- [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider) - Would register classification types with keys from [ClassificationTypeKeys](xref:ActiproSoftware.Text.ClassificationTypeKeys).

In this version, `DisplayItemClassificationTypeProvider` has been merged into [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider).  Simply replace code references from the one type to the other to convert.

> [!NOTE]
> The [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider).[RegisterAll](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider.RegisterAll*) method now registers all classification types previously registered by the separate two types.  Whereas the new [RegisterLanguageTextItems](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider.RegisterLanguageTextItems*) method only registers language text items (keyword, comment, string, etc.) and the new [RegisterDisplayItems](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider.RegisterDisplayItems*) method registers everything else (errors, selection, margin-related, etc.).  Use of one of those two methods instead of the [RegisterAll](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider.RegisterAll*) method may be warranted at times.

### AstNodeBase.Children Updates

In previous versions, the [AstNodeBase](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase).[Children](xref:ActiproSoftware.Text.Parsing.Implementation.AstNodeBase.Children) property created a new `List<IAstNode>` on each invocation when child AST nodes were present.  This was not efficient, especially when callers may have assumed that the list had been a cached list, and had referenced the property repeatedly.

In this version, the property has been updated to return `IEnumerable<IAstNode>` instead of `IList<IAstNode>`, and the AST nodes returned from the property are yielded.  This improves performance for many scenarios where simple enumeration is required, and even moreso when the enumeration doesn't require examination of all child AST nodes.

Some scenarios are more efficient when working with an `IList<IAstNode>`, such as when needing to know the total count of items and also using an indexer to get certain items.  In these scenarios, use LINQ's `ToList()` extension method to create an `IList<IAstNode>` with which you can work.

### Basic Structure Changes

Several core structures have been streamlined or updated.

#### TextPosition Updates

[TextPosition](xref:ActiproSoftware.Text.TextPosition) has had these members updates:
  - `Equals`, `CompareTo`, and `GetHashCode` methods - Comparison logic now includes the [HasFarAffinity](xref:ActiproSoftware.Text.TextPosition.HasFarAffinity) property value.  A new [CompareToWithoutAffinity](xref:ActiproSoftware.Text.TextPosition.CompareToWithoutAffinity*) method was added to do comparisons without consideration of [HasFarAffinity](xref:ActiproSoftware.Text.TextPosition.HasFarAffinity), similar to how `CompareTo` previously behaved.

#### TextRange Updates

[TextRange](xref:ActiproSoftware.Text.TextRange) is now a read-only struct and has had these members updated:

- `Invert` method - Removed since cannot be used with a read-only struct.
- `Normalize` method - Removed since cannot be used with a read-only struct.  Callers can use `range = range.Normalized` to accomplish the same behavior.
- `NormalizedTextRange` property - Replaced by the new shorter-named [Normalized](xref:ActiproSoftware.Text.TextRange.Normalized) property.

In addition, [TextRange](xref:ActiproSoftware.Text.TextRange) no longer implements [ITextRangeProvider](xref:ActiproSoftware.Text.ITextRangeProvider) itself.

#### TextPositionRange Updates

[TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) is now a read-only struct and has had these members updated:

- `Invert` method - Removed since cannot be used with a read-only struct.
- `Normalize` method - Removed since cannot be used with a read-only struct.  Callers can use `range = range.Normalized` to accomplish the same behavior.
- `NormalizedTextPositionRange` property - Replaced by the new shorter-named [Normalized](xref:ActiproSoftware.Text.TextPositionRange.Normalized) property.

#### TextSnapshotRange Updates

[TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) has had these members updated:

- `AbsoluteLength` property - Flagged obsolete and will be removed in the future since snapshot ranges are always normalized.  Use the [Length](xref:ActiproSoftware.Text.TextSnapshotRange.Length) property instead, which always returns a non-negative length already.
- [OverlapsWith](xref:ActiproSoftware.Text.TextSnapshotRange.OverlapsWith*) method - The overload with a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) parameter will now throw an exception if the two snapshots don't share the same document, similar to other [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) methods.  If this is a problem, check that the snapshots' documents are the same before calling the method.

### Text Buffer Reader Updates

The [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) interface has been refined and core portions of it moved into a new interface in the Core library.  These updates have been made:

- Fundamental portions of [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) split into a new [ISimpleTextBufferReader](xref:ActiproSoftware.Text.ISimpleTextBufferReader) interface, which [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) now inherits.  This is not a breaking change other than which interface defines the same members.
- `HasStackEntries` property - Renamed to [HasStates](xref:ActiproSoftware.Text.ITextBufferReader.HasStates) for better clarity.
- `Push` property - Renamed to [PushState](xref:ActiproSoftware.Text.ITextBufferReader.PushState*) for better clarity.
- `Pop` property - Renamed to [PopState](xref:ActiproSoftware.Text.ITextBufferReader.PopState*) for better clarity.
- `PopAll` method - Removed since not very useful.  If replacement logic is needed, call [PopState](xref:ActiproSoftware.Text.ITextBufferReader.PopState*) until [HasStates](xref:ActiproSoftware.Text.ITextBufferReader.HasStates) is false.

### Several Types Moved to New Core Assembly

This release includes a new Core assembly that is effectively meant to serve as a collection of basics types that are unrelated to any UI framework and can be easily supported across platforms.  The following types have been moved to the new Core assembly:
- [CaseSensitivity](xref:ActiproSoftware.Text.CaseSensitivity) enumeration.
- [CharClass](xref:ActiproSoftware.Text.RegularExpressions.CharClass) class.
- [CharInterval](xref:ActiproSoftware.Text.RegularExpressions.CharInterval) structure.
- [InvalidRegexPatternException](xref:ActiproSoftware.Text.RegularExpressions.InvalidRegexPatternException) class.
- [MatchType](xref:ActiproSoftware.Text.RegularExpressions.MatchType) enumeration.

### Other Notable Changes

In addition, the following notable changes have also been made in this release.

- The default value for [TextStylePreview](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextStylePreview).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextStylePreview.Text) was changed from `"AaBbCcXxYyZz"` to `"ij = I::oO(0xB81l);"`.  If the original value is preferred, it can be restored by explicitly setting the [Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextStylePreview.Text) property to the desired value.
- In cleaning up APIs, the setters have been removed from the [ITextRangeProvider](xref:ActiproSoftware.Text.ITextRangeProvider) and [ITextPositionRangeProvider](xref:ActiproSoftware.Text.ITextPositionRangeProvider) interfaces.  A provider should only return a value, and almost all implementations of that setter threw an unsupported exception.

#### Additional Breaking Changes

- [AstNodeMatch](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.AstNodeMatch)
  - Constructor now has a required [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) parameter.
  - [Node](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.AstNodeMatch.Node) property is now read-only.
- [CodeSnippetSelectionSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetSelectionSession)
  - Constructor now has a required [ICodeSnippetFolder](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetFolder) parameter.
  - [RootFolder](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetSelectionSession.RootFolder) property is now read-only.
- [CodeSnippetTemplateSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetTemplateSession)
  - Constructor now has a required [ICodeSnippet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippet) parameter.
  - [CodeSnippet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetTemplateSession.CodeSnippet) property is now read-only.
- [CollectionTagger\<T\>](xref:ActiproSoftware.Text.Tagging.Implementation.CollectionTagger`1) - `IntersectsWith` method renamed to [IntersectsWith](xref:ActiproSoftware.Text.Tagging.Implementation.CollectionTagger`1.IsTagIncluded*), made `public`, and its parameters were reordered for clarity.
@if (winforms) {
- [EditorDocument](xref:ActiproSoftware.Text.Implementation.EditorDocument) - Removed the `Text` property.  Text should be read through the [CurrentSnapshot](xref:ActiproSoftware.Text.ITextDocument.CurrentSnapshot).[Text](xref:ActiproSoftware.Text.ITextSnapshot.Text) property and assigned through the [SetText](xref:ActiproSoftware.Text.ITextDocument.SetText*) method.
}
- [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) - Static `GetNeutralForegroundColor` method renamed to [GetSecondaryTextForegroundColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider.GetSecondaryTextForegroundColor*) for better clarity.
- [IAstNodeMatch](xref:ActiproSoftware.Text.Parsing.LLParser.IAstNodeMatch) - [Node](xref:ActiproSoftware.Text.Parsing.LLParser.IAstNodeMatch.Node) property is now read-only.
- [ILexicalScopeStateNode](xref:ActiproSoftware.Text.Lexing.ILexicalScopeStateNode)
  - [LexicalScope](xref:ActiproSoftware.Text.Lexing.ILexicalScopeStateNode.LexicalScope) property no longer has a setter.
  - [Parent](xref:ActiproSoftware.Text.Lexing.ILexicalScopeStateNode.Parent) property no longer has a setter.
- [ILineIndicatorManager\<TTagger,TTag\>](xref:@ActiproUIRoot.Controls.SyntaxEditor.Indicators.ILineIndicatorManager`2) - Updated `TTag` to have a [ILineIndicatorTag](xref:ActiproSoftware.Text.Tagging.ILineIndicatorTag) constraint, which is a new interface to denote line-based indicators.
- [IParameterInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession) - [ControlKeyDownOpacity](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession.ControlKeyDownOpacity) property added to match the same property on the [ParameterInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.ParameterInfoSession) class.
- [NavigableSymbol](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.NavigableSymbol)
  - Constructor now has a required [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) parameter.
  - [SnapshotRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.NavigableSymbol.SnapshotRange) property is now read-only.
- `ObservableUndoableTextChangeStack` class removed since the internal `UndoableTextChangeStack` class now implements `INotifyCollectionChanged` directly.
- [QuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoSession)
  - Constructor now has a required `object` parameter to initialize [Context](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoSession.Context).
  - [Context](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoSession.Context) property is now read-only.
- `RegexHelper` static class removed and its methods moved elsewhere.
  - `Escape` method moved to the [MatchingRegexCode](xref:ActiproSoftware.Text.RegularExpressions.MatchingRegexCode) (find patterns) and [ReplacementRegexCode](xref:ActiproSoftware.Text.RegularExpressions.ReplacementRegexCode) (replace patterns) classes, renamed to `EscapePattern` in both.
  - `IsPatternSpecialChar` method moved to the [MatchingRegexCode](xref:ActiproSoftware.Text.RegularExpressions.MatchingRegexCode) class.
- [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage).[PlainText](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage.PlainText) static property now uses a cached instance of the syntax language, instead of creating a new one with each call.
- [SyntaxLanguageDefinitionSerializer](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer).`UseBuiltInClassificiationTypes` property renamed to `UseBuiltInClassificationTypes` to fix spelling error.

## MenuItem and RoutedUICommand Name Changes

As part of the new [Menu Factory](../shared/menu-factory.md) capabilities, several existing `MenuItem` and `RoutedUICommand` names have been updated to values based on available key constants.

### Docking

The following [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) context menu items have new names generated from key constants defined by `ActiproSoftware.Properties.Docking.CommandKeys`:

| Old Name | New Name | Related Constant |
| ----- | ----- | ----- |
| `"Activate1MenuItem"` (where `"1"` is a numerical position) | `"Docking_Activate_1"` | `CommandKeys.Activate` (prefix only) |
| `"CloseAllDocumentsMenuItem"` | `"Docking_CloseAllDocuments"` | `CommandKeys.CloseAllDocuments` |
| `"CloseAllInContainerMenuItem"` | `"Docking_CloseAll"` | `CommandKeys.CloseAll` |
| `"CloseOthersMenuItem"` | `"Docking_CloseOthers"` | `CommandKeys.CloseOthers` |
| `"CloseWindowMenuItem"` | `"Docking_Close"` | `CommandKeys.Close` |
| `"FloatAllInContainerMenuItem"` | `"Docking_FloatAll"` | `CommandKeys.FloatAll` |
| `"KeepTabOpenMenuItem"` | `"Docking_KeepTabOpen"` | `CommandKeys.KeepTabOpen` |
| `"MakeDockedWindowMenuItem"` | `"Docking_Dock"` | `CommandKeys.Dock` |
| `"MakeDocumentWindowMenuItem"` | `"Docking_MoveToMdi"` | `CommandKeys.MoveToMdi` |
| `"MakeFloatingWindowMenuItem"` | `"Docking_Float"` | `CommandKeys.Float` |
| `"MoveToNewHorizontalContainerMenuItem"` | `"Docking_MoveToNewHorizontalContainer"` | `CommandKeys.MoveToNewHorizontalContainer` |
| `"MoveToNewVerticalContainerMenuItem"` | `"Docking_MoveToNewVerticalContainer"` | `CommandKeys.MoveToNewVerticalContainer` |
| `"MoveToNextContainerMenuItem"` | `"Docking_MoveToNextContainer"` | `CommandKeys.MoveToNextContainer` |
| `"MoveToPreviousContainerMenuItem"` | `"Docking_MoveToPreviousContainer"` | `CommandKeys.MoveToPreviousContainer` |
| `"MoveToPrimaryMdiHostMenuItem"` | `"Docking_MoveToPrimaryMdi"` | `CommandKeys.MoveToPrimaryMdi` |
| `"PinTabMenuItem"` | `"Docking_PinTab"` | `CommandKeys.PinTab` |
| `"Select1MenuItem"` (where `"1"` is a numerical position) | `"Docking_SelectItem_1"` | `CommandKeys.SelectItem` (prefix only) |
| `"ToggleWindowAutoHideStateMenuItem"` | `"Docking_AutoHide"` | `CommandKeys.AutoHide` |

> [!IMPORTANT]
> @@PlatformName limits the value of `MenuItem.Name` to valid identifiers, so each name does not exactly match the defined key constant. See the [Menu Factory](../shared/menu-factory.md) topic for important details on how to properly identify a `MenuItem` based on the command key.

### Navigation

The following `RoutedUICommand` instances have new `Name` values that correspond to key constants defined by `ActiproSoftware.Properties.Navigation.CommandKeys`:

| RoutedUICommand | Old Name | New Name | Related Constant |
| ----- | ----- | ----- | ----- |
| [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb).[SelectPane](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands.SelectPane) | `"SelectPane"` | `"Navigation.Breadcrumb.SelectItem"` | `CommandKeys.NavigationBar.SelectItem` |
| [NavigationBarCommands](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands).[ShowFewerPanes](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands.ShowFewerPanes) | `"ShowFewerPanes"` | `"Navigation.NavigationBar.ShowFewerPanes"` | `CommandKeys.NavigationBar.ShowFewerPanes` |
| [NavigationBarCommands](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands).[ShowMorePanes](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands.ShowMorePanes) | `"ShowMorePanes"` | `"Navigation.NavigationBar.ShowMorePanes"` | `CommandKeys.NavigationBar.ShowMorePanes` |
| [NavigationBarCommands](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands).[ShowOptionsWindow](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands.ShowOptionsWindow) | `"ShowOptionsWindow"` | `"Navigation.NavigationBar.ShowOptionsWindow"` | `CommandKeys.NavigationBar.ShowOptionsWindow` |
| [NavigationBarCommands](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands).[ToggleMinimization](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands.ShowFewerPanes) | `"ToggleMinimization"` | `"Navigation.NavigationBar.ToggleMinimization"` | `CommandKeys.NavigationBar.ToggleMinimization` |
| [NavigationBarCommands](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands).[TogglePaneVisibility](xref:@ActiproUIRoot.Controls.Navigation.NavigationBarCommands.ShowFewerPanes) | `"TogglePaneVisibility"` | `"Navigation.NavigationBar.TogglePaneVisibility"` | `CommandKeys.NavigationBar.TogglePaneVisibility` |

#### Breaking Changes
- The [ZoomDecorator](xref:@ActiproUIRoot.Controls.Navigation.ZoomDecorator).`MakeVisible` method, which had no implementation, has been changed from `public` to an explicit implementation of the `IScrollInfo` interface.

### Shared Library

The following `RoutedUICommand` instances on [WindowChrome](xref:@ActiproUIRoot.Themes.WindowChrome) have new `Name` values that correspond to key constants defined by `ActiproSoftware.Properties.Shared.CommandKeys`:

| RoutedUICommand | Old Name | New Name | Related Constant |
| ----- | ----- | ----- | ----- |
| [CloseCommand](xref:@ActiproUIRoot.Themes.WindowChrome.CloseCommand) | `"Close"` | `"Window.Close"` | `CommandKeys.Window.Close` |
| [MaximizeCommand](xref:@ActiproUIRoot.Themes.WindowChrome.MaximizeCommand) | `"Maximize"` | `"Window.Maximize"` | `CommandKeys.Window.Maximize` |
| [MinimizeCommand](xref:@ActiproUIRoot.Themes.WindowChrome.MinimizeCommand) | `"Minimize"` | `"Window.Minimize"` | `CommandKeys.Window.Minimize` |
| [MoveCommand](xref:@ActiproUIRoot.Themes.WindowChrome.MoveCommand) | `"Move"` | `"Window.Move"` | `CommandKeys.Window.Move` |
| [RestoreCommand](xref:@ActiproUIRoot.Themes.WindowChrome.RestoreCommand) | `"Restore"` | `"Window.Restore"` | `CommandKeys.Window.Restore` |
| [SizeCommand](xref:@ActiproUIRoot.Themes.WindowChrome.SizeCommand) | `"Size"` | `"Window.Size"` | `CommandKeys.Window.Size` |

## Editors

### IPart Interface Changes

[IPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart) is an interface implemented by primitive types used by `EditBox` controls.  Anyone implementing this interface on their own types must update their implementation to meet the new interface requirements.  No changes are required for built-in types that implement the interface.

 To support new functionality which filters the text that can be typed into an [IPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart), a new [IsTextInputAllowed](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart.IsTextInputAllowed*) method has been added to the interface.  This method is passed a `string` of typed text, and the method should return `true` if the input is allowed or `false` if it is not.

### Intrinsic Numeric Types Refactored

While this should not be a breaking change, the class hierarchy of several `EditBox` and `Picker` controls (e.g., [DoubleEditBox](xref:@ActiproUIRoot.Controls.Editors.DoubleEditBox) and [DoublePicker](xref:@ActiproUIRoot.Controls.Editors.DoublePicker)) were refactored to include new abstract base classes that align with .NET Generic Math interfaces.  Property declarations, including `DependencyProperty` declarations) were moved from the individual classes to the base classes.

Additionally, the individual [IPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart) primitives that are used to define the editable parts of an `EditBox` were also refactored.  Previously, only [LiteralPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.LiteralPart), [DoublePart](xref:@ActiproUIRoot.Controls.Editors.Primitives.DoublePart), and [Int32Part](xref:@ActiproUIRoot.Controls.Editors.Primitives.Int32Part) were public.  With the following new classes, a public [IPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart) implementation is available for all numeric types:

The following new `EditBox`, `Picker`, and `IPart` classes were added:
- [NumberPartEditBoxBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.NumberPartEditBoxBase`1), [NumberPickerBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.NumberPickerBase`1), and [NumberPart\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.NumberPart`1)
  - Constrained to Generic Math interface `INumber<T>`.
  - Direct base class for `Byte`, `Int16`, `Int32`, and `Int64`.
- [FloatingPointNumberPartEditBoxBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.FloatingPointNumberPartEditBoxBase`1), [FloatingPointNumberPickerBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.FloatingPointNumberPickerBase`1), and [FloatingPointNumberPart\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.FloatingPointNumberPart`1)
  - Constrained to Generic Math interface `IFloatingPoint<T>`.
  - Direct base class for `Decimal`.
- [FloatingPointIeee754NumberPartEditBoxBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.FloatingPointIeee754NumberPartEditBoxBase`1), [FloatingPointIeee754NumberPickerBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.FloatingPointIeee754NumberPickerBase`1), and [FloatingPointIeee754NumberPart\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.FloatingPointIeee754NumberPart`1)
  - Constrained to Generic Math interface `IFloatingPointIeee754<T>`.
  - Direct base class for `Single` and `Double`.

### Edit Box ValueChanged Event Refactored

Each individual edit box previously defined its own `ValueChanged` event and the base class, which might need to raise the event, would call a protected abstract `RaiseValueChangedEvent` method.

The [ValueChanged](xref:@ActiproUIRoot.Controls.Editors.Primitives.PartEditBoxBase`1.ValueChanged) event is now defined on the base [PartEditBoxBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.PartEditBoxBase`1) class and the abstract `RaiseValueChangedEvent` method has been removed.  Any custom classes that derive from [PartEditBoxBase\<T\>](xref:@ActiproUIRoot.Controls.Editors.Primitives.PartEditBoxBase`1) should remove the `RaiseValueChangedEvent` method override.  If the logic of the removed method only raised the `ValueChanged` event then no further action is required as the base class can now raise the event directly.  If the logic performed additional processing in response to the value change, override the [OnValueChanged](xref:@ActiproUIRoot.Controls.Editors.Primitives.PartEditBoxBase`1.OnValueChanged*) method instead and place the logic there.  The `ValueChanged` event is raised immediately after the `OnValueChanged` method.

### Deprecated Types and Members

While still supported in v26.1, the following have been deprecated and will be removed in a future release:

- [DoublePart](xref:@ActiproUIRoot.Controls.Editors.Primitives.DoublePart) - Use generic class `FloatingPointIeee754NumberPart<Double>` instead.
- [Int32Part](xref:@ActiproUIRoot.Controls.Editors.Primitives.Int32Part) - Use generic class `NumberPart<Int32>` instead.
- [IPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart).[IsLiteral](xref:@ActiproUIRoot.Controls.Editors.Primitives.IPart.IsLiteral) - A new [ILiteralPart](xref:@ActiproUIRoot.Controls.Editors.Primitives.ILiteralPart) interface has been added. Instead of checking the `IsLiteral` property, check if a type implements the `ILiteralPart` interface.  For example, replace `if (part.IsLiteral) { ... }` with `if (part is ILiteralPart) { ... }`.

## Grids

The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) interface implementation was moved from [CachedPropertyModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CachedPropertyModelBase) to its parent class [PropertyModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBase).  This resulted in several new abstract declarations on `PropertyModelBase` to satisfy the interface.  Any classes that derive directly from `PropertyModelBase` (instead of `CachedPropertyModelBase`) will need to implement the new abstract members or change the class to inherit from `CachedPropertyModelBase` instead.  The following properties on `PropertyModelBase` and their equivalent implementations on `CachedPropertyModelBase` have been deprecated in favor of accessing the relevant property directly:

- `CanAddChildResolved`
- `CanRemoveResolved`
- `CanResetValueResolved`
- `ConverterResolved`
- `IsImmutableResolved`
- `IsValueReadOnlyResolved`
- `ShouldNotifyParentOnValueChangeResolved`
- `StandardValuesResolved`
- `TargetResolved`
- `ValueResolved`
- `ValueTypeResolved`

## Shell

The [ShellListView](xref:@ActiproUIRoot.Controls.Shell.ShellListView) and [ShellTreeListBox](xref:@ActiproUIRoot.Controls.Shell.ShellTreeListBox) controls now automatically detect changes in DPI.  The corresponding `NotifyDpiChanged` method on each control has been deprecated and will be removed in a future release.  Any calls to those methods should be removed.

## Removal of Legacy Editors and PropertyGrid Assemblies

The following assemblies were added in v17.1 to support compatibility with old APIs.  They have never been included in the NuGet packages.  In v26.1, they have been removed from the installer and are no longer available.

- `Editors.Interop.DataGrid.Legacy`
- `Editors.Interop.PropertyGrid.Legacy`
- `Editors.Interop.Ribbon.Legacy`
- `Editors.Legacy`
- `PropertyGrid.Interop.WinForms.Legacy`
- `PropertyGrid.Legacy`

If your application still requires them, you must use v25.1 for now, or upgrade to the current Editors and Grids products.

## Window Chrome

The internal logic of [Window Chrome](../themes/windowchrome.md) has been refactored to reduce complexity by removing logic necessary for legacy Windows systems.  Window chrome now will use native borders on Windows 11.  These updates make for a more stable experience and also fix an issue where resizing a window using the left or top borders would cause the window contents to appear jittery during resize.

Windows 10 (out of support by Microsoft already) and earlier systems will no longer have rounded corners for some themes.  Rounded corners are still fully supported in Windows 11+.

## Shared Library

The protected [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetScalePathPart](xref:@ActiproUIRoot.Media.ImageProvider.GetScalePathPart*) method previously had a typo in its name (`GetScalePathPath`) that has been fixed in this version.  Use the new method name instead.

## Assembly Image and Cursor Resources Moved to Root Path Level

All embedded image and cursor resources within product assemblies have been moved to a root path level.  The affected resources are typically only referenced by internal Actipro code and should not affect customer application logic.

## Product Metadata Namespace Changes

Metadata for each Actipro product was previously housed within an `ActiproSoftware.Products` namespace.  This has been renamed to the `ActiproSoftware.Properties` namespace instead.  Types in this namespace hierarchy are not typically used outside of Actipro-written code.

> [!TIP]
> Find `ActiproSoftware.Products` and replace with `ActiproSoftware.Properties` to convert any references to affected types.
