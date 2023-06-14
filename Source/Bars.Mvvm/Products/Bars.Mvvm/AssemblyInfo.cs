using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Windows.Markup;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("Actipro Bars MVVM (for WPF)")]
[assembly: AssemblyDescription("Common view models, templates, and other types used when implementing Bars controls with MVVM (Model-View-ViewModel) patterns.")]
// [assembly: AllowPartiallyTrustedCallers()] - Commented out because causes "System.Security.VerificationException: Operation could destabilize the runtime." in .NET 4.5
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Actipro Software LLC")]
[assembly: AssemblyProduct("Bars.Mvvm")]
[assembly: AssemblyCopyright("Copyright (c) 2009-2023 Actipro Software LLC.  All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en")]

#if NET
[assembly: SupportedOSPlatform("windows")]
#endif

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/barsmvvm", "barsmvvm")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/barsmvvm", "ActiproSoftware.Windows.Controls.Bars.Mvvm")]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/themes", "themes")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/themes", "ActiproSoftware.Windows.Themes")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("23.1.2.0")]  // WPF
[assembly: AssemblyInformationalVersion("23.1.2.0")]  // WPF
