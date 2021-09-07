using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Win32;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.DotNetAddonCSharpEditor {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private int					documentNumber;
		private bool				hasPendingParseData;

		// A project assembly (similar to a Visual Studio project) contains source files and assembly references for reflection
		private IProjectAssembly projectAssembly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			
			//
			// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
			//   since it tells you how to set up an ambient parse request dispatcher and an ambient
			//   code repository within your application OnStartup code, and add related cleanup in your
			//   application OnExit code.  These steps are essential to having the add-on perform well.
			//
			
			// Initialize the project assembly (enables support for automated IntelliPrompt features)
			projectAssembly = new CSharpProjectAssembly("SampleBrowser");
			projectAssembly.AssemblyReferences.ItemAdded += OnAssemblyReferencesChanged;
			projectAssembly.AssemblyReferences.ItemRemoved += OnAssemblyReferencesChanged;
			var assemblyLoader = new BackgroundWorker();
			assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
			assemblyLoader.RunWorkerAsync();

			// Load the .NET Languages Add-on C# language and register the project assembly on it
			var language = new CSharpSyntaxLanguage();
			language.RegisterProjectAssembly(projectAssembly);
			codeEditor.Document.Language = language;
		}
		
		private void DotNetProjectAssemblyReferenceLoader(object sender, DoWorkEventArgs e) {
			// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
			SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(projectAssembly);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new XML file.
		/// </summary>
		private void NewFile() {
			this.OpenFile(String.Format("Document{0}.cs", ++documentNumber), null);
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnAddReferenceButtonClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Assemblies (*.dll)|*.dll|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				try {
					// Add to references
					projectAssembly.AssemblyReferences.AddFrom(dialog.FileName);
				}
				catch (Exception ex) {
					MessageBox.Show("An exception occurred: " + ex.Message, "Error Loading Assembly", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
		}

		/// <summary>
		/// Occurs when the assembly references have changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>CollectionChangeEventArgs</c> that contains data related to this event.</param>
		private void OnAssemblyReferencesChanged(object sender, Text.Utility.CollectionChangeEventArgs<IProjectAssemblyReference> e) {
			this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate(object arg) {
				// Update list view items source on main dispatcher
				referencesListView.ItemsSource = null;
				referencesListView.ItemsSource = projectAssembly.AssemblyReferences;
				return null;
			}, null);		
		}
		
		/// <summary>
		/// Occurs when the document's parse data has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to this event.</param>
		private void OnCodeEditorDocumentParseDataChanged(object sender, EventArgs e) {
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
		/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnCodeEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
			// If there is a pending parse data change...
			if (hasPendingParseData) {
				// Clear flag
				hasPendingParseData = false;

				ILLParseData parseData = codeEditor.Document.ParseData as ILLParseData;
				if (parseData != null) {
					if (codeEditor.Document.CurrentSnapshot.Length < 10000) {
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
					astOutputEditor.Document.SetText(null);
					errorListView.ItemsSource = null;
				}
			}
		}

		/// <summary>
		/// Occurs when the document's view selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EditorViewSelectionEventArgs"/> that contains data related to this event.</param>
		private void OnCodeEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			// Update line, col, and character display
			linePanel.Text = String.Format("Ln {0}", e.CaretPosition.DisplayLine);
			columnPanel.Text = String.Format("Col {0}", e.CaretDisplayCharacterColumn);
			characterPanel.Text = String.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
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
				codeEditor.ActiveView.Selection.StartPosition = error.PositionRange.StartPosition;
				codeEditor.Focus();
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnNewFileButtonClick(object sender, RoutedEventArgs e) {
			this.NewFile();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenFileButtonClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			if (!BrowserInteropHelper.IsBrowserHosted)
				dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "C# files (*.cs)|*.cs|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Open a document (use dialog to help open the file because of security restrictions in XBAP/Silverlight)
				using (Stream stream = dialog.OpenFile()) {
					// Read the file
					this.OpenFile(Path.GetFileName(dialog.FileName), stream);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnRemoveReferenceButtonClick(object sender, RoutedEventArgs e) {
			if (referencesListView.SelectedIndex == -1) {
				MessageBox.Show("Select a reference first.", "No Reference Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			// Remove the selected reference
			projectAssembly.AssemblyReferences.RemoveAt(referencesListView.SelectedIndex);

			// Reset list
			referencesListView.ItemsSource = null;
			referencesListView.ItemsSource = projectAssembly.AssemblyReferences;
		}

		/// <summary>
		/// Opens a file.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="stream">The <see cref="Stream"/> to load.</param>
		private void OpenFile(string filename, Stream stream) {
			// Load the file
			if (stream != null)
				codeEditor.Document.LoadFile(stream, Encoding.UTF8);
			else
				codeEditor.Document.SetText(null);

			// Set the filename
			codeEditor.Document.FileName = filename;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			// Clear .NET Languages Add-on project assembly references when the sample unloads
			projectAssembly.AssemblyReferences.Clear();
		}
		
	}

}