using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for an object that can provide images for bar controls.
	/// </summary>
	public interface IBarImageProvider {

		/// <summary>
		/// Returns an <see cref="ImageSource"/> for the specified bar control key and size.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <param name="size">A <see cref="BarImageSize"/> that indicates the image size.</param>
		/// <returns>An <see cref="ImageSource"/> for the specified bar control key and size.</returns>
		public ImageSource GetImageSource(string key, BarImageSize size);

		/// <summary>
		/// Returns an <see cref="ImageSource"/> for the specified bar control key and set of options.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <param name="options">A <see cref="BarImageOptions"/> that indicates the image options.</param>
		/// <returns>An <see cref="ImageSource"/> for the specified bar control key and set of options.</returns>
		public ImageSource GetImageSource(string key, BarImageOptions options);

	}

}
