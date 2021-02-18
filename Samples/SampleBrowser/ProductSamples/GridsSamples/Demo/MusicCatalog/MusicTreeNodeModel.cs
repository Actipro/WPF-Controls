using ActiproSoftware.ProductSamples.GridsSamples.Common;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog {
	
	/// <summary>
	/// Provides a tree node model implementation for an artist, album, or track.
	/// </summary>
	public class MusicTreeNodeModel : TreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether there is a popularity value.
		/// </summary>
		/// <value>
		/// <c>true</c> if there is a popularity value; otherwise, <c>false</c>.
		/// </value>
		public bool HasPopularity {
			get {
				return (this.Popularity > 0);
			}
		}

		/// <summary>
		/// Gets whether this model is for an artist.
		/// </summary>
		/// <value>
		/// <c>true</c> if this model is for an artist; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsArtist {
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		public string Length { get; set; }

		/// <summary>
		/// Gets or sets the popularity.
		/// </summary>
		/// <value>The popularity.</value>
		public int Popularity { get; set; }

	}

}
