---
title: "Resolver"
page-title: "Resolver - SyntaxEditor Python Language Add-on"
order: 7
---
# Resolver

Each Python [IProject](xref:ActiproSoftware.Text.Languages.Python.Reflection.IProject) has an [IResolver](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver) that can examine a Python context and AST expression and return the results associated with that expression.  The resolver is used by the automated IntelliPrompt features to determine what to show in the UI for various scenarios.

An [IResolver](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver) instance is automatically created by the Python [Project](xref:ActiproSoftware.Text.Languages.Python.Reflection.Implementation.Project) when it is instantiated.

While it's not normally necessary to directly use the resolver (since it internally helps drive the built-in automated IntelliPrompt features), there are several ways to externally adjust how it functions.  These are described below.

## Unknown Return Type Reference Callback

Since the Python add-on doesn't have a full interpreter to run, sometimes the return types of functions will be unknown to it.  When the resolver is unable to determine the type that is returned by a function, variable, etc., it will call the [IResolver](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver).[UnknownReturnTypeReferenceCallback](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver.UnknownReturnTypeReferenceCallback) if one has been specified.

This code shows how a callback can be created to return types for functions, variables, etc. in scenarios where the resolver couldn't determine the type.

```csharp
...
project.Resolver.UnknownReturnTypeReferenceCallback = MyUnknownReturnTypeReferenceCallback;
...
private static ITypeReference MyUnknownReturnTypeReferenceCallback(IReflectionDefinition refDef) {
	var funcDef = refDef as IFunctionDefinition;
	if (funcDef != null) {
		if (funcDef.DeclaringModule != null) {
			// Examine the name of the declaring module
			switch (funcDef.DeclaringModule.Name) {
				case "somemodule":
					if (funcDef.DeclaringType != null) {
						// Examine the name of the declaring type
						switch (funcDef.DeclaringType.Name) {
							case "SomeClass":
								// Examine the name of the method
								switch (funcDef.Name) {
									case "somemethod":
										// Return a 'SomeOtherClass' type defined in a 'someotherclass.py' module
										return new TypeReference(Path.Combine(pathToTheReturnTypeModule, "someotherclass.py"), "SomeOtherClass");
								}
								break;
					}
					else {
						// Examine the name of the function (not a method since there is no DeclaringType)
						switch (funcDef.Name) {
							case "somefunction":
								// Return the built-in 'int' type
								return new TypeReference(null, "int");
						}
					}
					break;
			}
		}
	}
			
	return null;
}
```

## Maximum Import Recursion Level

Some very large external Python packages involve lots of import recursions, where certain modules import other modules/packages, which in turn import other modules/packages down the line.  While the Python resolver has a lot of optimizations and caching mechanisms to make examination of those imports as quick as possible, packages with many hundreds of interconnected modules might experience a brief slowdown as the resolver processes.  This may manifest itself as a brief pause while bringing up an IntelliPrompt completion list, etc.

There is an option via the [IResolver](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver).[MaximumImportRecursionLevel](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver.MaximumImportRecursionLevel) property that can be changed to limit how many import recursions deep the resolver can go.  The default value is `10`.  If the root module has an import statement, as the resolver navigates down to the imported module/package, that is the first recursion.  If that imported module/package has another import that would affect the resolver results, navigating into this other import would be the second recursion.  By making the `MaximumImportRecursionLevel` a smaller value, it prevents the resolver from going too deeply into import recursions (thus possibly improving resolution speed), but also may cause only a subset of the real resolver results to be returned.
