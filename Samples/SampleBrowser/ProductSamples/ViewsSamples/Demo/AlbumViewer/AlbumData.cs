namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.AlbumViewer;

/// <summary>
/// Stores brush data.
/// </summary>
public class AlbumData : ObservableObjectBase {

	private string? _albumName;
	private string? _artistName;
	private ImageSource? _imageSource;
	private double _rating;
	private string? _releaseDate;
	private int _trackCount;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The album name.
	/// </summary>
	public string? AlbumName {
		get => _albumName;
		set => SetProperty(ref _albumName, value);
	}

	/// <summary>
	/// The artist name.
	/// </summary>
	public string? ArtistName {
		get => _artistName;
		set => SetProperty(ref _artistName, value);
	}

	/// <summary>
	/// The image source.
	/// </summary>
	public ImageSource? ImageSource {
		get => _imageSource;
		set => SetProperty(ref _imageSource, value);
	}

	/// <summary>
	/// The rating.
	/// </summary>
	public double Rating {
		get => _rating;
		set => SetProperty(ref _rating, value);
	}

	/// <summary>
	/// The release date.
	/// </summary>
	public string? ReleaseDate {
		get => _releaseDate;
		set => SetProperty(ref _releaseDate, value);
	}

	/// <summary>
	/// The track count.
	/// </summary>
	public int TrackCount {
		get => _trackCount;
		set => SetProperty(ref _trackCount, value);
	}

}
