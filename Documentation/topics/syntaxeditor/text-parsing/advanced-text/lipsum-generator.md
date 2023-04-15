---
title: "Lipsum Generator"
page-title: "Lipsum Generator - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 9
---
# Lipsum Generator

A special utility class is included that supports the generation of "lorem ipsum" placeholder text.  Placeholder text is useful for developers and designers when prototyping out web pages and screens.  It is designed to contain unreadable gibberish sentences, which are ideal for layout.

## Generating Placeholder Text

A toolbar button or menu item (i.e., an **Insert Lorem Ipsum** item) clicked by the user in your app's UI might trigger the generation of placeholder text.

This code snippet shows how to use the [LipsumGenerator](xref:ActiproSoftware.Text.Utility.LipsumGenerator) class to generate the placeholder text:

```csharp
var text = new LipsumGenerator().GenerateParagraph(true, 30);
```

The result of that call might yield something like the following:

> Lorem ipsum dolor sit amet, consectetur adipisicing elit. Necessitatibus distinctio voluptates facilis nesciunt magnam, tempore similique voluptate cupiditate. Et fugit assumenda necessitatibus eum suscipit maiores ullam. Cum enim consequuntur adipisci.

The [GenerateParagraph](xref:ActiproSoftware.Text.Utility.LipsumGenerator.GenerateParagraph*) method generates a single paragraph of placeholder text.  It has two parameters.
- The first is a `boolean` indicating whether the generated text should begin with the standard starting words `"Lorem ipsum dolor sit amet, consectetur adipisicing elit."`
- The second parameter is an `integer` indicating the total number of words that should be generated in the paragraph.

The generated sentences in the paragraph will be of random length and will have commas sporatically inserted.
