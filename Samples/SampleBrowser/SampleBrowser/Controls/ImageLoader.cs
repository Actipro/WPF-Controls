using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the ability to load images.
	/// </summary>
	public static class ImageLoader {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an <see cref="ImageSource"/>.
		/// </summary>
		/// <param name="relPath">The path of the resource file relative to the <c>Images</c> folder.</param>
		/// <returns>An <see cref="ImageSource"/>.</returns>
		private static ImageSource LoadImageResource(string relPath) {
			if (relPath is null)
				throw new ArgumentNullException(nameof(relPath));

			var path = "pack://application:,,,/SampleBrowser;component/Images";
			if (relPath.StartsWith("/"))
				path += relPath;
			else
				path += "/" + relPath;

			var imageSource = new BitmapImage(new Uri(path, UriKind.Absolute));
			if (imageSource.CanFreeze)
				imageSource.Freeze();
			return imageSource;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an <see cref="ImageSource"/> for an icon.
		/// </summary>
		/// <param name="fileName">The name of the file in the <c>/Images/Icons</c> folder.</param>
		/// <returns>An <see cref="ImageSource"/>.</returns>
		public static ImageSource GetIcon(string fileName) => LoadImageResource("/Icons/" + fileName);

		/// <summary>
		/// Gets an <see cref="ImageSource"/> for an icon.
		/// </summary>
		/// <param name="fileName">The name of the file in the <c>/Images/Other</c> folder.</param>
		/// <returns>An <see cref="ImageSource"/>.</returns>
		public static ImageSource GetOther(string fileName) => LoadImageResource("/Other/" + fileName);


	}

}
