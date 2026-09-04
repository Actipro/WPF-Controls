using ActiproSoftware.Text;
using Step11 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted11;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted12;

/// <summary>
/// Represents a <c>Simple</c> syntax language definition.
/// </summary>
public partial class SimpleSyntaxLanguage : Step11.SimpleSyntaxLanguage {

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleSyntaxLanguage() {

		//
		// NOTE: This language inherits the language class defined in a previous step and thus
		//   automatically inherits all of its registered services
		//

		// Register an indenter provider service
		this.RegisterIndentProvider(new SimpleIndentProvider());

	}

}
