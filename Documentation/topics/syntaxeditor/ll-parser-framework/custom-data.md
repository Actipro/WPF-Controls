---
title: "Custom Data"
page-title: "Custom Data - SyntaxEditor LL(*) Parser Framework"
order: 11
---
# Custom Data

The [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) object passed to each callback in the parser framework has a property on it where custom data can be tracked and updated throughout each parsing operation.

Anything can be placed in this custom data store.  It could be used to help perform semantic validation, construct name tables, and more.

## Usage Scenarios

Although there are many things that can be tracked in custom data, here are a couple examples.

### XML Element Hierarchy Validation

In the advanced XML syntax language in the [Web Languages Add-on](../web-languages-addon/index.md) we maintain a stack of elements in this custom data.  Any time we enter a new element, we push it on the stack.  When we exit an element, we pop it off.  If the end tag name doesn't match the start tag name, we can intelligently report errors.

### Name Tables

Many parser implementations may wish to construct a name table.  The table could be stored in the custom data and then updated as a new type or variable name is encountered.

## Bad Ways to Track Custom Data

One way you may be tempted to track custom data during a parse is to put a member variable on your grammar class.  You would initialize it at some point and update it in your callbacks.

The problem is that if more than one document is using the language, the grammar is potentially operating on multiple documents at the same time.  This of course means that each parser working on the documents could be modifying the member variable while the other is working with the variable, thus leading to invalid data and possible exceptions.

The use of member variables is not recommended and for this reason, we recommend the use of techniques described below to store and update custom data instead.

## Initializing Custom Data

The [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState).[CustomData](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState.CustomData) property is the right place to store custom data for a parsing operation since it and its containing [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) are specific to each parse request.  The [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) object for a parsing operation is created internally by the LL(*) Parser Framework and then is passed to each callback in your grammar.  This begs the question; how can the custom data be initialized at the parsing operation start?

We recommend initializing the custom data in an initialize callback of the root production since that is the very first part of your grammar to be entered.

This example shows how to add initialize and complete callbacks to the root production.  Note that ellipses (`...`) indicate where the normal production terms and tree constructors would go.

```csharp
this.Root.Production = (...).OnInitialize(CompilationUnitInitialize).OnComplete(CompilationUnitComplete);
```

A `CompilationUnitInitialize` implementation could look like this, where `MyParserContext` is a class with various properties to help track custom data for this particular parsing operation:

```csharp
private void CompilationUnitInitialize(IParserState state) {
	// Initialize the state's custom data with a parser context
	state.CustomData = new MyParserContext();
}
```

## Updating the Custom Data While Parsing

Any callback has access to the [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState).[CustomData](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState.CustomData) property, and therefore can access the object created during initialization.

Properties on the object can be updated as needed.  If the custom data is tracking a name table, any variable declarations should add an entry into the name table.

A stack property can be added to the custom data object so that you can track scopes entered/exited as code is parsed.  That way you can know what sort of scope you are in when each callback is called.  A stack item would be pushed onto the stack by you from a callback whenever a scope is entered, and then popped when the scope is existed.

These are just a couple of ideas.  Really the custom data can be used to track anything while the parsing operation is active.

## Performing Semantic Validation with Custom Data

We've seen how to initialize the custom data for a parsing operation.  The corresponding complete callback for the root production is an ideal place to examine the custom data to see if any further syntax or semantic errors should be reported.

For instance, in XML you may wish to report if not all tags were closed properly, assuming the custom data had data in it to indicate this scenario.

A `CompilationUnitComplete` implementation could look like this:

```csharp
private void CompilationUnitComplete(IParserState state) {
	MyParserContext context = state.CustomData as MyParserContext;
	if (context != null) {
		// Report any semantic errors here with state.ReportError() based on data in the parser context
	}
}
```

## Returning and Consuming the Custom Data

Since the LL(*) Parser Framework is being used, the [IParser](xref:ActiproSoftware.Text.Parsing.IParser) for the language should inherit [LLParserBase](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase).  This base class has a virtual [CreateParseData](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase.CreateParseData*) method that creates the [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData) returned by the parser.

By creating a language-specific [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData) object, you can get data from your [CustomData](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState.CustomData) object, store it in the [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData), and it will be returned to the document.  Thus, it is available to consume by other language services such as IntelliPrompt since it is part of the parse data.

This code sample shows a possible implementation of a `CreateParseData` override that moves data from the `MyParserContext` object to the `MyParseData` object that is returned to the document:

```csharp
protected override IParseData CreateParseData(IParseRequest request, IParserState state) {
	if (request == null)
		throw new ArgumentNullException("request");
	if (state == null)
		throw new ArgumentNullException("state");

	MyParseData parseData = new MyParseData();

	// Initialize
	this.InitializeParseData(parseData, state);

	// Pass on parser context data
	MyParserContext context = state.CustomData as MyParserContext;
	if (context != null)
		parseData.SomeData = context.SomeData;

	return parseData;
}
```
