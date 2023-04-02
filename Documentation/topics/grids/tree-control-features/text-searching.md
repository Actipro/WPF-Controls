---
title: "Text Searching"
page-title: "Text Searching - Tree Control Features"
order: 12
---
# Text Searching

Text searching allows the end user to start typing text and for a visible (all ancestor items expanded, but not necessarily scrolled into view) item that starts with the typed text to be focused and scrolled into view.

## Runtime Functionality

When text searching is supported, any unhandled text input that bubbles up to the item's container will be used for text searching purposes.  As a character is typed, the character is added to a prefix string that is stored internally.  Then the active item, the one with focus, is examined and its search text is retrieved (see a section below on how to provide search text).  A case-insensitive check is made to see if the retrieved item search text starts with the currently typed prefix.  If the item search text does start with the prefix, the item is focused and searching quits.  Otherwise, the next visible item is examined with the same process.  Item examination continues and wraps to the top of the control as needed to find a match.

As more characters are typed, they are appended to the prefix string.  That is unless there was a pause in between typing.  In that case, the character starts a new prefix string.

A great feature built into text searching is that if you continue pressing the last character that was typed and no matches that were made using the old prefix with the new character appended, it will reuse the old prefix and look for another match without the newly-typed character.  Picture a scenario where there are three items (`"Foo 1"`, `"Foo 2"`, and `"Foo 3"`).  Typing the letter <kbd>F</kbd> focuses the first item.  Typing the letter <kbd>O</kbd> keeps focus on the first item.  Typing <kbd>O</kbd> again keeps focus on the first item since everything matches thus far.  Typing <kbd>O</kbd> another time doesn't match `"fooo"` (the text of all typed letters) with any items in the control, so a second search is made with the previous prefix (`"foo"`) and `"Foo 2"` is focused.  Similarly, typing <kbd>O</kbd> another time would focus `"Foo 3"`.

## Providing Search Text

Text search only works if the control knows what search text applies to each visible item.  Search text is provided to the control by calls to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetSearchText](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetSearchText*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)).

A `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[SearchTextBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SearchTextBinding) property to tell how an item should retrieve search text.  The [GetSearchText](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetSearchText*) method will use that binding if it is supplied, and will otherwise return null.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetSearchText](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetSearchText*) method instead with custom logic to retrieve the appropriate value.

This example code shows how to override the method for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override string GetSearchText(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.Name: null);
}
```
