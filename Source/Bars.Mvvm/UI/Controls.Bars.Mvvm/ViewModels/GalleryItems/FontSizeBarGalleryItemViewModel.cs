using ActiproSoftware.Products.Bars.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a font size.
	/// </summary>
	public class FontSizeBarGalleryItemViewModel : BarGalleryItemViewModel<double> {

		internal const double DefaultFontSize = 12.0;
		private const double FontSizeMultiplier = 96.0 / 72.0;  // Used to multiply against UI font sizes to generate a proper WPF font size
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default font size.
		/// </summary>
		public FontSizeBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(DefaultFontSize) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size and a default category.
		/// </summary>
		/// <param name="value">The font size.</param>
		public FontSizeBarGalleryItemViewModel(double value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size and category.
		/// </summary>
		/// <param name="value">The font size.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public FontSizeBarGalleryItemViewModel(double value, string category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size, category, and label.
		/// </summary>
		/// <param name="value">The font size.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public FontSizeBarGalleryItemViewModel(double value, string category, string label)
			: base(ValidateFontSize(value, nameof(value)), category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the given font size is valid and throws <see cref="ArgumentOutOfRangeException"/> if invalid.
		/// </summary>
		/// <param name="size">The value to be tested.</param>
		/// <param name="paramName">The name of the parameter which defined the value.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the given font size is not valid.</exception>
		/// <returns>Returns the font size if an exception is not thrown.</returns>
		private static double ValidateFontSize(double size, string paramName) {
			if (size < 1.0)
				throw new ArgumentOutOfRangeException(paramName, size, "Font size must be 1.0 or greater.");

			return size;
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
		/// Creates an <see cref="CollectionViewSource"/> of gallery item view models representing common font sizes, allowing for possible categorization and filtering.
		/// </summary>
		/// <param name="categorize">Whether the collection view source should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
		/// <returns>The <see cref="CollectionViewSource"/> of gallery item view models that was created.</returns>
		public static CollectionViewSource CreateDefaultCollectionViewSource(bool categorize)
			=> BarGalleryViewModel.CreateCollectionViewSource(CreateDefaultCollection(), categorize);

		/// <summary>
		/// Gets the localizable default category to be used for view models of this type.
		/// </summary>
		/// <value>The string category name.</value>
		public static string DefaultCategory => SR.GetString(SRName.UIGalleryItemCategoryFontSizesText);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public override bool IsLabelVisible => true;

		/// <inheritdoc/>
		public override double Value {
			get => base.Value;
			set => base.Value = ValidateFontSize(value, nameof(value));
		}

	}

}
