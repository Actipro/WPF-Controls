using System.Resources;
using System.Windows;

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