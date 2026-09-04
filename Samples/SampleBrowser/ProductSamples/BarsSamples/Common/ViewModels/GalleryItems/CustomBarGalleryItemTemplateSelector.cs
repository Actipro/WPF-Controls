using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <inheritdoc/>
/// <remarks>
/// The base class has been extended to define additional <see cref="DataTemplate"/> and <see cref="ResourceKey"/> properties
/// for common view models used by this sample.
/// </remarks>
public class CustomBarGalleryItemTemplateSelector : BarGalleryItemTemplateSelector {

	// Resource keys within CustomBarGalleryItemDictionary.xaml
	private static ComponentResourceKey? _bulletTemplateResourceKey;
	private static ComponentResourceKey? _numberingTemplateResourceKey;
	private static ComponentResourceKey? _shapeTemplateResourceKey;
	private static ComponentResourceKey? _underlineTemplateResourceKey;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomBarGalleryItemTemplateSelector() {
		var dictionary = CustomBarGalleryItemDictionary.Instance;

		BulletTemplate = dictionary[BulletTemplateResourceKey] as DataTemplate;
		NumberingTemplate = dictionary[NumberingTemplateResourceKey] as DataTemplate;
		ShapeTemplate = dictionary[ShapeTemplateResourceKey] as DataTemplate;
		UnderlineTemplate = dictionary[UnderlineTemplateResourceKey] as DataTemplate;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			BulletBarGalleryItemViewModel => BulletTemplate,
			NumberingBarGalleryItemViewModel => NumberingTemplate,
			ShapeBarGalleryItemViewModel => ShapeTemplate,
			UnderlineBarGalleryItemViewModel => UnderlineTemplate,
			_ => base.SelectTemplate(item, container)
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC DATATEMPLATE PROPERTIES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="BulletBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? BulletTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="NumberingBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? NumberingTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="ShapeBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? ShapeTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="UnderlineBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? UnderlineTemplate { get; set; }

	// --------------------------------------------------------------------------------------------------
	// PUBLIC RESOURCEKEY PROPERTIES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BulletTemplateResourceKey
		=> _bulletTemplateResourceKey ??= new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(BulletTemplateResourceKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey NumberingTemplateResourceKey
		=> _numberingTemplateResourceKey ??= new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(NumberingTemplateResourceKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey ShapeTemplateResourceKey
		=> _shapeTemplateResourceKey ??= new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(ShapeTemplateResourceKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey UnderlineTemplateResourceKey
		=> _underlineTemplateResourceKey ??= new ComponentResourceKey(typeof(CustomBarGalleryItemTemplateSelector), nameof(UnderlineTemplateResourceKey));

}
