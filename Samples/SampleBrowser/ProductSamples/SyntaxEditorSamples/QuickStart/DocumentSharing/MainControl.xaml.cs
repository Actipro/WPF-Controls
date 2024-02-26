using System.Windows.Media;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;

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

			// SAMPLE NOTE: To help differentiate the left and right editors, the left editor will
			// use a light theme and the right editor will use a dark theme.  This is unrelated to the topic
			// of sharing a document, but helps reinforce that two editors are being used instead of a single
			// editor with a split view.

			// Create a custom highlighting style registry for the left editor that is always using
			// the light color palette with explicit plain text and margin colors (instead of inheriting current theme)
			var leftRegistry = new HighlightingStyleRegistry();
			new DisplayItemClassificationTypeProvider(leftRegistry).RegisterAll();
			new DotNetClassificationTypeProvider(leftRegistry).RegisterAll();
			leftRegistry.CurrentColorPalette = leftRegistry.LightColorPalette;
			var leftPlainTextStyle = leftRegistry[leftRegistry.GetClassificationType(DisplayItemClassificationTypeKeys.PlainText)];
			if (leftPlainTextStyle is not null) {
				leftPlainTextStyle.Foreground = Colors.Black;
				leftPlainTextStyle.Background = Colors.White;
			}
			leftEditor.HighlightingStyleRegistry = leftRegistry;
			leftEditor.CaretBrush = Brushes.Black;

			// Create a custom highlighting style registry for the right editor that is always using
			// the dark color palette with explicit plain text and margin colors (instead of inheriting current theme)
			var rightRegistry = new HighlightingStyleRegistry();
			new DisplayItemClassificationTypeProvider(rightRegistry).RegisterAll();
			new DotNetClassificationTypeProvider(rightRegistry).RegisterAll();
			rightRegistry.CurrentColorPalette = rightRegistry.DarkColorPalette;
			var rightPlainTextStyle = rightRegistry[rightRegistry.GetClassificationType(DisplayItemClassificationTypeKeys.PlainText)];
			if (rightPlainTextStyle is not null) {
				rightPlainTextStyle.Foreground = UIColor.FromWebColor("#dcdcdc");
				rightPlainTextStyle.Background = UIColor.FromWebColor("#1e1e1e");
			}
			rightEditor.HighlightingStyleRegistry = rightRegistry;
			rightEditor.CaretBrush = Brushes.White;
		}

	}

}