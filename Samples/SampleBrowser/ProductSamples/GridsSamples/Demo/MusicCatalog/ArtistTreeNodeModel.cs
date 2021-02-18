using System;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog {
	
	/// <summary>
	/// Provides a tree node model implementation for an artist.
	/// </summary>
	public class ArtistTreeNodeModel : MusicTreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ArtistTreeNodeModel"/> class.
		/// </summary>
		public ArtistTreeNodeModel() {
			var imageUri = new Uri("/Images/Icons/User16.png", UriKind.Relative);
			this.ImageSource = new BitmapImage(imageUri);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether this model is for an artist.
		/// </summary>
		/// <value>
		/// <c>true</c> if this model is for an artist; otherwise, <c>false</c>.
		/// </value>
		public override bool IsArtist {
			get {
				return true;
			}
		}

	}

}
