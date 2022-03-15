---
title: "Licensing"
page-title: "Licensing"
order: 21
---
# Licensing

This topic talks about the various product licensing options available, and walks through how to apply licensing when both evaluating and after a purchase.

## License Types

### Evaluation License

Under the Evaluation License, you may only use the product for evaluation purposes for a period of 30 days, after which you must either remove all files associated with the product or purchase a Single Developer License for each individual developer on a project that uses the product.

Evaluation versions of the products may include some background watermarks or other means of displaying they are not licensed versions, all of which are removed once licenses are purchased.

### Single Developer License

Single Developer Licenses must be purchased for each individual developer on a project using a product and may NOT be transferred between developers.  This license grants each developer the right to use the purchased products in production applications and to include any files marked as redistributable with their applications.

### Site License

Site licenses may be purchased, automatically giving all developers at a physical location a Single Developer License.

### Enterprise License

Enterprise licenses may be purchased, automatically giving all developers in your entire organization and at all sites a Single Developer License.

### Blueprint License (Source Code)

Blueprint licenses enable access to product source code.  The term of Blueprint licenses must be kept in sync with product subscriptions.  Please read the EULA to see the Blueprint license terms and conditions.

## Free Subscriptions and Free New Products

All of the non-evaluation licenses described above for our WPF products include **one full year** of free upgrades to any new versions that are released within that timeframe.

In addition, customers with active WPF Studio subscriptions will receive any new products added to the suite for free while the subscription is active.

## No Runtime, Server, Distribution Royalties

Actipro Software control products have **NO** runtime, server, or distribution royalties if you own the proper amount of developer licenses for your development team (see above).

## Purchasing Licenses

Licenses may be purchased directly from the [Actipro web site](https://www.actiprosoftware.com/purchase/cart).  After a purchase has been made and the purchase has been processed, you will be e-mailed information on how to log into your Actipro account on our web site. Use the login information contained within the e-mail to log into the site.

Once you are within your account on our web site, download the latest release and e-mail yourself your license key.  Then be sure to follow the instructions in the following sections to properly license the product for use in your applications.

## Two Options to Apply Licensing

There are two ways to apply licensing once you have purchased licenses for Actipro controls.  Both cases require you to know the "Licensee" and "License Key" (license information) for the major.minor version you have licensed.

> [!NOTE]
> Each major.minor version has its own distinct license information that can be obtained for licensed versions on [your Actipro account](https://www.actiprosoftware.com/support/account)'s page for your organization.

The first licensing option is to make a method call at app startup before any UI is referenced to register your license information.  This option is quick and easy, and works best for scenarios where Actipro [NuGet packages](nuget.md) or build servers are used.  See the "Licensing Via a RegisterLicense Call" section below for detailed information on this option.

The second licensing option is to provide licensing via a licenses.licx file.  This is the classic style of implementing licensing in .NET Framework applications, and is what Actipro exclusively used prior to v2020.1.  See the "Licensing Via a Licenses.licx File" section below for detailed information on this option.

## Licensing Via a RegisterLicense Call

This licensing option is ideal when Actipro [NuGet packages](nuget.md) or build servers are used.

In this licensing option, we make a single code-based call to the `ActiproLicenseManager.RegisterLicense` method at application startup, before any UI is referenced.  This static call registers your license information globally throughout your application.

The following code calls the `ActiproLicenseManager.RegisterLicense` method using "licensee" and "licenseKey" variables containing string values that must exactly match the license information in your license e-mail:

```csharp
public partial class App : Application {
	protected override void OnStartup(StartupEventArgs e) {
		// NOTE: Set "licensee" and "licenseKey" variables to your license information
		ActiproSoftware.Products.ActiproLicenseManager.RegisterLicense(licensee, licenseKey);
		base.OnStartup(e);
	}
}
```

When using this licensing option, do NOT include any Actipro entries in your application's "licenses.licx" file.

> [!NOTE]
> It is important to protect your licensee and license key combination from decompilers.  We highly recommend using some form of string encryption on the "licensee" and "licenseKey" values passed into the `ActiproLicenseManager.RegisterLicense` method.  Many obfuscators include string encryption as an option, or you can use other custom logic to scramble/descramble the strings.

## Licensing Via a Licenses.licx File

This licensing option is the classic style of implementing licensing in .NET Framework applications, and is what Actipro exclusively used prior to v2020.1.  Note that .NET Framework applications can either use this licensing option or the `RegisterLicense` call licensing option described above.

### Summary of Licenses.licx File Licensing Steps

- Enter your Licensee and License Key during install
- Compile our sample project to see if you entered them correctly
- Update your project's "licenses.licx" file and you should be done

### Detailed Licenses.licx File Licensing Steps

The following detailed steps indicate how to install and license Actipro products for both fresh install and upgrade situations.

1. First, obtain the license information (Licensee and License Key) to use.
    
    - If you have purchased licenses for the product, e-mail yourself the licensing information for the product from [your Actipro account](https://www.actiprosoftware.com/support/account)'s page for your organization.
    
    - If you are evaluating, the Actipro products should already be pre-configured for evaluation and may show nag screens.  No further action is needed by you to register an evaluation license.

1. If you don't already have the latest product build installed, or if you need to enter your license information...
    
    - Download the latest build of the product from [your Actipro account](https://www.actiprosoftware.com/support/account)'s page for your organization.
    
    - Uninstall any old version of the product that is on your machine.
    
    - Run the installer for the latest build on your machine.
    
    - If you are a licensed customer, in the installer, check the "I have already purchased licenses for this product" box and enter your license information **exactly as specified** in the e-mail you sent to yourself in an earlier step.
    
    - Review the configuration options that you'd like to install.  For build servers, the documentation, samples, tools, and designer integration install options may be turned off.
    
    - Complete the install.

1. After completing the installation, we highly recommend that you open one of our sample projects for the product and try to rebuild its solution and run it.  If it runs without any license popup window displaying, then you installed the control correctly.  If it displays a license popup window, then you most likely did not enter the Licensee or License Key correctly in the previous step and need to reinstall.  Again, ensure both of them match exactly with what is in our license e-mail.

1. When adding products to your application the first time...
    
    - Add references to the Actipro product assemblies that are being used.  Default assembly install paths are indicated in the Readme file.  Please note that the "ActiproSoftware.Shared.Wpf.dll" assembly is always required since all products depend on it.

1. When upgrading products to new versions in an application that used old versions...
    
    - Ensure the references to the Actipro product assemblies are correctly pointing to the new version's assemblies you installed.  Default assembly install paths are indicated in the Readme file.  Update the references as needed to make sure the versions are correct.

1. If you have licensed the product, configure the "licenses.licx" file in the projects that reference the product.
    
    - Open your project in Visual Studio and ensure that there is a "licenses.licx" file in it.  If your project does not contain a "licenses.licx" file, follow the steps in the "Troubleshooting / No licenses.licx file exists in my project" section below to create a "licenses.licx" file manually.
    
    - Ensure that the "licenses.licx" has a correct entry for Actipro's @@PlatformName control products.  The valid entry is listed below in this topic.  Ensure the version numbers in the entry always match the deployed assembly version, especially after upgrading to a new version or build.

1. In Visual Studio, execute "Clean Solution" and then "Rebuild Solution".

After following these steps, licensing will be properly applied to your application.

### Licenses.licx Files and Valid Entries

A "licenses.licx" file is a file that Visual Studio maintains to know what third-party licensed components and controls are included in a project.  Visual Studio then uses that information to know what license data it needs to include in the output assembly for your project.  Therefore, if you use a third-party component and don't have the proper entries in your project's "licenses.licx" file, no licensing information will be included in your project's output assembly and licensing popup windows will display when your application is run on end user computers.

The contents of a "licenses.licx" file are pretty simple.  It needs a single line that supplies license information to all Actipro @@PlatformName control products for which you are licensed.  Make sure that the version numbers, including build, match exactly with the version number of the control that is installed.

This single line (update the version to match the one you use) should be added to the "licenses.licx" file in any project that uses Actipro @@PlatformName control or SyntaxEditor add-on products:

```
ActiproSoftware.Products.ActiproLicenseToken, ActiproSoftware.Shared.Wpf, Version=22.1.1.0, Culture=neutral, PublicKeyToken=36ff2196ab5654b9
```

### Notes on Build Machines When Using Licenses.licx Files

Just like when compiling on developer machines, build machines need to be able to locate license information, so that Visual Studio and/or MSBuild can embed it in your application.  Therefore after licensing the product, it must be installed using the already-licensed option at least once on any build machine that will be compiling your application.  This process installs the license information into the machine's registry.  The install can be done with minimal options and can be uninstalled immediately after.  The license information will remain in the machine's registry.

This process is only necessary once per major/minor version (e.g. 2020.1).  It doesn't need to be repeated for maintenance releases.  And it doesn't need to be done at all if you use the `RegisterLicense` call licensing option.

We do not charge additional licensing fees for build machines.

### Troubleshooting

#### I've purchased licenses and followed the steps above but a license popup window says 'Error: The licensee or license key that was entered is invalid'.

This means that you didn't enter the Licensee and License Key exactly as specified in our license e-mail.  Reinstall the product and be sure that both of those items match exactly with what is in our license e-mail.  The most common licensing mistake is when the Licensee (organization name) doesn't match what is in the license e-mail.

#### No licenses.licx file exists in my project.

If you don't see a "licenses.licx" file in your Visual Studio project (generally in the "Properties" folder), first ensure that the "Show All Files" option is set.  The "Show All Files" command is in the Visual Studio Solution Explorer tool window's toolbar.  If you still don't see one, follow these steps:

- Right click on your project and select "Add -> Add New Item".
- Select a "Text File" and name it "licenses.licx".
- Ensure its "Build Action" is set to "Embedded Resource".  This can be done in the Visual Studio "Properties" tool window when the "licenses.licx" file is selected.
- Copy the line from the sample "licenses.licx" file above into it.

#### A licenses.licx file exists but there is no entry for the Actipro Software product I purchased.

You can add the necessary line manually to the file.  Copy the line into your application's "licenses.licx" file from the sample "licenses.licx" file above.

#### A licenses.licx file exists and I've copied the entry above, but I still get a license popup window.

Make sure the licenses.licx file is included as an "Embedded Resource" in your project.  The license popup window should bold the product name that caused it to show, and icons next to each product indicate if they are covered under your license.  Also note that the SyntaxEditor language add-ons are sold separately from SyntaxEditor and WPF Studio.

#### A licenses.licx file exists and there is an entry for the Actipro Software products, however the version number is wrong.

This situation can occur if you upgraded to a new version.  Be sure that you update the version number in the "licenses.licx" in every project that uses the product.  The version number should be the same as the version number of the product that is used.  Also ensure that the "PublicKeyToken" entry matches with the entry in the sample "licenses.licx" file above.

#### I've purchased licenses, installed using the already licensed option, and followed the steps above but a license popup window says 'Evaluation Release'.

Windows has a feature where it may virtualize registry entries that are made by the evaluation release of our products post-installation.  This is called the "VirtualStore".  What happens is that an evaluation registry gets cached in the virtual store and Visual Studio may choose to use that instead of a full release license key that you installed in the setup program.  To clear this up, use "regedit", go to this key and remove it:

```
HKEY_CURRENT_USER\Software\Classes\VirtualStore\MACHINE\SOFTWARE\[Wow6432Node]\Actipro Software\WPF Controls\[Version]
```

The "Wow6432Node" part of the path above is only used on 64-bit machines, leave it out on 32-bit machines.  Then rebuild your project and it should pull in the correct license key you entered during installation.

## Licensing Questions?

If you have any further questions regarding purchasing or licensing, please [contact us](http://www.actiprosoftware.com/company/contact).  We're happy to help.
