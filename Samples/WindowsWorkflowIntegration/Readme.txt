This sample shows how to host the Windows Workflow designer in a WPF application.

It is loosely based on a MSDN sample, but has been enhanced to show integration 
with several Actipro products:

1) The main window uses Actipro Docking/MDI to show a workflow designer in a tabbed 
document.  Properties and ToolBox tool windows are added.

2) An Actipro SyntaxEditor in single line edit mode is injected in place of an 
expression editor.  It uses the .NET Languages Add-on's VB language and combines 
that with header/footer text to make for an expression editing environment with 
full automated IntelliPrompt features.

3) The entire application has been themed with an accented theme from Actipro Themes.

Just wire up the project references to the latest appropriate Actipro WPF Controls
assemblies and check it out.  Contact our support team if you have any questions.


-------------------------------------------------------------------------------------


Update History

2/18/2021:
- Completely refactored to the sample to place all files in a single, organized project with all types and members commented.

11/16/2020:
- Set SyntaxEditor.IsDefaultContextMenuEnabled = false since context menus steal focus and cause the designer to exit the expression editor.
- Updated to fix the breaking chnage made to Themes in v2020.1.

7/16/2020:
- Updated the completion list to open when a new word is starting to be typed.

8/14/2019:
- Fixed completion list issues introduced in a previous build.

7/30/2019:
- Added a SyntaxEditorThemeCatalogRegistrar.Register call to the static VBExpressionEditorSyntaxLanguage constructor
  to prevent a focus loss the first time an expression is edited due to themes getting registered and templates applied.

7/25/2019:
- Updated to fix the breaking changes made to SyntaxEditor in v2019.1.

9/25/2018:
- Updated Language.GetHeaderText to add namespace imports for collections, and to support generic collections.

2/4/2015:
- Updated to fix the breaking changes made to Docking/MDI in v2016.1.
- Updated application startup to apply an accented Metro theme.

11/4/2015:
- Moved all the document header/footer text determination to the Language.cs file so all language-related
  logic is together.

4/8/2015:
- Updated the CreateExpressionEditor method to pass along the variables to the Instance and updated
  InitializeHeaderAndFooter to use those variables instead.  This change allows root activities other
  than Sequence to support variables.

3/18/2014:
- Added a custom completion ListBox implementation to work around issue where ListBoxItem would steal focus
  on click and Workflow Designer thought that it then had to close the expression editor.

11/22/2013:
- Updated App.OnStartup code to theme native WPF controls using Actipro themes.
- Added a Menu above the designer with options for closing the window, and toggling the tool window visibility.

11/21/2013:
- Updated to properly commit changes on editor focus loss.
- Updated the language to be a single instance instead of getting created for each editor instance, which improves performance.
- Updated to focus editor properly on the first mouse click.
- Added proper notifications of the expression editor's events.
