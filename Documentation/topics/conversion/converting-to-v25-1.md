---
title: "Converting to v25.1"
page-title: "Converting to v25.1 - Conversion Notes"
order: 84
---
# Converting to v25.1

The 25.1 version made a number of infrastructure updates and improvements.

## New Targets

All NuGet packages include a new target for .NET 8.

NuGet packages for the SyntaxEditor Text assemblies have been moved from .NET 6 to .NET 8.  Those who still need to support older versions of .NET can use the .NET Standard 2.0 targets.

NuGet packages no longer include the legacy .NET Core 3.1 target.  This target was previously used to support .NET Core 3.1 and .NET 5, neither of which are still supported by Microsoft. Projects that target .NET should be moved to .NET 6 or greater.

The .NET Framework configurations are unchanged.

## Visual Studio 2019 Support Removed

Visual Studio 2019 does not support .NET 6 or higher, so it can no longer be used to build .NET applications that use Actipro components.  Applications that target .NET Framework should continue to work, but compatibility is no longer verified with new releases.

## Bars RibbonBackstageTabViewModel Content Updates

The [RibbonBackstageTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel) class added new [Content](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel.Content), [ContentTemplate](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel.ContentTemplate), and [ContentTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel.ContentTemplateSelector) properties.

Previously, most backstage tab view models would typically use an implicit `DataTemplate` based on the derived view model type.  To continue using an implicit `DataTemplate` in this manner, most view models that derive from [RibbonBackstageTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel) will need to initialize the `Content` property to an instance of the view model since the `Type` of the `Content` will determine the implicit `DataTemplate`.

Starting with v25.1.2, each `RibbonBackstageTabViewModel` will initialize the `Content` property to an instance of itself to improve the out-of-the-box experience.  Those using *only* v25.1.0 or v25.1.1 will need to explicitly assign the `Content` property as demonstrated in the following sample:

```csharp
public class CustomRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

	public CustomRibbonBackstageTabViewModel()
		: base("BackstageTabCustom", "Custom") {

		// Use this tab viewmodel as content
		Content = this;
	}

}
```

Alternatively, the new properties allow for updated configurations where an explicit `DataTemplate` can be defined directly on the view model.

## Bars MVVM Tag Property

To simplify storing user-defined data with [Bars view models](../bars/mvvm-support.md) without the need to create custom derived classes, each view model has a new [Tag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag.Tag) property exposed using the [IHasTag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag) interface.  The existing [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel) interface was updated to also import the new [IHasTag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag) interface, so this is a breaking change for any user-defined types that implement [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel) directly without deriving from [BarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1).

The following updates should be made to any class that implements [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel):
- Add a declaration and implementation for the [IHasTag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag).[Tag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag.Tag) property.

## Docking SingleTabLayoutBehavior Updates

To support the new [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[SingleTabLayoutBehavior](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.SingleTabLayoutBehavior) property, the [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer).`SingleTabLayoutBehavior` property was moved to the base class as [DockingWindowContainerBase](xref:@ActiproUIRoot.Controls.Docking.Primitives.DockingWindowContainerBase).[SingleTabLayoutBehavior](xref:@ActiproUIRoot.Controls.Docking.Primitives.DockingWindowContainerBase.SingleTabLayoutBehavior).

## Docking AdvancedTabControl Key Access Gestures

The individual tabs of [AdvancedTabControl](../docking/advanced-tab-control.md) can be accessed using modifiers keys and the position of the tab.  For example, <kbd>Ctrl</kbd>+<kbd>2</kbd> will activate the second tab in the normal state.  Previously, keys <kbd>9</kbd> and <kbd>0</kbd> would access the ninth and tenth tabs, respectively.  The behavior of the <kbd>9</kbd> key has been changed to activate the last tab instead of the ninth tab, which is consistent with modern web browser applications.  The <kbd>0</kbd> key is no longer used.

In addition, tabs in the preview state, which were previously not accessible by a keyboard shortcut, can now be accessed using <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>1</kbd> through <kbd>9</kbd>.

No application logic updates should be required for this change, but user documentation may need to be updated.

## SyntaxEditor Intra-Text Adornment Placement Updates

This version enhances the [Intra-Text Adornments](../syntaxeditor/user-interface/adornment/intra-text-adornments.md) feature with the ability to position the intra-text adornment after the tagged text range instead of before.  This option is handy for scenarios such as when AI suggestions should be displayed at the end of a line, after the last character.

The [IsSpacerBefore](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag.IsSpacerBefore) property was added to the [IIntraTextSpacerTag](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag) interface to support this new option.  Any classes implementing this interface should return `true` for this property to retain prior version functionality where the adornment appears before the tagged range.

## Shared Library String Resource Updates

These existing string resources were updated to include underscore characters so that menu item accelerators are available:

- `SRName.UICommandCloseWindowText`
- `SRName.UICommandMaximizeWindowText`
- `SRName.UICommandMinimizeWindowText`
- `SRName.UICommandMoveWindowText`
- `SRName.UICommandRestoreWindowText`
- `SRName.UICommandSizeWindowText`

Since some controls like window title bar buttons previously used the above string resources, new resources have been added that do not have underscores.  Use these new string resources in non-menu item contexts:

- `SRName.UICloseButtonToolTip`
- `SRName.UIMaximizeButtonToolTip`
- `SRName.UIMinimizeButtonToolTip`
- `SRName.UIMoveButtonToolTip`
- `SRName.UIRestoreButtonToolTip`
- `SRName.UISizeButtonToolTip`

> [!TIP]
> If you have previously localized the original string resources, you should also localize the new set of string resources.