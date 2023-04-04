using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that has variant <see cref="ImageSource"/> properties.
	/// </summary>
	public interface IHasVariantImages {

		/// <summary>
		/// Gets or sets the <see cref="ImageSource"/> for a large image, generally <c>32x32</c> size.
		/// </summary>
		/// <value>The <see cref="ImageSource"/> for a large image.</value>
		ImageSource LargeImageSource { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ImageSource"/> for a medium image, generally <c>24x24</c> size.
		/// </summary>
		/// <value>The <see cref="ImageSource"/> for a medium image.</value>
		ImageSource MediumImageSource { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ImageSource"/> for a small image, generally <c>16x16</c> size.
		/// </summary>
		/// <value>The <see cref="ImageSource"/> for a small image.</value>
		ImageSource SmallImageSource { get; set; }

	}

}
