---
title: "Troubleshooting (General)"
page-title: "Troubleshooting (General)"
order: 40
---
# Troubleshooting (General)

This topic provides several tips on common questions or issues that you may encounter on the products included in WPF Studio.

There also are some product-specific troubleshooting topics that provide information related to individual products:

- [PropertyGrid Troubleshooting](grids/troubleshooting.md)
- [Ribbon Troubleshooting](ribbon/troubleshooting.md)
- [Themes Troubleshooting](themes/troubleshooting.md)
- [Wizard Troubleshooting](wizard/troubleshooting.md)

## First Time Running the Sample Browser Results in 'SampleBrowser/MainWindow not found' Error

If this error message occurs the first time you attempt to compile and run the Sample Browser project in Visual Studio, simply Clean the solution and then Rebuild it.  That should resolve the problem.

## Designer Doesn't Load When Opening XAML File in Sample Browser

The first time you open the sample browser project and try to open a XAML file, you may encounter designer errors where the designer doesn't load properly.  This is due to the Sample Browser never being compiled on your system yet.

To resolve this issue, simply close the XAML files, rebuild the Sample Browser solution, and reopen them.  Everything should work fine from that point on.

## Designer Errors Reported That Don't Occur At Run-Time

If the Visual Studio designer is reporting errors that aren't errors that occur at run-time, such as "The member 'Foo' is not recognized or is not accessible.", it could be that you haven't referenced the `ActiproSoftware.Shared.Wpf.dll` assembly, which is the Shared Library.  This core assembly is referenced by all our product assemblies and is required to be referenced and deployed with your app when using any of our control products.

Once you've ensured you have a reference to that assembly in your project, please rebuild the project and then reopen the designer.

## 'Task failed because LC.exe was not found' Error When Compiling Visual Studio Project

The symptom of this problem is the error message below shows when compiling a project in Visual Studio that is using Actipro components.

```
Task failed because "LC.exe" was not found, or the correct Microsoft Windows SDK is not installed.
```

That error may show up if your Visual Studio SDK installation is not correct or is missing on your machine.  When Visual Studio tries to compile a project that uses a licensed component (like ours or any other third party developer that makes licensed controls), it looks for LC.exe.  LC.exe is the license compiler application it uses to embed license data in the assembly it creates for your project.

You should be able to resolve this by running your Visual Studio installer and making sure you install the SDK option it provides.

Alternatively, you could look at the path indicated by the registry key indicated by this error message.  Then go to that path and make sure LC.exe is there.  You'd have to copy it from another similar SDK folder.

But to restate, this error is not something our product has caused.  It's due to an invalid Visual Studio SDK installation.  Compiling a project with any other third party licensed component would cause the same error to show in Visual Studio if the SDK is missing or invalid.

## ClickOnce Deployment Error Saying Actipro Assemblies Must Be in the GAC

If a message similar to the following appears when you attempt to run a ClickOnce-deployed application, you have not configured your project references correctly.

```
Unable to install or run the application. The application requires that assembly ActiproSoftware.???.Wpf Version ?.?.????.0 be installed in the Global Assembly Cache (GAC) first.
```

You must ensure that all Actipro assemblies that are needed by your project have their references set so `Copy Local` is `true`.  That will allow them to be deployed and copied to the client machine properly, making this error message disappear.

If you don't do this, the end user machine is unable to locate the Actipro assemblies and therefore throws this exception.

## Some String Resource Customizations Don't Take Effect in UI

If you run into a scenario where some string resource customizations do take effect in the UI but others don't, the problem is most likely the location of your string customization code.

As indicated by a note in the [Customizing String Resources](customizing-string-resources.md) topic, all string resources should be customized immediately at application startup (such as in `Application.OnStartUp`) and before any UI or control classes are even referenced.  The note in the topic gives more detail on why this is necessary.

## Data Binding Errors at Run-Time

Sometimes there may be some data binding errors that show up in the Visual Studio console window when executing an application that uses a WPF Studio product.  WPF Studio has some very large and complex templates for its products' controls and these error messages may show up in the VS console due to the timing between data binding resolution and visual tree creation.

It is very important to not that the data binding errors are NOT problems in our code.  If they were, the bindings would not work at all at run-time and you would see broken UI functionality.  This is not the case, everything works correctly at run-time after the visual tree has been fully constructed and the bindings have been re-evaluated by WPF.

We wanted to get more information on this issue for a customer who wrote us, so we asked Dr. WPF (a WPF expert) for an explanation.  Here's what he had to say:

```
You can reassure your customer that the binding trace output is merely there to help developers debug unresolved bindings.  
It does not indicate an error in your code.
						
There are several levels at which a binding may be resolved.  
Most bindings are resolved when they are first parsed.  
However, for templates and some other specific cases (like generated item containers), 
the elements may be parsed as they are added to a partial tree or while under a different parser context.  
In such a case, a perfectly good binding may fail to resolve on the first try.
						
If a binding fails to resolve, it is marked as such with an internal flag and a later attempt will be made to resolve the binding. 
Additionally, for a RelativeSource binding of FindAncestor, any change in the ancestor tree will cause the binding expression 
to be re-resolved.
						
Unfortunately, there is no way to turn off the trace output when running under the debugger.  
If a debugger is present, all errors and warnings will be issued. 
If a debugger is not present, the trace output will only be generated if a specific key in the registry is set.
						
As for how you convince your customer that this is not a real problem, 
I'd start by pointing out that the messages are coming through the "trace" pipeline... not the "debug" pipeline.  
Second, I'd reemphasize that if there was indeed a problem, you would see it in the UI.
```

So just to reiterate, the data binding error messages are not problems with our code, and are simple warnings due to data bindings trying to resolve themselves before WPF has created their targets' visual trees.  You may safely ignore these error messages.

## Using WPF Controls in Windows Forms Applications

Some customers embed controls like the WPF SyntaxEditor in their Windows Forms applications.  In cases where Image embedded resources are used (such as in SyntaxEditor IntelliPrompt popups), the pack:// URI syntax will fail with a `NotSupportedException` and message "The URI prefix is not recognized."

This is due to no WPF `Application` object being created by default in Windows Forms applications and thus the pack:// syntax isn't supported.  The WPF `Application` object can be programmatically created in your application startup before any UI is created like this:

```csharp
if (System.Windows.Application.Current == null) {
	wpfApplication = new System.Windows.Application();
	wpfApplication.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
}
```

The `wpfApplication` variable should be stored at your static Windows Forms application level and in your application shutdown code, the `wpfApplication.Shutdown()` method should be called to explicitly close the WPF `Application`.

The reason we need to use `ShutdownMode.OnExplicitShutdown` above is that by default, the last WPF window that closes will shut down the WPF application and it will be as if no application was created, breaking pack:// syntax and causing the `NotSupportedException` exception.  If you are still in evaluation mode, the Actipro evaluation dialog for the WPF Controls could trigger this when it closes.  By explicitly controlling the lifetime of the WPF application, you prevent that scenario from occurring.
