using System;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog {
	
	/// <summary>
	/// Provides a tree node model implementation for an album.
	/// </summary>
	public class AlbumTreeNodeModel : MusicTreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AlbumTreeNodeModel"/> class.
		/// </summary>
		public AlbumTreeNodeModel() {
			var imageUri = new Uri("/Images/Icons/CD16.png", UriKind.Relative);
			this.ImageSource = new BitmapImage(imageUri);
		}
		
	}

}
