using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using ActiproSoftware.Windows.Themes;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("Actipro DataGrid Contrib (for WPF)")]
[assembly: AssemblyDescription("Common extensions, behaviors, and themes for the WPF DataGrid control.")]
// [assembly: AllowPartiallyTrustedCallers()] - Commented out because causes "System.Security.VerificationException: Operation could destabilize the runtime." in .NET 4.5
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Actipro Software LLC")]
[assembly: AssemblyProduct("DataGrid.Contrib")]
[assembly: AssemblyCopyright("Copyright (c) 2009-2021 Actipro Software LLC.  All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/datagrid", "datagrid")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/datagrid", "ActiproSoftware.Products.DataGrid.Contrib")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/datagrid", "ActiproSoftware.Windows.Controls.DataGrid")]

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

[assembly: AssemblyVersion("21.1.2.0")]  // WPF
[assembly: AssemblyInformationalVersion("21.1.2.0")]  // WPF

namespace ActiproSoftware.Products.DataGrid.Contrib {

	/// <summary>
	/// Provides a class for retrieving information about the <c>ActiproSoftware.Charts</c> assembly.
	/// </summary>
	public sealed class AssemblyInfo : ActiproSoftware.Products.AssemblyInfo {

		private static AssemblyInfo instance;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyInfo</c> class.
		/// </summary>
		/// <remarks>
		/// The default constructor initializes all fields to their default values.
		/// </remarks>
		private AssemblyInfo() {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the instance of the <see cref="ActiproSoftware.Products.AssemblyInfo"/> class for this assembly.
		/// </summary>
		/// <value>The instance of the <see cref="ActiproSoftware.Products.AssemblyInfo"/> class for this assembly.</value>
		public static AssemblyInfo Instance {
			get {
				if (AssemblyInfo.instance == null)
					AssemblyInfo.instance = new AssemblyInfo();

				return AssemblyInfo.instance;
			}
		}
		
		/// <summary>
		/// Gets the type of license that is available for the assembly.
		/// </summary>
		/// <value>A <see cref="AssemblyLicenseType"/> specifying the type of license that is available for the assembly.</value>
		public override AssemblyLicenseType LicenseType { 
			get {
				#if BETA
					return AssemblyLicenseType.Beta;
				#elif PRERELEASE
					return AssemblyLicenseType.PreRelease;
				#else
					return AssemblyLicenseType.Full;
				#endif
			}
		}

		/// <summary>
		/// Gets the target platform for the assembly.
		/// </summary>
		/// <value>A <see cref="AssemblyPlatform"/> specifying the target platform for the assembly.</value>
		public override AssemblyPlatform Platform { 
			get {
				#if SILVERLIGHT
				return AssemblyPlatform.Silverlight;
				#elif WINRT
				return AssemblyPlatform.WinRT;
				#else
				return AssemblyPlatform.Wpf;
				#endif
			}
		}

		/// <summary>
		/// Gets the product code of the assembly.
		/// </summary>
		/// <value>A three-letter product code of the assembly.</value>
		public sealed override string ProductCode { 
			get {
				return "MDG";
			}
		}
		
		/// <summary>
		/// Gets the product ID of the assembly.
		/// </summary>
		/// <value>The product ID of the assembly.</value>
		public sealed override int ProductId { 
			get {
				return 0x0;
			}
		}
		
		#if WPF
		/// <summary>
		/// Gets the <see cref="ImageSource"/> for the product logo.
		/// </summary>
		/// <value>The <see cref="ImageSource"/> for the product logo.</value>
		public static ImageSource ProductLogoImageSource => Instance.GetImageSource("DataGridLogo.xaml");
		#endif

	}

}