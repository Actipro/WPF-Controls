---
title: "Navigable Symbol Selector"
page-title: "Navigable Symbol Selector - SyntaxEditor IntelliPrompt Features"
order: 9
---
# Navigable Symbol Selector

The [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector) control displays two side-by-side drop-downs similar to the type/member drop-downs above the code editor in Visual Studio.  One drop-down shows all available root symbols (generally types), and the other shows all available member symbols within the currently selected root symbol.

As the caret in a bound [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) instance is moved, the selections in the [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector) update to indicate the enclosing symbols (types/members).  The end user can also select a different symbol from the drop-downs to navigate directly to the related symbol declaration.

## Attaching to a SyntaxEditor Instance

[NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector) controls must be bound to a single [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control instance.  The selector will watch for various events on the bound editor and will respond accordingly.

For instance, when the caret moves, the selector will find the appropriate symbols to select in its drop-downs based on which ones most closely enclose or are near the caret.  Likewise, when the end user picks another symbol from one of the drop-downs, the bound editor's caret will move to its declaration.

[NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector) controls generally appear directly above the bound [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control like in this example within a `Grid`:

```xaml
<Grid>
	<Grid.RowDefinitions>
		<RowDefinition Height="Auto" />
		<RowDefinition Height="*" />
	</Grid.RowDefinitions>
	<editor:NavigableSymbolSelector SyntaxEditor="{Binding ElementName=editor}" />
	<editor:SyntaxEditor x:Name="editor" Grid.Row="1" />
</Grid>
```

## Populating the NavigableSymbolSelector

While it's extremely simple to get a [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector) working in an app per binding it to an editor (as above), the current syntax language needs to support telling the selector what symbols are available in the document.

The [Navigable Symbol Provider](../../language-creation/feature-services/navigable-symbol-provider.md) topic describes the special language service that must be implemented so that a selector knows how to populate its drop-downs with symbols.

## Symbol Selection Logic

As mentioned above, the selector watches editor events and updates the symbols that are selected in the drop-downs according to the current editor caret location.  If the caret is within a symbol, the closest enclosing symbol is selected in the drop-down.  If the caret is not within a symbol, but there is one after the caret, it is selected in the drop-down.  Or if there are no symbols after the caret, the previous symbol is selected.

The selector has an additional feature where the selected symbol grays out when the symbol doesn't enclose the caret.  This provides a nice visual indicator for when the caret is actually within or just near the selected symbol.

## Rool Symbol-Only Display

Some languages may not support members and may only need to have a single drop-down to display root symbols, instead of an additional drop-down to display member symbols.

For these scenarios, set the [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector).[AreMemberSymbolsSupported](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector.AreMemberSymbolsSupported) property to `false`.

## Member Symbol-Only Display

Some languages may wish to only show members and have a single drop-down to display member symbols, instead of an additional drop-down to display root symbols.

For these scenarios, set the [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector).[AreRootSymbolsSupported](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector.AreRootSymbolsSupported) property to `false`.
