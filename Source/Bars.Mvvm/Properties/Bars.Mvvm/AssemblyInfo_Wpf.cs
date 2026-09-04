#if NET
using System.Runtime.Versioning;
#endif

[assembly: CLSCompliant(true)]

#if NET
[assembly: SupportedOSPlatform("windows")]
#endif

// XAML assembly attributes

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.None)]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/barsmvvm", "barsmvvm")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/barsmvvm", "ActiproSoftware.Windows.Controls.Bars.Mvvm")]

[assembly: XmlnsPrefix("http://schemas.actiprosoftware.com/winfx/xaml/themes", "themes")]
[assembly: XmlnsDefinition("http://schemas.actiprosoftware.com/winfx/xaml/themes", "ActiproSoftware.Windows.Themes")]
