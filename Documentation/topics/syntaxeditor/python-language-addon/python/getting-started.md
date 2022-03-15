---
title: "Getting Started"
page-title: "Getting Started - SyntaxEditor Python Language Add-on"
order: 2
---
# Getting Started

This topic covers how to get started using the Python language from the Python Language Add-on, implemented by the [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) class, and lists its requirements for supporting advanced features like parsing and automated IntelliPrompt.

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

> [!NOTE]
> Failure to set up an ambient parse request dispatcher when using the language will result in unnecessary UI slowdown since parse operations will be performed in the UI thread instead of in a worker thread.

## Configure the Ambient Package Repository

An ambient package repository should always be set up to ensure that the application is able to use and cache package data from all packages within the Python project's search paths.  A Python project (see below) normally adds search paths for the root of your project's `.py` files, along with external libraries such as the Python standard library's `Lib` folder.  When a search path is added to a project, it will search the cache path to see if the cached data there is up-to-date with the related source package.  If not, cache data is added to the package repository via a worker thread.

The ambient package repository should be set up in your application startup code.  The [FileBasedPackageRepository](xref:ActiproSoftware.Text.Languages.Python.Reflection.Implementation.FileBasedPackageRepository) class is the default implementation of a package repository, which supports the writing of binary package data to a cache folder specified in its constructor, as long as the application has read/write permissions to that folder @if (winrt) {(the app's data folder is advised). }@if (wpf winforms) {(usually requires full trust). }

@if (winrt) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	string appDataPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Package Repository");
	AmbientPackageRepositoryProvider.Repository = new FileBasedPackageRepository(appDataPath);
	...
}
```

}

@if (wpf winforms) {

```csharp
protected override void OnStartup(StartupEventArgs e) {
	...
	// Use a path like this if in full trust
	string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
		@"YourCompanyName\YourApplicationName\Package Repository");
	AmbientPackageRepositoryProvider.Repository = new FileBasedPackageRepository(appDataPath);
	...
}
```

}

> [!NOTE]
> Failure to set up an ambient package repository provider will result in IntelliPrompt features not working for module internals.

## Create the PythonSyntaxLanguage

The next step is to create an instance of the [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) class and configure its project.

This code creates a [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage):

```csharp
var language = new PythonSyntaxLanguage();
```

## Configure the Python Project

Each language has a [IProject](xref:ActiproSoftware.Text.Languages.Python.Reflection.IProject) registered as a service on it that provides information such as which modules are actively being edited in a SyntaxEditor and the collection of paths to search for external packages.

If you wish to support automated IntelliPrompt for external modules, it is important to add search paths to the project.  This tells the resolver where to look for `.py` files.  Note that the resolver will recursively search through child folders of specified paths as long as the folder names are valid Python identifiers.

It is common practice to specify these paths:

- The root path of your Python project code
- The path to the Python standard library `Lib` folder
- The path to the Python standard library's `Lib\site-packages` folder
- The path to any other folders that contain external libraries that are used by code in your project

This code gets the language's default project, and adds several search paths to where external packages can be located and used for IntelliPrompt:

```csharp
var project = language.GetService<IProject>();
project.SearchPaths.Add(@"C:\Python\Projects\MyProject");
project.SearchPaths.Add(@"C:\Python\Platform34\Lib");
project.SearchPaths.Add(@"C:\Python\Platform34\Lib\site-packages");
```

@if (winrt) {

> [!NOTE]
> Due to the security sandbox restrictions of the file system in Windows apps, take care to ensure that the search paths you add are fully accessible to the app.  The app's data folder is advised.

}

## Use the PythonSyntaxLanguage

Next, use the language on the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) instances that will be editing Python code.

This code applies the language to a document in a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor), whose instance is in the `editor` variable:

```csharp
editor.Document.Language = language;
```

> [!NOTE]
> We recommend reusing your [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) instance among all the documents in your application that are editing Python code.  This saves on overall memory usage and reduces load times.

## Documents Should Have an Appropriate FileName and Tab Settings

It is a good practice to set the [FileName](xref:ActiproSoftware.Text.ITextDocument.FileName) on any document using the language to an appropriate value that uniquely identifies the document's contents.  The filename is used as a key to register the module defined in the document with the language's [IProject](xref:ActiproSoftware.Text.Languages.Python.Reflection.IProject).

> [!NOTE]
> If the filename is left empty, relative paths can't be used in `from...import` statements and some other IntelliPrompt feature might not work properly.

Python also generally prefers that spaces are used in place of tabs for indentation.  Set the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[AutoConvertTabsToSpaces](xref:ActiproSoftware.Text.ITextDocument.AutoConvertTabsToSpaces) property to `true` to force the use of spaces.

This code sets the filename and tab options for a document in a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor), whose instance is in the `editor` variable:

```csharp
editor.Document.FileName = @"C:\mymodule.py";
editor.Document.TabSize = 4";
editor.Document.AutoConvertTabsToSpaces = true
```

## Assembly Requirements

The following list indicates the assemblies that are used with the Python syntax language implementation in this add-on.

| Assembly | Required | Author | Licensed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Text.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core text/parsing framework for @@PlatformName |
| ActiproSoftware.Text.LLParser.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | LL parser framework implementation |
| ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core framework for all Actipro @@PlatformName controls |
| ActiproSoftware.SyntaxEditor.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | SyntaxEditor for @@PlatformName control |
| ActiproSoftware.Text.Addons.Python.@@PlatformAssemblySuffix.dll | Yes | Actipro | Python Language Add-on | Core text/parsing for the Python languages |
| ActiproSoftware.SyntaxEditor.Addons.Python.@@PlatformAssemblySuffix.dll | Yes | Actipro | Python Language Add-on | SyntaxEditor for @@PlatformName advanced Python syntax language implementations |
