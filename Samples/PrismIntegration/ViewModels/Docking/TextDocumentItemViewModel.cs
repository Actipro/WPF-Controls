using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Represents a text view-model for the sample.
/// </summary>
/// <remarks>
/// This view-model derives from a base class that initializes the <c>DocumentWindow</c> from instance properties.
/// </remarks>
public class TextDocumentItemViewModel : DocumentItemViewModel {

	private string? _text;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TextDocumentItemViewModel() {
		Description = "Text document";
		ImageSource = new BitmapImage(new Uri("/Resources/Images/TextDocument16.png", UriKind.Relative));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The text associated with the view-model.
	/// </summary>
	public string? Text {
		get => _text;
		set => SetProperty(ref _text, value);
	}

}
