using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents an underline style as a gallery item view model for a bar gallery control.
	/// </summary>
	public class UnderlineBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		/// <summary>
		/// The name of the category for underlines.
		/// </summary>
		public const string UnderlineCategory = "Underline";
		
		private UnderlineKind kind;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="UnderlineBarGalleryItemViewModel"/> class.
		/// </summary>
		public UnderlineBarGalleryItemViewModel() : this(UnderlineKind.None, label: null) { }  // Parameterless constructor required for XAML support

		/// <summary>
		/// Initializes a new instance of the <see cref="UnderlineBarGalleryItemViewModel"/> class.
		/// </summary>
		/// <param name="kind">The kind of underline.</param>
		/// <param name="label">The text label to display.</param>
		public UnderlineBarGalleryItemViewModel(UnderlineKind kind, string label)
			: base(UnderlineCategory) {

			this.kind = kind;
			this.Label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing the underline kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<UnderlineBarGalleryItemViewModel> CreateDefaultCollection() {
			return new UnderlineBarGalleryItemViewModel[] {
				new UnderlineBarGalleryItemViewModel(UnderlineKind.Underline, "Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DoubleUnderline, "Double Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.ThickUnderline, "Thick Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DottedUnderline, "Dotted Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DashedUnderline, "Dashed Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DotDashUnderline, "Dot-Dash Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DotDotDashUnderline, "Dot-Dot-Dash Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.WaveUnderline, "Wave Underline"),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.None, "No Underline"),
			};
		}

		/// <summary>
		/// Gets or sets the kind of underline.
		/// </summary>
		/// <value>One of the <see cref="UnderlineKind"/> values.</value>
		public UnderlineKind Kind {
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
