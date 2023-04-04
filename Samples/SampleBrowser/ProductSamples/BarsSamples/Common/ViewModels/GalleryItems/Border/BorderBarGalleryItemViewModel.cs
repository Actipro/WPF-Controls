using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a gallery item view model for a border style that will appear as menu item, only intended for use within a menu gallery.
	/// </summary>
	public class BorderBarGalleryItemViewModel : MenuItemBarGalleryItemViewModel {

		/// <summary>
		/// The name of the category for edge borders.
		/// </summary>
		public const string EdgeBordersCategory = "Edge Borders";

		/// <summary>
		/// The name of the category for other borders.
		/// </summary>
		public const string OtherBordersCategory = "Other Borders";
		
		private BorderKind kind;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BorderBarGalleryItemViewModel"/> class with no border.
		/// </summary>
		public BorderBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(OtherBordersCategory, BorderKind.None, "No Border", smallImageSource: null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BorderBarGalleryItemViewModel"/> class.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="kind">The kind of border.</param>
		/// <param name="label">The text label to display.</param>
		/// <param name="smallImageSource">The image.</param>
		public BorderBarGalleryItemViewModel(string category, BorderKind kind, string label, ImageSource smallImageSource)
			: base(category, label, smallImageSource) {

			this.kind = kind;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the kind of border.
		/// </summary>
		/// <value>One of the <see cref="BorderKind"/> values.</value>
		public BorderKind Kind {
			get => kind;
			set {
				if (kind != value) {
					kind = value;
					this.NotifyPropertyChanged(nameof(Kind));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Kind='{this.Kind}']";
		}

	}

}
