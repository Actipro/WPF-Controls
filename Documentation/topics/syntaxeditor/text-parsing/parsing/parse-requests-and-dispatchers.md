---
title: "Parse Requests and Dispatchers"
page-title: "Parse Requests and Dispatchers - Parsing - SyntaxEditor Text/Parsing Framework"
order: 2
---
# Parse Requests and Dispatchers

Parse requests are created to request that syntax/semantic analysis be performed on some text. [CodeDocument](xref:ActiproSoftware.Text.Implementation.CodeDocument) is capable of automatically making parse requests when its text changes, so long as its [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) has an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) service registered.

A parse request dispatcher is notified of incoming parse requests and queues them.  It uses one or more worker threads to execute the requested parsing operations in prioritized order, thereby preventing any blocking of the main UI thread.

## Initializing an Ambient Parse Request Dispatcher for an Application

An [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) is an object that is capable of receiving parse requests ([IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest) instances).  Upon receipt of a request, it will generally queue the request, possibly disposing of similar previous requests that are still on the queue.

One or more worker threads are then used to perform parsing operations, one by one, based on the data in the requests.  More than one worker thread is advantageous when there are potentially many parsing requests queued, while editing of a code document (via a SyntaxEditor control for instance) is taking place.  This allows the requests made by the code document to continue to get handled fast, intermixed with the other queued requests.

> [!IMPORTANT]
> By default, no ambient [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) is installed meaning parsing from code documents (explained in next section) will be done in the main UI thread.  Configure an ambient parse request dispatcher (see below) to ensure parsing operations are offloaded into worker threads.

@if (winrt) {

### TaskBasedParseRequestDispatcher

[TaskBasedParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.Implementation.TaskBasedParseRequestDispatcher) is the default implementation of an [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) that ships with the parsing framework.  It is capable of handling parse requests on one or more worker threads.

}

@if (wpf winforms) {

### ThreadedParseRequestDispatcher

[ThreadedParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.Implementation.ThreadedParseRequestDispatcher) is the default implementation of an [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) that ships with the parsing framework.  It is capable of handling parse requests on one or more worker threads.

By default it creates a maximum number of worker threads that is equal to your machine's processor core count minus one.  Thus on a quad-core machine, up to three worker threads may be created.  If a worker thread is not used for some time, it will be disposed of.

}

### Configuring the Ambient Parse Request Dispatcher

As described below, code documents are capable of making automatic parse requests.  They need to know which [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) to use though.  If they can't find one, then they execute the parsing operation in the main UI thread, which can block UI if the parsing operation takes time.

The [AmbientParseRequestDispatcherProvider](xref:ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider) class is used to indicate which [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) should be used by default.  Code documents look at the [AmbientParseRequestDispatcherProvider](xref:ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider), and any manual parse requests should too.

@if (winrt) {

This code is executed at application startup and creates a [TaskBasedParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.Implementation.TaskBasedParseRequestDispatcher) for use as the ambient parse request dispatcher.

```csharp
AmbientParseRequestDispatcherProvider.Dispatcher = new TaskBasedParseRequestDispatcher();
```

}

@if (wpf winforms) {

This code is executed at application startup and creates a [ThreadedParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.Implementation.ThreadedParseRequestDispatcher) for use as the ambient parse request dispatcher.

```csharp
AmbientParseRequestDispatcherProvider.Dispatcher = new ThreadedParseRequestDispatcher();
```

}

Once the code above is executed, worker threads will now be used to execute parsing operations.

At application shutdown, this code can be used to remove the ambient parse request dispatcher, kill its worker threads, and empty its parse queue.

```csharp
IParseRequestDispatcher dispatcher = AmbientParseRequestDispatcherProvider.Dispatcher;
AmbientParseRequestDispatcherProvider.Dispatcher = null;
dispatcher.Dispose();
```

## CodeDocument Makes Automatic Parse Requests

Code documents ([CodeDocument](xref:ActiproSoftware.Text.Implementation.CodeDocument) instances) have special features built into them such that when a text change occurs, they look at the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) that is attached to the document, if any.  If the language has an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) registered as a service, an [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest) is automatically generated and sent to the [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) specified by the [AmbientParseRequestDispatcherProvider](xref:ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider) to be queued for processing.

> [!NOTE]
> As indicated above, if no ambient parse request dispatcher is configured, parsing operations will be performed in the main UI thread, possibly blocking it if the parsing operations take time to complete.

All of the parse request generation occurs automatically, as long as a parser is found on the language.  The results of the parse operation are returned asynchronously to the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property, at which time the document's [ParseDataChanged](xref:ActiproSoftware.Text.ICodeDocument.ParseDataChanged) event fires.

Note that [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) implements [IParseTarget](xref:ActiproSoftware.Text.Parsing.IParseTarget), so any code document is capable of receiving parsing operation results.  That is how [CodeDocument](xref:ActiproSoftware.Text.Implementation.CodeDocument) knows when to update its [ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property.

## Manually Triggering CodeDocument Parse Requests

Sometimes there are situations where a configuration option that the parser uses has changed but the text of an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) has not been modified.  Thus the parse data for the document may no longer be valid.

The [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[QueueParseRequest](xref:ActiproSoftware.Text.ICodeDocument.QueueParseRequest*) method can be called in this scenario to manually queue up a parse request for the document.  Once the parse request is processed, the document's [ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property will be updated.  As discussed above, like all parsing operations, this process may be asynchronous.

## Creating a Parse Request Manually

Parse requests can be created manually for any bit of text.  No document is necessary.  Say that you are loading a C# project and would like to load compilation unit data for each one of the files in the project.  In that case you could queue up parsing requests for each of the files.  It is best to use a low priority so that the queued requests don't interfere with normal editor/code document parsing.

The [ParseRequest](xref:ActiproSoftware.Text.Parsing.Implementation.ParseRequest) class implements [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest).  Its constructor takes several parameters:

- A string source key that identifies the source
- An [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) that is used by the parser to scan text
- The [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) (preferred) or [IParser](xref:ActiproSoftware.Text.Parsing.IParser) that should be used to perform the parsing operation
- The [IParseTarget](xref:ActiproSoftware.Text.Parsing.IParseTarget) that is notified once the parsing is completed

The source key is generally a filename but really can be anything you like that uniquely identifies the text content being parsed.

The [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) can be any object that supports text reading per the interface members.  The interface is described in the [Scanning Text Using a Reader](../core-text/scanning-text.md) topic.

The [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) is a language that has an [IParser](xref:ActiproSoftware.Text.Parsing.IParser).  If no [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) is available, then an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) instance can be passed in its place, but it is better to pass the language instance.  The [IParser](xref:ActiproSoftware.Text.Parsing.IParser) is any object that has a [Parse](xref:ActiproSoftware.Text.Parsing.IParser.Parse*) that accepts an [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest) and returns an [IParseData](xref:ActiproSoftware.Text.Parsing.IParseData) result.

The [IParseTarget](xref:ActiproSoftware.Text.Parsing.IParseTarget) is an object that has a unique ID identifying it, along with a method that is called after a request has been processed and completed.

This code shows how to create a manual request without a language available and queue it:

```csharp
// Create the request
string sourceKey = @"c:\myfilename.txt";
ITextBufferReader reader = new StringTextBufferReader("Text to parse");
IParser parser = new MyTextParser();  // Some class implementing IParser
IParseTarget parseTarget = this;  // Assuming class that executes this code implements IParseTarget
IParseRequest request = new ParseRequest(sourceKey, reader, parser, parseTarget);
request.Priority = ParseRequest.LowPriority;

// Queue the request
if (AmbientParseRequestDispatcherProvider.Dispatcher != null)
	AmbientParseRequestDispatcherProvider.Dispatcher.QueueRequest(request);
```

## Other Parse Request Features

The [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest) has some additional useful features.

### Creation and Identification

The [CreatedDateTime](xref:ActiproSoftware.Text.Parsing.IParseRequest.CreatedDateTime) property indicates when the request was created.

The [ParseHashKey](xref:ActiproSoftware.Text.Parsing.IParseRequest.ParseHashKey) property returns a hash key that can be used to identify the request, and is used in many [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) members.

### Queueing - Priority and Optimization

The [Priority](xref:ActiproSoftware.Text.Parsing.IParseRequest.Priority) property allows the priority of the request to be set.  Manual parse requests should generally use the priority specified by [ParseRequest](xref:ActiproSoftware.Text.Parsing.Implementation.ParseRequest).[LowPriority](xref:ActiproSoftware.Text.Parsing.Implementation.ParseRequest.LowPriority).  Code document requests use [MediumPriority](xref:ActiproSoftware.Text.Parsing.Implementation.ParseRequest.MediumPriority). [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) implementations generally use the priority setting to determine how to queue requests.

The [RepeatedRequestPause](xref:ActiproSoftware.Text.Parsing.IParseRequest.RepeatedRequestPause) property lets you indicate the number of milliseconds to pause if there are repeated attempts for the same request.  This scenario can happen if an editor is working on a code document and a lot of typing is occurring.  By introducing a small pause, performance is optimized.  The default value is `250`.

### Originating Text Snapshot

The [Snapshot](xref:ActiproSoftware.Text.Parsing.IParseRequest.Snapshot) property returns a [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) that indicates the snapshot from which the request was made, if available.  Note that while this property is automatically populated when [CodeDocument](xref:ActiproSoftware.Text.Implementation.CodeDocument) creates the request, manually-created parse requests may choose not to specify a snapshot.

### State

The [State](xref:ActiproSoftware.Text.Parsing.IParseRequest.State) property returns a [ParseRequestState](xref:ActiproSoftware.Text.Parsing.ParseRequestState) value that indicates the current state of the request, such as whether it is queued, or being parsed, etc.

### Custom Data

The [Tag](xref:ActiproSoftware.Text.Parsing.IParseRequest.Tag) property allows for custom data to be passed along with the request, all the way through to the [IParseTarget](xref:ActiproSoftware.Text.Parsing.IParseTarget).

## Other Parse Request Dispatcher Features

Generally it is not necessary to call any additional methods on [IParseRequestDispatcher](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher) since the entire queue and dispatch processing is mostly automated for your convenience.  However there are a number of useful members on the dispatcher that can tell you what it is currently doing, etc.

Many of the methods on the dispatcher deal with parse hash keys, which can be retrieved from the [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest).[ParseHashKey](xref:ActiproSoftware.Text.Parsing.IParseRequest.ParseHashKey) property.

### Queue Updating

The [QueueRequest](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.QueueRequest*) method is used to tell the dispatcher that a new [IParseRequest](xref:ActiproSoftware.Text.Parsing.IParseRequest) needs to be queued for processing.  See the manual parse request example above for a sample of code calling this method.

The [RemovePendingRequests](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.RemovePendingRequests*) method accepts a parse hash key and removes any queued instances of requests with that hash key, if there are any.

### Dispatcher Examination

The [IsBusy](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.IsBusy) property indicates if there are any requests that need to be completed. [PendingRequestCount](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.PendingRequestCount) returns the number of pending requests remaining.

[GetPendingRequests](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.GetPendingRequests*) returns an array of the pending requests remaining. [HasPendingRequest](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.HasPendingRequest*) returns whether there are any pending requests with a specific parse hash key.

### Blocking Until a Request Has Been Processed

The [WaitForParse](xref:ActiproSoftware.Text.Parsing.IParseRequestDispatcher.WaitForParse*) method can be used to block the calling thread until a pending request with the specified hash key is completed, or a certain timespan is elapsed.  The standard timespan used is `250ms`.

This method is generally used before building contextual data for IntelliPrompt display since ideally you want the IntelliPrompt UI to have the most up-to-date data in it.
