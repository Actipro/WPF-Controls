using ActiproSoftware.Text;
using Step14 = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted14;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted15;

/// <summary>
/// Represents a <c>Simple</c> syntax language definition.
/// </summary>
public partial class SimpleSyntaxLanguage : Step14.SimpleSyntaxLanguage {

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleSyntaxLanguage() {

		//
		// NOTE: This language inherits the language class defined in a previous step and thus
		//   automatically inherits all of its registered services
		//

		// Register a navigable symbol provider service
		this.RegisterNavigableSymbolProvider(new SimpleNavigableSymbolProvider());
	}

}
