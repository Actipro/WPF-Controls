using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a gallery item view model for a line spacing style that will appear as menu item, only intended for use within a menu gallery.
	/// </summary>
	public class LineSpacingBarGalleryItemViewModel : MenuItemBarGalleryItemViewModel {

		/// <summary>
		/// The name of the category for line spacing.
		/// </summary>
		public const string LineSpacingCategory = "Line Spacing";
		
		private double spacing;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="LineSpacingBarGalleryItemViewModel"/> class with default spacing.
		/// </summary>
		public LineSpacingBarGalleryItemViewModel() : this(1.2) { }  // Parameterless constructor required for XAML support

		/// <summary>
		/// Initializes a new instance of the <see cref="LineSpacingBarGalleryItemViewModel"/> class with the given spacing.
		/// </summary>
		/// <param name="spacing">The amount of spacing between lines.</param>
		public LineSpacingBarGalleryItemViewModel(double spacing)
			: base(LineSpacingCategory, label: null, smallImageSource: null) {

			this.spacing = spacing;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing the line spacing values.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<LineSpacingBarGalleryItemViewModel> CreateDefaultCollection() {
			return new LineSpacingBarGalleryItemViewModel[] {
				new LineSpacingBarGalleryItemViewModel(1.0),
				new LineSpacingBarGalleryItemViewModel(1.2),
				new LineSpacingBarGalleryItemViewModel(1.5),
				new LineSpacingBarGalleryItemViewModel(2.0),
				new LineSpacingBarGalleryItemViewModel(2.5),
				new LineSpacingBarGalleryItemViewModel(3.0),
			};
		}

		/// <inheritdoc/>
		public override string Label {
			get => base.Label ?? spacing.ToString("0.0");
			set => base.Label = value;
		}

		/// <summary>
		/// Gets or sets the amount of spacing between lines.
		/// </summary>
		/// <value>A double value.</value>
		public double Spacing {
			get => spacing;
			set {
				if (spacing != value) {
					spacing = value;
					this.NotifyPropertyChanged(nameof(Spacing));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Spacing={this.Spacing.ToString("0.0")}]";
		}

	}

}
