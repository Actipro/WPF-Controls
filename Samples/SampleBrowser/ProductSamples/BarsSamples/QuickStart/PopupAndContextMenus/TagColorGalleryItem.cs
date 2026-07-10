using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus;

/// <summary>
/// Represents a tag color for a gallery item used by the "View Options with Color Tagging" showcase sample.
/// </summary>
/// <param name="value">The color associated with the tag.</param>
/// <param name="label">The label associated with the tag.</param>
public class TagColorGalleryItem(Color value, string label) : ColorBarGalleryItemViewModel(value, category: null, label) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the default collection of <see cref="TagColorGalleryItem"/> instances.
	/// </summary>
	/// <returns>An array of type <see cref="TagColorGalleryItem"/>.</returns>
	public static TagColorGalleryItem[] CreateDefaultCollection() {
		return [
			new(UIColor.FromWebColor("#f04f58").ToColor(), "Red"),
			new(UIColor.FromWebColor("#f1a247").ToColor(), "Orange"),
			new(UIColor.FromWebColor("#f3cf4a").ToColor(), "Yellow"),
			new(UIColor.FromWebColor("#5dd260").ToColor(), "Green"),
			new(UIColor.FromWebColor("#5c85f5").ToColor(), "Blue"),
			new(UIColor.FromWebColor("#b163d3").ToColor(), "Purple"),
			new(UIColor.FromWebColor("#9c9ca0").ToColor(), "Gray"),
		];
	}

}
