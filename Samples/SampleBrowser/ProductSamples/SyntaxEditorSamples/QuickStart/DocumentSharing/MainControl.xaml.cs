using System;
using System.Reflection;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DocumentSharing {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Add an assembly reference
			var projectAssembly = leftEditor.Document.Language.GetProjectAssembly();
			if (projectAssembly != null)
				projectAssembly.AssemblyReferences.AddMsCorLib();

			// Use the default ambient highlighting style registry for the left editor
			var leftRegistry = AmbientHighlightingStyleRegistry.Instance;
			new DisplayItemClassificationTypeProvider(leftRegistry).RegisterAll();
			new DotNetClassificationTypeProvider(leftRegistry).RegisterAll();

			// Create a custom highlighting style registry for the right editor
			var rightRegistry = new HighlightingStyleRegistry();
			new DisplayItemClassificationTypeProvider(rightRegistry).RegisterAll();
			new DotNetClassificationTypeProvider(rightRegistry).RegisterAll();
			rightEditor.HighlightingStyleRegistry = rightRegistry;

			// Load a dark theme, which has some example pre-defined styles for some of the more common syntax languages
			string path = SyntaxEditorHelper.ThemesPath + "Dark.vssettings";
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
				if (stream != null)
					rightRegistry.ImportHighlightingStyles(stream);
			}
		}

	}

}