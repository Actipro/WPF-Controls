using ActiproSoftware.Windows.Media;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ActiproSoftware.ProductSamples.SharedSamples.Common {

	/// <summary>
	/// Defines a custom <see cref="ImageProvider"/> which overrides several Actipro-styled icons with their system-defined equivalents.
	/// </summary>
	public class SystemIconsImageProvider : ImageProvider {

		private static ImageSource errorImage;
		private static ImageSource infoImage;
		private static ImageSource questionImage;
		private static ImageSource warningImage;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="SystemIconsImageProvider"/> class.
		/// </summary>
		static SystemIconsImageProvider() {
			Instance = new SystemIconsImageProvider();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemIconsImageProvider"/> class.
		/// </summary>
		private SystemIconsImageProvider() {
			// Private constructor to enforce singleton pattern
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="BitmapSource"/> for the given <see cref="Icon"/>.
		/// </summary>
		/// <param name="icon">The icon.</param>
		/// <returns>A <see cref="BitmapSource"/> for the icon.</returns>
		private static BitmapSource CreateBitmapSource(Icon icon) {
			// Create the image
			var image = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

			// Prevent the image from being adapted to different themes
			image.SetValue(ImageProvider.CanAdaptProperty, false);

			return image;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the error image.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public static ImageSource ErrorImage {
			get {
				if (errorImage == null)
					errorImage = CreateBitmapSource(SystemIcons.Error);
				return errorImage;
			}
		}

		/// <summary>
		/// Returns the image referenced by the given <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key which identifies the image.</param>
		/// <returns>The <see cref="ImageSource"/> identified by the <paramref name="key"/>; or <c>null</c> if an <see cref="ImageSource"/> could not be identified.</returns>
		/// <seealso cref="SharedImageSourceKeys"/>
		public override ImageSource GetImageSource(string key) {
			// Map image keys to their system icon equivalents
			switch (key) {
				case SharedImageSourceKeys.Error:
					return ErrorImage;
				case SharedImageSourceKeys.Information:
					return InformationImage;
				case SharedImageSourceKeys.Question:
					return QuestionImage;
				case SharedImageSourceKeys.Warning:
					return WarningImage;
			}

			// Use default processing for all other images
			return base.GetImageSource(key);
		}

		/// <summary>
		/// Gets the information image.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public static ImageSource InformationImage {
			get {
				if (infoImage == null)
					infoImage = CreateBitmapSource(SystemIcons.Information);
				return infoImage;
			}
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="SystemIconsImageProvider"/> class.
		/// </summary>
		/// <value>The singleton instance of <see cref="SystemIconsImageProvider"/>.</value>
		public static SystemIconsImageProvider Instance { get; }

		/// <summary>
		/// Gets the question image.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public static ImageSource QuestionImage {
			get {
				if (questionImage == null)
					questionImage = CreateBitmapSource(SystemIcons.Question);
				return questionImage;
			}
		}

		/// <summary>
		/// Gets the warning image.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public static ImageSource WarningImage {
			get {
				if (warningImage == null)
					warningImage = CreateBitmapSource(SystemIcons.Warning);
				return warningImage;
			}
		}

	}

}
