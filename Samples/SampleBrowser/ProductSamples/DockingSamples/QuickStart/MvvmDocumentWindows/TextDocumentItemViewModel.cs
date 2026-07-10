using ActiproSoftware.ProductSamples.DockingSamples.Common;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmDocumentWindows;

/// <summary>
/// Represents the text document view-model.
/// </summary>
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
		ImageSource = new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative));
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
