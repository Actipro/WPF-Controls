#region Using directives

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Resources;
using System.Globalization;
using System.Windows;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("WPF Controls Sample Browser")]
[assembly: AssemblyDescription("Sample application to demo features of the Actipro WPF controls.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Actipro Software LLC")]
[assembly: AssemblyProduct("WPF Controls Sample Browser")]
[assembly: AssemblyCopyright("Copyright (c) 2007-2023 Actipro Software LLC.  All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

#if NET
[assembly: SupportedOSPlatform("windows")]
#endif

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


// Specifies the location in which theme dictionaries are stored for types in an assembly.
[assembly: ThemeInfo(
	// Specifies the location of system theme-specific resource dictionaries for this project.
	// The default setting in this project is "None" since this default project does not
	// include these user-defined theme files:
	//     Themes\Aero.NormalColor.xaml
	//     Themes\Classic.xaml
	//     Themes\Luna.Homestead.xaml
	//     Themes\Luna.Metallic.xaml
	//     Themes\Luna.NormalColor.xaml
	//     Themes\Royale.NormalColor.xaml
	ResourceDictionaryLocation.None,

	// Specifies the location of the system non-theme specific resource dictionary:
	//     Themes\generic.xaml
	ResourceDictionaryLocation.SourceAssembly)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("23.1.0.0")]
[assembly: AssemblyInformationalVersion("23.1.0.0 - 20230210")]
