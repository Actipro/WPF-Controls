using System;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog {
	
	/// <summary>
	/// Provides a tree node model implementation for a track.
	/// </summary>
	public class TrackTreeNodeModel : MusicTreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TrackTreeNodeModel"/> class.
		/// </summary>
		public TrackTreeNodeModel() {
			var imageUri = new Uri("/Images/Icons/Music16.png", UriKind.Relative);
			this.ImageSource = new BitmapImage(imageUri);
		}
		
	}

}
