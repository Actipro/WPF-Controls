---
title: "IntelliPrompt Navigable Symbol Provider"
page-title: "IntelliPrompt Navigable Symbol Provider - Feature Services - SyntaxEditor Language Creation Guide"
order: 14
---
# IntelliPrompt Navigable Symbol Provider

Languages can choose to support an IntelliPrompt navigable symbol provider that lists the accessible symbols within an editor's document.  Once a language implements this provider service, the [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) control can be paired with a SyntaxEditor to provide VS type/member dropdown-like functionality.

## Basic Concepts

### Navigable Symbols

Navigable symbols are any object that implements the [INavigableSymbol](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbol) interface.  The [NavigableSymbol](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.NavigableSymbol) class is a default implementation.  Symbols represents a type or member within a document's text.  They each provide these bits of information:

| Member | Description |
|-----|-----|
| [ContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbol.ContentProvider) Property | Gets a [Content Provider](../../user-interface/intelliprompt/popup-content-providers.md) that creates the content to render for the symbol within UI. |
| [NavigationSnapshotOffset](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbol.NavigationSnapshotOffset) Property | Gets the [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) within the symbol's source file to navigate when the symbol is selected. |
| [SnapshotRange](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbol.SnapshotRange) Property | Gets a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) indicating the offset range of the symbol within its source file. |

So for instance in a C# language, a symbol that represented a class declaration would have a content provider that listed the name of the class, a navigation snapshot offset that was the offset of the class name within the document, and a snapshot range that covered everything from the type's XML comments through to its closing curly brace.

### Navigable Request Contexts

When an [INavigableSymbolProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider) is called to obtain accessible symbols, it is passed an [INavigableRequestContext](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableRequestContext) object.  This object provides additional information about what is requesting the symbols.

The [NavigableRequestContexts](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.NavigableRequestContexts).[NavigableSymbolSelector](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.NavigableRequestContexts.NavigableSymbolSelector) is currently the only pre-defined request context, and indicates that a [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) control is making the request.

### Navigable Symbol Provider

The [INavigableSymbolProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider) interface is the language service used to return accessible symbols to callers.  It has a single [GetSymbols](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider.GetSymbols*) method that takes in:

- An [INavigableRequestContext](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableRequestContext) indicating the source of the request.
- An [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) that is the snapshot to examine.
- An optional parent [INavigableSymbol](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbol) to examine.

If the parent symbol is a null value, it means the request is for all root symbols within the document.  If a parent symbol is specified, then the request is for the members of that root symbol.

The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) provides access to its container [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument), and via that, the document's parse data, which usually contains information such as an AST.  That is assuming that the language has a parser that constructs an AST.

The [GetSymbols](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider.GetSymbols*) method implementation should use information such as the AST to create [INavigableSymbol](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbol) objects based on the parameters passed in, and then return those symbols.  By doing so, controls such as [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) can consume this information and provide an advanced editing experience for end users.

This code shows a sample [GetSymbols](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider.GetSymbols*) implementation.  It assumes that whatever type you use for parse data (in this case our `ILLParseData` type) has an AST available.  Then it calls a `AddTypeSymbolsFromAst` or `AddMemberSymbolsFromAst` method (both would need implementation based on your language) and passes along the relevant information needed to build the symbol list.

```csharp
public IEnumerable<INavigableSymbol> GetSymbols(INavigableRequestContext context, ITextSnapshot snapshot, INavigableSymbol parentSymbol) {
	if (context == NavigableRequestContexts.NavigableSymbolSelector) {
		var document = snapshot.Document as ICodeDocument;
		if (document != null) {
			// If there is AST data...
			var parseData = document.ParseData as ILLParseData;
			if ((parseData != null) && (parseData.Ast != null)) {
				// Recurse into the AST
				var symbols = new List<INavigableSymbol>();
				if (parentSymbol != null)
					this.AddMemberSymbolsFromAst(symbols, parseData.Snapshot ?? snapshot, parseData.Ast, parentSymbol);
				else
					this.AddTypeSymbolsFromAst(symbols, parseData.Snapshot ?? snapshot, parseData.Ast);
				
				// Sort (navigationSymbolComparer is a cached NavigableSymbolContentProviderComparer instance)
				symbols.Sort(navigationSymbolComparer);

				return new NavigableSymbolSet(symbols);
			}
		}
	}

	// No results
	return new NavigableSymbolSet(null);
}
```

## Registering with a Language

Any object that implements [INavigableSymbolProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider) can be associated with a syntax language by registering it as an [INavigableSymbolProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.INavigableSymbolProvider) service on the language.

This code creates a custom navigable symbol provider and registers it with the syntax language that is already declared in the `language` variable:

```csharp
INavigableSymbolProvider provider = new CustomNavigableSymbolProvider();
language.RegisterNavigableSymbolProvider(provider);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterNavigableSymbolProvider](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterNavigableSymbolProvider*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
