#if NET
using System.Runtime.Versioning;
#endif

[assembly: CLSCompliant(true)]

#if NET
[assembly: SupportedOSPlatform("windows")]
#endif

// XAML assembly attributes

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/datagrid", "datagrid")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/datagrid", "ActiproSoftware.Properties.DataGrid.Contrib")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/datagrid", "ActiproSoftware.Windows.Controls.DataGrid")]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/themes", "themes")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/themes", "ActiproSoftware.Windows.Themes")]


namespace ActiproSoftware.Properties.DataGrid.Contrib;

partial class AssemblyInfo {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the <see cref="ImageSource"/> for the product logo.
	/// </summary>
	/// <value>The <see cref="ImageSource"/> for the product logo.</value>
	public static ImageSource? ProductLogoImageSource => Instance.GetImageSource("DataGridLogo.xaml");

}
