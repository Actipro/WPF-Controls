---
title: "Getting Started"
page-title: "Getting Started - Wizard Reference"
order: 2
---
# Getting Started

Getting up and running with Wizard is extremely easy.  Wizard provides all the wizard-related user interface features right out of the box, saving you hours of work.

This topic's information will assume you are using Visual Studio to write your XAML code for a WPF `Window` that will contain a [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) control.

## Add Assembly References

First, add references to the `ActiproSoftware.Shared.Wpf.dll` and `ActiproSoftware.Wizard.Wpf.dll` assemblies.  They should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## The Visual Studio Item Templates (Wizard, Aero Wizard)

If you have Visual Studio, item templates named `Wizard Window (WPF)` and `Aero Wizard Window (WPF)` should have been installed during the WPF Studio installation procedure.

When you wish to create a new `Window` containing a `Wizard` or `AeroWizard` in your application, simply choose `Add New Item` in Visual Studio and select the appropriate item template.  A `Window` with a `Wizard` and a couple pages already on it will be added to your project and opened.

The use of item templates is the fastest way to get started with our products in your own applications.

## Create a WPF Window

Next, create a new empty WPF `Window` that looks like this:

```xaml
<Window x:Name="window" x:Class="Sample.Application.Window"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	Width="600" SizeToContent="Height" ResizeMode="CanResize"
	>
	
</Window>
```

Note that by using those `Width`, `SizeToContent`, and `ResizeMode` settings, the `Window` will keep a fixed width of `600` pixels but will grow vertically as needed to fit the contents of the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) we will add next.

## Declare the Wizard XML Namespace

Now in the root `Window` tag, add an `xmlns` declaration for the wizard namespace like this:

```xaml
<Window x:Name="window" x:Class="Sample.Application.Window"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
	xmlns:wizard="http://schemas.actiprosoftware.com/winfx/xaml/wizard"
	Width="600" SizeToContent="Height" ResizeMode="CanResize"
	>
	
</Window>
```

The xmlns attribute declares that the use of the `wizard` namespace in this `Window` refers to types in the `ActiproSoftware.Windows.Controls.Wizard` namespace within the `ActiproSoftware.Wizard.Wpf` assembly.

## Add a Wizard Control

Next, add a [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) control as the sole child of the `Window`:

```xaml
	<wizard:Wizard x:Name="wizard" WindowTitleBehavior="PageTitle" WindowTitleBaseText="Wizard Sample" PageSequenceType="Stack" 
		BackwardProgressTransitionDuration="0:0:0.5" ForwardProgressTransitionDuration="0:0:0.5">
		<wizard:Wizard.TransitionSelector>
			<shared:MultiTransitionSelector>
				<!-- This adds a single bar wipe transition -->
				<shared:BarWipeTransition />
			</shared:MultiTransitionSelector>
		</wizard:Wizard.TransitionSelector>
		
	</wizard:Wizard>
```

Note that several optional attributes were also added, making the Wizard add the current page's [Title](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.Title) property to the `Window`'s title, setting up [stack-based page sequencing](navigation-features/page-sequencing.md), and activating half-second wipe [transition effects](appearance-features/transition-effects.md).

## Add a Welcome Page

Next, add a welcome [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) as an item of the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) control:

```xaml
<wizard:WizardPage x:Name="welcomePage" PageType="Exterior"
				Caption="Welcome to the Wizard Sample Application"
				Description="Thank you for downloading the Actipro Wizard control.">
			<Grid>
				<Grid.RowDefinitions>
					<RowDefinition Height="*" />
					<RowDefinition Height="Auto" />
				</Grid.RowDefinitions>

				<TextBlock Grid.Row="1" TextWrapping="Wrap">To continue, click Next.</TextBlock>
			</Grid>
		</wizard:WizardPage>
```

The welcome page is an [exterior page](page-button-features/page-types.md), which means it has a watermark area on the left side.  It can be further customized to add header images and backgrounds later but for now we will stick with the default.

## Add an Interior Page

Next, add an interior [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) as an item of the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) control after the existing welcome page:

```xaml
		<wizard:WizardPage x:Name="interiorPage" 
				Caption="Interior Page Sample"
				Description="This is a sample interior page that represents a step in your wizard." 
				Title="Interior Page">
			<TextBlock TextWrapping="Wrap">Add your content here.</TextBlock>
		</wizard:WizardPage>
```

## Customize These Pages and Add More Pages

Now simply customize these pages by adding more content and perhaps some images.  Then continue adding more pages as needed to complete your wizard.

As the sample currently stands, it will iterate through the pages in sequential order and will enable the Finish button on the final page.

Also ensure that the wizard is set up to receive focus when the `Window` opens.  This is accomplished by setting the attached `FocusManager.FocusedElement` property to the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) control instance.  When the wizard control or its visual descendants have focus, changing pages will move focus within the newly-selected page.

The complete sample code is:

```xaml
<Window x:Name="window" x:Class="Sample.Application.Window"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
	xmlns:wizard="http://schemas.actiprosoftware.com/winfx/xaml/wizard"
	Width="600" SizeToContent="Height" ResizeMode="CanResize"
	FocusManager.FocusedElement="{Binding ElementName=wizard}"
	>
	
	<wizard:Wizard x:Name="wizard" WindowTitleBehavior="PageTitle" WindowTitleBaseText="Wizard Sample" PageSequenceType="Stack" 
		BackwardProgressTransitionDuration="0:0:0.5" ForwardProgressTransitionDuration="0:0:0.5">
		<wizard:Wizard.TransitionSelector>
			<shared:MultiTransitionSelector>
				<!-- This adds a single bar wipe transition -->
				<shared:BarWipeTransition />
			</shared:MultiTransitionSelector>
		</wizard:Wizard.TransitionSelector>
		
		<wizard:WizardPage x:Name="welcomePage" PageType="Exterior"
				Caption="Welcome to the Wizard Sample Application"
				Description="Thank you for downloading the Actipro Wizard control.">
			<Grid>
				<Grid.RowDefinitions>
					<RowDefinition Height="*" />
					<RowDefinition Height="Auto" />
				</Grid.RowDefinitions>

				<TextBlock Grid.Row="1" TextWrapping="Wrap">To continue, click Next.</TextBlock>
			</Grid>
		</wizard:WizardPage>
		
		<wizard:WizardPage x:Name="interiorPage" 
				Caption="Interior Page Sample"
				Description="This is a sample interior page that represents a step in your wizard." 
				Title="Interior Page">
			<TextBlock TextWrapping="Wrap">Add your content here.</TextBlock>
		</wizard:WizardPage>
		
	</wizard:Wizard>
	
</Window>
```

## The "Getting Started" QuickStart

The "Getting Started" QuickStart for Wizard (source code is in the sample project) has implemented the full source for this topic and is a great place to copy code from to start on your own wizards.

To use the source code files for the "Getting Started" QuickStart, run the Sample Browser and navigate to the "Getting Started" QuickStart.  Then click the Open Folder button above it to open an explorer window in the folder that contains the source code.  You can copy those files to your own project.  Alternatively, just use the Visual Studio item template we've created (described above).

## Further Study

It's very easy to use Wizard and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of Wizard.

If you require further assistance after looking through those, please visit our support forum for the product.
