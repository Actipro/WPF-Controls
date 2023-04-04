using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model that will appear as menu item, only intended for use within a menu gallery.
	/// </summary>
	public class MenuItemBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		private ImageSource smallImageSource;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItemBarGalleryItemViewModel"/> class.
		/// </summary>
		public MenuItemBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(category: null, label: null, smallImageSource: null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItemBarGalleryItemViewModel"/> class.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="label">The text label to display.</param>
		/// <param name="smallImageSource">The image.</param>
		public MenuItemBarGalleryItemViewModel(string category, string label, ImageSource smallImageSource)
			: base(category) {

			this.Label = label;
			this.LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem;
			this.smallImageSource = smallImageSource;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the image to be displayed for the gallery item.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public ImageSource SmallImageSource {
			get => smallImageSource;
			set {
				if (smallImageSource != value) {
					smallImageSource = value;
					NotifyPropertyChanged(nameof(SmallImageSource));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Label='{this.Label}']";
		}
		
	}

}
