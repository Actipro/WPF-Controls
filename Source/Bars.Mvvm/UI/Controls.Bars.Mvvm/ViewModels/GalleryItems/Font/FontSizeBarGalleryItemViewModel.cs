using ActiproSoftware.Products.Bars.Mvvm;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a font size.
	/// </summary>
	public class FontSizeBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		private double size;

		internal const double DefaultFontSize = 12.0;
		private const double FontSizeMultiplier = 96.0 / 72.0;  // Used to multiply against UI font sizes to generate a proper WPF font size
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public FontSizeBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(DefaultFontSize) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size.
		/// </summary>
		/// <param name="size">The font size.</param>
		public FontSizeBarGalleryItemViewModel(double size)
			: base(SR.GetString(SRName.UIGalleryItemCategoryFontSizesText)) {

			this.size = Math.Max(1.0, size);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts a WPF font size to a display font size (used in this view model).
		/// </summary>
		/// <param name="wpfFontSize">The WPF font size to convert.</param>
		/// <returns>The converted font size.</returns>
		public static double ConvertFontSizeFromWpfFontSize(double wpfFontSize)
			=> Math.Round(wpfFontSize / FontSizeMultiplier, MidpointRounding.AwayFromZero);
		
		/// <summary>
		/// Converts a display font size (used in this view model) to a WPF font size.
		/// </summary>
		/// <param name="fontSize">The display font size to convert.</param>
		/// <returns>The converted font size.</returns>
		public static double ConvertFontSizeToWpfFontSize(double fontSize)
			=> Math.Round(fontSize * FontSizeMultiplier, MidpointRounding.AwayFromZero);

		/// <summary>
		/// Creates a default collection of gallery item view models representing common font sizes.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<FontSizeBarGalleryItemViewModel> CreateDefaultCollection() {
			return new FontSizeBarGalleryItemViewModel[] {
				new FontSizeBarGalleryItemViewModel(8.0),
				new FontSizeBarGalleryItemViewModel(9.0),
				new FontSizeBarGalleryItemViewModel(10.0),
				new FontSizeBarGalleryItemViewModel(11.0),
				new FontSizeBarGalleryItemViewModel(12.0),
				new FontSizeBarGalleryItemViewModel(14.0),
				new FontSizeBarGalleryItemViewModel(16.0),
				new FontSizeBarGalleryItemViewModel(18.0),
				new FontSizeBarGalleryItemViewModel(20.0),
				new FontSizeBarGalleryItemViewModel(22.0),
				new FontSizeBarGalleryItemViewModel(24.0),
				new FontSizeBarGalleryItemViewModel(26.0),
				new FontSizeBarGalleryItemViewModel(28.0),
				new FontSizeBarGalleryItemViewModel(36.0),
				new FontSizeBarGalleryItemViewModel(48.0),
				new FontSizeBarGalleryItemViewModel(72.0),
			};
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public override bool IsLabelVisible => true;

		/// <summary>
		/// Gets or sets the text label to display.
		/// </summary>
		/// <value>The text label to display.</value>
		/// <remarks>If the label is not explicitly defined, the current <see cref="Size"/> value will be used as the label.</remarks>
		public override string Label {
			get => base.Label ?? Size.ToString();
			set => base.Label = value;
		}

		/// <summary>
		/// Gets or sets the font size.
		/// </summary>
		/// <value>The font size.</value>
		public double Size {
			get {
				return size;
			}
			set {
				if (size != value) {
					size = value;
					this.NotifyPropertyChanged(nameof(Size));
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Size={this.Size}]";
		}

	}

}
