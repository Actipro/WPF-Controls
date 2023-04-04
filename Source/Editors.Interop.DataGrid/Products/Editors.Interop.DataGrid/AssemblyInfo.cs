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
[assembly: AssemblyTitle("Actipro Editors/DataGrid Interop (for WPF)")]
[assembly: AssemblyDescription("Provides interoperability between Actipro's WPF Editors product and the WPF DataGrid.")]
// [assembly: AllowPartiallyTrustedCallers()] - Commented out because causes "System.Security.VerificationException: Operation could destabilize the runtime." in .NET 4.5
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Actipro Software LLC")]
[assembly: AssemblyProduct("Editors.Interop.DataGrid")]
[assembly: AssemblyCopyright("Copyright (c) 2009-2023 Actipro Software LLC.  All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en")]

#if NET
[assembly: SupportedOSPlatform("windows")]
#endif

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/datagrideditors", "datagrideditors")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/datagrideditors", "ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid")]

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

[assembly: AssemblyVersion("23.1.0.0")]  // WPF
[assembly: AssemblyInformationalVersion("23.1.0.0")]  // WPF
