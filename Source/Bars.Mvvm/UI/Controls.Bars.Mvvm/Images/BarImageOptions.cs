namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Stores options data for an image used in a bar control.
/// </summary>
/// <param name="size">A <see cref="BarImageSize"/> indicating the image size.</param>
public struct BarImageOptions(BarImageSize size) {

	/// <summary>
	/// The default image options instance.
	/// </summary>
	public static readonly BarImageOptions Default = new(BarImageSize.Small);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The optional contextual <see cref="Color"/>.
	/// </summary>
	public Color? ContextualColor { get; set; } = null;

	/// <summary>
	/// A <see cref="BarImageSize"/> indicating the image size.
	/// </summary>
	public BarImageSize Size { get; set; } = size;

}
