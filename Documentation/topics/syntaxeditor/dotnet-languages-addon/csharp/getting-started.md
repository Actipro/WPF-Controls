---
title: "Getting Started"
page-title: "Getting Started - C# Language - SyntaxEditor .NET Languages Add-on"
order: 2
---
# Getting Started

This topic covers how to get started using the C# language from the .NET Languages Add-on, implemented by the [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage) class, and lists its requirements for supporting advanced features like parsing and automated IntelliPrompt.

It is very important to follow the steps in this topic to configure the language correctly so that its advanced features operate as expected.

## Configure the Ambient Parse Request Dispatcher

The language's parser does a lot of processing when text changes occur.  To ensure that these parsing operations are offloaded into a worker thread that won't affect the UI performance, you must set up a parse request dispatcher for your application.

The ambient parse request dispatcher should be set up in your application startup code as described in the [Parse Requests and Dispatchers](../../text-parsing/parsing/parse-requests-and-dispatchers.md) topic.

@if (winrt) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	AmbientParseRequestDispatcherProvider.Dispatcher = new TaskBasedParseRequestDispatcher();
	...
}
```

}

@if (wpf winforms) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	AmbientParseRequestDispatcherProvider.Dispatcher = new ThreadedParseRequestDispatcher();
	...
}
```

}

Likewise it should be shut down on application exit, also as described in the [Parse Requests and Dispatchers](../../text-parsing/parsing/parse-requests-and-dispatchers.md) topic.

```csharp
protected override void OnExit(ExitEventArgs e) {
	...
	var dispatcher = AmbientParseRequestDispatcherProvider.Dispatcher;
	if (dispatcher != null) {
		AmbientParseRequestDispatcherProvider.Dispatcher = null;
		dispatcher.Dispose();
	}
	...
}
```

> [!IMPORTANT]
> Failure to set up an ambient parse request dispatcher when using the language will result in unnecessary UI slowdown since parse operations will be performed in the UI thread instead of in a worker thread.

## Configure the Ambient Assembly Repository

An ambient assembly repository should always be set up to ensure that the application reuses binary assembly reflection data whenever appropriate and that binary assembly references added to a project assembly are cached for future loading performance gains.

The ambient assembly repository should be set up in your application startup code as described in the [Assemblies](../assemblies.md) topic.  The [FileBasedAssemblyRepository](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.Implementation.FileBasedAssemblyRepository) class is the default implementation of an assembly repository, which supports the writing of binary assembly data to a cache folder specified in its constructor, as long as the application has read/write permissions to that folder@if (winrt) { (the app's data folder is advised). }@if (wpf winforms) {. }

@if (winrt) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	string appDataPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Assembly Repository");
	AmbientAssemblyRepositoryProvider.Repository = new FileBasedAssemblyRepository(appDataPath);
	...
}
```

}

@if (wpf winforms) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	// Use a path like this if in full trust; otherwise, pass null
	string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
		@"YourCompanyName\YourApplicationName\Assembly Repository");
	AmbientAssemblyRepositoryProvider.Repository = new FileBasedAssemblyRepository(appDataPath);
	...
}
```

}

Likewise on application exit, the assembly repository's cache should be pruned, also as described in the [Assemblies](../assemblies.md) topic.

```csharp
protected override void OnExit(ExitEventArgs e) {
	...
	var repository = AmbientAssemblyRepositoryProvider.Repository;
	if (repository != null)
		repository.PruneCache();
	...
}
```

> [!IMPORTANT]
> Failure to set up an ambient assembly repository provider may result in unnecessary increased memory usage and slowdown when loading assembly references.

## Configure the CSharpSyntaxLanguage and CSharpProjectAssembly

The next step is to create an instance of the [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage) class and configure its project assembly.

This code creates a [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage), gets its default project assembly, and uses a background worker (for asynchronous execution) to add some assembly references, and queue up a source file to be included:

```csharp
var language = new CSharpSyntaxLanguage();
var project = language.GetService<IProjectAssembly>();
var assemblyLoader = new BackgroundWorker();
assemblyLoader.DoWork += (sender, e) => {
	project.AssemblyReferences.AddMsCorLib();  // MsCorLib should always be added at a minimum
	project.AssemblyReferences.Add("System");
	project.AssemblyReferences.AddFrom(@"C:\MyAssembly.dll");
	project.SourceFiles.QueueFile(language, @"C:\MyFile.cs");
};
assemblyLoader.RunWorkerAsync();
```

> [!NOTE]
> The [Assemblies](../assemblies.md) topic contains detailed information on the multiple ways (some not shown above) to add assembly references and source files to a project assembly.

Note that the [IProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly).[AssemblyReferences](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly.AssemblyReferences) collection should be cleared when the project assembly will no longer be used.  This cleans up any memory or resources in use by the assembly repository for loaded assemblies.

```csharp
project.AssemblyReferences.Clear();
```

## Use the CSharpSyntaxLanguage

Next, use the language on the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) instances that will be editing C# code.

This code applies the language to a document in a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor), whose instance is in the `editor` variable:

```csharp
editor.Document.Language = language;
```

> [!TIP]
> We recommend reusing your [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage) instance among all the documents in your application that are editing C# code.  This saves on overall memory usage and reduces load times.

## Documents Should Have an Appropriate FileName

It is a good practice to set the [FileName](xref:ActiproSoftware.Text.ITextDocument.FileName) on any document using the language to an appropriate value that uniquely identifies the document's contents.  The filename is used as a key to register the types defined in the document with the language's [IProjectAssembly](xref:ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly).

If the filename is left empty, the add-on will fall back to using the document's unique ID (a GUID value) to identify its contents in the project assembly.

This code sets the filename for a document in a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor), whose instance is in the `editor` variable:

```csharp
editor.Document.FileName = @"C:\Code.cs";
```

## Assembly Requirements

The following list indicates the assemblies that are used with the C# syntax language implementation in this add-on.

| Assembly | Required | Author | Licensed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Text.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core text/parsing framework for SyntaxEditor |
| ActiproSoftware.Text.LLParser.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | LL parser framework implementation |
| ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core framework for all Actipro @@PlatformName controls |
| ActiproSoftware.SyntaxEditor.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | SyntaxEditor for @@PlatformName control |
| ActiproSoftware.Text.Addons.DotNet.@@PlatformAssemblySuffix.dll | Yes | Actipro | .NET Languages Add-on | Core text/parsing for the C# language |
| ActiproSoftware.SyntaxEditor.Addons.DotNet.@@PlatformAssemblySuffix.dll | Yes | Actipro | .NET Languages Add-on | SyntaxEditor for @@PlatformName advanced C# syntax language implementation |
