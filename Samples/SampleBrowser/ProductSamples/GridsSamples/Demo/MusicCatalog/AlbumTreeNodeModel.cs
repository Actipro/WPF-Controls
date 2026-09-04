using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog;

/// <summary>
/// Provides a tree node model implementation for an album.
/// </summary>
public class AlbumTreeNodeModel : MusicTreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AlbumTreeNodeModel() {
		var imageUri = new Uri("/Images/Icons/CD16.png", UriKind.Relative);
		ImageSource = new BitmapImage(imageUri);
	}

}
