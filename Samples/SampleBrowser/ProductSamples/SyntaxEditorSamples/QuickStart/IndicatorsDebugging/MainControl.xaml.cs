using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private TextSnapshotOffset currentStatementSnapshotOffset;
		
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

			// Register display item classification types (so breakpoint indicator styles are registered)
			new DisplayItemClassificationTypeProvider().RegisterAll();

			// Set the header/footer text to make the editor work as a method body
			editor.Document.SetHeaderAndFooterText("using System; using System.Diagnostics; using System.IO; private class Program { private void Execute() {\r\n", "\r\n}}");
			
			//
			// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
			//   since it tells you how to set up an ambient parse request dispatcher and an ambient
			//   code repository within your application OnStartup code, and add related cleanup in your
			//   application OnExit code.  These steps are essential to having the add-on perform well.
			//
			
			// Initialize the project assembly (enables support for automated IntelliPrompt features)
			projectAssembly = new CSharpProjectAssembly("SampleBrowser");
			var assemblyLoader = new BackgroundWorker();
			assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
			assemblyLoader.RunWorkerAsync();

			// Load the .NET Languages Add-on C# language and register the project assembly on it
			var language = new CSharpSyntaxLanguage();
			language.RegisterProjectAssembly(projectAssembly);

			// Register an indicator quick info provider
			language.RegisterService(new IndicatorQuickInfoProvider());

			// Register an event sink that allows for handling of clicks in the indicator margin
			language.RegisterService(new DebuggingPointerInputEventSink());

			// Register services to manage the elapsed time adornment
			language.RegisterService(new AdornmentManagerProvider<ElapsedTimeAdornmentManager>(typeof(ElapsedTimeAdornmentManager)));
			language.RegisterService(new CodeDocumentTaggerProvider<ElapsedTimeTagger>(typeof(ElapsedTimeTagger)));

			// Assign the language
			editor.Document.Language = language;

			this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (DispatcherOperationCallback)delegate(object arg) {
				// Since we are initializing some default breakpoints, make sure the document parse completes in the worker thread first
				AmbientParseRequestDispatcherProvider.Dispatcher.WaitForParse(ParseRequest.GetParseHashKey(editor.Document));

				// Add some indicators (this is dispatched since this sample relies on the document's AST being available and parsing occurs asynchronously)
				DebuggingHelper.ToggleBreakpoint(new TextSnapshotOffset(editor.ActiveView.CurrentSnapshot, editor.ActiveView.CurrentSnapshot.Lines[23].StartOffset), true);
				DebuggingHelper.ToggleBreakpoint(new TextSnapshotOffset(editor.ActiveView.CurrentSnapshot, editor.ActiveView.CurrentSnapshot.Lines[27].StartOffset), false);
				DebuggingHelper.ToggleBreakpoint(new TextSnapshotOffset(editor.ActiveView.CurrentSnapshot, editor.ActiveView.CurrentSnapshot.Lines[32].StartOffset), true);
				return null;
			}, null);
		}
		
		private void DotNetProjectAssemblyReferenceLoader(object sender, DoWorkEventArgs e) {
			// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
			SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(projectAssembly);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Hides the elapsed time adornment.
		/// </summary>
		private void HideElapsedTime() {
			// Get the tagger that was created by the language and has been persisted in the document's properties
			//   while the language is active on the document
			ElapsedTimeTagger tagger = null;
			if (editor.Document.Properties.TryGetValue(typeof(ElapsedTimeTagger), out tagger)) {
				// Clear any tags
				tagger.Clear();
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnClearBreakpointsButtonClick(object sender, RoutedEventArgs e) {
			// Clear breakpoints
			editor.Document.IndicatorManager.Breakpoints.Clear();

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnStartDebuggingButtonClick(object sender, RoutedEventArgs e) {
			// If starting debugging from a stopped state, begin looking at the document start
			if (currentStatementSnapshotOffset.IsDeleted)
				currentStatementSnapshotOffset = new TextSnapshotOffset(editor.ActiveView.CurrentSnapshot, 0);

			// Flag as debugging
			currentStatementSnapshotOffset = DebuggingHelper.SetCurrentStatement(editor.Document, currentStatementSnapshotOffset);
			stopDebuggingButton.IsEnabled = !currentStatementSnapshotOffset.IsDeleted;

			// Update the elapsed time
			if (currentStatementSnapshotOffset.IsDeleted)
				this.HideElapsedTime();
			else {
				// For this sample, generate a random timespan under a second long... a real debugging app would measure how long
				//   it takes to execute from one statement to the next statement
				var timeSpan = TimeSpan.FromMilliseconds(999.0 * new Random().NextDouble());
				
				this.ShowElapsedTime(currentStatementSnapshotOffset.Line, timeSpan);
			}

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnStopDebuggingButtonClick(object sender, RoutedEventArgs e) {
			// Flag as not debugging
			currentStatementSnapshotOffset = DebuggingHelper.SetCurrentStatement(editor.Document, TextSnapshotOffset.Deleted);
			stopDebuggingButton.IsEnabled = false;

			// Hide the elapsed time
			this.HideElapsedTime();

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleBreakpointButtonClick(object sender, RoutedEventArgs e) {
			// Toggle a breakpoint
			DebuggingHelper.ToggleBreakpoint(editor.ActiveView.Selection.EndSnapshotOffset, true);

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleBreakpointEnabledButtonClick(object sender, RoutedEventArgs e) {
			// Get the breakpoints at the caret and toggle their enabled states
			var tagRanges = editor.Document.IndicatorManager.Breakpoints.GetInstances(new TextSnapshotRange(editor.ActiveView.Selection.EndSnapshotOffset));
			var count = 0;
			foreach (var tagRange in tagRanges) {
				if (editor.Document.IndicatorManager.Breakpoints.ToggleEnabledState(tagRange.Tag))
					count++;
			}

			if (count == 0)
				MessageBox.Show("No breakpoints were found at the caret.", "Toggle Breakpoint Enabled State", MessageBoxButton.OK, MessageBoxImage.Exclamation);

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Shows an elapsed time at the end of the specified line.
		/// </summary>
		/// <param name="snapshotLine">The snapshot line.</param>
		/// <param name="timeSpan">The elapsed time.</param>
		private void ShowElapsedTime(ITextSnapshotLine snapshotLine, TimeSpan timeSpan) {
			// Hide any existing elapsed time
			this.HideElapsedTime();

			// Get the tagger that was created by the language and has been persisted in the document's properties
			//   while the language is active on the document
			ElapsedTimeTagger tagger = null;
			if (editor.Document.Properties.TryGetValue(typeof(ElapsedTimeTagger), out tagger)) {
				// Create a version range at the line's end offset
				var versionRange = new TextSnapshotRange(snapshotLine.Snapshot, snapshotLine.EndOffset).ToVersionRange(TextRangeTrackingModes.Default);
				
				// Create a tag that can render an adornment at the end of the line
				var tag = new ElapsedTimeTag(timeSpan);
				tag.Size = new Size(editor.ActiveView.DefaultCharacterWidth, 0.0);

				// Add the tag to the tagger
				tagger.Add(new TagVersionRange<IIntraTextSpacerTag>(versionRange, tag));
			}
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