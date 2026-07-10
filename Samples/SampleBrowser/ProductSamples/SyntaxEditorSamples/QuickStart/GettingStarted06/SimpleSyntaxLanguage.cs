using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using Step5 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted05;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted06;

/// <summary>
/// Represents a <c>Simple</c> syntax language definition.
/// </summary>
public partial class SimpleSyntaxLanguage : Step5.SimpleSyntaxLanguage {

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleSyntaxLanguage() {

		//
		// NOTE: This language inherits the language class defined in a previous step and thus
		//   automatically inherits all of its registered services
		//

		// Register an outliner
		this.RegisterOutliner(new SimpleOutliner());

		// Register a built-in service that automatically provides quick info tips
		//   when hovering over collapsed outlining nodes
		RegisterService(new CollapsedRegionQuickInfoProvider());

	}

}
