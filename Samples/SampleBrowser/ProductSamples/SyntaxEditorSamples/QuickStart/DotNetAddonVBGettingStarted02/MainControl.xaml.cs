using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.VB.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.DotNetAddonVBGettingStarted02;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// A project assembly (similar to a Visual Studio project) contains source files and assembly references for reflection
	private readonly VBProjectAssembly _projectAssembly;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
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
		_projectAssembly = new VBProjectAssembly("SampleBrowser");
		var assemblyLoader = new BackgroundWorker();
		assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
		assemblyLoader.RunWorkerAsync();

		// Load the .NET Languages Add-on Visual Basic language and register the project assembly on it
		var language = new VBSyntaxLanguage();
		language.RegisterProjectAssembly(_projectAssembly);
		codeEditor.Document.Language = language;
	}

	private void DotNetProjectAssemblyReferenceLoader(object? sender, DoWorkEventArgs e) {
		// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
		SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(_projectAssembly);
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
