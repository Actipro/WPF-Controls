using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.SdiCodeEditor {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private bool				hasPendingParseData;
		
		// Project assemblies used by C#/VB in the .NET Languages Add-on
		private ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly cSharpProjectAssembly;
		private ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly vbProjectAssembly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Initialize the project assemblies for .NET Languages Add-on languages
			cSharpProjectAssembly = new ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpProjectAssembly("SampleBrowser");
			vbProjectAssembly = new ActiproSoftware.Text.Languages.VB.Implementation.VBProjectAssembly("SampleBrowser");
			var assemblyLoader = new BackgroundWorker();
			assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
			assemblyLoader.RunWorkerAsync();

			// Load the .NET Languages Add-on C# language (sold separately) by default
			this.LoadLanguage("C# (in .NET Languages Add-on)");

			// Register display item classification types
			new DisplayItemClassificationTypeProvider().RegisterAll();

			// Register class command bindings
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, OnNewExecuted));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecuted));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, OnPrintExecuted));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.PrintPreview, OnPrintPreviewExecuted, OnPrintPreviewCanExecute));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSaveExecuted, OnSaveCanExecute));

			this.Loaded += new RoutedEventHandler(OnLoaded);
        }

		private void DotNetProjectAssemblyReferenceLoader(object sender, DoWorkEventArgs e) {
			// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)

			cSharpProjectAssembly.AssemblyReferences.AddMsCorLib();
			SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(cSharpProjectAssembly);

			vbProjectAssembly.AssemblyReferences.AddMsCorLib();
			SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(vbProjectAssembly);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			ListBoxItem item = new ListBoxItem();
			item.Content = text;
			eventsListBox.Items.Add(item);
			eventsListBox.SelectedItem = item;
			eventsListBox.ScrollIntoView(item);
		}

		/// <summary>
		/// Loads a language, with example text.
		/// </summary>
		/// <param name="language">The <see cref="ISyntaxLanguage"/> to load.</param>
		private void LoadLanguage(ISyntaxLanguage language) {
			if (language == null)
				return;

			// Load the language
			editor.Document.Language = language;

			// Get example text
			IExampleTextProvider exampleTextProvider = editor.Document.Language.GetExampleTextProvider();
			if ((exampleTextProvider != null) && (exampleTextProvider.ExampleText != null))
				editor.Document.SetText(exampleTextProvider.ExampleText);

			// Update symbol selector visibility
			symbolSelector.Visibility = (language.GetNavigableSymbolProvider() != null ? Visibility.Visible : Visibility.Collapsed);
			symbolSelector.AreMemberSymbolsSupported = (language.Key != "Python");
		}

		/// <summary>
		/// Loads a language definition.
		/// </summary>
		/// <param name="languageKey">The key that identifies the language.</param>
		private void LoadLanguage(string languageKey) {
			// Clear errors and document outline
			errorListView.ItemsSource = null;
			astOutputEditor.Document.SetText("(Language may not have AST building features)");

			switch (languageKey) {
				case "Assembly":
					this.LoadLanguageDefinitionFromFile("Assembly.langdef");
					break;
				case "Batch file":
					this.LoadLanguageDefinitionFromFile("BatchFile.langdef");
					break;
				case "C":
					this.LoadLanguageDefinitionFromFile("C.langdef");
					break;
				case "C#":
					this.LoadLanguageDefinitionFromFile("CSharp.langdef");
					editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
					break;
				case "C# (in .NET Languages Add-on)": {
					// .NET Languages Add-on C# language
					var language = new ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage();
					language.RegisterService<ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly>(cSharpProjectAssembly);
					this.LoadLanguage(language);

					// Register a code snippet provider that has several snippets available
					ICodeSnippetFolder snippetFolder = SyntaxEditorHelper.LoadSampleCSharpCodeSnippetsFromResources();
					editor.Document.Language.RegisterService(new ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpCodeSnippetProvider() { RootFolder = snippetFolder });
					break;
				}
				case "C++":
					this.LoadLanguageDefinitionFromFile("Cpp.langdef");
					break;
				case "CSS":
					this.LoadLanguageDefinitionFromFile("Css.langdef");
					break;
				case "Custom...":
					this.LoadLanguageDefinitionFromFile(null);
					break;
				case "HTML":
					this.LoadLanguageDefinitionFromFile("Html.langdef");
					editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "<!--", EndDelimiter = "-->" });
					break;
				case "INI file":
					this.LoadLanguageDefinitionFromFile("IniFile.langdef");
					break;
				case "Java":
					this.LoadLanguageDefinitionFromFile("Java.langdef");
					editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
					break;
				case "JavaScript":
					this.LoadLanguageDefinitionFromFile("JavaScript.langdef");
					editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
					break;
				case "JavaScript (in Web Languages Add-on)":
					// Web Languages Add-on JavaScript language
					this.LoadLanguage(new ActiproSoftware.Text.Languages.JavaScript.Implementation.JavaScriptSyntaxLanguage());
					break;
				case "JSON (in Web Languages Add-on)":
					// Web Languages Add-on JSON language
					this.LoadLanguage(new ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage());
					break;
				case "JSON with Comments (in Web Languages Add-on)":
					// Web Languages Add-on JSON language
					this.LoadLanguage(new ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage(areCommentsSupported: true));
					break;
				case "Lua":
					this.LoadLanguageDefinitionFromFile("Lua.langdef");
					break;
				case "Markdown":
					this.LoadLanguageDefinitionFromFile("Markdown.langdef");
					break;
				case "MSIL":
					this.LoadLanguageDefinitionFromFile("Msil.langdef");
					break;
				case "Pascal":
					this.LoadLanguageDefinitionFromFile("Pascal.langdef");
					break;
				case "Perl":
					this.LoadLanguageDefinitionFromFile("Perl.langdef");
					break;
				case "PHP":
					this.LoadLanguageDefinitionFromFile("Php.langdef");
					break;
				case "PowerShell":
					this.LoadLanguageDefinitionFromFile("PowerShell.langdef");
					break;
				case "Python":
					this.LoadLanguageDefinitionFromFile("Python.langdef");
					break;
				case "Python (in Python Language Add-on)":
					// Python Language Add-on Python language
					this.LoadLanguage(new ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage());
					break;
				case "RTF":
					this.LoadLanguageDefinitionFromFile("Rtf.langdef");
					break;
				case "Ruby":
					this.LoadLanguageDefinitionFromFile("Ruby.langdef");
					break;
				case "SQL":
					this.LoadLanguageDefinitionFromFile("Sql.langdef");
					break;
				case "VB":
					this.LoadLanguageDefinitionFromFile("VB.langdef");
					editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "'" });
					break;
				case "VB (in .NET Languages Add-on)": {
					// .NET Languages Add-on VB language
					var language = new ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage();
					language.RegisterService<ActiproSoftware.Text.Languages.DotNet.Reflection.IProjectAssembly>(vbProjectAssembly);
					this.LoadLanguage(language);

					// Register a code snippet provider that has several snippets available
					ICodeSnippetFolder snippetFolder = SyntaxEditorHelper.LoadSampleVBCodeSnippetsFromResources();
					editor.Document.Language.RegisterService(new ActiproSoftware.Text.Languages.VB.Implementation.VBCodeSnippetProvider() { RootFolder = snippetFolder });
					break;
				}
				case "VBScript":
					this.LoadLanguageDefinitionFromFile("VBScript.langdef");
					break;
				case "XAML":
					this.LoadLanguageDefinitionFromFile("Xaml.langdef");
					editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "<!--", EndDelimiter = "-->" });
					break;
				case "XML":
					this.LoadLanguageDefinitionFromFile("Xml.langdef");
					editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "<!--", EndDelimiter = "-->" });
					break;
				case "XML (in Web Languages Add-on)":
					// Web Languages Add-on XML language
					this.LoadLanguage(new ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage());
					break;
				default:
					// Plain text
					this.LoadLanguage(SyntaxLanguage.PlainText);
					break;
			}
		}
		
		/// <summary>
		/// Loads a language definition from a file.
		/// </summary>
		/// <param name="filename">The filename.</param>
		private void LoadLanguageDefinitionFromFile(string filename) {
			if (String.IsNullOrEmpty(filename)) {
				// Show a file open dialog
				OpenFileDialog dialog = new OpenFileDialog();
				if (!BrowserInteropHelper.IsBrowserHosted)
					dialog.CheckFileExists = true;
				dialog.Multiselect = false;
				dialog.Filter = "Language definition files (*.langdef)|*.langdef|All files (*.*)|*.*";
				if (dialog.ShowDialog() != true)
					return;

				// Open a language definition
				using (Stream stream = dialog.OpenFile()) {
					// Read the file
					SyntaxLanguageDefinitionSerializer serializer = new SyntaxLanguageDefinitionSerializer();
					this.LoadLanguage(serializer.LoadFromStream(stream));
				}
			}
			else {
				// Load an embedded resource .langdef file
				this.LoadLanguage(SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream(filename));
			}
		}
		
		/// <summary>
		/// Occurs when a mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
			ListBox listBox = (ListBox)sender;
			IParseError error = listBox.SelectedItem as IParseError;
			if (error != null) {
				editor.ActiveView.Selection.StartPosition = error.PositionRange.StartPosition;
				editor.Focus();
			}
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnFileExitMenuItemClick(object sender, RoutedEventArgs e) {
			// Show a message
			MessageBox.Show("Close the application here.");
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnFileImportVSSettingsMenuItemClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Visual Studio Settings files (*.vssettings)|*.vssettings|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				using (Stream stream = dialog.OpenFile()) {
					// Read the file
					ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry.Instance.ImportHighlightingStyles(stream);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnInsertLoremIpsumMenuItemClick(object sender, RoutedEventArgs e) {
			editor.ActiveView.SelectedText = new ActiproSoftware.Text.Utility.LipsumGenerator().GenerateParagraph(true, 30);
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLanguageMenuItemClick(object sender, RoutedEventArgs e) {
			MenuItem item = (MenuItem)sender;
			this.LoadLanguage(item.Header.ToString());
		}

		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate(object arg) {
				editor.Focus();
				return null;
			}, null);
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnNewExecuted(object sender, ExecutedRoutedEventArgs e) {
			MainControl control = (MainControl)sender;

			// Create a new document 
			control.editor.Document.SetText(String.Empty);
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnOpenExecuted(object sender, ExecutedRoutedEventArgs e) {
			MainControl control = (MainControl)sender;

			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Code files (*.cs;*.vb;*.js;*.json;*.py;*.xml;*.txt)|*.cs;*.vb;*.js;*.json;*.py;*.xml;*.txt|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Open a document 
				if (BrowserInteropHelper.IsBrowserHosted) {
					// Use dialog to help open the file because of security restrictions
					using (Stream stream = dialog.OpenFile()) {
						// Read the file
						control.editor.Document.LoadFile(stream, Encoding.UTF8);
					}
				}
				else {
					// Security is not an issue in a Windows app so use simple method
					control.editor.Document.LoadFile(dialog.FileName);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnEmailDocumentMenuItemClick(object sender, RoutedEventArgs e) {
			MessageBox.Show("This is an example of how to wire up extended functionality for a SyntaxEditor from custom buttons inside of it.  In this example you'd e-mail the document text here.", "E-mail Document", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnPostToBlogMenuItemClick(object sender, RoutedEventArgs e) {
			MessageBox.Show("This is an example of how to wire up extended functionality for a SyntaxEditor from custom buttons inside of it.  In this example you'd post the document text to a blog here.", "Post Document to Blog", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnPrintExecuted(object sender, ExecutedRoutedEventArgs e) {
			MainControl control = (MainControl)sender;

			// Show a print dialog
			control.editor.ShowPrintDialog();
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private static void OnPrintPreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = !BrowserInteropHelper.IsBrowserHosted;
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnPrintPreviewExecuted(object sender, ExecutedRoutedEventArgs e) {
			MainControl control = (MainControl)sender;

			// Show a print preview dialog
			control.editor.ShowPrintPreviewDialog();
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private static void OnSaveCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			MainControl control = (MainControl)sender;
			e.CanExecute = control.editor.Document.IsModified;
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnSaveExecuted(object sender, ExecutedRoutedEventArgs e) {
			MainControl control = (MainControl)sender;

			// NOTE: Save the document here
			MessageBox.Show("Save the document here.");

			// Flag as not modified
			control.editor.Document.IsModified = false;
		}
		
		/// <summary>
		/// Occurs when the <c>SyntaxEditor.DocumentChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorDocumentChangedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorDocumentChanged(object sender, EditorDocumentChangedEventArgs e) {
			this.AppendMessage("DocumentChanged");
		}
		
		/// <summary>
		/// Occurs when the <c>SyntaxEditor.DocumentIsModifiedChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorDocumentIsModifiedChanged(object sender, RoutedEventArgs e) {
			this.AppendMessage(String.Format("DocumentIsModifiedChanged: IsModified={0}", editor.Document.IsModified));
		}
		
		/// <summary>
		/// Occurs when the document's parse data has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to this event.</param>
		private void OnSyntaxEditorDocumentParseDataChanged(object sender, EventArgs e) {
			//
			// NOTE: The parse data here is generated in a worker thread... this event handler is called 
			//         back in the UI thread immediately when the worker thread completes... it is best
			//         practice to delay UI updates until the end user stops typing... we will flag that
			//         there is a pending parse data change, which will be handled in the 
			//         UserInterfaceUpdate event
			//

			hasPendingParseData = true;
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.IsOverwriteModeActiveChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BooleanPropertyChangedRoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorIsOverwriteModeActiveChanged(object sender, RoutedEventArgs e) {
			this.AppendMessage("IsOverwriteModeActiveChanged");
			overwriteModePanel.Content = (editor.IsOverwriteModeActive ? "OVR" : "INS");
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.MacroRecordingStateChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorMacroRecordingStateChanged(object sender, RoutedEventArgs e) {
			this.AppendMessage("MacroRecordingStateChanged: " + editor.MacroRecording.State);

			switch (editor.MacroRecording.State) {
				case MacroRecordingState.Recording:
					messagePanel.Content = "Macro recording is active";
					recordMacroMenuItem.SetBinding(MenuItem.IconProperty, new Binding() { Source = "MacroRecordingStop16.png", Converter = this.TryFindResource("ImageConverter") as IValueConverter });
					recordMacroButton.SetBinding(Button.ContentProperty, new Binding() { Source = "MacroRecordingStop16.png", Converter = this.TryFindResource("ImageConverter") as IValueConverter });
					recordMacroButton.ToolTip = "Stop Recording";
					pauseRecordingButton.IsChecked = false;
					pauseRecordingButton.ToolTip = "Pause Recording";
					break;
				case MacroRecordingState.Paused:
					messagePanel.Content = "Macro recording is paused";
					pauseRecordingButton.IsChecked = true;
					pauseRecordingButton.ToolTip = "Resume Recording";
					break;
				default:
					messagePanel.Content = "Ready";
					recordMacroMenuItem.SetBinding(MenuItem.IconProperty, new Binding() { Source = "MacroRecordingRecord16.png", Converter = this.TryFindResource("ImageConverter") as IValueConverter });
					recordMacroButton.SetBinding(Button.ContentProperty, new Binding() { Source = "MacroRecordingRecord16.png", Converter = this.TryFindResource("ImageConverter") as IValueConverter });
					recordMacroButton.ToolTip = "Record Macro";
					pauseRecordingButton.IsChecked = false;
					pauseRecordingButton.ToolTip = "Pause Recording";
					break;
			}

			recordMacroMenuItem.Header = recordMacroButton.ToolTip;
			pauseRecordingMenuItem.IsChecked = pauseRecordingButton.IsChecked.Value;
			pauseRecordingMenuItem.Header = pauseRecordingButton.ToolTip;
		}
		
		/// <summary>
		/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnSyntaxEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
			// If there is a pending parse data change...
			if (hasPendingParseData) {
				// Clear flag
				hasPendingParseData = false;

				ILLParseData parseData = editor.Document.ParseData as ILLParseData;
				if (parseData != null) {
					if (editor.Document.CurrentSnapshot.Length < 10000) {
						// Show the AST
						if (parseData.Ast != null)
							astOutputEditor.Document.SetText(parseData.Ast.ToTreeString(0));
						else
							astOutputEditor.Document.SetText(null);
					}
					else
						astOutputEditor.Document.SetText("(Not displaying large AST for performance reasons)");

					// Output errors
					errorListView.ItemsSource = parseData.Errors;
				}
				else {
					// Clear UI
					astOutputEditor.Document.SetText("(Language may not have AST building features)");
					errorListView.ItemsSource = null;
				}
			}
		}
		
		/// <summary>
		/// Occurs when the incremental search mode of an <see cref="ITextView"/> is activated or deactivated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewIsIncrementalSearchActiveChanged(object sender, TextViewEventArgs e) {
			IEditorView editorView = e.View as IEditorView;
			if ((editorView != null) && (!editorView.IsIncrementalSearchActive)) {
				// Incremental search is now deactivated
				messagePanel.Content = "Ready";
			}
		}
		
		/// <summary>
		/// Occurs when a search operation occurs in a view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorViewSearchEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSearch(object sender, EditorViewSearchEventArgs e) {
			// If an incremental search was performed...
			if (e.ResultSet.OperationType == SearchOperationType.FindNextIncremental) {
				// Show a statusbar message
				bool hasFindText = !String.IsNullOrEmpty(e.ResultSet.Options.FindText);
				bool notFound = (hasFindText) && (e.ResultSet.Results.Count == 0);
				string notFoundMessage = (notFound ? " (not found)" : String.Empty);
				messagePanel.Content = "Incremental Search: " + e.ResultSet.Options.FindText + notFoundMessage;
			}
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.ViewSelectionChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			// The line, col, and character display are updated using XAML bindings in the view, but the
			// following could also be used to programatically update the status of the caret position:
			Debug.WriteLineIf(false, string.Format("Ln {0}  Col {1}  Ch {2}",
				e.CaretPosition.DisplayLine,
				e.CaretDisplayCharacterColumn,
				e.CaretPosition.DisplayCharacter));

			// If token info should be displayed in the statusbar...
			if (toggleTokenInfoMenuItem.IsChecked) {
				// Get a snapshot reader
				ITextSnapshotReader reader = e.View.CurrentSnapshot.GetReader(e.View.Selection.EndOffset);
				IToken token = reader.Token;
				if (token != null) {
					IMergableToken mergableToken = token as IMergableToken;
					if (mergableToken != null)
						tokenPanel.Content = String.Format("{0} / {1} / {2}{3}", 
							mergableToken.Lexer.Key, mergableToken.LexicalState.Key,
							token.Key, (e.View.Selection.EndOffset == token.StartOffset ? "*" : String.Empty));
					else
						tokenPanel.Content = String.Format("{0} / {1}{2}", e.View.SyntaxEditor.Document.Language.Key,
							token.Key, (e.View.Selection.EndOffset == token.StartOffset ? "*" : String.Empty));
					return;
				}
			}
			tokenPanel.Content = null;
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.ViewSplitAdded</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSplitAdded(object sender, RoutedEventArgs e) {
			this.AppendMessage("ViewSplitAdded");
		}
		
		/// <summary>
		/// Occurs when the <c>SyntaxEditor.ViewSplitMoved</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSplitMoved(object sender, RoutedEventArgs e) {
			this.AppendMessage("ViewSplitMoved");
		}
		
		/// <summary>
		/// Occurs when the <c>SyntaxEditor.ViewSplitRemoved</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSplitRemoved(object sender, RoutedEventArgs e) {
			this.AppendMessage("ViewSplitRemoved");
		}
		
		/// <summary>
		/// Occurs when a <c>MenuItem</c> is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleTokenInfoMenuItemClick(object sender, RoutedEventArgs e) {
			// Force a new selection event
			using (editor.ActiveView.Selection.CreateBatch(EditorViewSelectionBatchOptions.ForceSelectionChangedEvent)) {}
		}
		
		/// <summary>
		/// Occurs when <c>MenuItem</c> is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWordWrapModeMenuItemClick(object sender, RoutedEventArgs e) {
			editor.WordWrapMode = (editor.WordWrapMode == WordWrapMode.Word ? WordWrapMode.None : WordWrapMode.Word);
			wordWrapMenuItem.IsChecked = (editor.WordWrapMode == WordWrapMode.Word);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			// Clear .NET Languages Add-on project assembly references when the sample unloads
			cSharpProjectAssembly.AssemblyReferences.Clear();
			vbProjectAssembly.AssemblyReferences.Clear();
		}
		
		/// <summary>
		/// Invoked when an unhandled <c>KeyDown</c> attached event is raised on this element.
		/// </summary>
		/// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
		protected override void OnKeyDown(KeyEventArgs e) {
			// Call the base method
			base.OnKeyDown(e);

			if (!e.Handled) {
				switch (e.Key) {
					case Key.S:
						if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift)) {
							// Screenshot mode
							editor.Width = 600;
							if (editor.Height == 300) {
								editor.Height = double.NaN;
								editor.Margin = new Thickness(0, 10, 0, 10);
							}
							else {
								editor.Height = 300;
								editor.Margin = new Thickness(0);
							}
						}
						break;
				}
			}
		}

	}

}