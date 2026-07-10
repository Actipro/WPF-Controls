using ActiproSoftware.Windows.Media;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.SharedSamples.Common;

/// <summary>
/// Defines a custom <see cref="ImageProvider"/> which overrides several Actipro-styled icons with their system-defined equivalents.
/// </summary>
public class SystemIconsImageProvider : ImageProvider {

	private static ImageSource? _errorImage;
	private static ImageSource? _infoImage;
	private static ImageSource? _questionImage;
	private static ImageSource? _warningImage;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static SystemIconsImageProvider() {
		Instance = new SystemIconsImageProvider();
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	private SystemIconsImageProvider() {
		// Private constructor to enforce singleton pattern
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="BitmapSource"/> for the given <see cref="Icon"/>.
	/// </summary>
	/// <param name="icon">The icon.</param>
	private static BitmapSource CreateBitmapSource(Icon icon) {
		// Create the image
		var image = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

		// Prevent the image from being adapted to different themes
		image.SetValue(CanAdaptProperty, false);

		return image;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The error image.
	/// </summary>
	public static ImageSource ErrorImage
		=> _errorImage ??= CreateBitmapSource(SystemIcons.Error);

	/// <inheritdoc/>
	public override ImageSource? GetImageSource(string key) {
		// Map image keys to their system icon equivalents
		return key switch {
			SharedImageSourceKeys.Error => ErrorImage,
			SharedImageSourceKeys.Information => InformationImage,
			SharedImageSourceKeys.Question => QuestionImage,
			SharedImageSourceKeys.Warning => WarningImage,

			// Use default processing for all other images
			_ => base.GetImageSource(key)
		};
	}

	/// <summary>
	/// The information image.
	/// </summary>
	public static ImageSource InformationImage
		=> _infoImage ??= CreateBitmapSource(SystemIcons.Information);

	/// <summary>
	/// The singleton instance of the <see cref="SystemIconsImageProvider"/> class.
	/// </summary>
	public static SystemIconsImageProvider Instance { get; }

	/// <summary>
	/// The question image.
	/// </summary>
	public static ImageSource QuestionImage
		=> _questionImage ??= CreateBitmapSource(SystemIcons.Question);

	/// <summary>
	/// The warning image.
	/// </summary>
	public static ImageSource WarningImage
		=> _warningImage ??= CreateBitmapSource(SystemIcons.Warning);

}
