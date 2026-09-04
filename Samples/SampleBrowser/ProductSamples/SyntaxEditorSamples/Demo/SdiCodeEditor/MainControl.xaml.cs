using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using Microsoft.Win32;
using System.Windows.Threading;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.SdiCodeEditor;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private bool _hasPendingParseData;

	// Project assemblies used by C#/VB in the .NET Languages Add-on
	private readonly Text.Languages.CSharp.Implementation.CSharpProjectAssembly _cSharpProjectAssembly;
	private readonly Text.Languages.VB.Implementation.VBProjectAssembly _vbProjectAssembly;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Initialize the project assemblies for .NET Languages Add-on languages
		_cSharpProjectAssembly = new Text.Languages.CSharp.Implementation.CSharpProjectAssembly("SampleBrowser");
		_vbProjectAssembly = new Text.Languages.VB.Implementation.VBProjectAssembly("SampleBrowser");
		var assemblyLoader = new BackgroundWorker();
		assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
		assemblyLoader.RunWorkerAsync();

		// Load the .NET Languages Add-on C# language (sold separately) by default
		LoadLanguage("C# (in .NET Languages Add-on)");

		// Register display item classification types
		new BuiltInClassificationTypeProvider().RegisterAll();

		// Register class command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.New, OnNewExecuted));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecuted));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, OnPrintExecuted));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.PrintPreview, OnPrintPreviewExecuted));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSaveExecuted, OnSaveCanExecute));

		Loaded += new RoutedEventHandler(OnLoaded);
	}

	private void DotNetProjectAssemblyReferenceLoader(object? sender, DoWorkEventArgs e) {
		// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)

		_cSharpProjectAssembly.AssemblyReferences.AddMsCorLib();
		SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(_cSharpProjectAssembly);

		_vbProjectAssembly.AssemblyReferences.AddMsCorLib();
		SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(_vbProjectAssembly);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends a message to the events <see cref="ListBox"/>.
	/// </summary>
	/// <param name="text">The text to append.</param>
	private void AppendMessage(string text) {
		var item = new ListBoxItem {
			Content = text
		};
		eventsListBox.Items.Add(item);
		eventsListBox.SelectedItem = item;
		eventsListBox.ScrollIntoView(item);
	}

	/// <summary>
	/// Loads a language, with example text.
	/// </summary>
	/// <param name="language">The <see cref="ISyntaxLanguage"/> to load.</param>
	private void LoadLanguage(ISyntaxLanguage language) {
		if (language is null)
			return;

		// Load the language
		editor.Document.Language = language;

		// Get example text
		var exampleTextProvider = editor.Document.Language.GetExampleTextProvider();
		if (exampleTextProvider?.ExampleText is { } exampleText)
			editor.Document.SetText(exampleText);

		// Update symbol selector visibility
		symbolSelector.Visibility = (language.GetNavigableSymbolProvider() is not null ? Visibility.Visible : Visibility.Collapsed);
		symbolSelector.AreMemberSymbolsSupported = (language.Key != "Python");
	}

	/// <summary>
	/// Loads a language definition.
	/// </summary>
	/// <param name="languageKey">The key that identifies the language.</param>
	private void LoadLanguage(string? languageKey) {
		// Clear errors and document outline
		errorListView.ItemsSource = null;
		astOutputEditor.Document.SetText("(Language may not have AST building features)");

		switch (languageKey) {
			case "Assembly":
				LoadLanguageDefinitionFromFile("Assembly.langdef");
				break;
			case "Batch file":
				LoadLanguageDefinitionFromFile("BatchFile.langdef");
				break;
			case "C":
				LoadLanguageDefinitionFromFile("C.langdef");
				break;
			case "C#":
				LoadLanguageDefinitionFromFile("CSharp.langdef");
				editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
				break;
			case "C# (in .NET Languages Add-on)": {
				// .NET Languages Add-on C# language
				var language = new Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage();
				language.RegisterService<Text.Languages.DotNet.Reflection.IProjectAssembly>(_cSharpProjectAssembly);
				LoadLanguage(language);

				// Register a code snippet provider that has several snippets available
				var snippetFolder = SyntaxEditorHelper.LoadSampleCSharpCodeSnippetsFromResources();
				editor.Document.Language.RegisterService(new Text.Languages.CSharp.Implementation.CSharpCodeSnippetProvider() { RootFolder = snippetFolder });
				break;
			}
			case "C++":
				LoadLanguageDefinitionFromFile("Cpp.langdef");
				break;
			case "CSS":
				LoadLanguageDefinitionFromFile("Css.langdef");
				break;
			case "Custom...":
				LoadLanguageDefinitionFromFile(null);
				break;
			case "HTML":
				LoadLanguageDefinitionFromFile("Html.langdef");
				editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "<!--", EndDelimiter = "-->" });
				break;
			case "INI file":
				LoadLanguageDefinitionFromFile("IniFile.langdef");
				break;
			case "Java":
				LoadLanguageDefinitionFromFile("Java.langdef");
				editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
				break;
			case "JavaScript":
				LoadLanguageDefinitionFromFile("JavaScript.langdef");
				editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
				break;
			case "JavaScript (in Web Languages Add-on)":
				// Web Languages Add-on JavaScript language
				LoadLanguage(new Text.Languages.JavaScript.Implementation.JavaScriptSyntaxLanguage());
				break;
			case "JSON (in Web Languages Add-on)":
				// Web Languages Add-on JSON language
				LoadLanguage(new Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage());
				break;
			case "JSON with Comments (in Web Languages Add-on)":
				// Web Languages Add-on JSON language
				LoadLanguage(new Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage(areCommentsSupported: true));
				break;
			case "Lua":
				LoadLanguageDefinitionFromFile("Lua.langdef");
				break;
			case "Markdown":
				LoadLanguageDefinitionFromFile("Markdown.langdef");
				break;
			case "MSIL":
				LoadLanguageDefinitionFromFile("Msil.langdef");
				break;
			case "Pascal":
				LoadLanguageDefinitionFromFile("Pascal.langdef");
				break;
			case "Perl":
				LoadLanguageDefinitionFromFile("Perl.langdef");
				break;
			case "PHP":
				LoadLanguageDefinitionFromFile("Php.langdef");
				break;
			case "PowerShell":
				LoadLanguageDefinitionFromFile("PowerShell.langdef");
				break;
			case "Python":
				LoadLanguageDefinitionFromFile("Python.langdef");
				break;
			case "Python (in Python Language Add-on)":
				// Python Language Add-on Python language
				LoadLanguage(new Text.Languages.Python.Implementation.PythonSyntaxLanguage());
				break;
			case "RTF":
				LoadLanguageDefinitionFromFile("Rtf.langdef");
				break;
			case "Ruby":
				LoadLanguageDefinitionFromFile("Ruby.langdef");
				break;
			case "SQL":
				LoadLanguageDefinitionFromFile("Sql.langdef");
				break;
			case "VB":
				LoadLanguageDefinitionFromFile("VB.langdef");
				editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "'" });
				break;
			case "VB (in .NET Languages Add-on)": {
				// .NET Languages Add-on VB language
				var language = new Text.Languages.VB.Implementation.VBSyntaxLanguage();
				language.RegisterService<Text.Languages.DotNet.Reflection.IProjectAssembly>(_vbProjectAssembly);
				LoadLanguage(language);

				// Register a code snippet provider that has several snippets available
				var snippetFolder = SyntaxEditorHelper.LoadSampleVBCodeSnippetsFromResources();
				editor.Document.Language.RegisterService(new Text.Languages.VB.Implementation.VBCodeSnippetProvider() { RootFolder = snippetFolder });
				break;
			}
			case "VBScript":
				LoadLanguageDefinitionFromFile("VBScript.langdef");
				break;
			case "XAML":
				LoadLanguageDefinitionFromFile("Xaml.langdef");
				editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "<!--", EndDelimiter = "-->" });
				break;
			case "XML":
				LoadLanguageDefinitionFromFile("Xml.langdef");
				editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "<!--", EndDelimiter = "-->" });
				break;
			case "XML (in Web Languages Add-on)":
				// Web Languages Add-on XML language
				LoadLanguage(new Text.Languages.Xml.Implementation.XmlSyntaxLanguage());
				break;
			default:
				// Plain text
				LoadLanguage(SyntaxLanguage.PlainText);
				break;
		}
	}

	/// <summary>
	/// Loads a language definition from a file.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	private void LoadLanguageDefinitionFromFile(string? fileName) {
		if (string.IsNullOrEmpty(fileName)) {
			// Show a file open dialog
			var dialog = new OpenFileDialog {
				CheckFileExists = true,
				Multiselect = false,
				Filter = "Language definition files (*.langdef)|*.langdef|All files (*.*)|*.*"
			};
			if (dialog.ShowDialog() != true)
				return;

			// Open a language definition
			using (var stream = dialog.OpenFile()) {
				// Read the file
				var serializer = new SyntaxLanguageDefinitionSerializer() {
					// Enable the use of common classification types (like Comment and String)
					//   for consistent highlighting styles
					UseBuiltInClassificationTypes = true,
				};
				LoadLanguage(serializer.LoadFromStream(stream));
			}
		}
		else {
			// Load an embedded resource .langdef file
			LoadLanguage(SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream(fileName!));
		}
	}

	private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IParseError error }) {
			if (error.PositionRange.HasValue)
				editor.ActiveView.Selection.StartPosition = error.PositionRange.Value.StartPosition;

			editor.Focus();
		}
	}

	private void OnFileExitMenuItemClick(object sender, RoutedEventArgs e) {
		// Show a message
		MessageBox.Show("Close the application here.");
	}

	private void OnFileImportVSSettingsMenuItemClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "Visual Studio Settings files (*.vssettings)|*.vssettings|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			using (var stream = dialog.OpenFile()) {
				// Read the file
				Windows.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry.Instance.ImportHighlightingStyles(stream);
			}
		}
	}

	private void OnInsertLoremIpsumMenuItemClick(object sender, RoutedEventArgs e)
		=> editor.ActiveView.SelectedText = new Text.Utility.LipsumGenerator().GenerateParagraph(useStartingWords: true, wordCount: 30);

	private void OnLanguageMenuItemClick(object sender, RoutedEventArgs e) {
		var item = (MenuItem)sender;
		LoadLanguage(item.Header.ToString());
	}

	private void OnLoaded(object sender, RoutedEventArgs e) {
		Dispatcher.BeginInvoke(DispatcherPriority.Send, () => {
			editor.Focus();
		});
	}

	private static void OnNewExecuted(object sender, ExecutedRoutedEventArgs e) {
		var control = (MainControl)sender;

		// Create a new document
		control.editor.Document.SetText(null);
	}

	private static void OnOpenExecuted(object sender, ExecutedRoutedEventArgs e) {
		var control = (MainControl)sender;

		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "Code files (*.cs;*.vb;*.js;*.json;*.py;*.xml;*.txt)|*.cs;*.vb;*.js;*.json;*.py;*.xml;*.txt|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Open a document
			control.editor.Document.LoadFile(dialog.FileName);
		}
	}

	private void OnEmailDocumentMenuItemClick(object sender, RoutedEventArgs e)
		=> MessageBox.Show("This is an example of how to wire up extended functionality for a SyntaxEditor from custom buttons inside of it.  In this example you'd e-mail the document text here.", "E-mail Document", MessageBoxButton.OK, MessageBoxImage.Information);

	private void OnPostToBlogMenuItemClick(object sender, RoutedEventArgs e)
		=> MessageBox.Show("This is an example of how to wire up extended functionality for a SyntaxEditor from custom buttons inside of it.  In this example you'd post the document text to a blog here.", "Post Document to Blog", MessageBoxButton.OK, MessageBoxImage.Information);

	private static void OnPrintExecuted(object sender, ExecutedRoutedEventArgs e) {
		var control = (MainControl)sender;

		// Show a print dialog
		control.editor.ShowPrintDialog();
	}

	private static void OnPrintPreviewExecuted(object sender, ExecutedRoutedEventArgs e) {
		var control = (MainControl)sender;

		// Show a print preview dialog
		control.editor.ShowPrintPreviewDialog();
	}

	private static void OnSaveCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var control = (MainControl)sender;
		e.CanExecute = control.editor.Document.IsModified;
	}

	private static void OnSaveExecuted(object sender, ExecutedRoutedEventArgs e) {
		var control = (MainControl)sender;

		// NOTE: Save the document here
		MessageBox.Show("Save the document here.");

		// Flag as not modified
		control.editor.Document.IsModified = false;
	}

	private void OnSyntaxEditorDocumentChanged(object sender, EditorDocumentChangedEventArgs e)
		=> AppendMessage("DocumentChanged");

	private void OnSyntaxEditorDocumentIsModifiedChanged(object sender, RoutedEventArgs e)
		=> AppendMessage(string.Format("DocumentIsModifiedChanged: IsModified={0}", editor.Document.IsModified));

	private void OnSyntaxEditorDocumentParseDataChanged(object sender, EventArgs e) {
		//
		// NOTE: The parse data here is generated in a worker thread... this event handler is called
		//   back in the UI thread immediately when the worker thread completes... it is best
		//   practice to delay UI updates until the end user stops typing... we will flag that
		//   there is a pending parse data change, which will be handled in the
		//   UserInterfaceUpdate event
		//

		_hasPendingParseData = true;
	}

	private void OnSyntaxEditorIsOverwriteModeActiveChanged(object sender, RoutedEventArgs e) {
		AppendMessage("IsOverwriteModeActiveChanged");
		overwriteModePanel.Content = (editor.IsOverwriteModeActive ? "OVR" : "INS");
	}

	private void OnSyntaxEditorMacroRecordingStateChanged(object sender, RoutedEventArgs e) {
		AppendMessage("MacroRecordingStateChanged: " + editor.MacroRecording.State);

		switch (editor.MacroRecording.State) {
			case MacroRecordingState.Recording:
				messagePanel.Content = "Macro recording is active";
				recordMacroMenuItem.SetBinding(MenuItem.IconProperty, new Binding() { Source = "MacroRecordingStop16.png", Converter = TryFindResource("ImageConverter") as IValueConverter });
				recordMacroButton.SetBinding(Button.ContentProperty, new Binding() { Source = "MacroRecordingStop16.png", Converter = TryFindResource("ImageConverter") as IValueConverter });
				recordMacroButton.ToolTip = "Stop Recording";
				pauseRecordingButton.IsChecked = false;
				pauseRecordingButton.ToolTip = "Pause Recording";
				break;
			case MacroRecordingState.Paused:
				messagePanel.Content = "Macro recording is paused";
				pauseRecordingButton.IsChecked = true;
				pauseRecordingButton.ToolTip = "Resume Recording";
				break;
			case MacroRecordingState.Stopped:
			default:
				messagePanel.Content = "Ready";
				recordMacroMenuItem.SetBinding(MenuItem.IconProperty, new Binding() { Source = "MacroRecordingRecord16.png", Converter = TryFindResource("ImageConverter") as IValueConverter });
				recordMacroButton.SetBinding(Button.ContentProperty, new Binding() { Source = "MacroRecordingRecord16.png", Converter = TryFindResource("ImageConverter") as IValueConverter });
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
	private void OnSyntaxEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
		// If there is a pending parse data change...
		if (_hasPendingParseData) {
			// Clear flag
			_hasPendingParseData = false;

			var parseData = editor.Document.ParseData as ILLParseData;
			if (parseData is not null) {
				if (editor.Document.CurrentSnapshot.Length < 10000) {
					// Show the AST
					astOutputEditor.Document.SetText(parseData.Ast?.ToTreeString(0));
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
	private void OnSyntaxEditorViewIsIncrementalSearchActiveChanged(object sender, TextViewEventArgs e) {
		if (e.View is IEditorView { IsIncrementalSearchActive: false }) {
			// Incremental search is now deactivated
			messagePanel.Content = "Ready";
		}
	}

	/// <summary>
	/// Occurs when a search operation occurs in a view.
	/// </summary>
	private void OnSyntaxEditorViewSearch(object sender, EditorViewSearchEventArgs e) {
		// If an incremental search was performed...
		if (e.ResultSet.OperationType == SearchOperationType.FindNextIncremental) {
			// Show a statusbar message
			var hasFindText = !string.IsNullOrEmpty(e.ResultSet.Options.FindText);
			var notFound = hasFindText && (e.ResultSet.Results.Count == 0);
			string notFoundMessage = (notFound ? " (not found)" : string.Empty);
			messagePanel.Content = "Incremental Search: " + e.ResultSet.Options.FindText + notFoundMessage;
		}
	}

	private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		// The line, col, and character display are updated using XAML bindings in the view, but the
		//   following could also be used to programmatically update the status of the caret position:
		Debug.WriteLineIf(false, string.Format("Ln {0}  Col {1}  Ch {2}",
			e.CaretPosition.DisplayLine,
			e.CaretDisplayCharacterColumn,
			e.CaretPosition.DisplayCharacter));

		// If token info should be displayed in the statusbar...
		if (toggleTokenInfoMenuItem.IsChecked) {
			// Get a snapshot reader
			var reader = e.View.CurrentSnapshot.GetReader(e.View.Selection.EndOffset);
			var token = reader.Token;
			if (token is not null) {
				if (token is IMergeableToken mergeableToken) {
					tokenPanel.Content = string.Format("{0} / {1} / {2}{3}",
						mergeableToken.Lexer?.Key,
						mergeableToken.LexicalState?.Key,
						token.Key,
						e.View.Selection.EndOffset == token.StartOffset ? "*" : string.Empty
					);
				}
				else {
					tokenPanel.Content = string.Format("{0} / {1}{2}",
						e.View.SyntaxEditor.Document.Language.Key,
						token.Key,
						e.View.Selection.EndOffset == token.StartOffset ? "*" : string.Empty
					);
				}
				return;
			}
		}
		tokenPanel.Content = null;
	}

	private void OnSyntaxEditorViewSplitAdded(object sender, RoutedEventArgs e)
		=> AppendMessage("ViewSplitAdded");

	private void OnSyntaxEditorViewSplitMoved(object sender, RoutedEventArgs e)
		=> AppendMessage("ViewSplitMoved");

	private void OnSyntaxEditorViewSplitRemoved(object sender, RoutedEventArgs e)
		=> AppendMessage("ViewSplitRemoved");

	private void OnToggleTokenInfoMenuItemClick(object sender, RoutedEventArgs e) {
		// Force a new selection event
		using (editor.ActiveView.Selection.CreateBatch(EditorViewSelectionBatchOptions.ForceSelectionChangedEvent)) { }
	}

	private void OnWordWrapModeMenuItemClick(object sender, RoutedEventArgs e) {
		editor.WordWrapMode = (editor.WordWrapMode == WordWrapMode.Word ? WordWrapMode.None : WordWrapMode.Word);
		wordWrapMenuItem.IsChecked = (editor.WordWrapMode == WordWrapMode.Word);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		// Clear .NET Languages Add-on project assembly references when the sample unloads
		_cSharpProjectAssembly.AssemblyReferences.Clear();
		_vbProjectAssembly.AssemblyReferences.Clear();
	}

	/// <inheritdoc/>
	protected override void OnKeyDown(KeyEventArgs e) {
		base.OnKeyDown(e);

		if (!e.Handled) {
			switch (e.Key) {
				case Key.S:
					//
					// NOTE: The "Ctrl+Alt+Shift+S" gesture is a special command used internally by Actipro to size
					//   a window for taking screenshots and is unrelated to the demonstration.
					//
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
