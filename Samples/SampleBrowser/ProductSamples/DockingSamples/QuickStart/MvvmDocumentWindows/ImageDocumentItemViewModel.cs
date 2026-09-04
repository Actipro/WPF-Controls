using ActiproSoftware.ProductSamples.DockingSamples.Common;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmDocumentWindows;

/// <summary>
/// Represents the image document view-model.
/// </summary>
public class ImageDocumentItemViewModel : DocumentItemViewModel {

	private Uri? _uri;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ImageDocumentItemViewModel() {
		Description = "Image document";
		ImageSource = new BitmapImage(new Uri("/Images/Icons/Picture16.png", UriKind.Relative));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The image <see cref="Url"/> associated with the view-model.
	/// </summary>
	public Uri? Uri {
		get => _uri;
		set => SetProperty(ref _uri, value);
	}

}
