using System.Windows.Media.Imaging;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the ability to load images.
/// </summary>
public static class ImageLoader {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns an <see cref="ImageSource"/>.
	/// </summary>
	/// <param name="relPath">The path of the resource file relative to the <c>Images</c> folder.</param>
	/// <param name="freeze">When <c>true</c>, supported images will be frozen after they are loaded.</param>
	private static BitmapImage LoadImageResource(string relPath, bool freeze) {
		if (relPath is null)
			throw new ArgumentNullException(nameof(relPath));

		var path = "pack://application:,,,/SampleBrowser;component/Images";
		if (relPath.StartsWith("/"))
			path += relPath;
		else
			path += "/" + relPath;

		var imageSource = new BitmapImage(new Uri(path, UriKind.Absolute));
		if (freeze && imageSource.CanFreeze)
			imageSource.Freeze();
		return imageSource;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns an <see cref="ImageSource"/> for an icon from the <c>/Images/Icons</c> folder.
	/// </summary>
	/// <param name="fileName">The name of the file in the <c>/Images/Icons</c> folder.</param>
	/// <param name="freeze">When <c>true</c>, supported images will be frozen after they are loaded.</param>
	public static ImageSource GetIcon(string fileName, bool freeze = true)
		=> LoadImageResource("/Icons/" + fileName, freeze);

	/// <summary>
	/// Returns an <see cref="ImageSource"/> for an icon from the <c>/Images/Other</c> folder.
	/// </summary>
	/// <param name="fileName">The name of the file in the <c>/Images/Other</c> folder.</param>
	/// <param name="freeze">When <c>true</c>, supported images will be frozen after they are loaded.</param>
	/// <returns>An <see cref="ImageSource"/>.</returns>
	public static ImageSource GetOther(string fileName, bool freeze = true)
		=> LoadImageResource("/Other/" + fileName, freeze);

}
