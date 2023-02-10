using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Media;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Represents a tag color for a gallery item used by the "View Options with Color Tagging" showcase sample.
	/// </summary>
	public class TagColorGalleryItem : ColorBarGalleryItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="TagColorGalleryItem"/> class.
		/// </summary>
		/// <param name="color">The color associated with the tag.</param>
		/// <param name="label">The label associated with the tag.</param>
		public TagColorGalleryItem(Color color, string label)
			: base(category: null, color, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the default collection of <see cref="TagColorGalleryItem"/> instances.
		/// </summary>
		/// <returns>An array of type <see cref="TagColorGalleryItem"/>.</returns>
		public static TagColorGalleryItem[] CreateDefaultCollection() {
			return new TagColorGalleryItem[] {
				new TagColorGalleryItem(UIColor.FromWebColor("#f04f58").ToColor(), "Red"),
				new TagColorGalleryItem(UIColor.FromWebColor("#f1a247").ToColor(), "Orange"),
				new TagColorGalleryItem(UIColor.FromWebColor("#f3cf4a").ToColor(), "Yellow"),
				new TagColorGalleryItem(UIColor.FromWebColor("#5dd260").ToColor(), "Green"),
				new TagColorGalleryItem(UIColor.FromWebColor("#5c85f5").ToColor(), "Blue"),
				new TagColorGalleryItem(UIColor.FromWebColor("#b163d3").ToColor(), "Purple"),
				new TagColorGalleryItem(UIColor.FromWebColor("#9c9ca0").ToColor(), "Gray"),
			};
		}

	}

}
