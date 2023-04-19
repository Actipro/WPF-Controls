using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <inheritdoc/>
	/// <remarks>
	/// The base class has been extended to define additional <see cref="DataTemplate"/> and <see cref="ResourceKey"/> properties
	/// for common view models used by this sample.
	/// </remarks>
	public class CustomBarGalleryItemTemplateSelector : BarGalleryItemTemplateSelector {
		
		// Resource keys within CustomBarGalleryItemDictionary.xaml
		private static ComponentResourceKey bulletTemplateResourceKey;
		private static ComponentResourceKey numberingTemplateResourceKey;
		private static ComponentResourceKey shapeTemplateResourceKey;
		private static ComponentResourceKey underlineTemplateResourceKey;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomBarGalleryItemTemplateSelector"/> class.
		/// </summary>
		public CustomBarGalleryItemTemplateSelector() {
			var dictionary = CustomBarGalleryItemDictionary.Instance;

			this.BulletTemplate = dictionary[BulletTemplateResourceKey] as DataTemplate;
			this.NumberingTemplate = dictionary[NumberingTemplateResourceKey] as DataTemplate;
			this.ShapeTemplate = dictionary[ShapeTemplateResourceKey] as DataTemplate;
			this.UnderlineTemplate = dictionary[UnderlineTemplateResourceKey] as DataTemplate;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			switch (item) {
				case BulletBarGalleryItemViewModel _:
					return this.BulletTemplate;
				case NumberingBarGalleryItemViewModel _:
					return this.NumberingTemplate;
				case ShapeBarGalleryItemViewModel _:
					return this.ShapeTemplate;
				case UnderlineBarGalleryItemViewModel _:
					return this.UnderlineTemplate;
			}

			return base.SelectTemplate(item, container);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="BulletBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate BulletTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="NumberingBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate NumberingTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="ShapeBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate ShapeTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="UnderlineBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate UnderlineTemplate { get; set; }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC RESOURCEKEY PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BulletTemplateResourceKey {
			get {
				if (bulletTemplateResourceKey is null)
					bulletTemplateResourceKey = new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(BulletTemplateResourceKey));
				return bulletTemplateResourceKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey NumberingTemplateResourceKey {
			get {
				if (numberingTemplateResourceKey is null)
					numberingTemplateResourceKey = new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(NumberingTemplateResourceKey));
				return numberingTemplateResourceKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey ShapeTemplateResourceKey {
			get {
				if (shapeTemplateResourceKey is null)
					shapeTemplateResourceKey = new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(ShapeTemplateResourceKey));
				return shapeTemplateResourceKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey UnderlineTemplateResourceKey {
			get {
				if (underlineTemplateResourceKey is null)
					underlineTemplateResourceKey = new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(UnderlineTemplateResourceKey));
				return underlineTemplateResourceKey;
			}
		}

	}

}
