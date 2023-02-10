using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a bullet style as a gallery item view model for a bar gallery control.
	/// </summary>
	public class BulletBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		/// <summary>
		/// The name of the category for bullets.
		/// </summary>
		public const string BulletLibraryCategory = "Bullet Library";

		private BulletKind kind;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletBarGalleryItemViewModel"/> class.
		/// </summary>
		public BulletBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(BulletLibraryCategory, BulletKind.None, label: null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletBarGalleryItemViewModel"/> class.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="kind">The kind of bullet.</param>
		/// <param name="label">The text label to display.</param>
		public BulletBarGalleryItemViewModel(string category, BulletKind kind, string label)
			: base(category) {

			this.kind = kind;
			this.Label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing the bullet kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<BulletBarGalleryItemViewModel> CreateDefaultCollection() {
			return new BulletBarGalleryItemViewModel[] {
				new BulletBarGalleryItemViewModel(BulletLibraryCategory, BulletKind.None, "None"),
				new BulletBarGalleryItemViewModel(BulletLibraryCategory, BulletKind.Circle, "Circle"),
				new BulletBarGalleryItemViewModel(BulletLibraryCategory, BulletKind.FilledCircle, "Filled Circle"),
				new BulletBarGalleryItemViewModel(BulletLibraryCategory, BulletKind.Square, "Square"),
				new BulletBarGalleryItemViewModel(BulletLibraryCategory, BulletKind.FilledSquare, "Filled Square"),
			};
		}

		/// <summary>
		/// Gets or sets the kind of bullet.
		/// </summary>
		/// <value>One of the <see cref="BulletKind"/> values.</value>
		public BulletKind Kind {
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
