using ActiproSoftware.Windows.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a <see cref="ResourceDictionary"/> related to the custom <see cref="BarGalleryItem"/> resources objects defined in this assembly.
/// </summary>
public sealed partial class CustomBarGalleryItemDictionary : ResourceDictionary {

	[ThreadStatic]
	private static CustomBarGalleryItemDictionary? _instance;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomBarGalleryItemDictionary() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The singleton instance of the resource dictionary.
	/// </summary>
	/// <remarks>
	/// The instance is not shared between threads.
	/// </remarks>
	public static CustomBarGalleryItemDictionary Instance
		=> _instance ??= [];

}
