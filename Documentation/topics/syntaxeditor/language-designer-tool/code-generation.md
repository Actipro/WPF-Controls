---
title: "Code Generation"
page-title: "Code Generation - SyntaxEditor Language Designer Tool"
order: 8
---
# Code Generation

The Language Designer's main purpose is to generate code that allows you to load custom syntax languages at run-time in your applications.

## Configuration

Click the **Configuration** button in the Code Generation ribbon group to open the code generation configuration pane.

On this pane, two fields must be entered. **Output namespace** indicates the namespace in which any generated C#/VB code is placed. **Output folder path** indicates the folder in which code files are written when generated.  It can be any valid path on your hard drive.

## Selecting the Output Platform

The Language Designer can output code files for the WPF, UWP, or WinForms platforms.  The **Output platform** selection affects the namespaces that are imported, so be sure to pick the correct platform for your application.

## Language Definition vs. Code File Output

A checkbox provides an option for generating a language definition instead of C#/VB code files.

Language definitions are intended to be portable XML files that can be distributed with an application and loaded on the fly to configure a language.  Use these if you plan on allowing end users to modify language definitions, or if you simply don't wish to deal with dedicated C#/VB code files.

The other option is to generate dedicated C#/VB code files for the language.  In this scenario, you create a language at run-time simply by creating a new instance of the generated language class.

## Selecting the Output Language

Code file output can occur in C# or VB, and is generated using CodeDOM.  Use the Options tab in the ribbon to switch between the various output language options.

## Previewing Generated File

If there are no project build errors, you can preview any of the generated code files by double clicking on the item in the code files list.  This opens a new document tab with a complete preview of the code that will be generated.  Selecting one or more code files and pressing the **Preview Selected** button also works.

## Saving Generated Files

If there are no project build errors, you can select one or more files and press the **Generated Selected** or **Generate All** buttons to save files to the hard drive in the output folder path.  Please note that generated files will overwrite any existing files with the same name.

## Usage of Generated Files

If you are generating a language definition (.langdef) file, you can either include the file in a folder under your application's main folder or can include the file as an embedded resource in your application project.  Then at run-time, create a [SyntaxLanguageDefinitionSerializer](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer) object, and use its @if (wpf winforms) {[LoadFromFile](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.LoadFromFile*) or}[LoadFromStream](xref:ActiproSoftware.Text.Implementation.SyntaxLanguageDefinitionSerializer.LoadFromStream*) method to create a [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage)-based class that is initialized with all the information in the language definition.

If you are generating code files instead, copy all the generated C#/VB code files to your application project and include them as compilable code.  To begin using a language, just create an instance of the syntax language class that was generated.  The constructor for the language class has all the information in it to configure itself.  All generated classes are marked as partial classes so you can further extend them in other files without worrying about your code customizations being wiped out when code is generated again.
