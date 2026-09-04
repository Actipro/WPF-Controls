namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Provides the base requirements for a control that has variant <see cref="ImageSource"/> properties.
/// </summary>
public interface IHasVariantImages {

	/// <summary>
	/// The <see cref="ImageSource"/> for a large image, generally <c>32x32</c> size.
	/// </summary>
	ImageSource? LargeImageSource { get; set; }

	/// <summary>
	/// The <see cref="ImageSource"/> for a medium image, generally <c>24x24</c> size.
	/// </summary>
	ImageSource? MediumImageSource { get; set; }

	/// <summary>
	/// The <see cref="ImageSource"/> for a small image, generally <c>16x16</c> size.
	/// </summary>
	ImageSource? SmallImageSource { get; set; }

}
