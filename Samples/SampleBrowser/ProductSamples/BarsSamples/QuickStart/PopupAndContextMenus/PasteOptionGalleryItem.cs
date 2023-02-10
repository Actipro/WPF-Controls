using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Represents a paste option for a gallery item used by the "Advanced Paste Options" showcase sample.
	/// </summary>
	public class PasteOptionGalleryItem : BarGalleryItemViewModelBase {

		private ImageSource image;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="PasteOptionGalleryItem"/> class.
		/// </summary>
		/// <param name="kind">The kind of special paste operation represented by the gallery item.</param>
		public PasteOptionGalleryItem(PasteSpecialKind kind)
			: base(category: "Paste Options:") {

			// The base gallery item category is used by a custom DataTemplate for CollectionViewGroup to display
			// the category name above the paste options

			this.Kind = kind;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the paste option can be executed against the given target.
		/// </summary>
		/// <param name="target">The target object.</param>
		/// <returns><c>true</c> if the paste option can execute; otherwise <c>false</c>.</returns>
		public bool CanExecute(object target) {
			if (target is TextBox) {
				// Assume all kinds are supported for the purpose of this sample
				return true;
			}
			return false;
		}

		/// <summary>
		/// Creates the default collection of <see cref="PasteOptionGalleryItem"/> instances.
		/// </summary>
		/// <returns>An array of type <see cref="PasteOptionGalleryItem"/>.</returns>
		public static PasteOptionGalleryItem[] CreateDefaultCollection() {
			return new PasteOptionGalleryItem[] {
				new PasteOptionGalleryItem(PasteSpecialKind.MergeFormatting) { Label = "Merge Formatting", KeyTipText = "M", Image = ImageLoader.GetIcon("PasteGalleryMerge24.png") },
				new PasteOptionGalleryItem(PasteSpecialKind.TextOnly) { Label = "Keep Text Only", KeyTipText = "T", Image = ImageLoader.GetIcon("PasteGalleryTextOnly24.png") },
				new PasteOptionGalleryItem(PasteSpecialKind.Picture) { Label = "Picture", KeyTipText = "U", Image = ImageLoader.GetIcon("PasteGalleryPicture24.png") },
			};
		}

		/// <summary>
		/// Executes the paste option against the given target.
		/// </summary>
		/// <param name="target">The target object.</param>
		public void Execute(object target) {
			if (target is TextBox textBox) {
				switch (this.Kind) {
					case PasteSpecialKind.Default:
					case PasteSpecialKind.TextOnly:
						// Only plain text is supported
						textBox.Paste();
						break;
					default:
						// This is where the other special paste operations would need to be handled
						ThemedMessageBox.Show($"This is where you would add logic to handle the '{this.Kind}' special paste operation.", "Paste Special", MessageBoxButton.OK, MessageBoxImage.Information);
						break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the image for this paste option.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public ImageSource Image {
			get => image;
			set {
				if (image != value) {
					image = value;
					NotifyPropertyChanged(nameof(Image));
				}
			}
		}

		/// <summary>
		/// Gets the kind of special paste operation represented by this gallery item.
		/// </summary>
		public PasteSpecialKind Kind { get; }

	}

}
