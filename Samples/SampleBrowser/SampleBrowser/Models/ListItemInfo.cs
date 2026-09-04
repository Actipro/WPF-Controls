namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides information about a list item.
/// </summary>
public class ListItemInfo {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The blurb text.
	/// </summary>
	public string? BlurbText { get; set; }

	/// <summary>
	/// Indicates whether there is any blurb text.
	/// </summary>
	public bool HasBlurbText
		=> !string.IsNullOrEmpty(BlurbText);

	/// <summary>
	/// The <see cref="System.Windows.Media.ImageSource"/> to display.
	/// </summary>
	public ImageSource? ImageSource { get; set; }

	/// <summary>
	/// Indicates whether the linked item is external.
	/// </summary>
	public bool IsExternal
		=> TargetUri?.Query?.Contains("action=open") == true;

	/// <summary>
	/// The target <see cref="Uri"/> if a link should be created.
	/// </summary>
	public Uri? TargetUri { get; set; }

	/// <summary>
	/// The title.
	/// </summary>
	public string? Title { get; set; }

}
