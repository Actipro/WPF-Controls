using Step8 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09;

/// <summary>
/// Represents a <c>Simple</c> syntax language definition.
/// </summary>
public partial class SimpleSyntaxLanguage : Step8.SimpleSyntaxLanguage {

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleSyntaxLanguage() {

		//
		// NOTE: This language inherits the language class defined in a previous step and thus
		//   automatically inherits all of its registered services
		//

		// Register a quick info provider
		RegisterService(new SimpleQuickInfoProvider());

	}

}
