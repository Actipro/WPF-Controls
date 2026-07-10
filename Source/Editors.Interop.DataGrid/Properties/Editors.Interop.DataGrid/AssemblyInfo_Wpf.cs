#if NET
using System.Runtime.Versioning;
#endif

[assembly: CLSCompliant(true)]

#if NET
[assembly: SupportedOSPlatform("windows")]
#endif

// XAML assembly attributes

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.None)]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/datagrideditors", "datagrideditors")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/datagrideditors", "ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid")]
