using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog;

/// <summary>
/// Provides a tree node model implementation for an artist.
/// </summary>
public class ArtistTreeNodeModel : MusicTreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ArtistTreeNodeModel() {
		var imageUri = new Uri("/Images/Icons/User16.png", UriKind.Relative);
		ImageSource = new BitmapImage(imageUri);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool IsArtist
		=> true;

}
