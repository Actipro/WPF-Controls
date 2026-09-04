using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus;

/// <summary>
/// Represents a paste option for a gallery item used by the "Advanced Paste Options" showcase sample.
/// </summary>
public class PasteOptionGalleryItem : BarGalleryItemViewModel<PasteSpecialKind> {

	private ImageSource? _image;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="kind">The kind of special paste operation represented by the gallery item.</param>
	public PasteOptionGalleryItem(PasteSpecialKind kind)
		: base(kind, category: "Paste Options:") {

		// The base gallery item category is used by a custom DataTemplate for CollectionViewGroup to display
		//   the category name above the paste options
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Tests if the paste option can be executed against the given target.
	/// </summary>
	/// <param name="target">The target object.</param>
	public bool CanExecute(object target) {
		if (target is TextBox) {
			// Assume all kinds are supported for the purpose of this sample
			return true;
		}
		return false;
	}

	/// <summary>
	/// Creates the default <see cref="CollectionViewSource"/> of <see cref="PasteOptionGalleryItem"/> instances.
	/// </summary>
	public static CollectionViewSource CreateDefaultCollectionViewSource() {
		// NOTE: A CollectionViewSource is necessary to support the display of categories
		return BarGalleryViewModel.CreateCollectionViewSource(
			new PasteOptionGalleryItem[] {
				new(PasteSpecialKind.MergeFormatting) { Label = "Merge Formatting", KeyTipText = "M", Image = ImageLoader.GetIcon("PasteGalleryMerge24.png") },
				new(PasteSpecialKind.TextOnly) { Label = "Keep Text Only", KeyTipText = "T", Image = ImageLoader.GetIcon("PasteGalleryTextOnly24.png") },
				new(PasteSpecialKind.Picture) { Label = "Picture", KeyTipText = "U", Image = ImageLoader.GetIcon("PasteGalleryPicture24.png") },
			},
			categorize: true
		);
	}

	/// <summary>
	/// Executes the paste option against the given target.
	/// </summary>
	/// <param name="target">The target object.</param>
	public void Execute(object target) {
		if (target is TextBox textBox) {
			switch (Value) {
				case PasteSpecialKind.Default:
				case PasteSpecialKind.TextOnly:
					// Only plain text is supported
					textBox.Paste();
					break;
				default:
					// This is where the other special paste operations would need to be handled
					MessageBox.Show($"This is where you would add logic to handle the '{Value}' special paste operation.", "Paste Special", MessageBoxButton.OK, MessageBoxImage.Information);
					break;
			}
		}
	}

	/// <summary>
	/// The image for this paste option.
	/// </summary>
	public ImageSource? Image {
		get => _image;
		set => SetProperty(ref _image, value);
	}

}
