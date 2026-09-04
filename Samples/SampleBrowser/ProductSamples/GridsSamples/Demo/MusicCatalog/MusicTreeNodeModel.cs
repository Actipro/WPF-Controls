using ActiproSoftware.ProductSamples.GridsSamples.Common;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog;

/// <summary>
/// Provides a tree node model implementation for an artist, album, or track.
/// </summary>
public class MusicTreeNodeModel : TreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether there is a popularity value.
	/// </summary>
	public bool HasPopularity
		=> Popularity > 0;

	/// <summary>
	/// Indicates whether this model is for an artist.
	/// </summary>
	public virtual bool IsArtist
		=> false;

	/// <summary>
	/// The length.
	/// </summary>
	public string? Length { get; set; }

	/// <summary>
	/// The popularity.
	/// </summary>
	public int Popularity { get; set; }

}
