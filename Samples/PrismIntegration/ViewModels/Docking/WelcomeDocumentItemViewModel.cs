using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Represents a welcome document view-model.
/// </summary>
/// <remarks>
/// This view-model derives from a base class that initializes the <c>DocumentWindow</c> from instance properties.
/// </remarks>
public class WelcomeDocumentItemViewModel : DocumentItemViewModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public WelcomeDocumentItemViewModel() {
		Description = "Rich-text document";
		ImageSource = new BitmapImage(new Uri("/Resources/Images/RichTextDocument16.png", UriKind.Relative));
		Title = "WelcomeDocument.rtf";
	}

}
