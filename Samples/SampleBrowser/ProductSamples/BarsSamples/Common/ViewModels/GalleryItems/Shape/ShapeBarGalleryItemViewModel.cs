using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a shape as a gallery item view model for a bar gallery control.
	/// </summary>
	public class ShapeBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		/// <summary>
		/// The name of the category for basic shapes.
		/// </summary>
		public const string BasicShapesCategory = "Basic Shapes";

		/// <summary>
		/// The name of the category for line shapes.
		/// </summary>
		public const string LinesCategory = "Lines";

		/// <summary>
		/// The name of the category for rectangle shapes.
		/// </summary>
		public const string RectanglesCategory = "Rectangles";

		private ShapeKind kind;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeBarGalleryItemViewModel"/> class with a rectangle shape.
		/// </summary>
		public ShapeBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(RectanglesCategory, ShapeKind.Rectangle, null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeBarGalleryItemViewModel"/> class.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="kind">The kind of shape.</param>
		/// <param name="label">The text label to display.</param>
		public ShapeBarGalleryItemViewModel(string category, ShapeKind kind, string label)
			: base(category) {

			this.kind = kind;
			this.Label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing the shape kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ShapeBarGalleryItemViewModel> CreateDefaultCollection() {
			return new ShapeBarGalleryItemViewModel[] {
				new ShapeBarGalleryItemViewModel(LinesCategory, ShapeKind.Line, "Line"),
				new ShapeBarGalleryItemViewModel(LinesCategory, ShapeKind.LineArrow, "Line Arrow"),
				new ShapeBarGalleryItemViewModel(LinesCategory, ShapeKind.LineArrowDouble, "Line Arrow: Double"),
				new ShapeBarGalleryItemViewModel(RectanglesCategory, ShapeKind.Rectangle, "Rectangle"),
				new ShapeBarGalleryItemViewModel(RectanglesCategory, ShapeKind.RectangleRoundedCorners, "Rectangle: Rounded Corners"),
				new ShapeBarGalleryItemViewModel(RectanglesCategory, ShapeKind.RectangleSingleCornerSnipped, "Rectangle: Single Corner Snipped"),
				new ShapeBarGalleryItemViewModel(BasicShapesCategory, ShapeKind.Oval, "Oval"),
				new ShapeBarGalleryItemViewModel(BasicShapesCategory, ShapeKind.RightTriangle, "Right Triangle"),
			};
		}

		/// <summary>
		/// Gets or sets the kind of shape.
		/// </summary>
		/// <value>One of the <see cref="ShapeKind"/> values.</value>
		public ShapeKind Kind {
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
