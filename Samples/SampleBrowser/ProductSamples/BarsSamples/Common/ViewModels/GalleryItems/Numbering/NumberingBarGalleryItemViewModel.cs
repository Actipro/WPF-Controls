using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a numbering style as a gallery item view model for a bar gallery control.
	/// </summary>
	public class NumberingBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		/// <summary>
		/// The name of the category for the numbering library.
		/// </summary>
		public const string NumberingLibraryCategory = "Numbering Library";

		/// <summary>
		/// The string format to use for numbering followed by a dot (e.g., <c>1.</c>, <c>2.</c>, etc.).
		/// </summary>
		public const string DotFormat = "{0}.";

		/// <summary>
		/// The string format to use for numbering followed by a parenthesis (e.g., <c>1)</c>, <c>2)</c>, etc.).
		/// </summary>
		public const string ParenthesisFormat = "{0})";
		
		private string format;
		private NumberingKind kind;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="NumberingBarGalleryItemViewModel"/> class.
		/// </summary>
		public NumberingBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(NumberingLibraryCategory, NumberingKind.None, format: DotFormat, label: null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NumberingBarGalleryItemViewModel"/> class.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="kind">The kind of numbering.</param>
		/// <param name="format">The numbering format.</param>
		/// <param name="label">The text label to display.</param>
		public NumberingBarGalleryItemViewModel(string category, NumberingKind kind, string format, string label)
			: base(category) {

			if (string.IsNullOrEmpty(format))
				throw new ArgumentNullException(nameof(format));

			this.kind = kind;
			this.format = format;
			this.Label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing the numbering kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<NumberingBarGalleryItemViewModel> CreateDefaultCollection() {
			return new NumberingBarGalleryItemViewModel[] {
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.None, DotFormat, "None"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.ArabicNumeral, DotFormat, "Arabic Numerals with Dots"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.ArabicNumeral, ParenthesisFormat, "Arabic Numerals with Parenthesis"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.UpperRomanNumeral, DotFormat, "Uppercase Roman Numerals with Dots"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.LowerRomanNumeral, DotFormat, "Lowercase Roman Numerals with Dots"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.UpperAlpha, DotFormat, "Uppercase Letters with Dots"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.UpperAlpha, ParenthesisFormat, "Uppercase Letters with Parenthesis"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.LowerAlpha, DotFormat, "Lowercase Letters with Dots"),
				new NumberingBarGalleryItemViewModel(NumberingLibraryCategory, NumberingKind.LowerAlpha, ParenthesisFormat, "Lowercase Letters with Parenthesis"),
			};
		}
		
		/// <summary>
		/// Gets or sets the string format used for displaying the numbering.
		/// </summary>
		/// <value>
		/// A string format value; e.g., <c>{0}.</c> to display a dot after the numbering.
		/// The default value is <see cref="DotFormat"/>.
		/// </value>
		/// <seealso cref="DotFormat"/>
		/// <seealso cref="ParenthesisFormat"/>
		public string Format {
			get => format;
			set {
				if (format != value) {
					if (string.IsNullOrEmpty(value))
						throw new ArgumentNullException(nameof(value));

					format = value;
					this.NotifyPropertyChanged(nameof(Format));
				}
			}
		}

		/// <summary>
		/// Gets or sets the kind of numbering.
		/// </summary>
		/// <value>One of the <see cref="NumberingKind"/> values.</value>
		public NumberingKind Kind {
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
			return $"{this.GetType().FullName}[Kind='{this.Kind}', Format='{this.Format}']";
		}

	}

}
