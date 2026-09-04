using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeFragments;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// A project assembly (similar to a Visual Studio project) contains source files and assembly references for reflection
	private readonly CSharpProjectAssembly _projectAssembly;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Set the header and footer on the fragment editor's document
		fragmentEditor.Document.SetHeaderAndFooterText(headerEditor.Document.CurrentSnapshot.Text, footerEditor.Document.CurrentSnapshot.Text);

		//
		// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
		//   since it tells you how to set up an ambient parse request dispatcher and an ambient
		//   code repository within your application OnStartup code, and add related cleanup in your
		//   application OnExit code.  These steps are essential to having the add-on perform well.
		//

		// Initialize the project assembly (enables support for automated IntelliPrompt features)
		_projectAssembly = new CSharpProjectAssembly("SampleBrowser");
		var assemblyLoader = new BackgroundWorker();
		assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
		assemblyLoader.RunWorkerAsync();

		// Load the .NET Languages Add-on C# language and register the project assembly on it
		var language = new CSharpSyntaxLanguage();
		language.RegisterProjectAssembly(_projectAssembly);
		fragmentEditor.Document.Language = language;

		// Create a parser-less C# language for the header/footer editors
		var parserlessLanguage = new CSharpSyntaxLanguage();
		parserlessLanguage.UnregisterParser();
		headerEditor.Document.Language = parserlessLanguage;
		footerEditor.Document.Language = parserlessLanguage;
	}

	private void DotNetProjectAssemblyReferenceLoader(object? sender, DoWorkEventArgs e) {
		// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
		SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(_projectAssembly);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnToggleEditModeButtonClick(object sender, RoutedEventArgs e) {
		bool fragmentEditorActive = fragmentEditor.Document.IsReadOnly;

		headerEditor.Document.IsReadOnly = fragmentEditorActive;
		fragmentEditor.Document.IsReadOnly = !fragmentEditorActive;
		footerEditor.Document.IsReadOnly = fragmentEditorActive;

		if (fragmentEditorActive) {
			fragmentEditor.Document.SetHeaderAndFooterText(headerEditor.Document.CurrentSnapshot.Text, footerEditor.Document.CurrentSnapshot.Text);
			fragmentEditor.Focus();
		}
		else
			headerEditor.Focus();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		// Clear .NET Languages Add-on project assembly references when the sample unloads
		_projectAssembly.AssemblyReferences.Clear();
	}

}
